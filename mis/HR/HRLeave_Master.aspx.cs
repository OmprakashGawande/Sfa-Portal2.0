using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_HR_HRLeave_Master : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblMsg.Text = "";
                if (Session["Emp_ID"] != null)
                {
                    FillGrid();

                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["LeaveMaster_ID"] = "0";
                    FillGrid();
                    FillFinancialYear();
                    FillLeaveType();
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
    protected void FillFinancialYear()
    {
        try
        {
            ds = objdb.ByProcedure("SpHrYear_Master",
                           new string[] { "flag" },
                           new string[] { "2" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlFinancial_Year.DataTextField = "Year";
                ddlFinancial_Year.DataValueField = "Year";
                ddlFinancial_Year.DataSource = ds;
                ddlFinancial_Year.DataBind();
                ddlFinancial_Year.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillLeaveType()
    {
        try
        {
            ds = objdb.ByProcedure("SpHRLeave_Type",
                                    new string[] { "flag" },
                                    new string[] { "2" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlLeave_Type.DataTextField = "Leave_Type";
                ddlLeave_Type.DataValueField = "Leave_ID";
                ddlLeave_Type.DataSource = ds;
                ddlLeave_Type.DataBind();
                ddlLeave_Type.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        ds = objdb.ByProcedure("SpHRLeave_Master",
                    new string[] { "flag" },
                    new string[] { "7" }, "dataset");
        if (ds.Tables[0].Rows.Count != 0)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

    }
    protected void ClearText()
    {
        try
        {
            ddlFinancial_Year.ClearSelection();
            ddlLeave_Type.ClearSelection();
            txtLeave_Days.Text = "";
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
            string LeaveMaster_IsActive = "1";
            if (ddlFinancial_Year.SelectedIndex == 0)
            {
                msg += "Please Select Year";
            }
            if (ddlLeave_Type.SelectedIndex == 0)
            {
                msg += "Please Select Leave Type";
            }

            if (txtLeave_Days.Text == "")
            {
                msg += "Please Enter Days of Leave";
            }

            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpHRLeave_Master", new string[] { "flag", "Financial_Year", "LeaveType_ID", "LeaveMaster_ID" },
                    new string[] { "6", ddlFinancial_Year.SelectedValue.ToString(), ddlLeave_Type.SelectedValue.ToString(), ViewState["LeaveMaster_ID"].ToString() }, "dataset");

                if (btnSave.Text == "Save" && ds.Tables[0].Rows.Count == 0)
                {
                    objdb.ByProcedure("SpHRLeave_Master",
                    new string[] { "flag", "LeaveMaster_IsActive", "Financial_Year", "LeaveType_ID", "Leave_Days", "LeaveMaster_UpdatedBy" },
                    new string[] { "0", LeaveMaster_IsActive, ddlFinancial_Year.SelectedValue.ToString(), ddlLeave_Type.SelectedValue.ToString(), txtLeave_Days.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillGrid();
                    ClearText();

                }
                else if (btnSave.Text == "Edit" && ds.Tables[0].Rows.Count != 0)
                {

                    objdb.ByProcedure("SpHRLeave_Master",
                    new string[] { "flag", "LeaveMaster_ID", "Financial_Year", "LeaveType_ID", "Leave_Days", "LeaveMaster_UpdatedBy" },
                    new string[] { "5", ViewState["LeaveMaster_ID"].ToString(), ddlFinancial_Year.SelectedValue.ToString(), ddlLeave_Type.SelectedValue.ToString(), txtLeave_Days.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    btnSave.Text = "Save";
                    FillGrid();
                    ClearText();

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Leave Type is already exist in this Year.');", true);
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["LeaveMaster_ID"] = GridView1.SelectedValue.ToString();
            ds = objdb.ByProcedure("SpHRLeave_Master",
                        new string[] { "flag", "LeaveMaster_ID" },
                        new string[] { "3", ViewState["LeaveMaster_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlFinancial_Year.ClearSelection();
                ddlFinancial_Year.Items.FindByValue(ds.Tables[0].Rows[0]["Financial_Year"].ToString()).Selected = true;
                ddlLeave_Type.ClearSelection();
                ddlLeave_Type.Items.FindByValue(ds.Tables[0].Rows[0]["LeaveType_ID"].ToString()).Selected = true;
                txtLeave_Days.Text = ds.Tables[0].Rows[0]["Leave_Days"].ToString();
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string LeaveMaster_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpHRLeave_Master",
                   new string[] { "flag", "LeaveMaster_ID", "LeaveMaster_IsActive", "LeaveMaster_UpdatedBy" },
                   new string[] { "4", LeaveMaster_ID, "0", ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlFinancial_Year_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlLeave_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}