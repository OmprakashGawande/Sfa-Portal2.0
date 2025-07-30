using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_HR_HRDateRangeWiseAttendenceRpt_New_Salary : System.Web.UI.Page
{
    DataSet ds, ds2, ds3, ds4;
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
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillOffice();
                    txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
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
            ds = objdb.ByProcedure("SpHRRptDailyAttendance", new string[] { "flag" }, new string[] { "4" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                if (ViewState["Office_ID"].ToString() != "1")
                {
                    ddlOffice.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void Fillgrid()
    {
        try
        {
            lblMsg.Text = "";

            string FROMDATE = Convert.ToDateTime(txtStartDate.Text, cult).ToString("yyyy-MM-dd");
            string TODATE = Convert.ToDateTime(txtEndDate.Text, cult).ToString("yyyy-MM-dd");

            ds = objdb.ByProcedure("SpHRPayrollAttendance",
               new string[] { "flag", "startDate", "endDate", "Office_ID", "TypeOfPost" },
               new string[] { "0", FROMDATE, TODATE, ddlOffice.SelectedValue.ToString(),ddlEmployeeType.SelectedValue.ToString()}, "dataset");
            string OfficialWorkingDays = "";
            OfficialWorkingDays += "<b style='font-size:14px; margin-right:20px;'><span style='color:blue; font-weight:bold;'> From Date </span>&nbsp;&nbsp;: &nbsp;&nbsp;" + txtStartDate.Text.ToString() + "</b>";
            OfficialWorkingDays += "<b style='font-size:14px; margin-right:20px;'><span style='color:blue; font-weight:bold;'> To Date </span>&nbsp;&nbsp;: &nbsp;&nbsp;" + txtEndDate.Text.ToString() + "</b>";
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
               
                OfficialWorkingDays += "<b style='font-size:14px; margin-right:20px;'><span style='color:blue; font-weight:bold;'> Report Name ";
                OfficialWorkingDays += "</span>&nbsp;&nbsp;:&nbsp;&nbsp;";
                OfficialWorkingDays += "Attendance For Payroll( "+ddlEmployeeType.SelectedItem.Text.ToString()+" )";
                OfficialWorkingDays += "</b>";
                OfficialWorkingDays += "<b style='font-size:14px; margin-right:20px;'><span style='color:blue; font-weight:bold;'> Time ";
                OfficialWorkingDays += "</span>&nbsp;&nbsp;:&nbsp;&nbsp;";
                OfficialWorkingDays += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                OfficialWorkingDays += "</b>";
                lblMsg.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();

            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
            }
            lbltotalworkingdays.Text = OfficialWorkingDays;
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    private void Fillgrid2()
    {
        try
        {
            string FROMDATE = Convert.ToDateTime(txtStartDate.Text, cult).ToString("yyyy-MM-dd");
            string TODATE = Convert.ToDateTime(txtEndDate.Text, cult).ToString("yyyy-MM-dd");

            ds = objdb.ByProcedure("SpHRPayrollAttendanceShort",
               new string[] { "flag", "startDate", "endDate", "Office_ID", "TypeOfPost" },
               new string[] { "0", FROMDATE, TODATE, ddlOffice.SelectedValue.ToString(), ddlEmployeeType.SelectedValue.ToString() }, "dataset");
            
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {

                GridView2.DataSource = ds.Tables[1];
                GridView2.DataBind();

            }
            else
            {
                GridView2.DataSource = new string[] { };
                GridView2.DataBind();
            }
            GridView2.UseAccessibleHeader = true;
            GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            //if (Rbtn_Type1.SelectedIndex > -1 || Rbtn_Type2.SelectedIndex > -1)
            //{
            Fillgrid();
            Fillgrid2();
            //}

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}