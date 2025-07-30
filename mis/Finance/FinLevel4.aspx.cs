using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_Finance_FinLevel4 : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                }
                else
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
                FillGrid();
                lblMsg.Text = "";
                ds = objdb.ByProcedure("SpFinLevel2",
                            new string[] { "flag" },
                            new string[] { "1" }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {
                    ddlLevel1_ID.DataTextField = "Level1_Name";
                    ddlLevel1_ID.DataValueField = "Level1_ID";
                    ddlLevel1_ID.DataSource = ds;
                    ddlLevel1_ID.DataBind();
                    ddlLevel1_ID.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        ds = objdb.ByProcedure("SpFinLevel4",
                       new string[] { "flag" },
                       new string[] { "7" }, "dataset");
      
            GridView1.DataSource = ds;
            GridView1.DataBind();
        
    }
    protected void ddlLevel1_ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string Level1_ID = ddlLevel1_ID.SelectedValue.ToString();
            ds = objdb.ByProcedure("SpFinLevel2",
                           new string[] { "flag", "Level1_ID" },
                           new string[] { "3", Level1_ID }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlLevel2_ID.DataTextField = "Level2_Name";
                ddlLevel2_ID.DataValueField = "Level2_ID";
                ddlLevel2_ID.DataSource = ds;
                ddlLevel2_ID.DataBind();
                ddlLevel2_ID.Items.Insert(0, new ListItem("Select", "0"));
            }

           
            ds = objdb.ByProcedure("SpFinLevel4",
                       new string[] { "flag", "Level1_ID" },
                       new string[] { "9", Level1_ID }, "dataset");
           
                GridView1.DataSource = ds;
                GridView1.DataBind();
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void ddlLevel2_ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string Level2_ID = ddlLevel2_ID.SelectedValue.ToString();
            ds = objdb.ByProcedure("SpFinLevel3",
                           new string[] { "flag", "Level2_ID" },
                           new string[] { "12", Level2_ID }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlLevel3_ID.DataTextField = "Level3_Name";
                ddlLevel3_ID.DataValueField = "Level3_ID";
                ddlLevel3_ID.DataSource = ds;
                ddlLevel3_ID.DataBind();
                ddlLevel3_ID.Items.Insert(0, new ListItem("Select", "0"));
            }

            ds = objdb.ByProcedure("SpFinLevel4",
                       new string[] { "flag", "Level2_ID" },
                       new string[] { "10", Level2_ID }, "dataset");
            
                GridView1.DataSource = ds;
                GridView1.DataBind();
            

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
            string Level4_IsActive = "1";
            if (ddlLevel1_ID.SelectedIndex < 0)
            {
                msg += "Select Level I /n";
            }
            if (ddlLevel2_ID.SelectedIndex < 0)
            {
                msg += "Select Level II /n";
            }
            if (ddlLevel3_ID.Text.Trim() == "")
            {
                msg += "Enter Office Email /n";
            }
            if (txtLevel4_Name.Text.Trim() == "")
            {
                msg += "Enter Level- 4 Name /n";
            }
            if (ddlLevel3_IsAppOnHO.SelectedIndex < 0)
            {
                msg += "Select Is Appoint On HO /n";
            }
            if (msg.Trim() == "")
            {
                ds = objdb.ByProcedure("SpFinLevel4",
                      new string[] { "flag", "Level4_Name", "Level3_ID", "Level2_ID", "Level1_ID" },
                      new string[] { "8", txtLevel4_Name.Text.Trim(), ddlLevel3_ID.SelectedValue.ToString(), ddlLevel2_ID.SelectedValue.ToString(), ddlLevel1_ID.SelectedValue.ToString() }, "dataset");

                if (btnSave.Text == "Accept" && ds.Tables[0].Rows.Count == 0)
                {
                    objdb.ByProcedure("SpFinLevel4",
                     new string[] { "flag", "Level4_IsActive", "Level4_Name", "Level4_IsAppOnHO", "Level3_ID", "Level2_ID", "Level1_ID", "Level4_UpdatedBy" },
                     new string[] { "0", Level4_IsActive, txtLevel4_Name.Text, ddlLevel3_IsAppOnHO.SelectedItem.Text, ddlLevel3_ID.SelectedValue.ToString(), ddlLevel2_ID.SelectedValue.ToString(), ddlLevel1_ID.SelectedValue.ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillGrid();
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-info", "Thank You!", "Level Name is Already Exist");
                    FillGrid();
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", msg);
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
            string Level4_ID = GridView1.SelectedValue.ToString();
            DataSet ds1 = objdb.ByProcedure("SpFinLevel4",
                    new string[] { "flag", "Level4_ID" },
                    new string[] { "3", Level4_ID }, "dataset");
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
            string Level4_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string Level4_IsActive = "0";
            objdb.ByProcedure("SpFinLevel4",
                    new string[] { "flag", "Level4_ID", "Level4_IsActive", "Level4_UpdatedBy" },
                    new string[] { "6", Level4_ID, Level4_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Delete Successfully Completed");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}