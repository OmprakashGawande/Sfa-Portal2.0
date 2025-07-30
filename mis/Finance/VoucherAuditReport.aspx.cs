using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;


public partial class mis_Finance_VoucherAuditReport : System.Web.UI.Page
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
                    DateTime ToDate = new DateTime(int.Parse(YEAR[1]), 3, 31);
                    txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                    txtToDate.Text = ToDate.ToString("dd/MM/yyyy");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    //FillVoucherDate();
                    FillDropdown();
                    GetCommonSearch();
                    //FillGrid();

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
            ds = objdb.ByProcedure("SpFinVoucherTx",
                   new string[] { "flag" },
                   new string[] { "26" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("All", "0"));
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
                txtFromDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
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

            //  GridView1.DataSource = new string[] { };

            //if (ddlOffice.SelectedIndex > 0)
            //{
            ds = objdb.ByProcedure("SpFinRptDeletedVoucher", new string[] { "flag", "Office_ID", "FromDate", "ToDate" }, new string[] { "2", ddlOffice.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                lblHeading.Text = "Office Wise Audit Voucher Report";
                divExcel.Visible = true;
                // GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                // GridView1.UseAccessibleHeader = true;

                //DataView dv = ds.Tables[0].DefaultView;

                //int i = 0;
                //foreach (GridViewRow rows in GridView1.Rows)
                //{

                //    HyperLink lnkEdit = (HyperLink)rows.FindControl("hpEdit");
                //    HyperLink hpView = (HyperLink)rows.FindControl("hpView");
                //    HyperLink hpprint1 = (HyperLink)rows.FindControl("hpprint1");
                //    HyperLink hpprint2 = (HyperLink)rows.FindControl("hpprint2");
                //    HiddenField HF_VoucherTx_ID = (HiddenField)rows.FindControl("HF_VoucherTx_ID");


                //    dv.RowFilter = " VoucherTx_ID = " + HF_VoucherTx_ID.Value.ToString();

                //    DataTable dt = dv.ToTable();

                //    string VoucherTx_Type = dt.Rows[0]["VoucherTx_Type"].ToString();
                //    string Url = dt.Rows[0]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(dt.Rows[0]["VoucherTx_ID"].ToString());

                //    hpView.NavigateUrl = Url + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(dt.Rows[0]["Office_ID"].ToString());
                //    if (VoucherTx_Type == "CreditNote Voucher" || VoucherTx_Type == "DebitNote Voucher")
                //    {
                //        lnkEdit.Visible = false;
                //    }
                //    else
                //    {
                //        lnkEdit.Visible = true;
                //        lnkEdit.NavigateUrl = Url + "&Action=" + objdb.Encrypt("2");

                //    }
                //    if (VoucherTx_Type == "Payment" || VoucherTx_Type == "Contra" || VoucherTx_Type == "GSTService Purchase")
                //    {
                //        if (ddlOffice.SelectedValue == ViewState["Office_ID"].ToString())
                //        {
                //            hpprint1.Visible = true;
                //            hpprint2.Visible = false;
                //        }
                //        else
                //        {
                //            hpprint1.Visible = false;
                //            hpprint2.Visible = false;
                //        }

                //    }
                //    else if (VoucherTx_Type == "Receipt" || VoucherTx_Type == "Journal")
                //    {
                //        if (ddlOffice.SelectedValue == ViewState["Office_ID"].ToString())
                //        {
                //            hpprint1.Visible = false;
                //            hpprint2.Visible = true;
                //        }
                //        else
                //        {
                //            hpprint1.Visible = false;
                //            hpprint2.Visible = false;
                //        }


                //    }
                //    else
                //    {
                //        hpprint1.Visible = false;
                //        hpprint2.Visible = false;
                //    }
                //    i++;

                //}

            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
            }
            //}


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {

        try
        {
            lblMsg.Text = "";
            string VoucherTx_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();

            objdb.ByProcedure("SpFinVoucherTx",
                   new string[] { "flag", "VoucherTx_ID", "Emp_ID" },
                   new string[] { "12", VoucherTx_ID, ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Deleted.");
            FillGrid();

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
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                lblMsg.Text = "";
                /*****************SET Search Option Start********************/
                #region SetSearchOption
                Session["CommonFromDate"] = txtFromDate.Text;
                Session["CommonToDate"] = txtToDate.Text;
                #endregion
                /*****************SET Search Option End********************/
                FillGrid();
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;

                lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
                //foreach (GridViewRow rows in GridView1.Rows)
                //{
                //    HyperLink lnkEdit = (HyperLink)rows.FindControl("hpEdit");
                //    LinkButton lnkDelete = (LinkButton)rows.FindControl("Delete");
                //    Label lblOfficeID = (Label)rows.FindControl("lblOfficeID");
                //    if (lblOfficeID.Text == ViewState["Office_ID"].ToString())
                //    {
                //        lnkEdit.Visible = true;
                //        lnkDelete.Visible = true;
                //    }
                //    else
                //    {

                //        lnkEdit.Visible = false;
                //        lnkDelete.Visible = false;
                //    }

                //}
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
            lblMsg.Text = "";
            FillGrid();
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