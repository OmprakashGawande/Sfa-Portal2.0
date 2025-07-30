using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;


public partial class mis_HR_HRLevelPayMaster : System.Web.UI.Page
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
                    ViewState["PayScale_ID"] = "0";
                    FillLevel();
                    FillGrid();
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
    protected void FillLevel()
    {
        try
        {
            ds = objdb.ByProcedure("SpHRLevelMaster", new string[] { "flag" }, new string[] { "6" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlLevelName.DataTextField = "Level_Name";
                ddlLevelName.DataValueField = "Level_ID";
                ddlLevelName.DataSource = ds;
                ddlLevelName.DataBind();
                ddlLevelName.Items.Insert(0, new ListItem("Select", "0"));
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
            ds = objdb.ByProcedure("SpHRLevelPayMaster", new string[] { "flag" }, new string[] { "6" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        try
        {
            ddlLevelName.ClearSelection();
            txtPayScale.Text = "";
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
            string PayScale_IsActive = "1";
            if (ddlLevelName.SelectedIndex == 0)
            {
                msg += "Select Level. \\n";
            }
            if (txtPayScale.Text == "")
            {
                msg += "Enter Pay Scale. \\n";
            }

            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpHRLevelPayMaster", new string[] { "flag", "Level_ID", "PayScale", "PayScale_ID" }, new string[] { "5", ddlLevelName.SelectedValue.ToString(), txtPayScale.Text, ViewState["PayScale_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (btnSave.Text == "Save" && ViewState["PayScale_ID"].ToString() == "0" && Status == 0)
                {
                    objdb.ByProcedure("SpHRLevelPayMaster",
                    new string[] { "flag", "PayScale_IsActive", "Level_ID", "PayScale", "PayScale_UpdatedBy" },
                    new string[] { "0", PayScale_IsActive, ddlLevelName.SelectedValue.ToString(), txtPayScale.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                    ViewState["PayScale_ID"] = "0";
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();

                }
                else if (btnSave.Text == "Edit" && ViewState["PayScale_ID"].ToString() != "0" && Status == 0)
                {
                    objdb.ByProcedure("SpHRLevelPayMaster",
                    new string[] { "flag", "PayScale_ID", "Level_ID", "PayScale", "PayScale_UpdatedBy" },
                    new string[] { "3", ViewState["PayScale_ID"].ToString(), ddlLevelName.SelectedValue.ToString(), txtPayScale.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    ViewState["PayScale_ID"] = "0";
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    btnSave.Text = "Save";
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('PayScale is already exist in this Level.');", true);
                    // ClearText();
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
            ViewState["PayScale_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpHRLevelPayMaster",
                        new string[] { "flag", "PayScale_ID" },
                        new string[] { "1", ViewState["PayScale_ID"].ToString() }, "dataset");
            ddlLevelName.ClearSelection();
            ddlLevelName.Items.FindByValue(ds.Tables[0].Rows[0]["Level_ID"].ToString()).Selected = true;
            txtPayScale.Text = ds.Tables[0].Rows[0]["PayScale"].ToString();
            btnSave.Text = "Edit";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string PayScale_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string PayScale_IsActive = "0";
            ds = objdb.ByProcedure("SpHRLevelPayMaster",
                        new string[] { "flag", "PayScale_ID", "PayScale_IsActive", "PayScale_UpdatedBy" },
                        new string[] { "4", PayScale_ID, PayScale_IsActive, ViewState["PayScale_ID"].ToString() }, "dataset");
            ViewState["PayScale_ID"] = "0";
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlLevelName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
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
            GridView1.PageIndex = e.NewPageIndex;
            ds = objdb.ByProcedure("SpHRLevelPayMaster", new string[] { "flag" }, new string[] { "6" }, "dataset");
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