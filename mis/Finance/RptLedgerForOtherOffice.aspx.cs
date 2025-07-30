using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_Finance_Rpt : System.Web.UI.Page
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
                    DateTime ToDate = new DateTime(int.Parse(YEAR[1]), 3, 31);
                    txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                    txtToDate.Text = ToDate.ToString("dd/MM/yyyy");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    FillVoucherDate();
                    FillDropdown();
                    btnPrint.Visible = false;
                    //FillGrid();

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

    protected void FillDropdown()
    {
        try
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


    protected void FillGrid()
    {
        try
        {
            btnPrint.Visible = false;
            //  GridView1.DataSource = new string[] { };

            //if (ddlOffice.SelectedIndex > 0)
            //{

            ds = objdb.ByProcedure("SpFinRptLedgerSummary", new string[] { "flag", "Office_ID", "Ledger_ID", "FromDate", "ToDate" }, new string[] { "5", "1", ddlLedger.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                btnPrint.Visible = true;
                GridView1.AllowPaging = true;
                GridView1.Columns[6].Visible = true;

                ViewState["Table"] = ds.Tables[0];



                GridView1.DataSource = ds;
                GridView1.DataBind();

                //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                //GridView1.UseAccessibleHeader = true;
                foreach (GridViewRow rows in GridView1.Rows)
                {

                    LinkButton lnkPrint = (LinkButton)rows.FindControl("hpprint");
                    Label lblVoucherTx_Type = (Label)rows.FindControl("lblVoucherTx_Type");
                    Label lblOfficeID = (Label)rows.FindControl("lblOfficeID");
                    if (ViewState["Office_ID"].ToString() != lblOfficeID.Text)
                    {
                        lnkPrint.Visible = false;
                    }
                    else
                    {
                        if (lblVoucherTx_Type.Text == "Payment" || lblVoucherTx_Type.Text == "Contra" || lblVoucherTx_Type.Text == "Receipt" || lblVoucherTx_Type.Text == "Journal" || lblVoucherTx_Type.Text == "Cash Payment" || lblVoucherTx_Type.Text == "Bank Receipt" || lblVoucherTx_Type.Text == "Journal HO" || lblVoucherTx_Type.Text == "GSTService Purchase")
                        {
                            lnkPrint.Visible = true;
                        }
                        else
                        {
                            lnkPrint.Visible = false;
                        }
                    }
                }




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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            lblMsg.Text = "";
            FillGrid();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

        GridView1.AllowPaging = false;

        // ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "FromDate", "ToDate" }, new string[] { "21", "1", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        //ds = objdb.ByProcedure("SpFinRptLedgerSummary", new string[] { "flag", "Office_ID", "Ledger_ID", "FromDate", "ToDate" }, new string[] { "5", "1", ddlLedger.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        DataTable dt = (DataTable)ViewState["Table"];

        if (dt.Rows.Count != 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.Columns[6].Visible = false;
        }
        StringBuilder sb1 = new StringBuilder();
        sb1.Append("<p class='text-center' style='font-weight:600; font-size:16px; text-align:center'> Branch Ledger Copy<br /> MP State Agro Industries Development Corporation, <br/> [ Head Office ] <br />  [Ledger :  " + ddlLedger.SelectedItem.ToString() + " ] <br /> " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>");
        //sb1.Append("<div style='padding-bottom:20px; font-size:20px;'>Custom DayBook:  "+ddlOffice.SelectedItem.Text+"  (Date: " + txtFromDate.Text + " - " + txtToDate.Text + ")</div>");
        string gridHTML = sb1.ToString();
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        GridView1.RenderControl(hw);


        gridHTML += sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");

        StringBuilder sb = new StringBuilder();
        GridView1.AllowPaging = true;
        GridView1.Columns[6].Visible = true;

        sb.Append("<script type = 'text/javascript'>");

        sb.Append("window.onload = new function(){");

        sb.Append("var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');");

        sb.Append("WinPrint.document.write(\"");


        sb.Append(gridHTML);

        sb.Append("\");");

        sb.Append("WinPrint.document.close();");

        sb.Append("WinPrint.focus();");

        sb.Append("WinPrint.print();");

        sb.Append("WinPrint.close();};");

        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());

        //ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "FromDate", "ToDate" }, new string[] { "21", "1", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        //if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
        //{
        GridView1.AllowPaging = true;
        GridView1.DataSource = dt;
        GridView1.DataBind();
        GridView1.Columns[6].Visible = true;

        //}

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            //if (e.CommandName == "Editing")
            //{
            //    string VoucherTx_ID = e.CommandArgument.ToString();
            //    DataSet dsPageURL = objdb.ByProcedure("SpFinVoucherTx",
            //        new string[] { "flag", "VoucherTx_ID" },
            //        new string[] { "30", VoucherTx_ID },
            //        "dataset");

            //    if (dsPageURL != null)
            //    {

            //        string Url = dsPageURL.Tables[0].Rows[0]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
            //        Url = Url + "&Action=" + objdb.Encrypt("2");
            //        Response.Redirect(Url);

            //    }
            //}
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblVID = (Label)e.Row.FindControl("lblVID");
            Label lblLID = (Label)e.Row.FindControl("lblLID");
            Label lblLedger_Name = (Label)e.Row.FindControl("lblLedger_Name");
            Label lblNarration = (Label)e.Row.FindControl("lblNarration");
            GridView gvLedger = (GridView)e.Row.FindControl("gvLedger");
            GridView GvItem = (GridView)e.Row.FindControl("GvItem");
            GridView gvSubLedger = (GridView)e.Row.FindControl("gvSubLedger");
            GridView GVLBillbyBill = (GridView)e.Row.FindControl("GVLBillbyBill");
            DataSet ds1 = null;

            ds1 = objdb.ByProcedure("SpFinRptCashBankBooksNew", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", }, new string[] { "13", lblVID.Text, lblLID.Text }, "dataset");
            if (ds1 != null)
            {
                gvLedger.DataSource = ds1.Tables[0];
                gvLedger.DataBind();
                GvItem.DataSource = ds1.Tables[1];
                GvItem.DataBind();
                gvSubLedger.DataSource = ds1.Tables[2];
                gvSubLedger.DataBind();
                GVLBillbyBill.DataSource = ds1.Tables[4];
                GVLBillbyBill.DataBind();
                lblNarration.Text = "<b>Narration:</b>" + " " + ds1.Tables[3].Rows[0]["VoucherTx_Narration"].ToString();
            }
        }

    }
    protected void gvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblBVID = (Label)e.Row.FindControl("lblBVID");
            Label lblBLID = (Label)e.Row.FindControl("lblBLID");
            GridView GVMLBillbyBill = (GridView)e.Row.FindControl("GVMLBillbyBill");
            DataSet ds2 = null;
            ds2 = objdb.ByProcedure("SpFinRptCashBankBooksNew", new string[] { "flag", "BVoucherTx_ID", "BLedger_ID", }, new string[] { "14", lblBVID.Text, lblBLID.Text }, "dataset");
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {

                GVMLBillbyBill.DataSource = ds2;
                GVMLBillbyBill.DataBind();

            }

        }
    }
}