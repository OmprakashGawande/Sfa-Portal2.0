using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class mis_HR_HRProjectMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["Emp_ID"] != null)
        {
            if(!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Project_ID"] = "0";
                FillGrid();
            }
            
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void ClearText()
    {
        txtProjectName.Text = "";
    }
    protected void FillGrid()
    {
        try
        {
            ds = objdb.ByProcedure("SpHRProjectMaster", new string[] { "flag" }, new string[] { "1"}, "dataset");
            if(ds!= null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
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
            if(txtProjectName.Text == "")
            {
                msg += "Enter Project Name </br>";
            }
            if(msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpHRProjectMaster", new string[] { "flag", "Project_Name", "Project_ID" }, new string[] { "5", txtProjectName.Text, ViewState["Project_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (btnSave.Text == "Save" && ViewState["Project_ID"].ToString() == "0" && Status == 0)
                {
                            objdb.ByProcedure("SpHRProjectMaster", new string[] { "flag", "Project_Name", "Project_UpdatedBy" }, new string[] { "0", txtProjectName.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                            ClearText();
                            FillGrid();

                }
                else if (btnSave.Text == "Edit" && ViewState["Project_ID"].ToString() != "0" && Status == 0)
                {
                    objdb.ByProcedure("SpHRProjectMaster", new string[] { "flag","Project_ID", "Project_Name", "Project_UpdatedBy" }, new string[] { "2",ViewState["Project_ID"].ToString(), txtProjectName.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    btnSave.Text = "Save";
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Project name is already exist.');", true);
                    lblMsg.Text = "";
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
            ClearText();
            ViewState["Project_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpHRProjectMaster", new string[] { "flag", "Project_ID" }, new string[] { "4", ViewState["Project_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtProjectName.Text = ds.Tables[0].Rows[0]["Project_Name"].ToString();
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
            string ProjectID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string Project_IsActive = "0";
            objdb.ByProcedure("SpHRProjectMaster", new string[] { "flag", "Project_ID", "Project_IsActive", "Project_UpdatedBy" }, new string[] { "3", ProjectID, Project_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ClearText();
            GridView1.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}