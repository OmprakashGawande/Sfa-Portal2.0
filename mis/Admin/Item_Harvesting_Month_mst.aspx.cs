using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class mis_Masters_Item_Harvesting_Month_mst : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    IFormatProvider culture = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["Emp_ID"] = "";
            fillItemType();

        }
    }


    protected void fillItemType()
    {
        #region
        try
        {
            ds = obj.ByProcedure("USP_Itemtype_GetById", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlItemType.DataValueField = "ItemType_id";
                    ddlItemType.DataTextField = "ItemTypeName";
                    ddlItemType.DataSource = ds;
                    ddlItemType.DataBind();
                }
            }
            ddlItemType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
        }
        catch (Exception)
        {

            throw;
        }
        #endregion
    }
    protected void fillItem(string id)
    {
        #region
        try
        {
            ddlItem.Enabled = false;
            ds = obj.ByProcedure("USP_Item_GetById", new string[] { "ItemType_id" }, new string[] { id }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlItem.Enabled = true;
                    ddlItem.DataValueField = "Item_id";
                    ddlItem.DataTextField = "ItemName";
                    ddlItem.DataSource = ds;
                    ddlItem.DataBind();
                }
            }
            ddlItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblmsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        #endregion
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        #region btnUpdate_Click
        try
        {
            if (Page.IsValid)
            {
                lblmsg.Text = "";
                DataTable dtInsert = new DataTable();
                dtInsert.Columns.Add("Item_id", typeof(string));
                dtInsert.Columns.Add("Month_No", typeof(string));
                dtInsert.Columns.Add("Harvesting_IsActive", typeof(string));

                DataTable dtUpdate = new DataTable();
                dtUpdate.Columns.Add("Item_id", typeof(string));
                dtUpdate.Columns.Add("Month_No", typeof(string));
                dtUpdate.Columns.Add("Harvesting_IsActive", typeof(string));

                Dictionary<int, string> getMonthByItem =
                       new Dictionary<int, string>();
                DataTable dtnew = (DataTable)ViewState["ds"];
                foreach (DataRow row in dtnew.Rows)
                {
                    getMonthByItem.Add(Convert.ToInt32(row["Month_No"]), row["Item_id"].ToString().Trim());
                }

                foreach (ListItem item in chkAllMonth.Items)
                {
                    string Item_id = "";

                    Item_id = getMonthByItem[Convert.ToInt32(item.Value)];
                    if (Item_id == "0")
                    {
                        if (item.Selected)
                        {
                            dtInsert.Rows.Add(ddlItem.SelectedValue.ToString(), item.Value, "1");
                        }

                    }
                    else
                    {
                        string Harvesting_IsActive = "0";
                        if (item.Selected == true)
                        {
                            Harvesting_IsActive = "1";
                        }
                        if (Item_id != "0")
                        {

                            dtUpdate.Rows.Add(Item_id, item.Value, Harvesting_IsActive);
                        }
                    }

                }

                ds = obj.ByProcedure("Usp_InsertUpdate_Item_Harvesting_Month",
                                            new string[] { "CreatedBy", "CreatedByIP" },
                                            new string[] { ViewState["Emp_ID"].ToString(), obj.GetLocalIPAddress() },
                                            new string[] { "type_MST_Item_Harvesting_Month_Insert", "type_MST_Item_Harvesting_Month_Update" },
                                            new DataTable[] { dtInsert, dtUpdate }, "TableSave");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblmsg.Text = obj.Alert("fa-check", "alert-success", "Thank You !", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                        else
                        {
                            lblmsg.Text = obj.Alert("fa-ban", "alert-danger", "Alert !", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                    }
                }
                ddlItemType.ClearSelection();
                ddlItem.Items.Clear();
                ddlItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                dv.Visible = false;
                chkAllMonth.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        #endregion
    }
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region ddlItemType_SelectedIndexChanged
        try
        {
            lblmsg.Text = "";
            ddlItem.Items.Clear();
            dv.Visible = false;
            chkAllMonth.Items.Clear();
            if (ddlItemType.SelectedValue != "0")
            {
                ddlItem.Enabled = true;
                fillItem(ddlItemType.SelectedValue.ToString());
            }
            else
            {
                ddlItem.Enabled = false;
                ddlItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblmsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        #endregion
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region ddlItem_SelectedIndexChanged
        try
        {
            lblmsg.Text = "";
            dv.Visible = false;
            chkAllMonth.Items.Clear();
            if (ddlItem.SelectedIndex > 0)
            {
                ds = obj.ByProcedure("MST_Item_Harvesting_Month_GetByItemId", new string[] { "Item_Id" }, new string[] { ddlItem.SelectedValue.ToString() }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dv.Visible = true;
                        chkAllMonth.DataTextField = "Month_Name";
                        chkAllMonth.DataValueField = "Month_No";
                        chkAllMonth.DataSource = ds.Tables[0];


                        chkAllMonth.DataBind();
                        ViewState["ds"] = ds.Tables[0];
                        int Count = ds.Tables[0].Rows.Count;
                        for (int i = 0; i < Count; i++)
                        {
                            string ItemModuleMapping_IsActive = ds.Tables[0].Rows[i]["Harvesting_IsActive"].ToString();
                            string Item_id = ds.Tables[0].Rows[i]["Month_No"].ToString();
                            foreach (ListItem item in chkAllMonth.Items)
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

        }
        catch (Exception ex)
        {
            lblmsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        #endregion
    }
}