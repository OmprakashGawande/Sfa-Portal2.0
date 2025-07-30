using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HRGradePay : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ds = objdb.ByProcedure("SpHRPayScale", new string[] { "flag" }, new string[] { "1" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlPayScale.DataSource = ds;
                    ddlPayScale.DataTextField = "PayScale_Name";
                    ddlPayScale.DataValueField = "PayScale_ID";
                    ddlPayScale.DataBind();
                    ddlPayScale.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlPayScale.Items.Insert(0, new ListItem("Select", "0"));
                }

                ViewState["GradePay_ID"] = "0";
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
            if (txtGradePay_Name.Text == "")
            {
                msg += "Enter Grade Pay. \\n";
            }
            if (ddlPayScale.SelectedIndex == 0)
            {
                msg += "Select Pay Scale. \\n";
            }
            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpHRGradePay", new string[] { "flag", "GradePay_ID", "GradePay_Name", "PayScale_ID", }, new string[] { "5", ViewState["GradePay_ID"].ToString(), txtGradePay_Name.Text, ddlPayScale.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (btnSave.Text == "Save" && ViewState["GradePay_ID"].ToString() == "0" && Status == 0)
                {
                    objdb.ByProcedure("SpHRGradePay",
                    new string[] { "flag", "GradePay_Name", "PayScale_ID", "GradePay_UpdatedBy" },
                    new string[] { "0", txtGradePay_Name.Text, ddlPayScale.SelectedValue.ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();

                }
                else if (btnSave.Text == "Edit" && ViewState["GradePay_ID"].ToString() != "0" && Status == 0)
                {
                    objdb.ByProcedure("SpHRGradePay",
                    new string[] { "flag", "GradePay_ID", "GradePay_Name", "PayScale_ID", "GradePay_UpdatedBy" },
                    new string[] { "2", ViewState["GradePay_ID"].ToString(), txtGradePay_Name.Text, ddlPayScale.SelectedValue.ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Grade Pay already exist.');", true);
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
            ds = objdb.ByProcedure("SpHRGradePay", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ClearText();
            ViewState["GradePay_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpHRGradePay", new string[] { "flag", "GradePay_ID" }, new string[] { "4", ViewState["GradePay_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPayScale.Items.FindByValue(ds.Tables[0].Rows[0]["PayScale_ID"].ToString()).Selected = true;
                txtGradePay_Name.Text = ds.Tables[0].Rows[0]["GradePay_Name"].ToString();
                btnSave.Text = "Edit";
            }
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
            ClearText();
            string GradePay_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpHRGradePay",
                   new string[] { "flag", "GradePay_ID", "GradePay_UpdatedBy" },
                   new string[] { "3", GradePay_ID, ViewState["Emp_ID"].ToString() }, "dataset");

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
    protected void ClearText()
    {
        ddlPayScale.ClearSelection();
        txtGradePay_Name.Text = "";
        ViewState["GradePay_ID"] = "0";
        btnSave.Text = "Save";
    }
}