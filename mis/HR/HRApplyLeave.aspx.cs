using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Net.Configuration;
using System.Configuration;


public partial class mis_HR_HRApplyLeave : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    StringBuilder sb = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    //spnAsterisk.Visible = false;
                    //divLeaveDay.Visible = false;
                    FillDropdown();
                    FillFinancialYear();
                }
                else
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        try
        {
            ddlLeaveTpye.ClearSelection();
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtLeaveRemark.Text = "";
            ddlEmployeeName.ClearSelection();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillFinancialYear()
    {
        try
        {
            ds = objdb.ByProcedure("SpHrYear_Master",
                           new string[] { "flag" },
                           new string[] { "2" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlFinancial_Year.DataTextField = "Year";
                ddlFinancial_Year.DataValueField = "Year";
                ddlFinancial_Year.DataSource = ds;
                ddlFinancial_Year.DataBind();
                // ddlFinancial_Year.SelectedValue = DateTime.Now.Year.ToString();
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
            ddlLeaveTpye.Items.Clear();
            ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag" }, new string[] { "37" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlLeaveTpye.DataSource = ds;
                ddlLeaveTpye.DataTextField = "Leave_Type";
                ddlLeaveTpye.DataValueField = "Leave_ID";
                ddlLeaveTpye.DataBind();

            }
            if (ddlLeaveTpye.Items.Count <= 0)
            {
                ddlLeaveTpye.Items.Insert(0, new ListItem("No record found", "0"));
            }
            else
            {
                ddlLeaveTpye.Items.Insert(0, new ListItem("Select", "0"));
            }
            //ds.Reset();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void InsertData()
    {
        try
        {
            string DocPath = "";
            string msg1 = "";
            if (EmpLeaveDoc.HasFile)
            {
                DocPath = "../HR/UploadDoc/LeaveDoc/" + Guid.NewGuid() + "-" + EmpLeaveDoc.FileName;
                EmpLeaveDoc.PostedFile.SaveAs(Server.MapPath(DocPath));
            }
            if (ddlLeaveTpye.SelectedItem.Text == "Medical Leave")
            {
                if (DocPath == "")
                {
                    msg1 += "कृपया सम्बंधित दस्तावेज संलग्न करें  | \\n";
                }
            }
            //if (txtFromDate.Text != txtToDate.Text)
            //{
            //  RbLeaveDays.SelectedItem.Text = "Full Day";
            // }
            string radioListValue = ddlLeaveDays.Text;
            if (txtFromDate.Text != txtToDate.Text)
            {
                ddlLeaveDays.SelectedItem.Text = "Multiple Day";
            }
            //else if (txtFromDate.Text != txtToDate.Text && ddlLeaveTpye.SelectedItem.Text == "Casual Leave")
            //{
            //    ddlLeaveDays.SelectedItem.Text = "Multiple Day";
            //}
            else if (txtFromDate.Text == txtToDate.Text && radioListValue == "Full Day")
            {
                ddlLeaveDays.SelectedItem.Text = "Full Day";
            }
            else if (txtFromDate.Text == txtToDate.Text && radioListValue == "First Half")
            {
                ddlLeaveDays.SelectedItem.Text = "First Half";
            }
            else if (txtFromDate.Text == txtToDate.Text && radioListValue == "Second Half")
            {
                ddlLeaveDays.SelectedItem.Text = "Second Half";
            }
            else
            {
                //ddlLeaveDays.SelectedItem.Text = "";
                ddlLeaveDays.SelectedItem.Text = "Full Day";
            }

            if (msg1 == "")
            {
                ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "Emp_ID", "Office_ID", "LeaveType", "LeaveFromDate", "LeaveToDate", "LeaveDay", "LeaveApproveAuthority", "LeaveRemark", "LeaveDocument", "LeaveStatus", "IsActive" },
                      new string[] { "0", ViewState["Emp_ID"].ToString(), ViewState["Office_ID"].ToString(), ddlLeaveTpye.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ddlLeaveDays.SelectedItem.Text, ddlEmployeeName.SelectedValue.ToString(), txtLeaveRemark.Text, DocPath, "Pending", "1" }, "datatset");
                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[1].Rows[0]["Status"].ToString() == "True")
                    {
                        lblMsg.Text = "<div class='alert alert-success alert-dismissible'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button><h4><i class='icon fa fa-check'></i> Success</h4>आपके द्वारा किया हुआ छुट्टी का आवेदन सफल रहा |</div>";
                        /*********Get MobileNo of approval authority and send SMS**************/
                        DataSet ds2;
                        ds2 = objdb.ByProcedure("SpHRLeaveApplication",
                            new string[] { "flag", "Emp_ID", "LeaveApproveAuthority" },
                            new string[] { "30", ViewState["Emp_ID"].ToString(), ddlEmployeeName.SelectedValue.ToString() }, "datatset");
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            //    /*******Message For Employee********/
                            string EmpMobileNo = ds2.Tables[0].Rows[0]["EmpMobile"].ToString();
                            string EmpName = ds2.Tables[0].Rows[0]["EmpName"].ToString();
                            //    if (EmpMobileNo != "9893098930" && EmpMobileNo != "0000000000" && EmpMobileNo != null)
                            //    {
                            //        //string Empmessage = "Your " + ddlLeaveTpye.SelectedItem.Text.ToString() + " request from " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd/MM/yyyy") + " to " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd/MM/yyyy") + " has been sent for approval.";
                            //        string Empmessage = "Your " + ddlLeaveDays.SelectedItem.Text + " " + ddlLeaveTpye.SelectedItem.Text.ToString() + " request from " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd/MM/yyyy") + " to " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd/MM/yyyy") + " has been sent for approval.";
                            //        string link = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=3597C1493C124F&type=unicode&routeid=2&contacts=" + EmpMobileNo + "&senderid=MPAGRO&msg=" + Empmessage;
                            //        HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
                            //        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                            //        Stream stream = response.GetResponseStream();
                            //    }
                            //    /***************/
                            //    /*******Message For Authority********/
                            //    string AuthorityMobileNo = ds2.Tables[0].Rows[0]["AuthMobile"].ToString();
                            //    if (AuthorityMobileNo != "9893098930" && AuthorityMobileNo != "0000000000" && AuthorityMobileNo != null)
                            //    {
                            //        //string Authoritymessage = EmpName + " has applied" + ddlLeaveTpye.SelectedItem.Text.ToString() + " request from " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd/MM/yyyy") + " to " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd/MM/yyyy") + ".";
                            //        string Authoritymessage = EmpName + " has applied " + ddlLeaveDays.SelectedItem.Text + " " + ddlLeaveTpye.SelectedItem.Text.ToString() + " from " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd/MM/yyyy") + " to " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd/MM/yyyy") + ".";
                            //        string link2 = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=3597C1493C124F&type=unicode&routeid=2&contacts=" + AuthorityMobileNo + "&senderid=MPAGRO&msg=" + Authoritymessage;
                            //        HttpWebRequest request2 = WebRequest.Create(link2) as HttpWebRequest;
                            //        HttpWebResponse response2 = request2.GetResponse() as HttpWebResponse;
                            //        Stream stream2 = response2.GetResponseStream();
                            //    }
                            //    /***************/
                            //    /***************/


                            if (ds != null && ds.Tables[2].Rows.Count != 0)
                            {
                                string Authoritymessage2 = EmpName + " has applied " + ddlLeaveTpye.SelectedItem.Text.ToString() + " from " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd/MM/yyyy") + " to " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd/MM/yyyy") + ".";

                                sb.Append("<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><title></title><link href='https://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' /><style>.Grid td {padding: 3px !important;}.Grid td input {padding: 3px 3px !important;text-align: right !important;font-size: 12px !important;height: 26px !important;}.Grid th {text-align: center;}.ss {text-align: left !important;}.bgcolor {background-color: #eeeeee !important;}.box {min-height: initial !important;}.table-striped > tbody > tr:nth-of-type(odd) {background-color: #f9f9f9;}.content {min-height: 700px;}.box {position: relative;border-radius: 3px;background: #ffffff;border-top: 3px solid #d2d6de;margin-bottom: 20px;width: 100%;box-shadow: 0 1px 1px rgba(0,0,0,0.1);box-shadow: none;border-top: none;}.table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {border: 1px solid #e1e1e1;}.text-center h3 {font-size: 15px;font-family: monospace;}.table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {padding: 0px 2px;}#subheading-salary {font-size: 13px;}.salary-logo {-webkit-filter: grayscale(100%);filter: grayscale(100%);width: 40px;}.printbutton {border-top: 1px dashed #838383;margin-top: 5px;padding-top: 5px;}table h4 {font-size: 15px;}.table {margin-bottom: 5px;}th, td, h3 {text-transform: uppercase !important;}.watermark {width: 300px;height: 100px;display: block;position: relative;}.watermark::after {content: '';background: url('http://45.114.143.215:8020/assets/images/1b6be679-a0e3-4c9e-aa9f-c853d17268f1.png');opacity: 0.2;top: 0;left: 0;bottom: 0;right: 0;position: absolute;z-index: -1;}</style></head><body style='font-family: 'open sans', 'sans-serif';'><div class='table-responsive'>    <table class='table table-bordered table-striped Grid earning-table' style='width: 100%;'><tbody>");
                                sb.Append("<tr><th colspan='2'><p>");
                                sb.Append(Authoritymessage2.ToString());
                                sb.Append("</p></th></tr>");
                                sb.Append("<tr><td colspan='2'><p style='text-transform: none !important;'>");
                                sb.Append("<b>Reason Of Leave :-</b> " + txtLeaveRemark.Text);
                                sb.Append("</p></td></tr>");
                                sb.Append(" <tr><td><p><a href='https://portal.sfatechnologies.com/mis/HR/HREmpLeaveRequestsEmail.aspx?LeaveStatus=Approved&FY=" + ds.Tables[2].Rows[0]["FY"].ToString() + "&LeaveId=" + ds.Tables[2].Rows[0]["LID"].ToString() + "&LeaveType=" + ds.Tables[2].Rows[0]["TypeOfLeave"].ToString() + "&EmpID=" + objdb.Encrypt(ds.Tables[2].Rows[0]["LeaveApprovalAuth"].ToString()) + "&Office=" + objdb.Encrypt(ds.Tables[2].Rows[0]["Office"].ToString()) + "'>Click Here To Approve Leave</a></p></td><td><p><a href='https://portal.sfatechnologies.com/mis/HR/HREmpLeaveRequestsEmail.aspx?LeaveStatus=Rejected&FY=" + ds.Tables[2].Rows[0]["FY"].ToString() + "&LeaveId=" + ds.Tables[2].Rows[0]["LID"].ToString() + "&LeaveType=" + ds.Tables[2].Rows[0]["TypeOfLeave"].ToString() + "&EmpID=" + objdb.Encrypt(ds.Tables[2].Rows[0]["LeaveApprovalAuth"].ToString()) + "&Office=" + objdb.Encrypt(ds.Tables[2].Rows[0]["Office"].ToString()) + "'>Click Here To Reject Leave</a></p></td></tr></tbody></table></div></body></html>");
                                string MailTo = "yajudev2018@gmail.com,himanshu61@gmail.com,richakhanna30@gmail.com,rai.mridula1981@gmail.com";
                                sendmail(MailTo, "Leave Approval Request", sb.ToString());





                                //        sb.Append("<div class='table-responsive'>");
                                //        sb.Append("<table class='table table-bordered table-striped Grid earning-table' style='width: 100%;'>");
                                //        sb.Append("<tbody>");
                                //        sb.Append("<tr>");
                                //        sb.Append("<th colspan='2'>");
                                //        sb.Append("<p>" + Authoritymessage2.ToString() + "</p>");

                                //        sb.Append("</th>");
                                //        sb.Append("</tr>");
                                //        sb.Append("<tr>");
                                //        sb.Append("<td>");

                                //        sb.Append("<p><a href='http://104.211.157.97/mis/HR/HREmpLeaveRequestsEmail.aspx?LeaveStatus=Approved&FY=" + ds.Tables[2].Rows[0]["FY"].ToString() + "&LeaveId=" + ds.Tables[2].Rows[0]["LID"].ToString() + "&LeaveType=" + ds.Tables[2].Rows[0]["TypeOfLeave"].ToString() + "&EmpID=" + objdb.Encrypt(ds.Tables[2].Rows[0]["LeaveApprovalAuth"].ToString()) + "&Office=" + objdb.Encrypt(ds.Tables[2].Rows[0]["Office"].ToString()) + "'>Click Here To Approve Leave</a></p>");

                                //        sb.Append("</td>");
                                //        sb.Append("<td>");

                                //        sb.Append("<p><a href='http://104.211.157.97/mis/HR/HREmpLeaveRequestsEmail.aspx?LeaveStatus=Rejected&FY=" + ds.Tables[2].Rows[0]["FY"].ToString() + "&LeaveId=" + ds.Tables[2].Rows[0]["LID"].ToString() + "&LeaveType=" + ds.Tables[2].Rows[0]["TypeOfLeave"].ToString() + "&EmpID=" + objdb.Encrypt(ds.Tables[2].Rows[0]["LeaveApprovalAuth"].ToString()) + "&Office=" + objdb.Encrypt(ds.Tables[2].Rows[0]["Office"].ToString()) + "'>Click Here To Reject Leave</a></p>");

                                //        sb.Append("</td>");
                                //        sb.Append("</tr>");
                                //        sb.Append("</tbody>");
                                //        sb.Append("</table>");

                                //        MailMessage mail = new MailMessage();
                                //        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 25);
                                //        SmtpServer.EnableSsl = false;

                                //        mail.From = new MailAddress("carempagro@gmail.com");
                                //        mail.To.Add(ds.Tables[2].Rows[0]["AuthEmail"].ToString());
                                //        mail.Subject = "Leave Application";

                                //        mail.IsBodyHtml = true;
                                //        string htmlBody;
                                //        htmlBody = "<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <title></title><link href='https://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet'> <style> .Grid td {             padding: 3px !important;         }              .Grid td input {                 padding: 3px 3px !important;                 text-align: right !important;                 font-size: 12px !important;                 height: 26px !important;             }          .Grid th {             text-align: center;         }          .ss {             text-align: left !important;         }          .bgcolor {             background-color: #eeeeee !important;         }          .box {             min-height: initial !important;         } .table-striped > tbody > tr:nth-of-type(odd) {   background-color: #f9f9f9; } .content {min-height: 700px; } .box { position: relative;border-radius: 3px;background: #ffffff;border-top: 3px solid #d2d6de;margin-bottom: 20px; width: 100%;box-shadow: 0 1px 1px rgba(0,0,0,0.1);box-shadow: none;border-top: none; }.table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {border: 1px solid #e1e1e1;}.text-center h3 {font-size: 15px; font-family: monospace;}.table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {padding: 0px 2px;}#subheading-salary {font-size: 13px;}.salary-logo {-webkit-filter: grayscale(100%);filter: grayscale(100%);width: 40px;         }          .printbutton {             border-top: 1px dashed #838383;             margin-top: 5px;             padding-top: 5px;         }          table h4 {             font-size: 15px;         }          .table {             margin-bottom: 5px;         }          th, td, h3 {             text-transform: uppercase !important;         }         .watermark {   width: 300px;   height: 100px;   display: block;   position: relative; }  .watermark::after {   content:'';  background:url('http://45.114.143.215:8020/assets/images/1b6be679-a0e3-4c9e-aa9f-c853d17268f1.png');   opacity: 0.2;   top: 0;   left: 0;   bottom: 0;   right: 0;   position: absolute;   z-index: -1;   }</style></head><body style='font-family: ' open sans', sans-serif;'>" + sb.ToString() + "</body></html>";
                                //        mail.Body = htmlBody;
                                //        SmtpServer.Port = 587;
                                //        SmtpServer.Credentials = new System.Net.NetworkCredential("carempagro@gmail.com", "mpagro@123");
                                //        SmtpServer.EnableSsl = true;
                                //        SmtpServer.Send(mail);
                            }
                            //    /***************/

                        }

                        /**********************************************************************/
                    }
                    else
                    {
                        lblMsg.Text = "<div class='alert alert-dnager alert-dismissible'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button><h4><i class='icon fa fa-check'></i> Success</h4>आपके द्वारा किया हुआ छुट्टी का आवेदन असफल रहा |</div>";
                    }
                }
                ClearText();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('सम्बंधित दस्तावेज संलग्न करें |');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void applyleave_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds1;
            lblMsg.Text = "";
            string msg = "";
            int TotalDays = 0;
            int Optional = 0;
            if (ddlLeaveTpye.SelectedIndex == 0)
            {
                msg += "कृपया छुट्टी का प्रकार चुनें |<br/>";
            }
            if (txtFromDate.Text == "")
            {
                msg += "कृपया 'कब से' दिनांक चुने | <br/>";
            }
            if (txtToDate.Text == "")
            {
                msg += "कृपया 'कब तक' दिनांक चुने | <br/>";
            }

            /*********************/
            string fdate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            string ldate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            DateTime date1 = DateTime.Parse(fdate);
            DateTime date2 = DateTime.Parse(ldate);
            TotalDays = ((date2 - date1).Days);
            if (TotalDays > float.Parse(hfBalanceLeave.Value.ToString()))
            {
                msg += "आपके पास इतनी छुट्टियाँ शेष नहीं हैं  | ";

                if (ddlLeaveTpye.SelectedItem.Text != "Earned Leave" && ddlLeaveTpye.SelectedItem.Text != "Medical Leave" && ddlLeaveTpye.SelectedItem.Text != "Child Care Leave")
                {
                    if (date1.Year != int.Parse(ddlFinancial_Year.SelectedValue.ToString()) || date2.Year != int.Parse(ddlFinancial_Year.SelectedValue.ToString()))
                    {
                        msg += "From Date और To Date सेलेक्ट किए हुए वर्ष की होनी चाहिए | ";
                    }
                }
            }
            /*********************/

            if (txtLeaveRemark.Text == "")
            {
                msg += "कृपया छुट्टी का कारण भरें | <br/>";
            }
            if (msg == "")
            {
                ds1 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "Emp_ID", "LeaveFromDate", "LeaveToDate" },
                  new string[] { "22", ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "datatset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    if (ddlLeaveTpye.SelectedItem.Text == "Casual Leave")
                    {
                        string d11 = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
                        string d22 = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
                        DateTime d1 = DateTime.Parse(d11);
                        DateTime d2 = DateTime.Parse(d22);
                        TotalDays = ((d2 - d1).Days);
                        TotalDays = TotalDays + 1;
                        if (TotalDays <= 8)
                        {
                            InsertData();
                        }
                        else
                        {
                            msg = "आप 8 दिनों से अधिक के लिए लगातार " + ddlLeaveTpye.SelectedItem.Text + " नहीं ले सकते";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                            txtFromDate.Text = "";
                            txtToDate.Text = "";
                        }
                    }
                    else if (ddlLeaveTpye.SelectedItem.Text == "Optional Leave")
                    {
                        if (txtFromDate.Text == txtToDate.Text)
                        {
                            DataSet ds2 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "FromDate", "ToDate", "Emp_ID" },
                new string[] { "25", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Emp_ID"].ToString() }, "datatset");
                            if (ds2 != null && ds2.Tables.Count != 0)
                            {
                                if (ds2.Tables[0].Rows[0]["Status"].ToString() == "True")
                                {
                                    if (ds2.Tables[1].Rows.Count > 0)
                                    {
                                        decimal OptionalLeave = Convert.ToDecimal(ds2.Tables[1].Rows[0]["OptionalLeave"].ToString());


                                        decimal TotalOptionalAllowed = 0;
                                        if (ds2.Tables[2].Rows.Count > 0)
                                        {
                                            TotalOptionalAllowed = Convert.ToDecimal(ds2.Tables[2].Rows[0]["Leave_Days"].ToString());
                                        }

                                        if (OptionalLeave < TotalOptionalAllowed)
                                        {
                                            InsertData();
                                        }
                                        else
                                        {
                                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Your all optional Leaves have been consumed.');", true);
                                            txtFromDate.Text = "";
                                            txtToDate.Text = "";
                                        }
                                    }
                                    else
                                    {
                                        InsertData();
                                    }
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please Select Valid Date For Optional Leave');", true);
                                    txtFromDate.Text = "";
                                    txtToDate.Text = "";
                                }
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please select same date to apply optional leave');", true);
                            txtFromDate.Text = "";
                            txtToDate.Text = "";
                        }
                    }
                    else
                    {
                        InsertData();
                    }
                }
                else
                {
                    string FromDate = ds1.Tables[0].Rows[0]["LeaveFromDate"].ToString();
                    string ToDate = ds1.Tables[0].Rows[0]["LeaveToDate"].ToString();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Leave Already Applied on " + FromDate + " to " + ToDate + ".');", true);
                    txtFromDate.Text = "";
                    txtToDate.Text = "";
                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void ddlLeaveTpye_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblBalanceLeave.Text = "";
        ds = objdb.ByProcedure("SpHRBalanceLeaveDetail", new string[] { "flag", "Emp_ID", "LeaveType_ID", "Financial_Year" },
                  new string[] { "20", ViewState["Emp_ID"].ToString(), ddlLeaveTpye.SelectedValue.ToString(), ddlFinancial_Year.SelectedValue.ToString() }, "datatset");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblBalanceLeave.Text = "Leave Type: <span style='color:tomato'>" + ddlLeaveTpye.SelectedItem.Text.ToString() + "</span>,   Remaining : <span style='color:tomato'>" + ds.Tables[0].Rows[0]["TotalRemainingLeaves"].ToString() + "  Day(s) Leaves</span>";
            if (ddlLeaveTpye.SelectedValue == "3")//09-06-2023 changes  by akash ( for LWP leave employee can able to take many leave so apply this condition)
            {
                lblBalanceLeave.Visible = false;
                hfBalanceLeave.Value = "365";
            }
            else
            {
                lblBalanceLeave.Visible = true;
                hfBalanceLeave.Value = ds.Tables[0].Rows[0]["TotalRemainingLeaves"].ToString();
            }


            if (decimal.Parse(hfBalanceLeave.Value) > 0)
            {
                applyleave.Enabled = true;
            }
            else
            {
                applyleave.Enabled = false;
            }

        }

        ds = objdb.ByProcedure("SpHRBalanceLeaveDetail", new string[] { "flag", "Office_ID", "Emp_ID", "LeaveType_ID" }, new string[] { "15", ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), ddlLeaveTpye.SelectedValue.ToString() }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlEmployeeName.DataSource = ds;
            ddlEmployeeName.DataTextField = "Emp_Name";
            ddlEmployeeName.DataValueField = "Emp_ID";
            ddlEmployeeName.DataBind();
            ddlEmployeeName.Items.Insert(0, new ListItem("Select", "0"));
        }
        if (ddlEmployeeName.Items.Count > 1)
        {
            ddlEmployeeName.SelectedIndex = 1;
        }

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
                // mm.CC.Add(CC);
                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtpSection.Network.Host;
                smtp.EnableSsl = smtpSection.Network.EnableSsl;
                NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                smtp.Credentials = networkCred;
                smtp.Port = smtpSection.Network.Port;
                smtp.Send(mm);
                //HttpContext.Current.Response.End();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alertMessage", "alert('Email sent.');", false);
            }
        }
        catch (Exception ex)
        {

        }
    }
}