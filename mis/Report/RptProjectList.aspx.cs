using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Report_RptProjectList : System.Web.UI.Page
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
                BindGridProjectList();
                BindProjectDropdown();
            }
        }
    }
    private void BindGridProjectList()
    {
        try
        {
            DataSet ds1 = objdb.ByProcedure("Usp_GetRptProjectList", new string[] { "TypeOfProject" }, new string[] { ddlTypeOfProject.SelectedValue }, "dataset");

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
        }
    }


    private void BindProjectDropdown()
    {
        try
        {
            Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            Grid.UseAccessibleHeader = true;
            DataSet ds = objdb.ByProcedure("Usp_GetddlTypeofProject", new string[] { }, new string[] { }, "dataset");

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlTypeOfProject.DataSource = ds.Tables[0];
                ddlTypeOfProject.DataTextField = "TypeOfProjectName";
                ddlTypeOfProject.DataValueField = "TypeOfProjectId";
                ddlTypeOfProject.DataBind();
                ddlTypeOfProject.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlTypeOfProject.Items.Clear();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
        }
    }


    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindGridProjectList();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
        }
    }
}