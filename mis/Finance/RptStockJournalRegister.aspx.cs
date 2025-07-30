using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Finance_RptStockJournalRegister : System.Web.UI.Page
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
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            ds = objdb.ByProcedure("SpFinVoucherStockJournal", new string[] { "flag", "Office_ID_Mlt", "TransactionFrom", "FromDate", "ToDate" }, new string[] { "9", Office, "Stock Journal", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                GridView3.DataSource = ds;
                GridView3.DataBind();
                GridView4.DataSource = null;
                GridView4.DataBind();

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
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            ViewState["SelectedMonth"] = MonthID.ToString();
            ds = objdb.ByProcedure("SpFinVoucherStockJournal", new string[] { "flag", "Office_ID_Mlt", "TransactionFrom", "LedgerTx_Month", "FromDate", "ToDate" }, new string[] { "3", Office, "Stock Journal", MonthID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                GridView4.DataSource = ds;
            }
            GridView4.DataBind();
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
            lblMsg.Text = "";
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView3.Rows[index];

                Label lblMonthID = (Label)row.Cells[0].FindControl("lblMonthID");


                string MonthID = lblMonthID.Text;

                FillGridNextLedgerMonth(MonthID);
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
            string ItmStock_id = GridView4.DataKeys[e.RowIndex].Value.ToString();

            objdb.ByProcedure("SpFinVoucherStockJournal",
                   new string[] { "flag", "ItmStock_id", "CreatedBy", "TransactionFrom" },
                   new string[] { "10", ItmStock_id, ViewState["Emp_ID"].ToString(), "Stock Journal" }, "dataset");
            /**************/
            FillGridNextLedgerMonth(ViewState["SelectedMonth"].ToString());
            /**************/
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
            lblMsg.Text = "";
            FillGridNextLedger();
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
            lblMsg.Text = "";
            string Office = "";
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
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
