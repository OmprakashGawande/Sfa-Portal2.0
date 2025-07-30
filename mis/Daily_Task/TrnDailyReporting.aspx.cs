using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Microsoft.Reporting.WebForms;
public partial class mis_Daily_Task_TrnDailyReporting : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = "";
                ViewState["Office_ID"] = "";
                ViewState["UserTypeId"] = "";

                ViewState["EmpId"] = Session["Emp_ID"].ToString();
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["UserTypeId"] = Session["UserTypeId"].ToString();
                //DivBtn.Visible = false;
                txtDate.Enabled = false;
                txtDate.Attributes.Add("readonly", "readonly");

                rdProjectName.Enabled = false;
                rdProjectName.Attributes.Add("readonly", "readonly");


                txtFromDate.Enabled = false;
                txtFromDate.Attributes.Add("readonly", "readonly");


                txtTodate.Enabled = false;
                txtTodate.Attributes.Add("readonly", "readonly");

                txtTaskAllocationDate.Enabled = false;
                txtTaskAllocationDate.Attributes.Add("readonly", "readonly");




                txtEmp.Attributes.Add("readonly", "readonly");
                txtEmp.Text = Session["Emp_Name"].ToString();

                DateTime dd = DateTime.Now;
                txtDate.Text = (Convert.ToDateTime(dd, cult).ToString("dd/MM/yyyy"));

                string empId = Session["Emp_ID"].ToString();
                BindGrid(empId);

                txtDate2.Enabled = false;
                txtDate2.Attributes.Add("readonly", "readonly");
                DateTime dd1 = DateTime.Now;
                txtDate2.Text = Convert.ToDateTime(dd1, cult).ToString("dd/MM/yyyy");


                txtEmp1.Attributes.Add("readonly", "readonly");
                txtEmp1.Text = Session["Emp_Name"].ToString();

            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void Grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = "";

        if (e.CommandName == "OpenTaskDetailModel")
        {
            ViewState["SelectedProjectId"] = "";
            ViewState["TaskAllocationId"] = "";
            ViewState["EmpId"] = "";
            ViewState["ProjectId"] = "";
            rdProjectName.Text = "";

            txtFromDate.Text = "";
            txtTodate.Text = "";
            txtTaskAllocationDate.Text = "";

            string projectId = e.CommandArgument.ToString();

            // 🔸 Store in ViewState
            ViewState["SelectedProjectId"] = projectId;

            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Label lblProjectName = (Label)row.FindControl("lblProjectName");
            rdProjectName.Text = lblProjectName.Text;

            Label lblFromDate = (Label)row.FindControl("lblFromDate");
            txtFromDate.Text = lblFromDate.Text;


            Label lblToDate = (Label)row.FindControl("lblToDate");
            txtTodate.Text = lblToDate.Text;

            Label lblCreatedDate = (Label)row.FindControl("lblCreatedDate");
            txtTaskAllocationDate.Text = lblCreatedDate.Text;







            HiddenField hfTaskId = row.FindControl("hfTaskAllocationId") as HiddenField;
            HiddenField hfEmpId = row.FindControl("hfEmpId") as HiddenField;
            HiddenField hfProjectId = row.FindControl("hfProjectId") as HiddenField;

            if (hfTaskId != null && hfEmpId != null && hfProjectId != null)
            {
                ViewState["TaskAllocationId"] = hfTaskId.Value;
                ViewState["EmpId"] = hfEmpId.Value;
                ViewState["ProjectId"] = hfProjectId.Value;
            }
            Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            Grid.UseAccessibleHeader = true;
            BindGridTaskHistory(ViewState["EmpId"].ToString(), ViewState["TaskAllocationId"].ToString(), ViewState["ProjectId"].ToString());


            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal').modal('show');", true);
        }
    }

    private void BindGridTaskHistory(string empId, string taskAllocationId, string projectId)
    {
        try
        {
            DataSet ds1 = objdb.ByProcedure("Usp_GetEmployeeTaskDailyDetails", new string[] { "EmpId", "TaskAllocationId", "ProjectId" }, new string[] { empId, taskAllocationId, projectId }, "dataset");

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                GridOldTask.DataSource = ds1.Tables[0];
                GridOldTask.DataBind();

            }
            else
            {
                GridOldTask.DataSource = null;
                GridOldTask.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
        }
    }



    private void BindGrid(string empId)
    {
        try
        {


            ds = objdb.ByProcedure("Usp_GetEmployeeTaskDetails", new string[] { "EmpId" }, new string[] { empId }, "dataset");

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Grid.DataSource = ds.Tables[0];
                Grid.DataBind();
                Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                Grid.UseAccessibleHeader = true;
            }
            else
            {
                Grid.DataSource = null;
                Grid.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
        }
    }

    protected void btnSaveTask_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        try
        {
            int DocFailedCntExt = 0;
            string strFileName = "";
            string strExtension = "";
            string strTimeStamp = "";
            string TaskDocument = "";

            if (FUDoc.HasFile)
            {
                // Allowed extensions
                string[] supportedTypes = { "pdf", "doc", "docx", "xls", "xlsx", "jpg", "jpeg", "png" };
                string fileExt = System.IO.Path.GetExtension(FUDoc.FileName).ToLower().TrimStart('.');

                // Check file size (max 20 MB)
                int maxSize = 20 * 1024 * 1024; // 20 MB in bytes
                if (!supportedTypes.Contains(fileExt) || FUDoc.PostedFile.ContentLength > maxSize)
                {
                    DocFailedCntExt += 1;
                }
                else
                {
                    strFileName = FUDoc.FileName;
                    strExtension = System.IO.Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                    string strName = System.IO.Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "TaskDoc-" + strTimeStamp + strExtension;

                    string path = Server.MapPath("~/mis/DailyTaskDoc");
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    string fullPath = System.IO.Path.Combine(path, strFileName);

                    // Save the file
                    FUDoc.SaveAs(fullPath);

                    TaskDocument = strFileName;

                    // Clear temp variables
                    strFileName = "";
                    strName = "";
                    strExtension = "";
                    strTimeStamp = "";
                }
            }

            if (Page.IsValid)
            {

                if (btnSaveTask.Text == "Save")
                {
                    ds = objdb.ByProcedure("Usp_InsertDailyTask", new string[] { "EmpId", "TaskAllocationId", "ProjectId", "Hours", "Minutes", "TaskStatus", "WorkingStatus", "InternalChallange", "ExternalChallange", "TaskDoc", "Remark", "OfficeId", "UserTypeId ", "CreatedBy", "CreatedByIP" }, new string[] {
                     ViewState["EmpId"].ToString(),ViewState["TaskAllocationId"].ToString(), ViewState["ProjectId"].ToString(),txtHours.Text,txtMinutes.Text,ddlStatus.SelectedValue,ddlWorkingStatus.SelectedValue ,txtInternalChallange.Value,txtExternalChallange.Value,TaskDocument,txtRemark.Value,Session["Office_ID"].ToString(),Session["UserTypeId"].ToString(),ViewState["Emp_ID"].ToString(),objdb.GetLocalIPAddress() }, "dataset");
                }
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                    ViewState["EmpId"] = "";
                    ViewState["TaskAllocationId"] = "";
                    ViewState["ProjectId"] = "";

                    txtHours.Text = "";
                    txtMinutes.Text = "";
                    ddlStatus.ClearSelection();
                    ddlWorkingStatus.ClearSelection();
                    txtInternalChallange.Value = "";
                    txtExternalChallange.Value = "";
                    txtRemark.Value = "";

                    BindGrid(ViewState["Emp_ID"].ToString());
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error : " + ex.Message);
        }
    }

    //private void UpdateSubmitButtonVisibility()
    //{
    //    // Initialize a flag to track if any checkbox is checked
    //    bool anyChecked = false;
    //    // Loop through all rows in the GridView
    //    foreach (GridViewRow row in Grid.Rows)
    //    {
    //        // Find the checkbox in the current row
    //        CheckBox chkbox = (CheckBox)row.FindControl("chkbox");
    //        if (chkbox != null && chkbox.Checked)
    //        {
    //            anyChecked = true; // Set the flag if any checkbox is checked
    //            break; // No need to check further, we found a checked checkbox
    //        }
    //    }
    //    DivBtn.Visible = anyChecked;
    //}

    //protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    //{

    //    CheckBox chkbox = (CheckBox)sender;
    //    GridViewRow row = (GridViewRow)chkbox.NamingContainer;
    //    bool isChecked = chkbox.Checked;
    //    UpdateSubmitButtonVisibility();
    //}
    //private void UpdateSubmitButtonVisibility()
    //{
    //    bool anyValidRow = false;
    //    string validationMsg = "Please enter both Hours and Minutes for selected task ";

    //    foreach (GridViewRow row in Grid.Rows)
    //    {
    //        CheckBox chkbox = row.FindControl("chkbox") as CheckBox;
    //        if (chkbox != null && chkbox.Checked)
    //        {
    //            TextBox txtHours = row.FindControl("txtHours") as TextBox;
    //            TextBox txtMinutes = row.FindControl("txtMinutes") as TextBox;
    //            Label lblTaskName = row.FindControl("lblTaskName") as Label;

    //            string hours = txtHours != null ? txtHours.Text.Trim() : "";
    //            string minutes = txtMinutes != null ? txtMinutes.Text.Trim() : "";

    //            // Check if both are filled
    //            if (!string.IsNullOrEmpty(hours) && !string.IsNullOrEmpty(minutes))
    //            {
    //                anyValidRow = true; // Allow showing Submit button
    //            }
    //            else
    //            {
    //                string taskName = lblTaskName != null ? lblTaskName.Text : "task";

    //                lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Please enter both Hours and Minutes for selected task !", taskName);
    //                anyValidRow = false;
    //                chkbox.Checked = false;
    //                break; // stop at first invalid row
    //            }
    //        }
    //        else {

    //            lblMsg.Text = "";
    //        }
    //    }

    //    // Show/hide Submit button based on validation
    //    DivBtn.Visible = anyValidRow;

    //    // Show message if not valid
    //    if (!anyValidRow)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Mandatory Fields!", validationMsg);
    //    }
    //    else
    //    {
    //        lblMsg.Text = ""; // clear if valid
    //    }
    //}



    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {


    //        foreach (GridViewRow row in Grid.Rows)
    //        {
    //            CheckBox chk = row.FindControl("chkbox") as CheckBox;
    //            if (chk != null && chk.Checked)
    //            {

    //                Label lblProjectName = row.FindControl("lblProjectName") as Label;
    //                Label lblParentTask = row.FindControl("lblParentTask") as Label;
    //                Label lblTaskName = row.FindControl("lblTaskName") as Label;
    //                Label lblTaskType = row.FindControl("lblTaskType") as Label;
    //                Label lblTaskDescription = row.FindControl("lblTaskDescription") as Label;
    //                Label lblAssignBy = row.FindControl("lblAssignBy") as Label;
    //                TextBox txtHours = row.FindControl("txtHours") as TextBox;
    //                TextBox txtMinutes = row.FindControl("txtMinutes") as TextBox;
    //                HiddenField hfTaskId = row.FindControl("hfTaskAllocationId") as HiddenField;
    //                HiddenField hfEmpId = row.FindControl("hfEmpId") as HiddenField;
    //                HiddenField hfProjectId = row.FindControl("hfProjectId") as HiddenField;
    //                RadioButton rbComplete = row.FindControl("rbComplete") as RadioButton;
    //                RadioButton rbPending = row.FindControl("rbPending") as RadioButton;

    //                // Reading values safely
    //                string projectName = lblProjectName != null ? lblProjectName.Text : "";
    //                string parentTask = lblParentTask != null ? lblParentTask.Text : "";
    //                string taskName = lblTaskName != null ? lblTaskName.Text : "";
    //                string taskType = lblTaskType != null ? lblTaskType.Text : "";
    //                string taskDescription = lblTaskDescription != null ? lblTaskDescription.Text : "";
    //                string assignedBy = lblAssignBy != null ? lblAssignBy.Text : "";
    //                string hours = txtHours != null ? txtHours.Text.Trim() : "0";
    //                string minutes = txtMinutes != null ? txtMinutes.Text.Trim() : "0";

    //                string status = "1"; // Working
    //                if (rbComplete != null && rbComplete.Checked)
    //                    status = "2"; // Complete
    //                else if (rbPending != null && rbPending.Checked)
    //                    status = "3"; //Pending

    //                // Reading value from <textarea>
    //                string remark = "";
    //                Control remarkControl = row.FindControl("txtRemark");
    //                if (remarkControl != null)
    //                    remark = Request.Form[remarkControl.UniqueID] ?? "";

    //                // Read TaskAllocationId safely
    //                int taskIdAllocationId = 0;
    //                if (hfTaskId != null)
    //                    int.TryParse(hfTaskId.Value, out taskIdAllocationId);

    //                int empid = 0;
    //                if (empid != null)
    //                    int.TryParse(hfEmpId.Value, out empid);

    //                int projectId = 0;
    //                if (projectId != null)
    //                    int.TryParse(hfProjectId.Value, out projectId);
    //                SaveTaskStatusToDatabase(taskIdAllocationId, empid, hours, minutes, status, remark, projectId);
    //            }
    //        }
    //    } catch (Exception ex) {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Error 1 !", ex.ToString());

    //    }

    //}


    //private void SaveTaskStatusToDatabase(int taskIdAllocationId, int empid, string hours, string minutes, string status, string remark,int projectId)
    //{
    //    try
    //    {
    //        string officeId = ViewState["Office_ID"].ToString();
    //    string userTypeId = ViewState["UserTypeId"].ToString();
    //    string createdBy = ViewState["Emp_ID"].ToString(); 
    //    string createdByIp = Request.UserHostAddress; 
    //    int isActive = 1;

    //    int h = 0, m = 0;
    //    short taskStatus = 0;
    //    int.TryParse(hours, out h);
    //    int.TryParse(minutes, out m);
    //    short.TryParse(status, out taskStatus);

    //    DataSet ds = objdb.ByProcedure("Usp_InsertDailyTask",
    //        new string[]
    //        {
    //        "ProjectId", "EmpId", "TaskAllocationId", "Hours", "Minutes",
    //        "TaskStatus", "Remark", "OfficeId", "UserTypeId", "IsActive",
    //        "CreatedBy", "CreatedByIp","CreatedOn"
    //        },
    //        new string[]
    //        {
    //        projectId.ToString(), empid.ToString(), taskIdAllocationId.ToString(), h.ToString(), m.ToString(),
    //        taskStatus.ToString(), remark, officeId.ToString(), userTypeId.ToString(), isActive.ToString(),
    //        createdBy, createdByIp
    //        },
    //        "dataset");

    //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        string message = ds.Tables[0].Rows[0]["Message"].ToString();
    //        string result = ds.Tables[0].Rows[0]["Result"].ToString();

    //        if (result == "1")
    //        {

    //            lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Thanks !", message);
    //                BindGrid(ViewState["Emp_ID"].ToString());
    //                DivBtn.Visible = false;
    //            }
    //        else
    //        {       
    //            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", message);
    //                BindGrid(ViewState["Emp_ID"].ToString());
    //                DivBtn.Visible = false;
    //            }
    //    }
    //    else
    //    {
    //        lblMsg.Text = "Unexpected error while saving task.";
    //    }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Error 1 !", ex.ToString());

    //    }
    //}



    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt.Columns.Add("EmpId", typeof(int));
    //        dt.Columns.Add("TaskAllocationId", typeof(int));
    //        dt.Columns.Add("ProjectId", typeof(int));
    //        dt.Columns.Add("Hours", typeof(int));
    //        dt.Columns.Add("Minutes", typeof(int));
    //        dt.Columns.Add("TaskStatus", typeof(short));
    //        dt.Columns.Add("Remark", typeof(string));
    //        dt.Columns.Add("OfficeId", typeof(int));
    //        dt.Columns.Add("UserTypeId", typeof(int));
    //        dt.Columns.Add("CreatedBy", typeof(int));
    //        dt.Columns.Add("CreatedByIp", typeof(string));

    //        int officeId = Convert.ToInt32(ViewState["Office_ID"]);
    //        int userTypeId = Convert.ToInt32(ViewState["UserTypeId"]);
    //        int createdBy = Convert.ToInt32(ViewState["Emp_ID"]);
    //        string createdByIp = Request.UserHostAddress;

    //        foreach (GridViewRow row in Grid.Rows)
    //        {
    //            CheckBox chk = row.FindControl("chkbox") as CheckBox;
    //            if (chk != null && chk.Checked)
    //            {
    //                TextBox txtHours = row.FindControl("txtHours") as TextBox;
    //                TextBox txtMinutes = row.FindControl("txtMinutes") as TextBox;
    //                HiddenField hfTaskId = row.FindControl("hfTaskAllocationId") as HiddenField;
    //                HiddenField hfEmpId = row.FindControl("hfEmpId") as HiddenField;
    //                HiddenField hfProjectId = row.FindControl("hfProjectId") as HiddenField;
    //                RadioButton rbComplete = row.FindControl("rbComplete") as RadioButton;
    //                RadioButton rbPending = row.FindControl("rbPending") as RadioButton;

    //                int h = 0, m = 0;
    //                int hours = int.TryParse(txtHours != null ? txtHours.Text.Trim() : "0", out h) ? h : 0;
    //                int minutes = int.TryParse(txtMinutes != null ? txtMinutes.Text.Trim() : "0", out m) ? m : 0;

    //                short taskStatus = 1;
    //                if (rbComplete != null && rbComplete.Checked) taskStatus = 2;
    //                else if (rbPending != null && rbPending.Checked) taskStatus = 3;

    //                string remark = "";
    //                Control remarkControl = row.FindControl("txtRemark");
    //                if (remarkControl != null)
    //                    remark = Request.Form[remarkControl.UniqueID] ?? "";

    //                int empId = int.Parse(hfEmpId.Value);
    //                int taskId = int.Parse(hfTaskId.Value);
    //                int projectId = int.Parse(hfProjectId.Value);

    //                dt.Rows.Add(empId, taskId, projectId, hours, minutes, taskStatus, remark, officeId, userTypeId, createdBy, createdByIp);
    //            }
    //        }

    //        if (dt.Rows.Count > 0)
    //        {
    //            SaveTaskStatusToDatabaseBulk(dt);
    //        }
    //        else
    //        {
    //            lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning!", "No valid tasks selected.");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Error!", ex.Message);
    //    }
    //}
    //private void SaveTaskStatusToDatabaseBulk(DataTable dt)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";

    //        DataSet ds = objdb.ByProcedure(
    //            "Usp_InsertDailyTask",
    //            new string[] { },
    //            new string[] { },
    //            new string[] { "TaskEntries" },
    //            new DataTable[] { dt }, "dataset");

    //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            string result = ds.Tables[0].Rows[0]["Result"].ToString();
    //            string message = ds.Tables[0].Rows[0]["Message"].ToString();

    //            if (result == "1")
    //            {
    //                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Success!", message);
    //                BindGrid(ViewState["Emp_ID"].ToString());
    //                //DivBtn.Visible = false;
    //            }
    //            else
    //            {
    //                lblMsg.Text = objdb.Alert("fa-exclamation-circle", "alert-warning", "Warning!", message);
    //                BindGrid(ViewState["Emp_ID"].ToString());
    //                //DivBtn.Visible = false;
    //            }
    //        }
    //        else
    //        {
    //            lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning!", "Unexpected response from server.");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Error!", ex.ToString());
    //    }
    //}
    //protected void ddlOvertime_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlOvertime.SelectedValue == "Yes")
    //    {
    //        dvOvertimeHour.Visible = true;
    //        dvOvertimeMinutes.Visible = true;
    //        dvOvertimeRemark.Visible = true;
    //        RequiredFieldValidator3.Enabled = true;
    //        RequiredFieldValidator4.Enabled = true;
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal').modal('show');", true);
    //    }
    //    else {
    //        dvOvertimeHour.Visible = false;
    //        dvOvertimeMinutes.Visible = false;
    //        dvOvertimeRemark.Visible = false;
    //        RequiredFieldValidator3.Enabled = false;
    //        RequiredFieldValidator4.Enabled = false;
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal').modal('show');", true);
    //    }
    //}



    protected void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void GridOldTask_RowDataBound(object sender, GridViewRowEventArgs e)
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