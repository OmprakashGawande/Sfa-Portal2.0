using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
public partial class mis_Daily_Task_latelogin : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();
    IFormatProvider culture = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null)
        {
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                txt_date.Attributes.Add("readonly", "readonly");
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["OfficeType_ID"] = Session["OfficeType_ID"].ToString();
                FillEmployeeName();

                CreateDataTable();

                DateTime dd = DateTime.Now;
                txt_date.Text = dd.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);


                btnSendEmail.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    protected void FillEmployeeName()
    {
        try
        {
            ds.Clear();
            ddl_Employee.Items.Clear();
            ds = objdb.ByProcedure("USP_Daily_Tasklatelogin", new string[] { }, new string[] { }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["ds"] = ds.Tables[0];
                        ddl_Employee.DataTextField = "Emp_Name";
                        ddl_Employee.DataValueField = "Emp_ID";
                        ddl_Employee.DataSource = ds.Tables[0];
                        ddl_Employee.DataBind();
                        ddl_Employee.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        ddl_Employee.Items.Insert(0, new ListItem("No record found", "0"));
                    }
                }
                else
                {
                    ddl_Employee.Items.Insert(0, new ListItem("No record found", "0"));
                }

            }
            else
            {
                ddl_Employee.Items.Insert(0, new ListItem("No record found", "0"));
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", ex.Message.ToString());
        }

    }


    protected void Clear()
    {
        DateTime dd = DateTime.Now;
        txt_date.Text = dd.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        ddl_Employee.ClearSelection();


    }



    protected void CreateDataTable()
    {
        try
        {
            if (ViewState["dt1"] == null)
            {
                DataTable dataTable = new DataTable();
                DataColumn rownumber = dataTable.Columns.Add("RowNo", typeof(int));
                dataTable.Columns.Add("Emp_ID", typeof(int));
                dataTable.Columns.Add("Late_Date", typeof(string));
                dataTable.Columns.Add("Employee_Name", typeof(string));
                dataTable.Columns.Add("Late_Time", typeof(string));
                rownumber.AutoIncrement = true;
                rownumber.AutoIncrementSeed = 1;
                rownumber.AutoIncrementStep = 1;
                ViewState["dt1"] = dataTable;

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", ex.Message.ToString());
        }


    }

    protected void GetDataTable()
    {

        DataTable dt = new DataTable();
        try
        {
            dt.Columns.Add(new DataColumn("Late_Date", typeof(string)));
            dt.Columns.Add(new DataColumn("Emp_ID", typeof(int)));
            dt.Columns.Add(new DataColumn("Employee_Name", typeof(string)));


            foreach (GridViewRow row in GridView1.Rows)
            {
                Label lblDate = (Label)row.FindControl("Late_Date");
                Label lblEmployee_Name = (Label)row.FindControl("Employee_Name");

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", ex.Message.ToString());
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        try
        {
            if (Page.IsValid)
            {
                string msg = "";
                foreach (GridViewRow row in GridView1.Rows)
                {

                    Label llEmployee_Name = (Label)row.FindControl("lblEmployee_Name");
                    if (llEmployee_Name.Text == ddl_Employee.SelectedItem.Text)
                    {
                        msg += "Employee Already Is Available.\\n";
                        break;
                    }
                }
                if (txt_date.Text == "") { msg += "Enter Date.\\n"; }
                if (ddl_Employee.SelectedValue == "0") { msg += "Select Employee Name.\\n"; }
                if (txtTime.Text == "") { msg += "Enter Time.\\n"; }
                if (msg == "")
                {

                    DateTime dt5 = DateTime.ParseExact(txt_date.Text, "dd/MM/yyyy", culture);
                    string Late_Date = dt5.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


                    string EmployeeName = ddl_Employee.SelectedItem.ToString();
                    int Emp_ID = Convert.ToInt32(ddl_Employee.SelectedValue.ToString());

                    DataTable dt = (DataTable)ViewState["dt1"];
                    int i = dt.Rows.Count + 1;

                    DateTime time24 = Convert.ToDateTime(txtTime.Text);

                    //String time12 = time24.ToString("HH:mm tt");
                    var timespan = new TimeSpan(time24.TimeOfDay.Hours, time24.TimeOfDay.Minutes, time24.TimeOfDay.Seconds);
                    var output = new DateTime().Add(timespan).ToString("hh:mm tt");

                    dt.Rows.Add(null, Emp_ID, Late_Date, EmployeeName, output); ;
                    ViewState["dt1"] = dt;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    ddl_Employee.ClearSelection();


                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                }
                int gridRows = GridView1.Rows.Count;
                if (gridRows > 0)
                {
                    btnSendEmail.Visible = true;
                }
                else
                {
                    btnSendEmail.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error ", ex.Message.ToString());
        }
    }
    protected void creteLateEmpList()
    {


    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            DateTime dt5 = DateTime.ParseExact(txt_date.Text, "dd/MM/yyyy", culture);
            string Latedate = dt5.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string mailSubject = "This is to inform you all that today you have logged in after 9:40 AM. Please be reminded that logging in after 9:40 AM is not permissible.<br> To maintain smooth operations, it is essential to login by 9:30 AM sharp. The late login report is attached below.";
            Dictionary<int, string> getMonthByItem = new Dictionary<int, string>();
            DataTable dtnew = (DataTable)ViewState["ds"];
            foreach (DataRow row in dtnew.Rows)
            {
                getMonthByItem.Add(Convert.ToInt32(row["Emp_ID"]), row["Emp_Email"].ToString().Trim());
            }
            DataTable dt = (DataTable)ViewState["dt1"];
            if (dt.Rows.Count <= 0) { msg += "Select atleast one employee\\n"; }

            if (msg == "")
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<div style='text-align:center'>");
                sb.Append("<h2 style=' margin: 0; padding:0'>SFA Technologies Pvt. Ltd. </h2>");
                sb.Append("<h3 style=' margin: 1px; padding:0'>Employee Late Login Report</h3>");
                sb.Append("<h4 style=' margin: 0; padding:0'>Date : " + txt_date.Text + "</h4>");
                sb.Append("<p style=' margin: 0; padding:0'>Date : " + mailSubject + "</p>");
                sb.Append("</div>");
                sb.Append("<div>");
                sb.Append("<table style='border: 1px solid black; border-collapse: collapse;width:80%;margin:1% 10%;'>");
                sb.Append("<tr>");
                sb.Append("<th style='border: 1px solid black; border-collapse: collapse;width:10%';text-align:center'>S.No.</th>");
                sb.Append("<th style='border: 1px solid black; border-collapse: collapse; width:70%';text-align:center'>Employee Name</th>");
                sb.Append("<th style='border: 1px solid black; border-collapse: collapse;width:20%';text-align:center'>Time</th>");
                sb.Append("</tr>");
                string empEmail = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (getMonthByItem[Convert.ToInt32(dt.Rows[i]["Emp_ID"])] != "")
                    {
                        empEmail += getMonthByItem[Convert.ToInt32(dt.Rows[i]["Emp_ID"])] + ",";
                    }
                    sb.Append("<tr>");
                    sb.Append("<td style='border: 1px solid black; border-collapse: collapse;text-align:center'>" + (i + 1) + "</td>");
                    sb.Append("<td style='border: 1px solid black; border-collapse: collapse;text-align:center'>" + dt.Rows[i]["Employee_Name"].ToString() + "</td>");
                    sb.Append("<td style='border: 1px solid black; border-collapse: collapse;text-align:center'>" + dt.Rows[i]["Late_Time"].ToString() + "</td>");
                    sb.Append("</tr>");
                }
                string empMail = empEmail.Substring(0, (empEmail.Length - 1));
                string adminMail = "yajudev2018@gmail.com,himanshu61@gmail.com,richakhanna30@gmail.com,rai.mridula1981@gmail.com";
                sb.Append("</table>");
                sb.Append("</div>");
                //sendmail(adminMail, empMail, sb.ToString(),Latedate);
                DivMail.InnerHtml = sb.ToString();
                dt.Columns.Remove("RowNo");
                dt.Columns.Remove("Employee_Name");

                //ds = objdb.ByProcedure("USP_Emp_LateLogin_Insert",
                //    new string[] { "OfficeType_ID", "Office_ID", "Created_By", "CreatedBy_IP", "Late_Date" },
                //    new string[] { ViewState["OfficeType_ID"].ToString(), ViewState["Office_ID"].ToString(), 
                //    ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress() , Latedate},
                //    new string[] { "EMP_Late_Login_Type" }, new DataTable[] { dt }, "dataset");

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string massage = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You !", massage);
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Not Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Alert !", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", ex.Message.ToString());
        }
        finally
        {

            ds.Clear();

            ViewState["dt1"] = null;
            GridView1.DataSource = (DataTable)ViewState["dt1"];
            GridView1.DataBind();
            CreateDataTable();
            btnSendEmail.Visible = false;

        }

    }



    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["dt1"] as DataTable;
            dt.Rows[index].Delete();
            ViewState["dt"] = dt;
            if (dt != null)
            {
                if (dt != null && dt.Columns.Count > 0 && dt.Rows.Count > 0)
                {
                    decimal TotalAmount = 0;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    ViewState["TotalAmount"] = TotalAmount.ToString();
                }
                else
                {
                    GridView1.DataSource = ViewState["dt"] as DataTable;
                    GridView1.DataBind();
                }
            }
            int gridRows = GridView1.Rows.Count;
            if (gridRows > 0)
            {
                btnSendEmail.Visible = true;
            }
            else
            {
                btnSendEmail.Visible = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry", ex.Message.ToString());
        }
    }
    private void sendmail(string TO, string CC, string content, string Latedate)
    {
        return;
        try
        {
            //  string AttachedEmailHTMLPath = Server.MapPath("~/HtmlTemplete/OIC_Email_Templete.html");
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            using (MailMessage mm = new MailMessage(smtpSection.From, TO))
            {
                mm.Subject = "Late Login Report :- " + Latedate;
                mm.Body = content;
                mm.IsBodyHtml = true;
                mm.CC.Add(CC);
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
    protected void txt_date_TextChanged(object sender, EventArgs e)
    {
        Clear();
        ViewState["dt1"] = null;
        GridView1.DataSource = (DataTable)ViewState["dt1"];
        GridView1.DataBind();
        CreateDataTable();
        btnSendEmail.Visible = false;
    }
}