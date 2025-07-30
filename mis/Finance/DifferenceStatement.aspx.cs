using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

public partial class mis_Finance_DifferenceStatement : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    APIProcedure api = new APIProcedure();
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
                    ViewState["OfficeType_Title"] = Session["OfficeType_Title"].ToString();
                    ViewState["Division_ID"] = Session["Division_ID"].ToString();
                   // ddlOffice.Enabled = false;

                    txtFromDate.Attributes.Add("readonly", "readonly");
                    string FY = GetCurrentFinancialYear();
                    string[] YEAR = FY.Split('-');
                    DateTime FromDate = new DateTime(int.Parse(YEAR[0]), 4, 1);
                    DateTime ToDate = new DateTime(int.Parse(YEAR[1]), 3, 31);
                    txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                    txtToDate.Text = ToDate.ToString("dd/MM/yyyy");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    FillVoucherDate();
                    FillOffice();
                    FillBank();


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
    protected void AddDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn RowNo = dt.Columns.Add("RowNo", typeof(int));
        dt.Columns.Add("Bank_id", typeof(string));
        dt.Columns.Add("BankName", typeof(string));
        dt.Columns.Add("BankDoc", typeof(string));
        RowNo.AutoIncrement = true;
        RowNo.AutoIncrementSeed = 1;
        RowNo.AutoIncrementStep = 1;
        ViewState["dtBank"] = dt;
        gvBankDocs.DataSource = dt;
        gvBankDocs.DataBind();
    }
  
    protected void FillOffice()
    {
        try
        {

            //ddlOffice.Enabled = false;
            //ddlRegionalOffice.Enabled = false;

            ddlOffice.Items.Clear();


            ds = objdb.ByProcedure("SpAdminOffice",
                    new string[] { "flag" },
                    new string[] { "22" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
            if (ViewState["Office_ID"].ToString() == "1")
            {

            }
            else
            {
                ddlOffice.Enabled = false;
            }

            ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
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
                txtFromDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {

                FillGrid();
                
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
            lblMsg.Text = "";
            ViewState["Id"] = "0";
            AddDataTable();
            txtDiifReason.Text = "";
            btnSubmit.Text = "Submit";
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

            //  GridView1.DataSource = new string[] { };

            //if (ddlOffice.SelectedIndex > 0)
            //{
            string Office = "";

            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            divreport.Visible = false;
            divhideshow.Visible = false;
            ds = objdb.ByProcedure("SpFinDifferenceReport", new string[] { "Office_ID_Mlt", "FromDate", "ToDate", "FinancialYear" }, new string[] { Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), FinancialYear }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables.Count != 0)
            {
                divreport.Visible = true;
                if (ViewState["Office_ID"].ToString() == ddlOffice.SelectedValue.ToString())
                {
                    divhideshow.Visible = true;
                }
                else
                {
                    divhideshow.Visible = false;

                }
                lblOpening.Text = ds.Tables[0].Rows[0]["BankOpening"].ToString();
                lblClosing.Text = ds.Tables[0].Rows[0]["BankClosing"].ToString();
                lblReceipts.Text = ds.Tables[0].Rows[0]["IncomeTran"].ToString();
                lblPayment.Text = ds.Tables[0].Rows[0]["ExpenseTran"].ToString();
                decimal OpeningPlusInc = decimal.Parse(ds.Tables[0].Rows[0]["BankOpening"].ToString()) + decimal.Parse(ds.Tables[0].Rows[0]["IncomeTran"].ToString());
                decimal ClosingPlusExp = decimal.Parse(ds.Tables[0].Rows[0]["BankClosing"].ToString()) + decimal.Parse(ds.Tables[0].Rows[0]["ExpenseTran"].ToString());
                lblTotalCredits.Text = OpeningPlusInc.ToString();
                lblTotalDebits.Text = ClosingPlusExp.ToString();
                lblDifference.Text = Math.Abs((OpeningPlusInc - ClosingPlusExp)).ToString();
                lblPaymentCr.Text = ds.Tables[0].Rows[0]["PaymentCreditAmount"].ToString();


                if (ds.Tables[1].Rows.Count > 0)
                {
                    txtDiifReason.Text = ds.Tables[1].Rows[0]["Difference_Reason"].ToString();
                    ViewState["Id"] = ds.Tables[1].Rows[0]["Id"].ToString();
                    btnSubmit.Text = "Update";
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    DataTable dt = (DataTable)ViewState["dtBank"];
                    int TCount = ds.Tables[2].Rows.Count;
                    for (int i = 0; i < TCount; i++)
                    {
                        dt.Rows.Add(null, ds.Tables[2].Rows[i]["Bank_id"].ToString(), ds.Tables[2].Rows[i]["BankName"].ToString(), ds.Tables[2].Rows[i]["UploadDocs"].ToString());
                    }
                    ViewState["dtBank"] = dt;
                    gvBankDocs.DataSource = dt;
                    gvBankDocs.DataBind();
                }
                lblLTTotal.Text = (OpeningPlusInc + decimal.Parse(ds.Tables[0].Rows[0]["PaymentCreditAmount"].ToString())).ToString();
                lblRTTotal.Text = ClosingPlusExp.ToString();
            }
            else
            {


            }
            //}


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillBank()
    {
        try
        {
            ddlBank_Name.Items.Clear();
            ds = objdb.ByProcedure("SpHRBankDetail", new string[] { "flag" }, new string[] { "4" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlBank_Name.DataSource = ds;
                ddlBank_Name.DataTextField = "BankName";
                ddlBank_Name.DataValueField = "Bank_id";
                ddlBank_Name.DataBind();


            }
            ddlBank_Name.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
   
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["dtBank"];
        string Msg = "";
        string Docs = "";
        if (ddlBank_Name.SelectedIndex == 0)
        {
            Msg += "Select Bank.\\n";

        }
        if (fuDocs.HasFile)
        {
            Docs = "DifferenceRptDocs/" + Guid.NewGuid() + "-" + fuDocs.FileName;
            fuDocs.PostedFile.SaveAs(Server.MapPath(Docs));
        }
        else
        {
            Msg += "Select File.";
        }
        if (Msg == "")
        {
            dt.Rows.Add(null, ddlBank_Name.SelectedValue, ddlBank_Name.SelectedItem.Text, Docs);
            ViewState["dtBank"] = dt;
            gvBankDocs.DataSource = dt;
            gvBankDocs.DataBind();
            ddlBank_Name.ClearSelection();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + Msg + "')", true);
        }

    }
    protected void gvBankDocs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string RowNo = e.CommandArgument.ToString();
        if (e.CommandName == "DeleteRecord")
        {
            DataTable dt = (DataTable)ViewState["dtBank"];
            int Count = dt.Rows.Count;

            for (int i = 0; i < Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (dr["RowNo"].ToString() == RowNo.ToString())
                {
                    dt.Rows.Remove(dr);
                    dt.AcceptChanges();
                }
            }
            ViewState["dtBank"] = dt;
            gvBankDocs.DataSource = dt;
            gvBankDocs.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string Msg = "";
            DataTable dt = (DataTable)ViewState["dtBank"];
            if (ddlOffice.SelectedIndex == 0)
            {
                Msg += "Select Office.\\n";
            }
            if (txtFromDate.Text == "")
            {
                Msg += "Select FromDate.\\n";
            }
            if (txtToDate.Text == "")
            {
                Msg += "Select ToDate.\\n";
            }
            if (txtDiifReason.Text == "")
            {
                Msg += "Enter Reason.\\n";
            }
            if (dt.Rows.Count == 0)
            {
                Msg += "Enter Bank Details";
            }
            if (Msg == "")
            {
                if (btnSubmit.Text == "Submit")
                {
                    ds = objdb.ByProcedure("Usp_FinDifferenceReport"
                                , new string[] {"flag",
                                                "Office_ID", 
			                                    "TransactionFromDt", 
			                                    "TransactionToDt", 
			                                    "Opening", 
			                                    "Receipts", 
			                                    "TotalCredits", 
			                                    "Closing", 
			                                    "Payment", 
			                                    "TotalDebits", 
			                                    "Difference", 
			                                    "PaymentCR", 
			                                    "Difference_Reason", 
			                                    "CreatedBy", 
			                                    "CreatedByIP"
                                                 }
                                , new string[] {"0"
                                               ,ddlOffice.SelectedValue
                                                ,Convert.ToDateTime(txtFromDate.Text,cult).ToString("yyyy/MM/dd")
                                                ,Convert.ToDateTime(txtToDate.Text,cult).ToString("yyyy/MM/dd")
                                               ,lblOpening.Text
                                               ,lblReceipts.Text
                                               ,lblTotalCredits.Text
                                               ,lblClosing.Text
                                               ,lblPayment.Text
                                               ,lblTotalDebits.Text
                                               ,lblDifference.Text
                                               ,lblPaymentCr.Text
                                               ,txtDiifReason.Text
                                               ,ViewState["Emp_ID"].ToString()
                                               ,api.GetLocalIPAddress()
                                
                                 },
                                new string[] { "Type_tblFinDiffRptBankDetails" },
                                new DataTable[] {dt,
                                                }, "TableSave");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                        }
                    }
                }
                else if (btnSubmit.Text == "Update")
                {
                    ds = objdb.ByProcedure("Usp_FinDifferenceReport"
                                   , new string[] {"flag",
                                                "Id", 
			                                    "TransactionFromDt", 
			                                    "TransactionToDt", 
			                                    "Opening", 
			                                    "Receipts", 
			                                    "TotalCredits", 
			                                    "Closing", 
			                                    "Payment", 
			                                    "TotalDebits", 
			                                    "Difference", 
			                                    "PaymentCR", 
			                                    "Difference_Reason", 
			                                    "CreatedBy", 
			                                    "CreatedByIP"
                                                 }
                                   , new string[] {"2"
                                                ,ViewState["Id"].ToString()
                                                ,Convert.ToDateTime(txtFromDate.Text,cult).ToString("yyyy/MM/dd")
                                                ,Convert.ToDateTime(txtToDate.Text,cult).ToString("yyyy/MM/dd")
                                               ,lblOpening.Text
                                               ,lblReceipts.Text
                                               ,lblTotalCredits.Text
                                               ,lblClosing.Text
                                               ,lblPayment.Text
                                               ,lblTotalDebits.Text
                                               ,lblDifference.Text
                                               ,lblPaymentCr.Text
                                               ,txtDiifReason.Text
                                               ,ViewState["Emp_ID"].ToString()
                                               ,api.GetLocalIPAddress()
                                
                                 },
                                   new string[] { "Type_tblFinDiffRptBankDetails" },
                                   new DataTable[] {dt,
                                                }, "TableSave");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                        }
                    }

                }
                txtDiifReason.Text = "";
                AddDataTable();
                divreport.Visible = false;
                btnSubmit.Text = "Submit";
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + Msg + "')", true);
            }

        }
        catch (Exception ex)
        {

        }
    }
}