using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_Finance_bankreconciliationreport : System.Web.UI.Page
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
                    pnlclosing.Visible = false;
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
                    GetCommonSearch();
                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        ddlOffice.Enabled = false;
                    }

                    //chkDebitAmt.Checked = false;
                    //chkCreditAmt.Checked = false;

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

    protected void FillDropdown()
    {
        try
        {

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

            DataSet ds1 = objdb.ByProcedure("spFinBankRecDetail", new string[] { "flag", "Office_ID" }, new string[] { "2", ddlOffice.SelectedValue.ToString() }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                ddlLedger.DataSource = ds1;
                ddlLedger.DataTextField = "Ledger_Name";
                ddlLedger.DataValueField = "Ledger_ID";
                ddlLedger.DataBind();
                ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

            }

            ddlOffice.Enabled = false;

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

            lblDebitAmtT.Text = "";
            lblCreditAmtT.Text = "";
            lblClosingBalanceDr.Text = "";
            lblClosingBalanceCr.Text = "";
            lblNotRefBankDr.Text = "";
            lblNotRefBankCr.Text = "";
            lblBankBalanceDr.Text = "";
            lblBankBalanceCr.Text = "";


            ddlLedger.DataSource = null;
            ddlLedger.DataBind();

            DataSet ds1 = objdb.ByProcedure("spFinBankRecDetail", new string[] { "flag", "Office_ID" }, new string[] { "2", ddlOffice.SelectedValue.ToString() }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                ddlLedger.DataSource = ds1;
                ddlLedger.DataTextField = "Ledger_Name";
                ddlLedger.DataValueField = "Ledger_ID";
                ddlLedger.DataBind();
                ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

            }

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
            pnlclosing.Visible = false;
            string AmtType = "All";
            string Reconciled = ddlBankStatus.SelectedValue.ToString();

            if (chkCreditAmt.Checked == true && chkDebitAmt.Checked == false)
            {
                AmtType = "Cr";
            }
            else if (chkCreditAmt.Checked == false && chkDebitAmt.Checked == true)
            {
                AmtType = "Dr";
            }
            //lblOpeningBalance.Text = "";
            //lblLedgerTxnAmt.Text = "";
            //lblBankTxnAmt.Text = "";

            lblDebitAmtT.Text = "";
            lblCreditAmtT.Text = "";
            lblClosingBalanceDr.Text = "";
            lblClosingBalanceCr.Text = "";
            lblNotRefBankDr.Text = "";
            lblNotRefBankCr.Text = "";
            lblBankBalanceDr.Text = "";
            lblBankBalanceCr.Text = "";

            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("spFinBankRecDetail", new string[] { "flag", "Office_ID", "Ledger_ID", "FromDate", "ToDate", "AmtType", "Reconciled" }, new string[] { "4", ddlOffice.SelectedValue.ToString(), ddlLedger.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), AmtType, Reconciled }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                pnlclosing.Visible = true;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();

                //decimal OpeningBalance = 0;
                //decimal LedgerTxnAmt = 0;
                //decimal BankTxnAmt = 0;

                //if (ds.Tables[1].Rows[0]["Opening"].ToString() != "")
                //{
                //    OpeningBalance = decimal.Parse(ds.Tables[1].Rows[0]["Opening"].ToString());
                //}
                //if (ds.Tables[1].Rows[0]["LedgerTxn"].ToString() != "")
                //{
                //    LedgerTxnAmt = decimal.Parse(ds.Tables[1].Rows[0]["LedgerTxn"].ToString());
                //}
                //if (ds.Tables[1].Rows[0]["BankTxn"].ToString() != "")
                //{
                //    BankTxnAmt = decimal.Parse(ds.Tables[1].Rows[0]["BankTxn"].ToString());
                //}
                
                
                //if (OpeningBalance < 0)
                //    lblOpeningBalance.Text = Math.Abs(OpeningBalance).ToString() + " Dr";
                //else
                //    lblOpeningBalance.Text = Math.Abs(OpeningBalance).ToString() + " Cr";

                //if (LedgerTxnAmt < 0)
                //    lblLedgerTxnAmt.Text = Math.Abs(LedgerTxnAmt).ToString() + " Dr";
                //else
                //    lblLedgerTxnAmt.Text = Math.Abs(LedgerTxnAmt).ToString() + " Cr";

                //if (BankTxnAmt < 0)
                //    lblBankTxnAmt.Text = Math.Abs(BankTxnAmt).ToString() + " Dr";
                //else
                //    lblBankTxnAmt.Text = Math.Abs(BankTxnAmt).ToString() + " Cr";


                lblDebitAmtT.Text = ds.Tables[2].Rows[0]["DebitAmtT"].ToString();
                lblCreditAmtT.Text = ds.Tables[2].Rows[0]["CreditAmtT"].ToString();

                decimal OpeningBalance = 0;
                decimal LedgerTxnAmt = 0;
                decimal BankTxnAmt = 0;
                decimal L_DebitTotal = 0;
                decimal L_CreditTotal = 0;

                decimal BankTxnDr = 0;
                decimal BankTxnCr = 0;
                decimal DiffTxn = 0;


                L_DebitTotal = decimal.Parse(ds.Tables[2].Rows[0]["DebitAmtT"].ToString());
                L_CreditTotal = decimal.Parse(ds.Tables[2].Rows[0]["CreditAmtT"].ToString());

                BankTxnDr = decimal.Parse(ds.Tables[2].Rows[0]["BankTxnDr"].ToString());
                BankTxnCr = decimal.Parse(ds.Tables[2].Rows[0]["BankTxnCr"].ToString());



                if (ds.Tables[1].Rows[0]["Opening"].ToString() != "")
                {
                    OpeningBalance = decimal.Parse(ds.Tables[1].Rows[0]["Opening"].ToString());
                }
                if (ds.Tables[1].Rows[0]["LedgerTxn"].ToString() != "")
                {
                    LedgerTxnAmt = decimal.Parse(ds.Tables[1].Rows[0]["LedgerTxn"].ToString());
                }
                if (ds.Tables[1].Rows[0]["BankTxn"].ToString() != "")
                {
                    BankTxnAmt = decimal.Parse(ds.Tables[1].Rows[0]["BankTxn"].ToString());
                }

                /***Closing Balance****/
               // decimal LedgerClosingBalance = (OpeningBalance + (-Math.Abs(L_DebitTotal)) + (Math.Abs(L_CreditTotal)));
                decimal LedgerClosingBalance = decimal.Parse(ds.Tables[3].Rows[0]["LedgerClosing"].ToString());
                if (LedgerClosingBalance < 0)
                {
                    lblClosingBalanceDr.Text = Math.Abs(LedgerClosingBalance).ToString();
                }
                else
                {
                    lblClosingBalanceCr.Text = Math.Abs(LedgerClosingBalance).ToString();
                }


                L_DebitTotal = L_DebitTotal - BankTxnDr;

                L_CreditTotal = L_CreditTotal - BankTxnCr;

                lblNotRefBankDr.Text = Math.Abs(L_DebitTotal).ToString();
                lblNotRefBankCr.Text = Math.Abs(L_CreditTotal).ToString();

                if (L_DebitTotal > L_CreditTotal)
                {
                    DiffTxn = -(L_DebitTotal - L_CreditTotal);
                }
                else if (L_CreditTotal > L_DebitTotal)
                {
                    DiffTxn = (L_CreditTotal - L_DebitTotal);
                }

                decimal BnkClosing = 0;
                if (LedgerClosingBalance < 0 && DiffTxn < 0)
                {
                    BnkClosing = Math.Abs(LedgerClosingBalance) - Math.Abs(DiffTxn);


                    if (Math.Abs(LedgerClosingBalance) > Math.Abs(DiffTxn))
                    {
                        lblBankBalanceDr.Text = Math.Abs(BnkClosing).ToString();
                    }
                    else
                    {
                        lblBankBalanceCr.Text = Math.Abs(BnkClosing).ToString();
                    }
                }
                else if (LedgerClosingBalance >= 0 && DiffTxn >= 0)
                {
                    BnkClosing = LedgerClosingBalance - DiffTxn;
                    if (LedgerClosingBalance < DiffTxn)
                    {
                        lblBankBalanceDr.Text = Math.Abs(BnkClosing).ToString();
                    }
                    else
                    {
                        lblBankBalanceCr.Text = Math.Abs(BnkClosing).ToString();
                    }
                }
                else if (LedgerClosingBalance >= 0 && DiffTxn < 0)
                {
                    BnkClosing = LedgerClosingBalance + Math.Abs(DiffTxn);

                    lblBankBalanceCr.Text = Math.Abs(BnkClosing).ToString();
                }
                else if (LedgerClosingBalance < 0 && DiffTxn >= 0)
                {
                    BnkClosing = Math.Abs(LedgerClosingBalance) + Math.Abs(DiffTxn);

                    lblBankBalanceDr.Text = Math.Abs(BnkClosing).ToString();
                }

            }

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
            //   DivTable.InnerHtml = "";
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
                    string headingFirst = "<p class='text-center' style='font-weight:600; font-size:16px;'>Bank Reconciliation<br /> M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd,<br/> [ " + ddlOffice.SelectedItem.Text + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "<br/>Ledger Name : " + ddlLedger.SelectedItem.ToString() +"</p>";
                    lblheadingFirst.Text = headingFirst;
                    /*****************SET Search Option Start********************/
                    #region SetSearchOption
                    Session["CommonFromDate"] = txtFromDate.Text;
                    Session["CommonToDate"] = txtToDate.Text;
                    #endregion
                    /*****************SET Search Option End********************/
                    FillGrid();
                }

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
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
    protected void GetCommonSearch()
    {
        try
        {
           
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
