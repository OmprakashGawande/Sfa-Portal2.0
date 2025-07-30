using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq;

public partial class mis_Finance_RptDetailedDaybook : System.Web.UI.Page
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
                    ddlOffice.Enabled = false;
                    txtDate.Attributes.Add("readonly", "readonly");
                    FillVoucherDate();
                    FillDropdown();
                    FillGrid();

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
            lblMsg.Text = "";
            FillGrid();
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
            string headingFirst = "<p class='text-center' style='font-weight:600; font-size:16px;'>Day Book<br />SFA Technologies Pvt. Ltd.<br/> [ " + ddlOffice.SelectedItem.Text + " ] <br />  " + Convert.ToDateTime(txtDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
            lblheadingFirst.Text = headingFirst;
            GridView1.DataSource = new string[] { };

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
           

            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "VoucherTx_Date", "FinancialYear" }, new string[] { "10", ddlOffice.SelectedValue.ToString(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), FinancialYear }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
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
                    if (lblVoucherTx_Type.Text == "Payment" || lblVoucherTx_Type.Text == "Contra" || lblVoucherTx_Type.Text == "Receipt" || lblVoucherTx_Type.Text == "Journal" || lblVoucherTx_Type.Text == "Cash Payment" || lblVoucherTx_Type.Text == "Bank Receipt" || lblVoucherTx_Type.Text == "Journal HO" || lblVoucherTx_Type.Text == "GSTService Purchase")
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
            //foreach (GridViewRow rows in GridView1.Rows)
            //{

            //    Label lblVoucherTx_Type = (Label)rows.FindControl("lblVoucherTx_Type");
            //    HyperLink lnkEdit = (HyperLink)rows.FindControl("hpEdit");
            //    HyperLink hpprint1 = (HyperLink)rows.FindControl("hpprint1");
            //    HyperLink hpprint2 = (HyperLink)rows.FindControl("hpprint2");
            //    if (lblVoucherTx_Type.Text == "CreditNote Voucher" || lblVoucherTx_Type.Text == "DebitNote Voucher")
            //    {
            //        lnkEdit.Visible = false;
            //    }
            //    else
            //    {
            //        lnkEdit.Visible = true;

            //    }
            //    if (lblVoucherTx_Type.Text == "Payment" || lblVoucherTx_Type.Text == "Contra" || lblVoucherTx_Type.Text == "GSTService Purchase" || lblVoucherTx_Type.Text == "Cash Payment")
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
            //    else if (lblVoucherTx_Type.Text == "Receipt" || lblVoucherTx_Type.Text == "Journal" || lblVoucherTx_Type.Text == "Bank Receipt" || lblVoucherTx_Type.Text == "Journal HO")
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
            if(ds1 != null)
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
            
            //int Count1 = ds1.Tables[0].Rows.Count;

            //string Narration = ds1.Tables[1].Rows[0]["VoucherTx_Narration"].ToString();
            //StringBuilder htmlStr = new StringBuilder();
            //htmlStr.Append("<table  id='DetailGrid' class='table table-hover table-bordered' style='width:100%'>");
            //htmlStr.Append("<tbody>");
            //htmlStr.Append("<tr>");

            //if (Count1 > 1)
            //{

            //    htmlStr.Append("<td><p class='HideRecord'>(As per Details) <br/></p>");
            //    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            //    {
            //        htmlStr.Append("<table width='100%'>");
            //        for (int j = 0; j < Count1; j++)
            //        {
            //            htmlStr.Append("<tr>");
            //            if (j == 0)
            //            {
            //                htmlStr.Append("<td><p class='subledger'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span></p></td>");
            //            }
            //            else
            //            {
            //                htmlStr.Append("<td><p class='subledger HideRecord' ><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span></p></td>");
            //            }

            //            htmlStr.Append("<td style='float:right;'><span class='Ledger_Amt HideRecord'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
            //            htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span></td>");

            //            htmlStr.Append("</tr>");
            //        }
            //        htmlStr.Append("</table>");
            //    }

            //    htmlStr.Append("\n<p class='subledger HideRecord'><span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span></p>");
            //    htmlStr.Append("</td>");

            //}
            //else
            //{
            //    htmlStr.Append("<td>" + lblLedger_Name.Text + "");
            //    htmlStr.Append("\n<p class='subledger HideRecord'><span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span></p>");
            //    htmlStr.Append("</td>");

            //}
            //htmlStr.Append("</tr>");
            //htmlStr.Append("</tbody>");
            //htmlStr.Append("</table>");
            //HtmlGenericControl div = (HtmlGenericControl)e.Row.FindControl("div");
            //div.InnerHtml = htmlStr.ToString();

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
              ds2 = objdb.ByProcedure("SpFinRptCashBankBooksNew", new string[] { "flag", "BVoucherTx_ID", "BLedger_ID", "LedgerTx_OrderBy" }, new string[] { "14", lblBVID.Text, lblBLID.Text, lblLedgerTx_OrderBy.Text }, "dataset");
            if (ds2 != null && ds2.Tables.Count > 0)
            {
                if(ds2.Tables[0].Rows.Count > 0)
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
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            lblMsg.Text = "";
            FillGrid();
            //watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;

            //lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}
