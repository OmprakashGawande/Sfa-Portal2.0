using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_FinSupplierOrderEntry : System.Web.UI.Page
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
                FillDropdown();
                FillCategory();
                //  FillDetail();
                // ddlGSTIncInDeduction.SelectedIndex = 1;
                //ddlCessApplicable.SelectedIndex = 1;
                //txtCessRate.Text = "0";
                //ddlGSTIncInDeduction.SelectedIndex = 1;
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Attributes.Add("readonly", "readonly");

                ViewState["OrderID"] = "0";
                CreateDataTable();
                btnSubmit.Visible = false;
                btnAdd.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void CreateDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn RowNo = dt.Columns.Add("RowNo", typeof(string));
        dt.Columns.Add(new DataColumn("Plant", typeof(string)));
        dt.Columns.Add(new DataColumn("PlantID", typeof(string)));
        dt.Columns.Add(new DataColumn("ItemCategoryID", typeof(string)));
        dt.Columns.Add(new DataColumn("Item", typeof(string)));
        dt.Columns.Add(new DataColumn("ItemID", typeof(string)));
        dt.Columns.Add(new DataColumn("Unit", typeof(string)));
        dt.Columns.Add(new DataColumn("RateInclusive", typeof(string)));
        dt.Columns.Add(new DataColumn("ItemQuantity", typeof(string)));
        dt.Columns.Add(new DataColumn("CessApplicable", typeof(string)));
        dt.Columns.Add(new DataColumn("CessRate", typeof(string)));
        dt.Columns.Add(new DataColumn("GSTApplicable", typeof(string)));
        dt.Columns.Add(new DataColumn("GSTRate", typeof(string)));
        dt.Columns.Add(new DataColumn("GSTType", typeof(string)));
        dt.Columns.Add(new DataColumn("GSTIncludeInDeduction", typeof(string)));
        RowNo.AutoIncrement = true;
        RowNo.AutoIncrementSeed = 1;
        RowNo.AutoIncrementStep = 1;
        ViewState["dt"] = dt;

        gvSupplier.DataSource = dt;
        gvSupplier.DataBind();
    }
    protected void FillDropdown()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinSupplierItem",
                                    new string[] { "flag" },
                                    new string[] { "6" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlSupplier.DataSource = ds;
                ddlSupplier.DataTextField = "NameOfAccountHolder";
                ddlSupplier.DataValueField = "ID";
                ddlSupplier.DataBind();
                ddlSupplier.Items.Insert(0, new ListItem("Select", "0"));



                ddlddlSupplierSearch.DataSource = ds;
                ddlddlSupplierSearch.DataTextField = "NameOfAccountHolder";
                ddlddlSupplierSearch.DataValueField = "ID";
                ddlddlSupplierSearch.DataBind();
                ddlddlSupplierSearch.Items.Insert(0, new ListItem("All", "0"));
            }

            //ds = null;
            //ds = objdb.ByProcedure("SpFinSupplierItem",
            //            new string[] { "flag" },
            //            new string[] { "7" }, "dataset");
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlItem.DataSource = ds;
            //    ddlItem.DataTextField = "ItemName";
            //    ddlItem.DataValueField = "ItemID";
            //    ddlItem.DataBind();
            //    ddlItem.Items.Insert(0, "Select");
            //}
            ds = null;
            ds = objdb.ByProcedure("SpFinSupplierItem",
                        new string[] { "flag" },
                        new string[] { "8" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlPlant.DataSource = ds;
                ddlPlant.DataTextField = "Office_Name";
                ddlPlant.DataValueField = "Office_ID";
                ddlPlant.DataBind();
                ddlPlant.Items.Insert(0, "Select");


                ddlPlantSearch.DataSource = ds;
                ddlPlantSearch.DataTextField = "Office_Name";
                ddlPlantSearch.DataValueField = "Office_ID";
                ddlPlantSearch.DataBind();
                ddlPlantSearch.Items.Insert(0, new ListItem("All", "0"));
               // ddlPlantSearch.Items.Insert(0, "Select");
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
            btnSubmit.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpFinSupplierOrderEntry", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
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
    //protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        txtGSTType.Text = "";
    //        if (ddlSupplier.SelectedIndex > 0)
    //        {
    //            ds = objdb.ByProcedure("SpFinSupplier",
    //                                new string[] { "flag", "ID" },
    //                                new string[] { "7", ddlSupplier.SelectedValue.ToString() }, "dataset");
    //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //            {
    //                if (ds.Tables[0].Rows[0]["StateID"].ToString() == "23")
    //                {
    //                    //divIGST.Visible = false;
    //                    txtGSTType.Text = "Intra State Supply";

    //                }
    //                else
    //                {
    //                    // divCGST.Visible = false;
    //                    txtGSTType.Text = "Inter State Supply";
    //                }
    //            }
    //        }



    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    //protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //       // lblUnit.Text = "";
    //       // txtItemRate.Text = "0.00";
    //        if (ddlItem.SelectedIndex > 0)
    //        {
    //            ds = objdb.ByProcedure("SpFinSupplierItem",
    //                                new string[] { "flag", "ItemID" },
    //                                new string[] { "9", ddlItem.SelectedValue.ToString() }, "dataset");
    //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //            {
    //                lblUnit.Text = ds.Tables[0].Rows[0]["ItemUnit"].ToString();
    //                txtItemRate.Text = ds.Tables[0].Rows[0]["ItemRate"].ToString();
    //            }
    //        }


    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateBasicRate();", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    //protected void ddlGSTApplicable_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        divGSTRate.Visible = true;
    //        if (ddlGSTApplicable.SelectedIndex > 0)
    //        {
    //            ddlGSTRate.ClearSelection();
    //            divGSTRate.Visible = false;
    //        }
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateBasicRate();", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    //protected void ddlCessApplicable_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        txtCessRate.Enabled = true;
    //        txtCessRate.Text = "400.00";
    //        if (ddlCessApplicable.SelectedIndex > 0)
    //        {
    //            txtCessRate.Text = "0.00";
    //            txtCessRate.Enabled = false;
    //        }
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateBasicRate();", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void ClearText()
    {
        ddlPlant.ClearSelection();
        gvItemList.DataSource = null;
        gvItemList.DataBind();
        btnAdd.Visible = false;
        //ddlItem.ClearSelection();
        //lblUnit.Text = "";
        //txtItemRate.Text = "";
        //txtQuantity.Text = "";
        //ddlGSTApplicable.ClearSelection();
        //ddlGSTRate.ClearSelection();
        //divGSTRate.Visible = true;
    }
    protected void Clear()
    {
        txtOrderNo.Text = "";
        txtOrderDate.Text = "";
        ddlSupplier.ClearSelection();
        ddlPlant.ClearSelection();
        CreateDataTable();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlPlant.SelectedIndex <= 0)
            {
                msg = msg + "Select Plant. \\n";
            }
            if (ddlItemCategory.SelectedIndex <= 0)
            {
                msg = msg + "Select Item Category. \\n";
            }
            if (msg == "")
            {
                int msg1 = 0;
                DataTable dt = (DataTable)ViewState["dt"];
                if (gvSupplier.Rows.Count > 0)
                {
                    foreach (GridViewRow row in gvSupplier.Rows)
                    {
                        Label lblPlantID = (Label)row.FindControl("lblPlantID");
                        Label lblItemID = (Label)row.FindControl("lblItemID");
                        //if (lblPlantID.Text == ddlPlant.SelectedValue.ToString() && lblItemID.Text == ddlItem.SelectedValue.ToString())
                        //{
                        //    msg1 = 1;
                        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "alert('" + ddlItem.SelectedItem.Text + " is already in the list for " + ddlPlant.SelectedItem.Text + ".');", true);
                        //    break;

                        //}
                    }
                }
                if (msg1 == 0)
                {
                    string Quantity, Rate;
                    foreach (GridViewRow row in gvItemList.Rows)
                    {
                        CheckBox chk = (CheckBox)row.FindControl("chk");
                        Label lblItemID = (Label)row.FindControl("lblItemID");
                        Label lblUnit = (Label)row.FindControl("lblUnit");
                        Label lblItem = (Label)row.FindControl("lblItem");
                        Label lblCessApplicable = (Label)row.FindControl("lblCessApplicable");
                        Label lblCessRate = (Label)row.FindControl("lblCessRate");
                        Label lblGSTApplicable = (Label)row.FindControl("lblGSTApplicable");
                        Label lblGSTRate = (Label)row.FindControl("lblGSTRate");
                        Label lblGSTType = (Label)row.FindControl("lblGSTType");
                        Label lblGSTIncInDeduction = (Label)row.FindControl("lblGSTIncInDeduction");
                        TextBox txtItemRateInclusive = (TextBox)row.FindControl("txtItemRateInclusive");
                        TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                        if (chk.Checked)
                        {
                            if (txtQuantity.Text == "")
                            {
                                Quantity = "0.00";
                            }
                            else
                            {
                                Quantity = txtQuantity.Text;
                            }
                            if (txtQuantity.Text == "")
                            {
                                Rate = "0.00";
                            }
                            else
                            {
                                Rate = txtItemRateInclusive.Text;
                            }
                            dt.Rows.Add(null, ddlPlant.SelectedItem.Text, ddlPlant.SelectedValue.ToString(), ddlItemCategory.SelectedValue.ToString(), lblItem.Text, lblItemID.Text, lblUnit.Text, Convert.ToDecimal(Rate).ToString("0.00"), Convert.ToDecimal(Quantity).ToString("0.00")
                                , lblCessApplicable.Text, lblCessRate.Text, lblGSTApplicable.Text, lblGSTRate.Text, lblGSTType.Text, lblGSTIncInDeduction.Text);
                        }
                    }

                    ViewState["dt"] = dt;

                    gvSupplier.DataSource = dt;
                    gvSupplier.DataBind();
                    if (gvSupplier.Rows.Count > 0)
                    {
                        btnSubmit.Visible = true;
                    }
                    gvItemList.DataSource = null;
                    gvItemList.DataBind();
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

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string GSTType = "";
            if (txtOrderNo.Text == "")
            {
                msg = msg + "Enter Order No. \\n";
            }
            if (txtOrderDate.Text == "")
            {
                msg = msg + "Enter Order Date. \\n";
            }
            if (ddlSupplier.SelectedIndex <= 0)
            {
                msg = msg + "Select Supplier Name. \\n";
            }
            if (msg == "")
            {
                DataTable dt = (DataTable)ViewState["dt"];
                if (btnSubmit.Text == "Final Submit")
                {
                    if (ddlSupplier.SelectedValue.ToString() == "23")
                    {
                        GSTType = "Intra State Supply";

                    }
                    else
                    {
                        GSTType = "Inter State Supply";
                    }
                    if (gvSupplier.Rows.Count > 0)
                    {
                        ds = objdb.ByProcedure("SpFinSupplierOrderEntry", new string[] { "flag", "OrderNo" }, new string[] { "1", txtOrderNo.Text }, "dataset");
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            foreach (GridViewRow row in gvSupplier.Rows)
                            {
                                Label lblPlantID = (Label)row.FindControl("lblPlantID");
                                Label lblPlant = (Label)row.FindControl("lblPlant");
                                Label lblItemCategoryID = (Label)row.FindControl("lblItemCategoryID");
                                Label lblItemID = (Label)row.FindControl("lblItemID");
                                Label lblItem = (Label)row.FindControl("lblItem");
                                Label lblUnit = (Label)row.FindControl("lblUnit");
                                Label lblCessApplicable = (Label)row.FindControl("lblCessApplicable");
                                Label lblCessRate = (Label)row.FindControl("lblCessRate");
                                Label lblGSTApplicable = (Label)row.FindControl("lblGSTApplicable");
                                Label lblGSTRate = (Label)row.FindControl("lblGSTRate");
                                Label lblGSTType = (Label)row.FindControl("lblGSTType");
                                Label lblGSTIncInDeduction = (Label)row.FindControl("lblGSTIncInDeduction");
                                Label lblRateInclusive = (Label)row.FindControl("lblRateInclusive");
                                Label lblItemQuantity = (Label)row.FindControl("lblItemQuantity");

                                ds = objdb.ByProcedure("SpFinSupplierOrderEntry",
                      new string[] { "flag", "OrderNo", "OrderDate", "SupplierID", "SupplierName"
                        , "PlantID", "PlantName","ItemCategoryID", "ItemID", "ItemName", "Unit"
                        , "ItemRate", "Quantity","CessApplicable","CessRate","GSTApplicable","GSTRate","GSTType","GSTIncInDeduction", "UpdatedBy"},
                      new string[] { "0", txtOrderNo.Text, Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy/MM/dd"), ddlSupplier.SelectedValue.ToString(), ddlSupplier.SelectedItem.ToString()
                        , Convert.ToInt32(lblPlantID.Text).ToString(), lblPlant.Text, lblItemCategoryID.Text, Convert.ToInt32(lblItemID.Text).ToString(), lblItem.Text, lblUnit.Text
                        , Convert.ToDecimal(lblRateInclusive.Text).ToString("0.00"), Convert.ToDecimal(lblItemQuantity.Text).ToString(), lblCessApplicable.Text,lblCessRate.Text,lblGSTApplicable.Text,lblGSTRate.Text,GSTType, lblGSTIncInDeduction.Text, ViewState["Emp_ID"].ToString()}, "dataset");
                            }
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Data Successfully Saved.");
                                btnSubmit.Text = "Final Submit";
                            }
                            Clear();
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Order No " + ds.Tables[0].Rows[0]["OrderNo"].ToString() + " is already exists.');", true);
                        }

                    }
                }
                else
                {
                    if (gvSupplier.Rows.Count > 0)
                    {
                        ds = objdb.ByProcedure("SpFinSupplierOrderEntry", new string[] { "flag", "OrderNo" }, new string[] { "3", txtOrderNo.Text }, "dataset");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (GridViewRow row in gvSupplier.Rows)
                            {
                                Label lblPlantID = (Label)row.FindControl("lblPlantID");
                                Label lblPlant = (Label)row.FindControl("lblPlant");
                                Label lblItemCategoryID = (Label)row.FindControl("lblItemCategoryID");
                                Label lblItemID = (Label)row.FindControl("lblItemID");
                                Label lblItem = (Label)row.FindControl("lblItem");
                                Label lblUnit = (Label)row.FindControl("lblUnit");
                                Label lblCessApplicable = (Label)row.FindControl("lblCessApplicable");
                                Label lblCessRate = (Label)row.FindControl("lblCessRate");
                                Label lblGSTApplicable = (Label)row.FindControl("lblGSTApplicable");
                                Label lblGSTRate = (Label)row.FindControl("lblGSTRate");
                                Label lblGSTType = (Label)row.FindControl("lblGSTType");
                                Label lblGSTIncInDeduction = (Label)row.FindControl("lblGSTIncInDeduction");
                                Label lblRateInclusive = (Label)row.FindControl("lblRateInclusive");
                                Label lblItemQuantity = (Label)row.FindControl("lblItemQuantity");


                                ds = objdb.ByProcedure("SpFinSupplierOrderEntry",
                      new string[] { "flag", "OrderNo", "OrderDate", "SupplierID", "SupplierName"
                        , "PlantID", "PlantName","ItemCategoryID", "ItemID", "ItemName", "Unit"
                        , "ItemRate", "Quantity","CessApplicable","CessRate","GSTApplicable","GSTRate","GSTType","GSTIncInDeduction", "UpdatedBy"},
                      new string[] { "0", txtOrderNo.Text, Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy/MM/dd"), ddlSupplier.SelectedValue.ToString(), ddlSupplier.SelectedItem.ToString()
                        , Convert.ToInt32(lblPlantID.Text).ToString(), lblPlant.Text, lblItemCategoryID.Text, Convert.ToInt32(lblItemID.Text).ToString(), lblItem.Text, lblUnit.Text
                        , Convert.ToDecimal(lblRateInclusive.Text).ToString("0.00"), Convert.ToDecimal(lblItemQuantity.Text).ToString(), lblCessApplicable.Text,lblCessRate.Text,lblGSTApplicable.Text,lblGSTRate.Text,GSTType, lblGSTIncInDeduction.Text, ViewState["Emp_ID"].ToString()}, "dataset");
                            }
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Data Updated Successfully.");
                                btnSubmit.Text = "Final Submit";
                            }
                            Clear();
                        }
                    }
                }
                // FillDetail();
                Clear();
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
    protected void gvSupplier_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)ViewState["dt"];
            string ID = gvSupplier.DataKeys[e.RowIndex].Value.ToString();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["RowNo"].ToString() == ID)
                {
                    dt.Rows.Remove(dr);
                    break;
                }
            }
            dt.AcceptChanges();
            ViewState["Farmtable"] = dt;

            gvSupplier.DataSource = dt;
            gvSupplier.DataBind();
            if (gvSupplier.Rows.Count > 0)
            {
                btnSubmit.Visible = true;
            }
            else
            {
                btnSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblOrderNo.Text ="";
            lblOrderDate.Text = "";
            lblSupplierName.Text = "";
            string OrderNo = e.CommandArgument.ToString();
            if (e.CommandName == "ViewRecord")
            {
                GvItemDetail.DataSource = null;
                GvItemDetail.DataBind();
                ds = objdb.ByProcedure("SpFinSupplierOrderEntry", new string[] { "flag", "OrderNo" }, new string[] { "1", OrderNo }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    lblOrderNo.Text = ds.Tables[0].Rows[0]["OrderNo"].ToString();
                    lblOrderDate.Text = ds.Tables[0].Rows[0]["OrderDate"].ToString();
                    lblSupplierName.Text = ds.Tables[0].Rows[0]["SupplierName"].ToString();

                    GvItemDetail.DataSource = ds.Tables[0];
                    GvItemDetail.DataBind();
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal();", true);
            }
            if (e.CommandName == "EditRecord")
            {
                CreateDataTable();
                ds = objdb.ByProcedure("SpFinSupplierOrderEntry", new string[] { "flag", "OrderNo" }, new string[] { "1", OrderNo }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtOrderNo.Text = ds.Tables[0].Rows[0]["OrderNo"].ToString();
                    txtOrderDate.Text = ds.Tables[0].Rows[0]["OrderDate"].ToString();
                    ddlSupplier.ClearSelection();
                    ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["SupplierID"].ToString()).Selected = true;
                    //txtGSTType.Text = ds.Tables[0].Rows[0]["GSTType"].ToString();
                    DataTable dt = (DataTable)ViewState["dt"];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string RowNo = ds.Tables[0].Rows[i]["RowNo"].ToString();
                        string PlantName = ds.Tables[0].Rows[i]["PlantName"].ToString();
                        string PlantID = ds.Tables[0].Rows[i]["PlantID"].ToString();
                        string ItemCategoryID = ds.Tables[0].Rows[i]["ItemCategoryID"].ToString();
                        string ItemName = ds.Tables[0].Rows[i]["ItemName"].ToString();
                        string ItemID = ds.Tables[0].Rows[i]["ItemID"].ToString();
                        string Unit = ds.Tables[0].Rows[i]["Unit"].ToString();
                        string CessApplicable = ds.Tables[0].Rows[i]["CessApplicable"].ToString();
                        string CessRate = ds.Tables[0].Rows[i]["CessRate"].ToString();
                        string GSTApplicable = ds.Tables[0].Rows[i]["GSTApplicable"].ToString();
                        string GSTRate = ds.Tables[0].Rows[i]["GSTRate"].ToString();
                        string GSTType = ds.Tables[0].Rows[i]["GSTType"].ToString();
                        string GSTIncludeInDeduction = ds.Tables[0].Rows[i]["GSTIncInDeduction"].ToString();
                        string ItemRate = ds.Tables[0].Rows[i]["ItemRate"].ToString();
                        string Quantity = ds.Tables[0].Rows[i]["Quantity"].ToString();


                        dt.Rows.Add(RowNo, PlantName, PlantID, ItemCategoryID, ItemName, ItemID, Unit, Convert.ToDecimal(ItemRate).ToString("0.00"), Convert.ToDecimal(Quantity).ToString("0.00"), CessApplicable, Convert.ToDecimal(CessRate).ToString("0.00"), GSTApplicable, Convert.ToDecimal(GSTRate).ToString("0.00"), GSTType, GSTIncludeInDeduction);
                    }

                    ViewState["dt"] = dt;

                    gvSupplier.DataSource = dt;
                    gvSupplier.DataBind();
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        if (ds.Tables[1].Rows[0]["Status"].ToString() == "True")
                        {
                            gvSupplier.Columns[11].Visible = false;
                        }
                        else
                        {
                            gvSupplier.Columns[11].Visible = true;
                        }
                    }
                    if (gvSupplier.Rows.Count > 0)
                    {
                        btnSubmit.Visible = true;
                    }

                    btnSubmit.Text = "Update";
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnGetItemList_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlPlant.SelectedIndex <= 0)
            {
                msg = msg + "Select Plant. \\n";
            }
            if (ddlItemCategory.SelectedIndex <= 0)
            {
                msg = msg + "Select Item Category. \\n";
            }
            if (msg == "")
            {
                btnAdd.Visible = false;
                gvItemList.DataSource = null;
                gvItemList.DataBind();
                ds = objdb.ByProcedure("SpFinSupplierItem", new string[] { "flag", "ItemCategory" }, new string[] { "10", ddlItemCategory.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnAdd.Visible = true;
                    gvItemList.DataSource = ds;
                    gvItemList.DataBind();
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            //if (ddlPlantSearch.SelectedIndex <= 0)
            //{
            //    msg = msg + "Select Plant. \\n";
            //}
            //if (ddlddlSupplierSearch.SelectedIndex <= 0)
            //{
            //    msg = msg + "Select Supplier Name. \\n";
            //}
            if (txtFromDate.Text == "")
            {
                msg = msg + "Select From Date. \\n";
            }
            if (txtToDate.Text == "")
            {
                msg = msg + "Select To Date. \\n";
            }
            
            if (msg == "")
            {
             
                ds = objdb.ByProcedure("SpFinSupplierOrderEntry",
                     new string[] { "flag", "PlantID", "SupplierID","FromDate","ToDate" },
                     new string[] { "10", ddlPlantSearch.SelectedValue.ToString(), ddlddlSupplierSearch.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
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
}