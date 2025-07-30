using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Finance_RptLedgerTrialBalance_aspx : System.Web.UI.Page
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
                    txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                    txtToDate.Attributes.Add("readonly", "readonly");



                    ddlOffice.Enabled = false;
                    btnBackB.Enabled = false;
                    // btnBackW.Enabled = false;

                    FillVoucherDate();
                    FillFromDate();

                    FillDropdown();
                    HeadList();


                    spnAltB.Visible = false;
                    spnAltW.Visible = false;
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
    protected string GetCurrentFinancialYear()
    {
        string VYear = "";
        string VMonth = "";
        ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "6", ViewState["Office_ID"].ToString() }, "dataset");
        if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
        {
            VYear = ds.Tables[0].Rows[0]["Year"].ToString();
            VMonth = ds.Tables[0].Rows[0]["Month"].ToString();
        }
        int CurrentYear = int.Parse(VYear);
        int PreviousYear = CurrentYear - 1;
        int NextYear = CurrentYear + 1;
        string PreYear = PreviousYear.ToString();
        string NexYear = NextYear.ToString();
        string CurYear = CurrentYear.ToString();
        string FinYear = null;

        if (int.Parse(VMonth) > 3)
        {
            FinYear = CurYear + "-" + NexYear;
            ViewState["CurYear"] = CurYear.ToString();
            ViewState["NexYear"] = NexYear.ToString();
        }
        else
        {
            FinYear = PreYear + "-" + CurYear;
            ViewState["CurYear"] = PreYear.ToString();
            ViewState["NexYear"] = CurYear.ToString();
        }

        return FinYear.Trim();
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string Session = ddlSession.SelectedValue.ToString();
            if (Session == "Quarter 1")
            {
                txtFromDate.Text = "01/04/" + ViewState["CurYear"].ToString();
                txtToDate.Text = "30/06/" + ViewState["CurYear"].ToString();
            }
            else if (Session == "Quarter 2")
            {
                txtFromDate.Text = "01/07/" + ViewState["CurYear"].ToString();
                txtToDate.Text = "30/09/" + ViewState["CurYear"].ToString();
            }
            else if (Session == "Quarter 3")
            {
                txtFromDate.Text = "01/10/" + ViewState["CurYear"].ToString();
                txtToDate.Text = "31/12/" + ViewState["CurYear"].ToString();
            }
            else if (Session == "Quarter 4")
            {
                txtFromDate.Text = "01/01/" + ViewState["NexYear"].ToString();
                txtToDate.Text = "31/03/" + ViewState["NexYear"].ToString();
            }
            else if (Session == "Quarter 1 & 2")
            {
                txtFromDate.Text = "01/04/" + ViewState["CurYear"].ToString();
                txtToDate.Text = "30/09/" + ViewState["CurYear"].ToString();
            }
            else if (Session == "Quarter 2 & 3")
            {
                txtFromDate.Text = "01/07/" + ViewState["CurYear"].ToString();
                txtToDate.Text = "31/12/" + ViewState["CurYear"].ToString();
            }
            else if (Session == "Quarter 3 & 4")
            {
                txtFromDate.Text = "01/10/" + ViewState["CurYear"].ToString();
                txtToDate.Text = "31/03/" + ViewState["NexYear"].ToString();
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
            if (ViewState["OfficeType_Title"].ToString() != "District Office" && ViewState["OfficeType_Title"].ToString() != "Production Unit")
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
                    if (ViewState["OfficeType_Title"].ToString() == "Regional Office")
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
            //ds = objdb.ByProcedure("SpFinRptTrialBalanceNew",
            //       new string[] { "flag" },
            //       new string[] { "0" }, "dataset");
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlOffice.DataSource = ds;
            //    ddlOffice.DataTextField = "Office_Name";
            //    ddlOffice.DataValueField = "Office_ID";
            //    ddlOffice.DataBind();
            //    // ddlOffice.Items.Insert(0, new ListItem("All", "0"));
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
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblTab.Text = "";


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ShowColumn()
    {
        try
        {

            // Ledger Name       
            GridView2.Columns[2].Visible = true;
            GridView2.Columns[3].Visible = true;
            GridView2.Columns[4].Visible = true;
            GridView2.Columns[5].Visible = true;
            GridView2.Columns[6].Visible = true;
            GridView2.Columns[7].Visible = true;


            GridView3.Columns[4].Visible = true;


        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void ShowHideColumn()
    {
        try
        {
            // Head Name
            if (chkOpeningBal.Checked == false)
            {
                GridView2.Columns[2].Visible = false;
                GridView2.Columns[3].Visible = false;
            }
            if (chkTransactionAmt.Checked == false)
            {
                GridView2.Columns[4].Visible = false;
                GridView2.Columns[5].Visible = false;
            }
            if (chkClosingBal.Checked == false)
            {
                GridView2.Columns[6].Visible = false;
                GridView2.Columns[7].Visible = false;

                GridView3.Columns[4].Visible = false;
            }
            if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
            {
                GridView2.Columns[3].Visible = false;
                GridView2.Columns[7].Visible = false;
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void FillGridNext()
    {
        try
        {


            lblMsg.Text = "";

            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
            lblTab.Text = "";
            DivTable.InnerHtml = "";
            btnBackB.Enabled = false;
            spnAltB.Visible = false;
            spnAltW.Visible = false;
            // btnBackW.Enabled = false;
            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            ShowColumn();
            //int rowcount = 0;
            ViewState["Ledger_ID_Mlt"] = "";
           // ds = objdb.ByProcedure("SpFinRptLedgerTrialBalanceNew", new string[] { "flag", "Office_ID_Mlt", "FromDate", "ToDate" }, new string[] { "1", Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset"); 
            ds = objdb.ByProcedure("SpFinRptTrialBalanceNewFF", new string[] { "flag", "Office_ID_Mlt", "FromDate", "ToDate", "Ledger_ID_Mlt" }, new string[] { "18", Office,  Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Ledger_ID_Mlt"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                ds.Tables[0].Rows.Add("0", "Difference in Opening Balances", "0", "0", "0", "0");
                GridView2.DataSource = ds.Tables[0];
                GridView2.DataBind();
                // GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                // GridView2.UseAccessibleHeader = true;
                if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                {
                    GridView2.HeaderRow.Cells[2].Text = "Opening";
                    GridView2.HeaderRow.Cells[6].Text = "Closing";
                }

                decimal TotalOpeningBal = 0;
                decimal TotalClosingBal = 0;
                decimal TotalDebitAmt = 0;
                decimal TotalCreditAmt = 0;
                //GridView1.FooterRow.Visible = true;

                decimal OpenTotalDr = 0;
                decimal OpenTotalCr = 0;
                decimal CloseTotalDr = 0;
                decimal CloseTotalCr = 0;

                int rowcount = ds.Tables[0].Rows.Count - 1;


                for (int i = 0; i < rowcount; i++)
                {
                    decimal OpeningBal = 0;
                    decimal PreOpeningBal = 0;


                    if (ds.Tables[0].Rows[i]["PreOpeningBalance"].ToString() != "")
                        PreOpeningBal = decimal.Parse(ds.Tables[0].Rows[i]["PreOpeningBalance"].ToString());

                    if (ds.Tables[0].Rows[i]["OpeningBalance"].ToString() != "")
                        OpeningBal = decimal.Parse(ds.Tables[0].Rows[i]["OpeningBalance"].ToString());

                    if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                        TotalDebitAmt = TotalDebitAmt + decimal.Parse(ds.Tables[0].Rows[i]["DebitAmt"].ToString());

                    if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                        TotalCreditAmt = TotalCreditAmt + decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());

                    TotalOpeningBal = TotalOpeningBal + PreOpeningBal;
                    TotalClosingBal = TotalClosingBal + OpeningBal;

                    if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                    {
                        if (PreOpeningBal >= 0)
                        {
                            GridView2.Rows[i].Cells[2].Text = PreOpeningBal.ToString() + " Cr";
                        }
                        else
                        {
                            GridView2.Rows[i].Cells[2].Text = Math.Abs(PreOpeningBal).ToString() + " Dr";
                        }

                        if (OpeningBal >= 0)
                        {
                            GridView2.Rows[i].Cells[6].Text = OpeningBal.ToString() + " Cr";

                        }
                        else
                        {
                            GridView2.Rows[i].Cells[6].Text = Math.Abs(OpeningBal).ToString() + " Dr";
                        }
                    }
                    else
                    {
                        if (PreOpeningBal >= 0)
                        {
                            GridView2.Rows[i].Cells[3].Text = PreOpeningBal.ToString();// +" Cr";
                            OpenTotalCr = OpenTotalCr + PreOpeningBal;
                        }
                        else
                        {
                            GridView2.Rows[i].Cells[2].Text = Math.Abs(PreOpeningBal).ToString();// + " Dr";
                            OpenTotalDr = OpenTotalDr + Math.Abs(PreOpeningBal);
                        }

                        if (OpeningBal >= 0)
                        {
                            GridView2.Rows[i].Cells[7].Text = OpeningBal.ToString();// + " Cr";
                            CloseTotalCr = CloseTotalCr + OpeningBal;

                        }
                        else
                        {
                            GridView2.Rows[i].Cells[6].Text = Math.Abs(OpeningBal).ToString();// + " Dr";
                            CloseTotalDr = CloseTotalDr + Math.Abs(OpeningBal);
                        }
                    }

                }

                GridView2.FooterRow.Cells[1].Text = "<b>Grand Total</b>";

                int status = 0;
                if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                {

                    if (TotalOpeningBal > 0)
                    {
                        status = 1;
                        GridView2.Rows[rowcount].Cells[2].Text = TotalOpeningBal.ToString() + " Dr";
                    }
                    else if (TotalOpeningBal < 0)
                    {
                        status = 1;
                        GridView2.Rows[rowcount].Cells[2].Text = Math.Abs(TotalOpeningBal).ToString() + " Cr";
                    }


                    if (TotalClosingBal > 0)
                    {
                        status = 1;
                        GridView2.Rows[rowcount].Cells[6].Text = TotalClosingBal.ToString() + " Dr";
                    }
                    else if (TotalClosingBal < 0)
                    {
                        status = 1;
                        GridView2.Rows[rowcount].Cells[6].Text = Math.Abs(TotalClosingBal).ToString() + " Cr";
                    }





                    //if (TotalOpeningBal >= 0)
                    //    GridView2.FooterRow.Cells[2].Text = TotalOpeningBal.ToString() + " Cr";
                    //else
                    //    GridView2.FooterRow.Cells[2].Text = Math.Abs(TotalOpeningBal).ToString() + " Dr";

                    GridView2.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
                    GridView2.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();

                    //if (TotalClosingBal >= 0)
                    //    GridView2.FooterRow.Cells[6].Text = TotalClosingBal.ToString() + " Cr";
                    //else
                    //    GridView2.FooterRow.Cells[6].Text = Math.Abs(TotalClosingBal).ToString() + " Dr";

                }
                else
                {
                    if (OpenTotalDr != OpenTotalCr || CloseTotalDr != CloseTotalCr || TotalDebitAmt != TotalCreditAmt)
                    {


                        if (chkOpeningBal.Checked == true)
                        {
                            if (OpenTotalDr < OpenTotalCr)
                            {
                                status = 1;
                                GridView2.Rows[rowcount].Cells[2].Text = (OpenTotalCr - OpenTotalDr).ToString();// +"Dr";
                                OpenTotalDr = OpenTotalCr;
                            }
                            else if (OpenTotalDr > OpenTotalCr)
                            {
                                status = 1;
                                GridView2.Rows[rowcount].Cells[3].Text = (OpenTotalDr - OpenTotalCr).ToString();// + "Cr";
                                OpenTotalCr = OpenTotalDr;
                            }
                        }

                        if (chkTransactionAmt.Checked == true)
                        {

                            if (TotalDebitAmt < TotalCreditAmt)
                            {
                                status = 1;
                                GridView2.Rows[rowcount].Cells[4].Text = (TotalCreditAmt - TotalDebitAmt).ToString();// +"Dr";
                                TotalDebitAmt = TotalCreditAmt;
                            }
                            else if (TotalDebitAmt > TotalCreditAmt)
                            {
                                status = 1;
                                GridView2.Rows[rowcount].Cells[5].Text = (TotalDebitAmt - TotalCreditAmt).ToString();// + "Cr";
                                TotalCreditAmt = TotalDebitAmt;
                            }
                        }
                        if (chkClosingBal.Checked == true)
                        {
                            if (CloseTotalDr < CloseTotalCr)
                            {
                                status = 1;
                                GridView2.Rows[rowcount].Cells[6].Text = (CloseTotalCr - CloseTotalDr).ToString();// + "Dr";
                                CloseTotalDr = CloseTotalCr;
                            }
                            else if (CloseTotalDr > CloseTotalCr)
                            {
                                status = 1;
                                GridView2.Rows[rowcount].Cells[7].Text = (CloseTotalDr - CloseTotalCr).ToString();// + "Cr";
                                CloseTotalCr = CloseTotalDr;
                            }
                        }


                    }

                    //if (TotalOpeningBal >= 0)
                    //    GridView2.FooterRow.Cells[3].Text = TotalOpeningBal.ToString();// + " Cr";
                    //else
                    //    GridView2.FooterRow.Cells[2].Text = Math.Abs(TotalOpeningBal).ToString();// + " Dr";

                    GridView2.FooterRow.Cells[2].Text = Math.Abs(OpenTotalDr).ToString();//Dr
                    GridView2.FooterRow.Cells[3].Text = OpenTotalCr.ToString(); //Cr


                    GridView2.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
                    GridView2.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();

                    //if (TotalClosingBal >= 0)
                    //    GridView2.FooterRow.Cells[7].Text = TotalClosingBal.ToString();// + " Cr";
                    //else
                    //    GridView2.FooterRow.Cells[6].Text = Math.Abs(TotalClosingBal).ToString();// + " Dr";
                    GridView2.FooterRow.Cells[6].Text = Math.Abs(CloseTotalDr).ToString(); //Dr
                    GridView2.FooterRow.Cells[7].Text = CloseTotalCr.ToString(); //Cr
                }
                if (status == 1)
                    GridView2.Rows[rowcount].Visible = true;

                GridView2.FooterRow.Cells[2].CssClass = "align-right";
                GridView2.FooterRow.Cells[3].CssClass = "align-right";
                GridView2.FooterRow.Cells[4].CssClass = "align-right";
                GridView2.FooterRow.Cells[5].CssClass = "align-right";
                GridView2.FooterRow.Cells[6].CssClass = "align-right";
                GridView2.FooterRow.Cells[7].CssClass = "align-right";
                GridView2.FooterRow.Style.Add("font-weight", "700");

                GridView2.FooterRow.TableSection = TableRowSection.TableFooter;

                //decimal OpenTotalDr = 0;
                //decimal OpenTotalCr = 0;
                //decimal CloseTotalDr = 0;
                //decimal CloseTotalCr = 0;

                //rowcount = ds.Tables[0].Rows.Count - 1;
                //for (int i = 0; i < rowcount; i++)
                //{
                //    decimal OpeningBalance = 0;
                //    decimal ClosingAmt = 0;


                //    if (ds.Tables[0].Rows[i]["OpeningBalance"].ToString() != "")
                //        OpeningBalance = decimal.Parse(ds.Tables[0].Rows[i]["OpeningBalance"].ToString());

                //    if (ds.Tables[0].Rows[i]["ClosingAmt"].ToString() != "")
                //        ClosingAmt = decimal.Parse(ds.Tables[0].Rows[i]["ClosingAmt"].ToString());

                //    string rowvisible = "";

                //    decimal Debit = 0;
                //    decimal Credit = 0;

                //    if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                //        Debit = decimal.Parse(ds.Tables[0].Rows[i]["DebitAmt"].ToString());

                //    if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                //        Credit = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());



                //    if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                //    {
                //        if (OpeningBalance >= 0)
                //        {
                //            GridView2.Rows[i].Cells[2].Text = OpeningBalance.ToString() + " Cr";
                //            OpenTotalCr = OpenTotalCr + OpeningBalance;
                //        }
                //        else
                //        {
                //            GridView2.Rows[i].Cells[2].Text = Math.Abs(OpeningBalance).ToString() + " Dr";
                //            OpenTotalDr = OpenTotalDr + OpeningBalance;
                //        }

                //        if (ClosingAmt >= 0)
                //        {
                //            GridView2.Rows[i].Cells[6].Text = ClosingAmt.ToString() + " Cr";
                //            CloseTotalCr = CloseTotalCr + ClosingAmt;

                //        }
                //        else
                //        {
                //            GridView2.Rows[i].Cells[6].Text = Math.Abs(ClosingAmt).ToString() + " Dr";
                //            CloseTotalDr = CloseTotalDr + ClosingAmt;
                //        }

                //        if (Debit == 0 && Credit == 0 && OpeningBalance == 0 && ClosingAmt == 0)
                //        {
                //            GridView2.Rows[i].Visible = false;
                //        }
                //    }
                //    else
                //    {
                //        if (OpeningBalance >= 0)
                //        {
                //            GridView2.Rows[i].Cells[3].Text = OpeningBalance.ToString();// +" Cr";
                //            OpenTotalCr = OpenTotalCr + OpeningBalance;
                //        }
                //        else
                //        {
                //            GridView2.Rows[i].Cells[2].Text = Math.Abs(OpeningBalance).ToString();// + " Dr";
                //            OpenTotalDr = OpenTotalDr + Math.Abs(OpeningBalance);
                //        }

                //        if (ClosingAmt >= 0)
                //        {
                //            GridView2.Rows[i].Cells[7].Text = ClosingAmt.ToString();// + " Cr";
                //            CloseTotalCr = CloseTotalCr + ClosingAmt;
                //        }
                //        else
                //        {
                //            GridView2.Rows[i].Cells[6].Text = Math.Abs(ClosingAmt).ToString();// + " Dr";
                //            CloseTotalDr = CloseTotalDr + Math.Abs(ClosingAmt);
                //        }

                //        if (chkOpeningBal.Checked == true && OpeningBalance != 0)
                //        {
                //            rowvisible = "1";
                //        }

                //        if (chkTransactionAmt.Checked == true && Debit != 0)
                //        {
                //            rowvisible = "1";
                //        }
                //        else if (chkTransactionAmt.Checked == true && Credit != 0)
                //        {
                //            rowvisible = "1";
                //        }

                //        if (chkClosingBal.Checked == true && ClosingAmt != 0)
                //        {
                //            rowvisible = "1";
                //        }
                //        if (rowvisible == "")
                //        {
                //            GridView2.Rows[i].Visible = false;
                //        }
                //    }

                //}

                //GridView2.Rows[rowcount].Visible = false;


                //decimal TotalOpeningBal = decimal.Parse(ds.Tables[1].Rows[0]["OpeningBalance"].ToString());
                //decimal TotalDebitAmt = decimal.Parse(ds.Tables[1].Rows[0]["DebitAmt"].ToString());
                //decimal TotalCreditAmt = decimal.Parse(ds.Tables[1].Rows[0]["CreditAmt"].ToString());
                //decimal TotalClosingBal = decimal.Parse(ds.Tables[1].Rows[0]["ClosingAmt"].ToString());


                //if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                //{

                //    if (Math.Abs(OpenTotalDr) != OpenTotalCr || TotalDebitAmt != TotalCreditAmt || Math.Abs(CloseTotalDr) != CloseTotalCr)
                //    {
                //        int status = 0;

                //        if (chkOpeningBal.Checked == true)
                //        {
                //            if (Math.Abs(OpenTotalDr) < OpenTotalCr)
                //            {
                //                status = 1;
                //                GridView2.Rows[rowcount].Cells[2].Text = "<b>" + (OpenTotalCr - Math.Abs(OpenTotalDr)).ToString() + " Dr </b>";
                //            }
                //            else if (Math.Abs(OpenTotalDr) > OpenTotalCr)
                //            {
                //                status = 1;
                //                GridView2.Rows[rowcount].Cells[2].Text = "<b>" + (Math.Abs(OpenTotalDr) - OpenTotalCr).ToString() + " Cr </b>";
                //            }
                //        }

                //        if (chkClosingBal.Checked == true)
                //        {
                //            if (Math.Abs(CloseTotalDr) < CloseTotalCr)
                //            {
                //                status = 1;
                //                GridView2.Rows[rowcount].Cells[6].Text = "<b>" + (CloseTotalCr - Math.Abs(CloseTotalDr)).ToString() + " Dr </b>";

                //            }
                //            else if (Math.Abs(CloseTotalDr) > CloseTotalCr)
                //            {
                //                status = 1;
                //                GridView2.Rows[rowcount].Cells[6].Text = "<b>" + (Math.Abs(CloseTotalDr) - CloseTotalCr).ToString() + " Cr </b>";

                //            }
                //        }
                //        if (status == 1)
                //            GridView2.Rows[rowcount].Visible = true;

                //    }


                //    /************************OLD 09 Dec 2019***************************/

                //    GridView2.FooterRow.Cells[1].Text = "<b>Grand Total</b>";

                //    //if (TotalOpeningBal >= 0)
                //    //    GridView2.FooterRow.Cells[2].Text = TotalOpeningBal.ToString() + " Cr";
                //    //else
                //    //    GridView2.FooterRow.Cells[2].Text = Math.Abs(TotalOpeningBal).ToString() + " Dr";

                //    GridView2.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
                //    GridView2.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();

                //    //if (TotalClosingBal >= 0)
                //    //    GridView2.FooterRow.Cells[6].Text = TotalClosingBal.ToString() + " Cr";
                //    //else
                //    //    GridView2.FooterRow.Cells[6].Text = Math.Abs(TotalClosingBal).ToString() + " Dr";

                //}
                //else
                //{

                //    if (Math.Abs(OpenTotalDr) != OpenTotalCr || TotalDebitAmt != TotalCreditAmt || Math.Abs(CloseTotalDr) != CloseTotalCr)
                //    {
                //        int status = 0;

                //        if (chkOpeningBal.Checked == true)
                //        {
                //            if (Math.Abs(OpenTotalDr) < OpenTotalCr)
                //            {
                //                status = 1;
                //                GridView2.Rows[rowcount].Cells[2].Text = (OpenTotalCr - Math.Abs(OpenTotalDr)).ToString();// +"Dr";
                //                OpenTotalDr = OpenTotalCr;
                //            }
                //            else if (Math.Abs(OpenTotalDr) > OpenTotalCr)
                //            {
                //                status = 1;
                //                GridView2.Rows[rowcount].Cells[3].Text = (Math.Abs(OpenTotalDr) - OpenTotalCr).ToString();// + "Cr";
                //                OpenTotalCr = Math.Abs(OpenTotalDr);
                //            }
                //        }

                //        if (chkTransactionAmt.Checked == true)
                //        {

                //            if (TotalDebitAmt < TotalCreditAmt)
                //            {
                //                status = 1;
                //                GridView2.Rows[rowcount].Cells[4].Text = (TotalCreditAmt - TotalDebitAmt).ToString();// +"Dr";
                //                TotalDebitAmt = TotalCreditAmt;
                //            }
                //            else if (TotalDebitAmt > TotalCreditAmt)
                //            {
                //                status = 1;
                //                GridView2.Rows[rowcount].Cells[5].Text = (TotalDebitAmt - TotalCreditAmt).ToString();// + "Cr";
                //                TotalCreditAmt = TotalDebitAmt;
                //            }
                //        }
                //        GridView2.FooterRow.Cells[6].Text = Math.Abs(CloseTotalDr).ToString(); //Dr
                //        GridView2.FooterRow.Cells[7].Text = CloseTotalCr.ToString(); //Cr
                //        if (chkClosingBal.Checked == true)
                //        {
                //            if (Math.Abs(CloseTotalDr) < CloseTotalCr)
                //            {
                //                status = 1;
                //                GridView2.Rows[rowcount].Cells[6].Text = (CloseTotalCr - Math.Abs(CloseTotalDr)).ToString();// + "Dr";
                //                CloseTotalDr = CloseTotalCr;
                //            }
                //            else if (Math.Abs(CloseTotalDr) > CloseTotalCr)
                //            {
                //                status = 1;
                //                GridView2.Rows[rowcount].Cells[7].Text = (Math.Abs(CloseTotalDr) - CloseTotalCr).ToString();// + "Cr";
                //                CloseTotalCr = Math.Abs(CloseTotalDr);
                //            }
                //        }
                //        if (status == 1)
                //            GridView2.Rows[rowcount].Visible = true;

                //    }


                //    GridView2.FooterRow.Cells[1].Text = "<b>Grand Total</b>";

                //    GridView2.FooterRow.Cells[2].Text = Math.Abs(OpenTotalDr).ToString();//Dr
                //    GridView2.FooterRow.Cells[3].Text = OpenTotalCr.ToString(); //Cr

                //    GridView2.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
                //    GridView2.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();

                //    GridView2.FooterRow.Cells[6].Text = Math.Abs(CloseTotalDr).ToString(); //Dr
                //    GridView2.FooterRow.Cells[7].Text = CloseTotalCr.ToString(); //Cr
                //}
                //GridView2.FooterRow.Cells[2].CssClass = "align-right";
                //GridView2.FooterRow.Cells[3].CssClass = "align-right";
                //GridView2.FooterRow.Cells[4].CssClass = "align-right";
                //GridView2.FooterRow.Cells[5].CssClass = "align-right";
                //GridView2.FooterRow.Cells[6].CssClass = "align-right";
                //GridView2.FooterRow.Cells[7].CssClass = "align-right";
                //GridView2.FooterRow.Style.Add("font-weight", "700");

                //GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                //GridView2.UseAccessibleHeader = true;

                //GridView2.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                lblTab.Text = "No record found.";
            }
            ShowHideColumn();
            // GridView1.FooterRow.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillGridNextLedger(string Ledger_ID)
    {
        try
        {
            lblMsg.Text = "";
            //  lblReportName.Text = "";
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
            lblTab.Text = "";
            DivTable.InnerHtml = "";
            btnBackB.Enabled = true;
            spnAltB.Visible = false;
            spnAltW.Visible = false;
            //btnBackW.Enabled = false;
            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }

            lblReportName.Text = "";
            DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerTrialBalanceNew", new string[] { "flag", "Ledger_ID" }, new string[] { "7", Ledger_ID }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                lblReportName.Text = ds1.Tables[0].Rows[0]["Ledger_Name"].ToString() + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
            }

            ds = objdb.ByProcedure("SpFinRptLedgerTrialBalanceNew", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate" }, new string[] { "4", Office, Ledger_ID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                spnAltB.Visible = true;

                GridView3.DataSource = ds;
                GridView3.DataBind();

                decimal PreOpening = 0;
                decimal OpeningBal = 0;

                if (ds.Tables[0].Rows[0]["PreOpening"].ToString() != "")
                    PreOpening = decimal.Parse(ds.Tables[0].Rows[0]["PreOpening"].ToString());


                OpeningBal = PreOpening;
                //if (chkOpeningBal.Checked == true)
                //{
                //    if (OpeningBal < 0)
                //    {
                //        lblReportName.Text = lblReportName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBal).ToString() + " Dr </span>";
                //    }
                //    else
                //    {
                //        lblReportName.Text = lblReportName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBal).ToString() + " Cr </span>";
                //    }
                //}
                if (chkOpeningBal.Checked == true)
                {
                    if ((PreOpening + decimal.Parse(ds.Tables[0].Rows[0]["CurrentOpening"].ToString())) < 0)
                    {
                        lblReportName.Text = lblReportName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(PreOpening + decimal.Parse(ds.Tables[0].Rows[0]["CurrentOpening"].ToString())).ToString() + " Dr </span>";
                    }
                    else
                    {
                        lblReportName.Text = lblReportName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(PreOpening + decimal.Parse(ds.Tables[0].Rows[0]["CurrentOpening"].ToString())).ToString() + " Cr </span>";
                    }
                }




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
            else
            {
                lblTab.Text = "No record found.";
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
            lblMsg.Text = "";
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
            lblTab.Text = "";
            DivTable.InnerHtml = "";
            btnBackB.Enabled = true;


            lblReportName.Text = "";
            DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerTrialBalanceNew", new string[] { "flag", "Ledger_ID" }, new string[] { "7", Ledger_ID }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                lblReportName.Text = ds1.Tables[0].Rows[0]["Ledger_Name"].ToString() + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
            }

            string Office = "";

            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }


            string sDate = (Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd")).ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            int Month = int.Parse(datevalue.Month.ToString());
            int Year = int.Parse(datevalue.Year.ToString());
            int FY = Year;
            string FinancialYear = Year.ToString();
            string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
            FinancialYear = "";
            int FY_Start = FY;
            if (Month <= 3)
            {
                FY = Year - 1;
                FinancialYear = FY.ToString() + "-" + LFY.ToString();
                FY_Start = FY;
            }
            else
            {

                FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
            }
            ViewState["Ledger_ID_Mlt"] = "";
            GridView3.DataSource = new string[] { };
           // ds = objdb.ByProcedure("SpFinRptLedgerTrialBalanceNew", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "LedgerTx_Month", "FromDate", "ToDate" }, new string[] { "5", Office, Ledger_ID, MonthID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            ds = objdb.ByProcedure("SpFinRptTrialBalanceNewF", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "LedgerTx_Month", "FromDate", "ToDate", "Ledger_ID_Mlt", "FinancialYear" }, new string[] { "6", Office, Ledger_ID, MonthID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Ledger_ID_Mlt"].ToString(), FinancialYear }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {

                spnAltB.Visible = true;
                spnAltW.Visible = true;

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

                GridView2.DataSource = new string[] { };
                GridView2.DataBind();
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

                string ValidDays = "No";

                int Count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                    ValidDays = "No";
                    ValidDays = ds.Tables[0].Rows[i]["V_Editright"].ToString();

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");

                    // Inner Transaction
                    ds1 = null;
                    ds1 = objdb.ByProcedure("SpFinRptLedgerTrialBalanceNew", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", }, new string[] { "6", ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString(), Ledger_ID }, "dataset");
                    int Count1 = ds1.Tables[0].Rows.Count;

                    string Narration = ds1.Tables[1].Rows[0]["VoucherTx_Narration"].ToString();

                    if (Count1 > 1)
                    {

                        htmlStr.Append("<td><p class='HideRecord' style='display:none;'>(As per Details) <br/></p>");
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
                                    htmlStr.Append("\n<p class='subledger HideRecord' style='display:none;'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                                }

                                htmlStr.Append("\t \t \t<span class='Ledger_Amt HideRecord' style='display:none;'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span><br/></p>");
                                //htmlStr.Append("<br/>");

                            }
                        }

                        htmlStr.Append("\n<p class='subledger HideRecord' style='display:none;'><span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span></p>");
                        htmlStr.Append("</td>");
                    }
                    else
                    {
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                        htmlStr.Append("\n<p class='subledger HideRecord' style='display:none;'><span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span></p>");
                        htmlStr.Append("</td>");
                    }


                    //  htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");


                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                    htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                    htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                    htmlStr.Append("<td  class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a> ");
                    if (ds.Tables[0].Rows[i]["Office_ID"].ToString() == ViewState["Office_ID"].ToString())
                    {
                        if (ValidDays != "No")
                        {
                            htmlStr.Append("<a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("2") + "' target='_blank'>Edit</a>");
                        }
                    }
                    htmlStr.Append("</td>");
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

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblReportName.Text = "";
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView2.Rows[index];
                Label lblLedger_ID = (Label)row.Cells[0].FindControl("lblLedger_ID");
                Label lblDebitAmt = (Label)row.Cells[2].FindControl("lblDebitAmt");
                Label lblCreditAmt = (Label)row.Cells[3].FindControl("lblCreditAmt");
                Label lblLedger_Name = (Label)row.Cells[3].FindControl("lblLedger_Name");

                if (lblLedger_ID.Text != "" && lblLedger_ID.Text != "0")
                {
                    lblReportName.Text = "Ledger Of : " + lblLedger_Name.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";

                    if (lblDebitAmt.Text != "" || lblCreditAmt.Text != "")
                    {
                        string Ledger_ID = lblLedger_ID.Text;

                        DataTable dt = (DataTable)ViewState["Heads"];
                        dt.Rows.Add(Ledger_ID, "0");

                        FillGridNextLedger(Ledger_ID);
                    }
                }
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
            lblReportName.Text = "";
            DataTable dt = ViewState["Heads"] as DataTable;
            int DtRow = dt.Rows.Count;

            if (dt.Rows.Count > 1)
            {
                string Ledger_ID = dt.Rows[DtRow - 2]["Ledger_ID"].ToString();
                string MonthID = dt.Rows[DtRow - 2]["MonthID"].ToString();

                string flag = "0";
                if (Ledger_ID != "0" && MonthID == "0")
                {
                    FillGridNextLedger(Ledger_ID);
                    flag = "1";
                }
                else if (Ledger_ID != "0" && MonthID != "0")
                {

                    FillGridNextLedgerMonth(Ledger_ID, MonthID);
                    flag = "1";
                }
                //else
                //{

                //    FillGridNext();
                //}

                if (flag == "1")
                {
                    dt.Rows.RemoveAt(DtRow - 1);
                    ViewState["Heads"] = dt;
                }
                //if (flag == "1")
                //{
                //    dt.Rows.RemoveAt(DtRow - 1);
                //    ViewState["Heads"] = dt;
                //}

            }
            else
            {
                FillGridNext();
            }
            // ViewState["Detail"] = "0";

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }

    protected void btnBackB_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
            lblTab.Text = "";
            DivTable.InnerHtml = "";
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                BackGrid();
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
            lblMsg.Text = "";
            lblReportName.Text = "";
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
            lblTab.Text = "";
            DivTable.InnerHtml = "";
            //if (txtFromDate.Text != "" && txtToDate.Text != "")
            //{
            //    FillGridNext();
            //}




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

            if (Office != "" && txtFromDate.Text != "" && txtToDate.Text != "")
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
                string headingFirst = "<p class='text-center' style='font-weight:600'>Alphabetical Trial Balance <br />SFA Technologies Pvt. Ltd. <br/> [ " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
                lblheadingFirst.Text = headingFirst;


                FillGridNext();
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
    protected void ddlRegionalOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillOffice();
    }

}
