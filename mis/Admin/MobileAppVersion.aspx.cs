using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_Admin_MobileAppVersion : System.Web.UI.Page
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
                    ViewState["Module_ID"] = "0";
                    FillGrid();
                    lblMsg.Text = "";
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
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            ds = objdb.ByProcedure("Sp_MobileAppVersion",
                new string[] { "flag" },
                new string[] { "0" }, "dataset");
            GridView1.DataSource = ds;
            GridView1.DataBind();

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
            string App_Version_Status = "1";
            if (txtLatestVersion.Text.Trim() == "")
            {
                msg += "Enter Latest Version";
            }
            if (msg.Trim() == "")
            {
                //ds = objdb.ByProcedure("SpUMModuleMaster",
                //       new string[] { "flag", "Module_Name", "Module_ID" },
                //       new string[] { "4", txtLatestVersion.Text.Trim(), ViewState["Module_ID"].ToString() }, "dataset");


                if (btnSave.Text == "Save")
                    {
                        objdb.ByProcedure("Sp_MobileAppVersion",
                        new string[] { "flag", "App_Version", "App_Version_Updated_By", "App_Version_Status" },
                        new string[] { "1", txtLatestVersion.Text.Trim(), ViewState["Emp_ID"].ToString(), App_Version_Status }, "dataset");

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    }


                else if (btnSave.Text == "Edit")
                    {
                        objdb.ByProcedure("Sp_MobileAppVersion",
                        new string[] { "flag", "App_ID", "App_Version", "App_Version_Updated_By" },
                        new string[] { "2", ViewState["App_ID"].ToString(), txtLatestVersion.Text.Trim(), ViewState["Emp_ID"].ToString() }, "dataset");

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    }
                    else
                    {
                        //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", "This Module Is Already Exist.");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('This Module  Is Already Exist');", true);
                    }

                txtLatestVersion.Text = "";
                btnSave.Text = "Save";
                FillGrid();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
            string App_ID = chk.ToolTip.ToString();
            string App_Version_Status = "0";
            if (chk != null & chk.Checked)
            {
                App_Version_Status = "1";
            }
            objdb.ByProcedure("Sp_MobileAppVersion",
                       new string[] { "flag", "App_Version_Status", "App_ID", "App_Version_Updated_By" },
                       new string[] { "4", App_Version_Status, App_ID, ViewState["Emp_ID"].ToString() }, "dataset");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["App_ID"] = GridView1.SelectedValue.ToString();
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Sp_MobileAppVersion",
                       new string[] { "flag", "App_ID" },
                       new string[] { "3", ViewState["App_ID"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtLatestVersion.Text = ds.Tables[0].Rows[0]["App_Version"].ToString();
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}