using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class mis_HR_HREmpListOffice : System.Web.UI.Page
{
    DataSet ds;
    string flag;
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
                    ds = null;
                    ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Emp_ID" }, new string[] { "29", ViewState["Emp_ID"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["Office_ID"] = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                    }
                    else { }
                    
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
   
    protected void FillList(string flag)
    {
        try
        {
            Repeater1.DataSource = null;
            Repeater1.DataBind();
            ds = null;
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { flag, ViewState["Office_ID"].ToString() }, "dataset");
            int rowcount = ds.Tables[0].Rows.Count;
            lblNoOfEmployee.Text = rowcount.ToString();
            if (ds != null && rowcount > 0)
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
            flag = "23";
            FillList(flag);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}