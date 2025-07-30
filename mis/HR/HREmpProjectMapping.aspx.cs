using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mis_HR_HREmpProjectMapping : System.Web.UI.Page
{
    DataSet ds, ds1;
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
                    FillEmployee();
                    divProject.Visible = false;
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
    protected void FillEmployee()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpHREmpProjMapping", new string[] { "flag" }, new string[] { "5" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmp_Name.DataSource = ds;
                ddlEmp_Name.DataTextField = "Emp_Name";
                ddlEmp_Name.DataValueField = "Emp_ID";
                ddlEmp_Name.DataBind();
                ddlEmp_Name.Items.Insert(0, new ListItem("Select", "0"));
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillProject()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpHREmpProjMapping", new string[] { "flag" }, new string[] { "6" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                chkProject.DataSource = ds;
                chkProject.DataTextField = "Project_Name";
                chkProject.DataValueField = "Project_ID";
                chkProject.DataBind();

                foreach (ListItem item in chkProject.Items)
                {
                    item.Selected = false;
                    item.Enabled = false;
                }
            }
            else { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlEmp_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            divProject.Visible = true;
            FillProject();
            foreach (ListItem item in chkProject.Items)
            {
                item.Enabled = true;
                string Project_ID = item.Value.ToString();
                ds1 = objdb.ByProcedure("SpHREmpProjMapping", new string[] { "flag", "Emp_ID", "Project_ID" }, new string[] { "7", ddlEmp_Name.SelectedValue, Project_ID }, "dataset");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    string EmpProj_IsActive = ds1.Tables[0].Rows[0]["EmpProj_IsActive"].ToString();
                    if (EmpProj_IsActive == "1")
                    {
                        item.Selected = true;
                    }
                    else
                    {
                        item.Selected = false;
                    }
                }
                else
                {
                    item.Selected = false;
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnMap_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            int count = 0;

            foreach (ListItem item in chkProject.Items)
            {
                if (item.Selected)
                {
                    count++;
                }
                else { }
            }
            if (count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select Atleast One Project.');", true);
                // msg += "Select Atleast One Project.<br/>";
            }
            else{
            string msg = "";
            if (ddlEmp_Name.SelectedIndex <= 0)
            {
                msg += "Select Employee Name<br/>";
            }
            if (msg == "")
            {
                ds = null;
                ds = objdb.ByProcedure("SpHREmpProjMapping", new string[] { "flag", "Emp_ID" }, new string[] { "3", ddlEmp_Name.SelectedValue}, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    // Update Mapping
                    foreach (ListItem item in chkProject.Items)
                    {
                       string Project_ID =  item.Value.ToString();
                       string EmpProj_IsActive = "0";
                       if (item.Selected)
                       {
                           EmpProj_IsActive = "1";
                       }
                       else
                       {
                           EmpProj_IsActive = "0";
                       }
                       ds1 = objdb.ByProcedure("SpHREmpProjMapping", new string[] { "flag", "Emp_ID", "Project_ID" }, new string[] { "8", ddlEmp_Name.SelectedValue, Project_ID }, "dataset");
                       if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                       {
                           string EmpProj_ID = ds1.Tables[0].Rows[0]["EmpProj_ID"].ToString();
                           objdb.ByProcedure("SpHREmpProjMapping",
                            new string[] { "flag", "EmpProj_ID", "Emp_ID", "Project_ID", "EmpProj_IsActive", "UpdatedBy" },
                            new string[] { "4", EmpProj_ID, ddlEmp_Name.SelectedValue, item.Value, EmpProj_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");
                           lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                       }
                       else
                       {
                           //Insert(If add new project)
                           objdb.ByProcedure("SpHREmpProjMapping",
                            new string[] { "flag", "Emp_ID", "Project_ID", "EmpProj_IsActive", "UpdatedBy" },
                            new string[] { "1", ddlEmp_Name.SelectedValue, item.Value, EmpProj_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");
                           lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                       }
                    }
                }
                else
                {
                    foreach (ListItem item in chkProject.Items)
                    {
                        string EmpProj_IsActive = "0";
                        if (item.Selected)
                        {
                            EmpProj_IsActive = "1";
                        }
                        else
                        {
                            EmpProj_IsActive = "0";
                        }
                        objdb.ByProcedure("SpHREmpProjMapping", 
                            new string[] { "flag", "Emp_ID", "Project_ID", "EmpProj_IsActive", "UpdatedBy" },
                            new string[] { "1", ddlEmp_Name.SelectedValue, item.Value, EmpProj_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    }
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
            }
        }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}