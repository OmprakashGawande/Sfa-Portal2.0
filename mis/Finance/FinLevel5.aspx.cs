using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_FinLevel5 : System.Web.UI.Page
{
    DataSet ds;
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
                    FillLevel1();
                    FillGrid();
                    lblMsg.Text = "";
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
    protected void FillLevel1()
    {
        try
        {

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

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillLevel2()
    {
        try
        {
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
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillLevel3()
    {
        try
        {
            DataSet dsLevel3_ID = objdb.ByProcedure("SpFinLevel5",
                 new string[] { "flag", "Level1_ID", "Level2_ID" },
                 new string[] { "7", ddlLevel1_ID.SelectedValue, ddlLevel2_ID.SelectedValue }, "dataset");

            if (dsLevel3_ID != null && dsLevel3_ID.Tables[0].Rows.Count > 0)
            {
                ddlLevel3_ID.DataSource = dsLevel3_ID;
                ddlLevel3_ID.DataTextField = "Level3_Name";
                ddlLevel3_ID.DataValueField = "Level3_ID";
                ddlLevel3_ID.DataBind();
                ddlLevel3_ID.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillLevel4()
    {
        try
        {
            string Level4_ID = ddlLevel1_ID.SelectedValue;
            DataSet dsLevel4_ID = objdb.ByProcedure("SpFinLevel5",
                 new string[] { "flag", "Level1_ID", "Level2_ID", "Level3_ID" },
                 new string[] { "8", Level4_ID, ddlLevel2_ID.SelectedValue, ddlLevel3_ID.SelectedValue }, "dataset");

            if (dsLevel4_ID != null && dsLevel4_ID.Tables[0].Rows.Count > 0)
            {
                ddlLevel4_ID.DataSource = dsLevel4_ID;
                ddlLevel4_ID.DataTextField = "Level4_Name";
                ddlLevel4_ID.DataValueField = "Level4_ID";
                ddlLevel4_ID.DataBind();
                ddlLevel4_ID.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {

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
            ds = objdb.ByProcedure("SpFinLevel5",
                new string[] { "flag", },
                new string[] { "9"}, "dataset");
           
            GridView1.DataSource = ds;
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlLevel1_ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillLevel2();
            GridView1.DataSource = null;
            GridView1.DataBind();
            DataSet ds1 = objdb.ByProcedure("SpFinLevel5",
                new string[] { "flag", "Level1_ID" },
                new string[] { "10", ddlLevel1_ID.SelectedValue }, "dataset");

            GridView1.DataSource = ds1;
            GridView1.DataBind();

            //FillGrid();

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
            FillLevel3();
            GridView1.DataSource = null;
            GridView1.DataBind();
            DataSet ds1 = objdb.ByProcedure("SpFinLevel5",
                new string[] { "flag", "Level1_ID", "Level2_ID" },
                new string[] { "11", ddlLevel1_ID.SelectedValue, ddlLevel2_ID.SelectedValue }, "dataset");

            GridView1.DataSource = ds1;
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlLevel3_ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillLevel4();
            GridView1.DataSource = null;
            GridView1.DataBind();
           DataSet ds1 = objdb.ByProcedure("SpFinLevel5",
                new string[] { "flag", "Level1_ID", "Level2_ID", "Level3_ID" },
                new string[] { "12", ddlLevel1_ID.SelectedValue, ddlLevel2_ID.SelectedValue, ddlLevel3_ID.SelectedValue }, "dataset");

            GridView1.DataSource = ds1;
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
            string Level5_IsActive = "1";
            if (ddlLevel1_ID.SelectedIndex == 0)
            {
                msg += "Select Level-1 Name<br/>";
            }
            if (ddlLevel2_ID.SelectedIndex == 0)
            {
                msg += "Select Level-2 Name<br/>";
            }
            if (ddlLevel3_ID.SelectedIndex == 0)
            {
                msg += "Select Level-3 Name<br/>";
            }
            if (ddlLevel4_ID.SelectedIndex == 0)
            {
                msg += "Select Level-4 Name<br/>";
            }
            if (txtLevel5_Name.Text.Trim() == "")
            {
                msg += "Enter Level-5 Name<br/>";
            }
         
            if (msg.Trim() == "")
            {
                ds = objdb.ByProcedure("SpFinLevel5",
                      new string[] { "flag", "Level5_Name", "Level4_ID", "Level3_ID", "Level2_ID", "Level1_ID" },
                      new string[] { "4", txtLevel5_Name.Text.Trim(), ddlLevel4_ID.SelectedValue, ddlLevel3_ID.SelectedValue, ddlLevel2_ID.SelectedValue, ddlLevel1_ID.SelectedValue }, "dataset");

                if (btnSave.Text == "Accept" && ds.Tables[0].Rows.Count == 0)
                {
                    objdb.ByProcedure("SpFinLevel5",
                      new string[] { "flag", "Level5_IsActive", "Level5_Name", "Level5_IsAppOnHO", "Level4_ID", "Level3_ID", "Level2_ID", "Level1_ID", "Level5_UpdatedBy" },
                      new string[] { "0", Level5_IsActive, txtLevel5_Name.Text.Trim(), ddlLevel5_IsAppOnHO.SelectedItem.ToString(), ddlLevel4_ID.SelectedValue, ddlLevel3_ID.SelectedValue, ddlLevel2_ID.SelectedValue, ddlLevel1_ID.SelectedValue, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else
                {
                    string Level5_Name = ds.Tables[0].Rows[0]["Level5_Name"].ToString();
                    if (Level5_Name == txtLevel5_Name.Text)
                    {
                        //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", "This Level3 Name Is Already Exist.");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('This Level5 Name Is Already Exist');", true);
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


    //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string Level5_ID = GridView1.SelectedValue.ToString();
    //        DataSet dsrecord = objdb.ByProcedure("SpFinLevel5",
    //                new string[] { "flag", "Level5_ID" },
    //                new string[] { "3", Level5_ID }, "dataset");
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    //protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        string Level5_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
    //        string Level5_IsActive = "0";
    //        objdb.ByProcedure("SpFinLevel5",
    //                new string[] { "flag", "Level5_ID", "Level5_IsActive", "Level5_UpdatedBy" },
    //                new string[] { "6", Level5_ID, Level5_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");
    //        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
    //        FillGrid();


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void ClearField()
    {
        txtLevel5_Name.Text = "";
        ddlLevel1_ID.ClearSelection();
        ddlLevel2_ID.ClearSelection();
        ddlLevel3_ID.ClearSelection();
        ddlLevel4_ID.ClearSelection();
      
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string Level5_ID = GridView1.SelectedValue.ToString();
            DataSet dsrecord = objdb.ByProcedure("SpFinLevel5",
                    new string[] { "flag", "Level5_ID" },
                    new string[] { "3", Level5_ID }, "dataset");
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
            string Level5_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string Level5_IsActive = "0";
            objdb.ByProcedure("SpFinLevel5",
                    new string[] { "flag", "Level5_ID", "Level5_IsActive", "Level5_UpdatedBy" },
                    new string[] { "6", Level5_ID, Level5_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlLevel4_ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        ds = objdb.ByProcedure("SpFinLevel5",
               new string[] { "flag", "Level1_ID", "Level2_ID", "Level3_ID", "Level4_ID" },
               new string[] { "13", ddlLevel1_ID.SelectedValue, ddlLevel2_ID.SelectedValue, ddlLevel3_ID.SelectedValue, ddlLevel4_ID.SelectedValue }, "dataset");

        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
}