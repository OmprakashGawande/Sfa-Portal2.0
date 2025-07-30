using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;

public partial class mis_Payroll_PayrollCoverLetter : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected string TotalSalary;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Office_ID"] == null && Request.QueryString["Salary_Year"] == null && Request.QueryString["Salary_Month"] == null && Request.QueryString["Emp_TypeOfPost"] == null)
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
                ds = objdb.ByProcedure("SpPayrollSalaryDetail",
                    new string[] { "flag", "Office_ID", "Salary_Year", "Salary_Month", "Emp_TypeOfPost" },
                    new string[] { "16", Request.QueryString["Office_ID"].ToString(), Request.QueryString["Salary_Year"].ToString(), Request.QueryString["Salary_Month"].ToString(), Request.QueryString["Emp_TypeOfPost"].ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                    //Calculate Sum and display in Footer Row
                    decimal total = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Salary_NetSalary"));
                    GridView1.FooterRow.Cells[1].Text = "कुल राशि";
                    GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;

                    GridView1.FooterRow.Cells[2].Text = total.ToString("N2");
                    TotalSalary = total.ToString();

                    lblDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                }
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
}