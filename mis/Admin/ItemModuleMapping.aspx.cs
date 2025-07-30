using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class mis_Masters_ItemModuleMapping : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();

    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            if (Session["Emp_ID"] != null && Session["Emp_ID"] != "" && Session["Office_ID"] != null && Session["Office_ID"] != "")
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"];
                    ViewState["Office_ID"] = Session["Office_ID"];
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    FillCategory();
                    FillModuleName();

                }

            }
            else
            {
                Response.Redirect("../login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    #endregion

    #region User Defined Function

    protected void FillCategory()
    {
        try
        {
            ddlitemcategory.Items.Clear();
            ds = objdb.ByProcedure("USP_FillItemCategory", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlitemcategory.DataTextField = "ItemCategory";
                    ddlitemcategory.DataValueField = "ItemType_id";
                    ddlitemcategory.DataSource = ds.Tables[0];
                    ddlitemcategory.DataBind();
                }
            }
            ddlitemcategory.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillModuleName()
    {
        try
        {
            ddlModule.Items.Clear();
            ds = objdb.ByProcedure("SpUMModuleMaster", new string[] {"flag" }, new string[] {"2" }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlModule.DataTextField = "Module_Name_E";
                    ddlModule.DataValueField = "Module_ID";
                    ddlModule.DataSource = ds.Tables[0];
                    ddlModule.DataBind();
                }
            }
            ddlModule.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    private DataTable GetMappedData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Item_id", typeof(string));
        dt.Columns.Add("Module_ID", typeof(string));
        dt.Columns.Add("ItemModuleMapping_IsActive", typeof(string));

        foreach (ListItem item in chkAllItem.Items)
        {
            string Module_ID = "";
            DataTable dtnew = (DataTable)ViewState["ds"];
            foreach (DataRow row in dtnew.Rows)
            {
                if (item.Value == row["Item_id"].ToString())
                {
                    Module_ID = row["Module_ID"].ToString();
                }
            }

            if (Module_ID == "0")
            {
                if (item.Selected)
                {
                    dt.Rows.Add(item.Value, ddlModule.SelectedValue.ToString(), "1");
                }

            }

        }
        return dt;
    }
    private DataTable GetUnMappedData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Item_id", typeof(string));
        dt.Columns.Add("Module_ID", typeof(string));
        dt.Columns.Add("ItemModuleMapping_IsActive", typeof(string));
        foreach (ListItem item in chkAllItem.Items)
        {
            string Module_ID = "";
            DataTable dtnew = (DataTable)ViewState["ds"];
            foreach (DataRow row in dtnew.Rows)
            {
                if (item.Value == row["Item_id"].ToString())
                {
                    Module_ID = row["Module_ID"].ToString();
                }
            }
            string ItemModuleMapping_IsActive = "0";
            if (item.Selected == true)
            {
                ItemModuleMapping_IsActive = "1";
            }
            if (Module_ID != "0")
            {

                dt.Rows.Add(item.Value, Module_ID, ItemModuleMapping_IsActive);
            }

        }
        return dt;
    }
    #endregion

    #region Button Click Event

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            dv.Visible = false;
            ds = objdb.ByProcedure("USP_GetItemMappedtoModule", new string[] { "ItemType_id" }, new string[] { ddlitemcategory.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dv.Visible = true;
                    chkAllItem.DataTextField = "ItemName_Eng";
                    chkAllItem.DataValueField = "Item_id";
                    chkAllItem.DataSource = ds.Tables[0];


                    chkAllItem.DataBind();
                    ViewState["ds"] = ds.Tables[0];
                    int Count = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < Count; i++)
                    {
                        string ItemModuleMapping_IsActive = ds.Tables[0].Rows[i]["ItemModuleMapping_IsActive"].ToString();
                        string Item_id = ds.Tables[0].Rows[i]["Item_id"].ToString();
                        foreach (ListItem item in chkAllItem.Items)
                        {
                            if (item.Value == Item_id)
                            {

                                if (ItemModuleMapping_IsActive.ToString() == "True")
                                {
                                    item.Selected = true;
                                }
                                
                            }
                        }
                    }
                    
                }
                
              

            }
        }

        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if(Page.IsValid)
            {
                lblmsg.Text = "";
                DataTable dt = GetMappedData();
                DataTable dt1 = GetUnMappedData();
                ds = objdb.ByProcedure("Usp_InsertUpdateItemModuleMapping",
                                       new string[] { "CreatedBy", "CreatedByIP" },
                                       new string[] { ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress() },
                                       new string[] { "type_InsertItemModuleMapping", "type_UpdateItemModuleMapping" },
                                       new DataTable[] { dt, dt1 }, "TableSave");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblmsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You !", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                        else
                        {
                            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                    }
                }
                ddlitemcategory.ClearSelection();
                ddlModule.ClearSelection();
                
                dv.Visible = false;
                chkAllItem.Items.Clear();
            }
            
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
}