using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;
using System.Linq;
public partial class mis_Finance_RptDaybook_PCS : System.Web.UI.Page
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
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
					//ViewState["OfficeType_Title"] = Session["OfficeType_Title"].ToString();
                   
                    divExcel.Visible = false;
                    txtDate.Attributes.Add("readonly", "readonly");
                    FillVoucherDate();
                    if(ViewState["Office_ID"].ToString()== "1")
                    {
                        FillDFO();
                        divdfo.Visible = true;
                        ddlOffice.Enabled = true;
                        ddlDFO_SelectedIndexChanged(sender, e);
                        
                    }
                    else
                    {
                        divdfo.Visible = false;
                        FillDropdown();
                        ddlOffice.Enabled = false;
                    }
                   
                    FillGrid("0");
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;

                    lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
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
    protected void FillDFO()
    {
        try
        {
           
            ds = objdb.ByProcedure("Usp_GetDFOForAccounting",
                   new string[] { },
                   new string[] { }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlDFO.DataSource = ds;
                ddlDFO.DataTextField = "Office_Name";
                ddlDFO.DataValueField = "Office_ID";
                ddlDFO.DataBind();
                ddlDFO.Items.Insert(0, new ListItem("Select", "0"));
                
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
           
            ds = objdb.ByProcedure("Usp_GetPCSForAccounting",
                   new string[] {},
                   new string[] {}, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
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
                txtDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            lblMsg.Text = "";
            FillGrid("0");
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid(string DayV)
    {
        try
        {
            string headingFirst = "<p class='text-center' style='font-weight:600; font-size:16px;'>Day Book<br /> M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd, <br/> [ " + ddlOffice.SelectedItem.Text + " ] <br />  " + Convert.ToDateTime(txtDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
            lblheadingFirst.Text = headingFirst;
            GridView1.DataSource = new string[] { };
			GridView1.DataBind();
            //if (ddlOffice.SelectedIndex > 0)
            //{

            string sDate = (Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")).ToString();
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
            if(ddlOffice.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "VoucherTx_Date", "FinancialYear", "DayV" }, new string[] { "44", ddlOffice.SelectedValue.ToString(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), FinancialYear, DayV }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[1].Rows.Count != 0)
                {
                    txtDate.Text = ds.Tables[1].Rows[0]["VoucherTx_Date"].ToString();
                }
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {

                    GridView1.DataSource = ds;
                    divExcel.Visible = true;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    decimal LedgerCreditTotal = 0;
                    decimal LedgerDebitTotal = 0;
                    LedgerCreditTotal = Convert.ToDecimal(ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt")));
                    LedgerDebitTotal = Convert.ToDecimal(ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt")));
                    GridView1.FooterRow.Cells[4].Text = "<b>Total : </b>";
                    GridView1.FooterRow.Cells[5].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
                    GridView1.FooterRow.Cells[6].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                }

                //}

                foreach (GridViewRow rows in GridView1.Rows)
                {
                    LinkButton lnkEdit = (LinkButton)rows.FindControl("hpEdit");
                    LinkButton lnkDelete = (LinkButton)rows.FindControl("Delete");
                    LinkButton lnkPrint = (LinkButton)rows.FindControl("hpprint");
                    Label lblVoucherTx_Type = (Label)rows.FindControl("lblVoucherTx_Type");
                    Label lblOfficeID = (Label)rows.FindControl("lblOfficeID");
                    Label lblV_Editright = (Label)rows.FindControl("lblV_Editright");


                    if (ViewState["Office_ID"].ToString() != lblOfficeID.Text)
                    {
                        lnkDelete.Visible = false;
                        lnkEdit.Visible = false;
                        lnkPrint.Visible = false;
                    }
                    else
                    {
                        if (lblVoucherTx_Type.Text == "Payment" || lblVoucherTx_Type.Text == "Contra" || lblVoucherTx_Type.Text == "Receipt" || lblVoucherTx_Type.Text == "Journal" || lblVoucherTx_Type.Text == "Cash Payment" || lblVoucherTx_Type.Text == "Bank Receipt" || lblVoucherTx_Type.Text == "Journal HO" || lblVoucherTx_Type.Text == "GSTService Purchase" || lblVoucherTx_Type.Text == "CashSale Voucher" || lblVoucherTx_Type.Text == "CreditSale Voucher" || lblVoucherTx_Type.Text == "GSTGoods Purchase" || lblVoucherTx_Type.Text == "Goods Purchase Tax Free")
                        {
                            lnkPrint.Visible = true;
                        }
                        else
                        {
                            lnkPrint.Visible = false;
                        }
                        lnkDelete.Visible = true;
                        if (lblVoucherTx_Type.Text == "Item Credit Note Voucher" || lblVoucherTx_Type.Text == "Item Debit Note Voucher")
                        {
                            lnkEdit.Visible = false;
                        }
                        else
                        {
                            lnkEdit.Visible = true;
                        }
                    }
                    if (lblV_Editright.Text == "No")
                    {
                        lnkDelete.Visible = false;
                        lnkEdit.Visible = false;
                    }


                }
            }
           
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
            FillGrid("0");

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
                    if (VoucherTx_Type == "Contra" || VoucherTx_Type == "GSTService Purchase" || VoucherTx_Type == "Cash Payment")
                    {

                        string Url = "VoucherContraInvoice.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                        Response.Redirect(Url);


                    }
                    else if (VoucherTx_Type == "Receipt" || VoucherTx_Type == "Journal" || VoucherTx_Type == "Payment")
                    {

                        if (ViewState["OfficeType_Title"].ToString() == "Division Office (DFO)" || ViewState["OfficeType_Title"].ToString() == "Primary Cooperative Society(PCS)")
                        {
                            //string Url = "VoucherContraInvoice.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
		                   string Url = "VoucherPrintNew.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                            Response.Redirect(Url);
                        }
                        else
                        {
                            string Url = "VoucherPrintNew.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                            Response.Redirect(Url);
                        }


                    }
                    //else if (VoucherTx_Type == "Receipt" || VoucherTx_Type == "Journal" || VoucherTx_Type == "Bank Receipt" || VoucherTx_Type == "Journal HO")
                    //{

                    //    string Url = "VoucherJournalInvoice.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                    //    Response.Redirect(Url);



                    //}
                    else if (VoucherTx_Type == "CreditSale Voucher")
                    {
                        string Url = "VoucherSalepurchaseInvocieQR.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                        Response.Redirect(Url);
                    }
                    else if (VoucherTx_Type == "CashSale Voucher")
                    {
                        string Url = "VoucherSalepurchaseInvocieNew.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                        Response.Redirect(Url);
                    }
                    //      else if (VoucherTx_Type == "CashSale Voucher" || VoucherTx_Type == "CreditSale Voucher" || VoucherTx_Type == "GSTGoods Purchase" || VoucherTx_Type == "Goods Purchase Tax Free")
                    else if (VoucherTx_Type == "GSTGoods Purchase" || VoucherTx_Type == "Goods Purchase Tax Free")
                    {
                        string Url = "VoucherSalepurchaseInvocie.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
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
            string OfficeID = objdb.Encrypt(ddlOffice.SelectedValue.ToString());
            string Date = objdb.Encrypt(txtDate.Text);


            string url = "RptGraphicalDayBook.aspx?OfficeID=" + OfficeID + "&Date=" + Date;

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_blank');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            lblMsg.Text = "";
            FillGrid("0");
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
            string DayV = "0";
            FillGrid(DayV);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearchPrv_Click(object sender, EventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            lblMsg.Text = "";
            string DayV = "-1";
            FillGrid(DayV);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearchNext_Click(object sender, EventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            lblMsg.Text = "";
            string DayV = "1";
            FillGrid(DayV);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlDFO_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlOffice.Items.Clear();
        if(ddlDFO.SelectedIndex  > 0)
        {
            ds = objdb.ByProcedure("Usp_GetDFOWisePCSForAccounting",
                new string[] { "DFO_ID" },
                new string[] { ddlDFO.SelectedValue }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                
                //ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
        }
        ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
    }
}
