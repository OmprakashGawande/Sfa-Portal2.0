using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_Admin_AdminAddScheme : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["SchemeID"] = "0";
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string Scheme_IsActive = "1";
            if (txtScheme_Name.Text.Trim() == "")
            {
                msg += "Enter Scheme Name";
            }
            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpAdminAddScheme", new string[] { "flag", "Scheme_Name", "SchemeID" }, new string[] { "6", txtScheme_Name.Text, ViewState["SchemeID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }

                if (btnSave.Text == "Save" && ViewState["SchemeID"].ToString() == "0" && Status == 0)
                {
                    ds = objdb.ByProcedure("SpAdminAddScheme",
                    new string[] { "flag", "Scheme_IsActive", "Scheme_Name", "Scheme_UpdatedBy" },
                    new string[] { "0", Scheme_IsActive, txtScheme_Name.Text.Trim(), ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
                else if (btnSave.Text == "Edit" && ViewState["SchemeID"].ToString() != "0" && Status == 0)
                {
                    ds = objdb.ByProcedure("SpAdminAddScheme",
                    new string[] { "flag", "SchemeID", "Scheme_Name", "Scheme_UpdatedBy" },
                    new string[] { "4", ViewState["SchemeID"].ToString(), txtScheme_Name.Text.Trim(), ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Scheme Name already exist.');", true);
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
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            ds = objdb.ByProcedure("SpAdminAddScheme",
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["SchemeID"] = GridView1.SelectedValue.ToString();
        lblMsg.Text = "";
        ds = objdb.ByProcedure("SpAdminAddScheme",
                   new string[] { "flag", "SchemeID" },
                   new string[] { "3", ViewState["SchemeID"].ToString() }, "dataset");

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtScheme_Name.Text = ds.Tables[0].Rows[0]["Scheme_Name"].ToString();
            btnSave.Text = "Edit";
        }
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
        string SchemeID = chk.ToolTip.ToString();
        string Scheme_IsActive = "0";
        if (chk != null & chk.Checked)
        {
            Scheme_IsActive = "1";
        }
        objdb.ByProcedure("SpAdminAddScheme",
                   new string[] { "flag", "Scheme_IsActive", "SchemeID", "Scheme_UpdatedBy" },
                   new string[] { "5", Scheme_IsActive, SchemeID, ViewState["Emp_ID"].ToString() }, "dataset");
    }
    protected void ClearText()
    {
        txtScheme_Name.Text = "";
        ViewState["SchemeID"] = "0";
        btnSave.Text = "Save";
    }
}