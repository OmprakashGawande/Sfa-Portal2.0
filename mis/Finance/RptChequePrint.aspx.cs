using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_Finance_RptChequePrint : System.Web.UI.Page
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
    protected void FillDropdown()
    {
        try
        {

            //DataSet ds1 = objdb.ByProcedure("spFinBankChequePrint", new string[] { "flag", "Office_ID" }, new string[] { "0", ViewState["Office_ID"].ToString() }, "dataset");
            //if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            //{
            //    ddlLedger.DataSource = ds1;
            //    ddlLedger.DataTextField = "Ledger_Name";
            //    ddlLedger.DataValueField = "Ledger_ID";
            //    ddlLedger.DataBind();
            //    ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

            //}
            DataSet ds1 = objdb.ByProcedure("spFinBankChequePrint", new string[] { "flag", "Office_ID" }, new string[] { "3", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                ddlBeneficiaryName.DataSource = ds1;
                ddlBeneficiaryName.DataTextField = "FavouringName";
                ddlBeneficiaryName.DataValueField = "FavouringName";
                ddlBeneficiaryName.DataBind();
                ddlBeneficiaryName.Items.Insert(0, new ListItem("Select", "0"));

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
 
    protected void FillGrid()
    {
        try
        {
            
           
            GridView1.DataSource = null;
            GridView1.DataBind();
            //ds = objdb.ByProcedure("spFinBankChequePrint", new string[] { "flag", "Office_ID", "Ledger_ID", "FromDate", "ToDate" }, new string[] { "1", ViewState["Office_ID"].ToString(), ddlLedger.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            //if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            //{
            //    GridView1.DataSource = ds.Tables[0];
            //    GridView1.DataBind();
            //}
            ds = objdb.ByProcedure("spFinBankChequePrint", new string[] { "flag", "Office_ID", "FavouringName", "FromDate", "ToDate" }, new string[] { "4", ViewState["Office_ID"].ToString(), ddlBeneficiaryName.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
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
            string msg = "";            
            //if (ddlLedger.SelectedIndex == 0)
            //{
            //    msg += "Select List Of Ledger.\\n";
            //}
            if (ddlBeneficiaryName.SelectedIndex== 0)
            {
                msg += "Select List Of Beneficiary.\\n";
            }
            if (msg == "")
            {
                if (txtFromDate.Text != "" && txtToDate.Text != "")
                {
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
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        //    objdb.ByProcedure("spFinBankRecDetail", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "LedgerTxChequeTx_ID", "LedgerCheque_Type", "ChequeDD_Type", "BankTxn_Date", "BankRec_Reason", "Office_ID", "BankRec_IsActive", "BankRec_UpdatedBy", "BankTxn_Amount" }, new string[] { "0", ViewState["VoucherTx_ID"].ToString(), ViewState["Ledger_ID"].ToString(), ViewState["LedgerTxChequeTx_ID"].ToString(), ViewState["LedgerCheque_Type"].ToString(), ddlChequeDD_Type.Text, Convert.ToDateTime(txtBankTxn_Date.Text, cult).ToString("yyyy/MM/dd"), txtBankRec_Reason.Text, ViewState["Office_ID"].ToString(), BankRec_IsActive, ViewState["Emp_ID"].ToString(), ViewState["Amount"].ToString() }, "dataset");

    //        foreach (GridViewRow row in GridView1.Rows)
    //        {
    //            Label lblVoucherTx_ID = (Label)row.FindControl("lblVoucherTx_ID");
    //            Label lblLedger_ID = (Label)row.FindControl("lblLedger_ID");
    //            Label lblLedgerTxChequeTx_ID = (Label)row.FindControl("lblLedgerTxChequeTx_ID");
    //            Label lblLedgerCheque_Type = (Label)row.FindControl("lblLedgerCheque_Type");
    //            TextBox txtBankTxn_Date = (TextBox)row.FindControl("txtBankTxn_Date");
    //            Label lblDebitAmt = (Label)row.FindControl("lblDebitAmt");
    //            Label lblCreditAmt = (Label)row.FindControl("lblCreditAmt");

    //            // Label lblChequeDD_Type = (Label)row.FindControl("lblChequeDD_Type");                
    //            DropDownList ddlChequeDD = (DropDownList)row.FindControl("ddlChequeDD");

    //            string bankdate = "";
    //            if (txtBankTxn_Date.Text != "")
    //            {
    //                bankdate = Convert.ToDateTime(txtBankTxn_Date.Text, cult).ToString("yyyy/MM/dd");
    //            }

    //            string BankTxn_Amount = "0.00";
    //            string BankRec_Reason = "";
    //            // string ChequeDD_Type = lblChequeDD_Type.Text; 
    //            string ChequeDD_Type = ddlChequeDD.SelectedValue.ToString();
    //            string BankRec_IsActive = "1";

    //            if (lblDebitAmt.Text != "")
    //            {
    //                BankTxn_Amount = "-" + lblDebitAmt.Text;
    //            }
    //            else if (lblCreditAmt.Text != "")
    //            {
    //                BankTxn_Amount = lblCreditAmt.Text;
    //            }

    //            objdb.ByProcedure("spFinBankRecDetail", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "LedgerTxChequeTx_ID", "LedgerCheque_Type", "ChequeDD_Type", "BankTxn_DateString", "BankRec_Reason", "Office_ID", "BankRec_IsActive", "BankRec_UpdatedBy", "BankTxn_Amount" }
    //                , new string[] { "5", lblVoucherTx_ID.Text, lblLedger_ID.Text, lblLedgerTxChequeTx_ID.Text, lblLedgerCheque_Type.Text, ChequeDD_Type, bankdate, BankRec_Reason, ViewState["Office_ID"].ToString(), BankRec_IsActive, ViewState["Emp_ID"].ToString(), BankTxn_Amount }, "dataset");

    //            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
    //        }

    //        FillGrid();

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            
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
            else if (e.CommandName == "Print")
            {
                ViewState["ChequeTx_ID"] = e.CommandArgument.ToString();

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBankTypeModalModal();", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnPnb_Click(object sender, ImageClickEventArgs e)
    {

        string Url = "ChequePrint.aspx?ChequeTx_ID=" + objdb.Encrypt(ViewState["ChequeTx_ID"].ToString());
        Url = Url + "&Type=" + objdb.Encrypt("PNB");
        Response.Redirect(Url);
    }
    protected void btnSbi_Click(object sender, ImageClickEventArgs e)
    {
        string Url = "ChequePrint.aspx?ChequeTx_ID=" + objdb.Encrypt(ViewState["ChequeTx_ID"].ToString());
        Url = Url + "&Type=" + objdb.Encrypt("SBI");
        Response.Redirect(Url);

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
