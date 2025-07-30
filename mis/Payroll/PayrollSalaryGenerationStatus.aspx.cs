using System;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollSalaryGenerationStatus : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ddlYear.Items.Insert(0, new ListItem("Select", "0"));
                    FillDropdown();
                    btnSave.Visible = false;
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpPayrollSalaryCancellationRequest", new string[] { "flag", "Year", "Month" }, new string[] { "2", ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                btnSave.Visible = true;
                GridView1.DataSource = ds;
                GridView1.DataBind();

                GridView1.FooterRow.Cells[1].Text = "| TOTAL | ";
                GridView1.FooterRow.Cells[2].Text = ds.Tables[1].Rows[0]["TotalEmp"].ToString();
                GridView1.FooterRow.Cells[3].Text = ds.Tables[1].Rows[0]["TotalGen"].ToString();

                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDropdown()
    {
        try
        {
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year. <br/>";
            }
            if (ddlMonth.SelectedIndex == 0)
            {
                msg += "Select Month. <br/>";
            }
            if (msg == "")
            {
                FillGrid();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByProcedure("SpPayrollSalaryGenerateReport",
               new string[] { "flag", "Year", "Month" },
               new string[] { "2", ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count == 0)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    Label OfficeName = (Label)row.FindControl("lblOfficeName");
                    int Office_ID = Convert.ToInt16(OfficeName.ToolTip.ToString());
                    Label TotalEmp = (Label)row.FindControl("lblTotalEmp");
                    Label GenStatus = (Label)row.FindControl("lblGenStatus");
                    string StatusCount = GenStatus.ToolTip.ToString();
                    objdb.ByProcedure("SpPayrollSalaryGenerateReport",
                    new string[] { "flag", "Year", "Month", "Office_ID", "Total_Emp", "Salary_Generated", "Salary_UpdatedBy" },
                    new string[] { "1", ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), Office_ID.ToString(), TotalEmp.Text,
                 StatusCount.ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                }
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record of " + ddlYear.SelectedValue.ToString() + " " + ddlMonth.SelectedItem.Text + " is already exists ');", true);
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}