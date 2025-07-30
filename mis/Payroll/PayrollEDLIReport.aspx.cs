using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollEDLIReport : System.Web.UI.Page
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
                DivDetail.Visible = false;
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
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // ==== From Year ====
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
    protected void RetirmentANDNew()
    {
        try
        {
            lblJoiningMsg.Text = "";
            lblRitireMsg.Text = "";
            GridView2.DataSource = new string[] { };
            GridView2.DataBind();
            GridView3.DataSource = new string[] { };
            GridView3.DataBind();
            string SelectedYear = ddlYear.SelectedValue.ToString();
            string SelectedMonth = ddlMonth.SelectedValue.ToString();
            int Year = Convert.ToInt16(SelectedYear);
            int Month = Convert.ToInt16(SelectedMonth);
            if (SelectedMonth == "01")
            {
                Year = (Year - 1);
                Month = 12;
            }
            else
            {
                Month = (Convert.ToInt16(SelectedMonth) - 1);
            }
            string MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month);
            ds = objdb.ByProcedure("SpPayrollEDLIReport", new string[] { "flag", "Salary_Year", "MonthNo" },
                new string[] { "1", Year.ToString(), Month.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
            }
            else
            {
                lblRitireMsg.Text = "There is No Retirement in " + MonthName + " Month";
            }
            ds = objdb.ByProcedure("SpPayrollEDLIReport", new string[] { "flag", "Salary_Year", "MonthNo" },
                new string[] { "2", SelectedYear, SelectedMonth }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView3.DataSource = ds;
                GridView3.DataBind();
            }
            else
            {
                lblJoiningMsg.Text = "There is No New Employee in " + ddlMonth.SelectedItem.Text + " Month";
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
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpPayrollEDLIReport", new string[] { "flag", "Salary_Year", "MonthNo" }, new string[] { "0", ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
                decimal EPF = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EPF"));
                decimal SalaryGenerated = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SalaryGenrated"));
                decimal ADA = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ADA"));
                decimal ADADed = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ADADed"));
                decimal GSLI = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("GSLI"));
                decimal GSLIDed = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("GSLIDed"));
                decimal EGLS = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EGLS"));
                decimal EGLSDed = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EGLSDed"));
                decimal LIC = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("LIC"));
                decimal Total = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Total"));
                GridView1.FooterRow.Cells[1].Text = "| TOTAL | ";
                GridView1.FooterRow.Cells[2].Text = "<b>" + EPF.ToString() + "</b>";
                GridView1.FooterRow.Cells[3].Text = "<b>" + SalaryGenerated.ToString() + "</b>";
                GridView1.FooterRow.Cells[4].Text = "<b>" + ADA.ToString() + "</b>";
                GridView1.FooterRow.Cells[5].Text = "<b>" + ADADed.ToString() + "</b>";
                GridView1.FooterRow.Cells[6].Text = "<b>" + GSLI.ToString() + "</b>";
                GridView1.FooterRow.Cells[7].Text = "<b>" + GSLIDed.ToString() + "</b>";
                GridView1.FooterRow.Cells[8].Text = "<b>" + EGLS.ToString() + "</b>";
                GridView1.FooterRow.Cells[9].Text = "<b>" + EGLSDed.ToString() + "</b>";
                GridView1.FooterRow.Cells[10].Text = "<b>" + LIC.ToString() + "</b>";
                GridView1.FooterRow.Cells[11].Text = "<b>" + Total.ToString() + "</b>";
            }
            else
            {
                GridView1.DataSource = new string[] { };
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DivDetail.Visible = true;
            FillGrid();
            RetirmentANDNew();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}