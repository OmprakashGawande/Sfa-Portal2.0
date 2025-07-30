using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Report_RptEmpWiseTaskDetail : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds, ds1;
    CultureInfo cult = new CultureInfo("gu-IN", true);
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["UserTypeId"] = Session["UserTypeId"].ToString();
                ViewState["Designation_ID"] = Session["Designation_ID"].ToString();
                BindDropdown();

                div2.Visible = false;
             
                ddlEmp.Enabled = true;
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }


    public void BindDropdown()
    {
        try
        {
            string empId = "0";
            string LoggedInRole = "";
            if (ViewState["Designation_ID"].ToString() == "1")
            {
                LoggedInRole = "Admin";
                RequiredFieldValidator1.Enabled = false;
            }
            else if (ViewState["Designation_ID"].ToString() == "8" || ViewState["Designation_ID"].ToString() == "13")
            {
                LoggedInRole = "Manager";
                empId = ViewState["Emp_ID"].ToString();
            }
            else
            {
                empId = ViewState["Emp_ID"].ToString();
            }

            DataSet ds3 = objdb.ByProcedure("UspGetEmpForDailyReport", new string[] { "EmpId", "LoggedInRole" }, new string[] { empId, LoggedInRole }, "dataset");

            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {

                ddlEmp.DataSource = ds3.Tables[0];
                ddlEmp.DataTextField = "Emp_Name";
                ddlEmp.DataValueField = "Emp_ID";
                ddlEmp.DataBind();


            }
            if (ds3.Tables[0].Rows.Count == 1)
            {
                ddlEmp.SelectedIndex = 1;
                ddlEmp.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlEmp.Items.Insert(0, new ListItem("ALL", "0"));
            }

        }
        catch (Exception ex)
        {
            // Optional: log or show error
            throw new Exception("Error while binding Type of Project dropdown: " + ex.Message);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            //GridView.DataSource = null;
            //GridView.DataBind();

            //div2.Visible = true;

            //string EmpID = "0";


            //if (ddlEmp.SelectedValue != "0")
            //{
            //    EmpID = ddlEmp.SelectedValue;
            //}



            //CultureInfo cult = new CultureInfo("en-GB");

            //DateTime fromDateVal;
            //DateTime toDateVal;

            //string FromDate = !string.IsNullOrWhiteSpace(txtFromDate.Text) && DateTime.TryParse(txtFromDate.Text, cult, DateTimeStyles.None, out fromDateVal)
            //    ? fromDateVal.ToString("dd/MM/yyyy")
            //    : "";

            //string ToDate = !string.IsNullOrWhiteSpace(txtToDate.Text) && DateTime.TryParse(txtToDate.Text, cult, DateTimeStyles.None, out toDateVal)
            //    ? toDateVal.ToString("dd/MM/yyyy")
            //    : "";
            //DataSet ds = objdb.ByProcedure("Usp_GetEmpWiseTaskReport", new string[] { "EmpId", "FromDate", "Todate" }, new string[] { EmpID, FromDate, ToDate }, "dataset");

            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    GridView.DataSource = ds.Tables[0];
            //    GridView.DataBind();
            //}


            //div2.Visible = true;
            BindGridData();


        }
        catch (Exception ex) { throw ex; }

    }
    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatus = (Label)e.Row.FindControl("lblTaskStatus");
            if (lblStatus != null)
            {
                string status = lblStatus.Text.Trim().ToLower();

                // Reset class
                lblStatus.CssClass = "btn btn-sm ";

                if (status.Contains("complete"))
                {
                    lblStatus.CssClass += "btn-success text-white"; // Green
                }
                else if (status.Contains("pending"))
                {
                    lblStatus.CssClass += "btn-danger text-white"; // Red
                }
                else if (status.Contains("working"))
                {
                    lblStatus.CssClass += "btn-warning text-dark"; // Yellow
                }
                else if (status.Contains("not"))
                {
                    lblStatus.CssClass += "btn-primary text-white"; // Gray for missing
                }
                else
                {
                    lblStatus.CssClass += "btn-dark text-white"; // Unknown fallback
                }
            }
        }
    }


    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            GridView.AllowPaging = false; // disable paging for full data
            BindGridData(); // reuse search logic

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=EmployeeTaskDetails.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            // Excel style for forcing text
            Response.Write("<style> .textmode { mso-number-format:\\@; } </style>");

            // Add custom top rows
            Response.Write("<table border='1'>");
            // 1. Title row
            Response.Write("<tr><td colspan='12' style='font-size:18px; font-weight:bold; text-align:center;'>Employee Task Detail</td></tr>");

            // 2. Export datetime row
            string timestamp = DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
            Response.Write("<tr><td colspan='12' style='font-size:12px; font-style:italic; text-align:center;'>Exported on: " + timestamp + "</td></tr>");

            // Start writing GridView content in next row
            using (StringWriter sw = new StringWriter())
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                PrepareGridViewForExport(GridView);
                GridView.RenderControl(hw);
                Response.Write(sw.ToString());
            }

            Response.Write("</table>");
            Response.Flush();
            Response.End();
        }
      
        catch (Exception ex)
        {
            // Log if necessary
            throw;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for export to work properly
    }
    private void PrepareGridViewForExport(Control gv)
    {
        for (int i = 0; i < gv.Controls.Count; i++)
        {
            Control ctrl = gv.Controls[i];

            if (ctrl is LinkButton)
            {
                LinkButton lnk = (LinkButton)ctrl;
                Literal literal = new Literal();
                literal.Text = lnk.Text;

                gv.Controls.Remove(ctrl);
                gv.Controls.AddAt(i, literal);
            }
            else if (ctrl.HasControls())
            {
                PrepareGridViewForExport(ctrl);
            }
        }
    }


    // Common method for both search and export
    private void BindGridData()
    {
        try
        {
            GridView.DataSource = null;
            GridView.DataBind();
            div2.Visible = true;

            string empId = ddlEmp.SelectedValue != "0" ? ddlEmp.SelectedValue : "0";

            CultureInfo cult = new CultureInfo("en-GB");
            DateTime fromDateVal, toDateVal;

            string fromDate = DateTime.TryParse(txtFromDate.Text, cult, DateTimeStyles.None, out fromDateVal)
                ? fromDateVal.ToString("dd/MM/yyyy")
                : "";

            string toDate = DateTime.TryParse(txtToDate.Text, cult, DateTimeStyles.None, out toDateVal)
                ? toDateVal.ToString("dd/MM/yyyy")
                : "";

            DataSet ds = objdb.ByProcedure("Usp_GetEmpWiseTaskReport",
                new string[] { "EmpId", "FromDate", "Todate" },
                new string[] { empId, fromDate, toDate },
                "dataset");

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView.DataSource = ds.Tables[0];
                GridView.DataBind();
                dvexportbtn.Visible = true;
            }
            else
            {
                GridView.DataSource = null;
                GridView.DataBind();
                dvexportbtn.Visible = false;



            }
            div2.Visible = true;
        }
        catch (Exception ex)
        {
            // Ideally log the error
            throw;
        }
    }



}