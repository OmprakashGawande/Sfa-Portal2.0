using System;
using System.Data;
using System.Globalization;

public partial class mis_Admin_AdminItemCategory : System.Web.UI.Page
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
                ViewState["ItemCat_id"] = "0";
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
            if (txtItem_Category.Text == "")
            {
                msg += "Enter Item Group. \\n";
            }
            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpItemCategory", new string[] { "flag", "ItemCatName", "ItemCat_id" }, new string[] { "5", txtItem_Category.Text, ViewState["ItemCat_id"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (btnSave.Text == "Save" && ViewState["ItemCat_id"].ToString() == "0" && Status == 0)
                {
                    objdb.ByProcedure("SpItemCategory",
                    new string[] { "flag", "ItemCatName", "CreatedBy" },
                    new string[] { "0", txtItem_Category.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();

                }
                else if (btnSave.Text == "Edit" && ViewState["ItemCat_id"].ToString() != "0" && Status == 0)
                {
                    objdb.ByProcedure("SpItemCategory",
                    new string[] { "flag", "ItemCat_id", "ItemCatName", "CreatedBy" },
                    new string[] { "2", ViewState["ItemCat_id"].ToString(), txtItem_Category.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Item Group is already exist.');", true);
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
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpItemCategory", new string[] { "flag" }, new string[] { "1" }, "dataset");
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
            ViewState["ItemCat_id"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpItemCategory", new string[] { "flag", "ItemCat_id" }, new string[] { "4", ViewState["ItemCat_id"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtItem_Category.Text = ds.Tables[0].Rows[0]["ItemCatName"].ToString();
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
            string ItemCat_id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpItemCategory",
                   new string[] { "flag", "ItemCat_id" },
                   new string[] { "3", ItemCat_id }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        txtItem_Category.Text = "";
        ViewState["ItemCat_id"] = "0";
        btnSave.Text = "Save";
    }
    protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridView1.PageIndex = e.NewPageIndex;
            ds = objdb.ByProcedure("SpItemCategory", new string[] { "flag" }, new string[] { "1" }, "dataset");
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