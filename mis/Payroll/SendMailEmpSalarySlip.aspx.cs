using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;


public partial class mis_Payroll_SendMailEmpSalarySlip : System.Web.UI.Page
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
                    ddlYear.Items.Insert(0, new ListItem("Select", "0"));
                    if (ViewState["Office_ID"].ToString() == "1")
                    {
                        ddlOfficeName.Enabled = true;
                    }
                    else
                    {
                        ddlOfficeName.Enabled = false;
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
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            }
            ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
            ds.Reset();
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            if(ddlOfficeName.SelectedIndex == 0)
            {
                msg += "Select Office. \\n";
            }
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year. \\n";
            }
            if (ddlMonth.SelectedIndex == 0)
            {
                msg += "Select Month. \\n";
            }
            if (ddlEmpType.SelectedIndex == 0)
            {
                msg += "Select Employee Type. \\n";
            }
            if(msg == "")
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
            //DivSlip.Visible = false;
            //lblNotGenerated.Visible = false;
           
                ds = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Office_ID", "Salary_Year", "Salary_MonthNo", "Emp_TypeOfPost" }, new string[] { "18",ddlOfficeName.SelectedValue.ToString(),ddlYear.SelectedValue.ToString(),ddlMonth.SelectedValue.ToString(),ddlEmpType.SelectedValue.ToString()}, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    //int Count = Convert.ToInt16(ds.Tables[1].Rows[0]["SalaryCount"].ToString());
                    int Count = ds.Tables[0].Rows.Count;
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < 1; i++)
                    {
                        string Emp_ID = ds.Tables[0].Rows[i]["Emp_ID"].ToString();
                        string Emp_Email = ds.Tables[0].Rows[i]["Emp_Email"].ToString();
                        DataSet ds1 = objdb.ByProcedure("SpPayrollSalaryDetail", new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo" }, new string[] { "2", Emp_ID,ddlOfficeName.SelectedValue.ToString(),ddlYear.SelectedValue.ToString(),ddlMonth.SelectedValue.ToString()}, "dataset");
                        if (ds1.Tables[0].Rows.Count != 0)
                        {
                            sb.Append("<div class='container'>");
                            sb.Append("<div class='content-wrapper'>");
                            sb.Append("<section class='content watermark' style='padding-top: 0px; height: 60px;'>");
                            sb.Append("<div style='width:21cm;  display:block; border:1px dashed lightgrey; color: black; margin-bottom:5px; overflow:hidden;'> ");
                            sb.Append("<div style='text-align:center'>");
                            sb.Append("<h3 style='font-weight:100'>");
                            sb.Append("<img src='http://45.114.143.215:8020/assets/images/1b6be679-a0e3-4c9e-aa9f-c853d17268f1.png' class='salary-logo'>");
                            sb.Append("&nbsp;&nbsp; THE M.P. STATE AGRO INDUSTRIES DEVELOPMENT CORPORATION LTD.  HEAD OFFICE <br/>");
                            sb.Append("<span class='subheading-salary'>PAY SLIP FOR THE MONTH OF <span id='lblMonth' runat='server'>" + ds1.Tables[0].Rows[0]["Month"] + " / " + ddlYear.SelectedValue.ToString() + "</span>&nbsp; <span id='lblFinancialYear' runat='server'></span>&nbsp; <span style='color: red;' id='lblGenStatus' runat='server'></span></span></h3>");
                            sb.Append("<table class='table table-bordered' style='font-family: monospace;font-size: 13px;'>");
                            sb.Append("<tbody>");

                            sb.Append("<tr>");
                            sb.Append("<th style='width: 106px !important; background-color:#eaeaea; style='text-align:left;'>EMPLOYEE NAME:</th>");
                            sb.Append("<td style='text-align:left; '>" + ds1.Tables[0].Rows[0]["Emp_Name"].ToString() + "</td>");
                            sb.Append("<th style='width: 77px !important; background-color:#eaeaea; style='text-align:left;'>BANK A/C:</th>");
                            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["Bank_AccountNo"].ToString() + "</td>");
                            sb.Append("<th style='width: 84px !important; background-color:#eaeaea; style='text-align:left;'>EPF No:</th>");
                            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["EPF_No"].ToString() + "</td>");
                            sb.Append("</tr>");

                            sb.Append("<tr>");
                            sb.Append("<th style='background-color:#eaeaea; style='text-align:left;'>DESIGNATION:</th>");
                            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["Designation_Name"].ToString() + "</td>");
                            sb.Append("<th style='background-color:#eaeaea; style='text-align:left;'>BANK NAME:</th>");
                            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["Bank_Name"].ToString() + "</td>");
                            sb.Append("<th style='background-color:#eaeaea; style='text-align:left;'>G.INS No:</th>");
                            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["GroupInsurance_No"].ToString() + "</td>");

                            sb.Append("</tr>");

                            sb.Append("<tr>");
                            sb.Append("<th style='background-color:#eaeaea; style='text-align:left;'>EMPLOYEE CODE:</th>");
                            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["UserName"].ToString() + "</td>");
                            sb.Append("<th style='background-color:#eaeaea; style='text-align:left;'>IFSC CODE:</th>");
                            sb.Append("<td style='text-align:left;'>" + ds1.Tables[0].Rows[0]["Bank_IfscCode"].ToString() + "</td>");
                            sb.Append("<th style='background-color:#eaeaea; style='text-align:left;'>NET SALARY:</th>");
                            sb.Append("<td style='text-align:right;'>" + ds1.Tables[0].Rows[0]["Salary_NetSalary"].ToString() + "</td>");
                            sb.Append("</tr>");

                            sb.Append("</tbody>");
                            sb.Append("</table>");
                            //Earning
                            sb.Append("<table class='table table-bordered table-striped' style='margin-bottom: 0px;font-family: monospace;font-size: 13px; width: 100%;'>");
                            sb.Append("<tbody>");
                            sb.Append("<tr>");
                            sb.Append("<td><h4 style='margin-bottom: 3px; font-weight:100'>PAY</h4></td>");
                            sb.Append("<td><h4 style='margin-bottom: 3px; font-weight:100'>DEDUCTIONS</h4></td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td style='width: 50%'>");
                            sb.Append("<div class='table-responsive'>");
                            sb.Append("<div>");
                            sb.Append("<table class='table table-bordered table-striped Grid earning-table' style='width: 100%; margin-bottom:110px;'>");
                            sb.Append("<tbody>");
                            sb.Append("<tr>");
                            sb.Append("<th style='text-align: left; background-color:#eaeaea;'>BASIC SALARY :</th>");
                            sb.Append("<td style='text-align:right;'>" + ds1.Tables[0].Rows[0]["Salary_Basic"].ToString() + "</td>");
                            sb.Append("</tr>");
                            //Earning Repeater
                            if (ds1.Tables[1].Rows.Count != 0)
                            {
                                for (int j = 0; j < ds1.Tables[1].Rows.Count; j++)
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<th style='text-align: left; background-color:#eaeaea;'>" + ds1.Tables[1].Rows[j]["EarnDeduction_Name"].ToString() + ":</th>");
                                    sb.Append("<td style='text-align:right;'>" + ds1.Tables[1].Rows[j]["Earning"].ToString() + "</td>");
                                    sb.Append("</tr>");
                                }
                            }
                            sb.Append("<tr class='total_salary'>");
                            sb.Append("<th style='text-align: left; background-color:#eaeaea;'>TOTAL PAY :</th>");
                            sb.Append("<th style='text-align:right;'>" + ds1.Tables[0].Rows[0]["Salary_EarningTotal"].ToString() + "</th>");
                            sb.Append("</tr>");
                            sb.Append("</tbody>");
                            sb.Append("</table>");
                            sb.Append("</div>");
                            sb.Append("</div>");
                            sb.Append("</td>");
                            //Deduction
                            sb.Append("<td style='width: 50%'>");
                            sb.Append("<div class='table-responsive'>");
                            sb.Append("<div>");
                            sb.Append("<table class='table table-bordered table-striped Grid earning-table' style='width: 100%;'>");
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
                                    sb.Append("<th style='text-align: left; background-color:#eaeaea;'>" + ds1.Tables[2].Rows[k]["EarnDeduction_Name"].ToString() + ":</th>");
                                    sb.Append("<td style='text-align:right;'>" + ds1.Tables[2].Rows[k]["Earning"].ToString() + "</td>");
                                    sb.Append("</tr>");
                                }
                            }
                            sb.Append("<tr>");
                            sb.Append("<th style='text-align:left; background-color:#eaeaea;'>POLICY :</th>");
                            sb.Append("<th style='text-align:right; background-color:#eaeaea;'>" + ds1.Tables[0].Rows[0]["PolicyDeduction"].ToString() + "</th>");
                            sb.Append("</tr>");
                            sb.Append("<tr class='total_salary'>");
                            sb.Append("<th style='text-align: left; background-color:#eaeaea;'>TOTAL DEDUCTION:</th>");
                            sb.Append("<th style='text-align:right; background-color:#eaeaea;'>" + ds1.Tables[0].Rows[0]["Salary_DeductionTotal"].ToString() + "</th>");
                            sb.Append("</tr>");
                            sb.Append("</tbody>");
                            sb.Append("</table>");
                            sb.Append("</div>");
                            sb.Append("</div>");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<th style='text-align: left; font-size: 11px; background-color:#eaeaea;'>WISH YOU A VERY HAPPY NEW YEAR !!</th>");
                            sb.Append("<th style='text-align: left; font-size: 11px; background-color:#eaeaea;'>THIS IS A COMPUTER GENERATED PAYSLIP, SIGNATURE NOT REQUIRED</th>");
                            sb.Append("</tr>");
                            /*sb.Append("<tr>");
                            sb.Append("<th colspan='2'>WISH YOU A VERY HAPPY NEW YEAR !!</th>");
                            sb.Append("</tr>");*/
                            sb.Append("</tbody>");
                            sb.Append("</table>");
                            sb.Append("</div>");
                            sb.Append("</div>");
                            sb.Append("</section>");
                            sb.Append("</div>");
                            sb.Append("</div>");
                            
                            //DivSlip.InnerHtml = sb.ToString();
                            //DivSlip.Visible = true;
                            MailMessage mail = new MailMessage();
                            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 25);
                            SmtpServer.EnableSsl = false;

                            mail.From = new MailAddress("carempagro@gmail.com");
                           mail.To.Add("raghuwanshimohini07@gmail.com");
                            mail.Subject = "Salary Slip";

                            mail.IsBodyHtml = true;
                            string htmlBody;
                            htmlBody = "<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <title></title><link href='https://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet'> <style> .Grid td {             padding: 3px !important;         }              .Grid td input {                 padding: 3px 3px !important;                 text-align: right !important;                 font-size: 12px !important;                 height: 26px !important;             }          .Grid th {             text-align: center;         }          .ss {             text-align: left !important;         }          .bgcolor {             background-color: #eeeeee !important;         }          .box {             min-height: initial !important;         } .table-striped > tbody > tr:nth-of-type(odd) {   background-color: #f9f9f9; } .content {min-height: 700px; } .box { position: relative;border-radius: 3px;background: #ffffff;border-top: 3px solid #d2d6de;margin-bottom: 20px; width: 100%;box-shadow: 0 1px 1px rgba(0,0,0,0.1);box-shadow: none;border-top: none; }.table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {border: 1px solid #e1e1e1;}.text-center h3 {font-size: 15px; font-family: monospace;}.table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {padding: 0px 2px;}#subheading-salary {font-size: 13px;}.salary-logo {-webkit-filter: grayscale(100%);filter: grayscale(100%);width: 40px;         }          .printbutton {             border-top: 1px dashed #838383;             margin-top: 5px;             padding-top: 5px;         }          table h4 {             font-size: 15px;         }          .table {             margin-bottom: 5px;         }          th, td, h3 {             text-transform: uppercase !important;         }         .watermark {   width: 300px;   height: 100px;   display: block;   position: relative; }  .watermark::after {   content:'';  background:url('http://45.114.143.215:8020/assets/images/1b6be679-a0e3-4c9e-aa9f-c853d17268f1.png');   opacity: 0.2;   top: 0;   left: 0;   bottom: 0;   right: 0;   position: absolute;   z-index: -1;   }</style></head><body style='font-family: ' open sans', sans-serif;'>" + sb.ToString() + "</body></html>";
                            mail.Body = htmlBody;
                            
                            //SmtpServer.Port = 587;
                            SmtpServer.Credentials = new System.Net.NetworkCredential("carempagro@gmail.com","mpagro@123");
                            SmtpServer.EnableSsl = true;

                            SmtpServer.Send(mail);
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        }
                    }
                }
                else
                {
                    //lblNotGenerated.Visible = true;
                }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}