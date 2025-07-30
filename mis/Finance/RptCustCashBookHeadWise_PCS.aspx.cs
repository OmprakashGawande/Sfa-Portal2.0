using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;


public partial class mis_Finance_RptCustCashBookHeadWise_PCS : System.Web.UI.Page
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
                    //ViewState["OfficeType_Title"] = Session["OfficeType_Title"].ToString();
                    ViewState["Division_ID"] = Session["Division_ID"].ToString();
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

    protected void FillDropdown()
    {
        try
        {
            if (ViewState["Office_ID"].ToString() == "1")
            {
                ddlOffice.Enabled = true;
            }
            ds = objdb.ByProcedure("Usp_GetPCSForAccounting",
                   new string[] { },
                   new string[] { }, "dataset");
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
                txtToDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    //Receipt Detail
   
    //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {

    //        if (e.CommandName == "Editing")
    //        {
    //            string VoucherTx_ID = e.CommandArgument.ToString();
    //            DataSet dsPageURL = objdb.ByProcedure("SpFinVoucherTx",
    //                new string[] { "flag", "VoucherTx_ID" },
    //                new string[] { "30", VoucherTx_ID },
    //                "dataset");

    //            if (dsPageURL != null)
    //            {

    //                string Url = dsPageURL.Tables[0].Rows[0]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
    //                Url = Url + "&Action=" + objdb.Encrypt("2");
    //                Response.Redirect(Url);

    //            }
    //        }
    //        if (e.CommandName == "View")
    //        {
    //            string VoucherTx_ID = e.CommandArgument.ToString();
    //            DataSet dsPageURL = objdb.ByProcedure("SpFinVoucherTx",
    //                new string[] { "flag", "VoucherTx_ID" },
    //                new string[] { "30", VoucherTx_ID },
    //                "dataset");

    //            if (dsPageURL != null)
    //            {

    //                string Url = dsPageURL.Tables[0].Rows[0]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
    //                Url = Url + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(dsPageURL.Tables[1].Rows[0]["Office_ID"].ToString());

    //                Response.Redirect(Url);

    //            }

    //        }
    //        if (e.CommandName == "Print")
    //        {

    //            string VoucherTx_ID = e.CommandArgument.ToString();
    //            ds = objdb.ByProcedure("SpFinVoucherTx",
    //              new string[] { "flag", "VoucherTx_ID" },
    //              new string[] { "31", VoucherTx_ID },
    //              "dataset");

    //            if (ds != null)
    //            {
    //                string VoucherTx_Type = ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString();
    //                if (VoucherTx_Type == "Contra" || VoucherTx_Type == "GSTService Purchase" || VoucherTx_Type == "Cash Payment")
    //                {

    //                    string Url = "VoucherContraInvoice.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
    //                    Response.Redirect(Url);


    //                }
    //                else if (VoucherTx_Type == "Receipt" || VoucherTx_Type == "Journal" || VoucherTx_Type == "Payment")
    //                {

    //                    string Url = "VoucherPrintNew.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
    //                    Response.Redirect(Url);



    //                }
                   
    //                else if (VoucherTx_Type == "CreditSale Voucher")
    //                {
    //                    string Url = "VoucherSalepurchaseInvocieQR.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
    //                    Response.Redirect(Url);
    //                }
    //                else if (VoucherTx_Type == "CashSale Voucher")
    //                {
    //                    string Url = "VoucherSalepurchaseInvocieNew.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
    //                    Response.Redirect(Url);
    //                }
    //                else if (VoucherTx_Type == "GSTGoods Purchase" || VoucherTx_Type == "Goods Purchase Tax Free")
    //                {
    //                    string Url = "VoucherSalepurchaseInvocie.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
    //                    Response.Redirect(Url);
    //                }
    //                else
    //                {

    //                }

    //            }




    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    //protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    //{

    //    try
    //    {
    //        lblMsg.Text = "";
    //        string VoucherTx_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();

    //        objdb.ByProcedure("SpFinVoucherTx",
    //               new string[] { "flag", "VoucherTx_ID", "Emp_ID" },
    //               new string[] { "12", VoucherTx_ID, ViewState["Emp_ID"].ToString() }, "dataset");

    //        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Deleted.");
    //        FillGrid();

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

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

                lblMsg.Text = "";
                FillGrid();
                //foreach (GridViewRow rows in GridView1.Rows)
                //{
                //    LinkButton lnkEdit = (LinkButton)rows.FindControl("hpEdit");
                //    LinkButton lnkDelete = (LinkButton)rows.FindControl("Delete");
                //    Label lblOfficeID = (Label)rows.FindControl("lblOfficeID");
                //    Label lblV_Editright = (Label)rows.FindControl("lblV_Editright");
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
                //    if (lblV_Editright.Text == "No")
                //    {
                //        lnkEdit.Visible = false;
                //        lnkDelete.Visible = false;
                //    }

                //}

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
        //try
        //{
        //    GridView1.PageIndex = e.NewPageIndex;
        //    lblMsg.Text = "";
        //    FillGrid();
        //}
        //catch (Exception ex)
        //{
        //    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        //}
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

        //GridView1.AllowPaging = false;
        //ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "FromDate", "ToDate" }, new string[] { "21", ddlOffice.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        //if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
        //{
        //    GridView1.DataSource = ds;
        //    GridView1.DataBind();
        //    GridView1.Columns[7].Visible = false;
        //}
        //StringBuilder sb1 = new StringBuilder();
        //sb1.Append("<p class='text-center' style='font-weight:600; font-size:16px; text-align:center'> Custom Cash Book<br /> MP State Agro Industries Development Corporation, <br/> [ " + ddlOffice.SelectedItem.Text + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>");
        ////sb1.Append("<div style='padding-bottom:20px; font-size:20px;'>Custom DayBook:  "+ddlOffice.SelectedItem.Text+"  (Date: " + txtFromDate.Text + " - " + txtToDate.Text + ")</div>");
        //string gridHTML = sb1.ToString();
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);

        //GridView1.RenderControl(hw);


        //gridHTML += sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");

        //StringBuilder sb = new StringBuilder();

        //sb.Append("<script type = 'text/javascript'>");

        //sb.Append("window.onload = new function(){");

        //sb.Append("var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');");

        //sb.Append("WinPrint.document.write(\"");


        //sb.Append(gridHTML);

        //sb.Append("\");");

        //sb.Append("WinPrint.document.close();");

        //sb.Append("WinPrint.focus();");

        //sb.Append("WinPrint.print();");

        //sb.Append("WinPrint.close();};");

        //sb.Append("</script>");

        //ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());

        //ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "FromDate", "ToDate" }, new string[] { "21", ddlOffice.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        //if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
        //{
        //    GridView1.AllowPaging = true;
        //    GridView1.DataSource = ds;
        //    GridView1.DataBind();
        //    GridView1.Columns[7].Visible = true;

        //}
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
    
    protected void FillGrid()
    {
        try
        {
            btnPrint.Visible = false;
            divbank.Visible = false;

            lnkBankOpening.Text = "";
            lnkBankOpening.Text = "";
            lblheadingFirst.Text = "";
            lblReceiptHeading.Text = "";
            lblPaymentHeading.Text = "";
            GvReceiptDetail.DataSource = string.Empty;
            GvReceiptDetail.DataBind();
            GvPaymentDetail.DataSource = string.Empty;
            GvPaymentDetail.DataBind();
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
            ds = objdb.ByProcedure("SpFinCustCashBookHeadWise", new string[] { "Office_ID_Mlt", "FromDate", "ToDate", "FinancialYear" }, new string[] { Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), FinancialYear }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables.Count != 0)
            {
                
                string headingFirst = "<p class='text-center' style='font-weight:600; text-align:center'>Head Wise Cash Book<br /> M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd, <br/> [ " + ddlOffice.SelectedItem.Text + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
                lblheadingFirst.Text = headingFirst;
                lblReceiptHeading.Text = "<p class='text-center' style='font-weight:600; text-align:center'>प्राप्ति विवरण</p>";
                lblPaymentHeading.Text = "<p class='text-center' style='font-weight:600; text-align:center'>भुगतान विवरण</p>";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    divExcel.Visible = true;

                    btnPrint.Visible = true;
                    GvReceiptDetail.DataSource = ds.Tables[0];
                    GvReceiptDetail.DataBind();

                    decimal TotalAmount = 0;
                    TotalAmount = Convert.ToDecimal(ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("Amount")));
                    
                    GvReceiptDetail.FooterRow.Cells[1].Text = "<b>Total : </b>";
                    GvReceiptDetail.FooterRow.Cells[2].Text = "<b>" + TotalAmount.ToString() + "</b>";
                   

                    GvReceiptDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GvReceiptDetail.UseAccessibleHeader = true;
                   
                    foreach (GridViewRow rows in GvReceiptDetail.Rows)
                    {
                        LinkButton lnkEdit = (LinkButton)rows.FindControl("hpEdit");
                        LinkButton lnkDelete = (LinkButton)rows.FindControl("Delete");
                        LinkButton lnkPrint = (LinkButton)rows.FindControl("hpprint");
                       
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
                            
                                lnkPrint.Visible = true;
                           

                        }
                        if (lblV_Editright.Text == "No")
                        {
                            lnkEdit.Visible = false;
                            lnkDelete.Visible = false;
                        }
                    }
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    divExcel.Visible = true;
                    btnPrint.Visible = true;
                    GvPaymentDetail.DataSource = ds.Tables[1];
                    GvPaymentDetail.DataBind();
                   
                    decimal TotalAmount = 0;
                    TotalAmount = Convert.ToDecimal(ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal?>("Amount")));

                  
                    GvPaymentDetail.FooterRow.Cells[1].Text = "<b>Total : </b>";
                    GvPaymentDetail.FooterRow.Cells[2].Text = "<b>" + TotalAmount.ToString() + "</b>";

                    GvPaymentDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GvPaymentDetail.UseAccessibleHeader = true;
                    foreach (GridViewRow rows in GvPaymentDetail.Rows)
                    {
                        LinkButton lnkEdit = (LinkButton)rows.FindControl("hpEdit");
                        LinkButton lnkDelete = (LinkButton)rows.FindControl("Delete");
                        LinkButton lnkPrint = (LinkButton)rows.FindControl("hpprint");
                      
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
                            
                                lnkPrint.Visible = true;
                           

                        }
                        if (lblV_Editright.Text == "No")
                        {
                            lnkEdit.Visible = false;
                            lnkDelete.Visible = false;
                        }
                    }
                }


                if (ds.Tables[2].Rows.Count > 0)
                {
                    divbank.Visible = true;
                    decimal OpeningBalance = 0;
                    decimal ClosingBalance = 0;
                    string OpeningType = "Cr";
                    string ClosingType = "Cr";
                    OpeningBalance = decimal.Parse(ds.Tables[2].Rows[0]["PreOpeningBalance"].ToString());
                    ClosingBalance = decimal.Parse(ds.Tables[2].Rows[0]["OpeningBalance"].ToString());
                    if (OpeningBalance < 0)
                    {
                        OpeningType = "Dr";
                    }
                    if (ClosingBalance < 0)
                    {
                        ClosingType = "Dr";
                    }
                    lnkBankOpening.Text = Math.Abs(OpeningBalance).ToString();
                    lnkBankClosing.Text = Math.Abs(ClosingBalance).ToString();

                }
             

            }
            else
            {
                
                GvReceiptDetail.DataSource = string.Empty;
                GvReceiptDetail.DataBind();
                GvPaymentDetail.DataSource = string.Empty;
                GvPaymentDetail.DataBind();
            }
            //}


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void GvReceiptDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string VoucherTx_ID = e.CommandArgument.ToString();
            LinkButton lnk = (e.CommandSource) as LinkButton;
            GridViewRow row = lnk.NamingContainer as GridViewRow;
            Label PageURL = (Label)row.FindControl("lblPageUrl");
            Label OfficeID = (Label)row.FindControl("lblOfficeID");
            if (e.CommandName == "Editing")
            {
                string Url = PageURL.Text + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                Url = Url + "&Action=" + objdb.Encrypt("2");
                Response.Redirect(Url);
                //string VoucherTx_ID = e.CommandArgument.ToString();
                //DataSet dsPageURL = objdb.ByProcedure("SpFinVoucherTx",
                //    new string[] { "flag", "VoucherTx_ID" },
                //    new string[] { "30", VoucherTx_ID },
                //    "dataset");

                //if (dsPageURL != null)
                //{

                    

                //}
            }
            if (e.CommandName == "View")
            {
                //string VoucherTx_ID = e.CommandArgument.ToString();
                //DataSet dsPageURL = objdb.ByProcedure("SpFinVoucherTx",
                //    new string[] { "flag", "VoucherTx_ID" },
                //    new string[] { "30", VoucherTx_ID },
                //    "dataset");

                //if (dsPageURL != null)
                //{

                //    string Url = dsPageURL.Tables[0].Rows[0]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                //    Url = Url + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(dsPageURL.Tables[1].Rows[0]["Office_ID"].ToString());

                //    Response.Redirect(Url);

                //}
                string Url = PageURL.Text + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                Url = Url + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(OfficeID.Text);

                Response.Redirect(Url);

            }
            if (e.CommandName == "Print")
            {

                //string VoucherTx_ID = e.CommandArgument.ToString();
                //ds = objdb.ByProcedure("SpFinVoucherTx",
                //  new string[] { "flag", "VoucherTx_ID" },
                //  new string[] { "31", VoucherTx_ID },
                //  "dataset");

                //if (ds != null)
                //{
                //    string VoucherTx_Type = ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString();
                //    if (VoucherTx_Type == "Contra" || VoucherTx_Type == "GSTService Purchase" || VoucherTx_Type == "Cash Payment")
                //    {

                //        string Url = "VoucherContraInvoice.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                //        Response.Redirect(Url);


                //    }
                //    else if (VoucherTx_Type == "Receipt" || VoucherTx_Type == "Journal" || VoucherTx_Type == "Payment")
                //    {

                //        string Url = "VoucherPrintNew.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                //        Response.Redirect(Url);



                //    }

                //    else if (VoucherTx_Type == "CreditSale Voucher")
                //    {
                //        string Url = "VoucherSalepurchaseInvocieQR.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                //        Response.Redirect(Url);
                //    }
                //    else if (VoucherTx_Type == "CashSale Voucher")
                //    {
                //        string Url = "VoucherSalepurchaseInvocieNew.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                //        Response.Redirect(Url);
                //    }
                //    else if (VoucherTx_Type == "GSTGoods Purchase" || VoucherTx_Type == "Goods Purchase Tax Free")
                //    {
                //        string Url = "VoucherSalepurchaseInvocie.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                //        Response.Redirect(Url);
                //    }
                //    else
                //    {

                //    }

                //}

                string Url = "VoucherPrintNew.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                Response.Redirect(Url);


            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GvReceiptDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lblMsg.Text = "";
        string VoucherTx_ID = GvReceiptDetail.DataKeys[e.RowIndex].Value.ToString();

        objdb.ByProcedure("SpFinVoucherTx",
               new string[] { "flag", "VoucherTx_ID", "Emp_ID" },
               new string[] { "12", VoucherTx_ID, ViewState["Emp_ID"].ToString() }, "dataset");

        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Deleted.");
        FillGrid();
    }

    protected void GvPaymentDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lblMsg.Text = "";
        string VoucherTx_ID = GvPaymentDetail.DataKeys[e.RowIndex].Value.ToString();

        objdb.ByProcedure("SpFinVoucherTx",
               new string[] { "flag", "VoucherTx_ID", "Emp_ID" },
               new string[] { "12", VoucherTx_ID, ViewState["Emp_ID"].ToString() }, "dataset");

        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Deleted.");
        FillGrid();
    }
    protected void GvPaymentDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string VoucherTx_ID = e.CommandArgument.ToString();
            LinkButton lnk = (e.CommandSource) as LinkButton;
            GridViewRow row = lnk.NamingContainer as GridViewRow;
            Label PageURL = (Label)row.FindControl("lblPageUrl");
            Label OfficeID = (Label)row.FindControl("lblOfficeID");
            if (e.CommandName == "Editing")
            {
                string Url = PageURL.Text + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                Url = Url + "&Action=" + objdb.Encrypt("2");
                Response.Redirect(Url);
                //string VoucherTx_ID = e.CommandArgument.ToString();
                //DataSet dsPageURL = objdb.ByProcedure("SpFinVoucherTx",
                //    new string[] { "flag", "VoucherTx_ID" },
                //    new string[] { "30", VoucherTx_ID },
                //    "dataset");

                //if (dsPageURL != null)
                //{



                //}
            }
            if (e.CommandName == "View")
            {
                //string VoucherTx_ID = e.CommandArgument.ToString();
                //DataSet dsPageURL = objdb.ByProcedure("SpFinVoucherTx",
                //    new string[] { "flag", "VoucherTx_ID" },
                //    new string[] { "30", VoucherTx_ID },
                //    "dataset");

                //if (dsPageURL != null)
                //{

                //    string Url = dsPageURL.Tables[0].Rows[0]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                //    Url = Url + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(dsPageURL.Tables[1].Rows[0]["Office_ID"].ToString());

                //    Response.Redirect(Url);

                //}
                string Url = PageURL.Text + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                Url = Url + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(OfficeID.Text);

                Response.Redirect(Url);

            }
            if (e.CommandName == "Print")
            {

                //string VoucherTx_ID = e.CommandArgument.ToString();
                //ds = objdb.ByProcedure("SpFinVoucherTx",
                //  new string[] { "flag", "VoucherTx_ID" },
                //  new string[] { "31", VoucherTx_ID },
                //  "dataset");

                //if (ds != null)
                //{
                //    string VoucherTx_Type = ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString();
                //    if (VoucherTx_Type == "Contra" || VoucherTx_Type == "GSTService Purchase" || VoucherTx_Type == "Cash Payment")
                //    {

                //        string Url = "VoucherContraInvoice.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                //        Response.Redirect(Url);


                //    }
                //    else if (VoucherTx_Type == "Receipt" || VoucherTx_Type == "Journal" || VoucherTx_Type == "Payment")
                //    {

                //        string Url = "VoucherPrintNew.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                //        Response.Redirect(Url);



                //    }

                //    else if (VoucherTx_Type == "CreditSale Voucher")
                //    {
                //        string Url = "VoucherSalepurchaseInvocieQR.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                //        Response.Redirect(Url);
                //    }
                //    else if (VoucherTx_Type == "CashSale Voucher")
                //    {
                //        string Url = "VoucherSalepurchaseInvocieNew.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                //        Response.Redirect(Url);
                //    }
                //    else if (VoucherTx_Type == "GSTGoods Purchase" || VoucherTx_Type == "Goods Purchase Tax Free")
                //    {
                //        string Url = "VoucherSalepurchaseInvocie.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                //        Response.Redirect(Url);
                //    }
                //    else
                //    {

                //    }

                //}

                string Url = "VoucherPrintNew.aspx" + "?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID);
                Response.Redirect(Url);


            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkBankOpening_Click(object sender, EventArgs e)
    {
		spnBankOpening.InnerText = " As on 1 April " + Convert.ToDateTime(txtFromDate.Text,cult).ToString("yyyy");
        GvPaymentDetail.UseAccessibleHeader = true;
        GvPaymentDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
        GvReceiptDetail.UseAccessibleHeader = true;
        GvReceiptDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
        gvBankOpening.DataSource = string.Empty;
        gvBankOpening.DataBind();
        ds = objdb.ByProcedure("SpFinRptCashBankBooksNew", new string[] { "flag", "Office_ID_Mlt", "Head_ID", "FromDate", "ToDate" }, new string[] { "3", ddlOffice.SelectedValue, "117", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
        {
            decimal Total = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PreOpeningBalance"));
            gvBankOpening.DataSource = ds.Tables[0];;
            gvBankOpening.DataBind();
            gvBankOpening.FooterRow.Cells[1].Text = "<b>Total : </b>";
            gvBankOpening.FooterRow.Cells[2].Text = "<b>" + Math.Abs(Total).ToString() + "</b>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBankOpeningModal();", true);

        }
    }
    protected void lnkBankClosing_Click(object sender, EventArgs e)
    {
        GvPaymentDetail.UseAccessibleHeader = true;
        GvPaymentDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
        GvReceiptDetail.UseAccessibleHeader = true;
        GvReceiptDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
        gvBankClosing.DataSource = string.Empty;
        gvBankClosing.DataBind();
        ds = objdb.ByProcedure("SpFinRptCashBankBooksNew", new string[] { "flag", "Office_ID_Mlt", "Head_ID", "FromDate", "ToDate" }, new string[] { "3", ddlOffice.SelectedValue, "117", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        if (ds.Tables.Count != 0 && ds.Tables[1].Rows.Count != 0)
        {
            gvBankClosing.DataSource = ds.Tables[1];;
            gvBankClosing.DataBind();
            decimal Total = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("OpeningBalance"));

            gvBankClosing.FooterRow.Cells[1].Text = "<b>Total : </b>";
            gvBankClosing.FooterRow.Cells[2].Text = "<b>" + Math.Abs(Total).ToString() + "</b>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBankClosingModal();", true);
        }
    }
}