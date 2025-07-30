using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
public partial class mis_HR_HREmpWiseBalanceLeaveAllEmp : System.Web.UI.Page
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
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Department_ID"] = Session["Department_ID"].ToString();
                    FillDropDown();
                    FillYear();
                    //FillGrid();
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
    protected void FillDropDown()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.Items.Insert(0, new ListItem("Select Office", "0"));
                ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
                if (ddlOfficeName.SelectedIndex > 0)
                {
                    FillEmp();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEmp()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag"}, new string[] { "33" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlLeaveType.DataSource = ds;
                ddlLeaveType.DataTextField = "Leave_Type";
                ddlLeaveType.DataValueField = "Leave_ID";
                ddlLeaveType.DataBind();
                ddlLeaveType.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillYear()
    {
        try
        {
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlFinancialYear.DataSource = ds;
                ddlFinancialYear.DataTextField = "Year";
                ddlFinancialYear.DataValueField = "Year";
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select Financial Year", "0"));
            }
            ddlFinancialYear.SelectedValue = DateTime.Now.Year.ToString();
            ddlFinancialYear.Enabled = false;
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
            lblMsg2.Text = "";
            lblInfo.Text = "Year: " + ddlFinancialYear.SelectedItem.Text.ToString() + ", Office: " + ddlOfficeName.SelectedItem.Text.ToString() + ", Leave Type: " + ddlLeaveType.SelectedItem.Text.ToString();
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "Leave_ID", "Financial_Year","Office_ID" },
                  new string[] { "34", ddlLeaveType.SelectedValue.ToString(), ddlFinancialYear.SelectedValue.ToString(),ddlOfficeName.SelectedValue.ToString() }, "datatset");
            if (ds.Tables[0].Rows.Count != 0)
            {

                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                lblMsg2.Text = "No Record Found...";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlOfficeName.SelectedIndex > 0)
            {
                lblMsg.Text = "";
                lblInfo.Text = "";
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
                FillEmp();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            if (ddlOfficeName.SelectedIndex == 0)
            {
                msg += "Select Office Name";
            }
            if (ddlLeaveType.SelectedIndex == 0)
            {
                msg += "Select Leave Type";
            }

            if (msg == "")
            {
                FillGrid();
            }
            else
            {
                lblMsg.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}