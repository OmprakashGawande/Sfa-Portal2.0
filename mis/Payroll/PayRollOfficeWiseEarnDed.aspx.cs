using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Linq;

public partial class mis_Payroll_PayRollOfficeWiseEarnDed : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();

                ds = objdb.ByProcedure("SpAdminOffice",
                       new string[] { "flag" },
                       new string[] { "10" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOffice.DataSource = ds;
                    ddlOffice.DataTextField = "Office_Name";
                    ddlOffice.DataValueField = "Office_ID";
                    ddlOffice.DataBind();
                    ddlOffice.Items.Insert(0, new ListItem("All", "0"));

                }

                FillDropdown();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillDropdown()
    {
        try
        {

            ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "8" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlFinancialYear.DataSource = ds;
                ddlFinancialYear.DataTextField = "Year";
                ddlFinancialYear.DataValueField = "Year";
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
            }

            ds = null;
            ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag" }, new string[] { "5" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEarnDeducHead.DataSource = ds;
                ddlEarnDeducHead.DataTextField = "EarnDeduction_Name";
                ddlEarnDeducHead.DataValueField = "EarnDeduction_ID";
                ddlEarnDeducHead.DataBind();
                ddlEarnDeducHead.Items.Insert(0, new ListItem("Select", "0"));
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
            ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "Salary_Year", "MonthNo", "EarnDeduction_ID" }, new string[] { "21", ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlEarnDeducHead.SelectedValue.ToString(), "0" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;

                decimal EPFWAGES = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                int EPF = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("EmployeeCount"));

                GridView1.FooterRow.Cells[0].Text = "| TOTAL | ";
                GridView1.FooterRow.Cells[1].Text = EPFWAGES.ToString();
                GridView1.FooterRow.Cells[2].Text = EPF.ToString();
            }
           

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
           
            FillGrid();
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
 
}