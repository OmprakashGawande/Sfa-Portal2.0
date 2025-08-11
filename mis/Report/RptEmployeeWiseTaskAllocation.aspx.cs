using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class mis_Report_RptEmployeeWiseTaskAllocation : System.Web.UI.Page
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
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["UserTypeId"] = Session["UserTypeId"].ToString();
                ViewState["Designation_ID"] = Session["Designation_ID"].ToString();
                BindDropdown();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void Grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            Grid.UseAccessibleHeader = true;
            string[] args = e.CommandArgument.ToString().Split(';');
            int projectId = args.Length > 0 ? Convert.ToInt32(args[0]) : 0;
            int empId = args.Length > 1 ? Convert.ToInt32(args[1]) : 0;

            ViewState["ProjectId"] = projectId;
            ViewState["CreatedByEmpId"] = empId;


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
        int projectId = Convert.ToInt32(ViewState["ProjectId"]);

        DataSet ds = objdb.ByProcedure("Usp_GetEmpWiseTaskAllocationList",
            new string[] { "EmpId", "ProjectId", "Status" },
            new string[] { createdByEmpId.ToString(), projectId.ToString(), status.ToString() },
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
                else if (status.Contains("work"))
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
    public void BindDropdown()
    {
        try
        {
            string LoggedInRole = "";

            string empId = ViewState["Emp_ID"].ToString();

            DataSet ds3 = objdb.ByProcedure("UspGetEmpForDailyReport", new string[] { "EmpId", "LoggedInRole" }, new string[] { empId, LoggedInRole }, "dataset");
            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                ddlEmp.DataSource = ds3.Tables[0];
                ddlEmp.DataTextField = "Emp_Name";
                ddlEmp.DataValueField = "Emp_ID";
                ddlEmp.DataBind();
            }
            if (ds3.Tables[1].Rows[0]["Status"].ToString() == "Admin")
            {
                ddlEmp.Items.Insert(0, new ListItem("ALL", "0"));
                RFV1.Enabled = false;

            }
            else
            {
                //ddlEmp.SelectedIndex = 0;
                ddlEmp.Items.Insert(0, new ListItem("Select", "0"));
                RFV1.Enabled = true;

            }

        }
        catch (Exception ex)
        {
            // Optional: log or show error
            throw new Exception("Error while binding Type of Employee dropdown: " + ex.Message);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Grid.DataSource = null;
            Grid.DataBind();
            CultureInfo cult = new CultureInfo("en-GB");
            DateTime fromDateVal, toDateVal;
            string fromDate = DateTime.TryParse(txtFromDate.Text, cult, DateTimeStyles.None, out fromDateVal)
              ? fromDateVal.ToString("dd/MM/yyyy")
              : "";

            string toDate = DateTime.TryParse(txtToDate.Text, cult, DateTimeStyles.None, out toDateVal)
                ? toDateVal.ToString("dd/MM/yyyy")
                : "";

            if (toDate == "") 
            {
                toDate = null;
            }

            if (fromDate == "")
            {
                fromDate = null;
            }
            DataSet ds = objdb.ByProcedure("Usp_GetTaskAllocationDetailEmpWise", new string[] { "EmpId", "FromDate", "ToDate" }, new string[] { ddlEmp.SelectedValue, fromDate ,toDate}, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = "";
                Grid.DataSource = ds.Tables[0];
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
}