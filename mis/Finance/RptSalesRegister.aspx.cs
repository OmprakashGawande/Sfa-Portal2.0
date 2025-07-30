using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Finance_RptSalesRegister : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
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
                    ddlOffice.Enabled = false;
                    divExcel.Visible = false;
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    string FY = GetCurrentFinancialYear();
                    string[] YEAR = FY.Split('-');
                    DateTime FromDate = new DateTime(int.Parse(YEAR[0]), 4, 1);
                    txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                    txtToDate.Attributes.Add("readonly", "readonly");

                    FillVoucherDate();
                    FillDropdown();
                    GetCommonSearch();
                }
                lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt");
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void FillDropdown()
    {
        try
        {
            if (ViewState["Office_ID"].ToString() == "1")
            {
                ddlOffice.Enabled = true;
            }
            ds = objdb.ByProcedure("SpFinRptTrialBalanceNew",
                   new string[] { "flag" },
                   new string[] { "0" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();

                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillVoucherDate()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtToDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    public static string GetCurrentFinancialYear()
    {
        int CurrentYear = DateTime.Today.Year;
        int PreviousYear = DateTime.Today.Year - 1;
        int NextYear = DateTime.Today.Year + 1;
        string PreYear = PreviousYear.ToString();
        string NexYear = NextYear.ToString();
        string CurYear = CurrentYear.ToString();
        string FinYear = null;

        if (DateTime.Today.Month > 3)
            FinYear = CurYear + "-" + NexYear;
        else
            FinYear = PreYear + "-" + CurYear;
        return FinYear.Trim();
    }

    protected void FillGridNextLedger()
    {
        try
        {
            GridView3.DataSource = new string[] { };
            GridView3.DataBind();
            string Office = "";
            string OfficeName = "";
            DivTable.InnerHtml = "";
            DivTable.InnerText = "";

            int SerialNo = 0;
            int totalListItem = ddlOffice.Items.Count;
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    SerialNo++;
                    Office += item.Value + ",";
                    OfficeName += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
                }
            }
            if (totalListItem == SerialNo)
            {
                OfficeName = "All Offices";
            }
            else if (SerialNo == 0)
            {
                OfficeName = "---Office Not Selected---";
            }
            else
            {
                OfficeName = OfficeName.Remove(OfficeName.Length - 1, 1);
            }
            //ds = objdb.ByProcedure("SpFinRptJournalRegister", new string[] { "flag", "Office_ID_Mlt", "VoucherTx_Name", "FromDate", "ToDate" }, new string[] { "3", Office, "Sales Voucher", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            ds = objdb.ByProcedure("SpFinRptJournalRegister", new string[] { "flag", "Office_ID_Mlt", "VoucherTx_Name", "FromDate", "ToDate" }, new string[] { "5", Office, "Sales Voucher", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                string headingFirst = "<p class='text-center' style='font-weight:600'>Sales Register <br /> MP State Agro Industries Development Corporation, <br/> [ " + OfficeName + " ] <br /></p>";
                lblHeading.Text = headingFirst;
                GridView3.DataSource = ds;
                divExcel.Visible = true;
                GridView3.DataBind();
                /*********************/
                GridView3.FooterRow.Cells[1].Text = "Total";
                GridView3.FooterRow.Cells[1].Font.Bold = true;
                GridView3.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;

                GridView3.FooterRow.Cells[2].Text = ds.Tables[1].Rows[0]["TVcCount"].ToString();
                GridView3.FooterRow.Cells[2].Font.Bold = true;
                GridView3.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                GridView3.FooterRow.Cells[3].Text = ds.Tables[1].Rows[0]["TCRAmount"].ToString();
                GridView3.FooterRow.Cells[3].Font.Bold = true;
                GridView3.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;


                GridView3.FooterRow.Cells[4].Text = ds.Tables[1].Rows[0]["TDRAmount"].ToString();
                GridView3.FooterRow.Cells[4].Font.Bold = true;
                GridView3.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                GridView3.FooterRow.Cells[5].Text = ds.Tables[1].Rows[0]["TClosingBalance"].ToString();
                GridView3.FooterRow.Cells[5].Font.Bold = true;
                GridView3.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;


                /*********************/
                //GridView4.DataSource = null;
                //GridView4.DataBind();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillGridNextLedgerMonth(string MonthID)
    {
        try
        {
            lblMsg.Text = "";
            GridView3.DataSource = null;
            GridView4.DataSource = null;
            string Office = "";
            string OfficeName = "";
            DivTable.InnerHtml = "";
            DivTable.InnerText = "";
            int SerialNo = 0;
            int totalListItem = ddlOffice.Items.Count;
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    SerialNo++;
                    Office += item.Value + ",";
                    OfficeName += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
                }
            }
            if (totalListItem == SerialNo)
            {
                OfficeName = "All Offices";
            }
            else if (SerialNo == 0)
            {
                OfficeName = "---Office Not Selected---";
            }
            else
            {
                OfficeName = OfficeName.Remove(OfficeName.Length - 1, 1);
            }
            string sDate = (Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd")).ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            int Month = int.Parse(datevalue.Month.ToString());
            int Year = int.Parse(datevalue.Year.ToString());
            int FY = Year;
            string FinancialYear = Year.ToString();
            string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
            FinancialYear = "";
            int FY_Start = FY;
            if (Month <= 3)
            {
                FY = Year - 1;
                FinancialYear = FY.ToString() + "-" + LFY.ToString();
                FY_Start = FY;
            }
            else
            {

                FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
            }
            DataSet ds1;
            //ds = objdb.ByProcedure("SpFinRptJournalRegister", new string[] { "flag", "Office_ID_Mlt", "VoucherTx_Name", "LedgerTx_Month", "FromDate", "ToDate" }, new string[] { "4", Office, "Sales Voucher", MonthID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
           // ds = objdb.ByProcedure("SpFinRptJournalRegister", new string[] { "flag", "Office_ID_Mlt", "VoucherTx_Name", "LedgerTx_Month", "FromDate", "ToDate" }, new string[] { "6", Office, "Sales Voucher", MonthID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            ds = objdb.ByProcedure("SpFinRptJournalRegister", new string[] { "flag", "Office_ID_Mlt", "VoucherTx_Name", "LedgerTx_Month", "FromDate", "ToDate", "FinancialYear" }, new string[] { "7", Office, "Sales Voucher", MonthID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), FinancialYear }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                GridView4.DataSource = ds;
                string headingFirst = "<p class='text-center' style='font-weight:600'>Sales Register <br /> MP State Agro Industries Development Corporation, <br/> [ " + OfficeName + " ] <br /></p>";
                lblHeading.Text = headingFirst;
                decimal CurrentBal = 0;
                decimal DebitTotal = 0;
                decimal CreditTotal = 0;
                int rowcount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < rowcount; i++)
                {

                    string DebitAmt = "0";
                    decimal CreditAmt = 0;

                    if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                    {
                        DebitAmt = "-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString();
                        DebitTotal = DebitTotal + decimal.Parse(ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                    {
                        CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                        CreditTotal = CreditTotal + decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                    }

                    CurrentBal = CurrentBal + decimal.Parse(DebitAmt) + CreditAmt;

                }





                StringBuilder htmlStr = new StringBuilder();

                htmlStr.Append("<table  id='DetailGrid' class='datatable table table-hover table-bordered' >");
                htmlStr.Append("<thead>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<th style='width: 65px;'>Voucher Date</th>");
                htmlStr.Append("<th>Particulars</th>");
                htmlStr.Append("<th>Vch Type</th>");
                htmlStr.Append("<th>Office Name</th>");
                htmlStr.Append("<th style='width:70px !important'>Vch No.</th>");
                htmlStr.Append("<th>Debit Amt.</th>");
                htmlStr.Append("<th>Credit Amt.</th>");
                htmlStr.Append("<th  class='Hiderow'>Action</th>");
                htmlStr.Append("</tr>");
                htmlStr.Append("</thead>");

                htmlStr.Append("<tbody>");

                string ValidDays = "No";
                int Count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                    ValidDays = "No";
                    ValidDays = ds.Tables[0].Rows[i]["V_Editright"].ToString();

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td style='vertical-align:top;text-align:left;'>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");

                    // Inner Transaction
                    ds1 = null;
                    ds1 = objdb.ByProcedure("SpFinRptJournalRegister", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", }, new string[] { "8", ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString(), ds.Tables[0].Rows[i]["Ledger_ID"].ToString() }, "dataset");
                    int Count1 = ds1.Tables[0].Rows.Count;

                    string Narration = ds1.Tables[1].Rows[0]["VoucherTx_Narration"].ToString();

                    if (Count1 > 1)
                    {

                        htmlStr.Append("<td  style='vertical-align:top;'>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                        htmlStr.Append("<table class='HideRecord' style='width:100%;'>");
                        if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                        {


                            for (int j = 0; j < Count1; j++)
                            {
                                //if (j == 0)
                                //{
                                //    htmlStr.Append("<p class='subledger HideRecord'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                                //}
                                //else
                                //{
                                //    htmlStr.Append("<p class='subledger HideRecord'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                                //}

                                //htmlStr.Append("\t \t \t<span class='Ledger_Amt HideRecord'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                //htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span></p>");
                                ////htmlStr.Append("<br/>");
                                htmlStr.Append("<tr>");

                                if (j == 0)
                                {

                                    htmlStr.Append("<td style='width:70%;border-top: 1px solid #ccc;'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</td>");
                                }
                                else
                                {
                                    htmlStr.Append("<td style='width:70%;border-top: 1px solid #ccc;'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</td>");
                                }

                                htmlStr.Append("<td style='width:30%;border-top: 1px solid #ccc;text-align: right;'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "&nbsp;&nbsp;" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</td>");
                                //htmlStr.Append("<br/>");
                                htmlStr.Append("</tr>");
                            }

                        }
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td colspan='2' style='border-top: 1px solid #ccc;'>");
                        htmlStr.Append("<b>Narration</b>\t : \t" + Narration + "");
                        htmlStr.Append("</td></tr>");
                        htmlStr.Append("</table></p>");
                        htmlStr.Append("</td>");
                    }
                    else
                    {
                        htmlStr.Append("<td  style='vertical-align:top;'>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                        htmlStr.Append("<p class='subledger HideRecord'><span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span></p>");
                        htmlStr.Append("</td>");
                    }


                    //  htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");


                    htmlStr.Append("<td style='vertical-align:top;'>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                    htmlStr.Append("<td style='vertical-align:top;'>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                    htmlStr.Append("<td style='vertical-align:top;'>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                    htmlStr.Append("<td class='align-right' style='vertical-align:top;'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                    htmlStr.Append("<td class='align-right' style='vertical-align:top;'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                    htmlStr.Append("<td  class='Hiderow' style='vertical-align:top;'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a> ");

                    if (ds.Tables[0].Rows[i]["Office_ID"].ToString() == ViewState["Office_ID"].ToString())
                    {
                        if (ValidDays != "No")
                        {
                            htmlStr.Append("<a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("2") + "' target='_blank'>Edit</a>");
                        }
                    }
                    htmlStr.Append("</td>");
                    htmlStr.Append("</tr>");
                }
                //htmlStr.Append("</tbody>");
                //htmlStr.Append("<tfoot>");

                //OPENING BALANCE TOTAL

                //CURRENT TOTAL
                htmlStr.Append("<tr>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td><b>TOTAL :</b></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");

                //DebitTotal = 0;
                //CreditTotal = 0;
                htmlStr.Append("<td class='align-right'><b>" + Math.Abs(DebitTotal).ToString() + "</b></td>");
                htmlStr.Append("<td class='align-right'><b>" + Math.Abs(CreditTotal).ToString() + "</b></td>");
                //if (CurrentBal < 0)
                //{
                //    htmlStr.Append("<td class='align-right'><b>" + Math.Abs(CurrentBal).ToString() + "</b></td>");
                //    htmlStr.Append("<td></td>");

                //}
                //else
                //{
                //    htmlStr.Append("<td></td>");
                //    htmlStr.Append("<td class='align-right'><b>" + Math.Abs(CurrentBal).ToString() + "</b></td>");
                //}
                htmlStr.Append("<td  class='Hiderow'></td>");
                htmlStr.Append("</tr>");
                htmlStr.Append("</tbody>");
                //htmlStr.Append("</tfoot>");
                htmlStr.Append("</table>");

                DivTable.InnerHtml = htmlStr.ToString();
            }
            //GridView4.DataBind();
            //GridView4.HeaderRow.TableSection = TableRowSection.TableHeader;
            //GridView4.UseAccessibleHeader = true;
            ///*********************/
            //GridView4.FooterRow.Cells[1].Text = "Total";
            //GridView4.FooterRow.Cells[1].Font.Bold = true;
            //GridView4.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;


            //GridView4.FooterRow.Cells[5].Text = ds.Tables[1].Rows[0]["TDebitAmt"].ToString();
            //GridView4.FooterRow.Cells[5].Font.Bold = true;
            //GridView4.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;

            //GridView4.FooterRow.Cells[6].Text = ds.Tables[1].Rows[0]["TCreditAmt"].ToString();
            //GridView4.FooterRow.Cells[6].Font.Bold = true;
            //GridView4.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;


            /*********************/
            GridView3.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            lblMsg.Text = "";
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView3.Rows[index];

                Label lblMonthID = (Label)row.Cells[0].FindControl("lblMonthID");


                string MonthID = lblMonthID.Text;

                FillGridNextLedgerMonth(MonthID);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                //lblHeading.Text = "Sales Register";
                lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView4_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {

        try
        {
            lblMsg.Text = "";
            string VoucherTx_ID = GridView4.DataKeys[e.RowIndex].Value.ToString();

            objdb.ByProcedure("SpFinVoucherTx",
                   new string[] { "flag", "VoucherTx_ID", "Emp_ID" },
                   new string[] { "12", VoucherTx_ID, ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Deleted.");

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            lblMsg.Text = "";
            DivTable.InnerHtml = "";
            DivTable.InnerText = "";
            FillGridNextLedger();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            lblHeading.Text = "Sales Register";
            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DivTable.InnerHtml = "";
            DivTable.InnerText = "";
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                lblMsg.Text = "";
                string Office = "";
                foreach (ListItem item in ddlOffice.Items)
                {
                    if (item.Selected)
                    {
                        Office += item.Value + ",";
                    }
                }
                if (Office != "")
                {
                    /*****************SET Search Option Start********************/
                    #region SetSearchOption
                    Session["CommonOffice"] = Office;
                    Session["CommonFromDate"] = txtFromDate.Text;
                    Session["CommonToDate"] = txtToDate.Text;
                    #endregion
                    /*****************SET Search Option End********************/

                    FillGridNextLedger();
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    //lblHeading.Text = "Sales Register";
                    lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
                }
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select at least one Office.');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "Editing")
            {
                string VoucherTx_ID = e.CommandArgument.ToString();
                DataSet dsPageURL = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "VoucherTx_ID" },
                    new string[] { "30", VoucherTx_ID },
                    "dataset");

                if (dsPageURL != null)
                {

                    string Url = dsPageURL.Tables[0].Rows[0]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                    Url = Url + "&Action=" + objdb.Encrypt("2");
                    Response.Redirect(Url);

                }
            }
            if (e.CommandName == "View")
            {
                string VoucherTx_ID = e.CommandArgument.ToString();
                DataSet dsPageURL = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "VoucherTx_ID" },
                    new string[] { "30", VoucherTx_ID },
                    "dataset");

                if (dsPageURL != null)
                {

                    string Url = dsPageURL.Tables[0].Rows[0]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                    Url = Url + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(dsPageURL.Tables[1].Rows[0]["Office_ID"].ToString());

                    Response.Redirect(Url);

                }

            }
            if (e.CommandName == "Print")
            {

                string VoucherTx_ID = e.CommandArgument.ToString();
                ds = objdb.ByProcedure("SpFinVoucherTx",
                  new string[] { "flag", "VoucherTx_ID" },
                  new string[] { "31", VoucherTx_ID },
                  "dataset");

                if (ds != null)
                {
                    string VoucherTx_Type = ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString();
                    if (VoucherTx_Type == "Payment" || VoucherTx_Type == "Contra" || VoucherTx_Type == "GSTService Purchase" || VoucherTx_Type == "Cash Payment")
                    {

                        string Url = "VoucherContraInvoice.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                        Response.Redirect(Url);


                    }
                    else if (VoucherTx_Type == "Receipt" || VoucherTx_Type == "Journal" || VoucherTx_Type == "Bank Receipt" || VoucherTx_Type == "Journal HO")
                    {

                        string Url = "VoucherJournalInvoice.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                        Response.Redirect(Url);



                    }
                    else
                    {

                    }

                }




            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btngraphical_Click(object sender, EventArgs e)
    {
        try
        {
            string OfficeID = "";
            string Office = "";
            string OfficeName = "";
            int SerialNo = 0;
            string headingFirst = "";
            int totalListItem = ddlOffice.Items.Count;
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    SerialNo++;
                    Office += item.Value + ",";
                    OfficeName += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
                }
            }

            if (Office != "")
            {
                if (totalListItem == SerialNo)
                {
                    OfficeName = "All Offices";
                }
                else if (SerialNo == 0)
                {
                    OfficeName = "---Office Not Selected---";
                }
                else
                {
                    OfficeName = OfficeName.Remove(OfficeName.Length - 1, 1);
                }
                headingFirst = "<p class='text-center' style='font-weight:600'>Sales Register<br /> MP State Agro Industries Development Corporation, <br/> [ " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";

                Session["SalesHeading"] = headingFirst.ToString();
                foreach (ListItem item in ddlOffice.Items)
                {
                    if (item.Selected)
                    {
                        OfficeID += item.Value + ",";
                    }
                }
                OfficeID = objdb.Encrypt(OfficeID);
                string FromDate = objdb.Encrypt(Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"));
                string ToDate = objdb.Encrypt(Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"));

                string url = "RptGraphicalSalesRegister.aspx?OfficeID=" + OfficeID + "&FromDate=" + FromDate + "&ToDate=" + ToDate;

                StringBuilder sb = new StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.open('");
                sb.Append(url);
                sb.Append("', '_blank');");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select atleast one Office.');", true);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetCommonSearch()
    {
        try
        {
            if (Session["CommonOffice"] != null)
            {
                string Office = Session["CommonOffice"].ToString();
                string[] OfficeList = Office.Split(new Char[] { ',' });
                if (OfficeList.Count() > 0)
                {
                    ddlOffice.ClearSelection();
                    foreach (string OfficeID in OfficeList)
                    {
                        // ddlOffice.SelectedValue = OfficeID.ToString();
                        foreach (ListItem item in ddlOffice.Items)
                        {
                            if (item.Value == OfficeID)
                                item.Selected = true;
                        }
                    }
                }

            }
            if (Session["CommonFromDate"] != null)
            {
                string FromDate = Session["CommonFromDate"].ToString();
                txtFromDate.Text = FromDate.ToString();
            }
            if (Session["CommonToDate"] != null)
            {
                string ToDate = Session["CommonToDate"].ToString();
                txtToDate.Text = ToDate.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
   
}
