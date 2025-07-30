using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_Admin_AdminDistrict : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null && Session["Emp_ID"] != "" && Session["Office_ID"] != null && Session["Office_ID"] != "")
        {
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                ClearText();
                ds = objdb.ByProcedure("SpAdminState",
                        new string[] { "flag" },
                        new string[] { "6" }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {
                    ddlState_Name.DataTextField = "State_Name";
                    ddlState_Name.DataValueField = "State_ID";
                    ddlState_Name.DataSource = ds;
                    ddlState_Name.DataBind();
                    ddlState_Name.Items.Insert(0, new ListItem("Select", "0"));
                }
                ViewState["Emp_ID"] = Session["Emp_ID"];
                ViewState["Office_ID"] = Session["Office_ID"];
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                FillGrid();
                lblMsg.Text = "";
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
            string District_IsActive = "1";
            if (ddlState_Name.SelectedIndex <= 0)
            {
                msg = "Please select State";
            }
            if (ddlDivision_Name.SelectedIndex <= 0)
            {
                msg = "Please select Division";
            }
            if (txtDistrict_Name.Text == "")
            {
                msg += "Enter Division Name. \\n";
            }
            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpAdminDistrict", new string[] { "flag", "District_Name", "District_ID", }, new string[] { "11", txtDistrict_Name.Text, ViewState["District_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }


                if (btnSave.Text == "Save" && ViewState["District_ID"].ToString() == "0" && Status == 0)
                {
                    objdb.ByProcedure("SpAdminDistrict",
                    new string[] { "flag", "District_IsActive", "State_ID", "Division_ID", "District_Name", "District_UpdatedBy" },
                    new string[] { "0", District_IsActive, ddlState_Name.SelectedValue.ToString(), ddlDivision_Name.SelectedValue.ToString(), txtDistrict_Name.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();

                }
                else if (btnSave.Text == "Edit" && ViewState["District_ID"].ToString() != "0" && Status == 0)
                {
                    objdb.ByProcedure("SpAdminDistrict",
                    new string[] { "flag", "District_ID", "State_ID", "Division_ID", "District_Name", "District_UpdatedBy" },
                    new string[] { "3", ViewState["District_ID"].ToString(), ddlState_Name.SelectedValue.ToString(), ddlDivision_Name.SelectedValue.ToString(), txtDistrict_Name.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('District Name is already exist.');", true);
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
            ds = objdb.ByProcedure("SpAdminDistrict", 
                new string[] { "flag" }, 
                new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
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
            ddlDivision_Name.Items.Clear();
            ViewState["District_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpAdminDistrict", new string[] { "flag", "District_ID" }, new string[] { "6", ViewState["District_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlState_Name.ClearSelection();
                ddlState_Name.Items.FindByValue(ds.Tables[0].Rows[0]["State_ID"].ToString()).Selected = true;
                if (ddlState_Name.SelectedIndex > 0)
                {
                    DataSet ds1 = objdb.ByProcedure("SpAdminDivision", 
                        new string[] { "flag", "State_ID" }, 
                        new string[] { "7", ddlState_Name.SelectedValue.ToString() }, "dataset");

                    if (ds1.Tables[0].Rows.Count != 0)
                    {
                        ddlDivision_Name.DataTextField = "Division_Name";
                        ddlDivision_Name.DataValueField = "Division_ID";
                        ddlDivision_Name.DataSource = ds1;
                        ddlDivision_Name.DataBind();
                        ddlDivision_Name.Items.Insert(0, "Select");
                        ddlDivision_Name.ClearSelection();
                        ddlDivision_Name.Items.FindByValue(ds.Tables[0].Rows[0]["Division_ID"].ToString()).Selected = true;
                    }
                    txtDistrict_Name.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                    btnSave.Text = "Edit";
                }
                
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
        txtDistrict_Name.Text = "";
        ddlDivision_Name.ClearSelection();
        ddlState_Name.ClearSelection();
        ViewState["District_ID"] = "0";
        btnSave.Text = "Save";
    }
    protected void ddlState_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminDivision", new string[] { "flag", "State_ID" }, new string[] { "7", ddlState_Name.SelectedValue.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDivision_Name.DataTextField = "Division_Name";
                ddlDivision_Name.DataValueField = "Division_ID";
                ddlDivision_Name.DataSource = ds;
                ddlDivision_Name.DataBind();
                ddlDivision_Name.Items.Insert(0, "Select");

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
        string District_ID = chk.ToolTip.ToString();
        string District_IsActive = "0";
        if (chk != null & chk.Checked)
        {
            District_IsActive = "1";
        }
        objdb.ByProcedure("SpAdminDistrict",
                   new string[] { "flag", "District_IsActive", "District_ID", "District_UpdatedBy" },
                   new string[] { "4", District_IsActive, District_ID, ViewState["Emp_ID"].ToString() }, "dataset");

    }
}