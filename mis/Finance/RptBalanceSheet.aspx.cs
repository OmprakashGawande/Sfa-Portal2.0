using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.IO;

public partial class mis_Finance_RptBalanceSheet : System.Web.UI.Page
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
                    divExcel.Visible = false;
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    string FY = GetCurrentFinancialYear();
                    string[] YEAR = FY.Split('-');
                    DateTime FromDate = new DateTime(int.Parse(YEAR[0]), 4, 1);
                    txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                    txtToDate.Attributes.Add("readonly", "readonly");

                    HeadList();
                    FillVoucherDate();
                    FillDropdown();
                    GetCommonSearch();

                    //btnHeadExcel.Visible = false;
                    //btnMonthExcel.Visible = false;
                    //btnDayBookExcel.Visible = false;
                    spnAltB.Visible = false;
                    spnAltW.Visible = false;
                    PnlLevel1.Visible = false;
                    btnShowDetailBook.Enabled = false;

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
    protected void HeadList()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Head_ID", typeof(string)));
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
            //ds = objdb.ByProcedure("SpFinRptBalanceSheet",
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
            lblHeadName.Text = "";
            FillGrid();

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
            // Head Name
            GridView1.Columns[2].Visible = true;
            GridView1.Columns[3].Visible = true;
            GridView1.Columns[4].Visible = true;
            GridView1.Columns[5].Visible = true;
            GridView1.Columns[6].Visible = true;
            GridView1.Columns[7].Visible = true;

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
                GridView1.Columns[2].Visible = false;
                GridView1.Columns[3].Visible = false;

                GridView2.Columns[2].Visible = false;
                GridView2.Columns[3].Visible = false;
            }
            if (chkTransactionAmt.Checked == false)
            {

                GridView1.Columns[4].Visible = false;
                GridView1.Columns[5].Visible = false;

                GridView2.Columns[4].Visible = false;
                GridView2.Columns[5].Visible = false;
            }
            if (chkClosingBal.Checked == false)
            {
                GridView1.Columns[6].Visible = false;
                GridView1.Columns[7].Visible = false;

                GridView2.Columns[6].Visible = false;
                GridView2.Columns[7].Visible = false;

                GridView3.Columns[4].Visible = false;
            }
            if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
            {
                GridView1.Columns[3].Visible = false;
                GridView1.Columns[7].Visible = false;

                GridView2.Columns[3].Visible = false;
                GridView2.Columns[7].Visible = false;
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
            HeadList();
            //btnHeadExcel.Visible = false;
            //btnMonthExcel.Visible = false;
            //btnDayBookExcel.Visible = false;
            spnAltB.Visible = false;
            spnAltW.Visible = false;

            GridView1.DataSource = null;
            GridView1.DataBind();

            GridView2.DataSource = null;
            GridView2.DataBind();

            GridView3.DataSource = null;
            GridView3.DataBind();
            lblHeadName.Text = "";
            DivTable.InnerHtml = "";
            PnlLevel1.Visible = true;

            btnShowDetailBook.Enabled = false;
            GridView4.DataSource = null;
            GridView4.DataBind();


            GridBalanceSheetLeft.DataSource = null;
            GridBalanceSheetLeft.DataBind();

            GridBalanceSheetRight.DataSource = null;
            GridBalanceSheetRight.DataBind();

            lblTotalLeft.Text = "";
            lblTotalRight.Text = "";

            string Office = "";

            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            ds = objdb.ByProcedure("SpFinRptBalanceSheet", new string[] { "flag", "Office_ID_Mlt", "FromDate", "ToDate" }, new string[] { "1", Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                divExcel.Visible = true;
                GridBalanceSheetLeft.DataSource = ds.Tables[0];
                GridBalanceSheetLeft.DataBind();

                GridBalanceSheetRight.DataSource = ds.Tables[1];
                GridBalanceSheetRight.DataBind();

                lblTotalLeft.Text = ds.Tables[2].Rows[0]["LeftTotal"].ToString();
                lblTotalRight.Text = ds.Tables[2].Rows[0]["RightTotal"].ToString();
            }
            ds = objdb.ByProcedure("SpFinRptTrialBalanceNewF", new string[] { "flag" }, new string[] { "16" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                ViewState["Ledger_ID_Mlt"] = ds.Tables[0].Rows[0]["Ledger_ID_Mlt"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillGridNextMltHead()
    {
        try
        {
            // lblGridMsg.Text = "";
            //btnHeadExcel.Visible = false;
            //btnMonthExcel.Visible = false;
            //btnDayBookExcel.Visible = false;
            spnAltW.Visible = false;
            PnlLevel1.Visible = false;

            btnShowDetailBook.Enabled = false;
            GridView4.DataSource = null;
            GridView4.DataBind();

            ShowColumn();


            lblHeadName.Text = "Working Capital" + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";

            GridView3.DataSource = null;
            GridView3.DataBind();

            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            int Grid1 = 0; int Grid2 = 0;
            decimal TotalOpeningBal = 0;
            decimal TotalClosingBal = 0;
            decimal TotalDebitAmt = 0;
            decimal TotalCreditAmt = 0;
            decimal TotalClosingDebitAmt = 0;
            decimal TotalClosingCreditAmt = 0;
            //GridView1.FooterRow.Visible = true;

            ds = objdb.ByProcedure("SpFinRptBalanceSheet", new string[] { "flag", "Office_ID_Mlt", "FromDate", "ToDate" }, new string[] { "3", Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                //btnHeadExcel.Visible = true;
                spnAltB.Visible = true;

                Grid1 = 1;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                {
                    GridView1.HeaderRow.Cells[2].Text = "Opening";
                    GridView1.HeaderRow.Cells[6].Text = "Closing";
                }
                // Opening Balance
                int rowcount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < rowcount; i++)
                {
                    decimal OpeningBal = 0;
                    decimal PreOpeningBal = 0;

                    decimal ClosingDebitAmt = 0;
                    decimal ClosingCreditAmt = 0;

                    if (ds.Tables[0].Rows[i]["PreOpeningBalance"].ToString() != "")
                        PreOpeningBal = decimal.Parse(ds.Tables[0].Rows[i]["PreOpeningBalance"].ToString());

                    if (ds.Tables[0].Rows[i]["OpeningBalance"].ToString() != "")
                        OpeningBal = decimal.Parse(ds.Tables[0].Rows[i]["OpeningBalance"].ToString());

                    if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                        TotalDebitAmt = TotalDebitAmt + decimal.Parse(ds.Tables[0].Rows[i]["DebitAmt"].ToString());

                    if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                        TotalCreditAmt = TotalCreditAmt + decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());

                    if (ds.Tables[0].Rows[i]["ClosingDebitAmt"].ToString() != "")
                        ClosingDebitAmt = decimal.Parse(ds.Tables[0].Rows[i]["ClosingDebitAmt"].ToString());

                    if (ds.Tables[0].Rows[i]["ClosingCreditAmt"].ToString() != "")
                        ClosingCreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["ClosingCreditAmt"].ToString());

                    TotalOpeningBal = TotalOpeningBal + PreOpeningBal;
                    TotalClosingBal = TotalClosingBal + OpeningBal;


                    TotalClosingDebitAmt = TotalClosingDebitAmt + ClosingDebitAmt;
                    TotalClosingCreditAmt = TotalClosingCreditAmt + ClosingCreditAmt;

                    if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                    {
                        if (PreOpeningBal >= 0)
                        {
                            GridView1.Rows[i].Cells[2].Text = PreOpeningBal.ToString() + " Cr";
                        }
                        else
                        {
                            GridView1.Rows[i].Cells[2].Text = Math.Abs(PreOpeningBal).ToString() + " Dr";
                        }

                        if (OpeningBal >= 0)
                        {
                            GridView1.Rows[i].Cells[6].Text = OpeningBal.ToString() + " Cr";

                        }
                        else
                        {
                            GridView1.Rows[i].Cells[6].Text = Math.Abs(OpeningBal).ToString() + " Dr";
                        }

                    }
                    else
                    {


                        if (PreOpeningBal >= 0)
                        {
                            GridView1.Rows[i].Cells[3].Text = PreOpeningBal.ToString();// +" Cr";
                        }
                        else
                        {
                            GridView1.Rows[i].Cells[2].Text = Math.Abs(PreOpeningBal).ToString();// + " Dr";
                        }

                        //if (OpeningBal >= 0)
                        //{
                        //    GridView1.Rows[i].Cells[7].Text = OpeningBal.ToString();// + " Cr";

                        //}
                        //else
                        //{
                        //    GridView1.Rows[i].Cells[6].Text = Math.Abs(OpeningBal).ToString();// + " Dr";
                        //}

                        GridView1.Rows[i].Cells[7].Text = ClosingCreditAmt.ToString();// + " Cr";
                        GridView1.Rows[i].Cells[6].Text = Math.Abs(ClosingDebitAmt).ToString();// + " Dr";
                    }


                }
                GridView1.FooterRow.Visible = false;



            }


            ShowHideColumn();

            // GridView1.FooterRow.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillGridNext(string Head_ID)
    {
        try
        {
            // lblGridMsg.Text = "";
            //btnHeadExcel.Visible = false;
            //btnMonthExcel.Visible = false;
            //btnDayBookExcel.Visible = false;

            //ViewState["Ledger_ID_Mlt"] = "";
            spnAltW.Visible = false;
            PnlLevel1.Visible = false;

            btnShowDetailBook.Enabled = false;
            GridView4.DataSource = null;
            GridView4.DataBind();

            ShowColumn();
            DataSet ds1 = objdb.ByProcedure("SpFinRptBalanceSheet", new string[] { "flag", "Head_ID" }, new string[] { "9", Head_ID }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                lblHeadName.Text = ds1.Tables[0].Rows[0]["Head_Name"].ToString() + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
            }


            GridView3.DataSource = null;
            GridView3.DataBind();

            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            int Grid1 = 0; int Grid2 = 0;
            decimal TotalOpeningBal = 0;
            decimal TotalClosingBal = 0;
            decimal TotalDebitAmt = 0;
            decimal TotalCreditAmt = 0;
            decimal TotalClosingDebitAmt = 0;
            decimal TotalClosingCreditAmt = 0;
            //GridView1.FooterRow.Visible = true;


            decimal OpenTotalDr = 0;
            decimal OpenTotalCr = 0;
            decimal CloseTotalDr = 0;
            decimal CloseTotalCr = 0;
            //  ds = objdb.ByProcedure("SpFinRptBalanceSheet", new string[] { "flag", "Office_ID_Mlt", "Head_ID", "FromDate", "ToDate" }, new string[] { "2", Office, Head_ID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            ds = objdb.ByProcedure("SpFinRptTrialBalanceNewF",
               new string[] { "flag", "Office_ID_Mlt", "Head_ID", "FromDate", "ToDate", "Ledger_ID_Mlt" },
               new string[] { "2", Office, Head_ID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Ledger_ID_Mlt"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                //btnHeadExcel.Visible = true;
                spnAltB.Visible = true;

                Grid1 = 1;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                // ViewState["Ledger_ID_Mlt"] = ds.Tables[1].Rows[0]["Ledger_ID_Mlt"].ToString();

                if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                {
                    GridView1.HeaderRow.Cells[2].Text = "Opening";
                    GridView1.HeaderRow.Cells[6].Text = "Closing";
                }
                // Opening Balance
                int rowcount = ds.Tables[0].Rows.Count;
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
                            GridView1.Rows[i].Cells[2].Text = PreOpeningBal.ToString() + " Cr";
                        }
                        else
                        {
                            GridView1.Rows[i].Cells[2].Text = Math.Abs(PreOpeningBal).ToString() + " Dr";
                        }

                        if (OpeningBal >= 0)
                        {
                            GridView1.Rows[i].Cells[6].Text = OpeningBal.ToString() + " Cr";

                        }
                        else
                        {
                            GridView1.Rows[i].Cells[6].Text = Math.Abs(OpeningBal).ToString() + " Dr";
                        }

                    }
                    else
                    {
                        // COMMENTED ON 20July2020
                        //if (ds.Tables[0].Rows[i]["DebitOpeningBalance"].ToString() != "")
                        //{
                        //    GridView1.Rows[i].Cells[2].Text = ds.Tables[0].Rows[i]["DebitOpeningBalance"].ToString();
                        //    OpenTotalDr = OpenTotalDr + decimal.Parse(ds.Tables[0].Rows[i]["DebitOpeningBalance"].ToString());
                        //}

                        //if (ds.Tables[0].Rows[i]["CreditOpeningBalance"].ToString() != "")
                        //{
                        //    GridView1.Rows[i].Cells[3].Text = ds.Tables[0].Rows[i]["CreditOpeningBalance"].ToString();
                        //    OpenTotalCr = OpenTotalCr + decimal.Parse(ds.Tables[0].Rows[i]["CreditOpeningBalance"].ToString());
                        //}
                        // CHANGED ON 20July2020
                        if (ds.Tables[0].Rows[i]["OpeningBalanceDR"].ToString() != "")
                        {
                            GridView1.Rows[i].Cells[2].Text = ds.Tables[0].Rows[i]["OpeningBalanceDR"].ToString();
                            OpenTotalDr = OpenTotalDr + decimal.Parse(ds.Tables[0].Rows[i]["OpeningBalanceDR"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["OpeningBalanceCR"].ToString() != "")
                        {
                            GridView1.Rows[i].Cells[3].Text = ds.Tables[0].Rows[i]["OpeningBalanceCR"].ToString();
                            OpenTotalCr = OpenTotalCr + decimal.Parse(ds.Tables[0].Rows[i]["OpeningBalanceCR"].ToString());
                        }


                        if (ds.Tables[0].Rows[i]["DebitClosingBalance"].ToString() != "")
                        {
                            GridView1.Rows[i].Cells[6].Text = ds.Tables[0].Rows[i]["DebitClosingBalance"].ToString();
                            CloseTotalDr = CloseTotalDr + decimal.Parse(ds.Tables[0].Rows[i]["DebitClosingBalance"].ToString());
                        }
                        if (ds.Tables[0].Rows[i]["CreditClosingBalance"].ToString() != "")
                        {
                            GridView1.Rows[i].Cells[7].Text = ds.Tables[0].Rows[i]["CreditClosingBalance"].ToString();
                            CloseTotalCr = CloseTotalCr + decimal.Parse(ds.Tables[0].Rows[i]["CreditClosingBalance"].ToString());
                        }
                        //if (PreOpeningBal >= 0)
                        //{
                        //    GridView1.Rows[i].Cells[3].Text = PreOpeningBal.ToString();// +" Cr";
                        //    OpenTotalCr = OpenTotalCr + PreOpeningBal;
                        //}
                        //else
                        //{
                        //    GridView1.Rows[i].Cells[2].Text = Math.Abs(PreOpeningBal).ToString();// + " Dr";
                        //    OpenTotalDr = OpenTotalDr + Math.Abs(PreOpeningBal);
                        //}

                        //if (OpeningBal >= 0)
                        //{
                        //    GridView1.Rows[i].Cells[7].Text = OpeningBal.ToString();// + " Cr";
                        //    CloseTotalCr = CloseTotalCr + OpeningBal;

                        //}
                        //else
                        //{
                        //    GridView1.Rows[i].Cells[6].Text = Math.Abs(OpeningBal).ToString();// + " Dr";
                        //    CloseTotalDr = CloseTotalDr + Math.Abs(OpeningBal);
                        //}
                    }


                }

                GridView1.FooterRow.Cells[1].Text = "<b>Grand Total</b>";


                if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                {
                    if (TotalOpeningBal >= 0)
                        GridView1.FooterRow.Cells[2].Text = TotalOpeningBal.ToString() + " Cr";
                    else
                        GridView1.FooterRow.Cells[2].Text = Math.Abs(TotalOpeningBal).ToString() + " Dr";

                    GridView1.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
                    GridView1.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();

                    if (TotalClosingBal >= 0)
                        GridView1.FooterRow.Cells[6].Text = TotalClosingBal.ToString() + " Cr";
                    else
                        GridView1.FooterRow.Cells[6].Text = Math.Abs(TotalClosingBal).ToString() + " Dr";

                }
                else
                {
                    //if (TotalOpeningBal >= 0)
                    //    GridView1.FooterRow.Cells[3].Text = TotalOpeningBal.ToString();// + " Cr";
                    //else
                    //    GridView1.FooterRow.Cells[2].Text = Math.Abs(TotalOpeningBal).ToString();// + " Dr";

                    GridView1.FooterRow.Cells[2].Text = Math.Abs(OpenTotalDr).ToString();//Dr
                    GridView1.FooterRow.Cells[3].Text = OpenTotalCr.ToString(); //Cr


                    GridView1.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
                    GridView1.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();

                    //if (TotalClosingBal >= 0)
                    //    GridView1.FooterRow.Cells[7].Text = TotalClosingBal.ToString();// + " Cr";
                    //else
                    //    GridView1.FooterRow.Cells[6].Text = Math.Abs(TotalClosingBal).ToString();// + " Dr";

                    GridView1.FooterRow.Cells[6].Text = Math.Abs(CloseTotalDr).ToString(); //Dr
                    GridView1.FooterRow.Cells[7].Text = CloseTotalCr.ToString(); //Cr

                }


                GridView1.FooterRow.Cells[2].CssClass = "align-right";
                GridView1.FooterRow.Cells[3].CssClass = "align-right";
                GridView1.FooterRow.Cells[4].CssClass = "align-right";
                GridView1.FooterRow.Cells[5].CssClass = "align-right";
                GridView1.FooterRow.Cells[6].CssClass = "align-right";
                GridView1.FooterRow.Cells[7].CssClass = "align-right";
                GridView1.FooterRow.Style.Add("font-weight", "700");

                GridView1.FooterRow.TableSection = TableRowSection.TableFooter;

            }
            TotalClosingDebitAmt = 0;
            TotalClosingCreditAmt = 0;


            //  ds = objdb.ByProcedure("SpFinRptBalanceSheet", new string[] { "flag", "Office_ID_Mlt", "Head_ID", "FromDate", "ToDate", "Ledger_ID_Mlt" }, new string[] { "5", Office, Head_ID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"),ViewState["Ledger_ID_Mlt"].ToString() }, "dataset");
            ds = objdb.ByProcedure("SpFinRptTrialBalanceNewF", new string[] { "flag", "Office_ID_Mlt", "Head_ID", "FromDate", "ToDate", "Ledger_ID_Mlt" }, new string[] { "3", Office, Head_ID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Ledger_ID_Mlt"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                //btnHeadExcel.Visible = true;
                spnAltB.Visible = true;

                Grid2 = 1;
                GridView2.DataSource = ds;
                GridView2.DataBind();
                GridView2.UseAccessibleHeader = true;
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;

                if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                {
                    GridView2.HeaderRow.Cells[2].Text = "Opening";
                    GridView2.HeaderRow.Cells[6].Text = "Closing";
                }
                int rowcount = ds.Tables[0].Rows.Count;

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


                if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                {
                    if (TotalOpeningBal >= 0)
                        GridView2.FooterRow.Cells[2].Text = TotalOpeningBal.ToString() + " Cr";
                    else
                        GridView2.FooterRow.Cells[2].Text = Math.Abs(TotalOpeningBal).ToString() + " Dr";

                    GridView2.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
                    GridView2.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();

                    if (TotalClosingBal >= 0)
                        GridView2.FooterRow.Cells[6].Text = TotalClosingBal.ToString() + " Cr";
                    else
                        GridView2.FooterRow.Cells[6].Text = Math.Abs(TotalClosingBal).ToString() + " Dr";

                }
                else
                {
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
                GridView2.FooterRow.Cells[2].CssClass = "align-right";
                GridView2.FooterRow.Cells[3].CssClass = "align-right";
                GridView2.FooterRow.Cells[4].CssClass = "align-right";
                GridView2.FooterRow.Cells[5].CssClass = "align-right";
                GridView2.FooterRow.Cells[6].CssClass = "align-right";
                GridView2.FooterRow.Cells[7].CssClass = "align-right";
                GridView2.FooterRow.Style.Add("font-weight", "700");

                GridView2.FooterRow.TableSection = TableRowSection.TableFooter;



                //GridView2.FooterRow.Cells[1].Text = "<b>Grand Total</b>";

                //if (TotalOpeningBal >= 0)
                //    GridView2.FooterRow.Cells[2].Text = TotalOpeningBal.ToString() + " Cr";
                //else
                //    GridView2.FooterRow.Cells[2].Text = Math.Abs(TotalOpeningBal).ToString() + " Dr";


            }

            if (Grid1 != 0 && Grid2 != 0)
            {
                GridView1.FooterRow.Visible = false;
            }


            if (Grid1 == 0 && Grid2 == 0)
            {
                // lblGridMsg.Text = "No Record Found.";
            }
            else if (Grid1 == 0)
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();

            }
            else if (Grid2 == 0)
            {
                GridView2.DataSource = new string[] { };
                GridView2.DataBind();
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
            ShowColumn();
            //btnHeadExcel.Visible = false;
            //btnMonthExcel.Visible = false;
            //btnDayBookExcel.Visible = false;

            spnAltW.Visible = false;
            PnlLevel1.Visible = false;

            btnShowDetailBook.Enabled = false;
            GridView4.DataSource = null;
            GridView4.DataBind();

            DataSet ds1 = objdb.ByProcedure("SpFinRptBalanceSheet", new string[] { "flag", "Ledger_ID" }, new string[] { "10", Ledger_ID }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                lblHeadName.Text = ds1.Tables[0].Rows[0]["Ledger_Name"].ToString() + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
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
            // ds = objdb.ByProcedure("SpFinRptBalanceSheet", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate" }, new string[] { "4", Office, Ledger_ID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            ds = objdb.ByProcedure("SpFinRptTrialBalanceNewF", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate", "Ledger_ID_Mlt" }, new string[] { "4", Office, Ledger_ID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Ledger_ID_Mlt"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                //btnMonthExcel.Visible = true;
                spnAltB.Visible = true;

                GridView3.DataSource = ds;
                GridView3.DataBind();

                decimal PreOpening = 0;
                decimal OpeningBal = 0;

                if (ds.Tables[0].Rows[0]["PreOpening"].ToString() != "")
                    PreOpening = decimal.Parse(ds.Tables[0].Rows[0]["PreOpening"].ToString());


                OpeningBal = PreOpening;

                if (chkOpeningBal.Checked == true)
                {
                    if ((PreOpening + decimal.Parse(ds.Tables[0].Rows[0]["CurrentOpening"].ToString())) < 0)
                    {
                        lblHeadName.Text = lblHeadName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(PreOpening + decimal.Parse(ds.Tables[0].Rows[0]["CurrentOpening"].ToString())).ToString() + " Dr </span>";
                    }
                    else
                    {
                        lblHeadName.Text = lblHeadName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(PreOpening + decimal.Parse(ds.Tables[0].Rows[0]["CurrentOpening"].ToString())).ToString() + " Cr </span>";
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

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();


            ShowHideColumn();
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

            PnlLevel1.Visible = false;

            lblHeadName.Text = "";

            ViewState["Ledger_ID"] = Ledger_ID;
            ViewState["MonthID"] = MonthID;
            ViewState["DayBookVisible"] = "true";
            btnShowDetailBook.Enabled = false;
            GridView4.DataSource = null;
            GridView4.DataBind();

            DataSet ds1 = objdb.ByProcedure("SpFinRptBalanceSheet", new string[] { "flag", "Ledger_ID" }, new string[] { "10", Ledger_ID }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                // lblHeadName.Text = ds1.Tables[0].Rows[0]["Ledger_Name"].ToString() + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";

                DateTimeFormatInfo mfi = new DateTimeFormatInfo();
                lblHeadName.Text = ds1.Tables[0].Rows[0]["Ledger_Name"].ToString() + "( Month : " + mfi.GetMonthName(int.Parse(MonthID)).ToString() + " )";
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

            GridView3.DataSource = new string[] { };
            //ds = objdb.ByProcedure("SpFinRptBalanceSheet", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "LedgerTx_Month", "FromDate", "ToDate" }, new string[] { "6", Office, Ledger_ID, MonthID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            ds = objdb.ByProcedure("SpFinRptTrialBalanceNewF", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "LedgerTx_Month", "FromDate", "ToDate", "Ledger_ID_Mlt", "FinancialYear" }, new string[] { "6", Office, Ledger_ID, MonthID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Ledger_ID_Mlt"].ToString(), FinancialYear }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                // btnDayBookExcel.Visible = true;

                spnAltB.Visible = true;
                spnAltW.Visible = true;

                DivTable.Visible = true;
                btnShowDetailBook.Enabled = true;

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

                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
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
                    ds1 = objdb.ByProcedure("SpFinRptBalanceSheet", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", }, new string[] { "12", ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString(), Ledger_ID }, "dataset");
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
                    htmlStr.Append("<td  class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a>");
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblExecTime.Text = "";
            var watch = System.Diagnostics.Stopwatch.StartNew();

            lblMsg.Text = "";
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                Label lblHead_ID = (Label)row.Cells[0].FindControl("lblHead_ID");

                string Head_ID = lblHead_ID.Text;
                DataTable dt = (DataTable)ViewState["Heads"];
                dt.Rows.Add(Head_ID, "0", "0");

                 //start done changes by Mohini on 21Mar 2022
               // FillGridNext(Head_ID);
                if (Head_ID != "0" && Head_ID != "-1")
                {
                    dt.Rows.Add(Head_ID, "0", "0");

                    FillGridNext(Head_ID);
                }
                else if (Head_ID == "0")
                {
                    string Office = "";

                    foreach (ListItem item in ddlOffice.Items)
                    {
                        if (item.Selected)
                        {
                            Office += item.Value + ",";
                        }
                    }
                    //  Response.Redirect("RptStockSummaryItem.aspx?FromDate=" + objdb.Encrypt(txtFromDate.Text) + "&ToDate=" + objdb.Encrypt(txtToDate.Text) + "&MltOfficeID=" + objdb.Encrypt(Office));

                    Response.Write("<script>window.open ('RptStockSummary.aspx?FromDate=" + objdb.Encrypt(txtFromDate.Text) + "&ToDate=" + objdb.Encrypt(txtToDate.Text) + "&MltOfficeID=" + objdb.Encrypt(Office) + "','_blank');</script>");
                }
                //end done changes by Mohini on 21Mar 2022
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
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
            lblExecTime.Text = "";
            var watch = System.Diagnostics.Stopwatch.StartNew();

            lblMsg.Text = "";
            // lblGridMsg.Text = "";
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView2.Rows[index];
                Label lblLedger_ID = (Label)row.Cells[0].FindControl("lblLedger_ID");
                Label lblDebitAmt = (Label)row.Cells[2].FindControl("lblDebitAmt");
                Label lblCreditAmt = (Label)row.Cells[3].FindControl("lblCreditAmt");

                if (lblDebitAmt.Text != "" || lblCreditAmt.Text != "")
                {
                    string Ledger_ID = lblLedger_ID.Text;

                    DataTable dt = (DataTable)ViewState["Heads"];
                    dt.Rows.Add("0", Ledger_ID, "0");

                    FillGridNextLedger(Ledger_ID);
                }
                else
                {
                    // lblGridMsg.Text = "No Record Found.";
                }
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
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
            lblExecTime.Text = "";
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
                dt.Rows.Add("0", Ledger_ID, MonthID);
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
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
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
                string Head_ID = dt.Rows[DtRow - 2]["Head_ID"].ToString();
                string Ledger_ID = dt.Rows[DtRow - 2]["Ledger_ID"].ToString();
                string MonthID = dt.Rows[DtRow - 2]["MonthID"].ToString();
                string flag = "0";
                if (Head_ID != "0" && Ledger_ID == "0" && MonthID == "0")
                {
                    FillGridNext(Head_ID);
                    flag = "1";
                }
                else if (Head_ID == "0" && Ledger_ID != "0" && MonthID == "0")
                {
                    FillGridNextLedger(Ledger_ID);
                    flag = "1";
                }
                else if (Head_ID == "0" && Ledger_ID != "0" && MonthID != "0")
                {
                    FillGridNextLedgerMonth(Ledger_ID, MonthID);
                    flag = "1";
                }
                else if (Head_ID == "00")
                {
                    FillGridNextMltHead();
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
                dt.Rows.RemoveAt(DtRow - 1);
                FillGrid();
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
            lblExecTime.Text = "";
            var watch = System.Diagnostics.Stopwatch.StartNew();
            //btnHeadExcel.Visible = false;
            //btnMonthExcel.Visible = false;
            //btnDayBookExcel.Visible = false;
            lblMsg.Text = "";
            BackGrid();

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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblExecTime.Text = "";
        var watch = System.Diagnostics.Stopwatch.StartNew();

        lblMsg.Text = "";
        DivTable.InnerHtml = "";
        GridBalanceSheetLeft.DataSource = null;
        GridBalanceSheetLeft.DataBind();

        GridBalanceSheetRight.DataSource = null;
        GridBalanceSheetRight.DataBind();

        if (chkClosingBal.Checked == false && chkOpeningBal.Checked == false && chkTransactionAmt.Checked == false)
            chkClosingBal.Checked = true;


        //btnHeadExcel.Visible = false;
        //btnMonthExcel.Visible = false;
        //btnDayBookExcel.Visible = false;

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
            string headingFirst = "<p class='text-center' style='font-weight:600'><span>Draft Balance Sheet <span><br /> SFA Technologies Pvt. Ltd. <br/> [ " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
            lblheadingFirst.Text = headingFirst;

            /*****************SET Search Option Start********************/
            #region SetSearchOption
            Session["CommonOffice"] = Office;
            Session["CommonFromDate"] = txtFromDate.Text;
            Session["CommonToDate"] = txtToDate.Text;
            #endregion
            /*****************SET Search Option End********************/

            FillGrid();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select atleast one Office.');", true);
        }

        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";


    }

    protected void btnHeadExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string ExcelName = "MainHead";
            if (lblHeadName.Text != "")
            {
                ExcelName = lblHeadName.Text;
            }
            string FileName = ExcelName + "_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            if (GridView1.Rows.Count > 0)
            {
                GridView1.GridLines = GridLines.Both;
                GridView1.HeaderStyle.Font.Bold = true;
                GridView1.RenderControl(htmltextwrtter);
            }

            if (GridView2.Rows.Count > 0)
            {
                GridView2.GridLines = GridLines.Both;
                GridView2.HeaderStyle.Font.Bold = true;
                GridView2.RenderControl(htmltextwrtter);
            }
            Response.Write(strwritter.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnMonthExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string ExcelName = "MainHead";
            if (lblHeadName.Text != "")
            {
                ExcelName = lblHeadName.Text;
            }
            string FileName = ExcelName + "_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            if (GridView3.Rows.Count > 0)
            {
                GridView3.GridLines = GridLines.Both;
                GridView3.HeaderStyle.Font.Bold = true;
                GridView3.RenderControl(htmltextwrtter);
            }
            Response.Write(strwritter.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnDayBookExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string ExcelName = "MainHead";
            if (lblHeadName.Text != "")
            {
                ExcelName = lblHeadName.Text;
            }
            string FileName = ExcelName + "_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);


            Response.Write(strwritter.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    protected void RptMain_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            lblExecTime.Text = "";
            var watch = System.Diagnostics.Stopwatch.StartNew();

            lblMsg.Text = "";
            string Head_ID = e.CommandName.ToString();

            DataTable dt = (DataTable)ViewState["Heads"];
            dt.Rows.Add(Head_ID, "0", "0");

            FillGridNext(Head_ID);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridBalanceSheetLeft_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblExecTime.Text = "";
            var watch = System.Diagnostics.Stopwatch.StartNew();

            lblMsg.Text = "";
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridBalanceSheetLeft.Rows[index];
                Label lblHead_ID = (Label)row.Cells[0].FindControl("lblHead_ID");

                string Head_ID = lblHead_ID.Text;
                DataTable dt = (DataTable)ViewState["Heads"];
                if (Head_ID != "0" && Head_ID != "00" && Head_ID != "000" && Head_ID != "0000" && Head_ID != "00000")
                {
                    dt.Rows.Add(Head_ID, "0", "0");
                    FillGridNext(Head_ID);
                }
                else if (Head_ID == "0000")
                {
                    dt.Rows.Add("00", "0", "0");
                    FillGridNextMltHead();
                }
                else if (Head_ID == "00")
                {
                    string Office = "";

                    foreach (ListItem item in ddlOffice.Items)
                    {
                        if (item.Selected)
                        {
                            Office += item.Value + ",";
                        }
                    }

                    //  Response.Redirect("RptStockSummaryItem.aspx?FromDate=" + objdb.Encrypt(txtFromDate.Text) + "&ToDate=" + objdb.Encrypt(txtToDate.Text) + "&MltOfficeID=" + objdb.Encrypt(Office));

                    Response.Redirect("RptProfit_Loss.aspx.aspx?FromDate=" + objdb.Encrypt(txtFromDate.Text) + "&ToDate=" + objdb.Encrypt(txtToDate.Text) + "&MltOfficeID=" + objdb.Encrypt(Office));
                }

                // FillGridNextMltHead()
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridBalanceSheetRight_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblExecTime.Text = "";
            var watch = System.Diagnostics.Stopwatch.StartNew();

            lblMsg.Text = "";
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridBalanceSheetRight.Rows[index];
                Label lblHead_ID = (Label)row.Cells[0].FindControl("lblHead_ID");

                string Head_ID = lblHead_ID.Text;
                DataTable dt = (DataTable)ViewState["Heads"];
                if (Head_ID != "0" && Head_ID != "00" && Head_ID != "000" && Head_ID != "0000" && Head_ID != "00000")
                {
                    dt.Rows.Add(Head_ID, "0", "0");
                    FillGridNext(Head_ID);
                }
                else if (Head_ID == "00")
                {
                    string Office = "";

                    foreach (ListItem item in ddlOffice.Items)
                    {
                        if (item.Selected)
                        {
                            Office += item.Value + ",";
                        }
                    }
                    //   Response.Redirect("RptStockSummaryItem.aspx?FromDate=" + objdb.Encrypt(txtFromDate.Text) + "&ToDate=" + objdb.Encrypt(txtToDate.Text) + "&MltOfficeID=" + objdb.Encrypt(Office));
                    Response.Redirect("RptProfit_Loss.aspx?FromDate=" + objdb.Encrypt(txtFromDate.Text) + "&ToDate=" + objdb.Encrypt(txtToDate.Text) + "&MltOfficeID=" + objdb.Encrypt(Office));
                }
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
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
            string OfficeID = "";
            string Office = "";
            string OfficeName = "";
            int SerialNo = 0;
            string headingPL = "";
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
                headingPL = "<p class='text-center' style='font-weight:600'>Draft Balance Sheet <br /> SFA Technologies Pvt. Ltd. <br/> [ " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";

                Session["headingBS"] = headingPL.ToString();

            }
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    OfficeID += item.Value + ",";
                }
            }
            OfficeID = objdb.Encrypt(OfficeID);
            string FromDate = objdb.Encrypt(Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"));
            string ToDate = objdb.Encrypt(Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"));

            string url = "RptGraphicalBalanceSheet.aspx?OfficeID=" + OfficeID + "&FromDate=" + FromDate + "&ToDate=" + ToDate;

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

    protected void btnShowDetailBook_Click(object sender, EventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            if (ViewState["DayBookVisible"].ToString() == "true")
            {
                DivTable.Visible = false;
                ViewState["DayBookVisible"] = "false";

                GridView4.DataSource = null;
                GridView4.DataBind();


                string Office = "";

                foreach (ListItem item in ddlOffice.Items)
                {
                    if (item.Selected)
                    {
                        Office += item.Value + ",";
                    }
                }


                ds = objdb.ByProcedure("SpFinRptBalanceSheet", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "LedgerTx_Month", "FromDate", "ToDate" }, new string[] { "13", Office, ViewState["Ledger_ID"].ToString(), ViewState["MonthID"].ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {


                    GridView4.Visible = true;

                    GridView4.DataSource = ds;
                    GridView4.DataBind();

                    decimal PreOpening = 0;
                    decimal OpeningBal = 0;
                    string PreClosing = "";
                    if (ds.Tables[0].Rows[0]["OpeningBal"].ToString() != "")
                        PreOpening = decimal.Parse(ds.Tables[0].Rows[0]["OpeningBal"].ToString());

                    if (PreOpening >= 0)
                    {
                        PreClosing = PreOpening.ToString() + " Cr";
                        // GridView4.Rows[0].BackColor = System.Drawing.Color.Bisque;
                    }
                    else
                    {
                        PreClosing = Math.Abs(PreOpening).ToString() + " Dr";
                    }


                    GridView4.Rows[0].Cells[3].Text = PreClosing;

                    int rowcount = ds.Tables[0].Rows.Count;

                    for (int i = 1; i < rowcount; i++)
                    {

                        string DebitAmt = "0";
                        decimal CreditAmt = 0;

                        //if (i != 0)
                        //{
                        if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                            DebitAmt = "-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString();

                        if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                            CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                        //}

                        if (i == 1)
                        {

                            OpeningBal = PreOpening + decimal.Parse(DebitAmt) + CreditAmt;

                        }
                        else
                        {
                            OpeningBal = OpeningBal + decimal.Parse(DebitAmt) + CreditAmt;
                        }

                        // OpeningBal = OpeningBal + decimal.Parse(DebitAmt) + CreditAmt;
                        if (OpeningBal >= 0)
                        {
                            GridView4.Rows[i].Cells[3].Text = OpeningBal.ToString() + " Cr";
                            //  GridView4.Rows[i].BackColor = System.Drawing.Color.Bisque;
                        }
                        else
                        {
                            GridView4.Rows[i].Cells[3].Text = Math.Abs(OpeningBal).ToString() + " Dr";
                        }
                    }
                    GridView4.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView4.UseAccessibleHeader = true;
                }
            }
            else
            {
                DivTable.Visible = true;
                GridView4.Visible = false;
                ViewState["DayBookVisible"] = "true";
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
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
