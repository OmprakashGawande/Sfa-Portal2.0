using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text;
using System.Globalization;

public partial class Trade_ItemMasterAgro : System.Web.UI.Page
{
    DataSet ds, ds1, ds2, ds3, ds4;
    static DataSet ds5;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    #region PageLoad Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";

            if (!IsPostBack)
            {
                FillItemMainCategory();
                FillItemCategory();
                FillItemUnit();
                if (txtSearch.Text == "")
                {
                    FillItemGrid();
                }
                FillHSNCode();
                ViewState["ItemId"] = "0";
                FillPurchaseLedger(Session["Office_ID"].ToString());
                FillSalesLedger(Session["Office_ID"].ToString());
                ViewState["Office_ID"] = Session["Office_ID"];
                ClearData();
                ddlcategory.Enabled = true;
                ddlmaincategory.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    #endregion
    #region User Defined Function
    protected void FillPurchaseLedger(string Office_ID)
    {
        try
        {
            ddlpurchaseledger.Items.Clear();
            ds = objdb.ByProcedure("USP_Item_SpFinLedgerMaster", new string[] { "flag", "Office_ID" }, new string[] { "2", Office_ID }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlpurchaseledger.DataSource = ds;
                ddlpurchaseledger.DataTextField = "Ledger_Name";
                ddlpurchaseledger.DataValueField = "Ledger_ID";
                ddlpurchaseledger.DataBind();
            }
            ddlpurchaseledger.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillSalesLedger(string Office_ID)
    {
        try
        {
            ddlsalesledger.Items.Clear();
            ds = objdb.ByProcedure("USP_Item_SpFinLedgerMaster", new string[] { "flag", "Office_ID" }, new string[] { "1", Office_ID }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlsalesledger.DataSource = ds;
                ddlsalesledger.DataTextField = "Ledger_Name";
                ddlsalesledger.DataValueField = "Ledger_ID";
                ddlsalesledger.DataBind();

            }
            ddlsalesledger.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //Fill Item Main Category Dropdown
    protected void FillItemMainCategory()
    {
        try
        {
            ddlmaincategory.Items.Clear();
            ds1 = objdb.ByProcedure("USP_GetMainCategory", new string[] { }, new string[] { }, "dataset");
            if (ds1 != null && ds1.Tables.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ddlmaincategory.DataTextField = "ItemMainCategory";
                    ddlmaincategory.DataValueField = "ItemCat_id";
                    ddlmaincategory.DataSource = ds1.Tables[0];
                    ddlmaincategory.DataBind();

                }
            }
            ddlmaincategory.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Fill Item Category Dropdown on Main Category Selection
    protected void FillItemCategory()
    {
        try
        {
            ddlcategory.Items.Clear();
            if (ddlmaincategory.SelectedIndex > 0)
            {
                ds2 = objdb.ByProcedure("USP_GetItemCategory", new string[] { "ItemCat_id" }, new string[] { ddlmaincategory.SelectedValue.ToString() }, "dataset");
                if (ds2 != null && ds2.Tables.Count > 0)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        ddlcategory.DataTextField = "ItemCategory";
                        ddlcategory.DataValueField = "ItemType_id";
                        ddlcategory.DataSource = ds2.Tables[0];
                        ddlcategory.DataBind();

                    }
                }
            }
            ddlcategory.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Fill Item Unit Dropdown
    protected void FillItemUnit()
    {
        try
        {
            ddlitemunit.Items.Clear();
            ds3 = objdb.ByProcedure("USP_GetUnits", new string[] { }, new string[] { }, "dataset");

            if (ds3 != null && ds3.Tables.Count > 0)
            {
                if (ds3.Tables[0].Rows.Count > 0)
                {
                    ddlitemunit.DataTextField = "UnitName";
                    ddlitemunit.DataValueField = "Unit_id";
                    ddlitemunit.DataSource = ds3.Tables[0];
                    ddlitemunit.DataBind();

                }
            }
            ddlitemunit.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Fill HSN Code Dropdown
    private void FillHSNCode()
    {
        try
        {
            ds = objdb.ByProcedure("USP_GetHSNcode", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlhsncode.DataTextField = "HSN_Code";
                    ddlhsncode.DataValueField = "HSN_ID";
                    ddlhsncode.DataSource = ds.Tables[0];
                    ddlhsncode.DataBind();

                }
                ddlhsncode.Items.Insert(0, new ListItem("select", "0"));

            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ClearData()
    {
        try
        {
            GVitemdetails.Visible = true;
            gvSearchItemDetails.Visible = false;
            txtitemnameE.Text = string.Empty;
            txtitemnameH.Text = string.Empty;
            txtitemcode.Text = string.Empty;
            txtpackagingsize.Text = string.Empty;
            txtitemdescription.Text = string.Empty;
            txtitemcode.Text = string.Empty;
            ddlmaincategory.ClearSelection();
            ddlhsncode.ClearSelection();
            ddlpurchaseledger.ClearSelection();
            ddlsalesledger.ClearSelection();
            FillItemCategory();
            ddlhsncode.ClearSelection();
            ddlitemunit.ClearSelection();
            ViewState["ItemId"] = "0";
            btnsave.Text = "Save";

        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Fill Item Details in GridView
    protected void FillItemGrid()
    {
        try
        {
            GVitemdetails.Visible = true;
            gvSearchItemDetails.Visible = false;
            GVitemdetails.VirtualItemCount = GetRowCount();
            GetPageData(1, 100);
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //UpperCase Conversion of First Letter
    public string FirstLetterToUpper(string str)
    {
        string txt = cult.TextInfo.ToTitleCase(str.ToLower());
        //  System.Web.Globalization.cult.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        return txt;
    }
    #endregion

    #region Button Click Event

    //Save Data
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            string msg = "";
            if (ddlmaincategory.SelectedIndex == 0)
            {
                msg += "Select MainCategory.\\n";
            }
            if (ddlcategory.Text == "")
            {
                msg += "Select ItemCategory.\\n";
            }
            if (txtitemnameE.Text == "")
            {
                msg = "Enter Item Name In English";
            }


            if (txtitemnameH.Text == "")
            {
                msg = "Enter Item Name in Hindi";
            }
            //if (txtpackagingsize.Text == "")
            //{
            //    msg = "Enter Packaging Size";
            //}
            //if (txtitemcode.Text == "")
            //{
            //    msg = "Enter Item Code";
            //}
            if (ddlitemunit.SelectedIndex == 0)
            {
                msg += "Select Item Unit";
            }
            //if (ddlhsncode.SelectedValue != "0")
            //{
            //    msg += "Select HSN Code";
            //}
            //if (chkpurchaseledger.Checked)
            //{
            //    if (ddlpurchaseledger.SelectedIndex > 0)
            //    {
            //        msg += "Select Purchase Ledger";
            //    }
            //}
            //if (chksalesledger.Checked)
            //{
            //    if (ddlsalesledger.SelectedIndex > 0)
            //    {
            //        msg += "Select Sales Ledger";
            //    }
            //}
            //if (txtitemdescription.Text == "")
            //{
            //    msg = "Enter Item Description";
            //}
            //if (txtpackagingsize.Text == "")
            //{
            //    msg = "Enter Size of Item";
            //}
            if (msg.Trim() == "")
            {

                txtitemnameE.Text = FirstLetterToUpper(txtitemnameE.Text);
                txtitemnameH.Text = FirstLetterToUpper(txtitemnameH.Text);


                if (btnsave.Text == "Save" && ViewState["ItemId"].ToString() == "0")
                {
                    ds = objdb.ByProcedure("USP_ItemInsert",
                        new string[] { "ItemName_Eng", 
                                           "ItemName_Hin",
                                           "ItemAliasCode", 
                                           "ItemCat_id",
                                           "ItemType_id",
                                           "Unit_id",
                                           "HSNCode",  
                                           "ItemSpecification", 
                                           "PackagingSize", 
                                           "CreatedBy", 
                                           "CreatedByIP" ,
                                            "PurchaseLedger_id",
                                            "SalesLedger_id"
                                            ,"HSN_ID"},
                        new string[] { txtitemnameE.Text, 
                                           txtitemnameH.Text, 
                                           txtitemcode.Text, 
                                           ddlmaincategory.SelectedValue, 
                                           ddlcategory.SelectedValue,
                                           ddlitemunit.SelectedValue,
                                           ddlhsncode.SelectedItem.Text , 
                                           txtitemdescription.Text, 
                                           txtpackagingsize.Text, 
                                           Session["Emp_ID"].ToString(), 
                                           objdb.GetLocalIPAddress(),
                                            ddlpurchaseledger.SelectedValue.ToString(),
                                            ddlsalesledger.SelectedValue.ToString(),
                                             ddlhsncode.SelectedValue}
                        , "dataset");

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                lblmsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                            {
                                lblmsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                            else
                            {
                                lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                            ClearData();
                            FillItemGrid();
                        }
                    }
                }

                else if (btnsave.Text == "Update" && ViewState["Item_id"].ToString() != "")
                {
                    ds = objdb.ByProcedure("USP_Item_Update",
                        new string[] { "ItemName_Eng", 
                                           "ItemName_Hin", 
                                           "ItemAliasCode", 
                                           "ItemCat_id", 
                                           "ItemType_id", 
                                           "Unit_id", 
                                           "HSNCode", 
                                           "ItemSpecification", 
                                           "PackagingSize", 
                                           "Item_id", 
                                           "LastUpdatedBy", 
                                           "LastUpdatedByIP" ,
                                            "PurchaseLedger_id",
                                            "SalesLedger_id",
                                             "HSN_ID"},
                        new string[] { txtitemnameE.Text, 
                                           txtitemnameH.Text, 
                                           txtitemcode.Text, 
                                           ddlmaincategory.SelectedValue, 
                                           ddlcategory.SelectedValue, 
                                           ddlitemunit.SelectedValue, 
                                           ddlhsncode.SelectedItem.Text,
                                           txtitemdescription.Text, 
                                           txtpackagingsize.Text, 
                                           ViewState["Item_id"].ToString(), 
                                           Session["Emp_ID"].ToString(), 
                                           objdb.GetLocalIPAddress(),
                                             ddlpurchaseledger.SelectedValue.ToString(),
                                            ddlsalesledger.SelectedValue.ToString()
                                            ,ddlhsncode.SelectedValue},
                                       "dataset");

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                lblmsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                            {
                                lblmsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                            else
                            {
                                lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                            ClearData();
                            
                            ddlcategory.Enabled = true;
                            ddlmaincategory.Enabled = true;
                            int pageNumber = GVitemdetails.PageIndex;
                            GetPageData((pageNumber + 1), 100);
                        }
                    }

                }
                GVitemdetails.Visible = true;
                gvSearchItemDetails.Visible = false;

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction()", "alert('" + msg + "')", true);
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Clear All Fields
    protected void btnclear_Click(object sender, EventArgs e)
    {
        ClearData();
        ddlcategory.Enabled = true;
        ddlmaincategory.Enabled = true;
        //if (GVitemdetails.Rows.Count > 0)
        //{
        //    GVitemdetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        //    GVitemdetails.UseAccessibleHeader = true;
        //}
    }

    #endregion

    #region Change Event

    protected void ddlmaincategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillItemCategory();
            if (GVitemdetails.Rows.Count > 0)
            {
                GVitemdetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                GVitemdetails.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    #endregion

    #region GridView Row Event

    //Get Selected Item Details
    protected void GVitemdetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lbl1 = (Label)row.FindControl("lblitemnameE");
                Label lbl2 = (Label)row.FindControl("lblitemnameH");
                Label lbl3 = (Label)row.FindControl("lblmaincategoryId");
                Label lbl4 = (Label)row.FindControl("lblitemcategoryID");
                Label lbl5 = (Label)row.FindControl("lblunit_ID");
                //Label lbl6 = (Label)row.FindControl("lblitemrate");
                Label lbl7 = (Label)row.FindControl("lblitemcode");
                Label lbl8 = (Label)row.FindControl("lblitempacsize");
                Label lbl9 = (Label)row.FindControl("lblitemDescription");
                Label lbl10 = (Label)row.FindControl("lblhsncode");
                Label lblPurchaseLedgerid = (Label)row.FindControl("lblPurchaseLedgerid");
                Label lblSalesLedgerid = (Label)row.FindControl("lblSalesLedgerid");
                txtitemnameE.Text = lbl1.Text;

                txtitemnameH.Text = lbl2.Text;

                ddlmaincategory.ClearSelection();

                ddlmaincategory.Items.FindByValue(lbl3.Text).Selected = true;

                FillItemCategory();

                ddlcategory.ClearSelection();

                ddlcategory.Items.FindByValue(lbl4.Text).Selected = true;

                ddlitemunit.ClearSelection();

                ddlitemunit.SelectedValue = lbl5.Text;

                txtitemcode.Text = lbl7.Text;

                txtpackagingsize.Text = lbl8.Text;

                txtitemdescription.Text = lbl9.Text;

                ddlhsncode.ClearSelection();
                if (ddlhsncode.Items.FindByText(lbl10.Text) != null)
                {
                    ddlhsncode.Items.FindByText(lbl10.Text).Selected = true;
                }
                ddlpurchaseledger.ClearSelection();
                string officeid = ViewState["Office_ID"].ToString();
                FillPurchaseLedger(officeid);
                if (lblPurchaseLedgerid.Text != "0")
                {
                    ddlpurchaseledger.Items.FindByText(lblPurchaseLedgerid.Text).Selected = true;
                }
                ddlsalesledger.ClearSelection();
                FillSalesLedger(officeid);
                if (lblSalesLedgerid.Text != "0")
                {
                    ddlsalesledger.Items.FindByValue(lblSalesLedgerid.Text).Selected = true;
                }
                ViewState["Item_id"] = e.CommandArgument;
                btnsave.Text = "Update";
                ddlcategory.Enabled = false;
                ddlmaincategory.Enabled = false;
                //GVitemdetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                //GVitemdetails.UseAccessibleHeader = true;
            }

        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void chkactiveItem_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            CheckBox cbx = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cbx.NamingContainer;
            string Item_Id = GVitemdetails.DataKeys[row.RowIndex].Value.ToString();
            string Activecbx = "";
            if (cbx.Checked == true)
            {
                Activecbx = "1";
            }
            else
            {
                Activecbx = "0";
            }
            ds = objdb.ByProcedure("USP_ItemMasterActive", new string[] { "Item_id", "Item_IsActive", "LastIsActiveBy", "LastIsActiveByIP" }, new string[] { Item_Id, Activecbx, Session["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");
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
            FillItemGrid();
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    #endregion
    protected void GetPageData(int currentPage, int pageSize)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = objdb.ByProcedure("USP_ItemFillGrid", new string[] { "PageSize", "PageNumber" }, new string[] { pageSize.ToString(), currentPage.ToString() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVitemdetails.DataSource = ds.Tables[0];
                    GVitemdetails.DataBind();
                    //GVitemdetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //GVitemdetails.UseAccessibleHeader = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds != null)
            {
                ds.Dispose();
            }
        }
    }
    protected int GetRowCount()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = objdb.ByProcedure("Usp_tblAdminVillageMst_GetRowsCount", new string[] { }, new string[] { }, "dataset");

            return Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            return 0;
        }
        finally
        {
            if (ds != null)
            {
                ds.Dispose();
            }
        }
    }
    protected void GVitemdetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //if (txtSearch.Text == "")
            //{
                GVitemdetails.PageIndex = e.NewPageIndex;
                GetPageData((e.NewPageIndex + 1), 100);
            //}
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    
    {
        try
        {
            if (txtSearch.Text != "")
            {
                DataSet ds1 = new DataSet();
                //ds1 = objdb.ByProcedure("USP_SearchItemGrid", new string[] { "Flag", "Search" }, new string[] { "2",txtSearch.Text }, "dataset");
                //GVitemdetails.PageSize =Convert.ToInt32(ds.Tables[0].Rows[0]["Item_id"].ToString());
                //GVitemdetails.DataBind();
                ds1 = objdb.ByProcedure("USP_SearchItemGrid", new string[] { "Flag","Search" }, new string[] { "1",txtSearch.Text }, "dataset");
                if (ds1 != null && ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        gvSearchItemDetails.DataSource = ds1.Tables[0];
                        gvSearchItemDetails.DataBind();
                        GVitemdetails.Visible = false;
                        gvSearchItemDetails.Visible = true;
                    }
                }
            }
            else
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
                
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}
