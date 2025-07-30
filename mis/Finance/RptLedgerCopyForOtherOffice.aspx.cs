using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
public partial class mis_Finance_RptLedgerCopyForOtherOffice : System.Web.UI.Page
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
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    string FY = GetCurrentFinancialYear();
                    string[] YEAR = FY.Split('-');
                    DateTime FromDate = new DateTime(int.Parse(YEAR[0]), 4, 1);
                    txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                    txtToDate.Attributes.Add("readonly", "readonly");


                    FillVoucherDate();
                    FillFromDate();

                    FillDropdown();
                    HeadList();
                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        ddlOffice.Enabled = false;
                    }
                    btnBack.Enabled = false;
                    btnBackN.Enabled = false;

                    chkOpeningBal.Checked = true;
                    chkDebitAmt.Checked = true;
                    chkCreditAmt.Checked = true;
                    chkClosingBal.Checked = true;
                    chkNarration.Checked = true;
                    chkOppositeLedger.Checked = true;

                    ViewState["DayBookVisible"] = "true";
                    btnShowDetailBook.Enabled = false;

                }
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
    protected void FillFromDate()
    {
        try
        {
            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd")));
            //String dy = datevalue.Day.ToString();
            int mn = datevalue.Month;
            int yy = datevalue.Year;
            if (mn < 4)
            {
                txtFromDate.Text = "01/04/" + (yy - 1).ToString();
            }
            else
            {
                txtFromDate.Text = "01/04/" + (yy).ToString();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void HeadList()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Head_ID", typeof(string)));
            dt.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
            dt.Columns.Add(new DataColumn("MonthID", typeof(string)));
            ViewState["Heads"] = dt;
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
                //ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
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
                DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary", new string[] { "flag", "Office_ID" }, new string[] { "6", "1" }, "dataset");
                if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                {
                    ddlLedger.DataSource = ds1;
                    ddlLedger.DataTextField = "Ledger_Name";
                    ddlLedger.DataValueField = "Ledger_ID";
                    ddlLedger.DataBind();
                    ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

                }
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
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlLedger.DataSource = null;
            ddlLedger.DataBind();
            DivTable.InnerHtml = "";
            btnBack.Enabled = false;
            btnBackN.Enabled = false;
            lblTab.Text = "";
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
                DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary", new string[] { "flag", "Office_ID_Mlt" }, new string[] { "1", Office }, "dataset");
                if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                {
                    ddlLedger.DataSource = ds1;
                    ddlLedger.DataTextField = "Ledger_Name";
                    ddlLedger.DataValueField = "Ledger_ID";
                    ddlLedger.DataBind();
                    ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

                }
            }
            //if (ddlOffice.SelectedIndex > 0)
            //{
            //    DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary", new string[] { "flag", "Office_ID" }, new string[] { "1", ddlOffice.SelectedValue.ToString() }, "dataset");
            //    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            //    {
            //        ddlLedger.DataSource = ds1;
            //        ddlLedger.DataTextField = "Ledger_Name";
            //        ddlLedger.DataValueField = "Ledger_ID";
            //        ddlLedger.DataBind();
            //        ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

            //    }
            //}

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlLedger_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblReportName.Text = "";
            DivTable.InnerHtml = "";
            btnBack.Enabled = false;
            btnBackN.Enabled = false;
            lblTab.Text = "";
            //if (ddlOffice.SelectedIndex >0 && ddlLedger.SelectedIndex > 0 && txtFromDate.Text !="" && txtToDate.Text !="")
            //{
            //    lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
            //    FillGrid();
            //}

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            ViewState["DayBookVisible"] = "true";
            btnShowDetailBook.Enabled = false;
            GridView4.DataSource = null;
            GridView4.DataBind();

            DivTable.InnerHtml = "";
            string Office = "1";
            //foreach (ListItem item in ddlOffice.Items)
            //{
            //    if (item.Selected)
            //    {
            //        Office += item.Value + ",";
            //    }
            //}


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
            DateTime StartDate = new DateTime(FY_Start, 4, 1);
            string FY_StartDate = StartDate.ToString("dd/MM/yyyy");

           // ds = objdb.ByProcedure("SpFinRptLedgerSummary", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate", "FinancialYear", "FY_StartDate" }, new string[] { "2", Office, ddlLedger.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), FinancialYear, Convert.ToDateTime(FY_StartDate, cult).ToString("yyyy/MM/dd") }, "dataset");
            ds = objdb.ByProcedure("SpFinRptLedgerSummary", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate", "FinancialYear", "FY_StartDate" }, new string[] { "2", Office, ddlLedger.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), FinancialYear, Convert.ToDateTime(FY_StartDate, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                //string clsHide = "";
                //if (chkClosingBal.Checked =true)
                //    clsHide = "hidden";
                btnShowDetailBook.Enabled = true;
                DivTable.Visible = true;

                btnBack.Enabled = true;
                btnBackN.Enabled = true;
                StringBuilder htmlStr = new StringBuilder();


                htmlStr.Append("<table  id='DetailGrid' class='datatable table table-hover table-bordered' >");
                htmlStr.Append("<thead>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<th>Voucher Date</th>");
                htmlStr.Append("<th>Particulars</th>");
                htmlStr.Append("<th>Vch Type</th>");
                htmlStr.Append("<th>Vch No.</th>");
                if (chkDebitAmt.Checked == true)
                    htmlStr.Append("<th>Debit Amt.</th>");
                if (chkCreditAmt.Checked == true)
                    htmlStr.Append("<th>Credit Amt.</th>");

                int Count = ds.Tables[0].Rows.Count;
                decimal OpeningBalance = 0;
                decimal TDebitAmt = 0;
                decimal TCreditAmt = 0;
                string AmtType = "";
                decimal PreOpening = 0;


                //if (ds.Tables[0].Rows[0]["OpeningBalance"].ToString() != "")
                //    OpeningBalance = decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString());

                //ViewState["OpeningBalance"] = OpeningBalance.ToString();

                if (ds.Tables[0].Rows[0]["OpeningBalance"].ToString() != "")
                    OpeningBalance = decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString());

                if (ds.Tables[1].Rows[0]["PreOpening"].ToString() != "")
                    PreOpening = decimal.Parse(ds.Tables[1].Rows[0]["PreOpening"].ToString());

                if (chkOpeningBal.Checked == true)
                {
                    OpeningBalance = OpeningBalance + PreOpening;
                }

                ViewState["OpeningBalance"] = OpeningBalance.ToString();


                if (chkClosingBal.Checked == true)
                    htmlStr.Append("<th class='ClosingBal'>Closing Bal.</th>");
                //htmlStr.Append("<th class='ClosingBal'>Closing Bal. <br />\n<p class='subledger'>" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString())) + " " + AmtType + "</p></th>");
                htmlStr.Append("<th style='width: 65px;'  class='hide_print'>Action</th>");
                htmlStr.Append("</tr>");
                htmlStr.Append("</thead>");
                htmlStr.Append("<tbody>");

                if (chkOpeningBal.Checked == true)
                {
                    //if (OpeningBalance >= 0)
                    //    lblReportName.Text = lblReportName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                    //else
                    //    lblReportName.Text = lblReportName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";

                    if (OpeningBalance >= 0)
                    {
                        lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                        HF_ReportName.Value = "Ledger Of - " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Bal - " + Math.Abs(OpeningBalance).ToString() + " Cr";
                    }
                    else
                    {
                        lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                        HF_ReportName.Value = "Ledger Of - " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Bal - " + Math.Abs(OpeningBalance).ToString() + " Dr";
                    }
                }
                else
                {
                    OpeningBalance = 0;
                }
                //Opening Balance Row Start
                if (chkOpeningBal.Checked == true)
                {
                    htmlStr.Append("<tr style='background-color: antiquewhite; font-weight: 700;'>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Opening Balance</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");


                    if (chkDebitAmt.Checked == true)
                        htmlStr.Append("<td></td>");
                    if (chkCreditAmt.Checked == true)
                        htmlStr.Append("<td></td>");
                    if (OpeningBalance >= 0)
                        htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                    else
                        htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");

                    htmlStr.Append("<td class='hide_print'></td>");
                    htmlStr.Append("</tr>");
                }
                //Opening Balance Row End
                string ValidDays = "No";
                for (int i = 0; i < Count; i++)
                {
                    decimal DebitAmt = 0;
                    decimal CreditAmt = 0;
                    decimal OpeningBal = 0;
                    decimal tempOpeningBal = 0;
                    ValidDays = "No";
                    ValidDays = ds.Tables[0].Rows[i]["V_Editright"].ToString();

                    if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                        DebitAmt = decimal.Parse("-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                    if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                        CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());

                    if (chkCreditAmt.Checked == true && chkDebitAmt.Checked == true)
                    {
                        tempOpeningBal = OpeningBalance;
                        OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                        OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                        TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                        TCreditAmt = TCreditAmt + CreditAmt;

                        if (OpeningBal >= 0)
                            AmtType = "Cr";
                        else
                            AmtType = "Dr";


                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                        if (chkDebitAmt.Checked == true)
                            htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                        if (chkCreditAmt.Checked == true)
                            htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                        if (chkClosingBal.Checked == true)
                            htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");

                        htmlStr.Append("<td class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a> ");

                        htmlStr.Append("</td>");

                        htmlStr.Append("</tr>");
                    }
                    else if (chkCreditAmt.Checked == true && ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                    {
                        decimal CreditAmt1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                        if (CreditAmt1 > 0)
                        {
                            tempOpeningBal = OpeningBalance;
                            OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                            OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                            TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                            TCreditAmt = TCreditAmt + CreditAmt;

                            if (OpeningBal >= 0)
                                AmtType = "Cr";
                            else
                                AmtType = "Dr";

                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");

                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                            if (chkDebitAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'></td>");
                            if (chkCreditAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                            if (chkClosingBal.Checked == true)
                                htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");
                            htmlStr.Append("<td class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a> ");

                            htmlStr.Append("</td>");

                            htmlStr.Append("</tr>");
                        }

                    }
                    else if (chkDebitAmt.Checked == true && ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                    {
                        decimal DebitAmt1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                        if (DebitAmt1 > 0)
                        {
                            tempOpeningBal = OpeningBalance;
                            OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                            OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                            TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                            TCreditAmt = TCreditAmt + CreditAmt;

                            if (OpeningBal >= 0)
                                AmtType = "Cr";
                            else
                                AmtType = "Dr";

                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                            if (chkDebitAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                            if (chkCreditAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'></td>");
                            if (chkClosingBal.Checked == true)
                                htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");
                            htmlStr.Append("<td class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a> ");

                            htmlStr.Append("</td>");


                            htmlStr.Append("</tr>");
                        }
                    }

                }


                //htmlStr.Append("</tbody>");
                //htmlStr.Append("<tfoot>");
                htmlStr.Append("<tr style='font-weight: 700;'>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Total :</td>");

                if (decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString()) >= 0)
                    AmtType = "Cr";
                else
                    AmtType = "Dr";

                htmlStr.Append("<td></td>");

                if (OpeningBalance >= 0)
                    AmtType = "Cr";
                else
                    AmtType = "Dr";
                if (chkDebitAmt.Checked == true)
                    htmlStr.Append("<td class='align-right'>" + TDebitAmt.ToString() + "</td>");
                if (chkCreditAmt.Checked == true)
                    htmlStr.Append("<td class='align-right'>" + TCreditAmt.ToString() + "</td>");
                if (chkClosingBal.Checked == true && chkDebitAmt.Checked == false && chkCreditAmt.Checked == false)
                {
                    decimal? Cr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
                    decimal? Dr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));

                    Dr = decimal.Parse("-" + Dr.ToString());
                    OpeningBalance = OpeningBalance + decimal.Parse(Cr.ToString()) + decimal.Parse(Dr.ToString());
                    if (OpeningBalance >= 0)
                    {
                        htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                    }
                    else
                    {
                        htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");
                    }

                }
                else if (chkClosingBal.Checked == true)
                {
                    htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " " + AmtType + "</td>");
                }

                htmlStr.Append("<td class='hide_print'></td>");
                htmlStr.Append("</tr>");

                //htmlStr.Append("</tfoot>");
                htmlStr.Append("</tbody>");
                htmlStr.Append("</table>");

                DivTable.InnerHtml = htmlStr.ToString();

            }
            else if (ds.Tables.Count != 0 && ds.Tables[1].Rows.Count != 0)
            {
                decimal OpeningBalance = 0;

                if (ds.Tables[1].Rows[0]["OpeningBalance"].ToString() != "")
                    OpeningBalance = decimal.Parse(ds.Tables[1].Rows[0]["OpeningBalance"].ToString());
                if (chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                {
                    if (OpeningBalance >= 0)
                    {
                        lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>  & " + "<span style='color:#d03535;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                        HF_ReportName.Value = "Ledger Of - " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Bal - " + Math.Abs(OpeningBalance).ToString() + " Cr  & " + " Closing Bal - " + Math.Abs(OpeningBalance).ToString() + " Cr";
                    }
                    else
                    {
                        lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span> & " + "<span style='color:#d03535;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                        HF_ReportName.Value = "Ledger Of - " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Bal - " + Math.Abs(OpeningBalance).ToString() + " Dr & " + " Closing Bal - " + Math.Abs(OpeningBalance).ToString() + " Dr";
                    }
                }
                else if (chkOpeningBal.Checked == true)
                {
                    if (OpeningBalance >= 0)
                    {
                        lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                        HF_ReportName.Value = "Ledger Of - " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Bal - " + Math.Abs(OpeningBalance).ToString() + " Cr";
                    }
                    else
                    {
                        lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                        HF_ReportName.Value = "Ledger Of - " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Bal - " + Math.Abs(OpeningBalance).ToString() + " Dr";
                    }
                }
                else if (chkClosingBal.Checked == true)
                {
                    if (OpeningBalance >= 0)
                    {
                        lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                        HF_ReportName.Value = "Ledger Of - " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Closing Bal. - " + Math.Abs(OpeningBalance).ToString() + " Cr";
                    }
                    else
                    {
                        lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                        HF_ReportName.Value = "Ledger Of - " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Closing Bal. - " + Math.Abs(OpeningBalance).ToString() + " Dr";
                    }
                }
            }
            else
            {
                lblTab.Text = "No record found.";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillGridDetail()
    {
        try
        {
            ViewState["DayBookVisible"] = "true";
            btnShowDetailBook.Enabled = false;
            GridView4.DataSource = null;
            GridView4.DataBind();

            DivTable.InnerHtml = "";
            string Office = "1";
            //foreach (ListItem item in ddlOffice.Items)
            //{
            //    if (item.Selected)
            //    {
            //        Office += item.Value + ",";
            //    }
            //}

            string sDate = (Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd")).ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            int Month = int.Parse(datevalue.Month.ToString());
            int Year = int.Parse(datevalue.Year.ToString());
            int FY = Year;
            string FinancialYear = Year.ToString();
            string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
            FinancialYear = "";
            if (Month <= 3)
            {
                FY = Year - 1;
                FinancialYear = FY.ToString() + "-" + LFY.ToString();
            }
            else
            {

                FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
            }

            ds = objdb.ByProcedure("SpFinRptLedgerSummary", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate", "FinancialYear" }, new string[] { "2", Office, ddlLedger.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), FinancialYear }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                btnShowDetailBook.Enabled = true;
                DivTable.Visible = true;

                btnBack.Enabled = true;
                btnBackN.Enabled = true;
                StringBuilder htmlStr = new StringBuilder();


                htmlStr.Append("<table  id='DetailGrid' class='datatable table table-hover table-bordered' >");
                htmlStr.Append("<thead>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<th style='width: 65px;'>Voucher Date</th>");
                htmlStr.Append("<th>Particulars</th>");
                htmlStr.Append("<th>Vch Type</th>");
                htmlStr.Append("<th style='min-width: 10%;'>Vch No.</th>");
                if (chkDebitAmt.Checked == true)
                    htmlStr.Append("<th>Debit Amt.</th>");
                if (chkCreditAmt.Checked == true)
                    htmlStr.Append("<th>Credit Amt.</th>");

                int Count = ds.Tables[0].Rows.Count;
                decimal OpeningBalance = 0;
                decimal TDebitAmt = 0;
                decimal TCreditAmt = 0;
                string AmtType = "";

                if (ds.Tables[0].Rows[0]["OpeningBalance"].ToString() != "")
                    OpeningBalance = decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString());

                ViewState["OpeningBalance"] = OpeningBalance.ToString();
                if (chkClosingBal.Checked == true)
                    htmlStr.Append("<th class='ClosingBal'>Closing Bal.</th>");
                // htmlStr.Append("<th class='ClosingBal'>Closing Bal. <br />\n<p class='subledger'>" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString())) + " " + AmtType + "</p></th>");

                htmlStr.Append("<th class='hide_print' style='width: 65px;'>Action</th>");
                htmlStr.Append("</tr>");
                htmlStr.Append("</thead>");
                htmlStr.Append("<tbody>");

                if (chkOpeningBal.Checked == true)
                {

                    //if (OpeningBalance >= 0)
                    //    lblReportName.Text = lblReportName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                    //else
                    //    lblReportName.Text = lblReportName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                    if (OpeningBalance >= 0)
                    {
                        lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                        HF_ReportName.Value = "Ledger Of - " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Bal - " + Math.Abs(OpeningBalance).ToString() + " Cr";
                    }
                    else
                    {
                        lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                        HF_ReportName.Value = "Ledger Of - " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Bal - " + Math.Abs(OpeningBalance).ToString() + " Dr";
                    }
                }
                else
                {
                    OpeningBalance = 0;
                }
                //Opening Balance Row Start
                if (chkOpeningBal.Checked == true)
                {
                    htmlStr.Append("<tr style='background-color: antiquewhite; font-weight: 700;'>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Opening Balance</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");


                    if (chkDebitAmt.Checked == true)
                        htmlStr.Append("<td></td>");
                    if (chkCreditAmt.Checked == true)
                        htmlStr.Append("<td></td>");

                    if (OpeningBalance >= 0)
                        htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                    else
                        htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");

                    htmlStr.Append("<td class='hide_print'></td>");
                    htmlStr.Append("</tr>");
                }
                //Opening Balance Row End
                string ValidDays = "No";
                for (int i = 0; i < Count; i++)
                {
                    decimal DebitAmt = 0;
                    decimal CreditAmt = 0;
                    decimal OpeningBal = 0;
                    decimal tempOpeningBal = 0;

                    ValidDays = "No";
                    ValidDays = ds.Tables[0].Rows[i]["V_Editright"].ToString();


                    if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                        DebitAmt = decimal.Parse("-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                    if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                        CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());


                    DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", }, new string[] { "3", ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString(), ddlLedger.SelectedValue.ToString() }, "dataset");
                    int Count1 = ds1.Tables[0].Rows.Count;

                    string Narration = ds1.Tables[1].Rows[0]["VoucherTx_Narration"].ToString();
                    if (chkCreditAmt.Checked == true && chkDebitAmt.Checked == true)
                    {
                        tempOpeningBal = OpeningBalance;
                        OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                        OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                        TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                        TCreditAmt = TCreditAmt + CreditAmt;

                        if (OpeningBal >= 0)
                            AmtType = "Cr";
                        else
                            AmtType = "Dr";



                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                        if (Count1 > 1)
                        {

                            htmlStr.Append("<td>");
                            if (chkOppositeLedger.Checked == true)
                            {
                                htmlStr.Append("(As per Details) <br/>");
                                if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                {

                                    for (int j = 0; j < Count1; j++)
                                    {
                                        htmlStr.Append("\n<p class='subledger'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                                        htmlStr.Append("\t \t \t<span class='Ledger_Amt'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                        htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span></p>");
                                        htmlStr.Append("<br/>");

                                    }
                                }
                            }
                            htmlStr.Append("\n<p class='subledger'>");
                            if (chkNarration.Checked == true)
                                htmlStr.Append("<span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span>");

                            htmlStr.Append("</p>");
                            htmlStr.Append("</td>");
                        }
                        else
                        {
                            if (chkOppositeLedger.Checked == true)
                            {
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                            }
                            else
                            {
                                htmlStr.Append("<td>");
                            }
                            htmlStr.Append("\n<p class='subledger'>");
                            if (chkNarration.Checked == true)
                                htmlStr.Append("<span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span>");
                            htmlStr.Append("</p>");
                            htmlStr.Append("</td>");
                        }
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                        if (chkDebitAmt.Checked == true)
                            htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                        if (chkCreditAmt.Checked == true)
                            htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                        if (chkClosingBal.Checked == true)
                            htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");

                        htmlStr.Append("<td class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a> ");

                        htmlStr.Append("</td>");

                        htmlStr.Append("</tr>");
                    }
                    else if (chkCreditAmt.Checked == true && ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                    {
                        decimal CreditAmt1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                        if (CreditAmt1 > 0)
                        {
                            tempOpeningBal = OpeningBalance;
                            OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                            OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                            TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                            TCreditAmt = TCreditAmt + CreditAmt;

                            if (OpeningBal >= 0)
                                AmtType = "Cr";
                            else
                                AmtType = "Dr";

                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                            if (Count1 > 1)
                            {

                                htmlStr.Append("<td>");
                                if (chkOppositeLedger.Checked == true)
                                {
                                    htmlStr.Append("(As per Details) <br/>");
                                    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                    {

                                        for (int j = 0; j < Count1; j++)
                                        {


                                            htmlStr.Append("\n<p class='subledger'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                                            htmlStr.Append("\t \t \t<span class='Ledger_Amt'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                            htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span></p>");
                                            htmlStr.Append("<br/>");

                                        }
                                    }
                                }

                                htmlStr.Append("\n<p class='subledger'>");
                                if (chkNarration.Checked == true)
                                    htmlStr.Append("<span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span>");
                                htmlStr.Append("</p>");
                                htmlStr.Append("</td>");
                            }
                            else
                            {
                                // htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");

                                if (chkOppositeLedger.Checked == true)
                                {
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                                }
                                else
                                {
                                    htmlStr.Append("<td>");
                                }
                                htmlStr.Append("\n<p class='subledger'>");
                                if (chkNarration.Checked == true)
                                    htmlStr.Append("<span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span>");
                                htmlStr.Append("</p>");
                                htmlStr.Append("</td>");
                            }
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                            if (chkDebitAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'></td>");
                            if (chkCreditAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                            if (chkClosingBal.Checked == true)
                                htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");
                            htmlStr.Append("<td class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a> ");

                            htmlStr.Append("</td>");


                            htmlStr.Append("</tr>");



                        }

                    }
                    else if (chkDebitAmt.Checked == true && ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                    {
                        decimal DebitAmt1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                        if (DebitAmt1 > 0)
                        {
                            tempOpeningBal = OpeningBalance;
                            OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                            OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                            TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                            TCreditAmt = TCreditAmt + CreditAmt;

                            if (OpeningBal >= 0)
                                AmtType = "Cr";
                            else
                                AmtType = "Dr";



                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                            if (Count1 > 1)
                            {

                                htmlStr.Append("<td>");
                                if (chkOppositeLedger.Checked == true)
                                {
                                    htmlStr.Append("(As per Details) <br/>");
                                    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                    {

                                        for (int j = 0; j < Count1; j++)
                                        {


                                            htmlStr.Append("\n<p class='subledger'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                                            htmlStr.Append("\t \t \t<span class='Ledger_Amt'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                            htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span></p>");
                                            htmlStr.Append("<br/>");

                                        }
                                    }
                                }
                                //htmlStr.Append("\n<p class='subledger'><span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span></p>");
                                htmlStr.Append("\n<p class='subledger'>");
                                if (chkNarration.Checked == true)
                                    htmlStr.Append("<span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span>");
                                htmlStr.Append("</p>");

                                htmlStr.Append("</td>");
                            }
                            else
                            {
                                // htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");

                                if (chkOppositeLedger.Checked == true)
                                {
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                                }
                                else
                                {
                                    htmlStr.Append("<td>");
                                }
                                htmlStr.Append("\n<p class='subledger'>");
                                if (chkNarration.Checked == true)
                                    htmlStr.Append("<span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span>");
                                htmlStr.Append("</p>");
                                htmlStr.Append("</td>");
                            }
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                            if (chkDebitAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                            if (chkCreditAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'></td>");
                            if (chkClosingBal.Checked == true)
                                htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");

                            htmlStr.Append("<td class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a> ");

                            htmlStr.Append("</td>");



                            htmlStr.Append("</tr>");
                        }
                    }
                }

                //htmlStr.Append("</tbody>");
                //htmlStr.Append("<tfoot>");
                htmlStr.Append("<tr style='font-weight: 700;'>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Total :</td>");

                if (decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString()) >= 0)
                    AmtType = "Cr";
                else
                    AmtType = "Dr";

                htmlStr.Append("<td></td>");

                if (OpeningBalance >= 0)
                    AmtType = "Cr";
                else
                    AmtType = "Dr";
                if (chkDebitAmt.Checked == true)
                    htmlStr.Append("<td class='align-right'>" + TDebitAmt.ToString() + "</td>");
                if (chkCreditAmt.Checked == true)
                    htmlStr.Append("<td class='align-right'>" + TCreditAmt.ToString() + "</td>");
                if (chkClosingBal.Checked == true && chkDebitAmt.Checked == false && chkCreditAmt.Checked == false)
                {
                    decimal? Cr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
                    decimal? Dr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));

                    Dr = decimal.Parse("-" + Dr.ToString());
                    OpeningBalance = OpeningBalance + decimal.Parse(Cr.ToString()) + decimal.Parse(Dr.ToString());
                    if (OpeningBalance >= 0)
                    {
                        htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                    }
                    else
                    {
                        htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");
                    }

                }
                else if (chkClosingBal.Checked == true)
                    htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " " + AmtType + "</td>");
                htmlStr.Append("<td class='hide_print'></td>");
                htmlStr.Append("</tr>");

                //htmlStr.Append("</tfoot>");
                htmlStr.Append("</tbody>");
                htmlStr.Append("</table>");

                DivTable.InnerHtml = htmlStr.ToString();
            }
            else
            {
                lblTab.Text = "No record found.";

            }

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
            lblTab.Text = "";
            lblReportName.Text = "";
            if (ddlLedger.SelectedIndex > 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            {
                FillGridDetail();
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnBackN_Click(object sender, EventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            lblMsg.Text = "";
            DivTable.InnerHtml = "";
            lblTab.Text = "";
            lblReportName.Text = "";
            if (ddlLedger.SelectedIndex > 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            {
                FillGrid();
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

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
            var watch = System.Diagnostics.Stopwatch.StartNew();

            lblMsg.Text = "";
            lblReportName.Text = "";
            DivTable.InnerHtml = "";
            btnBack.Enabled = false;
            btnBackN.Enabled = false;
            //if (ddlLedger.SelectedIndex > 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            //{
            //    lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
            //    FillGrid();
            //}



            string msg = "";
            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            if (Office == "")
            {
                msg = "Select at least one Office.";
            }
            if (ddlLedger.SelectedIndex == 0)
            {
                msg += "Select List Of Ledger.\\n";
            }
            if (msg == "")
            {
                if (txtFromDate.Text != "" && txtToDate.Text != "")
                {
                    lblReportName.Text = "Ledger Of : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
                    HF_ReportName.Value = "Ledger Of - " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
                    FillGrid();
                }

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;

                lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "hideshow();", true);
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
            string msg = "";
            string Office = "";
            string OfficeID = "";
            string heading = "";

            ViewState["DayBookVisible"] = "true";
            btnShowDetailBook.Enabled = false;
            GridView4.DataSource = null;
            GridView4.DataBind();


            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            if (Office == "")
            {
                msg = "Select at least one Office.\\n";
            }
            if (ddlLedger.SelectedIndex == 0)
            {
                msg += "Select List Of Ledger.";
            }
            if (msg == "")
            {

                int totalListItem = ddlOffice.Items.Count;
                heading = "<p class='text-center' style='font-weight:600'>Closing Balance : " + ddlLedger.SelectedItem.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )</p>";
                Session["heading"] = heading.ToString();
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
                string Ledger_ID = objdb.Encrypt(ddlLedger.SelectedValue.ToString());
                string url = "RptGraphicalLedgerSummary.aspx?OfficeID=" + OfficeID + "&FromDate=" + FromDate + "&ToDate=" + ToDate + "&Ledger_ID=" + Ledger_ID;

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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void btnShowDetailBook_Click(object sender, EventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            if (ViewState["DayBookVisible"].ToString() == "true")
            {
                DivTable.Visible = false;
                ViewState["DayBookVisible"] = "false";



                GridView4.DataSource = null;
                GridView4.DataBind();


                string Office = "";

                foreach (ListItem item in ddlOffice.Items)
                {
                    if (item.Selected)
                    {
                        Office += item.Value + ",";
                    }
                }


                ds = objdb.ByProcedure("SpFinRptLedgerSummary", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate" }, new string[] { "4", Office, ddlLedger.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {

                    GridView4.Visible = true;

                    GridView4.DataSource = ds;
                    GridView4.DataBind();

                    decimal PreOpening = decimal.Parse(ViewState["OpeningBalance"].ToString());
                    decimal OpeningBal = 0;
                    string PreClosing = "";



                    int rowcount = ds.Tables[0].Rows.Count;

                    for (int i = 0; i < rowcount; i++)
                    {

                        string DebitAmt = "0";
                        decimal CreditAmt = 0;

                        //if (i != 0)
                        //{
                        if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                            DebitAmt = "-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString();

                        if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                            CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                        //}

                        if (i == 0)
                        {

                            OpeningBal = PreOpening + decimal.Parse(DebitAmt) + CreditAmt;

                        }
                        else
                        {
                            OpeningBal = OpeningBal + decimal.Parse(DebitAmt) + CreditAmt;
                        }

                        //  OpeningBal = OpeningBal + decimal.Parse(DebitAmt) + CreditAmt;
                        if (OpeningBal >= 0)
                        {
                            GridView4.Rows[i].Cells[3].Text = OpeningBal.ToString() + " Cr";
                            //  GridView4.Rows[i].BackColor = System.Drawing.Color.Bisque;
                        }
                        else
                        {
                            GridView4.Rows[i].Cells[3].Text = Math.Abs(OpeningBal).ToString() + " Dr";
                        }
                    }
                    GridView4.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView4.UseAccessibleHeader = true;
                }
            }
            else
            {
                DivTable.Visible = true;
                GridView4.Visible = false;
                ViewState["DayBookVisible"] = "true";
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
}
