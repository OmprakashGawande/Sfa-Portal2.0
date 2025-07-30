using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class mis_Daily_Task_Rpt_Late_Login : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();    
    DataSet ds = new DataSet();
    IFormatProvider culture = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null)
        {
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                txt_date.Attributes.Add("readonly", "readonly");
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["OfficeType_ID"] = Session["OfficeType_ID"].ToString();
              

                DateTime dd = DateTime.Now;
                txt_date.Text = dd.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);


            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ds.Clear();
            if (e.CommandName == "Select")
            {
                ViewState["Emp_Late_ID"] = e.CommandArgument.ToString();
                ds = objdb.ByProcedure("USP_Emp_LateLogin_Delete", new string[] { "Emp_Late_ID" }, new string[] { ViewState["Emp_Late_ID"].ToString() }, "Dataset");
               if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Enabled", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        fillgrid();
                    }
                    else if (ds != null && ds.Tables[0].Rows[0]["Msg"].ToString() == "NOT OK")
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Disabled", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                }
            }

        }
        catch (Exception)
        {

            throw;
        }
       
    }

    protected void fillgrid()
    {
        DateTime dt5 = DateTime.ParseExact(txt_date.Text, "dd/MM/yyyy", culture);
        string Latedate = dt5.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        GridView1.DataSource = null;
        GridView1.DataBind();
        ds = objdb.ByProcedure("USP_Emp_LateLogin_GetAll", new string[] { "Late_Date" }, new string[] { Latedate }, "Dataset");
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (txt_date.Text!="")
        {

       
        try
        {
            ds.Clear();
            if (Page.IsValid)
            {
                fillgrid();
            }
        }
        catch (Exception)
        {

            throw;
        }
        }
    }
}