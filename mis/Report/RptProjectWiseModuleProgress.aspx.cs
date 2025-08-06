using DocumentFormat.OpenXml.Drawing;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class mis_Report_RptProjectWiseModuleProgress : System.Web.UI.Page
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds1 = objdb.ByProcedure("Usp_RptGetProjectWiseModulePhase", new string[] { "ProjectId" }, new string[] { ddlProjectName.SelectedValue }, "dataset");
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
                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", "No Record Found");

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", ex.ToString());
        }

    }
}