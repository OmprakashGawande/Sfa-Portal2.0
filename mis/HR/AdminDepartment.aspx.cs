using System;
using System.Data;
using System.Globalization;

public partial class mis_HR_AdminDepartment : System.Web.UI.Page
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
                ViewState["Department_ID"] = "0";
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpAdminDepartment", new string[] { "flag" }, new string[] { "1" }, "dataset");
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
        txtDepartment_Name.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (txtDepartment_Name.Text == "")
            {
                msg += "Enter Department Name. \\n";
            }
            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpAdminDepartment", new string[] { "flag", "Department_Name", "Department_ID" }, new string[] { "5", txtDepartment_Name.Text, ViewState["Department_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (btnSave.Text == "Save" && ViewState["Department_ID"].ToString() == "0" && Status == 0)
                {
                    objdb.ByProcedure("SpAdminDepartment",
                    new string[] { "flag", "Department_Name", "Department_UpdatedBy" },
                    new string[] { "0", txtDepartment_Name.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();

                }
                else if (btnSave.Text == "Edit" && ViewState["Department_ID"].ToString() != "0" && Status == 0)
                {
                    objdb.ByProcedure("SpAdminDepartment",
                    new string[] { "flag", "Department_ID", "Department_Name", "Department_UpdatedBy" },
                    new string[] { "2", ViewState["Department_ID"].ToString(), txtDepartment_Name.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    btnSave.Text = "Save";
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Department name is already exist.');", true);
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
            ViewState["Department_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpAdminDepartment", new string[] { "flag", "Department_ID" }, new string[] { "4", ViewState["Department_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDepartment_Name.Text = ds.Tables[0].Rows[0]["Department_Name"].ToString();
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
            string Department_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string Department_IsActive = "0";
            objdb.ByProcedure("SpAdminDepartment",
                   new string[] { "flag", "Department_ID", "Department_IsActive", "Department_UpdatedBy" },
                   new string[] { "3", Department_ID, Department_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");

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
}