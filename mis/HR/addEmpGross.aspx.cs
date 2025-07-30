using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class mis_HR_addEmpGross : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    IFormatProvider culture = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        string s = "";
        if (Request.QueryString.AllKeys.Contains("A"))
        {
            if (Request.QueryString["A"] != null)
            {
                s = Request.QueryString["A"].ToString();
            }
        }
        if (s != "2365")
        {
           Response.Redirect("../Login.aspx");
        }

        if (Session["Office_ID"] != null)
        {
            if (Session["Office_ID"].ToString() == "1")
            {
                if (!IsPostBack)
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                    fillgrid();
                }
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void fillgrid()
    {
        try
        {
            gridview.DataSource = null;
            gridview.DataBind();
            gridfieldset.Visible =  false;
            ds = obj.ByProcedure("Update_Emp_GrossSalery", new string[] { "flag" }, new string[] {"2" }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    gridfieldset.Visible = true;
                    gridview.DataSource = ds.Tables[0];
                    gridview.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! ", ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        if (gridview.Rows.Count > 0)
        {
            foreach (GridViewRow dd in gridview.Rows)
            {
                Label EmpID = (Label)dd.FindControl("Emp_ID");
                TextBox EmpGrossSalery = (TextBox)dd.FindControl("Emp_GrossSalery");
                CheckBox cbx = (CheckBox)dd.FindControl("CheckBox1");

                if (EmpGrossSalery.Text == "")
                {
                    EmpGrossSalery.Text = "0";
                }
                if (cbx.Checked)
                {
                    ds = obj.ByProcedure("Update_Emp_GrossSalery",
                        new string[] { "flag","Emp_ID","Emp_GrossSalery"},
                        new string[] { "1",EmpID.Text,EmpGrossSalery.Text.Trim()}, "dataset");
                }

            }
            fillgrid();
            lblMsg.Text = obj.Alert("fa-check", "alert-success", "ThankYou", "Operation completed successfully");

        }
    }
}