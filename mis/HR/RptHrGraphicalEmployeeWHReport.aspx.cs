using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_HR_RptHrGraphicalEmployeeWHReport : System.Web.UI.Page
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
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["OfficeID"] = Session["Office_ID"].ToString();
                    string CurrentDate = DateTime.Now.ToString("dd/MM/yyy");
                    txtFromDate.Text = CurrentDate.ToString();
                    txtToDate.Text = CurrentDate.ToString();
                    FillChart();
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
    protected void FillChart()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpHrDailyAttendenceGraphicalReport", new string[] { "flag", "Office_ID", "FromDate","ToDate" }, new string[] { "4", ViewState["OfficeID"].ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<table id='datatable'>");
                sb.Append("<thead'>");
                sb.Append("<tr>");
                sb.Append("<th></th>");
                sb.Append("<th>Employees</th>");
                sb.Append("</tr>");
                sb.Append("</thead'>");
                sb.Append("<tbody>");
                int count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<th>" + ds.Tables[0].Rows[i]["WorkingHour"].ToString() + "</th>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["TEmployee"].ToString() + "</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody>");
                sb.Append("</table>");
                divchart.InnerHtml = sb.ToString();
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();


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
            FillChart();
            
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
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow gvRow = GridView1.Rows[index];
            if (e.CommandName == "View")
            {
                Label lblStartHour = (Label)gvRow.FindControl("lblStartHour");
                string StartHour = lblStartHour.Text;
                Label lblEndHour = (Label)gvRow.FindControl("lblEndHour");
                string EndHour = lblEndHour.Text;
                ds = objdb.ByProcedure("SpHrDailyAttendenceGraphicalReport", new string[] { "flag", "Office_ID", "FromDate","ToDate", "StartHour", "EndHour" }, new string[] { "5", ViewState["OfficeID"].ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"),Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), StartHour, EndHour }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GVEmployee.DataSource = ds.Tables[0];
                    GVEmployee.DataBind();
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ModalView();", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}