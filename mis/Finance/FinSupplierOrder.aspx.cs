using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Finance_FinSupplierOrder : System.Web.UI.Page
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
                txtOrderDate.Attributes.Add("readonly", "readonly");
                ddlSupplier.Enabled = false;
                FillDropdown();
                ViewState["OrderID"] = "0";
                if (Request.QueryString["Action"] != null)
                {
                    ViewState["OrderID"] = objdb.Decrypt(Request.QueryString["Action"].ToString());
                    DataSet dsEdit = objdb.ByProcedure("SpFinSupplierOrder", new string[] { "flag", "OrderID" }, new string[] { "19", ViewState["OrderID"].ToString() }, "dataset");

                    if (dsEdit.Tables[0].Rows.Count > 0)
                    {

                        ddlOrderNo.SelectedValue = dsEdit.Tables[0].Rows[0]["OrderNo"].ToString();
                        ddlOrderNo_SelectedIndexChanged(sender, e);
                        txtOrderDate.Text = dsEdit.Tables[0].Rows[0]["OrderDate"].ToString();
                        ddlSupplier.ClearSelection();
                        ddlSupplier.Items.FindByValue(dsEdit.Tables[0].Rows[0]["SupplierID"].ToString()).Selected = true;
                        ddlPlant.ClearSelection();
                        ddlPlant.Items.FindByValue(dsEdit.Tables[0].Rows[0]["PlantID"].ToString()).Selected = true;
                        ddlPlant_SelectedIndexChanged(sender, e);
                        ddlItemCategory.ClearSelection();
                        ddlItemCategory.Items.FindByValue(dsEdit.Tables[0].Rows[0]["ItemCategoryID"].ToString()).Selected = true;
                        txtBillNo.Text = dsEdit.Tables[0].Rows[0]["BillNo"].ToString();
                        txtBillDate.Text = dsEdit.Tables[0].Rows[0]["BillDate"].ToString();
                        txtQualityReceiveDate.Text = dsEdit.Tables[0].Rows[0]["QuantityReceiveDate"].ToString();
                        gvItemDetail.DataSource = dsEdit;
                        gvItemDetail.DataBind();
                        foreach (GridViewRow row in gvItemDetail.Rows)
                        {
                            TextBox lblRateInclusive = (TextBox)row.FindControl("lblRateInclusive");
                            TextBox lblItemQuantity = (TextBox)row.FindControl("lblItemQuantity");
                            TextBox lblCessApplicable = (TextBox)row.FindControl("lblCessApplicable");
                            TextBox lblGSTApplicable = (TextBox)row.FindControl("lblGSTApplicable");
                            TextBox lblGSTRate = (TextBox)row.FindControl("lblGSTRate");
                            TextBox lblGSTInclude = (TextBox)row.FindControl("lblGSTInclude");
                            TextBox txtDIFFQuantity = (TextBox)row.FindControl("txtDIFFQuantity");
                            TextBox txtBasicRate = (TextBox)row.FindControl("txtBasicRate");
                            TextBox txtGSTRateAmt = (TextBox)row.FindControl("txtGSTRateAmt");
                            TextBox txtBasicAmount = (TextBox)row.FindControl("txtBasicAmount");
                            TextBox txtGSTAmount = (TextBox)row.FindControl("txtGSTAmount");
                            TextBox txtCess = (TextBox)row.FindControl("txtCess");
                            TextBox txtTotalAmount = (TextBox)row.FindControl("txtTotalAmount");
                            TextBox txtDeductionAmount = (TextBox)row.FindControl("txtDeductionAmount");
                            TextBox txtExtraSupDed = (TextBox)row.FindControl("txtExtraSupDed");
                            TextBox txtTDS = (TextBox)row.FindControl("txtTDS");
                            TextBox txtNetPayment = (TextBox)row.FindControl("txtNetPayment");
                            TextBox lblCessRate = (TextBox)row.FindControl("lblCessRate");
                            lblRateInclusive.Attributes.Add("readonly", "readonly");
                            lblItemQuantity.Attributes.Add("readonly", "readonly");
                            lblCessApplicable.Attributes.Add("readonly", "readonly");
                            lblGSTApplicable.Attributes.Add("readonly", "readonly");
                            lblGSTRate.Attributes.Add("readonly", "readonly");
                            lblGSTInclude.Attributes.Add("readonly", "readonly");
                            txtDIFFQuantity.Attributes.Add("readonly", "readonly");
                            txtBasicRate.Attributes.Add("readonly", "readonly");
                            txtGSTRateAmt.Attributes.Add("readonly", "readonly");
                            txtBasicAmount.Attributes.Add("readonly", "readonly");
                            txtGSTAmount.Attributes.Add("readonly", "readonly");
                            txtCess.Attributes.Add("readonly", "readonly");
                            txtTotalAmount.Attributes.Add("readonly", "readonly");
                            txtDeductionAmount.Attributes.Add("readonly", "readonly");
                            txtExtraSupDed.Attributes.Add("readonly", "readonly");
                            txtTDS.Attributes.Add("readonly", "readonly");
                            txtNetPayment.Attributes.Add("readonly", "readonly");
                            lblCessRate.Attributes.Add("readonly", "readonly");
                        }
                        btnSave.Text = "Update";
                    }
                    else
                    {
                        btnSave.Text = "Save";
                        Response.Redirect("FinSupplierOrder.aspx");

                    }
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
            }

            ds = null;
            ds = objdb.ByProcedure("SpFinSupplierOrderEntry",
                        new string[] { "flag" },
                        new string[] { "4" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOrderNo.DataSource = ds;
                ddlOrderNo.DataTextField = "OrderNo";
                ddlOrderNo.DataValueField = "OrderNo";
                ddlOrderNo.DataBind();
                ddlOrderNo.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDetail()
    {
        try
        {

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
            if (ddlOrderNo.SelectedIndex == 0)
            {
                msg = msg + "Select Order No. \\n";
            }
            if (txtOrderDate.Text == "")
            {
                msg = msg + "Enter Order Date. \\n";
            }
            if (ddlSupplier.SelectedIndex <= 0)
            {
                msg = msg + "Select Supplier Name. \\n";
            }
            if (ddlPlant.SelectedIndex <= 0)
            {
                msg = msg + "Select Plant. \\n";
            }
            if (ddlItemCategory.SelectedIndex <= 0)
            {
                msg = msg + "Select Item Category. \\n";
            }
            if (txtBillNo.Text == "")
            {
                msg = msg + "Enter Bill No. \\n";
            }
            if (txtBillDate.Text == "")
            {
                msg = msg + "Enter Bill Date. \\n";
            }
            if (txtQualityReceiveDate.Text == "")
            {
                msg = msg + "Enter Quantity  Receive Date. \\n";
            }
            if (msg.Trim() == "")
            {
                string IGST = "0.00"; string CGST = "0.00"; string SGST = "0.00", chkExtraSupDeduction = "";
                if (btnSave.Text != "Update")
                {
                    if (gvItemDetail.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvItemDetail.Rows)
                        {
                            CheckBox chk = (CheckBox)row.FindControl("chk");
                            if (chk.Checked)
                            {
                                Label lblItemID = (Label)row.FindControl("lblItemID");
                                Label lblItem = (Label)row.FindControl("lblItem");
                                Label lblUnit = (Label)row.FindControl("lblUnit");
                                TextBox lblRateInclusive = (TextBox)row.FindControl("lblRateInclusive");
                                TextBox lblItemQuantity = (TextBox)row.FindControl("lblItemQuantity");
                                TextBox lblCessApplicable = (TextBox)row.FindControl("lblCessApplicable");
                                TextBox lblCessRate = (TextBox)row.FindControl("lblCessRate");
                                TextBox lblGSTApplicable = (TextBox)row.FindControl("lblGSTApplicable");
                                TextBox lblGSTRate = (TextBox)row.FindControl("lblGSTRate");
                                Label lblGSTType = (Label)row.FindControl("lblGSTType");
                                TextBox lblGSTInclude = (TextBox)row.FindControl("lblGSTInclude");
                                TextBox txtQualityReceive = (TextBox)row.FindControl("txtQualityReceive");
                                TextBox txtDIFFQuantity = (TextBox)row.FindControl("txtDIFFQuantity");
                                TextBox txtBasicRate = (TextBox)row.FindControl("txtBasicRate");
                                TextBox txtGSTRateAmt = (TextBox)row.FindControl("txtGSTRateAmt");
                                TextBox txtBasicAmount = (TextBox)row.FindControl("txtBasicAmount");
                                TextBox txtGSTAmount = (TextBox)row.FindControl("txtGSTAmount");
                                TextBox txtCess = (TextBox)row.FindControl("txtCess");
                                TextBox txtTotalAmount = (TextBox)row.FindControl("txtTotalAmount");
                                TextBox txtDeductionProposed = (TextBox)row.FindControl("txtDeductionProposed");
                                TextBox txtDeductionAmount = (TextBox)row.FindControl("txtDeductionAmount");
                                HiddenField HF_BasicTotalAmount = (HiddenField)row.FindControl("HF_BasicTotalAmount");
                                CheckBox chkExtraSupDed = (CheckBox)row.FindControl("chkExtraSupDed");
                                TextBox txtExtraSupDed = (TextBox)row.FindControl("txtExtraSupDed");
                                TextBox txtTDSPer = (TextBox)row.FindControl("txtTDSPer");
                                TextBox txtTDS = (TextBox)row.FindControl("txtTDS");
                                TextBox txtTCS = (TextBox)row.FindControl("txtTCS");
                                TextBox txtRoundOff = (TextBox)row.FindControl("txtRoundOff");
                                TextBox txtNetPayment = (TextBox)row.FindControl("txtNetPayment");
                                TextBox txtQualityCleanReport = (TextBox)row.FindControl("txtQualityCleanReport");
                                TextBox txtNABLReport = (TextBox)row.FindControl("txtNABLReport");
                                TextBox txtSupplierInHose = (TextBox)row.FindControl("txtSupplierInHose");
                                TextBox txtTollKanta = (TextBox)row.FindControl("txtTollKanta");
                                TextBox txtEWayBill = (TextBox)row.FindControl("txtEWayBill");
                                TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                                if (chkExtraSupDed.Checked)
                                {
                                    chkExtraSupDeduction = "Yes";
                                }
                                else
                                {
                                    chkExtraSupDeduction = "No";
                                }
                                if (lblGSTType.Text == "Intra State Supply")
                                {
                                    decimal amt = decimal.Parse(txtGSTAmount.Text) / 2;
                                    CGST = amt.ToString();
                                    SGST = amt.ToString();
                                }
                                else
                                {
                                    if (txtGSTAmount.Text == "")
                                    {
                                        IGST = "0.00";
                                    }

                                }
                                string BasicTotalAmount = HF_BasicTotalAmount.Value;

                                ds = objdb.ByProcedure("SpFinSupplierOrder",
                          new string[] { "flag", "OrderNo", "OrderDate", "SupplierID", "SupplierName"
                                            , "PlantID", "PlantName","ItemCategoryID", "ItemID", "ItemName", "Unit"
                                            , "ItemRate", "Quantity", "CessApplicable", "CessRate", "GSTApplicable", "GSTRate","GSTType"
                                            , "QualityCleanReport", "BillNo", "BillDate", "QuantityReceiveDate", "QuantityReceive"
                                            , "DiffQuantity","BasicRate","GST","BasicAmount","GSTAmount", "Cess","TotalAmount", "DeductionProposed","DeductionAmount","TCS", "RoundOff","NetPayment", "Remark", "NABLReport","SupplierInHouseReport","TollKantaSlip","EWayBill"
                                            , "IGST", "CGST", "SGST" , "TDS","GSTIncInDeduction","BasicTotalAmount", "UpdatedBy","TDSPer","ExtraSupDedCHK","ExtraSupDed"},
                          new string[] { "0", ddlOrderNo.SelectedItem.Text, Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy/MM/dd"), ddlSupplier.SelectedValue.ToString(), ddlSupplier.SelectedItem.ToString()
                                            , ddlPlant.SelectedValue.ToString(), ddlPlant.SelectedItem.ToString(), ddlItemCategory.SelectedValue, lblItemID.Text, lblItem.Text, lblUnit.Text
                                            , lblRateInclusive.Text, lblItemQuantity.Text,lblCessApplicable.Text,lblCessRate.Text ,lblGSTApplicable.Text, lblGSTRate.Text,lblGSTType.Text
                                            ,  txtQualityCleanReport.Text, txtBillNo.Text,  Convert.ToDateTime(txtBillDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtQualityReceiveDate.Text, cult).ToString("yyyy/MM/dd")
                                            , txtQualityReceive.Text , txtDIFFQuantity.Text,txtBasicRate.Text,txtGSTRateAmt.Text,txtBasicAmount.Text,txtGSTAmount.Text,txtCess.Text,txtTotalAmount.Text, txtDeductionProposed.Text,txtDeductionAmount.Text,txtTCS.Text,txtRoundOff.Text, txtNetPayment.Text, txtRemark.Text, txtNABLReport.Text, txtSupplierInHose.Text, txtTollKanta.Text, txtEWayBill.Text
                                            , IGST, CGST, SGST , txtTDS.Text,lblGSTInclude.Text,BasicTotalAmount, ViewState["Emp_ID"].ToString(),txtTDSPer.Text,chkExtraSupDeduction,txtExtraSupDed.Text}, "dataset");
                            }
                        }
                    }
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Data Successfully Saved.");
                    ClearText();
                }
                else
                {
                    if (gvItemDetail.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvItemDetail.Rows)
                        {
                            CheckBox chk = (CheckBox)row.FindControl("chk");
                            Label lblItemID = (Label)row.FindControl("lblItemID");
                            Label lblItem = (Label)row.FindControl("lblItem");
                            Label lblUnit = (Label)row.FindControl("lblUnit");
                            TextBox lblRateInclusive = (TextBox)row.FindControl("lblRateInclusive");
                            TextBox lblItemQuantity = (TextBox)row.FindControl("lblItemQuantity");
                            TextBox lblCessApplicable = (TextBox)row.FindControl("lblCessApplicable");
                            TextBox lblCessRate = (TextBox)row.FindControl("lblCessRate");
                            TextBox lblGSTApplicable = (TextBox)row.FindControl("lblGSTApplicable");
                            TextBox lblGSTRate = (TextBox)row.FindControl("lblGSTRate");
                            Label lblGSTType = (Label)row.FindControl("lblGSTType");
                            TextBox lblGSTInclude = (TextBox)row.FindControl("lblGSTInclude");
                            TextBox txtQualityReceive = (TextBox)row.FindControl("txtQualityReceive");
                            TextBox txtDIFFQuantity = (TextBox)row.FindControl("txtDIFFQuantity");
                            TextBox txtBasicRate = (TextBox)row.FindControl("txtBasicRate");
                            TextBox txtGSTRateAmt = (TextBox)row.FindControl("txtGSTRateAmt");
                            TextBox txtBasicAmount = (TextBox)row.FindControl("txtBasicAmount");
                            TextBox txtGSTAmount = (TextBox)row.FindControl("txtGSTAmount");
                            TextBox txtCess = (TextBox)row.FindControl("txtCess");
                            TextBox txtTotalAmount = (TextBox)row.FindControl("txtTotalAmount");
                            TextBox txtDeductionProposed = (TextBox)row.FindControl("txtDeductionProposed");
                            TextBox txtDeductionAmount = (TextBox)row.FindControl("txtDeductionAmount");
                            HiddenField HF_BasicTotalAmount = (HiddenField)row.FindControl("HF_BasicTotalAmount");
                            CheckBox chkExtraSupDed = (CheckBox)row.FindControl("chkExtraSupDed");
                            TextBox txtExtraSupDed = (TextBox)row.FindControl("txtExtraSupDed");
                            TextBox txtTDSPer = (TextBox)row.FindControl("txtTDSPer");
                            TextBox txtTDS = (TextBox)row.FindControl("txtTDS");
                            TextBox txtTCS = (TextBox)row.FindControl("txtTCS");
                            TextBox txtRoundOff = (TextBox)row.FindControl("txtRoundOff");
                            TextBox txtNetPayment = (TextBox)row.FindControl("txtNetPayment");
                            TextBox txtQualityCleanReport = (TextBox)row.FindControl("txtQualityCleanReport");
                            TextBox txtNABLReport = (TextBox)row.FindControl("txtNABLReport");
                            TextBox txtSupplierInHose = (TextBox)row.FindControl("txtSupplierInHose");
                            TextBox txtTollKanta = (TextBox)row.FindControl("txtTollKanta");
                            TextBox txtEWayBill = (TextBox)row.FindControl("txtEWayBill");
                            TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                            if (chkExtraSupDed.Checked)
                            {
                                chkExtraSupDeduction = "Yes";
                            }
                            else
                            {
                                chkExtraSupDeduction = "No";
                            }
                            if (lblGSTType.Text == "Intra State Supply")
                            {
                                decimal amt = decimal.Parse(txtGSTAmount.Text) / 2;
                                CGST = amt.ToString();
                                SGST = amt.ToString();
                            }
                            else
                            {
                                IGST = txtGSTAmount.Text;
                            }
                            string BasicTotalAmount = HF_BasicTotalAmount.Value;
                            if (chk.Checked)
                            {
                                ds = objdb.ByProcedure("SpFinSupplierOrder",
            new string[] { "flag", "OrderID","OrderNo", "OrderDate", "SupplierID", "SupplierName"
                                            , "PlantID", "PlantName","ItemCategoryID", "ItemID", "ItemName", "Unit"
                                            , "ItemRate", "Quantity", "CessApplicable", "CessRate", "GSTApplicable", "GSTRate","GSTType"
                                            , "QualityCleanReport", "BillNo", "BillDate", "QuantityReceiveDate", "QuantityReceive"
                                            , "DiffQuantity","BasicRate","GST","BasicAmount","GSTAmount", "Cess","TotalAmount", "DeductionProposed","DeductionAmount","TCS", "RoundOff","NetPayment", "Remark", "NABLReport","SupplierInHouseReport","TollKantaSlip","EWayBill"
                                            , "IGST", "CGST", "SGST" , "TDS","GSTIncInDeduction","BasicTotalAmount", "UpdatedBy","TDSPer","ExtraSupDedCHK","ExtraSupDed"},
            new string[] { "20",ViewState["OrderID"].ToString(), ddlOrderNo.SelectedItem.Text, Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy/MM/dd"), ddlSupplier.SelectedValue.ToString(), ddlSupplier.SelectedItem.ToString()
                                            , ddlPlant.SelectedValue.ToString(), ddlPlant.SelectedItem.ToString(), ddlItemCategory.SelectedValue, lblItemID.Text, lblItem.Text, lblUnit.Text
                                            , lblRateInclusive.Text, lblItemQuantity.Text,lblCessApplicable.Text,lblCessRate.Text ,lblGSTApplicable.Text, lblGSTRate.Text,lblGSTType.Text
                                            ,  txtQualityCleanReport.Text, txtBillNo.Text,  Convert.ToDateTime(txtBillDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtQualityReceiveDate.Text, cult).ToString("yyyy/MM/dd")
                                            , txtQualityReceive.Text , txtDIFFQuantity.Text,txtBasicRate.Text,txtGSTRateAmt.Text,txtBasicAmount.Text,txtGSTAmount.Text,txtCess.Text,txtTotalAmount.Text, txtDeductionProposed.Text,txtDeductionAmount.Text,txtTCS.Text,txtRoundOff.Text, txtNetPayment.Text, txtRemark.Text, txtNABLReport.Text, txtSupplierInHose.Text, txtTollKanta.Text, txtEWayBill.Text
                                            , IGST, CGST, SGST , txtTDS.Text,lblGSTInclude.Text,BasicTotalAmount, ViewState["Emp_ID"].ToString(),txtTDSPer.Text,chkExtraSupDeduction,txtExtraSupDed.Text}, "dataset");
                            }
                        }
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Data Successfully Updated.");
                        ClearText();

                    }
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
    protected void ClearText()
    {
        ddlOrderNo.ClearSelection();
        txtOrderDate.Text = "";
        ddlSupplier.ClearSelection();
        ddlPlant.ClearSelection();
        ddlItemCategory.ClearSelection();
        txtBillNo.Text = "";
        txtBillDate.Text = "";
        txtQualityReceiveDate.Text = "";
        btnSave.Text = "Save";
        gvItemDetail.DataSource = null;
        gvItemDetail.DataBind();
    }
    protected void ddlOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ds = objdb.ByProcedure("SpFinSupplierOrderEntry", new string[] { "flag", "OrderNo" }, new string[] { "1", ddlOrderNo.SelectedValue }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            txtOrderDate.Text = ds.Tables[0].Rows[0]["OrderDate"].ToString();
            ddlSupplier.ClearSelection();
            ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["SupplierID"].ToString()).Selected = true;
        }
        ddlPlant.ClearSelection();
        DataSet dsPlant = objdb.ByProcedure("SpFinSupplierOrderEntry", new string[] { "flag", "OrderNo" }, new string[] { "6", ddlOrderNo.SelectedValue }, "dataset");
        if (dsPlant != null && dsPlant.Tables[0].Rows.Count > 0)
        {
            ddlPlant.DataTextField = "PlantName";
            ddlPlant.DataValueField = "PlantID";
            ddlPlant.DataSource = dsPlant;
            ddlPlant.DataBind();
            ddlPlant.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlItemCategory.ClearSelection();
            ds = objdb.ByProcedure("SpFinSupplierOrderEntry", new string[] { "flag", "OrderNo", "PlantID" }, new string[] { "7", ddlOrderNo.SelectedValue, ddlPlant.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlItemCategory.DataTextField = "CategoryName";
                ddlItemCategory.DataValueField = "ItemCategoryID";
                ddlItemCategory.DataSource = ds;
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void btnGetItems_Click(object sender, EventArgs e)
    {
        try
        {
            gvItemDetail.DataSource = null;
            gvItemDetail.DataBind();
            ds = objdb.ByProcedure("SpFinSupplierOrderEntry", new string[] { "flag", "ItemCategoryID", "SupplierID", "OrderNo", "PlantID" }, new string[] { "9", ddlItemCategory.SelectedValue.ToString(), ddlSupplier.SelectedValue.ToString(), ddlOrderNo.SelectedValue, ddlPlant.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvItemDetail.DataSource = ds;
                gvItemDetail.DataBind();
                foreach (GridViewRow row in gvItemDetail.Rows)
                {
                    TextBox lblRateInclusive = (TextBox)row.FindControl("lblRateInclusive");
                    TextBox lblItemQuantity = (TextBox)row.FindControl("lblItemQuantity");
                    TextBox lblCessApplicable = (TextBox)row.FindControl("lblCessApplicable");
                    TextBox lblGSTApplicable = (TextBox)row.FindControl("lblGSTApplicable");
                    TextBox lblGSTRate = (TextBox)row.FindControl("lblGSTRate");
                    TextBox lblGSTInclude = (TextBox)row.FindControl("lblGSTInclude");
                    TextBox txtDIFFQuantity = (TextBox)row.FindControl("txtDIFFQuantity");
                    TextBox txtBasicRate = (TextBox)row.FindControl("txtBasicRate");
                    TextBox txtGSTRateAmt = (TextBox)row.FindControl("txtGSTRateAmt");
                    TextBox txtBasicAmount = (TextBox)row.FindControl("txtBasicAmount");
                    TextBox txtGSTAmount = (TextBox)row.FindControl("txtGSTAmount");
                    TextBox txtCess = (TextBox)row.FindControl("txtCess");
                    TextBox lblCessRate = (TextBox)row.FindControl("lblCessRate");
                    TextBox txtTotalAmount = (TextBox)row.FindControl("txtTotalAmount");
                    TextBox txtDeductionAmount = (TextBox)row.FindControl("txtDeductionAmount");
                    TextBox txtExtraSupDed = (TextBox)row.FindControl("txtExtraSupDed");
                    TextBox txtTDS = (TextBox)row.FindControl("txtTDS");
                    TextBox txtNetPayment = (TextBox)row.FindControl("txtNetPayment");
                    lblRateInclusive.Attributes.Add("readonly", "readonly");
                    lblItemQuantity.Attributes.Add("readonly", "readonly");
                    lblCessApplicable.Attributes.Add("readonly", "readonly");
                    lblGSTApplicable.Attributes.Add("readonly", "readonly");
                    lblGSTRate.Attributes.Add("readonly", "readonly");
                    lblGSTInclude.Attributes.Add("readonly", "readonly");
                    txtDIFFQuantity.Attributes.Add("readonly", "readonly");
                    txtBasicRate.Attributes.Add("readonly", "readonly");
                    txtGSTRateAmt.Attributes.Add("readonly", "readonly");
                    txtBasicAmount.Attributes.Add("readonly", "readonly");
                    txtGSTAmount.Attributes.Add("readonly", "readonly");
                    txtCess.Attributes.Add("readonly", "readonly");
                    txtTotalAmount.Attributes.Add("readonly", "readonly");
                    txtDeductionAmount.Attributes.Add("readonly", "readonly");
                    txtExtraSupDed.Attributes.Add("readonly", "readonly");
                    txtTDS.Attributes.Add("readonly", "readonly");
                    txtNetPayment.Attributes.Add("readonly", "readonly"); 
                    lblCessRate.Attributes.Add("readonly", "readonly");
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}