using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Microsoft.Reporting.WebForms;


public partial class mis_Payroll_PayRollQuarterlyEarnDedDetail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
        {
            if (!IsPostBack)
            {
                HideShow("default");
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillOffice();
                FillFinancialYear();
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    ddlOffice_Name.Enabled = true;
                    ddlOffice_Name.Items.FindByValue(ViewState["Office_ID"].ToString()).Selected = true;
                }
                else
                {
                    ddlOffice_Name.Enabled = false;
                    ddlOffice_Name.Items.FindByValue(ViewState["Office_ID"].ToString()).Selected = true;
                }
                //FillEmptyGrid();

            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillOffice()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminOffice",
                        new string[] { "flag" },
                        new string[] { "1" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice_Name.DataSource = ds;
                ddlOffice_Name.DataTextField = "Office_Name";
                ddlOffice_Name.DataValueField = "Office_ID";
                ddlOffice_Name.DataBind();
                ddlOffice_Name.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlOffice_Name.DataSource = null;
                ddlOffice_Name.DataBind();
                ddlOffice_Name.Items.Clear();
                ddlOffice_Name.Items.Insert(0, new ListItem("All", "0"));
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblDeductionDetails.Text = "";
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                ds = null;
                ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "Year" }, new string[] { "2", ddlFinancialYear.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlEarnDeducHead.DataSource = ds;
                    ddlEarnDeducHead.DataTextField = "EarnDeduction_Name";
                    ddlEarnDeducHead.DataValueField = "EarnDeduction_ID";
                    ddlEarnDeducHead.DataBind();
                    ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlEarnDeducHead.Items.Clear();
                ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblDeductionDetails.Text = "";
    //        if (ddlFinancialYear.SelectedIndex > 0)
    //        {
    //            ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
    //            ddlEarnDeducHead.Items.Insert(1, new ListItem("Income Tax & Professional Tax", "1"));
    //        }
    //        else
    //        {
    //            ddlEarnDeducHead.Items.Clear();
    //            ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void FillFinancialYear()
    {
        try
        {

            ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "8" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlFinancialYear.DataSource = ds;
                ddlFinancialYear.DataTextField = "Year";
                ddlFinancialYear.DataValueField = "Year";
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
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
            ds = null;
            lblMsg.Text = "";
            string msg = "";
            lblDeductionDetails.Text = "";
            if (ddlFinancialYear.SelectedIndex <= 0)
            {
                msg += "Select Financial Year \\n";
            }
            if (ddlMonth.SelectedIndex <= 0)
            {
                msg += "Select Quarter \\n";
            }
            if (ddlEarnDeducHead.SelectedIndex <= 0)
            {
                msg += "Select Earning Deduction Head \\n";
            }
            if (msg == "")
            {
                lblDeductionDetails.Text = ddlEarnDeducHead.SelectedItem.ToString() + " Details (" + ddlMonth.SelectedItem.ToString() + "-" + ddlFinancialYear.SelectedItem.ToString() + ") of " + ddlOffice_Name.SelectedItem.ToString();

                ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise",
                    new string[] { "flag", "Office_ID", "SalaryMonth", "SalaryYear", "EarnDeduction_ID" },
                    new string[] { "13", ddlOffice_Name.SelectedValue, ddlMonth.SelectedValue, ddlFinancialYear.SelectedValue, ddlEarnDeducHead.SelectedValue }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    HideShow("o");
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    /*****************/
                    if (ddlMonth.SelectedValue.ToString() == "1")
                    {
                        GridView1.HeaderRow.Cells[5].Text = "Jan";
                        GridView1.HeaderRow.Cells[6].Text = "Feb";
                        GridView1.HeaderRow.Cells[7].Text = "Mar";
                        GridView1.HeaderRow.Cells[10].Text = "Jan EarnDed";
                        GridView1.HeaderRow.Cells[11].Text = "Feb EarnDed";
                        GridView1.HeaderRow.Cells[12].Text = "Mar EarnDed";

                    }
                    else if (ddlMonth.SelectedValue.ToString() == "2")
                    {
                        GridView1.HeaderRow.Cells[5].Text = "Apr";
                        GridView1.HeaderRow.Cells[6].Text = "May";
                        GridView1.HeaderRow.Cells[7].Text = "Jun";
                        GridView1.HeaderRow.Cells[10].Text = "Apr EarnDed";
                        GridView1.HeaderRow.Cells[11].Text = "May EarnDed";
                        GridView1.HeaderRow.Cells[12].Text = "Jun EarnDed";

                    }
                    else if (ddlMonth.SelectedValue.ToString() == "3")
                    {
                        GridView1.HeaderRow.Cells[5].Text = "Jul";
                        GridView1.HeaderRow.Cells[6].Text = "Aug";
                        GridView1.HeaderRow.Cells[7].Text = "Sep";
                        GridView1.HeaderRow.Cells[10].Text = "Jul EarnDed";
                        GridView1.HeaderRow.Cells[11].Text = "Aug EarnDed";
                        GridView1.HeaderRow.Cells[12].Text = "Sep EarnDed";

                    }
                    else if (ddlMonth.SelectedValue.ToString() == "4")
                    {
                        GridView1.HeaderRow.Cells[5].Text = "Oct";
                        GridView1.HeaderRow.Cells[6].Text = "Nov";
                        GridView1.HeaderRow.Cells[7].Text = "Dec";
                        GridView1.HeaderRow.Cells[10].Text = "Oct PTax";
                        GridView1.HeaderRow.Cells[11].Text = "Nov PTax";
                        GridView1.HeaderRow.Cells[12].Text = "Dec PTax";
                    }
                    /*****************/
                    Decimal EarningAmt_Arr = 0;
                    Decimal EarningAmt_1 = 0;
                    Decimal EarningAmt_2 = 0;
                    Decimal EarningAmt_3 = 0;
                    Decimal EarningAmt_Total = 0;

                    Decimal PTax_Total_Arr = 0;
                    Decimal PTax_Total_1 = 0;
                    Decimal PTax_Total_2 = 0;
                    Decimal PTax_Total_3 = 0;
                    Decimal PTax_Total = 0;

                    /*******************/
                    int i = 0;
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        decimal EarningAmt = 0;
                        decimal PTax_Amt = 0;
                        if (ds.Tables[0].Rows[i]["ArrearEarnings"].ToString() != "")
                        {
                            EarningAmt += Convert.ToDecimal(ds.Tables[0].Rows[i]["ArrearEarnings"].ToString()); // Column Value
                            EarningAmt_Arr += Convert.ToDecimal(ds.Tables[0].Rows[i]["ArrearEarnings"].ToString());
                            EarningAmt_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["ArrearEarnings"].ToString());
                        }
                        if (ds.Tables[0].Rows[i]["EarningTotal1"].ToString() != "")
                        {
                            EarningAmt += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal1"].ToString()); // Column Value
                            EarningAmt_1 += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal1"].ToString());
                            EarningAmt_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal1"].ToString());
                        }
                        if (ds.Tables[0].Rows[i]["EarningTotal2"].ToString() != "")
                        {
                            EarningAmt += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal2"].ToString()); // Column Value
                            EarningAmt_2 += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal2"].ToString());
                            EarningAmt_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal2"].ToString());
                        }
                        if (ds.Tables[0].Rows[i]["EarningTotal3"].ToString() != "")
                        {
                            EarningAmt += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal3"].ToString()); // Column Value
                            EarningAmt_3 += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal3"].ToString());
                            EarningAmt_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["EarningTotal3"].ToString());
                        }

                        /********************/
                                                
                        if (ds.Tables[0].Rows[i]["ArrearPTaxDed"].ToString() != "")
                        {
                            PTax_Amt += Convert.ToDecimal(ds.Tables[0].Rows[i]["ArrearPTaxDed"].ToString()); // Column Value
                            PTax_Total_Arr += Convert.ToDecimal(ds.Tables[0].Rows[i]["ArrearPTaxDed"].ToString());
                            PTax_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["ArrearPTaxDed"].ToString());
                        }
                        if (ds.Tables[0].Rows[i]["PTaxDed1"].ToString() != "")
                        {
                            PTax_Amt += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed1"].ToString()); // Column Value
                            PTax_Total_1 += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed1"].ToString());
                            PTax_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed1"].ToString());
                        }
                        if (ds.Tables[0].Rows[i]["PTaxDed2"].ToString() != "")
                        {
                            PTax_Amt += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed2"].ToString()); // Column Value
                            PTax_Total_2 += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed2"].ToString());
                            PTax_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed2"].ToString());
                        }
                        if (ds.Tables[0].Rows[i]["PTaxDed3"].ToString() != "")
                        {
                            PTax_Amt += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed3"].ToString()); // Column Value
                            PTax_Total_3 += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed3"].ToString());
                            PTax_Total += Convert.ToDecimal(ds.Tables[0].Rows[i]["PTaxDed3"].ToString());
                        }
                        Label lblEarningTotal = (Label)row.FindControl("lblEarningTotal");
                        Label lblPTaxDedTotal = (Label)row.FindControl("lblPTaxDedTotal");
                        lblEarningTotal.Text = EarningAmt.ToString();
                        lblPTaxDedTotal.Text = PTax_Amt.ToString();
                        i++;
                    }
                    /*********************/


                    GridView1.FooterRow.Cells[0].Text = "<b>| TOTAL |</b>";
                    GridView1.FooterRow.Cells[4].Text = "<b>" + EarningAmt_Arr.ToString() + "</b>";
                    GridView1.FooterRow.Cells[5].Text = "<b>" + EarningAmt_1.ToString() + "</b>";
                    GridView1.FooterRow.Cells[6].Text = "<b>" + EarningAmt_2.ToString() + "</b>";
                    GridView1.FooterRow.Cells[7].Text = "<b>" + EarningAmt_3.ToString() + "</b>";
                    GridView1.FooterRow.Cells[8].Text = "<b>" + EarningAmt_Total.ToString() + "</b>";
                    GridView1.FooterRow.Cells[9].Text = "<b>" + PTax_Total_Arr.ToString() + "</b>";
                    GridView1.FooterRow.Cells[10].Text = "<b>" + PTax_Total_1.ToString() + "</b>";
                    GridView1.FooterRow.Cells[11].Text = "<b>" + PTax_Total_2.ToString() + "</b>";
                    GridView1.FooterRow.Cells[12].Text = "<b>" + PTax_Total_3.ToString() + "</b>";
                    GridView1.FooterRow.Cells[13].Text = "<b>" + PTax_Total.ToString() + "</b>";

                }
                else if (ds != null && ds.Tables[0].Rows.Count == 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;

            }
            else
            { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        Page.Validate("a");

        if (!Page.IsValid)
        {
            return;
        }
        else
        {
            lblMsg.Text = string.Empty;
            if (rbnlist.SelectedValue == "1")
            {
                GetITOrPT_Part1();
            }
            else
            {
                FillGrid();
            }
        }
       
       
        
    }

    protected void ddlOffice_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblDeductionDetails.Text = "";
            ddlFinancialYear.ClearSelection();
            ddlMonth.ClearSelection();
            FillEmptyGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEmptyGrid()
    {
        try
        {
            ds = objdb.ByProcedure("SpPayrollPolicyDetail",
                   new string[] { "flag" },
                   new string[] { "6" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else if (ds != null && ds.Tables[0].Rows.Count == 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblDeductionDetails.Text = "";
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                ds = null;
                ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "Year" }, new string[] { "2", ddlFinancialYear.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlEarnDeducHead.DataSource = ds;
                    ddlEarnDeducHead.DataTextField = "EarnDeduction_Name";
                    ddlEarnDeducHead.DataValueField = "EarnDeduction_ID";
                    ddlEarnDeducHead.DataBind();
                    ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlEarnDeducHead.Items.Clear();
                ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    static string getFullName(int month,int year)
    {
        DateTime date = new DateTime(year, month, 1);

        return date.ToString("MMMM");
    }

    private void GetITOrPT_Part1()
    {
        lblMsg.Text = "";
        string msg = "";
        int dropdownval = 0,i=1, j = 0;
       
            foreach (ListItem itemss in ddlMonthNew.Items)
            {
                
                if (itemss.Selected)
                {
                    dropdownval = 1;
                    if (i==0)
                    {
                        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning ! ", "Kinldy Select Quarter in Sequence");
                        return;
                    }
                    i++;
                }
                else
                {
                    if( dropdownval == 1)
                    {
                        if (i > 0)
                        {
                            i = 0;
                        }
                    }
                   
                }
            }
            if (dropdownval == 0)
            {
                lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning ! ", "Kindly select atleast one Quarter");
                return;
               
            }
            else 
            {
               
                DataSet ds1 = new DataSet();
                try
                {
                    int k = 0;
                    string[] actual_val;
                
                    string monthval="",monthtext = "",multimonthtext="";
                  

                    foreach (ListItem itemss in ddlMonthNew.Items)
                    {
                        k++;
                        monthtext = itemss.Text;
                        monthval = itemss.Value;
                        if (itemss.Selected)
                        {
                            ++k;
                            if (k == 1)
                            {
                                multimonthtext = monthval;

                            }
                            else
                            {
                                multimonthtext += "," + monthval;

                            }
                        }
                    }
                    actual_val = multimonthtext.Split(',');
                    string smonth = "";
                    foreach (string item in actual_val)
                    {
                        string mno = "";
                        switch(item)
                        {
                            case "1":
                                mno = "1,2,3,";
                                break;
                            case "2":
                                mno = "4,5,6,";
                                break;
                            case "3":
                                mno = "7,8,9,";
                                break;
                            case "4":
                                mno = "10,11,12,";
                                break;
                        }
                        smonth += mno;
                    }
                    string allmonthno = smonth.Substring(0, smonth.Length - 1);
                    string[] acutalmonthno = allmonthno.Split(',');
                    string firstElement = acutalmonthno[0];
                    string lastElement = acutalmonthno[acutalmonthno.Length - 1];
                    string fmy = getFullName(Convert.ToInt32(firstElement), Convert.ToInt32(ddlFinancialYear.SelectedValue)) + " " + ddlFinancialYear.SelectedValue;
                    string tmy = getFullName(Convert.ToInt32(lastElement), Convert.ToInt32(ddlFinancialYear.SelectedValue)) + " " + ddlFinancialYear.SelectedValue;
                   
                    
                    ds1 = objdb.ByProcedure("USP_Payroll_ITOrPT_Report",
                            new string[] { "Office_ID", "smonth", "syear", "EarnDeduction_ID" },
                            new string[] { ddlOffice_Name.SelectedValue, allmonthno, ddlFinancialYear.SelectedValue, ddlEarnDeducHead.SelectedValue }, "dataset");

                    if (ds1 != null)
                    {
                        if (ds1.Tables.Count != 0)
                        {
                            if (ds1.Tables[0].Rows.Count != 0)
                            {
                                HideShow("n");
                                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                                ReportDataSource datasource = new ReportDataSource();
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("Rpt_Quaterly_IT_Or_PT_Part1.rdlc");
                                datasource = new ReportDataSource("dtQuterlyITOrPT", ds1.Tables[0]);

                                ReportParameter HeaderVal1, MonthName, ColumnName;

                                ColumnName = new ReportParameter("ColumnName", ddlEarnDeducHead.SelectedItem.Text);
                                if (ddlOffice_Name.SelectedValue == "81")
                                {
                                    HeaderVal1 = new ReportParameter("HeaderVal1", "Minor Forest Produce-Processing and Research Centre (MFP-PARC)");
                                    MonthName = new ReportParameter("MonthName", fmy + " TO " + tmy);


                                }
                                else
                                {
                                    HeaderVal1 = new ReportParameter("HeaderVal1", "SFA Technologies Pvt. Ltd.");
                                    MonthName = new ReportParameter("MonthName", fmy + " TO " + tmy);

                                }
                                ReportViewer1.LocalReport.DataSources.Clear();
                                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { HeaderVal1, MonthName, ColumnName });
                                ReportViewer1.LocalReport.DataSources.Add(datasource);
                                ReportViewer1.LocalReport.Refresh();

                                //START PART 2 REPORT FOR EXPORT EXCEL
                                ReportViewer2.ProcessingMode = ProcessingMode.Local;
                                ReportDataSource datasource2 = new ReportDataSource();
                                ReportViewer2.LocalReport.ReportPath = Server.MapPath("Rpt_Quaterly_IT_Or_PT_Part2.rdlc");
                                datasource2 = new ReportDataSource("dtQuterlyITOrPT2", ds1.Tables[0]);

                                ReportViewer2.LocalReport.DataSources.Clear();
                                ReportViewer2.LocalReport.SetParameters(new ReportParameter[] { HeaderVal1, MonthName, ColumnName });
                                ReportViewer2.LocalReport.DataSources.Add(datasource2);
                                ReportViewer2.LocalReport.Refresh();
                                // END OF PART 2 REPORT FOR EXPORT EXCEL
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning ! ", "No Record Found");
                                return;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
                }
                finally
                {
                    if (ds1 != null)
                    {
                        ds1.Dispose();
                    }
                }
            }
         
    }

    private void HideShow(string val)
    {
        switch(val)
        {
           
            case "n":
                pnlITOrPT_Part1.Visible = true;
                lnkExport.Visible = true;
                pnlgrid.Visible = false;
                pnloldmonth.Visible = false;
                lblDeductionDetails.Text = string.Empty;
                break;

            case "o":
                pnlITOrPT_Part1.Visible = false;
                lnkExport.Visible = false;
                pnlgrid.Visible = true;
                pnloldmonth.Visible = true;
                break;

            case "old":
                pnlITOrPT_Part1.Visible = false;
                lnkExport.Visible = false;
                pnlgrid.Visible = false;
                pnloldmonth.Visible = true;
                pnlnewmonth.Visible = false;
                lblDeductionDetails.Text = string.Empty;
                break;

            default:
                pnlnewmonth.Visible = true;
                pnlITOrPT_Part1.Visible = false;
                lnkExport.Visible = false;
                pnlgrid.Visible = false;
                pnloldmonth.Visible = false;
                lblDeductionDetails.Text = string.Empty;
                break;
        }
    }
    protected void rbnlist_SelectedIndexChanged(object sender, EventArgs e)
    { 
        if(rbnlist.SelectedValue=="1")
        {
            HideShow("default");
        }
        else
        {
            HideShow("old");
        }
       
    }
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        Warning[] warnings;
        string[] streamIds;
        string contentType;
        string encoding;
        string extension;

        //Export the RDLC Report to Byte Array.
        //byte[] bytes = ReportViewer1.LocalReport.Render(rbFormat.SelectedItem.Value, null, out contentType, out encoding, out extension, out streamIds, out warnings);
        byte[] bytes = ReportViewer2.LocalReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);
        //Download the RDLC Report in Word, Excel, PDF and Image formats.
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + ddlFinancialYear.SelectedValue + "_" + ddlEarnDeducHead.SelectedItem.Text + DateTime.Now.ToString() + "." + extension);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
}