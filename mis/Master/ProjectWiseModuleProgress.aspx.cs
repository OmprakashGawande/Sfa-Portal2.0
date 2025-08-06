using DocumentFormat.OpenXml.Drawing;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class mis_Master_ProjectWiseModuleProgress : System.Web.UI.Page
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
    protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlProjectModule.Items.Clear();
            DataSet ds3 = objdb.ByProcedure("Usp_GetTaskAllocationDropdown", new string[] { "flag", "ProjectId" }, new string[] { "5", ddlProjectName.SelectedValue }, "dataset");
            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                ddlProjectModule.DataSource = ds3.Tables[0];
                ddlProjectModule.DataTextField = "ModuleName";
                ddlProjectModule.DataValueField = "ModuleId";
                ddlProjectModule.DataBind();

            }
            ddlProjectModule.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-check", "alert-warning", "Warning !", ex.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            // 0 = Insert, 1 = Update
            int flag = btnSave.Text.ToLower().Contains("update") ? 1 : 0;

            DataSet ds = objdb.ByProcedure("Usp_InsertUpdateProjectWiseModulePhase", new string[] {
            "ModulePhaseId", "ProjectId", "ModuleId", "ModulePhase", "Remark",
            "IsActive", "CreatedBy", "CreatedByIp", "UpdatedBy", "UpdatedByIp"
        }, new string[] {
            flag.ToString(), // Pass 0 for insert, 1 for update
            ddlProjectName.SelectedValue,
           ddlProjectModule.SelectedValue,
            ddlPhase.SelectedValue,
            txtRemark.Value.Trim(),
            "1",
            ViewState["Emp_ID"].ToString(),
            Request.UserHostAddress,
             ViewState["Emp_ID"].ToString(),
            Request.UserHostAddress
        }, "dataset");

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string status = ds.Tables[0].Rows[0]["Status"].ToString();
                string message = ds.Tables[0].Rows[0]["Msg"].ToString();

                if (status == "Ok")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", message);

                    // Optional actions
                    btnSave.Text = "Save";
                    ddlProjectName.ClearSelection();
                    ddlProjectModule.ClearSelection();
                    ddlPhase.ClearSelection();
                    txtRemark.Value = "";
                    BindGrid();
                }
                else if (status == "Error")
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", message);
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", "Plese try after some time");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", ex.ToString());
        }
    }
    protected void Grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRecord")
        {
            try
            {
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                // Get label controls safely
                Label lblProjectId = row.FindControl("lblProjectId") as Label;
                Label lblTModuleName = row.FindControl("lblTModuleName") as Label;
                Label lblPhaseId = row.FindControl("lblPhaseId") as Label;
                Label lblRemark = row.FindControl("lblRemark") as Label;

                if (lblProjectId != null && lblTModuleName != null && lblPhaseId != null && lblRemark != null)
                {
                    // Populate dropdowns and text fields
                    if (ddlProjectName.Items.FindByValue(lblProjectId.Text) != null)
                    {
                        ddlProjectName.SelectedValue = lblProjectId.Text;
                        ddlProjectName_SelectedIndexChanged(sender, e);
                    }
                    string moduleId = GetModuleIdByModuleName(lblTModuleName.Text);
                    if (ddlProjectModule.Items.FindByValue(moduleId) != null)
                        ddlProjectModule.SelectedValue = moduleId;
                    if (ddlPhase.Items.FindByValue(lblPhaseId.Text) != null)
                        ddlPhase.SelectedValue = lblPhaseId.Text;
                    txtRemark.Value = lblRemark.Text;

                    btnSave.Text = "Update";
                    ViewState["IsUpdateMode"] = true;
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", "One or more label controls are missing in the row");
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", ex.ToString());
            }
        }
    }
    private string GetModuleIdByModuleName(string moduleName)
    {
        foreach (ListItem item in ddlProjectModule.Items)
        {
            if (item.Text.Trim().Equals(moduleName.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return item.Value;
            }
        }
        return "0"; // Handle gracefully in calling method
    }
    private void BindGrid()
    {
        try
        {
            DataSet ds1 = objdb.ByProcedure("Usp_GetProjectWiseModulePhase", new string[] { }, new string[] { }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                Grid.DataSource = ds1.Tables[0];
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", ex.ToString());
        }
    }

}