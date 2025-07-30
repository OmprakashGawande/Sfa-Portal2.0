using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class mis_HR_HREmpList : System.Web.UI.Page
{
    DataSet ds;
    string flag;
   // AbstApiDBApi objdb = new APIProcedure();
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillOffice();
                    flag = "22";
                    FillList(flag);
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
    protected void FillOffice()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "8" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlAdminOffice_ID.DataSource = ds;
                ddlAdminOffice_ID.DataTextField = "Office_Name";
                ddlAdminOffice_ID.DataValueField = "Office_ID";
                ddlAdminOffice_ID.DataBind();
                ddlAdminOffice_ID.Items.Insert(0, new ListItem("All", "0"));
                ddlAdminOffice_ID.SelectedValue = objdb.Office_ID();
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillList(string flag)
    {
        try
        {
            Repeater1.DataSource = null;
            Repeater1.DataBind();
            ds = null;
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] {flag, ddlAdminOffice_ID.SelectedValue }, "dataset");
            int rowcount = ds.Tables[0].Rows.Count;
            lblNoOfEmployee.Text = rowcount.ToString();
            if(ds != null &&  rowcount> 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
            else 
            {
                Repeater1.DataSource = null;
                Repeater1.DataBind();
                lblMsg.Text = "No Record Found.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlAdminOffice_ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            flag = "22";
            FillList(flag);
            lnkViewAllEmployee.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkViewAllEmployee_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            flag = "23" ;
            FillList(flag);
            lnkViewAllEmployee.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}