using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_FinLevel2 : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    lblMsg.Text = "";
                    lblRecordMsg.Text = "";

                }

                lblRecordMsg.Text = "";
                DataSet dsM = objdb.ByProcedure("SpFinLevel3",
                     new string[] { "flag" },
                     new string[] { "4" }, "dataset");

                if (dsM != null && dsM.Tables[0].Rows.Count > 0)
                {
                    ddlLevel1_ID.DataSource = dsM;
                    ddlLevel1_ID.DataTextField = "Level1_Name";
                    ddlLevel1_ID.DataValueField = "Level1_ID";
                    ddlLevel1_ID.DataBind();
                    ddlLevel1_ID.Items.Insert(0, new ListItem("Select", "0"));
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
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpFinLevel3",
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
            string Level3_IsActive = "1";
            if (ddlLevel1_ID.SelectedIndex == 0)
            {
                msg += "Select Level-1 Name<br/>";
            }
            if (ddlLevel2_ID.SelectedIndex == 0)
            {
                msg += "Select Level-2 Name<br/>";
            }
            if (txtLevel3_Name.Text.Trim() == "")
            {
                msg += "Enter Level-3 Name<br/>";
            }

            if (msg.Trim() == "")
            {
                ds = objdb.ByProcedure("SpFinLevel3",
                      new string[] { "flag", "Level3_Name", "Level2_ID", "Level1_ID" },
                      new string[] { "7", txtLevel3_Name.Text.Trim(), ddlLevel2_ID.SelectedValue, ddlLevel1_ID.SelectedValue }, "dataset");

                if (btnSave.Text == "Accept" && ds.Tables[0].Rows.Count == 0)
                {
                    objdb.ByProcedure("SpFinLevel3",
                      new string[] { "flag", "Level3_IsActive", "Level3_Name", "Level3_IsAppOnHO", "Level2_ID", "Level1_ID", "Level3_UpdatedBy" },
                      new string[] { "0", Level3_IsActive, txtLevel3_Name.Text.Trim(), ddlLevel3_IsAppOnHO.SelectedItem.ToString(), ddlLevel2_ID.SelectedValue, ddlLevel1_ID.SelectedValue, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillGrid();
                }
                else
                {
                    string Level3_Name = ds.Tables[0].Rows[0]["Level3_Name"].ToString();
                    if (Level3_Name == txtLevel3_Name.Text)
                    {
                        //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", "This Level3 Name Is Already Exist.");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('This Level3 Name Is Already Exist');", true);
                    }
                }
                FillGrid();
                ClearField();
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

    protected void ClearField()
    {
        txtLevel3_Name.Text = "";
        ddlLevel1_ID.ClearSelection();
        ddlLevel2_ID.ClearSelection();

    }
    protected void ddlLevel1_ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlLevel2_ID.Items.Clear();
            string Level1_ID = ddlLevel1_ID.SelectedValue;
            DataSet dsLevel2_ID = objdb.ByProcedure("SpFinLevel3",
                 new string[] { "flag", "Level1_ID" },
                 new string[] { "5", Level1_ID }, "dataset");

            if (dsLevel2_ID != null && dsLevel2_ID.Tables[0].Rows.Count > 0)
            {
                ddlLevel2_ID.DataSource = dsLevel2_ID;
                ddlLevel2_ID.DataTextField = "Level2_Name";
                ddlLevel2_ID.DataValueField = "Level2_ID";
                ddlLevel2_ID.DataBind();
                ddlLevel2_ID.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {

            }

            GridView1.DataSource = null;
            GridView1.DataBind();
            DataSet ds1 = objdb.ByProcedure("SpFinLevel3",
                     new string[] { "flag", "Level1_ID" },
                     new string[] { "13", Level1_ID }, "dataset");
            if (ds1.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds1;
                GridView1.DataBind();
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
            string Level3_ID = GridView1.SelectedValue.ToString();
            DataSet dsrecord = objdb.ByProcedure("SpFinLevel3",
                    new string[] { "flag", "Level3_ID" },
                    new string[] { "6", Level3_ID }, "dataset");
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
            string Level3_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string Level3_IsActive = "0";
            objdb.ByProcedure("SpFinLevel3",
                    new string[] { "flag", "Level3_ID", "Level3_IsActive", "Level3_UpdatedBy" },
                    new string[] { "9", Level3_ID, Level3_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlLevel2_ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try{
        GridView1.DataSource = null;
        GridView1.DataBind();
        string Level1_ID = ddlLevel1_ID.SelectedValue;
        string Level2_ID = ddlLevel2_ID.SelectedValue;
        ds = objdb.ByProcedure("SpFinLevel3",
                 new string[] { "flag", "Level1_ID", "Level2_ID" },
                 new string[] { "3", Level1_ID, Level2_ID }, "dataset");
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
}