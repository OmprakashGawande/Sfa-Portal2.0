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
public partial class mis_Admin_Set_Item_MinSaleQty : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    IFormatProvider culture = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        #region
        if (Session["Emp_ID"].ToString() != null && Session["Emp_ID"].ToString() != "" && Session["Office_ID"].ToString() != null && Session["Office_ID"].ToString() != "")
        {
            if (!IsPostBack)
            {
               

                lblMsg.Text = "";
                ViewState["UserId"] = Session["Emp_ID"].ToString();
                //ViewState["OfficeId"] = "1";
                //gridviewHistory.DataSource = null;
                //gridviewHistory.DataBind();
                fillGrid();
                fillItemType();
                //clrAll();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
        #endregion
    }
    #region Usr Def Event
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
        catch (Exception ex)
        {

            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! ", ex.ToString());

        }
        #endregion
    }
    protected void fillItem(string id)
    {
        #region
        try
        {
            //ddlItem.Enabled = false;
            ds = obj.ByProcedure("USP_Item_GetById", new string[] { "ItemType_id" }, new string[] { id }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    // ddlItem.Enabled = true;
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

            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! ", ex.ToString());

        }
        #endregion
    }
    protected void fillHistoryGrid(string Item_Id)
    {
        try
        {
            gridviewHistory.DataSource = null;
            gridviewHistory.DataBind();
            ViewState["CurrentQtyDate"] = "";
            ds = obj.ByProcedure("USP_TradeOrMSP_Mst_SetItem_MinSaleQty_GetHistory_ByItemID", new string[] { "Item_Id" }, new string[] { Item_Id }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["CurrentQtyDate"] = ds.Tables[1].Rows[0]["EffectiveDate"].ToString();
                    gridviewHistory.DataSource = ds.Tables[0];
                    gridviewHistory.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void fillGrid()
    {
        #region
        try
        {
            gridview.DataSource = null;
            gridview.DataBind();
            ds = obj.ByProcedure("USP_TradeOrMSP_Mst_SetItem_MinSaleQty_GetData", new string[] { "ItemType_Id" }, new string[] { ddlItemType.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    gridview.DataSource = ds.Tables[0];
                    gridview.DataBind();
                }

            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! ", ex.ToString());

        }
        #endregion
    }
    #endregion
    #region Click Event
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region
        try
        {
            lblMsg.Text = "";
            lblUnitRate.Text = "";
            ddlItem.Items.Clear();
            //gridviewHistory.DataSource = null;
            //gridviewHistory.DataBind();
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
           // fillgrid();

        }
        catch (Exception ex)
        {

            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! ", ex.ToString());

        }
        #endregion
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region
        try
        {
            lblMsg.Text = "";
            lblUnitRate.Text = "";
            if (ddlItem.SelectedIndex > 0)
            {
                ds = obj.ByProcedure("USP_GetItemUnit_ByItemId", new string[] { "Item_Id" }, new string[] { ddlItem.SelectedValue.ToString() }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        lblUnitRate.Text = " ( In " + ds.Tables[0].Rows[0]["UnitName_Eng"].ToString() + " )";
                    }
                }
            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! ", ex.ToString());

        }
        #endregion
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region
        try
        {

            lblMsg.Text = "";
            if (txtMinQty.Text.Trim() == ".")
            {
                txtMinQty.Text = "0.00";
            }
            if (Page.IsValid)
            {
                DateTime txtEffectiveDt = Convert.ToDateTime(txtDate.Text, culture);
                DateTime EffectiveDt = DateTime.Now.Date;
                int res = DateTime.Compare(txtEffectiveDt, EffectiveDt);

                if (res >= 0)
                {
                    if (ddlItem.SelectedValue != "0" && txtMinQty.Text != "" && txtDate.Text != "")
                    {
                        string ApplicableDate = (Convert.ToDateTime(txtDate.Text, culture).ToString("yyyy/MM/dd"));
                        if (btnSave.Text == "Save")
                        {

                            ds = obj.ByProcedure("USP_TradeOrMSP_Mst_SetItem_MinSaleQty_Insert",
                                new string[] { "ItemType_Id", "Item_Id", "Item_MinSaleQty",
                                               "EffectiveDate", "CreatedBy", "CreatedByIP" },
                                new string[] { ddlItemType.SelectedValue.ToString(), ddlItem.SelectedValue.ToString(), txtMinQty.Text.Trim(),
                                             ApplicableDate, ViewState["UserId"].ToString(), obj.GetLocalIPAddress() }, "dataset");

                        }
                        if (btnSave.Text == "Update")
                        {
                            decimal minQty =  Convert.ToDecimal(ViewState["MinQty"]);
                            decimal txtminQty = Convert.ToDecimal(txtMinQty.Text);
                            if (minQty != txtminQty)
                            {
                                ds = obj.ByProcedure("USP_TradeOrMSP_Mst_SetItem_MinSaleQty_Update",
                                    new string[] { "ItemType_Id", "Item_Id", "Item_MinSaleQty",
                                                "EffectiveDate", "LastUpdatedBy", "LastUpdatedByIP", 
                                                "MinID" },
                                            new string[] { ddlItemType.SelectedValue.ToString(), ddlItem.SelectedValue.ToString(), txtMinQty.Text.Trim(),
                                                ApplicableDate, ViewState["UserId"].ToString(), obj.GetLocalIPAddress(),
                                                ViewState["MinID"].ToString() }, "dataset");
                            }
                            else
                            {
                                lblMsg.Text = obj.Alert("fa-ban", "alert-info", "Sorry! ", "Qantity already exists");
                            }
                        }
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds != null && ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                                {
                                    lblMsg.Text = obj.Alert("fa-check", "alert-success", "ThankYou", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                }
                                else
                                {
                                    lblMsg.Text = obj.Alert("fa-ban", "alert-info", "Sorry! ", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                }
                            }
                        }


                        fillGrid();
                        //clrAll();
                        ddlItem.Enabled = true;
                    }
                }
                else
                {
                    lblMsg.Text = obj.Alert("fa-ban", "alert-info", "Sorry! ", "You can not update record on this date");
                }
            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! ", ex.ToString());

        }
        #endregion
    }
    protected void gridview_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try { 
        lblMsg.Text = "";
        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        if (e.CommandName == "btnUpdate")
        {
            Label EffectiveDate = (Label)row.FindControl("EffectiveDat");
            Label itemMinSaleQty = (Label)row.FindControl("ItemMinSaleQty");
            Label Item_Id = (Label)row.FindControl("ItemId");
            Label ItemType_Id = (Label)row.FindControl("ItemTypeId");

            ds = obj.ByProcedure("USP_Item_GetById", new string[] { "ItemType_id" }, new string[] { ItemType_Id.Text.Trim() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlItem.DataValueField = "Item_id";
                    ddlItem.DataTextField = "ItemName";
                    ddlItem.DataSource = ds;
                    ddlItem.DataBind();
                }
            }
            ddlItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            ddlItem.SelectedValue = Item_Id.Text;
            ddlItemType.SelectedValue = ItemType_Id.Text;
            txtMinQty.Text = itemMinSaleQty.Text;
            ViewState["MinQty"] = itemMinSaleQty.Text;
            txtDate.Text = (Convert.ToDateTime(EffectiveDate.Text, culture).ToString("dd/MM/yyyy"));

            ViewState["EffectiveDate"] = (Convert.ToDateTime(EffectiveDate.Text, culture).ToString("dd/MM/yyyy"));
            ddlItem_SelectedIndexChanged(sender, e);
            ViewState["MinID"] = e.CommandArgument.ToString();
            btnSave.Text = "Update";
            ddlItemType.Enabled = false;
            ddlItem.Enabled = false;

        }
        else if (e.CommandName == "ViewDetails")
        {
            fillHistoryGrid(e.CommandArgument.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ViewDetails()", true);
        }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    protected void gridviewHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try { 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label effectiveDat = (Label)e.Row.FindControl("EffectiveDat");
            Image imgNew = (Image)e.Row.FindControl("ImgNew");
            imgNew.Visible = false;

            if (ViewState["CurrentQtyDate"] != null)
            {
                DateTime EfDt = Convert.ToDateTime(effectiveDat.Text, culture);
                DateTime CurrentDt = Convert.ToDateTime(ViewState["CurrentQtyDate"].ToString(), culture);
                if (EfDt == CurrentDt)
                {
                    e.Row.BackColor = System.Drawing.Color.Green;
                    e.Row.ForeColor = System.Drawing.Color.White;


                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.White;
                    e.Row.ForeColor = System.Drawing.Color.Black;

                }
                if (EfDt > CurrentDt)
                {
                    imgNew.Visible = true;
                    imgNew.ImageUrl = "../image/new.gif";
                }
            }
        }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}