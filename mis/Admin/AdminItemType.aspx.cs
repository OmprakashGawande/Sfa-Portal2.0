using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;

public partial class mis_Admin_AdminItemType : System.Web.UI.Page
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
                ds = objdb.ByProcedure("SpItemCategory",
                        new string[] { "flag" },
                        new string[] { "6" }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {
                    ddlItemCategory.DataTextField = "ItemCatName";
                    ddlItemCategory.DataValueField = "ItemCat_id";
                    ddlItemCategory.DataSource = ds;
                    ddlItemCategory.DataBind();
                    ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
                    ddlItemCategory.SelectedValue = "1";

                }
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["ItemType_id"] = "0";
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
            if (ddlItemCategory.SelectedIndex <= 0)
            {
                msg = "Please select Nature";
            }
            if (txtItem_Type.Text == "")
            {
                msg += "Enter Item Group. \\n";
            }

            if (msg.Trim() == "")
            {
                int Status = 0;
                txtItem_Type.Text = FirstLetterToUpper(txtItem_Type.Text);
                ds = objdb.ByProcedure("SpItemType", new string[] { "flag", "ItemTypeName", "ItemType_id", "ItemCat_id" }, new string[] { "5", txtItem_Type.Text, ViewState["ItemType_id"].ToString(),ddlItemCategory.SelectedValue }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (btnSave.Text == "Save" && ViewState["ItemType_id"].ToString() == "0" && Status == 0)
                {
                    objdb.ByProcedure("SpItemType",
                    new string[] { "flag", "ItemCat_id", "ItemTypeName", "CreatedBy" },
                    new string[] { "0", ddlItemCategory.SelectedValue.ToString(), txtItem_Type.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();

                }
                else if (btnSave.Text == "Edit" && ViewState["ItemType_id"].ToString() != "0" && Status == 0)
                {
                    objdb.ByProcedure("SpItemType",
                    new string[] { "flag", "ItemType_id", "ItemCat_id", "ItemTypeName", "CreatedBy" },
                    new string[] { "2", ViewState["ItemType_id"].ToString(), ddlItemCategory.SelectedValue.ToString(), txtItem_Type.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Item Group is already exist.');", true);
                    FillGrid();
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
            if (ddlItemCategory.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpItemType", new string[] { "flag", "ItemCat_id" }, new string[] { "6", ddlItemCategory.SelectedValue.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;

                }
                else
                {
                    GridView1.DataSource = new string[] { };

                }
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
                foreach(GridViewRow rows in GridView1.Rows)
                {
                    LinkButton lnkdelete = (LinkButton)rows.FindControl("Delete");
                    Label lblid = (Label)rows.FindControl("lblid");
                    ds = objdb.ByProcedure("SpItemType", new string[] { "flag", "ItemType_id" }, new string[] { "7", lblid.Text }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if(ds.Tables[0].Rows[0]["status"].ToString() == "true")
                        {
                            lnkdelete.Visible = false;
                        }
                        else
                        {
                            lnkdelete.Visible = true;
                        }

                    }
                   
                }
            }
            else
            {
                ds = objdb.ByProcedure("SpItemType", new string[] { "flag" }, new string[] { "1" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;

                }
                else
                {
                    GridView1.DataSource = new string[] { }; 
                }
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;
            lblMsg.Text = "";
            ClearText();
            ViewState["ItemType_id"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpItemType", new string[] { "flag", "ItemType_id" }, new string[] { "4", ViewState["ItemType_id"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlItemCategory.ClearSelection();
                ddlItemCategory.Items.FindByValue(ds.Tables[0].Rows[0]["ItemCat_id"].ToString()).Selected = true;
                txtItem_Type.Text = ds.Tables[0].Rows[0]["ItemTypeName"].ToString();
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
            string ItemType_id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpItemType",
                   new string[] { "flag", "ItemType_id" },
                   new string[] { "3", ItemType_id }, "dataset");

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
        //ddlItemCategory.ClearSelection();
        txtItem_Type.Text = "";
        ViewState["ItemType_id"] = "0";
        btnSave.Text = "Save";
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridView1.PageIndex = e.NewPageIndex;
            ds = objdb.ByProcedure("SpItemType", new string[] { "flag" }, new string[] { "1" }, "dataset");
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
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public string FirstLetterToUpper(string str)
    {
        string txt = cult.TextInfo.ToTitleCase(str.ToLower());
        return txt;
    }

}