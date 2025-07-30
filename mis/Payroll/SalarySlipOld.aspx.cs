using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

public partial class mis_Payroll_SalarySlip : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Emp_ID"] != null)
            {
                if (Request.QueryString["Year"] != null && Request.QueryString["Month"] != null)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office"] = objdb.Decrypt(Request.QueryString["Office"].ToString());
                    ViewState["Year"] = objdb.Decrypt(Request.QueryString["Year"].ToString());
                    ViewState["Month"] = objdb.Decrypt(Request.QueryString["Month"].ToString());
                    DivSlip.Visible = false;
                    //lblNotGenerated.Visible = false;
                    FillSalary();
                }
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillSalary()
    {
        try
        {
            DivSlip.Visible = false;
            //lblNotGenerated.Visible = false;
            if (ViewState["Year"].ToString() != null && ViewState["Month"].ToString() != null)
            {
                ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Salary_Year", "Salary_MonthNo" }, new string[] { "6", ViewState["Year"].ToString(), ViewState["Month"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count != 0 )
                {
                    //int Count = Convert.ToInt16(ds.Tables[1].Rows[0]["SalaryCount"].ToString());
                    int Count = ds.Tables[0].Rows.Count;
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < Count; i++)
                    {
                        string Emp_ID = ds.Tables[0].Rows[i]["Emp_ID"].ToString();
                        DataSet ds1 = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo" }, new string[] { "2", Emp_ID, ViewState["Office"].ToString(), ViewState["Year"].ToString(), ViewState["Month"].ToString() }, "dataset");
                        if (ds1.Tables[0].Rows.Count != 0)
                        {
                            sb.Append("<div style='width:21cm; height:15.5cm; display:block; border:1px dashed lightgrey; color: black; margin-bottom:5px; overflow:hidden;'> ");
                            sb.Append("<div class='text-center' style='padding:5px;' >");
                            sb.Append("<h3 class=''>");
                            sb.Append("<img src='../image/mpagro-logo.png' class='salary-logo'>");
                            sb.Append("&nbsp;&nbsp; THE M.P. STATE AGRO INDUSTRIES DEVELOPMENT CORPORATION LTD.  HEAD OFFICE <br>");
                            sb.Append("<span id='subheading-salary'>PAY SLIP FOR THE MONTH OF <span id='lblMonth' runat='server'>" + ds1.Tables[0].Rows[0]["Month"] + " / " + ViewState["Year"] + "</span>&nbsp; <span id='lblFinancialYear' runat='server'></span>&nbsp; <span style='color: red;' id='lblGenStatus' runat='server'></span></span></h3>");
                            sb.Append("<table class='table table-bordered' style='font-family: monospace;font-size: 13px;'>");
                            sb.Append("<tbody>");

                            sb.Append("<tr>");
                            sb.Append("<th style='width: 106px !important;'>EMPLOYEE NAME:</th>");
                            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["Emp_Name"].ToString() + "</td>");
                            sb.Append("<th style='width: 77px !important;'>BANK A/C:</th>");
                            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["Bank_AccountNo"].ToString() + "</td>");
                            sb.Append("<th style='width: 84px !important;'>EPF No:</th>");
                            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["EPF_No"].ToString() + "</td>");
                            sb.Append("</tr>");

                            sb.Append("<tr>");
                            sb.Append("<th>DESIGNATION:</th>");
                            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["Designation_Name"].ToString() + "</td>");
                            sb.Append("<th>BANK NAME:</th>");
                            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["Bank_Name"].ToString() + "</td>");
                            sb.Append("<th>G.INS No:</th>");
                            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["GroupInsurance_No"].ToString() + "</td>");
                            
                            sb.Append("</tr>");

                            sb.Append("<tr>");
                            sb.Append("<th>EMPLOYEE CODE:</th>");
                            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["UserName"].ToString() + "</td>");
                            sb.Append("<th>IFSC CODE:</th>");
                            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["Bank_IfscCode"].ToString() + "</td>");
                            sb.Append("<th>NET SALARY:</th>");
                            sb.Append("<td style='text-align:right;'>" + ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString() + "</td>");
                            sb.Append("</tr>");

                            sb.Append("</tbody>");
                            sb.Append("</table>");
                            //Earning
                            sb.Append("<table class='table table-striped' style='margin-bottom: 0px;font-family: monospace;font-size: 13px;'>");
                            sb.Append("<tbody>");
                            sb.Append("<tr>");
                            sb.Append("<td style='width: 50%'>");
                            sb.Append("<div class='' style='padding-bottom: 0; padding-left: 5px; font-weight: 700;'>");
                            sb.Append("<h4 style='margin-bottom: 3px;'>PAY</h4>");
                            sb.Append("</div>");
                            sb.Append("<div class='table-responsive'>");
                            sb.Append("<div>");
                            sb.Append("<table class='table table-bordered table-striped Grid earning-table'>");
                            sb.Append("<tbody>");
                            sb.Append("<tr>");
                            sb.Append("<th>BASIC SALARY :</th>");
                            sb.Append("<td style='text-align:right;'>" + ds1.Tables[0].Rows[0]["Salary_Basic"].ToString() + "</td>");
                            sb.Append("</tr>");
                            //Earning Repeater
                            if (ds1.Tables[1].Rows.Count != 0)
                            {
                                for (int j = 0; j < ds1.Tables[1].Rows.Count; j++)
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<th>" + ds1.Tables[1].Rows[j]["EarnDeduction_Name"].ToString() + ":</th>");
                                    sb.Append("<td style='text-align:right;'>" + ds1.Tables[1].Rows[j]["Earning"].ToString() + "</td>");
                                    sb.Append("</tr>");
                                }
                            }
                            sb.Append("<tr class='total_salary'>");
                            sb.Append("<th>TOTAL PAY :</th>");
                            sb.Append("<th style='text-align:right;'>" + ds1.Tables[0].Rows[0]["Salary_EarningTotal"].ToString() + "</th>");
                            sb.Append("</tr>");
                            sb.Append("</tbody>");
                            sb.Append("</table>");
                            sb.Append("</div></div>");
                            sb.Append("</td>");
                            //Deduction
                            sb.Append("<td style='width: 50%'>");
                            sb.Append("<div class=''>");
                            sb.Append("<div class='' style='padding-bottom: 0; padding-left: 5px; font-weight: 700;'>");
                            sb.Append("<h4 style='margin-bottom: 3px;'>DEDUCTIONS</h4>");
                            sb.Append("</div>");
                            sb.Append("<div class=''>");
                            sb.Append("<div class='table-responsive'>");
                            sb.Append("<div>");
                            sb.Append("<table class='table table-bordered table-striped Grid earning-table'>");
                            sb.Append("<tbody>");
                            /*sb.Append("<tr>");
                            sb.Append("<th>SALARY DEDUCTION (For Absent Days) :</th>");
                            sb.Append("<td style='text-align:right;'>" + ds1.Tables[0].Rows[0]["Salary_NoDayDeduAmt"].ToString() + "</td>");
                            sb.Append("</tr>");*/
                            //Deduction Repeater
                            if (ds1.Tables[2].Rows.Count != 0)
                            {
                                for (int k = 0; k < ds1.Tables[2].Rows.Count; k++)
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<th>" + ds1.Tables[2].Rows[k]["EarnDeduction_Name"].ToString() + ":</th>");
                                    sb.Append("<td style='text-align:right;'>" + ds1.Tables[2].Rows[k]["Earning"].ToString() + "</td>");
                                    sb.Append("</tr>");
                                }
                            }
                            sb.Append("<tr>");
                            sb.Append("<th>POLICY :</th>");
                            sb.Append("<th style='text-align:right;'>" + ds1.Tables[0].Rows[0]["PolicyDeduction"].ToString() + "</th>");
                            sb.Append("</tr>");
                            sb.Append("<tr class='total_salary'>");
                            sb.Append("<th>TOTAL DEDUCTION:</th>");
                            sb.Append("<th style='text-align:right;'>" + ds1.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString() + "</th>");
                            sb.Append("</tr>");
                            sb.Append("</tbody>");
                            sb.Append("</table>");
                            sb.Append("</div></div></div>");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<th style='font-size:11px;'>WISH YOU A VERY HAPPY NEW YEAR !!</th>");
                            sb.Append("<th style='text-align: right; font-size:11px'>THIS IS A COMPUTER GENERATED PAYSLIP, SIGNATURE NOT REQUIRED</th>");
                            sb.Append("</tr>");
                            /*sb.Append("<tr>");
                            sb.Append("<th colspan='2'>WISH YOU A VERY HAPPY NEW YEAR !!</th>");
                            sb.Append("</tr>");*/
                            sb.Append("</tbody>");
                            sb.Append("</table>");
                            sb.Append("</div>");
                            sb.Append("</div>");
                            DivSlip.InnerHtml = sb.ToString();
                            DivSlip.Visible = true;
                        }
                    }
                }
                else
                {
                    //lblNotGenerated.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}