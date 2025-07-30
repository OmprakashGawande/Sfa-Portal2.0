using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_HR_HRYearMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                lblMsg.Text = "";
                FillGrid();
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Year_ID"] = "0";
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
            ds = objdb.ByProcedure("SpHrYear_Master",
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
            string Year_IsActive = "1";
            if (txtFinancial_Year.Text == "")
            {
                msg = "Please Enter Financial Year";
            }
            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag", "Financial_Year", "Year_ID" }, new string[] { "6", txtFinancial_Year.Text, ViewState["Year_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (btnSave.Text == "Save" && ViewState["Year_ID"].ToString() == "0" && Status == 0)
                {
                    objdb.ByProcedure("SpHrYear_Master",
                    new string[] { "flag", "Financial_Year", "Year_IsActive", "Year_UpdatedBy" },
                    new string[] { "0", txtFinancial_Year.Text, Year_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillGrid();
                    txtFinancial_Year.Text = "";

                }
                else if (btnSave.Text == "Edit" && ViewState["Year_ID"].ToString() != "0" && Status == 0)
                {

                    objdb.ByProcedure("SpHrYear_Master",
                    new string[] { "flag", "Year_ID", "Financial_Year", "Year_UpdatedBy" },
                    new string[] { "5", ViewState["Year_ID"].ToString(), txtFinancial_Year.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillGrid();
                    txtFinancial_Year.Text = "";
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Financial Year is already exist.');", true);
                    txtFinancial_Year.Text = "";
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
            ViewState["Year_ID"] = GridView1.SelectedValue.ToString();
            ds = objdb.ByProcedure("SpHrYear_Master",
                        new string[] { "flag", "Year_ID" },
                        new string[] { "3", ViewState["Year_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                txtFinancial_Year.Text = ds.Tables[0].Rows[0]["Financial_Year"].ToString();
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
            string Year_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpHrYear_Master",
                   new string[] { "flag", "Year_ID", "Year_IsActive", "Year_UpdatedBy" },
                   new string[] { "4", Year_ID, "0", ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}