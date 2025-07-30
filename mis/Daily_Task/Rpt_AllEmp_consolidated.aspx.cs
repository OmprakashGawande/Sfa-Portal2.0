using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Drawing;
using System.IO;
using System.Net;
using Microsoft.Reporting.WebForms;

public partial class mis_Daily_Task_Rpt_AllEmp_consolidated : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds, ds1, ds5, dsC, dsCU, dsphad;
    static DataTable dt = new DataTable();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    IFormatProvider culture = new CultureInfo("en-US", true);
    string[] arr = { "1", "2", "3", "4", "5", "59" };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!IsPostBack)
            {
                //gridvew1.DataSource = null;
                //gridvew1.DataBind();
                lblMsg.Text = "";
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Attributes.Add("readonly", "readonly");

                DateTime dd = DateTime.Now;
                txtFromDate.Text = (Convert.ToDateTime(dd, culture).ToString("dd/MM/yyyy"));
                txtToDate.Text = (Convert.ToDateTime(dd, culture).ToString("dd/MM/yyyy"));
                fillEmp();
                if (arr.Contains(Session["Emp_ID"].ToString()))
                {

                }
                else
                {
                    if (ddlEmp.Items.FindByValue(Session["Emp_ID"].ToString()) != null)
                    {
                        ddlEmp.Items.FindByValue(Session["Emp_ID"].ToString()).Selected = true;
                    }
                    ddlEmp.Enabled = false;
                }
            }
        }
    }
    protected void fillEmp()
    {
        try
        {
            ddlEmp.Items.Clear();
            ds1 = objdb.ByProcedure("USP_Daily_Task_GetAllEmp", new string[] { }, new string[] { }, "dataset");
            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlEmp.DataValueField = "Emp_ID";
                        ddlEmp.DataTextField = "Emp_Name";
                        ddlEmp.DataSource = ds1;
                        ddlEmp.DataBind();
                    }
                }
            }
            if (ddlEmp.Items.Count > 0)
            {
                ddlEmp.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlEmp.Items.Insert(0, new ListItem("No Record Found", "-1"));
            }
            if (ds1 != null) ds1.Dispose();

        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7: " + ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (arr.Contains(Session["Emp_ID"].ToString()))
                {

                }
                else
                {
                    if (ddlEmp.Items.FindByValue(Session["Emp_ID"].ToString()) != null)
                    {
                        ddlEmp.Items.FindByValue(Session["Emp_ID"].ToString()).Selected = true;
                    }
                    ddlEmp.Enabled = false;
                }
                DateTime DATE1 = Convert.ToDateTime(txtFromDate.Text.Trim(), cult);
                DateTime DATE2 = Convert.ToDateTime(txtToDate.Text.Trim(), cult);
                int res = DateTime.Compare(DATE1, DATE1);
                if (DATE1.Month != DATE2.Month)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert(' \\n PLEASE SELECT BOTH DATES IN THE SAME MONTH.')", true);
                }
                else if (DATE1 <= DATE2)
                {
                    lblMsg.Text = "";
                    //gridvew1.DataSource = null;
                    //gridvew1.DataBind();
                    //if (ddlEmp.SelectedValue != "0")
                    //{
                    empConsolidatedReport();
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert(' \\n SELECT EMPLOYEE NAME')", true);
                    //}
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert(' \\n TO DATE SHOULD BE GREATER THAN FROM DATE.')", true);
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", ex.Message.ToString());
        }
    }


    private void empConsolidatedReport()
    {

        ReportViewer1.Visible = false;
        string fromdate = Convert.ToDateTime(txtFromDate.Text.Trim(), cult).ToString("yyyy/MM/dd");
        string todate = Convert.ToDateTime(txtToDate.Text.Trim(), cult).ToString("yyyy/MM/dd");
        ds = objdb.ByProcedure("USP_Daily_Task_RPT_AllEmp_consolidated_DateWise", new string[] { "FromDate", "ToDate", "EmpId" }, new string[] { fromdate, todate, ddlEmp.SelectedValue }, "dataset");
        //btnExport.Visible = false;
        try
        {


            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        // Process the dataset to replace <br/> with Environment.NewLine

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            row["Work_Description"] = row["Work_Description"].ToString().Replace("<br />", Environment.NewLine);
                        }

                        ReportViewer1.Visible = true;
                        //btnExport.Visible = true;
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("Rdl_Emp_Consolidated_Rpt.rdlc");
                        ReportDataSource datasource = new ReportDataSource("Emp_Consolidated", ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(datasource);
                        ReportViewer1.LocalReport.Refresh();
                        this.Response.Cache.SetNoStore();
                        this.Response.Expires = 0;
                        this.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                        this.Response.AddHeader("pragma", "no-cache");
                        this.Response.AddHeader("cache-control", "private");
                        this.Response.CacheControl = "no-cache";
                    }
                }
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7: " + ex.Message.ToString());
        }
        finally
        {
            ds.Dispose();
        }

    }

}