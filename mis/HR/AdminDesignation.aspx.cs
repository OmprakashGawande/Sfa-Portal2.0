using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_AdminDesignation : System.Web.UI.Page
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
                ViewState["Designation_ID"] = "0";

                ds = objdb.ByProcedure("SpHRClass", new string[] { "flag" }, new string[] { "1" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    ddlClass.DataSource = ds;
                    ddlClass.DataTextField = "Class_Name";
                    ddlClass.DataValueField = "Class_Name";
                    ddlClass.DataBind();
                    ddlClass.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlClass.Items.Insert(0, new ListItem("Select", "0"));
                }
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
            if (txtDesignation_Name.Text == "")
            {
                msg += "Enter Designation Name. \\n";
            }
            if(txtOrderNo.Text =="")
            {
                msg += "Enter Designation Order. \\n";
            }
            if(ddlClass.SelectedIndex ==0)
            {
                msg += "Select Class. \\n";
            }
            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpAdminDesignation", new string[] { "flag", "Designation_Name", "Class", "Designation_ID" }, new string[] { "5", txtDesignation_Name.Text,ddlClass.SelectedValue.ToString(), ViewState["Designation_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (btnSave.Text == "Save" && ViewState["Designation_ID"].ToString() == "0" && Status == 0)
                {
                    objdb.ByProcedure("SpAdminDesignation",
                    new string[] { "flag", "Designation_Name", "Class", "OrderNo", "Designation_UpdatedBy" },
                    new string[] { "0", txtDesignation_Name.Text,ddlClass.SelectedValue.ToString(),txtOrderNo.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();

                }
                else if (btnSave.Text == "Edit" && ViewState["Designation_ID"].ToString() != "0" && Status == 0)
                {
                    objdb.ByProcedure("SpAdminDesignation",
                    new string[] { "flag", "Designation_ID", "Designation_Name", "Class", "OrderNo", "Designation_UpdatedBy" },
                    new string[] { "2", ViewState["Designation_ID"].ToString(), txtDesignation_Name.Text,ddlClass.SelectedValue.ToString(),txtOrderNo.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Designation name is already exist.');", true);
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
            ds = objdb.ByProcedure("SpAdminDesignation", new string[] { "flag" }, new string[] { "1" }, "dataset");
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ClearText();
            ViewState["Designation_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpAdminDesignation", new string[] { "flag", "Designation_ID" }, new string[] { "4", ViewState["Designation_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDesignation_Name.Text = ds.Tables[0].Rows[0]["Designation_Name"].ToString();
                txtOrderNo.Text = ds.Tables[0].Rows[0]["OrderNo"].ToString();
                ddlClass.Items.FindByValue(ds.Tables[0].Rows[0]["Class"].ToString()).Selected = true;
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
            string Designation_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string Designation_IsActive = "0";
            objdb.ByProcedure("SpAdminDesignation",
                   new string[] { "flag", "Designation_ID", "Designation_IsActive", "Designation_UpdatedBy" },
                   new string[] { "3", Designation_ID,Designation_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();
            lblMsg.Text = "";
            ClearText();
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
        txtDesignation_Name.Text = "";
        txtOrderNo.Text = "";
        ddlClass.ClearSelection();
        ViewState["Designation_ID"] = "0";
        btnSave.Text = "Save";
    }
}