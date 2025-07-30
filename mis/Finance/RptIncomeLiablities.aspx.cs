using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Drawing;

public partial class mis_Finance_RptIncomeLiablities : System.Web.UI.Page
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
                    HeadList();
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
    protected void HeadList()
    {
        try
        {
            DataTable dt = new DataTable();          
            dt.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
            dt.Columns.Add(new DataColumn("MonthID", typeof(string)));
            ViewState["Heads"] = dt;
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

            ddlOffice.Enabled = false;
            ddlRegionalOffice.Enabled = false;
            divRegionalOffice.Visible = false;
            if (ViewState["OfficeType_Title"].ToString() != "Division Office (DFO)" && ViewState["OfficeType_Title"].ToString() != "Production Unit")
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
                    //ddlRegionalOffice.Items.Insert(ds.Tables[0].Rows.Count + 1, new ListItem("Production Unit", "010"));
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
           // divshow.Visible = false;
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
            if (Office != "")
            {
                ds = objdb.ByProcedure("SpFinIncomeLiablitiesReport", new string[] { "Office_ID_mlt", "FromDate", "ToDate", "OpeningFromDate" }, new string[] { Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    //divshow.Visible = true;
                    string headingFirst = "<p class='text-center' style='font-weight:600; text-align:center'> SFA Technologies Pvt. Ltd.<br/>  " + FinancialYear.ToString() + "  <br />" + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + " प्राप्ति का विवरण</p>";
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class='table table-bordered' border='1'>");
                    sb.Append("<thead class='header'>");
                    sb.Append("<tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='7'>" + headingFirst.ToString() + "</td>");
                    sb.Append("<tr>");
                    sb.Append("<th>मुख्य लेखा शीर्ष</th>");
                    sb.Append("<th>विवरण</th>");
                    sb.Append("<th>लेखा शीर्ष</th>");
                    sb.Append("<th>विवरण</th>");
                    sb.Append("<th>गत माह तक आय</th>");
                    sb.Append("<th>वर्तमान माह में प्राप्त आय</th>");
                    sb.Append("<th>कुल प्राप्त आय</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    sb.Append("<tr style='background-color:wheat'>");
                    sb.Append("<td>1</td>");
                    sb.Append("<td>2</td>");
                    sb.Append("<td>3</td>");
                    sb.Append("<td>4</td>");
                    sb.Append("<td>5</td>");
                    sb.Append("<td>6</td>");
                    sb.Append("<td>7</td>");
                    sb.Append("</tr>");
                    int Count = ds.Tables[0].Rows.Count;
                    string HeadName = "";
                    decimal PreFromOpeningBalance_Total = 0;
                    decimal CurrentTxn_Total = 0;
                    decimal T_Total = 0;
                    for (int i = 0; i < Count; i++)
                    {
                        //decimal Total = Math.Abs((Convert.ToDecimal(ds.Tables[0].Rows[i]["PreFromOpeningBalance"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentTxn"].ToString())));
                        decimal Total = (Convert.ToDecimal(ds.Tables[0].Rows[i]["PreFromOpeningBalance"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentTxn"].ToString()));

                        if (i == 0)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Head_Code"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Head_Name_Hindi"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Code"].ToString() + "</td>");
                            //sb.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name_Hindi"].ToString() + "</td>");
                            sb.Append("<td><a href='#' onclick='GetMonthData(" + ds.Tables[0].Rows[i]["Ledger_ID"].ToString() + ")'>" + ds.Tables[0].Rows[i]["Ledger_Name_Hindi"].ToString() + "</a></td>");
                            sb.Append("<td>" + Math.Abs(Convert.ToDecimal(ds.Tables[0].Rows[i]["PreFromOpeningBalance"].ToString())) + "</td>");
                            sb.Append("<td>" + Math.Abs(Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentTxn"].ToString())) + "</td>");
                            sb.Append("<td>" + Math.Abs(Total).ToString() + "</td>");
                            sb.Append("</tr>");
                            PreFromOpeningBalance_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["PreFromOpeningBalance"].ToString());
                            CurrentTxn_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentTxn"].ToString());
                            T_Total += Total;
                        }
                        else if (HeadName == ds.Tables[0].Rows[i]["Head_Name"].ToString())
                        {
                            sb.Append("<tr>");
                            sb.Append("<td></td>");
                            sb.Append("<td></td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Code"].ToString() + "</td>");
                            sb.Append("<td><a href='#' onclick='GetMonthData(" + ds.Tables[0].Rows[i]["Ledger_ID"].ToString() + ")'>" + ds.Tables[0].Rows[i]["Ledger_Name_Hindi"].ToString() + "</a></td>");
                            sb.Append("<td>" + Math.Abs(Convert.ToDecimal(ds.Tables[0].Rows[i]["PreFromOpeningBalance"].ToString())) + "</td>");
                            sb.Append("<td>" + Math.Abs(Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentTxn"].ToString())) + "</td>");
                            sb.Append("<td>" + Math.Abs(Total).ToString() + "</td>");
                            sb.Append("</tr>");
                            PreFromOpeningBalance_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["PreFromOpeningBalance"].ToString());
                            CurrentTxn_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentTxn"].ToString());
                            T_Total += Total;
                        }
                        else
                        {
                            sb.Append("<tr>");
                            sb.Append("<td></td>");
                            sb.Append("<td></td>");
                            sb.Append("<td></td>");
                            sb.Append("<td style='text-align:right'><b>योग</b></td>");
                            sb.Append("<td><b>" + Math.Abs(PreFromOpeningBalance_Total).ToString() + "</b></td>");
                            sb.Append("<td><b>" + Math.Abs(CurrentTxn_Total).ToString() + "</b></td>");
                            sb.Append("<td><b>" + Math.Abs(T_Total).ToString() + "</b></td>");
                            sb.Append("</tr>");
                            PreFromOpeningBalance_Total = 0;
                            CurrentTxn_Total = 0;
                            T_Total = 0;
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Head_Code"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Head_Name_Hindi"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Code"].ToString() + "</td>");
                            //sb.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name_Hindi"].ToString() + "</td>");
                            sb.Append("<td><a href='#' onclick='GetMonthData(" + ds.Tables[0].Rows[i]["Ledger_ID"].ToString() + ")'>" + ds.Tables[0].Rows[i]["Ledger_Name_Hindi"].ToString() + "</a></td>");
                            sb.Append("<td>" + Math.Abs(Convert.ToDecimal(ds.Tables[0].Rows[i]["PreFromOpeningBalance"].ToString())) + "</td>");
                            sb.Append("<td>" + Math.Abs(Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentTxn"].ToString())) + "</td>");
                            sb.Append("<td>" + Math.Abs(Total).ToString() + "</td>");
                            sb.Append("</tr>");
                            PreFromOpeningBalance_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["PreFromOpeningBalance"].ToString());
                            CurrentTxn_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentTxn"].ToString());
                            T_Total += Total;
                        }
                        HeadName = ds.Tables[0].Rows[i]["Head_Name"].ToString();


                    }
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td style='text-align:right'><b>योग</b></td>");
                    sb.Append("<td><b>" + Math.Abs(PreFromOpeningBalance_Total).ToString() + "</b></td>");
                    sb.Append("<td><b>" + Math.Abs(CurrentTxn_Total).ToString() + "</b></td>");
                    sb.Append("<td><b>" + Math.Abs(T_Total).ToString() + "</b></td>");
                    sb.Append("</tr>");



                    decimal TotalPreFromOpeningBalance_TotalSum = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PreFromOpeningBalance"));
                    decimal CurrentTxn_TotalSum = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CurrentTxn"));
                    decimal TTotalSum = (TotalPreFromOpeningBalance_TotalSum + CurrentTxn_TotalSum);
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td style='text-align:right'><b>महा योग</b></td>");
                    sb.Append("<td><b>" + Math.Abs(TotalPreFromOpeningBalance_TotalSum).ToString() + "</b></td>");
                    sb.Append("<td><b>" + Math.Abs(CurrentTxn_TotalSum).ToString() + "</b></td>");
                    sb.Append("<td><b>" + Math.Abs(TTotalSum).ToString() + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    tableData.InnerHtml = sb.ToString();


                }
                else
                {

                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select atleast one Office.');", true);
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
                //FillGrid();
                FillGridTesting();
              

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
               
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
    

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";

    //        Response.Clear();
    //        Response.AddHeader("content-disposition", "attachment;filename=" + "आय का विवरण" + DateTime.Now + ".xls");
    //        Response.Charset = "";
    //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //        Response.ContentType = "application/vnd.xls";
    //        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
    //        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
    //        tableData.RenderControl(htmlWrite);

    //        Response.Write(stringWrite.ToString());

    //        Response.End();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void ddlRegionalOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillOffice();
    }
    protected void FillGridNextLedger(string Ledger_ID)
    {
        try
        {
            
            GridView3.Visible = true;
            gvData.Visible = false;
           
          
            DataSet ds1 = objdb.ByProcedure("SpFinRptTrialBalanceNewFF", new string[] { "flag", "Ledger_ID" }, new string[] { "10", Ledger_ID }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                lblHeadName.Text = ds1.Tables[0].Rows[0]["Ledger_Name_Hindi"].ToString() + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
            }
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
            // ds = objdb.ByProcedure("SpFinRptTrialBalanceNewFF", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate" }, new string[] { "4", Office, Ledger_ID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            ds = objdb.ByProcedure("SpFinRptTrialBalanceNewFF", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate" }, new string[] { "4", Office, Ledger_ID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                //btnMonthExcel.Visible = true;
                //spnAltB.Visible = true;

                GridView3.DataSource = ds;
                GridView3.DataBind();

                decimal PreOpening = 0;
                decimal OpeningBal = 0;

                if (ds.Tables[0].Rows[0]["PreOpening"].ToString() != "")
                    PreOpening = decimal.Parse(ds.Tables[0].Rows[0]["PreOpening"].ToString());


                OpeningBal = PreOpening;

                //if (chkOpeningBal.Checked == true)
                //{
                //    if ((PreOpening + decimal.Parse(ds.Tables[0].Rows[0]["CurrentOpening"].ToString())) < 0)
                //    {
                //        lblHeadName.Text = lblHeadName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(PreOpening + decimal.Parse(ds.Tables[0].Rows[0]["CurrentOpening"].ToString())).ToString() + " Dr </span>";
                //    }
                //    else
                //    {
                //        lblHeadName.Text = lblHeadName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(PreOpening + decimal.Parse(ds.Tables[0].Rows[0]["CurrentOpening"].ToString())).ToString() + " Cr </span>";
                //    }
                //}

                ViewState["Bal"] = (PreOpening + decimal.Parse(ds.Tables[0].Rows[0]["CurrentOpening"].ToString())).ToString();

                decimal TotDebitAmt = 0;
                decimal TotCreditAmt = 0;

                int rowcount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < rowcount; i++)
                {

                    string DebitAmt = "0";
                    decimal CreditAmt = 0;
                    decimal CurrentOpening = 0;

                    //if (i != 0)
                    //{
                    if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                    {
                        DebitAmt = "-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString();
                        TotDebitAmt = TotDebitAmt + decimal.Parse(ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                    }
                       

                    if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                    {
                        CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                        TotCreditAmt = TotCreditAmt + decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                    }
                        
                    //}

                    if (ds.Tables[0].Rows[0]["CurrentOpening"].ToString() != "")
                        CurrentOpening = decimal.Parse(ds.Tables[0].Rows[i]["CurrentOpening"].ToString());


                    OpeningBal = OpeningBal + CurrentOpening + decimal.Parse(DebitAmt) + CreditAmt;
                    if (OpeningBal >= 0)
                    {
                        GridView3.Rows[i].Cells[4].Text = OpeningBal.ToString() + " Cr";
                    }
                    else
                    {
                        GridView3.Rows[i].Cells[4].Text = Math.Abs(OpeningBal).ToString() + " Dr";
                    }
                }

                GridView3.FooterRow.Cells[1].Text = "Total";
                GridView3.FooterRow.Cells[1].Font.Bold = true;
                GridView3.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;

                if (TotDebitAmt != 0)
                {
                    GridView3.FooterRow.Cells[2].Text = TotDebitAmt.ToString();
                    GridView3.FooterRow.Cells[2].Font.Bold = true;
                    GridView3.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                }

                if (TotCreditAmt != 0)
                {
                    GridView3.FooterRow.Cells[3].Text = TotCreditAmt.ToString();
                    GridView3.FooterRow.Cells[3].Font.Bold = true;
                    GridView3.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                }

            }

            //GridView1.DataSource = new string[] { };
            //GridView1.DataBind();
            //GridView2.DataSource = new string[] { };
            //GridView2.DataBind();


            //ShowHideColumn();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    
    protected void FillGridTesting()
    {
        try
        {
            gvData.DataSource = new string[] { };
            gvData.DataBind();
            GridView3.DataSource = new string[] { };
            GridView3.DataBind();
            GridView3.Visible = false;
            gvData.Visible = true;
            lblheadingFirst.Text = "";
            lblHeadName.Text = "";
            DivTable.InnerHtml = "";
            //divshow.Visible = false;
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
            string OfficeName = "";
            int SerialNo = 0;
            int totalListItem = ddlOffice.Items.Count;
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    SerialNo++;                  
                    Office += item.Value + ",";
                    OfficeName += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
                }
            }
            if (Office != "")
            {
                if (totalListItem == SerialNo)
                {
                    OfficeName = "All Offices";
                }
                else if (SerialNo == 0)
                {
                    OfficeName = "---Office Not Selected---";
                }
                else
                {
                    OfficeName = OfficeName.Remove(OfficeName.Length - 1, 1);
                }
                ds = objdb.ByProcedure("SpFinIncomeLiablitiesReport_New", new string[] { "Office_ID_mlt", "FromDate", "ToDate", "OpeningFromDate" }, new string[] { Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    //divshow.Visible = true;
                    string headingFirst = "<p class='text-center' style='font-weight:600; text-align:center'> मध्य प्रदेश राज्य लघु वनोपज (व्यापर एवं विकास)  सहकारी संघ मर्यादित<br/> [ " + OfficeName + " ] <br />  " + FinancialYear.ToString() + "  <br />" + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + " प्राप्ति का विवरण</p>";
                    lblheadingFirst.Text = headingFirst;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Head_Code", typeof(string));
                    dt.Columns.Add("Head_Name_Hindi", typeof(string));
                    dt.Columns.Add("Ledger_Code", typeof(string));
                    dt.Columns.Add("Ledger_ID", typeof(string));
                    dt.Columns.Add("Ledger_Name_Hindi", typeof(string));
                    dt.Columns.Add("PreFromOpeningBalance", typeof(string));
                    dt.Columns.Add("CurrentTxn", typeof(string));
                    dt.Columns.Add("Total", typeof(string));
                    dt.Rows.Add("1", "2", "3","", "4", "5", "6", "7");
                   
                    int Count = ds.Tables[0].Rows.Count;
                    string HeadName = "";
                    decimal PreFromOpeningBalance_Total = 0;
                    decimal CurrentTxn_Total = 0;
                    decimal T_Total = 0;
                    for (int i = 0; i < Count; i++)
                    {
                        //decimal Total = Math.Abs((Convert.ToDecimal(ds.Tables[0].Rows[i]["PreFromOpeningBalance"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentTxn"].ToString())));
                        decimal Total = (Convert.ToDecimal(ds.Tables[0].Rows[i]["PreFromOpeningBalance"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentTxn"].ToString()));

                        if (i == 0)
                        {
                            dt.Rows.Add(ds.Tables[0].Rows[i]["Head_Code"].ToString(),
                                        ds.Tables[0].Rows[i]["Head_Name_Hindi"].ToString(),
                                        ds.Tables[0].Rows[i]["Ledger_Code"].ToString(),
                                        ds.Tables[0].Rows[i]["Ledger_ID"].ToString(),
                                        ds.Tables[0].Rows[i]["Ledger_Name_Hindi"].ToString(),
                                        Math.Abs(Convert.ToDecimal(ds.Tables[0].Rows[i]["PreFromOpeningBalance"].ToString())),
                                        Math.Abs(Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentTxn"].ToString())),
                                        Math.Abs(Total).ToString()
                                        );
                            
                            PreFromOpeningBalance_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["PreFromOpeningBalance"].ToString());
                            CurrentTxn_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentTxn"].ToString());
                            T_Total += Total;
                        }
                        else if (HeadName == ds.Tables[0].Rows[i]["Head_Name"].ToString())
                        {
                            dt.Rows.Add("",
                                       "",
                                        ds.Tables[0].Rows[i]["Ledger_Code"].ToString(),
                                        ds.Tables[0].Rows[i]["Ledger_ID"].ToString(),
                                        ds.Tables[0].Rows[i]["Ledger_Name_Hindi"].ToString(),
                                        Math.Abs(Convert.ToDecimal(ds.Tables[0].Rows[i]["PreFromOpeningBalance"].ToString())),
                                        Math.Abs(Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentTxn"].ToString())),
                                        Math.Abs(Total).ToString()
                                        );
                            
                            PreFromOpeningBalance_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["PreFromOpeningBalance"].ToString());
                            CurrentTxn_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentTxn"].ToString());
                            T_Total += Total;
                        }
                        else
                        {
                            dt.Rows.Add("",
                                        "",
                                        "",
                                        "",                                       
                                        "योग",
                                        Math.Abs(PreFromOpeningBalance_Total).ToString(),
                                        Math.Abs(CurrentTxn_Total).ToString(),
                                        Math.Abs(T_Total).ToString()
                                        );
                            
                           
                            PreFromOpeningBalance_Total = 0;
                            CurrentTxn_Total = 0;
                            T_Total = 0;
                            dt.Rows.Add(ds.Tables[0].Rows[i]["Head_Code"].ToString(),
                                       ds.Tables[0].Rows[i]["Head_Name_Hindi"].ToString(),
                                       ds.Tables[0].Rows[i]["Ledger_Code"].ToString(),
                                       ds.Tables[0].Rows[i]["Ledger_ID"].ToString(),
                                       ds.Tables[0].Rows[i]["Ledger_Name_Hindi"].ToString(),
                                       Math.Abs(Convert.ToDecimal(ds.Tables[0].Rows[i]["PreFromOpeningBalance"].ToString())),
                                       Math.Abs(Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentTxn"].ToString())),
                                       Math.Abs(Total).ToString()
                                       );
                            PreFromOpeningBalance_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["PreFromOpeningBalance"].ToString());
                            CurrentTxn_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["CurrentTxn"].ToString());
                            T_Total += Total;
                        }
                        HeadName = ds.Tables[0].Rows[i]["Head_Name"].ToString();


                    }
                    dt.Rows.Add("",
                                        "",
                                        "",
                                        "",                                        
                                        "योग",
                                        Math.Abs(PreFromOpeningBalance_Total).ToString(),
                                        Math.Abs(CurrentTxn_Total).ToString(),
                                        Math.Abs(T_Total).ToString()
                                        );



                    decimal TotalPreFromOpeningBalance_TotalSum = ds.Tables[0].AsEnumerable().Where(row => row.Field<string>("Head_Code") != "61").Sum(row => row.Field<decimal>("PreFromOpeningBalance"));
                    decimal CurrentTxn_TotalSum = ds.Tables[0].AsEnumerable().Where(row => row.Field<string>("Head_Code") != "61").Sum(row => row.Field<decimal>("CurrentTxn"));
                    decimal TTotalSum = (TotalPreFromOpeningBalance_TotalSum + CurrentTxn_TotalSum);
                    dt.Rows.Add("",
                                        "",
                                        "",
                                        "",
                                        "महा योग",
                                        Math.Abs(TotalPreFromOpeningBalance_TotalSum).ToString(),
                                        Math.Abs(CurrentTxn_TotalSum).ToString(),
                                        Math.Abs(TTotalSum).ToString()
                                        );

                    gvData.DataSource = dt;
                    gvData.DataBind();

                }
                else
                {

                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select atleast one Office.');", true);
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            lblMsg.Text = "";
            // lblGridMsg.Text = "";
            if (e.CommandName == "View")
            {
                string Ledger_ID = e.CommandArgument.ToString();
                DataTable dt = (DataTable)ViewState["Heads"];
                dt.Rows.Add(Ledger_ID, "0");

                FillGridNextLedger(Ledger_ID);


            }
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
            var watch = System.Diagnostics.Stopwatch.StartNew();
            lblMsg.Text = "";
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView3.Rows[index];
                Label lblLedger_ID = (Label)row.Cells[0].FindControl("lblLedger_ID");
                Label lblMonthID = (Label)row.Cells[0].FindControl("lblMonthID");

                string Ledger_ID = lblLedger_ID.Text;
                string MonthID = lblMonthID.Text;

                DataTable dt = (DataTable)ViewState["Heads"];
                dt.Rows.Add(Ledger_ID, MonthID);
                string PreBal = "0";
                if (index != 0)
                {
                    PreBal = GridView3.Rows[index - 1].Cells[4].Text;

                    string[] bal = PreBal.Split(' ', '\t');

                    if (bal[1].ToString() == "Dr")
                    {
                        ViewState["Bal"] = "-" + bal[0].ToString();
                    }
                    else
                    {
                        ViewState["Bal"] = bal[0].ToString();
                    }
                }
               
                FillGridNextLedgerMonth(Ledger_ID, MonthID);

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;

               // lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGridNextLedgerMonth(string Ledger_ID, string MonthID)
    {
        try
        {
            //btnHeadExcel.Visible = false;
            //btnMonthExcel.Visible = false;
            //btnDayBookExcel.Visible = false;

            ViewState["Ledger_ID"] = Ledger_ID;
            ViewState["MonthID"] = MonthID;
            ViewState["DayBookVisible"] = "true";
            //btnShowDetailBook.Enabled = false;
            //GridView4.DataSource = null;
            //GridView4.DataBind();


            //lblHeadName.Text = "";
            DataSet ds1 = objdb.ByProcedure("SpFinRptTrialBalanceNewFF", new string[] { "flag", "Ledger_ID" }, new string[] { "10", Ledger_ID }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
               
                DateTimeFormatInfo mfi = new DateTimeFormatInfo();
                lblHeadName.Text = ds1.Tables[0].Rows[0]["Ledger_Name_Hindi"].ToString() + "( Month : " + mfi.GetMonthName(int.Parse(MonthID)).ToString() + " )";
            }

            string Office = "";

            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }

            GridView3.DataSource = new string[] { };
            ds = objdb.ByProcedure("SpFinRptTrialBalanceNewFF", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "LedgerTx_Month", "FromDate", "ToDate" }, new string[] { "6", Office, Ledger_ID, MonthID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                DivTable.Visible = true;

                //btnDayBookExcel.Visible = true;
                //btnShowDetailBook.Enabled = true;
                //spnAltB.Visible = true;
                //spnAltW.Visible = true;

                decimal OpeningBal = decimal.Parse(ViewState["Bal"].ToString());

                decimal CurrentBal = 0;
                decimal DebitTotal = 0;
                decimal CreditTotal = 0;
                int rowcount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < rowcount; i++)
                {

                    string DebitAmt = "0";
                    decimal CreditAmt = 0;

                    if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                    {
                        DebitAmt = "-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString();
                        DebitTotal = DebitTotal + decimal.Parse(ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                    {
                        CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                        CreditTotal = CreditTotal + decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                    }

                    CurrentBal = CurrentBal + decimal.Parse(DebitAmt) + CreditAmt;

                }
                gvData.DataSource = new string[] { };
                gvData.DataBind();
                GridView3.DataSource = new string[] { };
                GridView3.DataBind();



                StringBuilder htmlStr = new StringBuilder();

                htmlStr.Append("<table  id='DetailGrid' class='lastdatatable table table-hover table-bordered' >");
                htmlStr.Append("<thead>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<th style='width: 65px;'>Voucher Date</th>");
                htmlStr.Append("<th>Particulars</th>");
                htmlStr.Append("<th>Vch Type</th>");
                htmlStr.Append("<th>Office Name</th>");
                htmlStr.Append("<th style='width:70px !important'>Vch No.</th>");
                htmlStr.Append("<th>Debit Amt.</th>");
                htmlStr.Append("<th>Credit Amt.</th>");
                htmlStr.Append("<th class='hide_print'>Action</th>");
                htmlStr.Append("</tr>");
                htmlStr.Append("</thead>");

                htmlStr.Append("<tbody>");
                int Count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");

                    // Inner Transaction
                    ds1 = null;
                    ds1 = objdb.ByProcedure("SpFinRptTrialBalanceNewFF", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", }, new string[] { "12", ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString(), Ledger_ID }, "dataset");
                    int Count1 = ds1.Tables[0].Rows.Count;

                    string Narration = ds1.Tables[1].Rows[0]["VoucherTx_Narration"].ToString();

                    if (Count1 > 1)
                    {

                        htmlStr.Append("<td><p class='HideRecord'>(As per Details) <br/></p>");
                        if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                        {

                            for (int j = 0; j < Count1; j++)
                            {
                                if (j == 0)
                                {
                                    htmlStr.Append("\n<p class='subledger'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                                }
                                else
                                {
                                    htmlStr.Append("\n<p class='subledger HideRecord'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                                }

                                htmlStr.Append("\t \t \t<span class='Ledger_Amt HideRecord'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span><br/></p>");
                                //htmlStr.Append("<br/>");

                            }
                        }

                        htmlStr.Append("\n<p class='subledger HideRecord'><span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span></p>");
                        htmlStr.Append("</td>");
                    }
                    else
                    {
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                        htmlStr.Append("\n<p class='subledger HideRecord'><span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span></p>");
                        htmlStr.Append("</td>");
                    }


                    //  htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");


                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                    htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                    htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                    htmlStr.Append("<td  class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a> </td>");
                    htmlStr.Append("</tr>");
                }
                htmlStr.Append("</tbody>");
                htmlStr.Append("<tfoot>");

                //OPENING BALANCE TOTAL
                htmlStr.Append("<tr>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td><b>OPENING BALANCE :</b></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                if (OpeningBal < 0)
                {
                    htmlStr.Append("<td class='align-right'><b>" + Math.Abs(OpeningBal).ToString() + "</b></td>");
                    htmlStr.Append("<td></td>");

                }
                else
                {
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td class='align-right'><b>" + Math.Abs(OpeningBal).ToString() + "</b></td>");
                }
                htmlStr.Append("<td  class='hide_print'></td>");
                htmlStr.Append("</tr>");

                //CURRENT TOTAL
                htmlStr.Append("<tr>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td><b>CURRENT TOTAL :</b></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");

                //DebitTotal = 0;
                //CreditTotal = 0;
                htmlStr.Append("<td class='align-right'><b>" + Math.Abs(DebitTotal).ToString() + "</b></td>");
                htmlStr.Append("<td class='align-right'><b>" + Math.Abs(CreditTotal).ToString() + "</b></td>");
                //if (CurrentBal < 0)
                //{
                //    htmlStr.Append("<td class='align-right'><b>" + Math.Abs(CurrentBal).ToString() + "</b></td>");
                //    htmlStr.Append("<td></td>");

                //}
                //else
                //{
                //    htmlStr.Append("<td></td>");
                //    htmlStr.Append("<td class='align-right'><b>" + Math.Abs(CurrentBal).ToString() + "</b></td>");
                //}
                htmlStr.Append("<td class='hide_print'></td>");
                htmlStr.Append("</tr>");
                //CLOSING BALANCE TOTAL
                htmlStr.Append("<tr>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td><b>CLOSING BALANCE : </b></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                if ((OpeningBal + CurrentBal) < 0)
                {
                    htmlStr.Append("<td class='align-right'><b>" + Math.Abs((OpeningBal + CurrentBal)).ToString() + "</b></td>");
                    htmlStr.Append("<td></td>");

                }
                else
                {
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td class='align-right'><b>" + Math.Abs((OpeningBal + CurrentBal)).ToString() + "</b></td>");
                }
                htmlStr.Append("<td class='hide_print'></td>");
                htmlStr.Append("</tr>");
                htmlStr.Append("</tfoot>");
                htmlStr.Append("</table>");

                DivTable.InnerHtml = htmlStr.ToString();

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void BackGrid()
    {
        try
        {

            lblMsg.Text = "";
            DivTable.InnerHtml = "";
            DataTable dt = ViewState["Heads"] as DataTable;
            int DtRow = dt.Rows.Count;
            if (dt.Rows.Count > 1)
            {
                
                string Ledger_ID = dt.Rows[DtRow - 2]["Ledger_ID"].ToString();
                string MonthID = dt.Rows[DtRow - 2]["MonthID"].ToString();
                string flag = "0";
                
                if(Ledger_ID != "0" && MonthID == "0")
                {
                    FillGridNextLedger(Ledger_ID);
                    flag = "1";
                }
                else if (Ledger_ID != "0" && MonthID != "0")
                {
                    FillGridNextLedgerMonth(Ledger_ID, MonthID);
                    flag = "1";
                }
                if (flag == "1")
                {
                    dt.Rows.RemoveAt(DtRow - 1);
                    ViewState["Heads"] = dt;
                }

            }
            else
            {
                FillGridTesting();
            }


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
            var watch = System.Diagnostics.Stopwatch.StartNew();           
            lblMsg.Text = "";
            BackGrid();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

          
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
 
}