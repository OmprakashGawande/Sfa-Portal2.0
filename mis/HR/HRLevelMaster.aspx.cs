using System;
using System.Data;
using System.Globalization;


public partial class mis_HR_HRLevelMaster : System.Web.UI.Page
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
                    ViewState["Level_ID"] = "0";
                    FillGrid();
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
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHRLevelMaster", new string[] { "flag" }, new string[] { "2" }, "dataset");
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
    protected void ClearText()
    {
        try
        {
            txtLevel_Name.Text = "";
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
            string Level_IsActive = "1";
            if (txtLevel_Name.Text == "")
            {
                msg += "Enter Level Name. \n";
            }

            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpHRLevelMaster", new string[] { "flag", "Level_Name", "Level_ID" }, new string[] { "5", txtLevel_Name.Text, ViewState["Level_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (btnSave.Text == "Save" && ViewState["Level_ID"].ToString() == "0" && Status == 0)
                {
                    objdb.ByProcedure("SpHRLevelMaster",
                    new string[] { "flag", "Level_Name", "Level_IsActive", "Level_UpdatedBy" },
                    new string[] { "0", txtLevel_Name.Text, Level_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();

                }
                else if (btnSave.Text == "Edit" && ViewState["Level_ID"].ToString() != "0" && Status == 0)
                {
                    objdb.ByProcedure("SpHRLevelMaster",
                    new string[] { "flag", "Level_ID", "Level_Name", "Level_UpdatedBy" },
                    new string[] { "3", ViewState["Level_ID"].ToString(), txtLevel_Name.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    btnSave.Text = "Save";
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Level name already exist.');", true);
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
            ViewState["Level_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpHRLevelMaster",
                        new string[] { "flag", "Level_ID" },
                        new string[] { "1", ViewState["Level_ID"].ToString() }, "dataset");
            txtLevel_Name.Text = ds.Tables[0].Rows[0]["Level_Name"].ToString();
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
            string Level_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string Level_IsActive = "0";
            ds = objdb.ByProcedure("SpHRLevelMaster",
                        new string[] { "flag", "Level_ID", "Level_IsActive", "Level_UpdatedBy" },
                        new string[] { "4", Level_ID, Level_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }
}