using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollAllEmpEarnDedDetail : System.Web.UI.Page
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
                    ddlOffice.Enabled = false;
                    if (ViewState["Office_ID"].ToString() == "1")
                    {
                        ddlOffice.Enabled = true;
                    }
                    ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag" },
                           new string[] { "10" }, "dataset");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlOffice.DataSource = ds;
                        ddlOffice.DataTextField = "Office_Name";
                        ddlOffice.DataValueField = "Office_ID";
                        ddlOffice.DataBind();
                        ddlOffice.Items.Insert(0, new ListItem("Select", "0"));

                        ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                    }
                    FillDropdown();
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
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DivDetail.InnerHtml = "";
            lblMsg.Text = "";
            if (ddlOffice.SelectedIndex > 0 && ddlFinancialYear.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
            {
                FillSalary();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillSalary()
    {
        try
        {
            DivDetail.InnerHtml = "";
            string Office_ID = ddlOffice.SelectedValue.ToString();
            string Salary_Year = ddlFinancialYear.SelectedValue.ToString();
            string Salary_MonthNo = ddlMonth.SelectedValue.ToString();


            //Bank_AccountNo
           // Bank_IfscCode
            ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "Salary_MonthNo", "Emp_TypeOfPost" }, new string[] { "10", Office_ID, Salary_Year, Salary_MonthNo, ddlEmp_TypeOfPost.SelectedValue.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                int Count = ds.Tables[0].Rows.Count;
                StringBuilder htmlStr = new StringBuilder();
                htmlStr.Append("<table class='datatable table table-hover table-bordered pagination-ys' id='SalaryTable'>");
                for (int i = 0; i < Count; i++)
                {

                    string Emp_ID = ds.Tables[0].Rows[i]["Emp_ID"].ToString();
                    if (ds.Tables[0].Rows[i]["Status"].ToString() == "NotGenerated")
                    {
                        string ff = "";
                    }
                    DataSet ds1 = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo" }, new string[] { "11", Emp_ID, Office_ID, Salary_Year, Salary_MonthNo }, "dataset");
                    if (ds1.Tables[0].Rows.Count != 0)
                    {
                        if (i == 0)
                        {
                            htmlStr.Append("<thead><tr><th style='width: 5%;'>");
                          //  htmlStr.Append("SNo.</th><th>STATUS</th><th>NAME OF EMPLOYEE </th><th>BASIC PAY</th>");
                            htmlStr.Append("SNo.</th><th>NAME OF EMPLOYEE </th><th>Account No.</th><th>IFSC Code</th><th>BASIC PAY</th>");
                            for (int j = 0; j < ds1.Tables[1].Rows.Count; j++)
                            {
                                htmlStr.Append("<th>" + ds1.Tables[1].Rows[j]["EarnDeduction_Name"].ToString() + "</th>");
                            }
                            htmlStr.Append("<th>TOTAL PAY</th>");
                         //   htmlStr.Append("<th>DEDUCTION (FOR ABSENT DAYS)</th>");
                            for (int j = 0; j < ds1.Tables[2].Rows.Count; j++)
                            {
                                htmlStr.Append("<th>" + ds1.Tables[2].Rows[j]["EarnDeduction_Name"].ToString() + "</th>");
                            }
                            htmlStr.Append("<th>LIC PREMIUM</th>");
                            htmlStr.Append("<th>TOTAL DEDUCTION</th>");
                            htmlStr.Append("<th>NET SALARY</th>");
                           

                            htmlStr.Append("</tr></thead><tbody>");
                        }
                       



                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td style=' text-align:left;'>" + (i + 1).ToString() + "</td>");
                        //htmlStr.Append("<td class='" + ds.Tables[0].Rows[i]["Status"].ToString() + "' style=' text-align:center;'>" + ds.Tables[0].Rows[i]["Status"].ToString() + "</td>");
                        //htmlStr.Append(" <td style=' text-align:left;'>" + ds1.Tables[0].Rows[0]["Emp_Name"].ToString() + " [" + ds1.Tables[0].Rows[0]["UserName"].ToString() + "]</td>");
                        htmlStr.Append(" <td style=' text-align:left;'>" + ds1.Tables[0].Rows[0]["Emp_Name"].ToString() + "</td>");
                        htmlStr.Append(" <td style=' text-align:left;'>'" + ds.Tables[0].Rows[i]["Bank_AccountNo"].ToString() + "'</td>");
                        htmlStr.Append(" <td style=' text-align:left;'>" + ds.Tables[0].Rows[i]["Bank_IfscCode"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds1.Tables[0].Rows[0]["Salary_Basic"].ToString() + "</td>");

                        for (int j = 0; j < ds1.Tables[1].Rows.Count; j++)
                        {
                            htmlStr.Append("<td>" + ds1.Tables[1].Rows[j]["Earning"].ToString() + "</td>");
                        }


                        htmlStr.Append("<td>" + ds1.Tables[0].Rows[0]["Salary_EarningTotal"].ToString() + "</td>");
                       // htmlStr.Append("<td>" + ds1.Tables[0].Rows[0]["Salary_NoDayDeduAmt"].ToString() + "</td>");

                        for (int j = 0; j < ds1.Tables[2].Rows.Count; j++)
                        {
                            htmlStr.Append("<td>" + ds1.Tables[2].Rows[j]["Earning"].ToString() + "</td>");
                        }


                        htmlStr.Append("<td>" + ds1.Tables[0].Rows[0]["PolicyDeduction"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds1.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString() + "</td>");
                       
                        htmlStr.Append("</tr>");




                    }
                }
                htmlStr.Append("</tbody>");
                htmlStr.Append("</table>");

                DivDetail.InnerHtml = htmlStr.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}
