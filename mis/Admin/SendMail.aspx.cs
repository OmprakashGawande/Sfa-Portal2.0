using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Net.Mail;

public partial class mis_Admin_SendMail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            lblMsg.Text = "";
            if (txtSubject.Text == "")
            {
                msg += "Enter Email Subject. \\n";
            }
            if (txtMessage.InnerText == "")
            {
                msg += "Enter Email Message. \\n";
            }
            if (msg == "")
            {
                //string ToEmail = string.Empty;
                //foreach (GridViewRow gvrow in gvDetails.Rows)
                //{
                //    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                //    if (chk != null & chk.Checked)
                //    {
                //        ToEmail += gvrow.Cells[3].Text + ",";
                //    }
                //}
                //ToEmail += txtEmail.Text;
                //if (ToEmail.Trim() != "")
                //{
                //    MailMessage mail = new MailMessage();
                //    //mail.From = new MailAddress("keralaslbc@gmail.com");
                //    mail.From = new MailAddress("ramveer.dangi@sfatechgroup.com");
                //    mail.ReplyTo = new MailAddress("ramveer.dangi@sfatechgroup.com");
                //    string[] Emails = ToEmail.Split(',');
                //    foreach (string mail1 in Emails)
                //    {
                //        if (mail1 != "")
                //            mail.To.Add(mail1);
                //    }



                //    mail.Subject = txtSubject.Text;

                //    mail.IsBodyHtml = true;
                //    string Body;

                //    // Body = txtMessage.Text;
                //    Body = txtMessage.InnerText;
                //    mail.Body = Body;


                //    if (FU_Attachment.HasFile)
                //    {
                //        string fileName = Path.GetFileName(FU_Attachment.PostedFile.FileName);
                //        mail.Attachments.Add(new Attachment(FU_Attachment.PostedFile.InputStream, fileName));
                //    }

                //    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 25);
                //    SmtpServer.EnableSsl = false;
                //    //SmtpServer.Port = 25;
                //    SmtpServer.Credentials = new System.Net.NetworkCredential("", "");
                //    //SmtpServer.Credentials = new System.Net.NetworkCredential("keralaslbc@gmail.com", "slbckerala!@#123");
                //    // SmtpServer.Credentials = new System.Net.NetworkCredential("slbckerala@canarabank.com", "slbckerala1234");
                //    SmtpServer.EnableSsl = true;

                //    SmtpServer.Send(mail);
                //    txtEmail.Text = "";
                //    txtSubject.Text = "";
                //    txtMessage.InnerText = "";
                //    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                //}
                //else
                //{
                //    msg = "Please Enter atleast one Email address or Select one Bank from below Table!";
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                //}

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
    //protected void FillGrid()
    //{
    //    try
    //    {
    //        gvDetails.DataSource = null;
    //        gvDetails.DataBind();

    //        ds = objdb.ByProcedure("SpAdminState",
    //            new string[] { "flag" },
    //            new string[] { "1" }, "dataset");
    //        gvDetails.DataSource = ds;
    //        gvDetails.DataBind();

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    //protected void gvDetails_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillGrid();
    //}
}