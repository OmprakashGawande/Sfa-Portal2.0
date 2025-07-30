using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Net.Configuration;
using System.Configuration;

public partial class mis_HR_HREmpLeaveRequestsEmail : System.Web.UI.Page
{
    DataSet ds,ds9;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {

        DataSet ds1;
        string DocPath = "";
        string OrderDate = "";
        string msg = "";
        string TakenLeave = "";
        int DayCount = 0;
        decimal TotalTakenLeave = 0;
        /*************/
        //ViewState["Emp_ID"] = objdb.Decrypt(Request.QueryString["Emp_ID"].ToString());
        ViewState["EmpID"] = objdb.Decrypt(Request.QueryString["EmpID"].ToString());
        ViewState["LeaveId"] = Request.QueryString["LeaveId"].ToString();
        ViewState["LeaveType"] = Request.QueryString["LeaveType"].ToString();
        ViewState["Office_ID"] = objdb.Decrypt(Request.QueryString["Office"].ToString());
        string FinancialYear = Request.QueryString["FY"].ToString();
        string LeaveStatus = Request.QueryString["LeaveStatus"].ToString();
        string LeaveMessage = "";
        //string LeaveStatuss = "?LeaveStatus=Approved&FY=2019&LeaveId=10237&LeaveType=2&EmpID=526&Office=1";
        /*************/

        ds9 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId" },
        new string[] { "32", ViewState["LeaveId"].ToString() }, "datatset");
        if (ds9 != null || ds9.Tables.Count != 0){
        if (ds9.Tables[0].Rows[0]["LeaveStatus"].ToString() == "Approved")
        {
            LeaveMessage = "<p style='text-align:center; color:green;  font-weight:bold; '>This leave has been already approved, it will not change. </p>";

        }
        else if (ds9.Tables[0].Rows[0]["LeaveStatus"].ToString() == "Rejected")
        {
            LeaveMessage = "<p style='text-align:center; color:red; font-weight:bold; '>This leave has been already rejected, it will not change. </p>";

        }
        else
        {
            

            OrderDate = "";
            ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveType_ID", "Financial_Year" },
            new string[] { "7", ViewState["LeaveType"].ToString(), FinancialYear.ToString() }, "datatset");

            string financialYear = ds.Tables[0].Rows[0]["Financial_Year"].ToString();
            string LeaveDay1 = ds.Tables[0].Rows[0]["Leave_Days"].ToString();

            if (LeaveStatus == "Approved")
            {
                ds1 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId" },
            new string[] { "8", ViewState["LeaveId"].ToString() }, "datatset");
                if (ds1 != null && ds1.Tables.Count != 0)
                {
                    if (ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "First Half" || ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "Second Half" || ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "Half Day")
                    {
                        TakenLeave = "0.5";
                        TotalTakenLeave = Convert.ToDecimal(TakenLeave.ToString());
                    }
                    else
                    {
                        int count = ds1.Tables[1].Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            string DayName = ds1.Tables[1].Rows[i]["DayName"].ToString();
                            string Leave_Date = ds1.Tables[1].Rows[i]["LeaveDate"].ToString();
                            if (DayName == "Sunday")
                            {
                                DayCount = DayCount;
                            }
                            else
                            {
                                int status = 1;
                                DataSet ds2 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId" },
             new string[] { "21", ViewState["LeaveId"].ToString() }, "datatset");
                                if (ds1 != null)
                                {
                                    int leaveCount = ds2.Tables[0].Rows.Count;
                                    if (ds2 != null && ds2.Tables[0].Rows.Count != 0)
                                    {
                                        for (int j = 0; j < leaveCount; j++)
                                        {
                                            string Holiday_Date = ds2.Tables[0].Rows[j]["Holiday_Date"].ToString();
                                            if (Leave_Date == Holiday_Date)
                                            {
                                                status = 0;
                                                break;
                                            }
                                            else
                                            {
                                                status = 1;
                                            }
                                        }
                                    }
                                    if (status == 1)
                                    {
                                        DayCount++;
                                    }
                                    else
                                    {
                                        DayCount = DayCount;
                                    }
                                }
                            }
                        }
                        TotalTakenLeave = DayCount;
                        //TotalTakenLeave = Convert.ToDecimal(ds1.Tables[0].Rows[0]["TakenLeave"].ToString());
                    }
                }

                LeaveMessage = "<p style='text-align:center; color:green'>Thank you for your action, this leave has been approved; </p>";
            }
            else
            {
                LeaveMessage = "<p style='text-align:center; color:red'>Thank you for your action, this leave has been rejected; </p>";
                TotalTakenLeave = 0;
            }
            objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "Emp_ID", "LeaveType", "TotalAllowedLeave", "TotalTakenLeave", "FinancialYear" },
            new string[] { "2", ViewState["EmpID"].ToString(), ViewState["LeaveType"].ToString(), LeaveDay1, TotalTakenLeave.ToString(), financialYear }, "datatset");


            objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId", "LeaveStatus", "RemarkByApprovalAuth", "LeaveApprovalOrderNo", "LeaveApprovalOrderDate", "LeaveApprovalOrderFile" },
            new string[] { "9", ViewState["LeaveId"].ToString(), LeaveStatus, "", "", OrderDate, DocPath }, "datatset");


            /***********SEND SMS TO EMPLOYEE****************/
            DataSet ds3;
            ds3 = objdb.ByProcedure("SpHRLeaveApplication",
                new string[] { "flag", "LeaveId" },
                new string[] { "31", ViewState["LeaveId"].ToString() }, "datatset");
            if (ds3.Tables[0].Rows.Count > 0)
            {
                string EmpMobileNo = ds3.Tables[0].Rows[0]["Emp_MobileNo"].ToString();
                string EmpName = ds3.Tables[0].Rows[0]["Emp_Name"].ToString();
                string LeaveType = ds3.Tables[0].Rows[0]["Leave_Type"].ToString();
                string LeaveFromDate = ds3.Tables[0].Rows[0]["LeaveFromDate"].ToString();
                string LeaveToDate = ds3.Tables[0].Rows[0]["LeaveToDate"].ToString();
                string EmpEmail = ds3.Tables[0].Rows[0]["Emp_Email"].ToString();
                if (EmpEmail != "" && EmpEmail != null)
                {
                    string Empmessage = "";
                    if (TotalTakenLeave.ToString() == "0.5")
                    {
                        Empmessage = "Your leave request for half day of " + LeaveFromDate + "  has been " + LeaveStatus + ".";
                    }
                    else
                    {
                        Empmessage = "Your leave request from " + LeaveFromDate + " to " + LeaveToDate + " has been " + LeaveStatus + ".";
                    }
                    sendmail(EmpEmail, "Leave Approval Request Response",Empmessage);
                   // Empmessage = "Your leave request from " + LeaveFromDate + " to " + LeaveToDate + " has been " + ddlStatus.SelectedItem.Text + ".";
                    //string link = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=3597C1493C124F&type=unicode&routeid=2&contacts=" + EmpMobileNo + "&senderid=MPAGRO&msg=" + Empmessage;
                    //HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
                    //HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    //Stream stream = response.GetResponseStream();
                }

            }
        }
        }

        lblMsg.Text = LeaveMessage.ToString();
    }
    private void sendmail(string TO, string subJect, string content)
    {
        try
        {
            //  string AttachedEmailHTMLPath = Server.MapPath("~/HtmlTemplete/OIC_Email_Templete.html");
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            using (MailMessage mm = new MailMessage(smtpSection.From, TO))
            {
                mm.Subject = subJect;
                mm.Body = content;
                mm.IsBodyHtml = true;
                //mm.CC.Add(CC);
                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtpSection.Network.Host;
                smtp.EnableSsl = smtpSection.Network.EnableSsl;
                NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                smtp.Credentials = networkCred;
                smtp.Port = smtpSection.Network.Port;
                smtp.Send(mm);
                //HttpContext.Current.Response.End();
              //  Page.ClientScript.RegisterStartupScript(this.GetType(), "alertMessage", "alert('Email sent.');", false);
            }
        }
        catch (Exception ex)
        {

        }
    }
}