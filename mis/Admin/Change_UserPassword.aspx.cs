using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Data;

public partial class mis_Masters_Change_UserPassword : System.Web.UI.Page
{
    APIProcedure Objdb = new APIProcedure();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        
            if (!IsPostBack)
            {
                //ViewState["OfficeID"] = Session["Office_ID"].ToString();
                //ViewState["UserTypeID"] = Session["UserTypeId"].ToString();
            }
        
    }

    public string SHA512_HASH(string rawData)
    {
        //Create a SHA512   
        using (SHA512 sha512Hash = SHA512.Create())
        {
            // ComputeHash - returns byte array  
            byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                lblMsg.Text = "";
                if (txtCurrentPassword.Text != "")
                {
                    ds = Objdb.ByProcedure("USP_ChangePassword", new string[] { "flag", "Emp_ID",  },
                                                                 new string[] { "0", Session["Emp_ID"].ToString() }, "dataset");
                    if (ds.Tables != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string Currnt = SHA512_HASH(txtCurrentPassword.Text.Trim());
                            string APEX = SHA512_HASH(txtNewPassword.Text.Trim());

                            if (Currnt == ds.Tables[0].Rows[0]["PassWord"].ToString())
                            {
                                if (Currnt != APEX)
                                {
                                    if (txtNewPassword.Text == txtConfirmPassword.Text)
                                    {
                                        DataSet ds1 = Objdb.ByProcedure("USP_ChangePassword", new string[] { "flag", "Emp_ID", "Password" },
                                                                                              new string[] { "1", Session["Emp_ID"].ToString(), APEX }, "dataset");
                                        if (ds1 != null && ds1.Tables[0].Rows[0]["status"].ToString() == "OK")
                                        {
                                            lblMsg.Text = Objdb.Alert("fa-check", "alert-success", "Successful!", ds1.Tables[0].Rows[0]["Errormsg"].ToString());
                                        }
                                        if (ds1 != null && ds1.Tables[0].Rows[0]["status"].ToString() == "NOT OK")
                                        {
                                            lblMsg.Text = Objdb.Alert("fa-ban", "alert-warning", "Sorry!", ds1.Tables[0].Rows[0]["Errormsg"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('New Password and Confirm Passwors is not same')", true);

                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Your Last Password and new password is same')", true);

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Incorrect Current Password')", true);
                            }
                        }
                    }
                }


            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = Objdb.Alert("fa-ban", "alert-danger", "Sorry", ex.Message.ToString());
        }
    }
}