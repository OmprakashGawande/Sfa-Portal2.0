using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Transaction_TrnTaskAllocation : System.Web.UI.Page
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
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["UserTypeId"] = Session["UserTypeId"].ToString();
                ViewState["Designation_ID"] = Session["Designation_ID"].ToString();
                GetProjecName();
                BindGrid();
                fillWorkCategory();
                GetTaskPriority();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    public void GetProjecName()
    {
        try
        {
            string empId = empId = ViewState["Emp_ID"].ToString();
            //if (Session["Designation_ID"].ToString() == "1")
            //{
            //    empId = "0";
            //}
            //if (Session["Designation_ID"].ToString() == "8" || Session["Designation_ID"].ToString() == "13")
            //{
            //    empId = ViewState["Emp_ID"].ToString();
            //}
            DataSet ds = objdb.ByProcedure("Usp_GetTaskAllocationDropdown", new string[] { "flag", "EmpId" }, new string[] { "1", empId }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlProjectName.DataSource = ds.Tables[0];
                ddlProjectName.DataTextField = "ProjectName";
                ddlProjectName.DataValueField = "ProjectId";
                ddlProjectName.DataBind();
            }
            ddlProjectName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            throw new Exception("Error while binding Type of Project dropdown: " + ex.Message);
        }
    }
    public void GetTaskPriority()
    {
        try
        {

            DataSet ds = objdb.ByProcedure("Usp_GetTaskPriority", new string[] { }, new string[] { }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlTaPriority.DataSource = ds.Tables[0];
                ddlTaPriority.DataTextField = "TaskPriority";
                ddlTaPriority.DataValueField = "TaskPriorityId";
                ddlTaPriority.DataBind();
            }
            ddlTaPriority.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            throw new Exception("Error while binding Type of Task Priority dropdown: " + ex.Message);
        }
    }

    protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ddlMainPower.Items.Clear();
            ddlTask.Items.Clear();
            ddlProjectModule.Items.Clear();
            ddlMainPower.Items.Clear();
            DataSet ds = objdb.ByProcedure("Usp_GetTaskAllocationDropdown", new string[] { "flag", "ProjectId", "EmpId" }, new string[] { "2", ddlProjectName.SelectedValue, ViewState["Emp_ID"].ToString() }, "dataset");
            DataSet ds3 = objdb.ByProcedure("Usp_GetTaskAllocationDropdown", new string[] { "flag", "ProjectId" }, new string[] { "5", ddlProjectName.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlMainPower.DataSource = ds.Tables[0];
                ddlMainPower.DataTextField = "Emp_Name";
                ddlMainPower.DataValueField = "EmpId";
                ddlMainPower.DataBind();
            }

            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                ddlProjectModule.DataSource = ds3.Tables[0];
                ddlProjectModule.DataTextField = "ModuleName";
                ddlProjectModule.DataValueField = "ModuleId";
                ddlProjectModule.DataBind();

            }


            ddlMainPower.Items.Insert(0, new ListItem("Select", "0"));
            ddlProjectModule.Items.Insert(0, new ListItem("Select", "0"));
            ddlTask.Items.Insert(0, new ListItem("Select", "0"));


        }
        catch (Exception ex)
        {
            throw new Exception("Error while binding Type of Project dropdown: " + ex.Message);
        }
    }
    protected void fillWorkCategory()
    {
        try
        {
            ddlWorkCategoryId.Items.Clear();
            DataSet ds1 = objdb.ByProcedure("USP_Daily_Task_GetAllWorkCategory", new string[] { }, new string[] { }, "dataset");
            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlWorkCategoryId.DataValueField = "WorkCategoryId";
                        ddlWorkCategoryId.DataTextField = "WorkCategoryEng";
                        ddlWorkCategoryId.DataSource = ds1;
                        ddlWorkCategoryId.DataBind();
                        ddlWorkCategoryId.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        ddlWorkCategoryId.Items.Insert(0, new ListItem("No Record Found", "0"));
                    }
                }
                else
                {
                    ddlWorkCategoryId.Items.Insert(0, new ListItem("No Record Found", "0"));
                }
            }
            else
            {
                ddlWorkCategoryId.Items.Insert(0, new ListItem("No Record Found", "0"));
            }
            if (ds1 != null) ds1.Dispose();

        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7: " + ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string TaskId = "";
                int DocFailedCntExt = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                string Document = "";
                //foreach (ListItem item in ddlTask.Items)
                //{
                //    if (item.Selected)
                //    {
                //        TaskId += item.Value + ",";
                //    }
                //}
                if (FUDoc.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(FUDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    //else
                    //{
                    strFileName = FUDoc.FileName.ToString();
                    strExtension = System.IO.Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = System.IO.Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "TaskAllocationDoc-" + strTimeStamp + strExtension;
                    string path = System.IO.Path.Combine(Server.MapPath("~/mis/Document"), strFileName);
                    FUDoc.SaveAs(path);

                    Document = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                    //}
                }
                string FromDate = txtFromDate.Text != "" ? Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd") : "";
                string ToDate = txtToDate.Text != "" ? Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") : "";

                if (btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("Usp_InsertOrUpdateTaskAllocation", new string[] { "ProjectId", "ModuleId", "EmpId", "TaskId", "FromDate", "ToDate", "Discription", "TaskPriorityId", "TaskAllocationDoc", "UserTypeId", "OfficeId", "CreatedBy", "CreatedByIP", "CategoreyId" }, new string[] {
                   ddlProjectName.SelectedValue,ddlProjectModule.SelectedValue,ddlMainPower.SelectedValue,ddlTask.SelectedValue,FromDate,ToDate,txtDiscription.Value,ddlTaPriority.SelectedValue,Document, Session["UserTypeId"].ToString(),Session["Office_ID"].ToString(),ViewState["Emp_ID"].ToString(),objdb.GetLocalIPAddress(),ddlWorkCategoryId.SelectedValue }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["TaskAllocationId"] != "" && ViewState["TaskAllocationId"] != null)
                {
                    ds = objdb.ByProcedure("Usp_InsertOrUpdateTaskAllocation", new string[] { "ProjectId", "ModuleId", "EmpId", "TaskId", "FromDate", "ToDate", "Discription", "TaskPriorityId", "TaskAllocationDoc", "UserTypeId", "OfficeId", "LastupdatedBy", "LastupdatedByIP", "TaskAllocationId", "CategoreyId" }, new string[] {
                    ddlProjectName.SelectedValue,ddlProjectModule.SelectedValue,ddlMainPower.SelectedValue,ddlTask.SelectedValue,FromDate,ToDate,txtDiscription.Value,ddlTaPriority.SelectedValue,Document, Session["UserTypeId"].ToString(),Session["Office_ID"].ToString(),ViewState["Emp_ID"].ToString(),objdb.GetLocalIPAddress(),ViewState["TaskAllocationId"].ToString(),ddlWorkCategoryId.SelectedValue }, "dataset");
                }
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);

                    BindGrid();
                    ddlProjectName.ClearSelection();
                    ddlMainPower.ClearSelection();
                    ddlTask.ClearSelection();
                    ddlProjectName_SelectedIndexChanged(sender, e);
                    txtFromDate.Text = "";
                    txtToDate.Text = "";
                    txtDiscription.Value = "";
                    ddlProjectModule.ClearSelection();
                    btnSave.Text = "Save";
                    ddlTaPriority.ClearSelection();
                    ddlWorkCategoryId.ClearSelection();
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error: " + ex.Message);
        }
    }

    protected void BindGrid()
    {
        try
        {
            string empId = empId = ViewState["Emp_ID"].ToString(); ;
            //if (Session["Designation_ID"].ToString() == "1")
            //{
            //    empId = "0";
            //}
            //if (Session["Designation_ID"].ToString() == "8" || Session["Designation_ID"].ToString() == "13")
            //{
            //    empId = ViewState["Emp_ID"].ToString();
            //}
            DataSet ds = objdb.ByProcedure("Usp_BindTaskAllocationData", new string[] { "EmpId" }, new string[] { empId }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
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
            throw new Exception("Error: " + ex.Message);

        }
    }

    protected void Grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["TaskAllocationId"] = "";
            if (e.CommandName == "EditRecord")
            {
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblProjectId = (Label)row.FindControl("lblProjectId");
                Label lblEmpId = (Label)row.FindControl("lblEmpId");
                Label lblTaskId = (Label)row.FindControl("lblTaskId");
                Label lblFromDate = (Label)row.FindControl("lblFromDate");
                Label lblToDate = (Label)row.FindControl("lblToDate");
                Label lblDiscrisption = (Label)row.FindControl("lblDiscrisption");
                Label lblCategoreyId = (Label)row.FindControl("lblCategoreyId");
                Label lblTaskPriorityId = (Label)row.FindControl("lblTaskPriorityId");
                Label lblModuleId = (Label)row.FindControl("lblModuleId");


                ViewState["TaskAllocationId"] = e.CommandArgument;
                btnSave.Text = "Update";
                txtFromDate.Text = !string.IsNullOrEmpty(lblFromDate.Text) ? lblFromDate.Text : null;
                txtToDate.Text = !string.IsNullOrEmpty(lblToDate.Text) ? lblToDate.Text : null;
                txtDiscription.Value = !string.IsNullOrEmpty(lblDiscrisption.Text) ? lblDiscrisption.Text : null;

                if (!string.IsNullOrEmpty(lblProjectId.Text))
                {
                    ddlProjectName.ClearSelection();
                    ddlProjectName.Items.FindByValue(lblProjectId.Text).Selected = true;
                    ddlProjectName_SelectedIndexChanged(sender, e);
                }
                if (!string.IsNullOrEmpty(lblEmpId.Text))
                {
                    ddlMainPower.ClearSelection();
                    var item = ddlMainPower.Items.FindByValue(lblEmpId.Text);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                    else
                    {
                        // Optional: handle the case where the value is not in the list
                        // e.g., log, show message, or add item dynamically
                    }
                }

                if (!string.IsNullOrEmpty(lblCategoreyId.Text))
                {
                    ddlWorkCategoryId.ClearSelection();
                    var item = ddlWorkCategoryId.Items.FindByValue(lblCategoreyId.Text);
                    if (item != null)
                    {
                        item.Selected = true;
                    }

                }
                if (!string.IsNullOrEmpty(lblModuleId.Text))
                {
                    ddlProjectModule.ClearSelection();
                    var item = ddlProjectModule.Items.FindByValue(lblModuleId.Text);
                    if (item != null)
                    {
                        item.Selected = true;
                        ddlProjectModule_SelectedIndexChanged(sender, e);
                    }

                }
                if (!string.IsNullOrEmpty(lblTaskId.Text))
                {
                    ddlTask.ClearSelection();
                    var item = ddlTask.Items.FindByValue(lblTaskId.Text);
                    if (item != null)
                    {
                        item.Selected = true;
                    }

                }
                //if (!string.IsNullOrEmpty(lblTaskId.Text))
                //{
                //    ddlTask.ClearSelection();
                //    ddlTask.Items.FindByValue(lblTaskId.Text).Selected = true;
                //}
                if (!string.IsNullOrEmpty(lblTaskPriorityId.Text))
                {
                    ddlTaPriority.ClearSelection();
                    ddlTaPriority.Items.FindByValue(lblTaskPriorityId.Text).Selected = true;
                }
                //string[] selectedTaskIds = lblTaskId.Text.Split(',');
                //ddlTask.ClearSelection();
                //foreach (ListItem item in ddlTask.Items)
                //{
                //    if (selectedTaskIds.Contains(item.Value))
                //    {
                //        item.Selected = true;
                //    }
                //}
            }
            else if (e.CommandName == "ChangeStatus")
            {

                objdb.ByProcedure("Usp_GetTaskAllocationDropdown", new string[] { "flag", "TaskAllocationId", "LastupdatedBy", "LastupdatedByIP", }, new string[] { "4", e.CommandArgument.ToString(), ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "Update");
                BindGrid();
            }

        }
        catch (Exception ex)
        {
            throw new Exception("Error: " + ex.Message);

        }
    }

    protected void ddlMainPower_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objdb.ByProcedure("Usp_BindTaskAllocationData", new string[] { "EmpId" }, new string[] { ddlMainPower.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                grvWorkingProject.DataSource = ds.Tables[1];
                grvWorkingProject.DataBind();
                DivWorkinProject.Visible = true;
            }
            else
            {
                grvWorkingProject.DataSource = null;
                grvWorkingProject.DataBind();
                DivWorkinProject.Visible = false;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void ddlProjectModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            DataSet ds = objdb.ByProcedure("Usp_GetTaskAllocationDropdown", new string[] { "flag", "ProjectId", "ModuleId" }, new string[] { "3", ddlProjectName.SelectedValue, ddlProjectModule.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlTask.DataSource = ds.Tables[0];
                ddlTask.DataTextField = "TaskName";
                ddlTask.DataValueField = "TaskId";
                ddlTask.DataBind();
            }
            else
            {
                ddlTask.Items.Clear();
            }
            ddlTask.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {

            throw new Exception("Error while binding Type of Task dropdown: " + ex.Message);
        }
    }
}