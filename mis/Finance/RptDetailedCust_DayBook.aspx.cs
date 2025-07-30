using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

public partial class mis_Finance_RptDetailedCust_DayBook : System.Web.UI.Page
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
                    ViewState["OfficeType_Title"] = Session["OfficeType_Title"].ToString();
                    ViewState["Division_ID"] = Session["Division_ID"].ToString();
                    ddlOffice.Enabled = false;
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
                    GetCommonSearch();
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

    //protected void FillDropdown()
    //{
    //    try
    //    {
    //        if (ViewState["Office_ID"].ToString() == "1")
    //        {
    //            ddlOffice.Enabled = true;
    //        }
    //        ds = objdb.ByProcedure("SpFinVoucherTx",
    //               new string[] { "flag" },
    //               new string[] { "26" }, "dataset");
    //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlOffice.DataSource = ds;
    //            ddlOffice.DataTextField = "Office_Name";
    //            ddlOffice.DataValueField = "Office_ID";
    //            ddlOffice.DataBind();
    //            ddlOffice.Items.Insert(0, new ListItem("All", "0"));
    //            ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

    //    }
    //}
    protected void FillDropdown()
    {
        try
        {

            ddlOffice.Enabled = false;
            ddlRegionalOffice.Enabled = false;
            divRegionalOffice.Visible = false;
            if (ViewState["OfficeType_Title"].ToString() != "District Office (DFO)" && ViewState["OfficeType_Title"].ToString() != "Production Unit")
            {
                ddlOffice.Enabled = true;
                ddlRegionalOffice.Enabled = true;
                divRegionalOffice.Visible = true;
                ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "21" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlRegionalOffice.DataSource = ds;
                    ddlRegionalOffice.DataTextField = "Office_Name";
                    ddlRegionalOffice.DataValueField = "Office_ID";
                    ddlRegionalOffice.DataBind();
                    ddlRegionalOffice.Items.Insert(0, new ListItem("All", "0"));
                    ddlRegionalOffice.Items.Insert(ds.Tables[0].Rows.Count + 1, new ListItem("Production Unit", "010"));
                    if (ViewState["OfficeType_Title"].ToString() == "Circle Office (CCF)")
                    {
                        ddlRegionalOffice.Enabled = false;
                        ddlRegionalOffice.SelectedValue = ViewState["Division_ID"].ToString();
                    }
                }
            }

            FillOffice();



            //if (ViewState["Office_ID"].ToString() == "1")
            //{
            //    ddlOffice.Enabled = true;
            //}

            //ds = objdb.ByProcedure("SpFinRptTrialBalanceNewFF",
            //       new string[] { "flag" },
            //       new string[] { "0" }, "dataset");
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlOffice.DataSource = ds;
            //    ddlOffice.DataTextField = "Office_Name";
            //    ddlOffice.DataValueField = "Office_ID";
            //    ddlOffice.DataBind();
            //    //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
            //    ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillOffice()
    {
        try
        {

            //ddlOffice.Enabled = false;
            //ddlRegionalOffice.Enabled = false;

            ddlOffice.Items.Clear();
            if (ddlRegionalOffice.SelectedIndex > 0)
            {
                if (ddlRegionalOffice.SelectedValue.ToString() != "010" && ddlRegionalOffice.SelectedItem.ToString() != "Production Unit")
                {
                    ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag", "Office_ID" },
                           new string[] { "23", ddlRegionalOffice.SelectedValue.ToString() }, "dataset");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlOffice.DataSource = ds;
                        ddlOffice.DataTextField = "Office_Name";
                        ddlOffice.DataValueField = "Office_ID";
                        ddlOffice.DataBind();
                        //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                        // ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                    }
                }
                else
                {
                    ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag" },
                           new string[] { "24" }, "dataset");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlOffice.DataSource = ds;
                        ddlOffice.DataTextField = "Office_Name";
                        ddlOffice.DataValueField = "Office_ID";
                        ddlOffice.DataBind();
                        //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                        // ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                    }

                }
            }
            else
            {
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
            string headingFirst = "<p class='text-center' style='font-weight:600; font-size:16px;'>Custom Day Book<br />SFA Technologies Pvt. Ltd.<br/> [ " + ddlOffice.SelectedItem.Text + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
            lblheadingFirst.Text = headingFirst;


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
            //ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "FromDate", "ToDate", "FinancialYear" }, new string[] { "21", ddlOffice.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), FinancialYear }, "dataset");
            ds = objdb.ByProcedure("SpFinCustDayBook", new string[] { "Office_ID_Mlt", "FromDate", "ToDate", "FinancialYear" }, new string[] { Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), FinancialYear }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                btnPrint.Visible = true;
                GridView1.DataSource = ds;
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
                        lnkEdit.Visible = false;
                        lnkDelete.Visible = false;
                    }
                }


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
                //    if (VoucherTx_Type == "Payment" || VoucherTx_Type == "Contra" || VoucherTx_Type == "GSTService Purchase" || VoucherTx_Type == "Cash Payment")
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
                //    else if (VoucherTx_Type == "Receipt" || VoucherTx_Type == "Journal" || VoucherTx_Type == "Bank Receipt" || VoucherTx_Type == "Journal HO")
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

                /*****************SET Search Option Start********************/
                #region SetSearchOption
                Session["CommonFromDate"] = txtFromDate.Text;
                Session["CommonToDate"] = txtToDate.Text;
                #endregion
                /*****************SET Search Option End********************/

                lblheadingFirst.Text = "";
                lblMsg.Text = "";
                GridView1.DataSource = null;
                GridView1.DataBind();

                FillGrid();
                foreach (GridViewRow rows in GridView1.Rows)
                {
                    LinkButton lnkEdit = (LinkButton)rows.FindControl("hpEdit");
                    LinkButton lnkDelete = (LinkButton)rows.FindControl("Delete");
                    Label lblOfficeID = (Label)rows.FindControl("lblOfficeID");
                    Label lblV_Editright = (Label)rows.FindControl("lblV_Editright");
                    if (lblOfficeID.Text == ViewState["Office_ID"].ToString())
                    {
                        lnkEdit.Visible = true;
                        lnkDelete.Visible = true;
                    }
                    else
                    {

                        lnkEdit.Visible = false;
                        lnkDelete.Visible = false;
                    }
                    if (lblV_Editright.Text == "No")
                    {
                        lnkEdit.Visible = false;
                        lnkDelete.Visible = false;
                    }

                }
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;

                lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

        GridView1.AllowPaging = false;
        ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "FromDate", "ToDate" }, new string[] { "21", ddlOffice.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
            GridView1.Columns[7].Visible = false;
            GridView1.Columns[8].Visible = false; 
            GridView1.Columns[9].Visible = false;
            
            
        }
        StringBuilder sb1 = new StringBuilder();
        sb1.Append("<p class='text-center' style='font-weight:600; font-size:16px; text-align:center'> Custom Day Book<br />M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd<br/> [ " + ddlOffice.SelectedItem.Text + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>");
        //sb1.Append("<div style='padding-bottom:20px; font-size:20px;'>Custom DayBook:  "+ddlOffice.SelectedItem.Text+"  (Date: " + txtFromDate.Text + " - " + txtToDate.Text + ")</div>");
        string gridHTML = sb1.ToString();
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        GridView1.RenderControl(hw);


        gridHTML += sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");

        StringBuilder sb = new StringBuilder();

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

        ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "FromDate", "ToDate" }, new string[] { "21", ddlOffice.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
        {
            GridView1.AllowPaging = true;
            GridView1.DataSource = ds;
            GridView1.DataBind();
            GridView1.Columns[7].Visible = true;

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

                        //string Url = "VoucherPrintNew.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                        //Response.Redirect(Url);
                        if (ViewState["OfficeType_Title"].ToString() == "Division Office (DFO)")
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
                    else if (VoucherTx_Type == "CashSale Voucher")
                    {
                        string Url = "VoucherSalepurchaseInvocieNew.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                        Response.Redirect(Url);
                    }
                    else if (VoucherTx_Type == "CreditSale Voucher" || VoucherTx_Type == "GSTGoods Purchase" || VoucherTx_Type == "Goods Purchase Tax Free")
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
            GridView GVLCostCentre = (GridView)e.Row.FindControl("GVLCostCentre");
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

                GVLCostCentre.DataSource = ds1.Tables[5];
                GVLCostCentre.DataBind();
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
			Label lblLedgerTx_OrderBy = (Label)e.Row.FindControl("lblLedgerTx_OrderBy");
            GridView GVMLBillbyBill = (GridView)e.Row.FindControl("GVMLBillbyBill");
            GridView GVMLCostCentre = (GridView)e.Row.FindControl("GVMLCostCentre");
			 GridView GVMLChequeDetail = (GridView)e.Row.FindControl("GVMLChequeDetail");
            DataSet ds2 = null;
            //ds2 = objdb.ByProcedure("SpFinRptCashBankBooksNew", new string[] { "flag", "BVoucherTx_ID", "BLedger_ID", }, new string[] { "14", lblBVID.Text, lblBLID.Text }, "dataset");
			 ds2 = objdb.ByProcedure("SpFinRptCashBankBooksNew", new string[] { "flag", "BVoucherTx_ID", "BLedger_ID", "LedgerTx_OrderBy" }, new string[] { "14", lblBVID.Text, lblBLID.Text, lblLedgerTx_OrderBy.Text }, "dataset");
            if (ds2 != null && ds2.Tables.Count > 0)
            {
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    GVMLBillbyBill.DataSource = ds2.Tables[0];
                    GVMLBillbyBill.DataBind();
                }
                if (ds2.Tables[1].Rows.Count > 0)
                {
                    GVMLCostCentre.DataSource = ds2.Tables[1];
                    GVMLCostCentre.DataBind();
                }
				if (ds2.Tables[2].Rows.Count > 0)
                {
                    GVMLChequeDetail.DataSource = ds2.Tables[2];
                    GVMLChequeDetail.DataBind();
                }

            }

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
    protected void ddlRegionalOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillOffice();
    }
}