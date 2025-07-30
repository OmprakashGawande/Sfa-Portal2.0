using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_Report_RptTaskAllocationStatics : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds, ds1;
    //CultureInfo cult = new CultureInfo("gu-IN", true);
    CultureInfo cult = new CultureInfo("en-GB", true);

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
                txtDate.Text = DateTime.Now.ToString();
                GetTaskAllocationData();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    protected void GetTaskAllocationData()
    {
        try
        {
            Grid.DataSource = null;
            Grid.DataBind();
            string EmpID = ViewState["Emp_ID"].ToString();

            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            DataSet ds = objdb.ByProcedure("Usp_GetTaskAllocationSummary", new string[] { "EmpId", "FilterDate" }, new string[] { EmpID, currentDate, }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "";
                Grid.DataSource = ds.Tables[0];  // Privious week grid 
                Grid.DataBind();
                Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                Grid.UseAccessibleHeader = true;
                Datagrid.Visible = true;
            }
            else
            {
                Grid.DataSource = null;
                Grid.DataBind();
                Datagrid.Visible = false;
                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", "Warning ! " + "No Record Found");
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", "Warning ! " + ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Grid.DataSource = null;
            Grid.DataBind();
            string EmpID = ViewState["Emp_ID"].ToString(); ;
            DateTime DateVal;

            string FromDate = !string.IsNullOrWhiteSpace(txtDate.Text) && DateTime.TryParse(txtDate.Text, cult, DateTimeStyles.None, out DateVal)
                ? DateVal.ToString("yyyy/MM/dd ")
                : "";
            DataSet ds = objdb.ByProcedure("Usp_GetTaskAllocationSummary", new string[] { "EmpId", "FilterDate" }, new string[] { EmpID, FromDate, }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "";
                Grid.DataSource = ds.Tables[0];  // Privious week grid 
                Grid.DataBind();
                Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                Grid.UseAccessibleHeader = true;
                Datagrid.Visible = true;
            }
            else
            {
                Grid.DataSource = null;
                Grid.DataBind();
                Datagrid.Visible = false;
                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", "Warning ! " + "No Record Found");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
        }
    }


    protected void Grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string[] args = e.CommandArgument.ToString().Split(';');
            int projectId = args.Length > 0 ? Convert.ToInt32(args[0]) : 0;
            int empId = args.Length > 1 ? Convert.ToInt32(args[1]) : 0;
            string role = args.Length > 2 ? args[2] : string.Empty;
            int assignedToEmpId = (args.Length > 3 && !string.IsNullOrEmpty(args[3])) ? Convert.ToInt32(args[3]) : 0;

            ViewState["ProjectId"] = projectId;
            ViewState["CreatedByEmpId"] = empId;
            ViewState["UserRole"] = role;
            ViewState["AssignedToEmpId"] = assignedToEmpId;

            switch (e.CommandName)
            {
                case "ViewPendingTasks":
                    LoadTaskStatus(3); // Pending
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#taskModal').modal('show');", true);
                    break;

                case "ViewCompleteTasks":
                    LoadTaskStatus(2); // Completed
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#taskModal').modal('show');", true);
                    break;

                case "ViewWipTasks":
                    LoadTaskStatus(1); // Work in Progress
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal').modal('show'); setupDataTable();", true);
                    break;
            }
        }
        catch (Exception ex)
        {
            // Optionally log error or show message
            lblMsg.Text = "Error: " + ex.Message;
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
    }


    private void LoadTaskStatus(int status)
    {
        int createdByEmpId = Convert.ToInt32(ViewState["CreatedByEmpId"]);
        int assignedToEmpId = Convert.ToInt32(ViewState["AssignedToEmpId"]);
        int projectId = Convert.ToInt32(ViewState["ProjectId"]);
        string role = ViewState["UserRole"].ToString();


        DateTime DateVal;

        string FromDate = !string.IsNullOrWhiteSpace(txtDate.Text) && DateTime.TryParse(txtDate.Text, cult, DateTimeStyles.None, out DateVal)
            ? DateVal.ToString("yyyy/MM/dd ")
            : "";

        DataSet ds = objdb.ByProcedure("Usp_GetTaskStatusReport",
            new string[] { "EmpId", "ProjectId", "Status", "Role", "AssignedToEmpId", "FilterDate" },
            new string[] { createdByEmpId.ToString(), projectId.ToString(), status.ToString(), role, assignedToEmpId.ToString(), FromDate },
            "dataset");

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            GridTaskDetail.DataSource = ds.Tables[0];
            GridTaskDetail.DataBind();
            GridTaskDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridTaskDetail.UseAccessibleHeader = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup",
                "$('#exampleModal').modal('show'); setupDataTable();", true);
        }
        else
        {
            GridTaskDetail.DataSource = null;
            GridTaskDetail.DataBind();
            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "No Record Found");
        }
    }
    protected void GridTaskDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatus = (Label)e.Row.FindControl("lblTaskStatusText");
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


}