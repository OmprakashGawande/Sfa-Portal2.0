using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Finance_FinItemSubsetMaster : System.Web.UI.Page
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
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["SubSet_ID"] = "0";
                ds = objdb.ByProcedure("SpFinItemIngredientTx", new string[] { "flag" }, new string[] { "6" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    ddlItem_Id.DataSource = ds;
                    ddlItem_Id.DataTextField = "ItemName";
                    ddlItem_Id.DataValueField = "Item_id";
                    ddlItem_Id.DataBind();
                    ddlItem_Id.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlItem_Id.Items.Insert(0, new ListItem("Select", "0"));
                }
                //FillGrid();
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
            if (txtSubsetName.Text == "")
            {
                msg += "Enter subset Name. \\n";
            }
            if (txtSubsetSize.Text == "")
            {
                msg += "Enter Subset Size. \\n";
            }
            if (txtSubsetUnit.Text == "")
            {
                msg += "Enter Subset Unit. \\n";
            }
            if (ddlItem_Id.SelectedIndex == 0)
            {
                msg += "Select Main Set. \\n";
            }
            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpFinItemIngredientTx", new string[] { "flag", "Ingredient_Name", "Item_Id", "Ingredient_Unit", "Ingredient_Size", "Ingredient_Id" }, new string[] { "10", txtSubsetName.Text, ddlItem_Id.SelectedValue.ToString(), txtSubsetUnit.Text, txtSubsetSize.Text, ViewState["SubSet_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows.Count);
                }
                if (btnSave.Text == "Save" && ViewState["SubSet_ID"].ToString() == "0" && Status == 0)
                {
                    objdb.ByProcedure("SpFinItemIngredientTx",
                    new string[] { "flag", "Ingredient_Name", "Item_Id", "Ingredient_Unit", "Ingredient_Size", "UpdatedBy","Office_ID" },
                    new string[] { "4", txtSubsetName.Text, ddlItem_Id.SelectedValue.ToString(), txtSubsetUnit.Text, txtSubsetSize.Text, ViewState["Emp_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();

                }
                else if (btnSave.Text == "Edit" && ViewState["SubSet_ID"].ToString() != "0" && Status == 0)
                {
                    objdb.ByProcedure("SpFinItemIngredientTx",
                    new string[] { "flag", "Ingredient_Id", "Ingredient_Name", "Item_Id", "Ingredient_Unit", "Ingredient_Size", "UpdatedBy" },
                    new string[] { "9", ViewState["SubSet_ID"].ToString(), txtSubsetName.Text, ddlItem_Id.SelectedValue.ToString(), txtSubsetUnit.Text, txtSubsetSize.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Subset name is already exist.');", true);
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
            ds = objdb.ByProcedure("SpFinItemIngredientTx", new string[] { "flag", "Item_id" }, new string[] { "5", ddlItem_Id.SelectedValue.ToString() }, "dataset");
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
            ViewState["SubSet_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpFinItemIngredientTx", new string[] { "flag", "Ingredient_Id" }, new string[] { "7", ViewState["SubSet_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtSubsetName.Text = ds.Tables[0].Rows[0]["Ingredient_Name"].ToString();
                txtSubsetUnit.Text = ds.Tables[0].Rows[0]["Ingredient_Unit"].ToString();
                txtSubsetSize.Text = ds.Tables[0].Rows[0]["Ingredient_Size"].ToString();
                ddlItem_Id.Items.FindByValue(ds.Tables[0].Rows[0]["Item_Id"].ToString()).Selected = true;
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
            string Ingredient_Id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpFinItemIngredientTx",
                   new string[] { "flag", "Ingredient_Id", "UpdatedBy" },
                   new string[] { "8", Ingredient_Id, ViewState["Emp_ID"].ToString() }, "dataset");

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

    protected void ClearText()
    {
        txtSubsetName.Text = "";
        txtSubsetSize.Text = "";
        txtSubsetUnit.Text = "";
        //ddlItem_Id.ClearSelection();
        ViewState["SubSet_ID"] = "0";
        btnSave.Text = "Save";
    }
    protected void ddlItem_Id_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
}