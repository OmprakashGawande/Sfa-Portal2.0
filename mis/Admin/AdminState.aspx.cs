using System;
using System.Data;
using System.Web.UI.WebControls;


public partial class mis_Admin_AdminState : System.Web.UI.Page
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
                ViewState["State_ID"] = "0";
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
            string State_IsActive = "1";
            if (txtState_Name.Text.Trim() == "")
            {
                msg += "Enter State Name";
            }
            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpAdminState", new string[] { "flag", "State_Name", "State_ID" }, new string[] { "8", txtState_Name.Text, ViewState["State_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }

                if (btnSave.Text == "Save" && ViewState["State_ID"].ToString() == "0" && Status == 0)
                {
                    ds = objdb.ByProcedure("SpAdminState",
                    new string[] { "flag", "State_IsActive", "State_Name", "State_UpdatedBy" },
                    new string[] { "0", State_IsActive, txtState_Name.Text.Trim(), ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
                else if (btnSave.Text == "Edit" && ViewState["State_ID"].ToString() != "0" && Status == 0)
                {
                    ds = objdb.ByProcedure("SpAdminState",
                    new string[] { "flag", "State_ID", "State_Name", "State_UpdatedBy" },
                    new string[] { "4", ViewState["State_ID"].ToString(), txtState_Name.Text.Trim(), ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('State Name already exist.');", true);
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

            ds = objdb.ByProcedure("SpAdminState",
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
        ViewState["State_ID"] = GridView1.SelectedValue.ToString();
        lblMsg.Text = "";
        ds = objdb.ByProcedure("SpAdminState",
                   new string[] { "flag", "State_ID" },
                   new string[] { "3", ViewState["State_ID"].ToString() }, "dataset");

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtState_Name.Text = ds.Tables[0].Rows[0]["State_Name"].ToString();
            btnSave.Text = "Edit";
        }

    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
        string State_ID = chk.ToolTip.ToString();
        string State_IsActive = "0";
        if (chk != null & chk.Checked)
        {
            State_IsActive = "1";
        }
        objdb.ByProcedure("SpAdminState",
                   new string[] { "flag", "State_IsActive", "State_ID", "State_UpdatedBy" },
                   new string[] { "5", State_IsActive, State_ID, ViewState["Emp_ID"].ToString() }, "dataset");


    }
    protected void ClearText()
    {
        txtState_Name.Text = "";
        ViewState["State_ID"] = "0";
        btnSave.Text = "Save";
    }
}