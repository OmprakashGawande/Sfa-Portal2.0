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
              
                string currentPath = Request.Url.AbsolutePath.Substring(Request.Url.AbsolutePath.LastIndexOf("/") + 1);
                ((MainMaster)this.Master).GenerateBreadcrumb(currentPath);

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


                lblTaskDetails.Attributes.Add("readonly", "readonly");
                lblTaskDiscreiptionData.Attributes.Add("readonly", "readonly");

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

                txtEmp1.Enabled = false;
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
            lblTaskDetails.Value = "";
            lblTaskDiscreiptionData.Value = "";

            string projectId = e.CommandArgument.ToString();

            // 🔸 Store in ViewState
            ViewState["SelectedProjectId"] = projectId;

            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Label lblProjectName = (Label)row.FindControl("lblProjectName");

            rdProjectName.Text = lblProjectName.Text;
            txtEmp.Text = Session["Emp_Name"].ToString();

            Label lblFromDate = (Label)row.FindControl("lblFromDate");
            txtFromDate.Text = lblFromDate.Text;


            Label lblToDate = (Label)row.FindControl("lblToDate");
            txtTodate.Text = lblToDate.Text;

            Label lblCreatedDate = (Label)row.FindControl("lblCreatedDate");
            txtTaskAllocationDate.Text = lblCreatedDate.Text;



            Label lblTaskName = (Label)row.FindControl("lblTaskName");
            lblTaskDetails.Value = lblTaskName.Text;
            Label lblTaskDescription = (Label)row.FindControl("lblTaskDescription");
            lblTaskDiscreiptionData.Value = lblTaskDescription.Text;




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



            string script2 = @"var myModal = new bootstrap.Modal(document.getElementById('exampleModal')); myModal.show(); setTimeout(function() { $('.select2').select2({ dropdownParent: $('#exampleModal')}); $('.multiselect-dropdown').attr('style', 'width:250px !important;');}, 200);";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script2, true);
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
                    lblmsgErr.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    string script2 = @"var myModal = new bootstrap.Modal(document.getElementById('exampleModal')); myModal.show(); setTimeout(function() { $('.select2').select2({ dropdownParent: $('#exampleModal')}); $('.multiselect-dropdown').attr('style', 'width:250px !important;');}, 200);";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script2, true);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error : " + ex.Message);
        }
    }


    protected void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatus = (Label)e.Row.FindControl("lblTaskStatusText");
            if (lblStatus != null)
            {
                string status = lblStatus.Text.Trim().ToLower();

                // Reset class and inline style
                lblStatus.CssClass = "";
                lblStatus.Style["font-weight"] = "bold";

                if (status.Contains("complete"))
                {
                    lblStatus.CssClass = "text-success";
                }
                else if (status.Contains("pending"))
                {
                    lblStatus.CssClass = "text-danger";
                }
                else if (status.Contains("working"))
                {
                    lblStatus.CssClass = "text-warning";
                }
                else if (status.Contains("not"))
                {
                    lblStatus.CssClass = "text-primary";
                }
                else
                {
                    lblStatus.CssClass = "text-dark";
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