using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Linq;

public partial class mis_Payroll_PayRollEpfDedOfficeWise : System.Web.UI.Page
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
            ds = objdb.ByProcedure("SpPayrollEpfDedMonthWise", new string[] { "flag", "SalaryYear", "SalaryMonth", "Office_ID" }, new string[] { "1", ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), "0" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[2];
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;

                decimal EPFWAGES = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("EPFWAGES"));
                decimal EPF = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("EPF"));
                decimal REFUND_OF_ADVANCES = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("REFUND_OF_ADVANCES"));
                decimal EPF_EPS_DIFF_REMITTED = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("EPF_EPS_DIFF_REMITTED"));
                decimal EPS_CONTRI_REMITTED = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("EPS_CONTRI_REMITTED"));

                GridView1.FooterRow.Cells[0].Text = "| TOTAL | ";
                GridView1.FooterRow.Cells[1].Text = EPFWAGES.ToString();
                GridView1.FooterRow.Cells[2].Text = EPF.ToString();
                GridView1.FooterRow.Cells[3].Text = REFUND_OF_ADVANCES.ToString();
                GridView1.FooterRow.Cells[4].Text = EPF_EPS_DIFF_REMITTED.ToString();
                GridView1.FooterRow.Cells[5].Text = EPS_CONTRI_REMITTED.ToString();
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