using System;
using System.Data;
using System.Globalization;
using System.Net.Mail;
using System.Web.UI.WebControls;

public partial class mis_Finance_FinSupplierAuditTrail : System.Web.UI.Page
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
                FillDropdown();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillDropdown()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinSupplierItem",
                                     new string[] { "flag" },
                                     new string[] { "6" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlSupplier.DataSource = ds;
                ddlSupplier.DataTextField = "NameOfAccountHolder";
                ddlSupplier.DataValueField = "ID";
                ddlSupplier.DataBind();
                ddlSupplier.Items.Insert(0, new ListItem("Select", "0"));
            }

            ddlBillNo.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlBillNo.Items.Clear();
            txtNetPayment.Text = "";
            ViewState["EmailID"] = "";
            if (ddlSupplier.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpFinSupplier",
                                    new string[] { "flag", "ID" },
                                    new string[] { "8", ddlSupplier.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBillNo.DataSource = ds;
                        ddlBillNo.DataTextField = "BillNo";
                        ddlBillNo.DataValueField = "OrderID";
                        ddlBillNo.DataBind();
                        
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        ViewState["EmailID"] = ds.Tables[1].Rows[0]["EmailID"].ToString();
                    }

                }

            }

            ddlBillNo.Items.Insert(0, new ListItem("Select", "0"));


            FillGrid();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlBillNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            txtNetPayment.Text = "";
            if (ddlBillNo.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpFinSupplier",
                                    new string[] { "flag", "ID" },
                                    new string[] { "9", ddlBillNo.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtNetPayment.Text = ds.Tables[0].Rows[0]["NetPayment"].ToString();
                }
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
            if (ddlSupplier.SelectedIndex <= 0)
            {
                msg = msg + "Select Supplier Name. \\n";
            }
            if (ddlBillNo.SelectedIndex <= 0)
            {
                msg = msg + "Select Bill No. \\n";
            }
            if (txtNetPayment.Text == "")
            {
                msg = msg + "Enter Net Payment. \\n";
            }


            if (msg.Trim() == "")
            {

                if (txtChequeDate.Text != "")
                {

                    ds = objdb.ByProcedure("SpFinSupplierAuditTrail",
               new string[] { "flag", "SupplierID", "SupplierName", "OrderID", "BillNo", "NetPayment", "ChequeNo", "ChequeDate", "UpdatedBy" },
               new string[] { "0", ddlSupplier.SelectedValue.ToString(), ddlSupplier.SelectedItem.ToString(), ddlBillNo.SelectedValue.ToString(), ddlBillNo.SelectedItem.ToString(), txtNetPayment.Text, txtChequeNo.Text, Convert.ToDateTime(txtChequeDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Emp_ID"].ToString() }, "dataset");

                }
                else
                {
                    ds = objdb.ByProcedure("SpFinSupplierAuditTrail",
               new string[] { "flag", "SupplierID", "SupplierName", "OrderID", "BillNo", "NetPayment", "ChequeNo", "UpdatedBy" },
               new string[] { "0", ddlSupplier.SelectedValue.ToString(), ddlSupplier.SelectedItem.ToString(), ddlBillNo.SelectedValue.ToString(), ddlBillNo.SelectedItem.ToString(), txtNetPayment.Text, txtChequeNo.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                }
                if (ViewState["EmailID"].ToString() != "")
                {
                    SendMail();
                }
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Data Successfully Saved.");
                ClearText();
                GridView1.DataSource = null;
                GridView1.DataBind();

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
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ddlSupplier.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpFinSupplierAuditTrail",
                new string[] { "flag", "SupplierID" },
                new string[] { "1", ddlSupplier.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
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
        ddlSupplier.ClearSelection();
        ddlBillNo.Items.Clear();
        ddlBillNo.Items.Insert(0, new ListItem("Select", "0"));
        txtNetPayment.Text = "";
        txtChequeNo.Text = "";
        txtChequeDate.Text = "";
        ViewState["EmailID"] = "";

    }
    protected void SendMail()
    {
        try
        {
            // ViewState["EmailID"] = "rajendra1990.sidhi@gmail.com";

            MailMessage mail = new MailMessage();
            //mail.From = new MailAddress("keralaslbc@gmail.com");
            mail.From = new MailAddress("carempagro@gmail.com");
            mail.ReplyTo = new MailAddress("carempagro@gmail.com");



            //string[] Emails = ToEmail.Split(',');
            //foreach (string mail1 in Emails)
            //{
            //    if (mail1 != "")
            //        mail.To.Add(mail1);
            //}

            mail.To.Add(ViewState["EmailID"].ToString());
            mail.Subject = "Payment process for bill number '123'  ";

            mail.IsBodyHtml = true;
            string Body;

            // Body = txtMessage.Text;
            Body = "To " + ddlSupplier.SelectedItem.ToString() + ", <br> <br> <br> The payment amount of " + txtNetPayment.Text + " Rs. for your bill number '" + ddlBillNo.SelectedItem.ToString() + "' has been process by MPAGRO, you will receive the amount soon.";
            mail.Body = Body;


            //if (FU_Attachment.HasFile)
            //{
            //    string fileName = Path.GetFileName(FU_Attachment.PostedFile.FileName);
            //    mail.Attachments.Add(new Attachment(FU_Attachment.PostedFile.InputStream, fileName));
            //}

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            //SmtpServer.EnableSsl = false;
            //SmtpServer.Port = 465;
            SmtpServer.Credentials = new System.Net.NetworkCredential("carempagro@gmail.com", "mpagro@123");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            /*******************/

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

}