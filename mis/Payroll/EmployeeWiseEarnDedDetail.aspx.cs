using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Payroll_EmployeeWiseEarnDedDetail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Emp_ID"] != null && Request.QueryString["EmpArrearID"] != null)
        {
            ViewState["EmpArrearID"] = objdb.Decrypt(Request.QueryString["EmpArrearID"].ToString());
            ViewState["EmpID"] = objdb.Decrypt(Request.QueryString["Emp_ID"].ToString());
            FillEarning();
        }
        else
        {
            Response.Redirect("PayrollEmpArrearDetail.aspx");
        }
    }
    protected void FillEarning()
    {
        ds = objdb.ByProcedure("SpPayrollEmpArrear", new string[] { "flag", "EmpArrearID", "Emp_ID" }, new string[] { "3", ViewState["EmpArrearID"].ToString(), ViewState["EmpID"].ToString() }, "dataset");
        if (ds.Tables[0].Rows.Count != 0 && ds.Tables[1].Rows.Count != 0 && ds.Tables[2].Rows.Count != 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class='table table-bordered table-striped'>");
            sb.Append("<tr>");
            sb.Append("<th>Employee Name</th>");
            sb.Append("<td>" + ds.Tables[0].Rows[0]["Emp_Name"].ToString() + "</td>");
            sb.Append("<th>From Year</th>");
            sb.Append("<td>" + ds.Tables[0].Rows[0]["FromYear"].ToString() + "</td>");
            sb.Append("<th>From Month</th>");
            sb.Append("<td>" + ds.Tables[0].Rows[0]["FromMonthName"].ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th>To Year</th>");
            sb.Append("<td>" + ds.Tables[0].Rows[0]["ToYear"].ToString() + "</td>");
            sb.Append("<th>To Month</th>");
            sb.Append("<td>" + ds.Tables[0].Rows[0]["ToMonth"].ToString() + "</td>");
            sb.Append("<th>Basic Salary</th>");
            sb.Append("<td>" + ds.Tables[0].Rows[0]["BasicSalary"].ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th>Total Earning</th>");
            sb.Append("<td>" + ds.Tables[0].Rows[0]["TotalEarning"].ToString() + "</td>");
            sb.Append("<th>Policy</th>");
            sb.Append("<td>" + ds.Tables[0].Rows[0]["Policy"].ToString() + "</td>");
            sb.Append("<th>Total Deduction</th>");
            sb.Append("<td>" + ds.Tables[0].Rows[0]["TotalDeduction"].ToString() + "</td>");

            sb.Append("</tr>");
            sb.Append("</table>");
            DivDetail.InnerHtml = sb.ToString();
            DivDetail.Visible = true;


            RepeaterEarn.DataSource = ds.Tables[1];
            RepeaterEarn.DataBind();

            RepeaterDed.DataSource = ds.Tables[2];
            RepeaterDed.DataBind();

            txtNetPayment.Text = ds.Tables[0].Rows[0]["NetPaymentAmount"].ToString();
        }
    }
}