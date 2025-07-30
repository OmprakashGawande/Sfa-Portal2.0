using System;
using System.Data;
using System.Web.UI;
using System.Globalization;

public partial class mis_Dashboard_PayrollGmDashboard : System.Web.UI.Page
{
    DataSet ds, dsRecord;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["Emp_ID"] != null)
            {
                ds = objdb.ByProcedure("SpPayrollDashboard", new string[] { "flag" }, new string[] { "1" }, "dataset");
                if (ds.Tables.Count != 0)
                {
                    spnMonth.InnerText = ds.Tables[0].Rows[0]["Salary_Month"].ToString();
                    spnYear.InnerText = ds.Tables[0].Rows[0]["Salary_Year"].ToString();
                    salaryDisbursed.InnerText = ds.Tables[0].Rows[0]["Salary_NetSalary"].ToString();
                    TotalsalaryDisbursed.InnerText = ds.Tables[0].Rows[0]["Salary_NetSalary"].ToString();
                    spnEmpSalaryDisbursed.InnerText = ds.Tables[0].Rows[0]["TotEmpSalaryDisbursed"].ToString();
                    spnTotalEmpSalaryDisbursed.InnerText = ds.Tables[0].Rows[0]["TotEmpSalaryDisbursed"].ToString();
                    spnEmpNotSlryDisbursment.InnerText = ds.Tables[0].Rows[0]["TotEmpSalaryNotDisbursed"].ToString();
                    spnTotalEmpNotSlryDisbursment.InnerText = ds.Tables[0].Rows[0]["TotEmpSalaryNotDisbursed"].ToString();

                    //Arrear
                    spnArrearMonth.InnerText = ds.Tables[0].Rows[0]["Salary_Month"].ToString();
                    spnArrearYear.InnerText = ds.Tables[0].Rows[0]["Salary_Year"].ToString();
                    spnArrearDistributed.InnerText = ds.Tables[1].Rows[0]["TotArrearDisbursed"].ToString();
                    spnTotalArrearDistributed.InnerText = ds.Tables[1].Rows[0]["TotArrearDisbursed"].ToString();
                    SpnEPF.InnerText = ds.Tables[1].Rows[0]["TotEPF"].ToString();
                    SpnTotalEPF.InnerText = ds.Tables[1].Rows[0]["TotEPF"].ToString();
                    spnEmpArrear.InnerText = ds.Tables[1].Rows[0]["TotEmpArrearDisbursed"].ToString();
                    spnTotalEmpArrear.InnerText = ds.Tables[1].Rows[0]["TotEmpArrearDisbursed"].ToString();
                }
            }
        }
    }
}