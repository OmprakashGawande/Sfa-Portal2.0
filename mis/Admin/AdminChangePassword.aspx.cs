using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Admin_AdminChangePassword : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
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
                    txtNewPassword.Text = "";
                    txtOldPassword.Text = "";
                    txtConfirmPassword.Text = "";
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
    protected void btnChange_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string NewPassword = txtNewPassword.Text;
            string ConfirmPassword = txtConfirmPassword.Text;
            if (txtOldPassword.Text == "")
            {
                msg += "Enter Old Passwor1 \\n";
            }
            if (txtNewPassword.Text == "")
            {
                msg += "Enter New Password \\n";
            }
            if (txtConfirmPassword.Text == " ")
            {
                msg += "Enter Confirm New Password \\n";
            }

            if (txtNewPassword.Text != "" && txtConfirmPassword.Text != "" && NewPassword != ConfirmPassword)
            {
                msg += "New Password And Confirm New Password Do Not Match. Please Try Again. \n";
            }
            if (txtOldPassword.Text != "" && txtNewPassword.Text != "" && txtOldPassword.Text == txtNewPassword.Text)
            {
                msg += "Old password and new password cannot be same. \n";
            }
            if (msg == "")
            {
                ds = objdb.ByProcedure("SpLogin", new string[] { "flag", "Emp_ID", "Password" }, new string[] { "1", ViewState["Emp_ID"].ToString(), txtOldPassword.Text }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    string IPadd2 = Request.UserHostAddress;
                    objdb.ByProcedure("SpLogin", new string[] { "flag", "Emp_ID", "Password", "IPAddress" }, new string[] { "2", ViewState["Emp_ID"].ToString(), txtConfirmPassword.Text, IPAddress }, "dataset");
                    txtOldPassword.Text = "";
                    txtNewPassword.Text = "";
                    txtConfirmPassword.Text = "";
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else
                {
                    txtOldPassword.Text = "";
                    txtNewPassword.Text = "";
                    txtConfirmPassword.Text = "";
                    lblMsg.Text = "";
                    msg += "Enter Correct Old Password \\n";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
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
}