using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_Finance_FinLevel2 : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            lblMsg.Text = "";
            if (!IsPostBack)
            {

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
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Level2_ID"] = "0";
                FillGrid();
               
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string Level2_IsActive = "1";

            if (ddlLevel1_ID.SelectedIndex == 0)
            {
                msg += "Select Level -I Name. \\n";
            }
            if (txtLevel2_Name.Text == "")
            {
                msg += "Enter Level -II Name. \\n";
            }

            if (msg.Trim() == "")
            {
                ds = objdb.ByProcedure("SpFinLevel2",
                     new string[] { "flag", "Level2_Name", "Level1_ID" },
                     new string[] { "7", txtLevel2_Name.Text.Trim(), ddlLevel1_ID.SelectedValue.ToString() }, "dataset");

                if (btnSave.Text == "Accept" && ds.Tables[0].Rows.Count == 0)
                {
                    objdb.ByProcedure("SpFinLevel2",
                    new string[] { "flag", "Level2_IsActive", "Level2_Name", "Level2_IsAppOnHO", "Level1_ID", "Level2_UpdatedBy" },
                    new string[] { "0", Level2_IsActive, txtLevel2_Name.Text.Trim(), ddlLevel2_IsAppOnHO.SelectedItem.Text, ddlLevel1_ID.SelectedValue.ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
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

            ds = objdb.ByProcedure("SpFinLevel2",
                new string[] { "flag" },
                new string[] { "6" }, "dataset");
            GridView1.DataSource = ds;
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        ddlLevel1_ID.ClearSelection();
        txtLevel2_Name.Text = "";

        ViewState["Level2_ID"] = "0";
        btnSave.Text = "Accept";
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ClearText();
            string Level2_IsActive = "0";
            string Level2_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpFinLevel2",
                   new string[] { "flag", "Level2_ID", "Level2_IsActive", "Level2_UpdatedBy" },
                   new string[] { "5", Level2_ID, Level2_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();

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
            string Level2_ID = GridView1.SelectedValue.ToString();
            DataSet ds1 = objdb.ByProcedure("SpFinLevel2",
                    new string[] { "flag", "Level2_ID" },
                    new string[] { "3", Level2_ID }, "dataset");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlLevel1_ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        ddlLevel2_IsAppOnHO.Focus();
    }
}