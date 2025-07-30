using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HRLoginPermission : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    txtLoginTime.Text = "10:00 AM";
                    txtLogOutTime.Text = "05:30 PM";
                    txtApplyingLoginDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtApplyingLogoutDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    
                    //ViewState["Name"] = Session["Name"].ToString();
                    // ManageField();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string[] arrtime = new string[] { };
            string[] arrdiff = new string[] { };
            string[] time = new string[] { };
            TimeSpan LogTime = new TimeSpan();
            TimeSpan LogDiff = new TimeSpan();
            TimeSpan ab = new TimeSpan();
            string PermissionType = "", ApplyingDate="";
            string Remark = "";
            ViewState["Time"] = "";
            string LoginLogout = "";
            if (rbtnLateLogin.Checked == true)
            {
                arrtime = txtLoginTime.Text.Split(':');
                LoginLogout = txtLoginTime.Text;
                time = arrtime[1].Split(' ');
                if (txtLoginTime.Text.Contains("PM") && arrtime[0] == "12")
                {
                    ViewState["Time"] = arrtime[0];
                }
                if (txtLoginTime.Text.Contains("PM") && arrtime[0] != "12")
                {
                    ViewState["Time"] = (12 + Int32.Parse(arrtime[0])).ToString();
                }
                if (txtLoginTime.Text.Contains("AM") && arrtime[0] == "12")
                {
                    ViewState["Time"] = "0";
                }
                if (txtLoginTime.Text.Contains("AM") && arrtime[0] != "12")
                {
                    ViewState["Time"] = arrtime[0];
                }
                string Time = ViewState["Time"].ToString() + ":" + time[0];
                LogTime = TimeSpan.Parse(Time.ToString());
                TimeSpan CorrectLoginTime = TimeSpan.Parse("10:00");
                LogDiff = LogTime - CorrectLoginTime;
                int aa = Math.Abs(LogDiff.Minutes);
                ab = TimeSpan.FromMinutes(aa);
                // ab = TimeSpan.Parse(aa.ToString());
                PermissionType = "Late Login";
                Remark = txtLoginRemark.Text;
                txtLogoutDiffTime.Text = "";
                txtLogoutRemark.Enabled = false;
                //txtApplyingLoginDate.Enabled = false;                
                ApplyingDate = Convert.ToDateTime(txtApplyingLoginDate.Text, cult).ToString("yyyy-MM-dd");
            }
            if (rbtnEarlyLogout.Checked == true)
            {
                LoginLogout = txtLogOutTime.Text;
                arrtime = txtLogOutTime.Text.Split(':');
                time = arrtime[1].Split(' ');
                if (txtLogOutTime.Text.Contains("PM") && arrtime[0] == "12")
                {
                    ViewState["Time"] = arrtime[0];
                }
                if (txtLogOutTime.Text.Contains("PM") && arrtime[0] != "12")
                {
                    ViewState["Time"] = (12 + Int32.Parse(arrtime[0])).ToString();
                }
                if (txtLogOutTime.Text.Contains("AM") && arrtime[0] == "12")
                {
                    ViewState["Time"] = "0";
                }
                if (txtLogOutTime.Text.Contains("AM") && arrtime[0] != "12")
                {
                    ViewState["Time"] = arrtime[0];
                }
                string Time = ViewState["Time"].ToString() + ":" + time[0];
                LogTime = TimeSpan.Parse(Time.ToString());
                TimeSpan CorrectLogOutTime = TimeSpan.Parse("17:30");
                LogDiff = CorrectLogOutTime - LogTime;
                int aa = Math.Abs(LogDiff.Minutes);
                ab = TimeSpan.FromMinutes(aa);
                PermissionType = "Early Logout";
                Remark = txtLogoutRemark.Text;
                txtLoginDiffTime.Text = "";
                txtLoginRemark.Enabled = false;
                ApplyingDate = Convert.ToDateTime(txtApplyingLogoutDate.Text, cult).ToString("yyyy-MM-dd");
                //txtApplyingLogoutDate.Enabled = false;
            }
            if (Remark == "")
            {
                msg += "Enter Remark.\n";
                SetFocus(txtLoginRemark);
            }

            if (msg == "")
            {
                ds = null;
                ds = objdb.ByProcedure("SpHRLoginPermission",
                    new string[] { "flag", "Emp_ID", "PermissionType", "ApplyingDate" },
                    new string[] { "7", ViewState["Emp_ID"].ToString(), PermissionType, ApplyingDate }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count == 0)
                {
                    ds = objdb.ByProcedure("SpHRLoginPermission",
                                       new string[] { "flag", "Emp_ID", "PermissionType", "Login_LogoutTime", "DiffTime", "Emp_Remark", "ApplyingDate" },
                                       new string[] { "0", ViewState["Emp_ID"].ToString(), PermissionType, LogTime.ToString(), ab.ToString(), Remark, ApplyingDate }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");

                    string LoginLogoutId = ds.Tables[0].Rows[0]["LoginLogoutID"].ToString();

                    //MailMessage mail = new MailMessage();
                    //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 25);
                    //SmtpServer.EnableSsl = false;

                    //mail.From = new MailAddress("sfatechgroup@gmail.com");

                    ////mail.To.Add("deshmukhhimani26@gmail.com");
					
                    ////mail.To.Add("himanshu61@gmail.com,richakhanna30@gmail.com,nagar.shikha@gmail.com");
					
                    //mail.To.Add("richakhanna30@gmail.com");
					
                    //mail.Subject = "Login Logout Permission";

                    //mail.IsBodyHtml = true;
                    //string htmlBody;
                    //string url ="";
                    //if (PermissionType == "Late Login")
                    //{
                    //    htmlBody = "<html xmlns='http://www.w3.org/1999/xhtml'> <head>     <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />     <title></title>     <link href='https://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet'> </head> <body style='font-family: ' open sans', sans-serif;'>     <table width='100%' border='0' cellspacing='0' cellpadding='0'>   <tr >     <td align='center' valign='top' bgcolor='#f5f5f5' style='background-color: rgba(54, 153, 211, 0.25);padding:60px 0;'>     <table width='600' border='0' cellspacing='0' cellpadding='0'>       <tr>         <td align='left' valign='top'  style='background-color:white;border-bottom:2px dashed gray; font-size:13px; color:#ffffff; padding:25px 15px;border-radius: 10px 10px 0 0;'>         <div align='center'><img src='http://www.sfatechgroup.com/img/logo.png'></div>           </td>       </tr>               <tr>         <td align='left' valign='top' bgcolor='#1ba5db' style='background-color:#fff;color:#222; padding:35px 15px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>           <tr>             <td align='left' valign='top'>             <table width='100%' border='0' cellspacing='0' cellpadding='0'>             	<tr>                 	<td align='left' valign='top'> 	 <h4 style='margin: 0 0 10px 0;'>Login Logout Permission,</h4> 	   </tr>                 <tr>                     <td align='left' valign='top'> <p style='margin: 0 0 10px 0;font-size: 14px;'>Date :" + DateTime.Now.ToString("dd/MM/yyyy") + "</p>	<p style='margin: 0 0 10px 0;font-size: 14px;'>Late Login :" + LoginLogout + "</p>  <p style='margin: 0 0 10px 0;font-size: 14px;'></p> <p style='margin: 0 0 10px 0;font-size: 14px;'>Remark :" + txtLoginRemark.Text + "</p> 	 <p style='margin: 0 0 10px 0;font-size: 14px;'></p> <br /><br /><br /><p style='margin: 0 0 10px 0;font-size: 14px;'>Thanks,</p><p style='margin: 0 0 10px 0;font-size: 14px;'>Name : " + ViewState["Name"].ToString() + "</p> 	 </tr>	   <tr> <td align='center'><span style='font-size:18px' class='btn btn-primary'><a style='     padding: 10px;     text-decoration: none;     background-color: #0a5d0a;     color: white;     border-radius: 10px;' href=" + url + HttpUtility.HtmlEncode("&Status=" + common.Client_Encrypt("Approve")) + ">Approve</a><a style='  margin-left:30px;   padding: 10px;     text-decoration: none;     background-color: #ca2717;     color: white;     border-radius: 10px;' href=" + url + HttpUtility.HtmlEncode("&Status=" + common.Client_Encrypt("Reject")) + ">Reject</a></span>  </tr>      </table> 	 </td>        </tr>       </table> 	 <tr>  <td align='center' style='background-image: linear-gradient(to bottom,#3ABFEF,#3588C7); font-size:12px; color:#ffffff; padding:12px;    border-radius: 0px 0px 10px 10px;'>Copyright © 2018-2020 SFA Technologies. All rights reserved.</td> 	 </tr>      </td>     </tr>    </table>   </td> </tr> </table> </body> </html>";
                    //}
                    //else
                    //{
                    //    htmlBody = "<html xmlns='http://www.w3.org/1999/xhtml'> <head>     <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />     <title></title>     <link href='https://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet'> </head> <body style='font-family: ' open sans', sans-serif;'>     <table width='100%' border='0' cellspacing='0' cellpadding='0'>   <tr >     <td align='center' valign='top' bgcolor='#f5f5f5' style='background-color: rgba(54, 153, 211, 0.25);padding:60px 0;'>     <table width='600' border='0' cellspacing='0' cellpadding='0'>       <tr>         <td align='left' valign='top'  style='background-color:white;border-bottom:2px dashed gray; font-size:13px; color:#ffffff; padding:25px 15px;border-radius: 10px 10px 0 0;'>         <div align='center'><img src='http://www.sfatechgroup.com/img/logo.png'></div>           </td>       </tr>               <tr>         <td align='left' valign='top' bgcolor='#1ba5db' style='background-color:#fff;color:#222; padding:35px 15px;'><table width='100%' border='0' cellspacing='0' cellpadding='0'>           <tr>             <td align='left' valign='top'>             <table width='100%' border='0' cellspacing='0' cellpadding='0'>             	<tr>                 	<td align='left' valign='top'> 	 <h4 style='margin: 0 0 10px 0;'>Login Logout Permission,</h4> 	   </tr>                 <tr>                     <td align='left' valign='top'> <p style='margin: 0 0 10px 0;font-size: 14px;'>Date :" + DateTime.Now.ToString("dd/MM/yyyy") + "</p>	<p style='margin: 0 0 10px 0;font-size: 14px;'>Early Logout :" + LoginLogout + "</p>  <p style='margin: 0 0 10px 0;font-size: 14px;'></p> <p style='margin: 0 0 10px 0;font-size: 14px;'>Remark :" + txtLogoutRemark.Text + "</p> 	 <p style='margin: 0 0 10px 0;font-size: 14px;'></p> <br /><br /><br /><p style='margin: 0 0 10px 0;font-size: 14px;'>Thanks,</p><p style='margin: 0 0 10px 0;font-size: 14px;'>Name : " + ViewState["Name"].ToString() + "</p> 	 </tr>	   <tr> <td align='center'><span style='font-size:18px' class='btn btn-primary'><a style='     padding: 10px;     text-decoration: none;     background-color: #0a5d0a;     color: white;     border-radius: 10px;' href=" + url + HttpUtility.HtmlEncode("&Status=" + common.Client_Encrypt("Approve")) + ">Approve</a><a style='     padding: 10px;     text-decoration: none;     background-color: #ca2717;     color: white;     border-radius: 10px;' href=" + url + HttpUtility.HtmlEncode("&Status=" + common.Client_Encrypt("Reject")) + ">Reject</a></span>  </tr>      </table> 	 </td>        </tr>       </table> 	 <tr>  <td align='center' style='background-image: linear-gradient(to bottom,#3ABFEF,#3588C7); font-size:12px; color:#ffffff; padding:12px;    border-radius: 0px 0px 10px 10px;'>Copyright © 2018-2020 SFA Technologies. All rights reserved.</td> 	 </tr>      </td>     </tr>    </table>   </td> </tr> </table> </body> </html>";
                    //}
                    //mail.Body = htmlBody;

                    ////SmtpServer.Port = 587;
                    //SmtpServer.Credentials = new System.Net.NetworkCredential("sfatechgroup.care@gmail.com", "Sfa@123!@#");
                    //SmtpServer.EnableSsl = true;

                    //SmtpServer.Send(mail);
                    txtLoginRemark.Text = "";
                    txtLoginTime.Text = "10:00 AM";
                    txtLogOutTime.Text = "05:30 PM";
                    txtLogoutRemark.Text = "";
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-danger", "Alert!", "You are Aleady Applied.");
                    txtLoginRemark.Text = "";
                    txtLoginTime.Text = "10:00 AM";
                    txtLogOutTime.Text = "05:30 PM";
                    txtLogoutRemark.Text = "";
                }

            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ManageField()
    {
        try
        {
            if (rbtnLateLogin.Checked == true)
            {
                txtLogOutTime.Enabled = false;
                txtLogoutDiffTime.Text = "";
                txtLogoutRemark.Enabled = false;
            }
            if (rbtnEarlyLogout.Checked == true)
            {
                txtLoginTime.Enabled = false;
                txtLoginDiffTime.Text = "";
                txtLoginRemark.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}