using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class mis_Report_DailyTaskReport : System.Web.UI.Page
{

    APIProcedure objdb = new APIProcedure();
    DataSet ds, ds1;
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {



            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected string GetStatusCss(string status)
    {
        switch (status.ToLower())
        {
            case "task not filled":
                return "btn btn-danger btn-sm";  // Red
            case "on leave":
                return "btn btn-warning btn-sm"; // Yellow
            default:
                return "btn btn-success btn-sm"; // Optional fallback
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        BindReport();

    }

    private void BindReport()
    {
        // Convert date to string in the required format (e.g., 'yyyy-MM-dd')

        DateTime DateVal;

        string FromDate = !string.IsNullOrWhiteSpace(txtDate.Text) && DateTime.TryParse(txtDate.Text, cult, DateTimeStyles.None, out DateVal)
            ? DateVal.ToString("yyyy/MM/dd ")
            : "";
        // Call the stored procedure using objdb.ByProcedure (assumed signature)
        DataSet ds = objdb.ByProcedure("Usp_GetDailyTaskReportByDate",
            new string[] { "Date" },                     // parameter names
            new string[] { FromDate },                     // parameter values
            "dataset");

        if (ds != null && ds.Tables.Count > 0)
        {
            // Bind Filled Tasks (first result set)
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvFilledTasks.DataSource = ds.Tables[0];
                gvFilledTasks.DataBind();
            }
            else
            {
                gvFilledTasks.DataSource = null;
                gvFilledTasks.DataBind();
            }

            // Bind Not Filled Tasks (second result set)
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                gvNotFilledTasks.DataSource = ds.Tables[1];
                gvNotFilledTasks.DataBind();
            }
            else
            {
                gvNotFilledTasks.DataSource = null;
                gvNotFilledTasks.DataBind();
            }
            // Bind Project Wise Detail 
            if (ds.Tables.Count > 1 && ds.Tables[2].Rows.Count > 0)
            {
                GridProjectwise.DataSource = ds.Tables[2];
                GridProjectwise.DataBind();
            }
            else
            {
                GridProjectwise.DataSource = null;
                GridProjectwise.DataBind();
            }
        }
        else
        {
            gvFilledTasks.DataSource = null;
            gvFilledTasks.DataBind();
            gvNotFilledTasks.DataSource = null;
            gvNotFilledTasks.DataBind();
            GridProjectwise.DataSource = null;
            GridProjectwise.DataBind();
        }
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtDate.Text))
        {
            lblSelectedDate.Text = "Date: " + txtDate.Text;
            Label1.Text = "Date: " + txtDate.Text;
            Label2.Text = "Date: " + txtDate.Text;
            gvFilledTasks.DataSource = null;
            gvFilledTasks.DataBind();

            gvNotFilledTasks.DataSource = null;
            gvNotFilledTasks.DataBind();
            GridProjectwise.DataSource = null;
            GridProjectwise.DataBind();

        }
        else
        {
            lblSelectedDate.Text = "";
        }
    }


    protected void gvFilledTasks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            if (lblSerial != null)
            {
                lblSerial.Text = (e.Row.RowIndex + 1).ToString();
            }
        }

    }

    protected void gvNotFilledTasks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            if (lblSerial != null)
            {
                lblSerial.Text = (e.Row.RowIndex + 1).ToString();
            }
        }
    }

    protected void GridProjectwise_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            if (lblSerial != null)
            {
                lblSerial.Text = (e.Row.RowIndex + 1).ToString();
            }
        }
    }
}
