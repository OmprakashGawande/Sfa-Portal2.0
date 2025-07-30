using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class mis_Masters_ItemOfficeMapping : System.Web.UI.Page
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
                    FillItemCategory();
                    GetOfficeType();

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
    protected void FillItemCategory()
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

    protected void GetOfficeType()
    {
        try
        {
            ddlofficetype.Items.Clear();
            ds = objdb.ByProcedure("USP_GetOfficeType", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlofficetype.DataTextField = "OfficeType_Title";
                    ddlofficetype.DataValueField = "OfficeType_ID";
                    ddlofficetype.DataSource = ds.Tables[0];
                    ddlofficetype.DataBind();
                }
            }
            ddlofficetype.Items.Insert(0, new ListItem("Select", "0"));
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
        dt.Columns.Add("Office_ID", typeof(string));
        dt.Columns.Add("ItemOffice_IsActive", typeof(string));

        foreach (ListItem item in chkAllOfc.Items)
        {
            string Item_id = "";
            DataTable dtnew = (DataTable)ViewState["ds"];
            foreach (DataRow row in dtnew.Rows)
            {
                if (item.Value == row["Office_ID"].ToString())
                {
                    Item_id = row["Item_id"].ToString();
                }
            }

            if (Item_id == "0")
            {
                if (item.Selected)
                {
                    dt.Rows.Add(ddlitem.SelectedValue.ToString(), item.Value, "1");
                }

            }

        }
        return dt;
    }

    private DataTable GetUnMappedData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Item_id", typeof(string));
        dt.Columns.Add("Office_ID", typeof(string));
        dt.Columns.Add("ItemOffice_IsActive", typeof(string));
        foreach (ListItem item in chkAllOfc.Items)
        {
            string Item_id = "";
            DataTable dtnew = (DataTable)ViewState["ds"];
            foreach (DataRow row in dtnew.Rows)
            {
                if (item.Value == row["Office_ID"].ToString())
                {
                    Item_id = row["Item_id"].ToString();
                }
            }
            string ItemOffice_IsActive = "0";
            if (item.Selected == true)
            {
                ItemOffice_IsActive = "1";
            }
            if (Item_id != "0")
            {

                dt.Rows.Add(Item_id, item.Value, ItemOffice_IsActive);
            }

        }
        return dt;
    }
    #endregion

    #region Change Event

    protected void ddlitemcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlitem.ClearSelection();
            ds = objdb.ByProcedure("USP_GetItems", new string[] { "ItemType_id" }, new string[] { ddlitemcategory.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlitem.DataTextField = "ItemName";
                    ddlitem.DataValueField = "Item_id";
                    ddlitem.DataSource = ds.Tables[0];
                    ddlitem.DataBind();
                }
            }
            ddlitem.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    #endregion
    #region Button Click Event
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            dv.Visible = false;
            ds = objdb.ByProcedure("USP_getallOffices", new string[] { "OfficeType_ID", "Item_id" }, new string[] { ddlofficetype.SelectedValue.ToString() ,ddlitem.SelectedValue}, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dv.Visible = true;
                    chkAllOfc.DataTextField = "Office_Name";
                    chkAllOfc.DataValueField = "Office_ID";
                    chkAllOfc.DataSource = ds.Tables[0];
                    chkAllOfc.DataBind();
                    ViewState["ds"] = ds.Tables[0];
                    int Count = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < Count; i++)
                    {
                        string ItemOffice_IsActive = ds.Tables[0].Rows[i]["ItemOffice_IsActive"].ToString();
                        string OfficeID = ds.Tables[0].Rows[i]["Office_ID"].ToString();
                        foreach (ListItem item in chkAllOfc.Items)
                        {
                            if (item.Value == OfficeID)
                            {

                                if (ItemOffice_IsActive == "1")
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
            if (Page.IsValid)
            {
                lblmsg.Text = "";
                DataTable dt = GetMappedData();
                DataTable dt1 = GetUnMappedData();
                ds = objdb.ByProcedure("Usp_InsertUpdateItemOfficeMapping",
                                       new string[] { "CreatedBy", "CreatedByIP" },
                                       new string[] { ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress() },
                                       new string[] { "type_InsertItemOfficeChild", "type_UpdateItemOfficeChild" },
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
                ddlofficetype.ClearSelection();
                ddlitemcategory_SelectedIndexChanged(sender, e);
                dv.Visible = false;
                chkAllOfc.Items.Clear();
            }

        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
}