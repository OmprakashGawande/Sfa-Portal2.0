using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_Admin_AdminDivision : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                ds = objdb.ByProcedure("SpAdminState",
                        new string[] { "flag" },
                        new string[] { "2" }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {
                    ddlState_Name.DataTextField = "State_Name";
                    ddlState_Name.DataValueField = "State_ID";
                    ddlState_Name.DataSource = ds;
                    ddlState_Name.DataBind();
                    ddlState_Name.Items.Insert(0, new ListItem("Select", "0"));
                }
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Division_ID"] = "0";
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
            string Division_IsActive = "1";
            if (ddlState_Name.SelectedIndex == 0)
            {
                msg += "Select State Name. \\n";
            }
            if (txtDivision_Name.Text == "")
            {
                msg += "Enter Division Name. \\n";
            }
            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpAdminDivision", new string[] { "flag", "Division_Name", "Division_ID", }, new string[] { "9", txtDivision_Name.Text, ViewState["Division_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }

                if (btnSave.Text == "Save" && ViewState["Division_ID"].ToString() == "0" && Status == 0)
                {
                    objdb.ByProcedure("SpAdminDivision",
                    new string[] { "flag", "Division_IsActive", "State_ID", "Division_Name", "Division_UpdatedBy" },
                    new string[] { "0", Division_IsActive, ddlState_Name.SelectedValue.ToString(), txtDivision_Name.Text.Trim(), ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
                else if (btnSave.Text == "Edit" && ViewState["Division_ID"].ToString() != "0" && Status == 0)
                {
                    objdb.ByProcedure("SpAdminDivision",
                    new string[] { "flag", "Division_ID", "State_ID", "Division_Name", "Division_UpdatedBy" },
                    new string[] { "4", ViewState["Division_ID"].ToString(), ddlState_Name.SelectedValue.ToString(), txtDivision_Name.Text.Trim(), ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Division Name already exist.');", true);
                    ClearText();
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

            ds = objdb.ByProcedure("SpAdminDivision",
                new string[] { "flag" },
                new string[] { "6" }, "dataset");
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
        try
        {
            lblMsg.Text = "";
            ClearText();
            ViewState["Division_ID"] = GridView1.SelectedValue.ToString();
            ds = objdb.ByProcedure("SpAdminDivision", new string[] { "flag", "Division_ID" }, new string[] { "3", ViewState["Division_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDivision_Name.Text = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                ddlState_Name.ClearSelection();
                ddlState_Name.Items.FindByValue(ds.Tables[0].Rows[0]["State_ID"].ToString()).Selected = true;
               
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ClearText();
            GridView1.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        txtDivision_Name.Text = "";
        ddlState_Name.ClearSelection();
        ViewState["Division_ID"] = "0";
        btnSave.Text = "Save";
       
    }


    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
        string Division_ID = chk.ToolTip.ToString();
        string Division_IsActive = "0";
        if (chk != null & chk.Checked)
        {
            Division_IsActive = "1";
        }
        objdb.ByProcedure("SpAdminDivision",
                   new string[] { "flag", "Division_IsActive", "Division_ID", "Division_UpdatedBy" },
                   new string[] { "5", Division_IsActive, Division_ID, ViewState["Emp_ID"].ToString() }, "dataset");

    }
}