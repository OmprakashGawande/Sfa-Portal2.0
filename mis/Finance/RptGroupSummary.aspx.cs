using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

public partial class mis_Finance_RptGroupSummary : System.Web.UI.Page
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

                    string FY = GetCurrentFinancialYear();
                    string[] YEAR = FY.Split('-');
                    DateTime FromDate = new DateTime(int.Parse(YEAR[0]), 4, 1);
                    txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    txtFromDate.Attributes.Add("readonly", "readonly");

                    FillVoucherDate();
                    FillDropdown();
                    HeadList();

                    btnHeadExcel.Visible = false;
                    btnMonthExcel.Visible = false;
                    btnDayBookExcel.Visible = false;
                    spnAltB.Visible = false;
                    spnAltW.Visible = false;

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
            if (ViewState["Office_ID"].ToString() == "1")
            {
                ddlOffice.Enabled = true;
            }
            ds = objdb.ByProcedure("SpFinRptGroupSummary",
                   new string[] { "flag" },
                   new string[] { "0" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }

            DataSet ds1 = objdb.ByProcedure("SpFinRptGroupSummary", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                ddlGroup.DataSource = ds1;
                ddlGroup.DataTextField = "Head_Name";
                ddlGroup.DataValueField = "Head_ID";
                ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, new ListItem("Select", "0"));

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
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblHeadName.Text = "";
          //  FillGrid();

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
    //protected void FillGrid()
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        btnHeadExcel.Visible = false;
    //        btnMonthExcel.Visible = false;
    //        btnDayBookExcel.Visible = false;
    //        spnAltB.Visible = false;
    //        spnAltW.Visible = false;


    //        decimal? DebitAmt = 0;
    //        decimal? CreditAmt = 0;

    //        GridView1.DataSource = null;
    //        GridView1.DataBind();
    //        GridView2.DataSource = new string[] { };
    //        GridView3.DataSource = new string[] { };

    //        lblHeadName.Text = "";
    //        ds = null;

    //        string Office = "";

    //        foreach (ListItem item in ddlOffice.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                Office += item.Value + ",";
    //            }
    //        }
    //        ShowColumn();
    //        ds = objdb.ByProcedure("SpFinRptGroupSummary", new string[] { "flag", "Office_ID_Mlt", "FromDate", "ToDate" }, new string[] { "1", Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
    //        if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
    //        {
    //            btnHeadExcel.Visible = true;

    //            ds.Tables[0].Rows.Add("0", "Difference in Opening Balances", "0", "0", "0", "0", "0", "0", "0", "0");

    //            GridView1.DataSource = ds;
    //            GridView1.DataBind();

    //            GridView1.UseAccessibleHeader = true;
    //            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

    //            decimal TotalOpeningBal = 0;
    //            decimal TotalClosingBal = 0;
    //            decimal TotalDebitAmt = 0;
    //            decimal TotalCreditAmt = 0;


    //            decimal OpenTotalDr = 0;
    //            decimal OpenTotalCr = 0;
    //            decimal CloseTotalDr = 0;
    //            decimal CloseTotalCr = 0;


    //            int rowcount = ds.Tables[0].Rows.Count - 1;
    //            for (int i = 0; i < rowcount; i++)
    //            {
    //                decimal OpeningBal = 0;
    //                decimal PreOpeningBal = 0;


    //                if (ds.Tables[0].Rows[i]["PreOpeningBalance"].ToString() != "")
    //                    PreOpeningBal = decimal.Parse(ds.Tables[0].Rows[i]["PreOpeningBalance"].ToString());

    //                if (ds.Tables[0].Rows[i]["OpeningBalance"].ToString() != "")
    //                    OpeningBal = decimal.Parse(ds.Tables[0].Rows[i]["OpeningBalance"].ToString());

    //                if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
    //                    TotalDebitAmt = TotalDebitAmt + decimal.Parse(ds.Tables[0].Rows[i]["DebitAmt"].ToString());

    //                if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
    //                    TotalCreditAmt = TotalCreditAmt + decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());

    //                TotalOpeningBal = TotalOpeningBal + PreOpeningBal;
    //                TotalClosingBal = TotalClosingBal + OpeningBal;


    //                if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
    //                {
    //                    if (PreOpeningBal >= 0)
    //                    {
    //                        GridView1.Rows[i].Cells[2].Text = PreOpeningBal.ToString() + " Cr";
    //                    }
    //                    else
    //                    {
    //                        GridView1.Rows[i].Cells[2].Text = Math.Abs(PreOpeningBal).ToString() + " Dr";
    //                    }

    //                    if (OpeningBal >= 0)
    //                    {
    //                        GridView1.Rows[i].Cells[6].Text = OpeningBal.ToString() + " Cr";

    //                    }
    //                    else
    //                    {
    //                        GridView1.Rows[i].Cells[6].Text = Math.Abs(OpeningBal).ToString() + " Dr";
    //                    }

    //                }
    //                else
    //                {
    //                    if (ds.Tables[0].Rows[i]["DebitOpeningBalance"].ToString() != "")
    //                    {
    //                        GridView1.Rows[i].Cells[2].Text = ds.Tables[0].Rows[i]["DebitOpeningBalance"].ToString();
    //                        OpenTotalDr = OpenTotalDr + decimal.Parse(ds.Tables[0].Rows[i]["DebitOpeningBalance"].ToString());
    //                    }

    //                    if (ds.Tables[0].Rows[i]["CreditOpeningBalance"].ToString() != "")
    //                    {
    //                        GridView1.Rows[i].Cells[3].Text = ds.Tables[0].Rows[i]["CreditOpeningBalance"].ToString();
    //                        OpenTotalCr = OpenTotalCr + decimal.Parse(ds.Tables[0].Rows[i]["CreditOpeningBalance"].ToString());
    //                    }
    //                    if (ds.Tables[0].Rows[i]["DebitClosingBalance"].ToString() != "")
    //                    {
    //                        GridView1.Rows[i].Cells[6].Text = ds.Tables[0].Rows[i]["DebitClosingBalance"].ToString();
    //                        CloseTotalDr = CloseTotalDr + decimal.Parse(ds.Tables[0].Rows[i]["DebitClosingBalance"].ToString());
    //                    }
    //                    if (ds.Tables[0].Rows[i]["CreditClosingBalance"].ToString() != "")
    //                    {
    //                        GridView1.Rows[i].Cells[7].Text = ds.Tables[0].Rows[i]["CreditClosingBalance"].ToString();
    //                        CloseTotalCr = CloseTotalCr + decimal.Parse(ds.Tables[0].Rows[i]["CreditClosingBalance"].ToString());
    //                    }
    //                }
    //            }
    //            GridView1.Rows[rowcount].Visible = false;
    //            if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
    //            {
    //                GridView1.FooterRow.Cells[1].Text = "<b>Grand Total</b>";
    //                if (TotalOpeningBal >= 0)
    //                    GridView1.FooterRow.Cells[2].Text = TotalOpeningBal.ToString() + " Cr";
    //                else
    //                    GridView1.FooterRow.Cells[2].Text = Math.Abs(TotalOpeningBal).ToString() + " Dr";

    //                GridView1.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
    //                GridView1.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();

    //                if (TotalClosingBal >= 0)
    //                    GridView1.FooterRow.Cells[6].Text = TotalClosingBal.ToString() + " Cr";
    //                else
    //                    GridView1.FooterRow.Cells[6].Text = Math.Abs(TotalClosingBal).ToString() + " Dr";

    //            }
    //            else
    //            {
    //                if (OpenTotalDr != OpenTotalCr || CloseTotalDr != CloseTotalCr || TotalDebitAmt != TotalCreditAmt)
    //                {
    //                    int status = 0;

    //                    if (chkOpeningBal.Checked == true)
    //                    {
    //                        if (OpenTotalDr < OpenTotalCr)
    //                        {
    //                            status = 1;
    //                            GridView1.Rows[rowcount].Cells[2].Text = (OpenTotalCr - OpenTotalDr).ToString();// +"Dr";
    //                            OpenTotalDr = OpenTotalCr;
    //                        }
    //                        else if (OpenTotalDr > OpenTotalCr)
    //                        {
    //                            status = 1;
    //                            GridView1.Rows[rowcount].Cells[3].Text = (OpenTotalDr - OpenTotalCr).ToString();// + "Cr";
    //                            OpenTotalCr = OpenTotalDr;
    //                        }
    //                    }

    //                    if (chkTransactionAmt.Checked == true)
    //                    {

    //                        if (TotalDebitAmt < TotalCreditAmt)
    //                        {
    //                            status = 1;
    //                            GridView1.Rows[rowcount].Cells[4].Text = (TotalCreditAmt - TotalDebitAmt).ToString();// +"Dr";
    //                            TotalDebitAmt = TotalCreditAmt;
    //                        }
    //                        else if (TotalDebitAmt > TotalCreditAmt)
    //                        {
    //                            status = 1;
    //                            GridView1.Rows[rowcount].Cells[5].Text = (TotalDebitAmt - TotalCreditAmt).ToString();// + "Cr";
    //                            TotalCreditAmt = TotalDebitAmt;
    //                        }
    //                    }
    //                    if (chkClosingBal.Checked == true)
    //                    {
    //                        if (CloseTotalDr < CloseTotalCr)
    //                        {
    //                            status = 1;
    //                            GridView1.Rows[rowcount].Cells[6].Text = (CloseTotalCr - CloseTotalDr).ToString();// + "Dr";
    //                            CloseTotalDr = CloseTotalCr;
    //                        }
    //                        else if (CloseTotalDr > CloseTotalCr)
    //                        {
    //                            status = 1;
    //                            GridView1.Rows[rowcount].Cells[7].Text = (CloseTotalDr - CloseTotalCr).ToString();// + "Cr";
    //                            CloseTotalCr = CloseTotalDr;
    //                        }
    //                    }
    //                    if (status == 1)
    //                        GridView1.Rows[rowcount].Visible = true;

    //                }
    //                GridView1.FooterRow.Cells[1].Text = "<b>Grand Total</b>";
    //                //if (TotalOpeningBal >= 0)
    //                //    GridView1.FooterRow.Cells[3].Text = TotalOpeningBal.ToString();// + " Cr";
    //                //else
    //                //    GridView1.FooterRow.Cells[2].Text = Math.Abs(TotalOpeningBal).ToString();// + " Dr";

    //                GridView1.FooterRow.Cells[2].Text = Math.Abs(OpenTotalDr).ToString();//Dr
    //                GridView1.FooterRow.Cells[3].Text = OpenTotalCr.ToString(); //Cr



    //                GridView1.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
    //                GridView1.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();

    //                //if (TotalClosingBal >= 0)
    //                //    GridView1.FooterRow.Cells[7].Text = TotalClosingBal.ToString();// + " Cr";
    //                //else
    //                //    GridView1.FooterRow.Cells[6].Text = Math.Abs(TotalClosingBal).ToString();// + " Dr";

    //                GridView1.FooterRow.Cells[6].Text = Math.Abs(CloseTotalDr).ToString(); //Dr
    //                GridView1.FooterRow.Cells[7].Text = CloseTotalCr.ToString(); //Cr

    //            }


    //            GridView1.FooterRow.Cells[2].CssClass = "align-right";
    //            GridView1.FooterRow.Cells[3].CssClass = "align-right";
    //            GridView1.FooterRow.Cells[4].CssClass = "align-right";
    //            GridView1.FooterRow.Cells[5].CssClass = "align-right";
    //            GridView1.FooterRow.Cells[6].CssClass = "align-right";
    //            GridView1.FooterRow.Cells[7].CssClass = "align-right";
    //            GridView1.FooterRow.Style.Add("font-weight", "700");


    //            GridView1.UseAccessibleHeader = true;
    //            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

    //            GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
    //        }


    //        GridView2.DataBind();
    //        GridView3.DataBind();

    //        HeadList();

    //        ShowHideColumn();

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

    //    }
    //}
    protected void FillGridNext(string Head_ID)
    {
        try
        {
            // lblGridMsg.Text = "";
            btnHeadExcel.Visible = false;
            btnMonthExcel.Visible = false;
            btnDayBookExcel.Visible = false;
            spnAltW.Visible = false;

            ShowColumn();
            DataSet ds1 = objdb.ByProcedure("SpFinRptGroupSummary", new string[] { "flag", "Head_ID" }, new string[] { "9", Head_ID }, "dataset");
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
            //GridView1.FooterRow.Visible = true;

            decimal OpenTotalDr = 0;
            decimal OpenTotalCr = 0;
            decimal CloseTotalDr = 0;
            decimal CloseTotalCr = 0;
            int rowcount = 0;

            ds = objdb.ByProcedure("SpFinRptGroupSummary", new string[] { "flag", "Office_ID_Mlt", "Head_ID", "FromDate", "ToDate" }, new string[] { "2", Office, Head_ID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                btnHeadExcel.Visible = true;
                spnAltB.Visible = true;

                Grid1 = 1;


                ds.Tables[0].Rows.Add("0", "Difference in Opening Balances", "0", "0", "0", "0", "0", "0", "0", "0");
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                // Opening Balance
                rowcount = ds.Tables[0].Rows.Count - 1;
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
                        if (ds.Tables[0].Rows[i]["DebitOpeningBalance"].ToString() != "")
                        {
                            GridView1.Rows[i].Cells[2].Text = ds.Tables[0].Rows[i]["DebitOpeningBalance"].ToString();
                            OpenTotalDr = OpenTotalDr + decimal.Parse(ds.Tables[0].Rows[i]["DebitOpeningBalance"].ToString());
                        }

                        if (ds.Tables[0].Rows[i]["CreditOpeningBalance"].ToString() != "")
                        {
                            GridView1.Rows[i].Cells[3].Text = ds.Tables[0].Rows[i]["CreditOpeningBalance"].ToString();
                            OpenTotalCr = OpenTotalCr + decimal.Parse(ds.Tables[0].Rows[i]["CreditOpeningBalance"].ToString());
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
                       
                    }




                }

                GridView1.Rows[rowcount].Visible = false;

                if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                {
                    GridView1.FooterRow.Cells[1].Text = "<b>Grand Total</b>";
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
                    if (OpenTotalDr != OpenTotalCr || CloseTotalDr != CloseTotalCr || TotalDebitAmt != TotalCreditAmt)
                    {
                        int status = 0;

                        if (chkOpeningBal.Checked == true)
                        {
                            if (OpenTotalDr < OpenTotalCr)
                            {
                                status = 1;
                                GridView1.Rows[rowcount].Cells[2].Text = (OpenTotalCr - OpenTotalDr).ToString();// +"Dr";
                                // = OpenTotalCr;
                            }
                            else if (OpenTotalDr > OpenTotalCr)
                            {
                                status = 1;
                                GridView1.Rows[rowcount].Cells[3].Text = (OpenTotalDr - OpenTotalCr).ToString();// + "Cr";
                                //  OpenTotalCr = OpenTotalDr;
                            }
                        }

                        if (chkTransactionAmt.Checked == true)
                        {

                            if (TotalDebitAmt < TotalCreditAmt)
                            {
                                status = 1;
                                GridView1.Rows[rowcount].Cells[4].Text = (TotalCreditAmt - TotalDebitAmt).ToString();// +"Dr";
                                // TotalDebitAmt = TotalCreditAmt;
                            }
                            else if (TotalDebitAmt > TotalCreditAmt)
                            {
                                status = 1;
                                GridView1.Rows[rowcount].Cells[5].Text = (TotalDebitAmt - TotalCreditAmt).ToString();// + "Cr";
                                // TotalCreditAmt = TotalDebitAmt;
                            }
                        }
                        if (chkClosingBal.Checked == true)
                        {
                            if (CloseTotalDr < CloseTotalCr)
                            {
                                status = 1;
                                GridView1.Rows[rowcount].Cells[6].Text = (CloseTotalCr - CloseTotalDr).ToString();// + "Dr";
                                //  CloseTotalDr = CloseTotalCr;
                            }
                            else if (CloseTotalDr > CloseTotalCr)
                            {
                                status = 1;
                                GridView1.Rows[rowcount].Cells[7].Text = (CloseTotalDr - CloseTotalCr).ToString();// + "Cr";
                                //  CloseTotalCr = CloseTotalDr;
                            }
                        }
                        if (status == 1)
                            GridView1.Rows[rowcount].Visible = true;

                    }


                    GridView1.FooterRow.Cells[1].Text = "<b>Grand Total</b>";


                    if (chkOpeningBal.Checked == true)
                    {
                        if (OpenTotalDr < OpenTotalCr)
                        {
                            //  GridView1.FooterRow.Cells[2].Text = Math.Abs(OpenTotalCr - OpenTotalDr).ToString();//Dr
                            GridView1.FooterRow.Cells[2].Text = Math.Abs(OpenTotalCr).ToString();//Dr
                            GridView1.FooterRow.Cells[3].Text = OpenTotalCr.ToString(); //Cr
                        }
                        else if (OpenTotalDr > OpenTotalCr)
                        {
                            GridView1.FooterRow.Cells[2].Text = OpenTotalDr.ToString();//Dr
                            // GridView1.FooterRow.Cells[3].Text = (OpenTotalDr - OpenTotalCr).ToString(); //Cr
                            GridView1.FooterRow.Cells[3].Text = (OpenTotalDr).ToString(); //Cr
                        }
                    }

                    if (chkTransactionAmt.Checked == true)
                    {

                        if (TotalDebitAmt < TotalCreditAmt)
                        {
                            // GridView1.FooterRow.Cells[4].Text = (TotalCreditAmt - TotalDebitAmt).ToString();
                            GridView1.FooterRow.Cells[4].Text = (TotalCreditAmt).ToString();
                            GridView1.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();
                        }
                        else if (TotalDebitAmt > TotalCreditAmt)
                        {
                            GridView1.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
                            //GridView1.FooterRow.Cells[5].Text = (TotalDebitAmt - TotalCreditAmt).ToString();
                            GridView1.FooterRow.Cells[5].Text = (TotalDebitAmt).ToString();
                        }
                    }
                    if (chkClosingBal.Checked == true)
                    {
                        if (CloseTotalDr < CloseTotalCr)
                        {
                            //GridView1.FooterRow.Cells[6].Text = Math.Abs(CloseTotalCr - CloseTotalDr).ToString(); //Dr
                            GridView1.FooterRow.Cells[6].Text = Math.Abs(CloseTotalCr).ToString(); //Dr
                            GridView1.FooterRow.Cells[7].Text = CloseTotalCr.ToString(); //Cr
                        }
                        else if (CloseTotalDr > CloseTotalCr)
                        {
                            GridView1.FooterRow.Cells[6].Text = CloseTotalDr.ToString(); //Dr
                            //   GridView1.FooterRow.Cells[7].Text = (CloseTotalDr - CloseTotalCr).ToString(); //Cr
                            GridView1.FooterRow.Cells[7].Text = (CloseTotalDr).ToString(); //Cr
                        }
                    }
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

            ds = objdb.ByProcedure("SpFinRptGroupSummary", new string[] { "flag", "Office_ID_Mlt", "Head_ID", "FromDate", "ToDate" }, new string[] { "3", Office, Head_ID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                btnHeadExcel.Visible = true;
                spnAltB.Visible = true;
                GridView1.Rows[rowcount].Visible = false;
                Grid2 = 1;

                ds.Tables[0].Rows.Add("0", "Difference in Opening Balances", "0", "0", "0", "0");
                GridView2.DataSource = ds;
                GridView2.DataBind();
                GridView2.UseAccessibleHeader = true;
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;

                rowcount = 0;
                rowcount = ds.Tables[0].Rows.Count - 1;


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

               
                GridView2.Rows[rowcount].Visible = false;

                if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                {
                    GridView2.FooterRow.Cells[1].Text = "<b>Grand Total</b>";
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
                    if (OpenTotalDr != OpenTotalCr || CloseTotalDr != CloseTotalCr || TotalDebitAmt != TotalCreditAmt)
                    {
                        int status = 0;

                        if (chkOpeningBal.Checked == true)
                        {
                            if (OpenTotalDr < OpenTotalCr)
                            {
                                status = 1;
                                GridView2.Rows[rowcount].Cells[2].Text = (OpenTotalCr - OpenTotalDr).ToString();// +"Dr";
                            }
                            else if (OpenTotalDr > OpenTotalCr)
                            {
                                status = 1;
                                GridView2.Rows[rowcount].Cells[3].Text = (OpenTotalDr - OpenTotalCr).ToString();// + "Cr";
                            }
                        }

                        if (chkTransactionAmt.Checked == true)
                        {

                            if (TotalDebitAmt < TotalCreditAmt)
                            {
                                status = 1;
                                GridView2.Rows[rowcount].Cells[4].Text = (TotalCreditAmt - TotalDebitAmt).ToString();// +"Dr";
                            }
                            else if (TotalDebitAmt > TotalCreditAmt)
                            {
                                status = 1;
                                GridView2.Rows[rowcount].Cells[5].Text = (TotalDebitAmt - TotalCreditAmt).ToString();// + "Cr";
                            }
                        }
                        if (chkClosingBal.Checked == true)
                        {
                            if (CloseTotalDr < CloseTotalCr)
                            {
                                status = 1;
                                GridView2.Rows[rowcount].Cells[6].Text = (CloseTotalCr - CloseTotalDr).ToString();// + "Dr";
                            }
                            else if (CloseTotalDr > CloseTotalCr)
                            {
                                status = 1;
                                GridView2.Rows[rowcount].Cells[7].Text = (CloseTotalDr - CloseTotalCr).ToString();// + "Cr";
                            }
                        }
                        if (status == 1)
                            GridView2.Rows[rowcount].Visible = true;

                    }


                    GridView2.FooterRow.Cells[1].Text = "<b>Grand Total</b>";


                    if (chkOpeningBal.Checked == true)
                    {
                        if (OpenTotalDr < OpenTotalCr)
                        {
                            // GridView2.FooterRow.Cells[2].Text = Math.Abs(OpenTotalCr - OpenTotalDr).ToString();//Dr
                            GridView2.FooterRow.Cells[2].Text = Math.Abs(OpenTotalCr).ToString();//Dr
                            GridView2.FooterRow.Cells[3].Text = OpenTotalCr.ToString(); //Cr
                        }
                        else if (OpenTotalDr > OpenTotalCr)
                        {
                            GridView2.FooterRow.Cells[2].Text = OpenTotalDr.ToString();//Dr
                            // GridView2.FooterRow.Cells[3].Text = (OpenTotalDr - OpenTotalCr).ToString(); //Cr
                            GridView2.FooterRow.Cells[3].Text = (OpenTotalDr).ToString(); //Cr
                        }
                    }

                    if (chkTransactionAmt.Checked == true)
                    {

                        if (TotalDebitAmt < TotalCreditAmt)
                        {
                            // GridView2.FooterRow.Cells[4].Text = (TotalCreditAmt - TotalDebitAmt).ToString();
                            GridView2.FooterRow.Cells[4].Text = (TotalCreditAmt).ToString();
                            GridView2.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();
                        }
                        else if (TotalDebitAmt > TotalCreditAmt)
                        {
                            GridView2.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
                            //GridView2.FooterRow.Cells[5].Text = (TotalDebitAmt - TotalCreditAmt).ToString();
                            GridView2.FooterRow.Cells[5].Text = (TotalDebitAmt).ToString();
                        }
                    }
                    if (chkClosingBal.Checked == true)
                    {
                        if (CloseTotalDr < CloseTotalCr)
                        {
                            // GridView2.FooterRow.Cells[6].Text = Math.Abs(CloseTotalCr - CloseTotalDr).ToString(); //Dr
                            GridView2.FooterRow.Cells[6].Text = Math.Abs(CloseTotalCr).ToString(); //Dr
                            GridView2.FooterRow.Cells[7].Text = CloseTotalCr.ToString(); //Cr
                        }
                        else if (CloseTotalDr > CloseTotalCr)
                        {
                            GridView2.FooterRow.Cells[6].Text = CloseTotalDr.ToString(); //Dr
                            GridView2.FooterRow.Cells[7].Text = (CloseTotalDr).ToString(); //Cr
                            //GridView2.FooterRow.Cells[7].Text = (CloseTotalDr - CloseTotalCr).ToString(); //Cr
                        }
                    }
                }


                GridView2.FooterRow.Cells[2].CssClass = "align-right";
                GridView2.FooterRow.Cells[3].CssClass = "align-right";
                GridView2.FooterRow.Cells[4].CssClass = "align-right";
                GridView2.FooterRow.Cells[5].CssClass = "align-right";
                GridView2.FooterRow.Cells[6].CssClass = "align-right";
                GridView2.FooterRow.Cells[7].CssClass = "align-right";
                GridView2.FooterRow.Style.Add("font-weight", "700");

                GridView2.FooterRow.TableSection = TableRowSection.TableFooter;



               
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
            btnHeadExcel.Visible = false;
            btnMonthExcel.Visible = false;
            btnDayBookExcel.Visible = false;

            spnAltW.Visible = false;

            DataSet ds1 = objdb.ByProcedure("SpFinRptGroupSummary", new string[] { "flag", "Ledger_ID" }, new string[] { "10", Ledger_ID }, "dataset");
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
            ds = objdb.ByProcedure("SpFinRptGroupSummary", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate" }, new string[] { "4", Office, Ledger_ID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                btnMonthExcel.Visible = true;
                spnAltB.Visible = true;

                GridView3.DataSource = ds;
                GridView3.DataBind();

                decimal PreOpening = 0;
                decimal OpeningBal = 0;

                if (ds.Tables[0].Rows[0]["PreOpening"].ToString() != "")
                    PreOpening = decimal.Parse(ds.Tables[0].Rows[0]["PreOpening"].ToString());


                OpeningBal = PreOpening;

                //if(chkOpeningBal.Checked ==true)
                //{
                //    if (OpeningBal < 0)
                //    {
                //        lblHeadName.Text = lblHeadName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBal).ToString() + " Dr </span>";
                //    }
                //    else
                //    {
                //        lblHeadName.Text = lblHeadName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBal).ToString() + " Cr </span>";
                //    }
                //}
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
                int rowcount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < rowcount; i++)
                {

                    string DebitAmt = "0";
                    decimal CreditAmt = 0;
                    decimal CurrentOpening = 0;

                    //if (i != 0)
                    //{
                    if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                        DebitAmt = "-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString();

                    if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                        CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
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
            }

            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            GridView2.DataSource = new string[] { };
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
            btnHeadExcel.Visible = false;
            btnMonthExcel.Visible = false;
            btnDayBookExcel.Visible = false;


            lblHeadName.Text = "";
            DataSet ds1 = objdb.ByProcedure("SpFinRptGroupSummary", new string[] { "flag", "Ledger_ID" }, new string[] { "10", Ledger_ID }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                lblHeadName.Text = ds1.Tables[0].Rows[0]["Ledger_Name"].ToString() + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
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
            ds = objdb.ByProcedure("SpFinRptGroupSummary", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "LedgerTx_Month", "FromDate", "ToDate" }, new string[] { "6", Office, Ledger_ID, MonthID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                btnDayBookExcel.Visible = true;
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
                int Count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");

                    // Inner Transaction
                    ds1 = null;
                    ds1 = objdb.ByProcedure("SpFinRptGroupSummary", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", }, new string[] { "12", ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString(), Ledger_ID }, "dataset");
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
                                    htmlStr.Append("\n<p class='subledger HideRecord'  style='display:none;'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
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
                string TotalBAL = "";
                if (OpeningBal < 0)
                {
                    TotalBAL = "";

                    if (Math.Abs(OpeningBal) != 0)
                        TotalBAL = Math.Abs(OpeningBal).ToString();

                    htmlStr.Append("<td class='align-right'><b>" + TotalBAL.ToString() + "</b></td>");
                    htmlStr.Append("<td></td>");

                }
                else
                {
                    TotalBAL = "";
                    htmlStr.Append("<td></td>");

                    if (Math.Abs(OpeningBal) != 0)
                        TotalBAL = Math.Abs(OpeningBal).ToString();

                    htmlStr.Append("<td class='align-right'><b>" + TotalBAL.ToString() + "</b></td>");
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
            lblMsg.Text = "";
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                Label lblHead_ID = (Label)row.Cells[0].FindControl("lblHead_ID");

                string Head_ID = lblHead_ID.Text;
                if (Head_ID != "0")
                {
                    DataTable dt = (DataTable)ViewState["Heads"];
                    dt.Rows.Add(Head_ID, "0", "0");

                    FillGridNext(Head_ID);
                }

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
            // lblGridMsg.Text = "";
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView2.Rows[index];
                Label lblLedger_ID = (Label)row.Cells[0].FindControl("lblLedger_ID");
                Label lblDebitAmt = (Label)row.Cells[2].FindControl("lblDebitAmt");
                Label lblCreditAmt = (Label)row.Cells[3].FindControl("lblCreditAmt");

                if ((lblDebitAmt.Text != "" || lblCreditAmt.Text != "") && lblLedger_ID.Text != "0")
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
                //else
                //{
                //    PreBal = ViewState["Bal"].ToString();
                //}


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
                if (flag == "1")
                {
                    dt.Rows.RemoveAt(DtRow - 1);
                    ViewState["Heads"] = dt;
                }

            }
            else
            {
                FillGridNext(ddlGroup.SelectedValue.ToString());
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
            btnHeadExcel.Visible = false;
            btnMonthExcel.Visible = false;
            btnDayBookExcel.Visible = false;
            lblMsg.Text = "";
            BackGrid();

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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        string msg = "";
        DivTable.InnerHtml = "";

        if (chkClosingBal.Checked == false && chkOpeningBal.Checked == false && chkTransactionAmt.Checked == false)
            chkClosingBal.Checked = true;


        btnHeadExcel.Visible = false;
        btnMonthExcel.Visible = false;
        btnDayBookExcel.Visible = false;

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
        if (Office == "")
        {
            msg = msg + "Select atleast one Office. \\n";
        }
        if(ddlGroup.SelectedIndex ==0)
        {
            msg = msg + "Select Group Name. \\n";
        }

        if (msg == "")
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
            string headingFirst = "<p class='text-center' style='font-weight:600'>Group Summary <br /> MP State Agro Industries Development Corporation, <br/> [ " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
            lblheadingFirst.Text = headingFirst;
            FillGridNext(ddlGroup.SelectedValue.ToString());
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert(" + msg + ");", true);
        }


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


}
