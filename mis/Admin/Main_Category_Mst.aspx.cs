using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Master_Main_Category_Mst : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"].ToString() != null && Session["Emp_ID"].ToString() != "" && Session["Office_ID"].ToString() != null && Session["Office_ID"].ToString() != "")
            {
                if (!IsPostBack)
                {
                    lblmsg.Text = "";
                    Session["Emp_ID"] = Session["Emp_ID"].ToString();
                    Session["Office_ID"] = Session["Office_ID"].ToString();
                    FillCategoryGrid();

                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void FillCategoryGrid()
    {
        try
        {

            GVItemCategory.DataSource = string.Empty;
            GVItemCategory.DataBind();

            ds = objdb.ByProcedure("USP_ItemCategory_Grid", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVItemCategory.DataSource = ds.Tables[0];
                    GVItemCategory.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void chkactive_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            CheckBox cbx = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cbx.NamingContainer;
            string ItemCat_id = GVItemCategory.DataKeys[row.RowIndex].Value.ToString();
            string Activecbx = "";
            if (cbx.Checked == true)
            {
                Activecbx = "1";
            }
            else
            {
                Activecbx = "0";
            }
            ds = objdb.ByProcedure("USP_ItemCategory_Isactive", new string[] { "ItemCat_id", "ItemCat_IsActive", "LastIsActiveBy", "LastIsActiveByIP" }, new string[] { ItemCat_id, Activecbx, Session["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        lblmsg.Text = objdb.Alert("fa-check", "alert-success", "Alert !", ErrMsg);
                    }

                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "NotOk")
                    {
                        lblmsg.Text = objdb.Alert("fa-ban", "alert-warning", "Alert !", ErrMsg);
                    }
                }
            }
            FillCategoryGrid();
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
    protected void GVItemCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            if (e.CommandName == "EditRecord")
            {

                Label lbl1 = (Label)row.FindControl("lblcatEng");
                Label lbl2 = (Label)row.FindControl("lblcatHin");

                txtItemCategoryEnglish.Text = lbl1.Text;
                txtItemCategoryHindi.Text = lbl2.Text;
                ViewState["ItemCat_id"] = e.CommandArgument.ToString();
                btnSave.Text = "Update";

            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            if (Page.IsValid)
            {
                if (btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("USP_ItemCategory_Insert", new string[] { "ItemCatName_Eng", "ItemCatName_Hin", "CreatedBy", "CreatedByIP" },
                        new string[] { txtItemCategoryEnglish.Text.Trim(), txtItemCategoryHindi.Text.Trim(), Session["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");

                }
                if (btnSave.Text == "Update")
                {
                    ds = objdb.ByProcedure("USP_ItemCategory_Update", new string[] { "ItemCatName_Eng", "ItemCatName_Hin", "ItemCat_id", "LastUpdatedBy", "LastUpdatedByIP" }, new string[] { txtItemCategoryEnglish.Text.Trim(), txtItemCategoryHindi.Text.Trim(), ViewState["ItemCat_id"].ToString(), Session["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");




                }
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            lblmsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            if (ds != null) { ds.Dispose(); }
                            //FillCategoryGrid();
                            //btnSave.Text = "Save";
                            //txtItemCategoryEnglish.Text = string.Empty;
                            //txtItemCategoryHindi.Text = string.Empty;
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            lblmsg.Text = objdb.Alert("fa-check", "alert-warning", "Warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Not Ok")
                        {
                            lblmsg.Text = objdb.Alert("fa-check", "alert-danger", "warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                    }

                }

                btnSave.Text = "Save";
                FillCategoryGrid();
                txtItemCategoryEnglish.Text = string.Empty;
                txtItemCategoryHindi.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblmsg.Text = string.Empty;
        txtItemCategoryEnglish.Text = string.Empty;
        txtItemCategoryHindi.Text = string.Empty;
        btnSave.Text = "Save";

    }
}