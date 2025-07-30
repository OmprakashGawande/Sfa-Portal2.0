        using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_HR_HREmpBalanceLeave : System.Web.UI.Page
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
                    FillLeaveType();
                    FillGrid1();
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
    protected void FillLeaveType()
    {
        try
        {

            ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlLeaveTpye.DataSource = ds;
                ddlLeaveTpye.DataTextField = "Leave_Type";
                ddlLeaveTpye.DataValueField = "Leave_ID";
                ddlLeaveTpye.DataBind();
                ddlLeaveTpye.Items.Insert(0, new ListItem("All", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid1()
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag" },
                  new string[] { "14" }, "datatset");
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
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveType" },
                  new string[] { "10", ddlLeaveTpye.SelectedValue.ToString() }, "datatset");
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
    protected void ddlLeaveTpye_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlLeaveTpye.SelectedIndex > 0)
            {
                FillGrid();
            }
            else
            {
                FillGrid1();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}