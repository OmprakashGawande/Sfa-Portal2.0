using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Finance_FinSupplierItemCreation : System.Web.UI.Page
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
                ViewState["ItemID"] = "0";
                FillDropdown();
                FillCategory();
                FillGrid();
                txtEffectiveDate.Text = "01/11/2020";
                ddlCessApplicable_SelectedIndexChanged(sender, e);
                if (txtItemRate.Text == "")
                {
                    txtItemRate.Text = "0";
                }
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
            ds = objdb.ByProcedure("SpItemType",
                                    new string[] { "flag", "ItemCat_id" },
                                    new string[] { "6", "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlItemGroup.DataSource = ds;
                ddlItemGroup.DataTextField = "ItemTypeName";
                ddlItemGroup.DataValueField = "ItemType_id";
                ddlItemGroup.DataBind();
                ddlItemGroup.Items.Insert(0, new ListItem("Select", "0"));
            }
            ds = null;
            ds = objdb.ByProcedure("SpUnit",
                        new string[] { "flag" },
                        new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlItemUnit.DataSource = ds;
                ddlItemUnit.DataTextField = "UnitName";
                ddlItemUnit.DataValueField = "Unit_Id";
                ddlItemUnit.DataBind();
                ddlItemUnit.Items.Insert(0, "Select");
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
            string msg = "";
            //if (ddlItemCategory.SelectedIndex == 0)
            //{
            //    msg = msg + "Select Item Category. \\n";
            //}
            if (txtItemName.Text == "")
            {
                msg = msg + "Enter Item Name. \\n";
            }
            if (ddlItemGroup.SelectedIndex <= 0)
            {
                msg = msg + "Select State. \\n";
            }
            if (ddlItemUnit.SelectedIndex <= 0)
            {
                msg = msg + "Select Item Unit. \\n";
            }
            if (txtItemRate.Text == "")
            {
                msg = msg + "Enter Item Rate. \\n";
            }
            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpFinSupplierItem", new string[] { "flag", "ItemName", "ItemID" }, new string[] { "5", txtItemName.Text, ViewState["ItemID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }

                if (btnSave.Text == "Save" && ViewState["ItemID"].ToString() == "0" && Status == 0)
                {
                    string CessRate = "0";
                    if (txtCessRate.Text != "")
                    {
                        CessRate = txtCessRate.Text;
                    }
                    ds = objdb.ByProcedure("SpFinSupplierItem",
                    new string[] { "flag", "ItemCategory", "ItemName", "ItemGroupID", "ItemGroup", "ItemUnitID", "ItemUnit", "ItemRate", "CessApplicable", "CessRate", "GSTApplicable", "GSTRate", "GSTIncInDeduction", "EffectiveDate", "UpdatedBy" },
                    new string[] { "0", ddlItemCategory.SelectedValue.ToString(), txtItemName.Text, ddlItemGroup.SelectedValue.ToString(), ddlItemGroup.SelectedItem.ToString(), ddlItemUnit.SelectedValue.ToString(), ddlItemUnit.SelectedItem.ToString(), txtItemRate.Text, ddlCessApplicable.SelectedItem.Text, CessRate, ddlGSTApplicable.SelectedItem.Text, ddlGSTRate.SelectedValue.ToString(), ddlGSTIncInDeduction.SelectedItem.Text, Convert.ToDateTime(txtEffectiveDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Item Successfully Saved.");
                    ClearText();
                    FillGrid();
                }
                else if (btnSave.Text == "Update" && ViewState["ItemID"].ToString() != "0" && Status == 0)
                {
                    objdb.ByProcedure("SpFinSupplierItem",
                     new string[] { "flag", "ItemCategory", "ItemID", "ItemName", "ItemGroupID", "ItemGroup", "ItemUnitID", "ItemUnit", "ItemRate", "CessApplicable", "CessRate", "GSTApplicable", "GSTRate", "GSTIncInDeduction", "EffectiveDate", "UpdatedBy" },
                     new string[] { "3", ddlItemCategory.SelectedValue.ToString(), ViewState["ItemID"].ToString(), txtItemName.Text, ddlItemGroup.SelectedValue.ToString(), ddlItemGroup.SelectedItem.ToString(), ddlItemUnit.SelectedValue.ToString(), ddlItemUnit.SelectedItem.ToString(), txtItemRate.Text, ddlCessApplicable.SelectedItem.Text, txtCessRate.Text, ddlGSTApplicable.SelectedItem.Text, ddlGSTRate.SelectedValue.ToString(),ddlGSTIncInDeduction.SelectedItem.Text, Convert.ToDateTime(txtEffectiveDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Item Successfully Updated");
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Name Of Item already exist.');", true);
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
    protected void FillCategory()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinSupplierOrderEntry",
                new string[] { "flag" },
                new string[] { "8" }, "dataset");
            if (ds.Tables.Count > 0)
            {
                ddlItemCategory.DataTextField = "CategoryName";
                ddlItemCategory.DataValueField = "CategoryId";
                ddlItemCategory.DataSource = ds;
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
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

            ds = objdb.ByProcedure("SpFinSupplierItem",
                new string[] { "flag" },
                new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0)
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
            ViewState["ItemID"] = GridView1.SelectedValue.ToString();
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpFinSupplierItem",
                       new string[] { "flag", "ItemID" },
                       new string[] { "4", ViewState["ItemID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ItemCategory"].ToString() != null && ds.Tables[0].Rows[0]["ItemCategory"].ToString() != "")
                {
                    ddlItemCategory.ClearSelection();
                    ddlItemCategory.Items.FindByValue(ds.Tables[0].Rows[0]["ItemCategory"].ToString()).Selected = true;
                }

                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();

                ddlItemGroup.ClearSelection();
                ddlItemGroup.Items.FindByValue(ds.Tables[0].Rows[0]["ItemGroupID"].ToString()).Selected = true;

                ddlItemUnit.ClearSelection();
                ddlItemUnit.Items.FindByValue(ds.Tables[0].Rows[0]["ItemUnitID"].ToString()).Selected = true;

                txtItemRate.Text = ds.Tables[0].Rows[0]["ItemRate"].ToString();

                ddlCessApplicable.ClearSelection();
                ddlCessApplicable.Items.FindByValue(ds.Tables[0].Rows[0]["CessApplicable"].ToString()).Selected = true;
                txtCessRate.Text = ds.Tables[0].Rows[0]["CessRate"].ToString();
                ddlGSTApplicable.ClearSelection();
                ddlGSTApplicable.Items.FindByValue(ds.Tables[0].Rows[0]["GSTApplicable"].ToString()).Selected = true;
                ddlGSTRate.ClearSelection();
                ddlGSTRate.Items.FindByValue(ds.Tables[0].Rows[0]["GSTRate"].ToString()).Selected = true;
                ddlGSTIncInDeduction.ClearSelection();
                ddlGSTIncInDeduction.Items.FindByValue(ds.Tables[0].Rows[0]["GSTIncInDeduction"].ToString()).Selected = true;
                txtEffectiveDate.Text = ds.Tables[0].Rows[0]["EffectiveDate"].ToString();

                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void ClearText()
    {
        txtItemName.Text = "";
        txtItemRate.Text = "";
        ddlItemGroup.ClearSelection();
        ddlItemUnit.ClearSelection();
        ddlItemCategory.ClearSelection();
        ViewState["ItemID"] = "0";
        btnSave.Text = "Save";
    }
    protected void ddlGSTApplicable_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            divGSTRate.Visible = true;
            if (ddlGSTApplicable.SelectedIndex > 0)
            {
                ddlGSTRate.ClearSelection();
                divGSTRate.Visible = false;
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateBasicRate();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlCessApplicable_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            txtCessRate.Enabled = true;
            txtCessRate.Text = "400.00";
            if (ddlCessApplicable.SelectedIndex > 0)
            {
                txtCessRate.Text = "0.00";
                txtCessRate.Enabled = false;
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateBasicRate();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}