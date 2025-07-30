using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Master_Item_Category_Mst : System.Web.UI.Page
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
                GetMaincategory();
                FillItemCategoryGrid();

            }
            }
        }
        catch(Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        
        }

    }
    private void GetMaincategory()
    {
        try
        {
            ddlMainCategory.Items.Clear();
            ds = objdb.ByProcedure("USP_GetMainCategory", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables.Count> 0)
            {
                if(ds.Tables[0].Rows.Count>0)
                {
                    ddlMainCategory.DataTextField = "ItemMainCategory";
                    ddlMainCategory.DataValueField = "ItemCat_id";
                    ddlMainCategory.DataSource = ds.Tables[0];
                    ddlMainCategory.DataBind();

                }
                ddlMainCategory.Items.Insert(0, new ListItem("Select", "0"));
                }
                
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    private void FillItemCategoryGrid()
    {
        try
        {
            GVItemSubCat.DataSource = string.Empty;
            GVItemSubCat.DataBind();

            ds = objdb.ByProcedure("USP_ItemCategoryGrid", new string[] { }, new string[] { }, "dataset");
            if(ds!=null && ds.Tables.Count>0)
            {
                if(ds.Tables[0].Rows.Count>0)
                {
                    GVItemSubCat.DataSource = ds;
                    GVItemSubCat.DataBind();
                }
                
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void GVItemSubCat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            if (e.CommandName == "EditRecord")
            {

                Label lbl1 = (Label)row.FindControl("lblitemMaincategoryID");
                Label lbl2 = (Label)row.FindControl("lblitemcategoryE");
                Label lbl3 = (Label)row.FindControl("lblitemcategoryH");


                ddlMainCategory.SelectedValue = lbl1.Text;
                txtItemCategoryEnglish.Text = lbl2.Text;
                txtItemCategoryHindi.Text = lbl3.Text;
                ViewState["ItemType_id"] = e.CommandArgument.ToString();
                btnSave.Text = "Update";



               
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
            string ItemSubCat_Id = GVItemSubCat.DataKeys[row.RowIndex].Value.ToString();
            string Activecbx = "";
            if (cbx.Checked == true)
            {
                Activecbx = "1";
            }
            else
            {
                Activecbx = "0";
            }
            ds = objdb.ByProcedure("USP_ItemSubCategory_IsActive", new string[] { "ItemType_id", "ItemType_IsActive", "LastIsActiveBy", "LastIsActiveByIP" }, new string[] { ItemSubCat_Id, Activecbx, Session["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");
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
            FillItemCategoryGrid();
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
           if(Page.IsValid)
           {
               lblmsg.Text = "";
               if (btnSave.Text == "Save")
               {
                   ds = objdb.ByProcedure("USP_ItemSubCategory_Insert", new string[] { "ItemCat_id", "ItemTypeName_Eng", "ItemTypeName_Hin", "CreatedBy", "CreatedByIP" }, new string[] {ddlMainCategory.SelectedValue,txtItemCategoryEnglish.Text,txtItemCategoryHindi.Text ,Session["Emp_ID"].ToString(), objdb.GetLocalIPAddress()}, "dataset");
                   if(ds!=null && ds.Tables.Count>0)
                   {
                       if(ds.Tables[0].Rows.Count>0)
                       {
                           if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                           {

                               lblmsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                               if (ds != null) { ds.Dispose(); }
                               FillItemCategoryGrid();
                               ddlMainCategory.ClearSelection();
                               txtItemCategoryEnglish.Text = string.Empty;
                               txtItemCategoryHindi.Text = string.Empty;
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
               }
               if(btnSave.Text=="Update")
               {
                   ds = objdb.ByProcedure("USP_ItemSubCategory_Update", new string[] { "ItemCat_id", "ItemTypeName_Eng", "ItemTypeName_Hin", "LastUpdatedBy", "LastUpdatedByIP", "ItemType_id" }, new string[] { ddlMainCategory.SelectedValue, txtItemCategoryEnglish.Text, txtItemCategoryHindi.Text, Session["Emp_ID"].ToString(), objdb.GetLocalIPAddress(), ViewState["ItemType_id"].ToString() }, "dataset");
                   if(ds!=null && ds.Tables.Count>0)
                   {
                       if(ds.Tables[0].Rows.Count>0)
                       {
                           if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                           {

                               lblmsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                               if (ds != null) { ds.Dispose(); }
                               FillItemCategoryGrid();
                               ddlMainCategory.ClearSelection();
                               txtItemCategoryEnglish.Text = string.Empty;
                               txtItemCategoryHindi.Text = string.Empty;
                               
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

               }
               btnSave.Text = "Save";
           }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ddlMainCategory.ClearSelection();
            txtItemCategoryEnglish.Text = string.Empty;
            txtItemCategoryHindi.Text = string.Empty;
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
}