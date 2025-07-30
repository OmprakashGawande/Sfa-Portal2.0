using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_Admin_OfficeType : System.Web.UI.Page
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
                    ViewState["OfficeType_ID"] = "0";
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

            ds = objdb.ByProcedure("SpAdminOfficeType",
                new string[] { "flag" },
                new string[] { "1" }, "dataset");
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
            string OfficeType_IsActive = "1";
            if (txtOfficeType_Title.Text.Trim() == "")
            {
                msg += "Enter Office Type Title";
            }
            if (msg.Trim() == "")
            {
                ds = objdb.ByProcedure("SpAdminOfficeType",
                      new string[] { "flag", "OfficeType_Title", "OfficeType_ID" },
                      new string[] { "4", txtOfficeType_Title.Text.Trim(),  ViewState["OfficeType_ID"].ToString() }, "dataset");
               
                    if (btnSave.Text == "Save" && ViewState["OfficeType_ID"].ToString() == "0" && ds.Tables[0].Rows.Count == 0)
                    {
                        objdb.ByProcedure("SpAdminOfficeType",
                        new string[] { "flag", "OfficeType_IsActive", "OfficeType_Title", "OfficeType_UpdatedBy" },
                        new string[] { "0", OfficeType_IsActive, txtOfficeType_Title.Text.Trim(), ViewState["Emp_ID"].ToString() }, "dataset");

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    }
               
                    else if (btnSave.Text == "Edit" && ViewState["OfficeType_ID"].ToString() != "0" && ds.Tables[0].Rows.Count == 0)
                    {
                        objdb.ByProcedure("SpAdminOfficeType",
                        new string[] { "flag", "OfficeType_ID", "OfficeType_Title", "OfficeType_UpdatedBy" },
                        new string[] { "5", ViewState["OfficeType_ID"].ToString(), txtOfficeType_Title.Text.Trim(), ViewState["Emp_ID"].ToString() }, "dataset");

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        ViewState["OfficeType_ID"] = 0;
                    }
                    else
                    {
                       // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", "This Office Type Is Already Exist.");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('This Office Type Is Already Exist');", true);
                    }
               
                txtOfficeType_Title.Text = "";
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
            string OfficeType_ID = chk.ToolTip.ToString();
            string OfficeType_IsActive = "0";
            if (chk != null & chk.Checked)
            {
                OfficeType_IsActive = "1";
            }
            objdb.ByProcedure("SpAdminOfficeType",
                       new string[] { "flag", "OfficeType_IsActive", "OfficeType_ID", "OfficeType_UpdatedBy" },
                       new string[] { "6", OfficeType_IsActive, OfficeType_ID, ViewState["Emp_ID"].ToString() }, "dataset");
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
            ViewState["OfficeType_ID"] = GridView1.SelectedValue.ToString();
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOfficeType",
                       new string[] { "flag", "OfficeType_ID" },
                       new string[] { "3", ViewState["OfficeType_ID"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtOfficeType_Title.Text = ds.Tables[0].Rows[0]["OfficeType_Title"].ToString();
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}