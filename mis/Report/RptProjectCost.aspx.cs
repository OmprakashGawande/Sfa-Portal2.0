using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Report_RptProjectCost : System.Web.UI.Page
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

    private void BindData()
    {
        try
        {
            DataSet ds1 = objdb.ByProcedure("Usp_GetEmployeeWiseProjectCost", new string[] { "ProjectId" }, new string[] { ddlProjectName.SelectedValue }, "dataset");

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


    protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindData();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
        }
    }

    decimal totalHoursWorked = 0;
    decimal totalDaysWorked = 0;
    decimal totalCost = 0;

    protected void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            object hoursObj = DataBinder.Eval(e.Row.DataItem, "TotalHoursWorked");
            object daysObj = DataBinder.Eval(e.Row.DataItem, "TotalDaysWorked");
            object costObj = DataBinder.Eval(e.Row.DataItem, "TotalCost");

            if (hoursObj != DBNull.Value && hoursObj != null)
                totalHoursWorked += Convert.ToDecimal(hoursObj);

            if (daysObj != DBNull.Value && daysObj != null)
                totalDaysWorked += Convert.ToDecimal(daysObj);

            if (costObj != DBNull.Value && costObj != null)
                totalCost += Convert.ToDecimal(costObj);
        }

        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblHoursFooter = (Label)e.Row.FindControl("lblTotalHoursWorkedFooter");
            Label lblDaysFooter = (Label)e.Row.FindControl("lblTotalDaysWorkedFooter");
            Label lblCostFooter = (Label)e.Row.FindControl("lblTotalCostFooter");

            if (lblHoursFooter != null)
                lblHoursFooter.Text = totalHoursWorked.ToString("N2");

            if (lblDaysFooter != null)
                lblDaysFooter.Text = totalDaysWorked.ToString("N2");

            if (lblCostFooter != null)
                lblCostFooter.Text = "₹" + totalCost.ToString("N2");
        }
    }



}