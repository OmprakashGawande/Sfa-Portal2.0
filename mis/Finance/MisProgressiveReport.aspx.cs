using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class mis_Finance_MisProgressiveReport : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillDropdown();
                    btnSave.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }

        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    protected void FillDropdown()
    {
        try
        {
            lblMsg.Text = "";
            DataSet dsOffice = objdb.ByProcedure("SpFinMisProgressiveNew", new string[] {"flag" }, new string[] {"8" }, "dataset");
            if(dsOffice != null)
            {
                ddlOffice.DataSource = dsOffice;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                if (ViewState["Office_ID"].ToString() !="1")
                {
                   ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                   
                }else{
                    ddlOffice.Enabled = true;
                }
                
            }
            DataSet dsYear = objdb.ByProcedure("SpFinMisProgressiveNew", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (dsYear != null)
            {
                ddlYear.DataSource = dsYear;
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            btnSave.Visible = false;
            if(ddlOffice.SelectedIndex == 0)
            {
                msg += "Select Office";
            }
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year";
            }
            if (ddlMonth.SelectedIndex == 0)
            {
                msg += "Select Month";
            }
            if(msg == "")
            {
                ds = objdb.ByProcedure("SpFinMisProgressiveNew", new string[] { "flag", "Office_ID", "Year", "Month_ID" }, new string[] {"2",ddlOffice.SelectedValue,ddlYear.SelectedValue,ddlMonth.SelectedValue }, "dataset");
                if(ds!= null)
                if(ds.Tables[0].Rows[0]["Status"].ToString() == "true")
                {
                    btnSave.Visible = true;
                    GridView1.DataSource = ds.Tables[1];
                    GridView1.DataBind();
                    btnSave.Text = "Update";
                    GridView1.Visible = true;
                }
                else
                {
                    btnSave.Visible = true;
                    GridView1.DataSource = ds.Tables[1];
                    GridView1.DataBind();
                    btnSave.Text = "Save";
                    GridView1.Visible = true;
                }
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
  
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string IsActive = "1";
            if(btnSave.Text == "Save")
            {
                foreach(GridViewRow rows in GridView1.Rows)
                {
                    Label Mis_ID = (Label)rows.FindControl("lblMisID");
                    Label Particular_ID = (Label)rows.FindControl("lblParticularID");
                    Label Particular_Name = (Label)rows.FindControl("lblParticularName");
                    Label Type = (Label)rows.FindControl("lblType");
                    TextBox Amount = (TextBox)rows.FindControl("txtCYear");
                    objdb.ByProcedure("SpFinMisProgressiveNew", new string[] { "flag", "Office_ID", "Year", "Month_ID", "Month_Name", "Particular_ID", "Particular_Name", "Type", "Amount", "UpdatedBy", "IsActive" },
                                                             new string[] { "3", ddlOffice.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlMonth.SelectedItem.Text, Particular_ID.Text, Particular_Name.Text, Type.Text, Amount.Text, ViewState["Emp_ID"].ToString(), IsActive }, "dataset");
                    
                }
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                btnSave.Visible = false;
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
                GridView1.Visible = false;
            }
            else if(btnSave.Text == "Update")
            {
                foreach (GridViewRow rows in GridView1.Rows)
                {
                    Label Mis_ID = (Label)rows.FindControl("lblMisID");
                    Label Particular_ID = (Label)rows.FindControl("lblParticularID");
                    Label Particular_Name = (Label)rows.FindControl("lblParticularName");
                    Label Type = (Label)rows.FindControl("lblType");
                    TextBox Amount = (TextBox)rows.FindControl("txtCYear");
                    objdb.ByProcedure("SpFinMisProgressiveNew", new string[] { "flag", "Mis_ID", "Office_ID", "Year", "Month_ID", "Month_Name", "Particular_ID", "Particular_Name", "Type", "Amount", "UpdatedBy", "IsActive" },
                                                             new string[] { "4", Mis_ID.Text, ddlOffice.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlMonth.SelectedItem.Text, Particular_ID.Text, Particular_Name.Text, Type.Text, Amount.Text, ViewState["Emp_ID"].ToString(), IsActive }, "dataset");
                    
                }
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                btnSave.Visible = false;
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
                GridView1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}