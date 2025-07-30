using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Linq;

public partial class mis_Payroll_PayrollArrearOfficeTotal : System.Web.UI.Page
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

            ds = objdb.ByProcedure("SpPayrollEmpArrear", new string[] { "flag", "CurrentYear", "CurrentMonth" }, new string[] { "11", ddlFinancialYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();

                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;

                decimal EPF = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPF"));
                decimal BasicSalary = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BasicSalary"));
                decimal TotalEarning = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalEarning"));
                decimal TotalDeduction = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalDeduction"));
                decimal NetPaymentAmount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("NetPaymentAmount"));

                GridView1.FooterRow.Cells[1].Text = "| TOTAL | ";
                GridView1.FooterRow.Cells[3].Text = EPF.ToString();
                GridView1.FooterRow.Cells[4].Text = BasicSalary.ToString();
                GridView1.FooterRow.Cells[5].Text = TotalEarning.ToString();
                GridView1.FooterRow.Cells[6].Text = TotalDeduction.ToString();
                GridView1.FooterRow.Cells[7].Text = NetPaymentAmount.ToString();
                

            }
            else if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
              
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
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
            lblMsg.Text = "";
            if (ddlFinancialYear.SelectedIndex > 0  && ddlMonth.SelectedIndex > 0)
            {
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
   
}