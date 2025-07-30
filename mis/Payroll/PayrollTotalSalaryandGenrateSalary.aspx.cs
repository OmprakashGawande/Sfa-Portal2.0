using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollTotalSalaryandGenrateSalary : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    if (Request.QueryString["Parameter"] != null)
                    {
                        ViewState["TotalSalary"] = Request.QueryString["Parameter"].ToString();
                        if (ViewState["TotalSalary"].ToString() == "TotalSalary")
                        {
                            FillGrid();
                        }
                        else if (ViewState["TotalSalary"].ToString() == "SalaryGenerated")
                        {
                            FillDetail();
                        }
                        else
                        {

                        }
                    }

                }
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
            lblMsg.Text = "";
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpPayrollDashboard", new string[] { "flag" }, new string[] { "3" }, "dataset");
            if (ds != null && ds.Tables.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                decimal Totalsalary = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Salary_NetSalary"));
                GridView1.FooterRow.Cells[1].Text = "<b>| Total |</b>";
                GridView1.FooterRow.Cells[3].Text = Totalsalary.ToString();
            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDetail()
    {
        try
        {
            lblMsg.Text = "";
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpPayrollDashboard", new string[] { "flag" }, new string[] { "3" }, "dataset");
            if (ds != null && ds.Tables.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Columns[3].Visible = false;
            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}