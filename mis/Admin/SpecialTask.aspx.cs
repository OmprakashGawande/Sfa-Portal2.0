using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Admin_SpecialTask : System.Web.UI.Page
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
                BindDropdown();
                BindGrid();
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

            DataSet dsEmp = objdb.ByProcedure("Usp_GetddlEmployee", new string[] { "EmpId" }, new string[] { empId }, "dataset");
            DataSet dsPro = objdb.ByProcedure("Usp_GetAllProject", new string[] { }, new string[] { }, "dataset");
            DataSet dsTastPri = objdb.ByProcedure("Usp_GetTaskPriority", new string[] { }, new string[] { }, "dataset");


            if (dsEmp != null && dsEmp.Tables[0].Rows.Count > 0)
            {

                ddlEmployee.DataSource = dsEmp.Tables[0];
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_ID";
                ddlEmployee.DataBind();
            }
            if (dsPro != null && dsPro.Tables[0].Rows.Count > 0)
            {
                ddlProjectName.DataSource = dsPro.Tables[0];
                ddlProjectName.DataTextField = "ProjectName";
                ddlProjectName.DataValueField = "ProjectId";
                ddlProjectName.DataBind();

            }
            if (dsTastPri != null && dsTastPri.Tables[0].Rows.Count > 0)
            {
                ddlTaskPriority.DataSource = dsTastPri.Tables[0];
                ddlTaskPriority.DataTextField = "TaskPriority";
                ddlTaskPriority.DataValueField = "TaskPriorityId";
                ddlTaskPriority.DataBind();

            }
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            ddlProjectName.Items.Insert(0, new ListItem("Select", "0"));
            ddlTaskPriority.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            // Optional: log or show error
            throw new Exception("Error while binding Type of Project dropdown: " + ex.Message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string CompletionDate = txtCompletionDate.Text != "" ? Convert.ToDateTime(txtCompletionDate.Text, cult).ToString("yyyy/MM/dd") : "";


            if (btnSave.Text == "Save")
            {
                ds = objdb.ByProcedure("Usp_InsertOrUpdateSpecialTask", new string[] { "EmpId", "ProjectId", "CompletionDate", "TaskPriorityId", "Reason", "UserTypeId", "OfficeId", "CreatedBy", "CreatedByIP" }, new string[] {
                   ddlEmployee.SelectedValue,ddlProjectName.SelectedValue,CompletionDate,ddlTaskPriority.SelectedValue,txtReason.Value, Session["UserTypeId"].ToString(),Session["Office_ID"].ToString(),ViewState["Emp_ID"].ToString(),objdb.GetLocalIPAddress()}, "dataset");
            }
            else if (btnSave.Text == "Update" && ViewState["SpecialTaskId"] != "" && ViewState["SpecialTaskId"] != null)
            {
                ds = objdb.ByProcedure("Usp_InsertOrUpdateSpecialTask", new string[] { "EmpId", "ProjectId", "CompletionDate", "TaskPriorityId", "Reason", "UserTypeId", "OfficeId", "LastupdatedBy", "LastupdatedByIP", "SpecialTaskId" }, new string[] {
                    ddlEmployee.SelectedValue,ddlProjectName.SelectedValue,CompletionDate,ddlTaskPriority.SelectedValue,txtReason.Value, Session["UserTypeId"].ToString(),Session["Office_ID"].ToString(),ViewState["Emp_ID"].ToString(),objdb.GetLocalIPAddress(),ViewState["SpecialTaskId"].ToString() }, "dataset");
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);

                    BindGrid();
                    ddlProjectName.ClearSelection();
                    ddlEmployee.ClearSelection();
                    ddlTaskPriority.ClearSelection();
                    txtReason.Value = "";
                    txtCompletionDate.Text = "";
                    btnSave.Text = "Save";
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                }
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    protected void BindGrid()
    {
        try
        {

            DataSet ds = objdb.ByProcedure("Usp_GetSpecialTaskData", new string[] { }, new string[] { }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                Grid.DataSource = ds.Tables[0];
                Grid.DataBind();
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
            ViewState["SpecialTaskId"] = "";
            if (e.CommandName == "EditRecord")
            {
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblProjectId = (Label)row.FindControl("lblProjectId");
                Label lblSpecialTaskId = (Label)row.FindControl("lblSpecialTaskId");
                Label lblEmpId = (Label)row.FindControl("lblEmpId");
                Label lblCompletionDate = (Label)row.FindControl("lblCompletionDate");
                Label lblTaskPriorityId = (Label)row.FindControl("lblTaskPriorityId");
                Label lblReason = (Label)row.FindControl("lblReason");


                ViewState["SpecialTaskId"] = e.CommandArgument;
                btnSave.Text = "Update";
                txtReason.Value = !string.IsNullOrEmpty(lblReason.Text) ? lblReason.Text : null;
                txtCompletionDate.Text = !string.IsNullOrEmpty(lblCompletionDate.Text) ? lblCompletionDate.Text : null;

                if (!string.IsNullOrEmpty(lblProjectId.Text))
                {
                    ddlProjectName.ClearSelection();
                    ddlProjectName.Items.FindByValue(lblProjectId.Text).Selected = true;
                }
                if (!string.IsNullOrEmpty(lblEmpId.Text))
                {
                    ddlEmployee.ClearSelection();
                    var item = ddlEmployee.Items.FindByValue(lblEmpId.Text);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
           
                if (!string.IsNullOrEmpty(lblTaskPriorityId.Text))
                {
                    ddlTaskPriority.ClearSelection();
                    ddlTaskPriority.Items.FindByValue(lblTaskPriorityId.Text).Selected = true;
                }
               
            }
            //else if (e.CommandName == "ChangeStatus")
            //{

            //    objdb.ByProcedure("Usp_GetTaskAllocationDropdown", new string[] { "flag", "TaskAllocationId", "LastupdatedBy", "LastupdatedByIP", }, new string[] { "4", e.CommandArgument.ToString(), ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "Update");
            //    BindGrid();
            //}

        }
        catch (Exception ex)
        {
            throw new Exception("Error: " + ex.Message);

        }
    }

   
}
