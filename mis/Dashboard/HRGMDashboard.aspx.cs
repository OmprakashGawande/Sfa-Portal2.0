using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;

public partial class mis_Dashboard_HRGMDashboard : System.Web.UI.Page
{
    DataSet ds, dsRecord;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ds = objdb.ByProcedure("SpHRGMDashboard", new string[] { "flag" }, new string[] { "1" }, "dataset");
                if (ds.Tables.Count != 0)
                {
                    TotalEmp.InnerText = ds.Tables[0].Rows[0]["TotalEmp"].ToString();
                    spnEmpCount.InnerText = ds.Tables[0].Rows[0]["TotalEmp"].ToString();
                    TotalOffice.InnerText = ds.Tables[1].Rows[0]["TotalOffice"].ToString();
                    spnOfficeCount.InnerText = ds.Tables[1].Rows[0]["TotalOffice"].ToString();
                    TodayOnLeave.InnerText = ds.Tables[2].Rows[0]["TodayOnLeave"].ToString();
                    spnTodayOnLeave.InnerText = ds.Tables[2].Rows[0]["TodayOnLeave"].ToString();
                    LastMonthTransfer.InnerText = ds.Tables[3].Rows[0]["Transfer30days"].ToString();
                    spnLastMonthTransfer.InnerText = ds.Tables[3].Rows[0]["Transfer30days"].ToString();
                    PendingReleiving.InnerText = ds.Tables[4].Rows[0]["PendingTransfer"].ToString();
                    spnPendingReleiving.InnerText = ds.Tables[4].Rows[0]["PendingTransfer"].ToString();
                    PendingJoining.InnerText = ds.Tables[5].Rows[0]["PendingJoining"].ToString();
                    spnPendingJoining.InnerText = ds.Tables[5].Rows[0]["PendingJoining"].ToString();
                    TotalEmployee.InnerText = ds.Tables[0].Rows[0]["TotalEmp"].ToString();
                    spnTotalEmployee.InnerText = ds.Tables[0].Rows[0]["TotalEmp"].ToString();
                    BirthdayThisMonth.InnerText = ds.Tables[6].Rows[0]["BirthdayThisMonth"].ToString();
                    spnBirthdayThisMonth.InnerText = ds.Tables[6].Rows[0]["BirthdayThisMonth"].ToString();
                }
                ds = objdb.ByProcedure("SpHRGMDashboard", new string[] { "flag", "Emp_ID" }, new string[] { "7", ViewState["Emp_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0)
                {
                    LeaveRequest.InnerText = ds.Tables[0].Rows[0]["LeaveRequest"].ToString();
                    spnLeaveRequest.InnerText = ds.Tables[0].Rows[0]["LeaveRequest"].ToString();
                }
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillDetail()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            ds = objdb.ByProcedure("SpHRDashboard", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}