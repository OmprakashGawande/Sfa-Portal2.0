using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_HR_HRLeaveType : System.Web.UI.Page
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
                    ViewState["Leave_ID"] = "0";
                    FillGrid();
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
            ds = objdb.ByProcedure("SpHRLeave_Type",
                        new string[] { "flag" },
                        new string[] { "2" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string Leave_IsActive = "1";
            if (txtLeave_Type.Text == "")
            {
                msg = "Please Enter Leave";
            }
            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpHRLeave_Type", new string[] { "flag", "Leave_Type", "Leave_ID" }, new string[] { "6", txtLeave_Type.Text, ViewState["Leave_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }

                if (btnSave.Text == "Save" && ViewState["Leave_ID"].ToString() == "0" && Status == 0)
                {
                    objdb.ByProcedure("SpHRLeave_Type",
                    new string[] { "flag", "Leave_Type", "Leave_IsActive", "Leave_UpdatedBy" },
                    new string[] { "0", txtLeave_Type.Text, Leave_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();


                }
                else if (btnSave.Text == "Edit" && ViewState["Leave_ID"].ToString() != "0" && Status == 0)
                {

                    objdb.ByProcedure("SpHRLeave_Type",
                    new string[] { "flag", "Leave_ID", "Leave_Type", "Leave_UpdatedBy" },
                    new string[] { "5", ViewState["Leave_ID"].ToString(), txtLeave_Type.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();


                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Leave Type already exist.');", true);
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
            ViewState["Leave_ID"] = GridView1.SelectedValue.ToString();
            ds = objdb.ByProcedure("SpHRLeave_Type",
                        new string[] { "flag", "Leave_ID" },
                        new string[] { "3", ViewState["Leave_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                txtLeave_Type.Text = ds.Tables[0].Rows[0]["Leave_Type"].ToString();
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
            string Leave_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpHRLeave_Type",
                   new string[] { "flag", "Leave_ID", "Leave_IsActive", "Leave_UpdatedBy" },
                   new string[] { "4", Leave_ID, "0", ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Deleted Successfully");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        try
        {
            txtLeave_Type.Text = "";
            ViewState["Leave_ID"] = "0";
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}