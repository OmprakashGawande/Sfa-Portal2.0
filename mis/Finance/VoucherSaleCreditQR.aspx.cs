using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;

public partial class mis_Finance_VoucherSaleCreditQR : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                ViewState["CGST"] = "1";
                ViewState["SGST"] = "2";
                ViewState["RoundOff"] = "3";
                // txtTotalAmount.Attributes.Add("readonly", "readonly");
                txtTotalAmount.ReadOnly = true;
                txtQuantity.ReadOnly = true;
                txtRate.ReadOnly = true;

                if (Session["Emp_ID"] != null)
                {
                    ViewState["GrandTotal"] = "0";
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    hfofficeID.Value = ViewState["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["VoucherTx_ID"] = "0";
                    FillDropDown();
                    FillItem();
                    ViewState["Action"] = "";
                    FillVoucherDate();
                    //btnAdd.Enabled = false;
                    FillWareHouse();
                    AddItem("NA");
                    ViewState["RowNo"] = "0";
                    lblGrandTotal.Attributes.Add("readonly", "readonly");
                    GridViewItem.DataSource = new string[] { };
                    GridViewItem.DataBind();
                    //ViewState["TableId"] = "-1";
                    ViewState["LedgerTotal"] = "0";
                    CreateLedgerTable();
                    CreateBillByBillDataSet();
                    FillSchemeDropDown();
                    txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
                    GridViewBillByBillDetail.DataSource = new string[] { };
                    GridViewBillByBillDetail.DataBind();
                    /******Create Datatble Subitem Start********/
                    CreateTableFinSubItem();
                    /*********Create Datatble Subitem End*****/
                    GridViewDebtor.DataSource = new string[] { };
                    GridViewDebtor.DataBind();
                    DataTable dt_BillByBillData = new DataTable();
                    dt_BillByBillData.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
                    dt_BillByBillData.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
                    dt_BillByBillData.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
                    ViewState["dt_BillByBillData"] = dt_BillByBillData;
                    if (Request.QueryString["VoucherTx_ID"] != null && Request.QueryString["Action"] != null)
                    {
                        string Action = objdb.Decrypt(Request.QueryString["Action"].ToString());
                        ViewState["Action"] = Action;
                        ViewState["VoucherTx_ID"] = objdb.Decrypt(Request.QueryString["VoucherTx_ID"].ToString());
                        if (Action == "2")
                        {
                            FillDetail();
                            string ValidStatus = ValidDate();
                            if (ValidStatus == "No")
                            {
                                Response.Redirect("~/mis/Login.aspx");
                            }
                        }
                        else if (Action == "1")
                        {
                            if (Request.QueryString["Office_ID"] != null)
                            {
                                ViewState["Office_ID"] = objdb.Decrypt(Request.QueryString["Office_ID"].ToString());
                                FillDetail();
                                ViewVoucher();
                            }
                            else
                            {
                                FillDetail();
                                ViewVoucher();
                            }
                        }
                    }
                    else
                    {
                        GetPreviousVoucherNo();
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);


                }

            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    //Fill Item DropDown
    protected void FillItem()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID" }, new string[] { "45", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlItemName.Items.Clear();
                ddlItemName.DataSource = ds.Tables[0];
                ddlItemName.DataTextField = "AvailableStock1";
                ddlItemName.DataValueField = "Item_id";
                ddlItemName.DataBind();
                ddlItemName.Items.Insert(0, new ListItem("Select", "0"));

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    //Fill Ledger DropDown
    protected void FillDropDown()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster",
                new string[] { "flag", "Office_ID", "MultipleHeadIDs" },
                new string[] { "22", ViewState["Office_ID"].ToString(), "1,2,3,4" }, "dataset");
            if (ds != null)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddlDebitLedger.DataSource = ds.Tables[0];
                    ddlDebitLedger.DataTextField = "Ledger_Name";
                    ddlDebitLedger.DataValueField = "Ledger_ID";
                    ddlDebitLedger.DataBind();
                    ddlDebitLedger.Items.Insert(0, new ListItem("Select", "0"));

                }
                if (ds.Tables[1].Rows.Count > 0)
                {

                    ddlLedger.DataSource = ds.Tables[1];
                    ddlLedger.DataTextField = "Ledger_Name";
                    ddlLedger.DataValueField = "Ledger_ID";
                    ddlLedger.DataBind();
                    ddlLedger.Items.Insert(0, new ListItem("Select", "0"));
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    //Fill WareHouse DropDown
    protected void FillWareHouse()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinVoucherSaleCredit", new string[] { "flag", "Office_ID" }, new string[] { "1", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlWarehouse.DataSource = ds.Tables[0];
                ddlWarehouse.DataTextField = "WarehouseName";
                ddlWarehouse.DataValueField = "Warehouse_id";
                ddlWarehouse.DataBind();
                ddlWarehouse.Items.Insert(0, new ListItem("Select", "0"));

                ddlWarehouse.SelectedIndex = 1;

            }
            else
            {
                ddlWarehouse.Items.Clear();
                ddlWarehouse.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    //Fill Scheme DropDown
    protected void FillSchemeDropDown()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinSchemeTx", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlScheme.DataSource = ds;
                ddlScheme.DataTextField = "SchemeTx_Name";
                ddlScheme.DataValueField = "SchemeTx_ID";
                ddlScheme.DataBind();
                ddlScheme.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlScheme.Items.Clear();
                ddlScheme.Items.Insert(0, new ListItem("Select", "0"));

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    //Fill VoucherDate
    protected void FillVoucherDate()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
                //ViewState["Voucher_FY"] = ds.Tables[0].Rows[0]["Voucher_FY"].ToString();
                FillVoucherNo();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    //Fill VoucherSeries
    protected void FillVoucherNo()
    {
        try
        {
            string VoucherTx_No = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
            string sDate = (Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd")).ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            int Month = int.Parse(datevalue.Month.ToString());
            int Year = int.Parse(datevalue.Year.ToString());
            int FY = Year;
            string FinancialYear = Year.ToString();
            string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
            FinancialYear = "";
            if (Month <= 3)
            {
                FY = Year - 1;
                FinancialYear = FY.ToString() + "-" + LFY.ToString();
            }
            else
            {

                FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
            }
            //string VoucherTx_Names_ForSno = "'Payment,Journal,Contra'";
            //string VoucherTx_Names_ForSno = "'Receipt'";
            //string VoucherTx_Names_ForSno = "'Purchase Voucher'";
            string VoucherTx_Names_ForSno = "Sales Voucher";

            DataSet ds1 = objdb.ByProcedure("SpFinVoucherTx",
                new string[] { "flag", "Office_ID", "VoucherTx_FY", "VoucherTx_Names_ForSno" },
                new string[] { "13", ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_Names_ForSno }, "dataset");

            //int VoucherTx_SNo = 0;
            //if (ds1.Tables[0].Rows.Count != 0)
            //{
            //    VoucherTx_SNo = Convert.ToInt32(ds1.Tables[0].Rows[0]["VoucherTx_SNo"].ToString());

            //}
            //VoucherTx_SNo++;
            //ViewState["VoucherTx_SNo"] = VoucherTx_SNo;
            string Office_Code = "";
            if (ds1.Tables[1].Rows.Count != 0)
            {
                Office_Code = ds1.Tables[1].Rows[0]["Office_Code"].ToString();
            }
            lblVoucherTx_No.Text = Office_Code + FinancialYear.ToString().Substring(2) + "BN";
            //txtVoucherTx_No.Text = VoucherTx_SNo.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }



    protected void btnAdd_Click_Action()
    {
        try
        {
            int ItemStatus = 0;
            string msg = "";
            if (ddlItemName.SelectedIndex == 0)
            {
                msg += "Select Item Name. \\n";
            }
            if (ddlWarehouse.SelectedIndex == 0)
            {
                msg += "Select Location. \\n";
            }
            if (txtQuantity.Text.Trim() == "")
            {
                msg += "Enter Quantity. \\n";
            }
            if (txtRate.Text.Trim() == "")
            {
                msg += "Enter Rate. \\n";
            }
            if (txtRate.Text.Trim() != "")
            {
                if (decimal.Parse(txtRate.Text) == 0)
                {
                    msg += "Rate Should not be zero. \\n";
                }
            }
            if (txtTotalAmount.Text.Trim() == "")
            {
                msg += "Enter Amount. \\n";
            }
            if (msg == "")
            {

                DataTable dt_GridViewItem = new DataTable();
                DataColumn RowNo = dt_GridViewItem.Columns.Add("ID", typeof(int));
                dt_GridViewItem.Columns.Add(new DataColumn("ItemID", typeof(string)));
                dt_GridViewItem.Columns.Add(new DataColumn("Unit_id", typeof(string)));
                dt_GridViewItem.Columns.Add(new DataColumn("Warehouse_id", typeof(string)));
                dt_GridViewItem.Columns.Add(new DataColumn("WarehouseName", typeof(string)));
                dt_GridViewItem.Columns.Add(new DataColumn("Item", typeof(string)));
                dt_GridViewItem.Columns.Add(new DataColumn("Quantity", typeof(string)));
                dt_GridViewItem.Columns.Add(new DataColumn("Rate", typeof(string)));
                dt_GridViewItem.Columns.Add(new DataColumn("Amount", typeof(string)));
                dt_GridViewItem.Columns.Add(new DataColumn("CGST", typeof(decimal)));
                dt_GridViewItem.Columns.Add(new DataColumn("SGST", typeof(decimal)));
                dt_GridViewItem.Columns.Add(new DataColumn("CGST_Per", typeof(string)));
                dt_GridViewItem.Columns.Add(new DataColumn("SGST_Per", typeof(string)));
                dt_GridViewItem.Columns.Add(new DataColumn("TotalAmount", typeof(string)));
                dt_GridViewItem.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
                RowNo.AutoIncrement = true;
                RowNo.AutoIncrementSeed = 1;
                RowNo.AutoIncrementStep = 1;
                // dt_GridViewItem.Columns.Add(new DataColumn("Unit", typeof(string)));

                GetItemSalesLedgerId();
                AddItem(ViewState["RowNo"].ToString());
                ClearItem();
                ViewState["RowNo"] = "0";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);

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

    //Add ItemDetail Event & Function
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            if (ddlItemName.SelectedIndex == 0)
            {
                msg += "Select Item Name. \\n";
            }
            if (ddlWarehouse.SelectedIndex == 0)
            {
                msg += "Select Location. \\n";
            }
            if (txtQuantity.Text.Trim() == "")
            {
                msg += "Enter Quantity. \\n";
            }
            if (txtRate.Text.Trim() == "")
            {
                msg += "Enter Rate. \\n";
            }
            if (txtRate.Text.Trim() != "")
            {
                if (decimal.Parse(txtRate.Text) == 0)
                {
                    msg += "Rate Should not be zero. \\n";
                }
            }
            if (txtTotalAmount.Text.Trim() == "")
            {
                msg += "Enter Amount. \\n";
            }
            if (msg == "")
            {

                /**********************************/
                //ds = objdb.ByProcedure("SpFinItemIngredientTx",
                //        new string[] { "flag", "Item_id" },
                //        new string[] { "1", ddlItemName.SelectedValue.ToString() }, "dataset");
                //if (ds.Tables[0].Rows.Count != 0)
                //{
                //    lblSubItem_amount.Text = txtTotalAmount.Text;
                //    lblSubItem_item.Text = ddlItemName.SelectedItem.ToString();
                //    lblSubItem_quantity.Text = txtQuantity.Text;
                //    lblSubItem_rate.Text = txtRate.Text;
                //    GVFinItemIngredientTx.DataSource = ds.Tables[0];
                //    GVFinItemIngredientTx.DataBind();
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ModalSubItemDetail();", true);
                //}
                //else
                //{
                //    btnAdd_Click_Action();
                //}
                /*********************************/
                btnAdd_Click_Action();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ClearItem();
            lblMsg.Text = "";
            // txtQuantity.Text = "";
            lblUnit.Text = "0";
            txtTotalAmount.Text = "";

            txtQuantity.Text = "";
            txtRate.Text = "";
            txtTotalAmount.ReadOnly = true;
            txtQuantity.ReadOnly = true;
            txtRate.ReadOnly = true;

            if (ddlItemName.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpItemMaster",
                        new string[] { "flag", "ItemId" },
                        new string[] { "20", ddlItemName.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    lblUnit.Text = ds.Tables[0].Rows[0]["NoOfDecimalPlace"].ToString();

                }
                txtTotalAmount.ReadOnly = false;
                txtQuantity.ReadOnly = false;
                txtRate.ReadOnly = false;
                //    ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Item_id", "Vendor_id" },
                //       new string[] { "4", ddlItemName.SelectedValue.ToString(), "1" }, "dataset");
                //    if (ds.Tables[0].Rows.Count != 0)
                //    {
                //        // txtLedger.Text = ds.Tables[0].Rows[0]["Ledger_Name"].ToString();
                //        txtQuantity.Text = "1";
                //        txtRate.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                //        txtTotalAmount.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                //        //txtUnitName.Text = ds.Tables[0].Rows[0]["UnitName"].ToString();
                //        //lblUnitName.Text = ds.Tables[0].Rows[0]["Unit_id"].ToString();

                //  }

            }
            DataSet ds1 = objdb.ByProcedure("SpFinVoucherTx",
                       new string[] { "flag", "Item_id", "Office_ID" },
                       new string[] { "19", ddlItemName.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            if (ds1.Tables[0].Rows.Count != 0)
            {
                txtRate.Text = ds1.Tables[0].Rows[0]["Rate"].ToString();

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    //Get Sales Ledger Mapped With Item
    protected void GetItemSalesLedgerId()
    {
        try
        {
            string Item_id = ddlItemName.SelectedValue.ToString();
            ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Item_id" }, new string[] { "19", Item_id }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["ParticularID"] = ds.Tables[0].Rows[0]["SalesLedger_id"].ToString();
                ViewState["ParticularName"] = ds.Tables[0].Rows[0]["Ledger_Name"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void AddItem(string ID)
    {
        try
        {

            DataTable dt_GridViewItem = new DataTable();
            DataColumn RowNo = dt_GridViewItem.Columns.Add("ID", typeof(int));
            //dt_GridViewItem.Columns.Add(new DataColumn("ID", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("ItemID", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Unit_id", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Warehouse_id", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("WarehouseName", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("HSN_Code", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Item", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Quantity", typeof(float)));
            dt_GridViewItem.Columns.Add(new DataColumn("Rate", typeof(decimal)));
            dt_GridViewItem.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            dt_GridViewItem.Columns.Add(new DataColumn("CGSTAmt", typeof(decimal)));
            dt_GridViewItem.Columns.Add(new DataColumn("SGSTAmt", typeof(decimal)));
            dt_GridViewItem.Columns.Add(new DataColumn("IGSTAmt", typeof(decimal)));
            dt_GridViewItem.Columns.Add(new DataColumn("CGST_Per", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("SGST_Per", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("IGST_Per", typeof(string)));
            //dt_GridViewItem.Columns.Add(new DataColumn("TotalAmount", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Taxbility", typeof(string)));
            RowNo.AutoIncrement = true;
            RowNo.AutoIncrementSeed = 1;
            RowNo.AutoIncrementStep = 1;
            int rowIndex = 0;
            int gridRows = GridViewItem.Rows.Count;
            int RID = 0;
            for (rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {
                //Label lblID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblID");
                Label lblItemRowNo = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemRowNo");
                Label lblItemID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
                Label lblUnit_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblUnit_id");
                Label lblWarehouse_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_id");
                Label lblWarehouseName = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouseName");
                Label lblHSNCode = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblHSNCode");
                Label lblItem = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItem");
                Label lblQuantity = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblQuantity");
                Label lblRate = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblRate");
                Label lblAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblAmount");
                Label lblCGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("CGST");
                Label lblSGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("SGST");
                Label lblIGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("IGST");
                Label lblCGSTPer = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblCGSTPer");
                Label lblSGSTPer = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblSGSTPer");
                Label lblIGSTPer = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblIGSTPer");
                //Label lblTotalAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblTotalAmount");
                Label lblUnit = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblUnit");
                Label lblLedgerName = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblLedgerName");
                Label lblLedgerID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblLedgerID");
                Label lblTaxbility = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");

                if (lblItemRowNo.Text != ID && ViewState["RowNo"].ToString() == "0")
                {
                    RID++;
                    dt_GridViewItem.Rows.Add(lblItemRowNo.Text, lblItemID.Text, lblUnit_id.Text, lblWarehouse_id.Text, lblWarehouseName.Text, lblHSNCode.Text, lblItem.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, lblCGST.Text, lblSGST.Text, lblIGST.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblUnit.Text, lblLedgerName.Text, lblLedgerID.Text, lblTaxbility.Text);
                }
                else if (ViewState["RowNo"].ToString() != "0")
                {
                    ds = objdb.ByProcedure("SpFinVoucherSaleCredit", new string[] { "flag", "Item_id" },
                       new string[] { "2", ddlItemName.SelectedValue.ToString() }, "dataset");
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        gridRows = gridRows + 1;
                        string Item = ddlItemName.SelectedItem.ToString();
                        string UnitID = ds.Tables[0].Rows[0]["Unit_id"].ToString();
                        string Unit = ds.Tables[0].Rows[0]["UQCCode"].ToString();
                        string HSNCode = ds.Tables[0].Rows[0]["HSNCode"].ToString();
                        string Taxbility = ds.Tables[0].Rows[0]["Taxbility"].ToString();
                        string CGSTPer = ds.Tables[0].Rows[0]["CGST"].ToString();
                        string SGSTPer = ds.Tables[0].Rows[0]["SGST"].ToString();
                        string IGSTPer = "0";
                        decimal CGST = decimal.Parse(ds.Tables[0].Rows[0]["CGST"].ToString());
                        decimal SGST = decimal.Parse(ds.Tables[0].Rows[0]["SGST"].ToString());
                        decimal IGST;
                        decimal Amount = decimal.Parse(txtTotalAmount.Text);
                        CGST = Math.Round((Amount * CGST) / 100, 2);
                        SGST = Math.Round((Amount * SGST) / 100, 2);
                        IGST = 0;

                        dt_GridViewItem.Rows.Add(null, ddlItemName.SelectedValue.ToString(), UnitID.ToString(), ddlWarehouse.SelectedValue.ToString(), ddlWarehouse.SelectedItem.Text, HSNCode.ToString(), Item.ToString(), txtQuantity.Text, txtRate.Text, Amount.ToString(), CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTPer.ToString(), SGSTPer.ToString(), IGSTPer.ToString(), Unit.ToString(), ViewState["ParticularName"].ToString(), ViewState["ParticularID"].ToString(), Taxbility);
                    }
                }

                // dt_GridViewItem.Rows.Add(rowIndex.ToString(), lblItem.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text);

            }
            if (ID == "0" && ViewState["RowNo"].ToString() == "0")
            {
                ds = objdb.ByProcedure("SpFinVoucherSaleCredit", new string[] { "flag", "Item_id" },
                   new string[] { "2", ddlItemName.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    gridRows = gridRows + 1;
                    string Item = ddlItemName.SelectedItem.ToString();
                    string UnitID = ds.Tables[0].Rows[0]["Unit_id"].ToString();
                    string Unit = ds.Tables[0].Rows[0]["UQCCode"].ToString();
                    string HSNCode = ds.Tables[0].Rows[0]["HSNCode"].ToString();
                    string Taxbility = ds.Tables[0].Rows[0]["Taxbility"].ToString();
                    string CGSTPer = ds.Tables[0].Rows[0]["CGST"].ToString();
                    string SGSTPer = ds.Tables[0].Rows[0]["SGST"].ToString();
                    string IGSTPer = ds.Tables[0].Rows[0]["IGST"].ToString();
                    IGSTPer = "0";
                    decimal CGST = decimal.Parse(ds.Tables[0].Rows[0]["CGST"].ToString());
                    decimal SGST = decimal.Parse(ds.Tables[0].Rows[0]["SGST"].ToString());
                    decimal IGST;
                    decimal Amount = decimal.Parse(txtTotalAmount.Text);
                    CGST = Math.Round((Amount * CGST) / 100, 2);
                    SGST = Math.Round((Amount * SGST) / 100, 2);
                    IGST = 0;
                    decimal TotalAmount = Amount + CGST + SGST;
                    RID++;
                    dt_GridViewItem.Rows.Add(null, ddlItemName.SelectedValue.ToString(), UnitID.ToString(), ddlWarehouse.SelectedValue.ToString(), ddlWarehouse.SelectedItem.Text, HSNCode.ToString(), Item.ToString(), txtQuantity.Text, txtRate.Text, Amount.ToString(), CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTPer.ToString(), SGSTPer.ToString(), IGSTPer.ToString(), Unit.ToString(), ViewState["ParticularName"].ToString(), ViewState["ParticularID"].ToString(), Taxbility);
                }

            }
            decimal TAmount = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
            decimal CGSTAmount = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
            decimal SGSTTAmount = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
            GridViewItem.DataSource = dt_GridViewItem;
            GridViewItem.DataBind();
            if (GridViewItem.Rows.Count > 0)
            {

                GridViewItem.FooterRow.Cells[4].Text = "<b>Total : </b>";
                GridViewItem.FooterRow.Cells[5].Text = "<b>" + TAmount.ToString() + "</b>";
                GridViewItem.FooterRow.Cells[6].Text = "<b>" + CGSTAmount.ToString() + "</b>";
                GridViewItem.FooterRow.Cells[7].Text = "<b>" + SGSTTAmount.ToString() + "</b>";
                GridViewItem.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                GridViewItem.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                GridViewItem.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            }


            string status = "0";
            string HSN_Code = null;
            decimal CGST_Per = 0;
            decimal SGST_Per = 0;
            decimal IGST_Per = 0;
            decimal CGSTAmt = 0;
            decimal SGSTAmt = 0;
            decimal IGSTAmt = 0;
            DataTable dt_GridViewLedger = new DataTable();
            dt_GridViewLedger.Columns.Add(new DataColumn("LedgerID", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("LedgerName", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("HSN_Code", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("CGST_Per", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("SGST_Per", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("IGST_Per", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("CGSTAmt", typeof(decimal)));
            dt_GridViewLedger.Columns.Add(new DataColumn("SGSTAmt", typeof(decimal)));
            dt_GridViewLedger.Columns.Add(new DataColumn("IGSTAmt", typeof(decimal)));
            dt_GridViewLedger.Columns.Add(new DataColumn("Status", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("GSTApplicable", typeof(string)));
            dt_GridViewLedger.Columns.Add(new DataColumn("Taxbility", typeof(string)));

            gridRows = GridViewLedger.Rows.Count;
            if (gridRows > 2)
            {
                for (rowIndex = 0; rowIndex < gridRows; rowIndex++)
                {

                    Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                    Label lblLedgerName = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblLedgerName");
                    Label lblHSNCode = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblHSNCode");
                    Label lblCGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTPer");
                    Label lblSGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTPer");
                    Label lblIGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTPer");
                    Label lblCGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTAmt");
                    Label lblSGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTAmt");
                    Label lblIGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTAmt");
                    TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                    Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                    Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                    if (lblID.Text == "1" || lblID.Text == "2" || lblID.Text == "3")
                    {
                        status = "0";
                    }
                    else
                    {
                        status = "1";
                        dt_GridViewLedger.Rows.Add(lblID.Text, lblLedgerName.Text, txtAmount.Text, lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, status, lblGSTApplicable.Text, lblTaxbility.Text);
                        foreach (DataRow dr in dt_GridViewLedger.Rows) // search whole table
                        {
                            if (dr["LedgerName"].ToString() == "CGST") // if id==2
                            {
                                dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + decimal.Parse(lblCGSTAmt.Text); //change the name
                                //break; break or not depending on you
                            }
                            if (dr["LedgerName"].ToString() == "SGST") // if id==2
                            {
                                dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + decimal.Parse(lblSGSTAmt.Text); //change the name
                                //break; break or not depending on you
                            }
                        }
                    }

                }
                if (dt_GridViewItem.Rows.Count > 0)
                {

                    decimal CGST = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt")) + dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    decimal SGST = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt")) + dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST", CGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST", SGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                }
                else
                {
                    decimal CGST = dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    decimal SGST = dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST", CGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST", SGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                }
                dt_GridViewLedger.Rows.Add(ViewState["RoundOff"].ToString(), "Round off", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
            }
            else
            {
                status = "0";
                if (dt_GridViewItem.Rows.Count > 0)
                {

                    decimal CGST = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt")) + dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    decimal SGST = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt")) + dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST", CGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST", SGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                }
                else
                {
                    decimal CGST = dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    decimal SGST = dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST", CGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST", SGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                }
                dt_GridViewLedger.Rows.Add(ViewState["RoundOff"].ToString(), "Round off", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
            }


            GridViewLedger.DataSource = dt_GridViewLedger;
            GridViewLedger.DataBind();
            ViewState["LedgerAmount"] = dt_GridViewLedger;

            ViewState["RowNo"] = "0";
            // GridViewLedger
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string ID = GridViewItem.DataKeys[e.RowIndex].Value.ToString();
            string ItemID = GridViewItem.DataKeys[e.RowIndex].Values["ItemID"].ToString();
            /*****************/
            DeleteSubItem(ItemID);
            /*****************/
            AddItem(ID);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearItem()
    {
        try
        {
            lblMsg.Text = "";
            ddlItemName.ClearSelection();
            // txtLedger.Text = "";
            txtQuantity.Text = "";
            txtRate.Text = "";
            txtTotalAmount.Text = "";
            txtTotalAmount.ReadOnly = true;
            txtQuantity.ReadOnly = true;
            txtRate.ReadOnly = true;


            //txtUnitName.Text = "";
            //lblUnitName.Text = "";
            ddlWarehouse.ClearSelection();
            ddlWarehouse.SelectedIndex = 1;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    //Add Ledger/Amount Detail Event & Function
    protected void btnAddLedgerAmt_Click(object sender, EventArgs e)
    {
        try
        {
            int LedgerStatus = 0;
            lblMsg.Text = "";
            if (ddlLedger.SelectedIndex > 0 && txtLedgerAmt.Text != "")
            {
                int rowIndex = 0;
                int gridRows = GridViewLedger.Rows.Count;
                if (gridRows > 0)
                {
                    for (rowIndex = 0; rowIndex < gridRows; rowIndex++)
                    {
                        Label lblLedgerID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                        if (ddlLedger.SelectedIndex > 0)
                        {
                            if (lblLedgerID.Text == ddlLedger.SelectedValue.ToString())
                            {
                                LedgerStatus = 1;
                            }
                            else
                            {


                            }
                        }

                    }
                }
                if (LedgerStatus == 0)
                {
                    FillLedgerAmount("0");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                    ViewState["GrandTotal"] = lblGrandTotal.Text;
                    //btnAcceptEnable();
                    ddlLedger.ClearSelection();
                    txtLedgerAmt.Text = "";
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Ledger already exists');", true);
                }


            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillLedgerAmount(string ID)
    {
        try
        {
            int gridRows = GridViewLedger.Rows.Count;
            if (ID == "0")
            {
                string status = "0";
                DataTable dt_GridViewLedger = new DataTable();
                dt_GridViewLedger.Columns.Add(new DataColumn("LedgerID", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("LedgerName", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("Amount", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("HSN_Code", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("CGST_Per", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("SGST_Per", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("IGST_Per", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("CGSTAmt", typeof(decimal)));
                dt_GridViewLedger.Columns.Add(new DataColumn("SGSTAmt", typeof(decimal)));
                dt_GridViewLedger.Columns.Add(new DataColumn("IGSTAmt", typeof(decimal)));
                dt_GridViewLedger.Columns.Add(new DataColumn("Status", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("GSTApplicable", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("Taxbility", typeof(string)));
                decimal CGST = 0;
                decimal SGST = 0;
                decimal IGST = 0;
                decimal CGSTAmt = 0;
                decimal SGSTAmt = 0;
                decimal IGSTAmt = 0;
                string HSN_Code = null;
                string GSTApplicable = "No";
                string Taxbility = "";
                ds = objdb.ByProcedure("SpFinLedgerGSTDetails", new string[] { "flag", "Ledger_ID", "VoucherTx_Date" }, new string[] { "3", ddlLedger.SelectedValue, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "true")
                    {
                        if (ds.Tables[0].Rows[0]["GSTApplicable"].ToString() != "")
                        {
                            GSTApplicable = ds.Tables[0].Rows[0]["GSTApplicable"].ToString();
                        }
                        Taxbility = ds.Tables[0].Rows[0]["Taxbility"].ToString();
                        CGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                        SGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                        IGST = 0;
                        decimal Amount = decimal.Parse(txtLedgerAmt.Text);
                        CGSTAmt = Math.Round((Amount * CGST) / 100, 2);
                        SGSTAmt = Math.Round((Amount * SGST) / 100, 2);
                        IGSTAmt = 0;
                        HSN_Code = ds.Tables[0].Rows[0]["HSN_Code"].ToString();
                        dt_GridViewLedger.Rows.Add(ddlLedger.SelectedValue.ToString(), ddlLedger.SelectedItem.ToString(), txtLedgerAmt.Text, HSN_Code, CGST, SGST, IGST, CGSTAmt, SGSTAmt, IGSTAmt, "1", GSTApplicable, Taxbility);
                        for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                        {

                            Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                            Label lblLedgerName = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblLedgerName");
                            Label lblHSNCode = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblHSNCode");
                            Label lblCGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTPer");
                            Label lblSGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTPer");
                            Label lblIGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTPer");
                            Label lblCGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTAmt");
                            Label lblSGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTAmt");
                            Label lblIGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTAmt");
                            TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                            Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                            Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                            if (lblID.Text == "1" || lblID.Text == "2" || lblID.Text == "3")
                            {
                                status = "0";
                            }
                            else
                            {
                                status = "1";
                            }
                            dt_GridViewLedger.Rows.Add(lblID.Text, lblLedgerName.Text, txtAmount.Text, lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, status, lblGSTApplicable.Text, lblTaxbility.Text);
                        }

                        foreach (DataRow dr in dt_GridViewLedger.Rows) // search whole table
                        {
                            if (dr["LedgerName"].ToString() == "CGST") // if id==2
                            {
                                dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + CGSTAmt; //change the name
                                //break; break or not depending on you
                            }
                            if (dr["LedgerName"].ToString() == "SGST") // if id==2
                            {
                                dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + SGSTAmt; //change the name
                                //break; break or not depending on you
                            }
                        }
                        ViewState["LedgerAmount"] = dt_GridViewLedger;
                        GridViewLedger.DataSource = dt_GridViewLedger;
                        GridViewLedger.DataBind();
                    }
                    else
                    {
                        dt_GridViewLedger.Rows.Add(ddlLedger.SelectedValue.ToString(), ddlLedger.SelectedItem.ToString(), txtLedgerAmt.Text, HSN_Code, CGST, SGST, IGST, CGSTAmt, SGSTAmt, IGSTAmt, "1", GSTApplicable, Taxbility);
                        for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                        {

                            Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                            Label lblLedgerName = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblLedgerName");
                            Label lblHSNCode = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblHSNCode");
                            Label lblCGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTPer");
                            Label lblSGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTPer");
                            Label lblIGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTPer");
                            Label lblCGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTAmt");
                            Label lblSGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTAmt");
                            Label lblIGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTAmt");
                            TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                            Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                            Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                            if (lblID.Text == "1" || lblID.Text == "2" || lblID.Text == "3" || lblID.Text == "737")
                            {
                                status = "0";
                            }
                            else
                            {
                                status = "1";
                            }
                            dt_GridViewLedger.Rows.Add(lblID.Text, lblLedgerName.Text, txtAmount.Text, lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, status, lblGSTApplicable.Text, lblTaxbility.Text);
                        }
                        ViewState["LedgerAmount"] = dt_GridViewLedger;
                        GridViewLedger.DataSource = dt_GridViewLedger;
                        GridViewLedger.DataBind();
                    }

                }

            }
            else
            {
                DataTable dt_GridViewLedger = (DataTable)ViewState["LedgerAmount"];
                for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                {

                    Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                    Label lblLedgerName = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblLedgerName");
                    Label lblHSNCode = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblHSNCode");
                    Label lblCGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTPer");
                    Label lblSGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTPer");
                    Label lblIGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTPer");
                    Label lblCGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTAmt");
                    Label lblSGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTAmt");
                    Label lblIGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTAmt");
                    TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                    Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                    Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                    if (int.Parse(lblID.Text) == int.Parse(ID))
                    {
                        for (int i = dt_GridViewLedger.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow dr = dt_GridViewLedger.Rows[i];
                            if (dr["LedgerID"].ToString() == ID)
                            {
                                dr.Delete();
                            }

                        }
                        dt_GridViewLedger.AcceptChanges();
                        foreach (DataRow dr in dt_GridViewLedger.Rows) // search whole table
                        {
                            if (dr["LedgerName"].ToString() == "CGST") // if id==2
                            {
                                dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) - decimal.Parse(lblCGSTAmt.Text); //change the name
                                //break; break or not depending on you
                            }
                            if (dr["LedgerName"].ToString() == "SGST") // if id==2
                            {
                                dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) - decimal.Parse(lblSGSTAmt.Text); //change the name
                                //break; break or not depending on you
                            }

                        }
                    }



                }
                GridViewLedger.DataSource = dt_GridViewLedger;
                GridViewLedger.DataBind();
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewLedger_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string ID = GridViewLedger.DataKeys[e.RowIndex].Value.ToString();
            FillLedgerAmount(ID);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Add Ledger/DebtorDetail
    protected void CreateLedgerTable()
    {
        DataTable dt_LedgerTable = new DataTable();
        dt_LedgerTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_Amount", typeof(decimal)));
        dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_MaintainType", typeof(string)));
        //dt_LedgerTable.Columns.Add(new DataColumn("Ledger_TableID", typeof(decimal)));

        ViewState["LedgerTable"] = dt_LedgerTable;


        GridViewDebtor.DataSource = dt_LedgerTable;
        GridViewDebtor.DataBind();

        if (dt_LedgerTable.Rows.Count > 0)
        {
            decimal Amount = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));

            GridViewDebtor.FooterRow.Cells[2].Text = "<b>| TOTAL |</b> ";
            GridViewDebtor.FooterRow.Cells[3].Text = "<b>" + Amount.ToString() + "</b>";
        }
    }
    protected void btnAddDebtor_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string LedgerId = ddlDebitLedger.SelectedValue.ToString();
            txtBillByBillTx_Ref.Visible = true;
            ddlBillByBillTx_Ref.Visible = false;
            txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
            txtChequeTx_Amount.Text = txDebtorAmt.Text;

            if (lblGrandTotal.Text.Contains("-"))
            {
                ViewState["DebtorAmount"] = txDebtorAmt.Text;

            }
            else
            {
                ViewState["DebtorAmount"] = "-" + txDebtorAmt.Text;
            }
            //if (ddlcreditdebit.SelectedValue == "Dr")
            //{
            //    ViewState["LedgerAmount"] = "-" + txDebtorAmt.Text;
            //}
            //else
            //{
            //    ViewState["LedgerAmount"] = txDebtorAmt.Text;
            //}
            ViewState["Amount"] = txDebtorAmt.Text;
            ViewState["BillByBillAmount"] = "0";
            txtBillByBillTx_Amount.Text = txDebtorAmt.Text;
            ddlRefType.ClearSelection();
            ddlBillByBillTx_crdr.ClearSelection();
            //ddlBillByBillTx_Ref.Items.Clear();
            lnkView.Visible = false;
            txtBillByBillTx_Ref.Enabled = true;
            string msg = "";
            if (ddlDebitLedger.SelectedIndex <= 0)
            {
                msg = "Select Debtor.\\n";
            }
            if (txDebtorAmt.Text == "")
            {
                msg += "Enter Amount.\\n";
            }
            if (chkitem.Checked == true)
            {
                if (GridViewItem.Rows.Count == 0)
                {
                    msg += "Enter Item Detail. \\n";
                }

            }
            if (txtVoucherTx_No.Text == "")
            {
                msg += "Enter Voucher/Bill No. \\n";
            }
            if (msg == "")
            {
                ViewState["action"] = "Add";
                ViewState["LedgerIDModel"] = ddlDebitLedger.SelectedValue;

                int rowIndex = 0;
                int gridRows = GridViewDebtor.Rows.Count;
                if (gridRows > 0)
                {
                    for (rowIndex = 0; rowIndex < gridRows; rowIndex++)
                    {
                        Label lblLedgerID = (Label)GridViewDebtor.Rows[rowIndex].Cells[0].FindControl("Ledger_ID");
                        if (lblLedgerID.Text == ddlDebitLedger.SelectedValue.ToString())
                        {
                            status = 1;
                        }
                        else
                        {


                        }
                    }
                }
                if (status == 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Ledger already exists');", true);
                }
                else
                {
                    ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", LedgerId, ViewState["Office_ID"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "Yes")
                        {
                            CreateBillByBillTable();
                            if (lblGrandTotal.Text.Contains("-"))
                            {
                                ddlBillByBillTx_crdr.SelectedValue = "Cr";
                            }
                            else
                            {
                                ddlBillByBillTx_crdr.SelectedValue = "Dr";
                            }
                            //txtBillByBillTx_Amount.Text = txtLedgerTx_Amount.Text;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);
                            BindBillByBillData();
                        }
                        else if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "BankLedger")
                        {
                            btnAddCheque.Enabled = true;
                            //btnAddChequeDetail.Enabled = false;
                            CreatTableFinChequeTx();
                            //txtChequeTx_Amount.Text = txtLedgerTx_Amount.Text;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
                        }

                        else
                        {
                            //CreateBillByBillTable();
                            //ViewState["GrandTotal"] = lblGrandTotal.Text;
                            //DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                            //dsBillByBill.Merge((DataTable)ViewState["BillByBillTable"]);
                            //ViewState["dsBillByBill"] = dsBillByBill;
                            DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                            dt_LedgerTable.Rows.Add(ddlDebitLedger.SelectedValue.ToString(), ddlDebitLedger.SelectedItem.Text, txDebtorAmt.Text, "None");

                            GridViewDebtor.DataSource = dt_LedgerTable;
                            GridViewDebtor.DataBind();

                            if (dt_LedgerTable.Rows.Count > 0)
                            {
                                decimal Amount = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));

                                GridViewDebtor.FooterRow.Cells[2].Text = "<b>| TOTAL |</b>";
                                GridViewDebtor.FooterRow.Cells[3].Text = "<b>" + Amount.ToString() + "</b>";
                                GridViewDebtor.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                            }


                            decimal LedgerTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));
                            ViewState["LedgerTotal"] = LedgerTotal;
                            hfvalue.Value = ViewState["LedgerTotal"].ToString();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                            //btnAcceptEnable();
                            //if (ViewState["GrandTotal"].ToString() == ViewState["LedgerTotal"].ToString())
                            //{
                            //    btnAccept.Enabled = true;
                            //    btnAddDebtor.Enabled = false;
                            //}
                            ClearBillByBillModal();

                        }
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
    protected void GridViewDebtor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
            int LedgerId = int.Parse(GridViewDebtor.SelectedDataKey.Value.ToString());
            int rowindex = int.Parse(GridViewDebtor.SelectedRow.RowIndex.ToString());
            Label lbl = (Label)GridViewDebtor.Rows[rowindex].FindControl("lblMaintainType");
            //     int RowNo = int.Parse(GridViewLedgerDetail.SelectedDataKey.Value.ToString());
            // Label lblTypeledger = (Label)GridViewDebtor.Rows[rowindex].FindControl("Type");
            Label lblLedgerTx_Amount = (Label)GridViewDebtor.Rows[rowindex].FindControl("LedgerTx_Amount");
            Label lblLedger_IDMod = (Label)GridViewDebtor.Rows[rowindex].FindControl("Ledger_ID");

            ViewState["LedgerIDModel"] = lblLedger_IDMod.Text;

            if (ViewState["action"].ToString() == "View")
            {
                if (lbl.Text == "BillByBill")
                {
                    GridViewBillByBillViewDetail.DataSource = dsBillByBill.Tables[LedgerId.ToString()];
                    GridViewBillByBillViewDetail.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillByBillViewModal();", true);
                }

                else if (lbl.Text == "Cheque")
                {
                    GVViewFinChequeTx.DataSource = dsBillByBill.Tables[LedgerId.ToString()];
                    GVViewFinChequeTx.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetailView();", true);
                }
            }
            else if (ViewState["action"].ToString() == "Edit")
            {
                ViewState["RowNo"] = LedgerId.ToString();
                ViewState["BillByBillTable"] = dsBillByBill.Tables[LedgerId.ToString()];


                txtBillByBillTx_Ref.Text = "";
                txtBillByBillTx_Ref.Visible = true;
                ddlBillByBillTx_Ref.Visible = false;
                ddlRefType.ClearSelection();
                txtBillByBillTx_Amount.Text = "";

                BindBillByBillData();


                GridViewBillByBillDetail.DataSource = dsBillByBill.Tables[LedgerId.ToString()];
                GridViewBillByBillDetail.DataBind();

                decimal LedgerAmount = 0;
                foreach (GridViewRow rows in GridViewBillByBillDetail.Rows)
                {
                    decimal Amount = 0;
                    Label lblAmount = (Label)rows.FindControl("lblAmount");
                    Label lblType = (Label)rows.FindControl("lblType");
                    if (lblType.Text == "Dr")
                    {
                        Amount = decimal.Parse("-" + lblAmount.Text);
                    }
                    else
                    {
                        Amount = decimal.Parse(lblAmount.Text);
                    }
                    LedgerAmount = LedgerAmount + Amount;
                }

                ViewState["DebtorAmount"] = "-" + lblLedgerTx_Amount.Text;

                if (txDebtorAmt.Text == "")
                    ViewState["Amount"] = "0";
                else
                    ViewState["Amount"] = txDebtorAmt.Text;

                ViewState["BillByBillAmount"] = LedgerAmount;


                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);


            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewDebtor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int LedgerID = int.Parse(GridViewDebtor.DataKeys[e.RowIndex].Value.ToString());
            DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
            DataSet dsBillByBillTemp = new DataSet();
            dsBillByBillTemp = dsBillByBill;
            for (int i = 0; i < dsBillByBillTemp.Tables.Count;


                i++)
            {
                if (dsBillByBillTemp.Tables[i].TableName == LedgerID.ToString())
                {
                    dsBillByBill.Tables.Remove(dsBillByBillTemp.Tables[i].TableName);
                    //dsBillByBill.Tables[i].Merge(dsBillByBillTemp.Tables[i]);
                }
            }



            DataTable dt_LedgerTableTemp = new DataTable();
            dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_Amount", typeof(decimal)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_MaintainType", typeof(string)));
            //dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_TableID", typeof(decimal)));


            int gridRows = GridViewDebtor.Rows.Count;
            for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {
                Label Ledger_ID = (Label)GridViewDebtor.Rows[rowIndex].Cells[0].FindControl("Ledger_ID");
                Label Ledger_Name = (Label)GridViewDebtor.Rows[rowIndex].Cells[0].FindControl("Ledger_Name");
                Label LedgerTx_Amount = (Label)GridViewDebtor.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Amount");
                Label lblMaintainType = (Label)GridViewDebtor.Rows[rowIndex].Cells[0].FindControl("lblMaintainType");
                //Label Ledger_TableID = (Label)GridViewDebtor.Rows[rowIndex].Cells[0].FindControl("Ledger_TableID");
                if (Ledger_ID.Text != LedgerID.ToString())
                {

                    dt_LedgerTableTemp.Rows.Add(Ledger_ID.Text, Ledger_Name.Text, LedgerTx_Amount.Text, lblMaintainType.Text);
                }
            }
            GridViewDebtor.DataSource = null;
            GridViewDebtor.DataBind();
            GridViewDebtor.DataSource = dt_LedgerTableTemp;
            GridViewDebtor.DataBind();

            if (dt_LedgerTableTemp.Rows.Count > 0)
            {
                decimal Amount = dt_LedgerTableTemp.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));
                ViewState["Amount"] = Amount;
                hfvalue.Value = ViewState["Amount"].ToString();
                GridViewDebtor.FooterRow.Cells[2].Text = "<b>| TOTAL |</b> ";
                GridViewDebtor.FooterRow.Cells[3].Text = "<b>" + Amount.ToString() + "</b>";
                GridViewDebtor.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            }

            ViewState["LedgerTable"] = dt_LedgerTableTemp;
            if (dt_LedgerTableTemp.Rows.Count == 0)
            {
                ViewState["Amount"] = "0";
                hfvalue.Value = ViewState["Amount"].ToString();

            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    //Add BillByBillDetail Event & Function
    protected void CreateBillByBillDataSet()
    {
        DataSet dsBillByBill = new DataSet();
        ViewState["dsBillByBill"] = dsBillByBill;

    }
    protected void CreateBillByBillTable()
    {

        string TNO = ddlDebitLedger.SelectedValue.ToString();
        DataTable dt_BillByBillTable = new DataTable(TNO);
        DataColumn RowNo = dt_BillByBillTable.Columns.Add("RowNo", typeof(int));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
        dt_BillByBillTable.Columns.Add(new DataColumn("Type", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));

        RowNo.AutoIncrement = true;
        RowNo.AutoIncrementSeed = 1;
        RowNo.AutoIncrementStep = 1;

        ViewState["BillByBillTable"] = dt_BillByBillTable;

        GridViewBillByBillDetail.DataSource = dt_BillByBillTable;
        GridViewBillByBillDetail.DataBind();
    }
    protected void ddlRefType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindBillByBillData();
        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);
            if (ddlRefType.SelectedValue.ToString() == "1")
            {
                txtBillByBillTx_Ref.Visible = false;
                ddlBillByBillTx_Ref.Visible = true;
                ddlBillByBillTx_Ref.ClearSelection();
                lnkView.Visible = true;
                //txtBillByBillTx_Ref.Enabled = false;

            }
            else if (ddlRefType.SelectedValue.ToString() == "3")
            {
                //txtBillByBillTx_Ref.Visible = true;
                //ddlBillByBillTx_Ref.Visible = false;
                //txtBillByBillTx_Ref.Enabled = false;
                //txtBillByBillTx_Ref.Text = "On Account";
            }
            else
            {
                txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
                if (GridViewBillByBillDetail.Rows.Count < 1)
                {
                    txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();

                }
                else
                {

                }
                txtBillByBillTx_Ref.Visible = true;
                ddlBillByBillTx_Ref.Visible = false;
                lnkView.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //Bind Ledger AgstRef Detail in BillByBillModal
    protected void BindBillByBillData()
    {
        try
        {
            if (btnAccept.Text == "Accept")
            {
                DataTable dt_BillByBillData = (DataTable)ViewState["dt_BillByBillData"];
                ddlBillByBillTx_Ref.Items.Clear();
                //string LedgerID = ddlDebitLedger.SelectedValue.ToString();
                string LedgerID = ViewState["LedgerIDModel"].ToString();
                ds = objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "2", LedgerID, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["AgnstRef"] = ds.Tables[0].Rows[0]["AgnstRef"].ToString();
                    dt_BillByBillData = ds.Tables[0];
                    ViewState["dt_BillByBillData"] = dt_BillByBillData;
                    ddlBillByBillTx_Ref.DataSource = dt_BillByBillData;
                    ddlBillByBillTx_Ref.DataTextField = "AgnstRef";
                    ddlBillByBillTx_Ref.DataValueField = "BillByBillTx_Ref";
                    ddlBillByBillTx_Ref.DataBind();
                    ddlBillByBillTx_Ref.Items.Insert(0, "Select");
                    GridViewRefDetail.DataSource = ds.Tables[0];
                    GridViewRefDetail.DataBind();

                }
                else
                {
                    ddlBillByBillTx_Ref.Items.Clear();
                    ddlBillByBillTx_Ref.Items.Insert(0, "Select");
                    GridViewRefDetail.DataSource = new string[] { };
                    GridViewRefDetail.DataBind();
                }
            }
            else
            {
                DataTable dt_BillByBillData = (DataTable)ViewState["dt_BillByBillData"];
                ddlBillByBillTx_Ref.Items.Clear();
                //string LedgerID = ddlDebitLedger.SelectedValue.ToString();
                string LedgerID = ViewState["LedgerIDModel"].ToString();
                ds = objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "Ledger_ID", "Office_ID", "VoucherTx_ID" }, new string[] { "12", LedgerID, ViewState["Office_ID"].ToString(), ViewState["VoucherTx_ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["AgnstRef"] = ds.Tables[0].Rows[0]["AgnstRef"].ToString();
                    dt_BillByBillData = ds.Tables[0];
                    ViewState["dt_BillByBillData"] = dt_BillByBillData;
                    ddlBillByBillTx_Ref.DataSource = dt_BillByBillData;
                    ddlBillByBillTx_Ref.DataTextField = "AgnstRef";
                    ddlBillByBillTx_Ref.DataValueField = "BillByBillTx_Ref";
                    ddlBillByBillTx_Ref.DataBind();
                    ddlBillByBillTx_Ref.Items.Insert(0, "Select");
                    GridViewRefDetail.DataSource = ds.Tables[0];
                    GridViewRefDetail.DataBind();

                }
                else
                {
                    ddlBillByBillTx_Ref.Items.Clear();
                    ddlBillByBillTx_Ref.Items.Insert(0, "Select");
                    GridViewRefDetail.DataSource = new string[] { };
                    GridViewRefDetail.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void btnAddBillByBill_Click(object sender, EventArgs e)
    {
        string msg = "";
        if (txtBillByBillTx_Ref.Text == "")
        {
            msg += "Enter Name.\n";
        }
        if (txtBillByBillTx_Amount.Text == "")
        {
            msg += "Enter Amount.\n";
        }
        if (ddlBillByBillTx_crdr.SelectedIndex == 0)
        {
            msg += "Select Cr/Dr.\n";
        }
        if (msg == "")
        {
            string LedgerAmount = BillAmount("0");
            ManageBillByBill(LedgerAmount);

        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }


    }
    protected string BillAmount(string ID)
    {
        decimal LedgerAmount = 0;
        try
        {
            if (ID == "0")
            {
                DataTable dt_BillByBillTable = (DataTable)ViewState["BillByBillTable"];
                string Type = "";

                if (ddlBillByBillTx_crdr.SelectedValue == "Cr")
                {
                    Type = "Cr";
                }
                else
                {
                    Type = "Dr";
                }

                if (ddlRefType.SelectedItem.Text == "Agst Ref")
                {
                    //dt_BillByBillTable.Rows.Add(null, ddlRefType.SelectedItem.Text, ddlBillByBillTx_Ref.SelectedValue, txtBillByBillTx_Amount.Text, Type, ddlDebitLedger.SelectedValue.ToString());
                    dt_BillByBillTable.Rows.Add(null, ddlRefType.SelectedItem.Text, ddlBillByBillTx_Ref.SelectedValue, txtBillByBillTx_Amount.Text, Type, ViewState["LedgerIDModel"].ToString());
                }
                else
                {
                    //dt_BillByBillTable.Rows.Add(null, ddlRefType.SelectedItem.Text, txtBillByBillTx_Ref.Text, txtBillByBillTx_Amount.Text, Type, ddlDebitLedger.SelectedValue.ToString());
                    dt_BillByBillTable.Rows.Add(null, ddlRefType.SelectedItem.Text, txtBillByBillTx_Ref.Text, txtBillByBillTx_Amount.Text, Type, ViewState["LedgerIDModel"].ToString());
                }

                GridViewBillByBillDetail.DataSource = dt_BillByBillTable;
                GridViewBillByBillDetail.DataBind();
                txtBillByBillTx_Ref.Text = "";
                ddlRefType.ClearSelection();


                ViewState["BillByBillTable"] = dt_BillByBillTable;

            }
            else
            {
                DataTable dt_BillByBillTable = (DataTable)ViewState["BillByBillTable"];
                int Count = dt_BillByBillTable.Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                    DataRow dr = dt_BillByBillTable.Rows[i];
                    if (dr["RowNo"].ToString() == ID.ToString())
                    {
                        dr.Delete();
                        break;
                    }
                }
                dt_BillByBillTable.AcceptChanges();
                ViewState["BillByBillTable"] = dt_BillByBillTable;
                GridViewBillByBillDetail.DataSource = dt_BillByBillTable;
                GridViewBillByBillDetail.DataBind();
            }
            foreach (GridViewRow rows in GridViewBillByBillDetail.Rows)
            {
                decimal Amount = 0;
                Label lblAmount = (Label)rows.FindControl("lblAmount");
                Label lblType = (Label)rows.FindControl("lblType");
                if (lblType.Text == "Dr")
                {
                    Amount = decimal.Parse("-" + lblAmount.Text);
                }
                else
                {
                    Amount = decimal.Parse(lblAmount.Text);
                }
                LedgerAmount = LedgerAmount + Amount;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        //return ViewState["Amount"].ToString();
        return LedgerAmount.ToString();
    }
    protected void ClearBillByBillModal()
    {
        ddlRefType.ClearSelection();
        ddlBillByBillTx_Ref.ClearSelection();
        txtBillByBillTx_Ref.Text = "";
        ddlDebitLedger.ClearSelection();
        txDebtorAmt.Text = "";
        ViewState["BillByBillTable"] = "";

    }

    //AddChequeDetail Event & Function
    protected void CreatTableFinChequeTx()
    {
        string TNO = ddlDebitLedger.SelectedValue.ToString();
        DataTable dt_FinChequeTx = new DataTable(TNO);
        dt_FinChequeTx.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_No", typeof(string)));
        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Date", typeof(string)));
        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Amount", typeof(decimal)));

        ViewState["FinChequeTx"] = dt_FinChequeTx;

        GVFinChequeTx.DataSource = dt_FinChequeTx;
        GVFinChequeTx.DataBind();
    }
    protected void btnAddCheque_Click(object sender, EventArgs e)
    {
        string msg = "";
        //if (txtChequeTx_No.Text == "")
        //{
        //    msg += "Enter Cheque/ DD No \\n";
        //}
        //if (txtChequeTx_Date.Text == "")
        //{
        //    msg += "Enter Cheque/ DD Date \\n";
        //}
        if (txtChequeTx_Amount.Text == "")
        {
            msg += "Enter Amount \\n";
        }
        if (msg == "")
        {
            string CheqAmount = ChequeAmount("0");
            if (decimal.Parse(txDebtorAmt.Text) != decimal.Parse(CheqAmount))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
            }
            else
            {
                ViewState["GrandTotal"] = lblGrandTotal.Text;
                DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                dsBillByBill.Merge((DataTable)ViewState["FinChequeTx"]);
                ViewState["dsBillByBill"] = dsBillByBill;
                DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                dt_LedgerTable.Rows.Add(ddlDebitLedger.SelectedValue.ToString(), ddlDebitLedger.SelectedItem.Text, txDebtorAmt.Text, "Cheque");

                GridViewDebtor.DataSource = dt_LedgerTable;
                GridViewDebtor.DataBind();

                if (dt_LedgerTable.Rows.Count > 0)
                {
                    decimal Amount = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));

                    GridViewDebtor.FooterRow.Cells[2].Text = "<b>| TOTAL |</b>";
                    GridViewDebtor.FooterRow.Cells[3].Text = "<b>" + Amount.ToString() + "</b>";
                    GridViewDebtor.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                }


                decimal LedgerTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));
                ViewState["LedgerTotal"] = LedgerTotal;
                hfvalue.Value = ViewState["LedgerTotal"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                ClearFinChequeTxModal();
            }

        }
        else
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }

    }
    protected string ChequeAmount(string ID)
    {

        string CheqAmount = "0";
        try
        {
            DataTable dt_FinChequeTx = (DataTable)ViewState["FinChequeTx"];

            dt_FinChequeTx.Rows.Add(ddlDebitLedger.SelectedValue.ToString(), txtChequeTx_No.Text, txtChequeTx_Date.Text, txtChequeTx_Amount.Text);


            GVFinChequeTx.DataSource = dt_FinChequeTx;
            GVFinChequeTx.DataBind();

            decimal ChequeTx_AmountTotal = 0;

            ChequeTx_AmountTotal = dt_FinChequeTx.AsEnumerable().Sum(row => row.Field<decimal>("ChequeTx_Amount"));

            //GVFinChequeTx.FooterRow.Cells[2].Text = "<b>Total : </b>";
            //GVFinChequeTx.FooterRow.Cells[3].Text = "<b>" + ChequeTx_AmountTotal.ToString() + "</b>";

            txtChequeTx_Amount.Text = (Convert.ToDecimal(txDebtorAmt.Text) - ChequeTx_AmountTotal).ToString();

            txtChequeTx_No.Text = "";
            txtChequeTx_Date.Text = "";
            if (dt_FinChequeTx.Rows.Count > 0)
            {

                decimal Amt = dt_FinChequeTx.AsEnumerable().Sum(row => row.Field<decimal>("ChequeTx_Amount"));
                CheqAmount = Amt.ToString();
            }

            ViewState["FinChequeTx"] = dt_FinChequeTx;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        return CheqAmount.ToString();
    }
    protected void ClearFinChequeTxModal()
    {
        txtChequeTx_No.Text = "";
        txtChequeTx_Date.Text = "";
        txtChequeTx_Amount.Text = "";

        ViewState["FinChequeTx"] = "";

        GVFinChequeTx.DataSource = new string[] { };
        GVFinChequeTx.DataBind();


        ddlDebitLedger.ClearSelection();
        txDebtorAmt.Text = "";




    }

    //Save Data
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        try
        {

            string msg = "";
            string Scheme = "0";
            if (txtVoucherTx_No.Text == "")
            {
                msg += "Enter Voucher/Bill No. \\n";
            }
            if (txtVoucherTx_Date.Text == "")
            {
                msg += "Enter Date. \\n";
            }
            else
            {
                string ValidStatus = ValidDate();
                if (ValidStatus == "No")
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
            }
            if (ddlScheme.SelectedIndex != 0)
            {
                // msg += "Select Scheme . \\n";
                Scheme = ddlScheme.SelectedValue.ToString();
            }
            //if (txtVoucherTx_OrderNo.Text == "")
            //{
            //    msg += "Enter Order No. \\n";
            //}
            //if (txtVoucherTx_RegNo.Text == "")
            //{
            //    msg += "Enter Registration No. \\n";
            //}
            if (txtVoucherTx_Narration.Text == "")
            {
                msg += "Enter Narration. \\n";
            }
            if (msg == "")
            {
                string VoucherTx_No = lblVoucherTx_No.Text + txtVoucherTx_No.Text;

                string sDate = (Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd")).ToString();
                DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
                int Month = int.Parse(datevalue.Month.ToString());
                int Year = int.Parse(datevalue.Year.ToString());
                int FY = Year;
                string FinancialYear = Year.ToString();
                string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
                FinancialYear = "";
                if (Month <= 3)
                {
                    FY = Year - 1;
                    FinancialYear = FY.ToString() + "-" + LFY.ToString();
                }
                else
                {

                    FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
                }
                string VoucherTx_OrderDate = "";
                if (txtVoucherTx_OrderDate.Text != "")
                {
                    VoucherTx_OrderDate = Convert.ToDateTime(txtVoucherTx_OrderDate.Text, cult).ToString("yyyy/MM/dd");
                }
                else
                {
                    VoucherTx_OrderDate = "";
                }
                string VoucherTx_IsActive = "1";
                string LedgerTx_IsActive = "1";
                string ItemTx_IsActive = "1";
                int Status = 0;
                DataSet ds11 = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "VoucherTx_No", "VoucherTx_ID" },
                    new string[] { "9", VoucherTx_No, ViewState["VoucherTx_ID"].ToString() }, "dataset");
                if (ds11.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds11.Tables[0].Rows[0]["Status"].ToString());

                }
                if (btnAccept.Text == "Accept" && ViewState["VoucherTx_ID"].ToString() == "0" && Status == 0)
                {
                    VoucherTx_IsActive = "0";
                    LedgerTx_IsActive = "0";
                    ItemTx_IsActive = "0";
                    // ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy", "VoucherTx_SalesCenterID", "SchemeTx_ID", "VoucherTx_SoldTo", "VoucherTx_OrderNo", "VoucherTx_RegNo", "VoucherTx_SNo" }, new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Sales Voucher", "CreditSale Voucher", txtVoucherTx_No.Text, txtVoucherTx_Ref.Text, txtVoucherTx_Narration.Text, lblGrandTotal.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_IsActive, ViewState["Emp_ID"].ToString(), ddlSalesCenter.SelectedValue.ToString(), ddlScheme.SelectedValue.ToString(), txtNameofConsignee.Text, txtVoucherTx_OrderNo.Text, txtVoucherTx_RegNo.Text, ViewState["VoucherTx_SNo"].ToString() }, "dataset");
                    ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy", "VoucherTx_SalesCenterID", "SchemeTx_ID", "VoucherTx_SoldTo", "VoucherTx_OrderNo", "VoucherTx_RegNo", "VoucherTx_OrderDate", "GSTVoucher" }, new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Sales Voucher", "CreditSale Voucher", VoucherTx_No, txtVoucherTx_Ref.Text, txtVoucherTx_Narration.Text, lblGrandTotal.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_IsActive, ViewState["Emp_ID"].ToString(), "0", Scheme, txtNameofConsignee.Text, txtVoucherTx_OrderNo.Text, txtVoucherTx_RegNo.Text, VoucherTx_OrderDate, "Yes" }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string VoucherTx_ID = ds.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
                        int rowItemIndex = 0;
                        int gridItemRows = GridViewItem.Rows.Count;
                        for (rowItemIndex = 0; rowItemIndex < gridItemRows; rowItemIndex++)
                        {
                            Label lblItemRowNo = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblItemRowNo");
                            Label lblItemID = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblItemID");
                            Label lblUnit_id = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblUnit_id");
                            Label lblWarehouse_id = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblWarehouse_id");
                            Label lblHSNCode = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblHSNCode");
                            Label lblItem = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblItem");
                            Label lblQuantity = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblQuantity");
                            Label lblRate = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblRate");
                            Label lblAmount = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblAmount");
                            Label lblCGST = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("CGST");
                            Label lblSGST = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("SGST");
                            Label lblIGST = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("IGST");
                            Label lblCGSTPer = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblCGSTPer");
                            Label lblSGSTPer = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblSGSTPer");
                            Label lblIGSTPer = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblIGSTPer");
                            Label lblLedgerID = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblLedgerID");
                            Label lblTaxbility = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblTaxbility");
                            objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Name", "VoucherTx_Type", "Ledger_ID", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "HSN_Code", "IGST_Per", "CGST_Per", "SGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Warehouse_id", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy", "Taxbility" }
                                , new string[] { "0", VoucherTx_ID, "Sales Voucher", "CreditSale Voucher", lblLedgerID.Text, lblItemID.Text, lblUnit_id.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, lblHSNCode.Text, lblIGSTPer.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblCGST.Text, lblSGST.Text, lblIGST.Text, ddlWarehouse.SelectedValue.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), lblItemRowNo.Text, lblTaxbility.Text }, "dataset");
                            ds = objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "Ledger_ID" }, new string[] { "3", lblLedgerID.Text }, "dataset");
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["InventoryAffected"].ToString() == "Yes")
                                {
                                    objdb.ByProcedure("SpFinItemTx",
                                        new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "TransactionFrom", "InvoiceNo", "Office_Id", "Warehouse_id", "CreatedBy", "TranDt", "Amount" }
                                       , new string[] { "4", lblItemID.Text, "0", lblQuantity.Text, lblRate.Text, VoucherTx_ID, "CreditSale Voucher", VoucherTx_No, ViewState["Office_ID"].ToString(), lblWarehouse_id.Text, ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), lblAmount.Text }, "dataset");
                                }
                            }
                            objdb.ByProcedure("SpFinLedgerTx",
                                new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
                                new string[] { "0", lblLedgerID.Text, "Item Ledger", VoucherTx_ID, lblItemID.Text, "CreditSale Voucher", lblAmount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), lblItemRowNo.Text }, "dataset");
                        }

                        /******Insert Dubtable Data to Database**********/
                        InsertSubItemDB(VoucherTx_ID.ToString(), "Sales Voucher", "CreditSale Voucher","0");
                        /******Insert Dubtable Data to Database**********/

                        int gridRows = GridViewLedger.Rows.Count;
                        for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                        {

                            Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                            Label lblHSNCode = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblHSNCode");
                            Label lblCGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTPer");
                            Label lblSGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTPer");
                            Label lblIGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTPer");
                            Label lblCGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTAmt");
                            Label lblSGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTAmt");
                            Label lblIGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTAmt");
                            TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                            Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                            Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                            objdb.ByProcedure("SpFinLedgerTx",
                            new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "GSTApplicable", "Taxbility" },
                            new string[] { "0", lblID.Text, "Sub Ledger", VoucherTx_ID, "CreditSale Voucher", txtAmount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 1).ToString(), lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, lblGSTApplicable.Text, lblTaxbility.Text }, "dataset");

                        }

                        int LedgerTable = GridViewDebtor.Rows.Count;
                        for (int i = 0; i < LedgerTable; i++)
                        {
                            Label lbltype = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("lblMaintainType");
                            if (lbltype.Text == "BillByBill")
                            {
                                Label Ledger_ID = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("Ledger_ID");

                                Label LedgerTx_Amount = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("LedgerTx_Amount");
                                string LedgerTxAmount = LedgerTx_Amount.Text;
                                if (lblGrandTotal.Text.Contains("-"))
                                {
                                    LedgerTxAmount = LedgerTxAmount.Replace(@"-", string.Empty);
                                }
                                else
                                {
                                    LedgerTxAmount = "-" + LedgerTxAmount;
                                }

                                int TableId = int.Parse(Ledger_ID.Text);

                                objdb.ByProcedure("SpFinLedgerTx",
                                new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                                new string[] { "0", Ledger_ID.Text, "Main Ledger", VoucherTx_ID, "CreditSale Voucher", LedgerTxAmount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (i + 1).ToString(), "BillByBill" }, "dataset");
                                DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                                DataSet dsBillByBillTemp = new DataSet();
                                dsBillByBillTemp = dsBillByBill;
                                for (int j = 0; j < dsBillByBillTemp.Tables.Count; j++)
                                {
                                    if (dsBillByBillTemp.Tables[j].TableName == TableId.ToString())
                                    {
                                        for (int k = 0; k < dsBillByBillTemp.Tables[j].Rows.Count; k++)
                                        {
                                            string Type = dsBillByBillTemp.Tables[j].Rows[k]["Type"].ToString();
                                            string BillByBillTx_RefType = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_RefType"].ToString();
                                            string BillByBillTx_Ref = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_Ref"].ToString();
                                            string BillByBillTx_Amount = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_Amount"].ToString();
                                            string BillByBillTx_OrderBy = dsBillByBillTemp.Tables[j].Rows[k]["RowNo"].ToString();
                                            if (Type == "Dr")
                                            {
                                                BillByBillTx_Amount = "-" + BillByBillTx_Amount;
                                            }

                                            if (BillByBillTx_RefType.ToString() == "New Ref")
                                            {
                                                objdb.ByProcedure("SpFinBillByBillTx",
                                            new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy", "BillByBillTx_OrderNo", "SchemeTx_ID", "BillByBillTx_OrderDate", "BillByBillTx_ConsigneeName", "LedgerTx_OrderBy" },
                                            new string[] { "3", VoucherTx_ID, Ledger_ID.Text, BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "0", BillByBillTx_OrderBy.ToString(), txtVoucherTx_OrderNo.Text, ddlScheme.SelectedValue.ToString(), VoucherTx_OrderDate, txtNameofConsignee.Text, (i + 1).ToString() }, "dataset");

                                            }
                                            else
                                            {
                                                objdb.ByProcedure("SpFinBillByBillTx",
                                            new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy", "LedgerTx_OrderBy" },
                                            new string[] { "3", VoucherTx_ID, Ledger_ID.Text, BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "0", BillByBillTx_OrderBy.ToString(), (i + 1).ToString() }, "dataset");
                                            }
                                        }
                                    }
                                }
                            }
                            else if (lbltype.Text == "Cheque")
                            {
                                Label Ledger_ID = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("Ledger_ID");

                                Label LedgerTx_Amount = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("LedgerTx_Amount");
                                string LedgerTxAmount = LedgerTx_Amount.Text;
                                if (lblGrandTotal.Text.Contains("-"))
                                {
                                    LedgerTxAmount = LedgerTxAmount.Replace(@"-", string.Empty);
                                }
                                else
                                {
                                    LedgerTxAmount = "-" + LedgerTxAmount;
                                }


                                int TableId = int.Parse(Ledger_ID.Text);

                                objdb.ByProcedure("SpFinLedgerTx",
                                new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                                new string[] { "0", Ledger_ID.Text, "Main Ledger", VoucherTx_ID, "CreditSale Voucher", LedgerTxAmount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (i + 1).ToString(), "Cheque" }, "dataset");
                                DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                                DataSet dsBillByBillTemp = new DataSet();
                                dsBillByBillTemp = dsBillByBill;
                                for (int j = 0; j < dsBillByBillTemp.Tables.Count; j++)
                                {
                                    if (dsBillByBillTemp.Tables[j].TableName == TableId.ToString())
                                    {
                                        for (int k = 0; k < dsBillByBillTemp.Tables[j].Rows.Count; k++)
                                        {
                                            string ChequeTx_No = dsBillByBill.Tables[j].Rows[k]["ChequeTx_No"].ToString();
                                            if (ChequeTx_No == "")
                                            {
                                                ChequeTx_No = null;
                                            }
                                            else
                                            {

                                            }
                                            string ChequeTx_Date = dsBillByBill.Tables[j].Rows[k]["ChequeTx_Date"].ToString();
                                            if (ChequeTx_Date == "")
                                            {
                                                ChequeTx_Date = null;
                                            }
                                            else
                                            {
                                                ChequeTx_Date = Convert.ToDateTime(ChequeTx_Date, cult).ToString("yyyy/MM/dd");
                                            }
                                            string ChequeTx_Amount = dsBillByBill.Tables[j].Rows[k]["ChequeTx_Amount"].ToString();
                                            objdb.ByProcedure("SpFinChequeTx",
                                            new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy" },
                                            new string[] { "1", VoucherTx_ID, Ledger_ID.Text, "CreditSale Voucher", ChequeTx_No, ChequeTx_Date, ChequeTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "0", ViewState["Emp_ID"].ToString(), (k + 1).ToString() }, "dataset");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Label Ledger_ID = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("Ledger_ID");

                                Label LedgerTx_Amount = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("LedgerTx_Amount");

                                string LedgerTxAmount = LedgerTx_Amount.Text;
                                if (lblGrandTotal.Text.Contains("-"))
                                {
                                    LedgerTxAmount = LedgerTxAmount.Replace(@"-", string.Empty);
                                }
                                else
                                {
                                    LedgerTxAmount = "-" + LedgerTxAmount;
                                }



                                // int TableId = int.Parse(Ledger_ID.Text);

                                objdb.ByProcedure("SpFinLedgerTx",
                                new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                                new string[] { "0", Ledger_ID.Text, "Main Ledger", VoucherTx_ID, "CreditSale Voucher", LedgerTxAmount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (i + 1).ToString(), "None" }, "dataset");
                            }

                        }
                        objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "40", VoucherTx_ID }, "dataset");
                    }

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                    ClearText();

                }
                else if (btnAccept.Text == "Update" && ViewState["VoucherTx_ID"].ToString() != "0" && Status == 0)
                {
                    // objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_InsertedBy", "VoucherTx_SalesCenterID", "SchemeTx_ID", "VoucherTx_SoldTo", "VoucherTx_OrderNo", "VoucherTx_RegNo" }, new string[] { "7", ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Sales Voucher", "CreditSale Voucher", txtVoucherTx_No.Text, txtVoucherTx_Ref.Text, txtVoucherTx_Narration.Text, lblGrandTotal.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), ViewState["Emp_ID"].ToString(), ddlSalesCenter.SelectedValue.ToString(), ddlScheme.SelectedValue.ToString(), txtNameofConsignee.Text, txtVoucherTx_OrderNo.Text, txtVoucherTx_RegNo.Text }, "dataset");
                    objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_InsertedBy", "VoucherTx_SalesCenterID", "SchemeTx_ID", "VoucherTx_SoldTo", "VoucherTx_OrderNo", "VoucherTx_RegNo", "VoucherTx_OrderDate" }, new string[] { "7", ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Sales Voucher", "CreditSale Voucher", VoucherTx_No, txtVoucherTx_Ref.Text, txtVoucherTx_Narration.Text, lblGrandTotal.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), ViewState["Emp_ID"].ToString(), "0", Scheme, txtNameofConsignee.Text, txtVoucherTx_OrderNo.Text, txtVoucherTx_RegNo.Text, VoucherTx_OrderDate }, "dataset");
                    objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "1", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                    objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                    int rowItemIndex = 0;
                    int gridItemRows = GridViewItem.Rows.Count;
                    for (rowItemIndex = 0; rowItemIndex < gridItemRows; rowItemIndex++)
                    {
                        Label lblItemRowNo = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblItemRowNo");
                        Label lblItemID = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblItemID");
                        Label lblUnit_id = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblUnit_id");
                        Label lblWarehouse_id = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblWarehouse_id");
                        Label lblHSNCode = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblHSNCode");
                        Label lblItem = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblItem");
                        Label lblQuantity = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblQuantity");
                        Label lblRate = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblRate");
                        Label lblAmount = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblAmount");
                        Label lblCGST = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("CGST");
                        Label lblSGST = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("SGST");
                        Label lblIGST = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("IGST");
                        Label lblCGSTPer = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblCGSTPer");
                        Label lblSGSTPer = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblSGSTPer");
                        Label lblIGSTPer = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblIGSTPer");
                        Label lblLedgerID = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblLedgerID");
                        Label lblTaxbility = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblTaxbility");
                        objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Name", "VoucherTx_Type", "Ledger_ID", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "HSN_Code", "IGST_Per", "CGST_Per", "SGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Warehouse_id", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy", "Taxbility" }, new string[] { "0", ViewState["VoucherTx_ID"].ToString(), "Sales Voucher", "CreditSale Voucher", lblLedgerID.Text, lblItemID.Text, lblUnit_id.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, lblHSNCode.Text, lblIGSTPer.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblCGST.Text, lblSGST.Text, lblIGST.Text, ddlWarehouse.SelectedValue.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), lblItemRowNo.Text, lblTaxbility.Text }, "dataset");
                        ds = objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "Ledger_ID" }, new string[] { "3", lblLedgerID.Text }, "dataset");
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["InventoryAffected"].ToString() == "Yes")
                            {
                                objdb.ByProcedure("SpFinItemTx",
                                    new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "TransactionFrom", "InvoiceNo", "Office_Id", "Warehouse_id", "CreatedBy", "TranDt", "Amount" }
                                   , new string[] { "4", lblItemID.Text, "0", lblQuantity.Text, lblRate.Text, ViewState["VoucherTx_ID"].ToString(), "CreditSale Voucher", VoucherTx_No, ViewState["Office_ID"].ToString(), lblWarehouse_id.Text, ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), lblAmount.Text }, "dataset");
                            }
                        }
                        objdb.ByProcedure("SpFinLedgerTx",
                                new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
                                new string[] { "0", lblLedgerID.Text, "Item Ledger", ViewState["VoucherTx_ID"].ToString(), lblItemID.Text, "CreditSale Voucher", lblAmount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), lblItemRowNo.Text }, "dataset");
                    }

                    /******Insert Dubtable Data to Database**********/
                    InsertSubItemDB(ViewState["VoucherTx_ID"].ToString(), "Sales Voucher", "CreditSale Voucher","1");
                    /******Insert Dubtable Data to Database**********/


                    int gridRows = GridViewLedger.Rows.Count;
                    for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                    {

                        Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                        Label lblHSNCode = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblHSNCode");
                        Label lblCGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTPer");
                        Label lblSGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTPer");
                        Label lblIGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTPer");
                        Label lblCGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTAmt");
                        Label lblSGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTAmt");
                        Label lblIGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTAmt");
                        TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                        Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                        Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                        objdb.ByProcedure("SpFinLedgerTx",
                        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "GSTApplicable", "Taxbility" },
                        new string[] { "0", lblID.Text, "Sub Ledger", ViewState["VoucherTx_ID"].ToString(), "CreditSale Voucher", txtAmount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 2).ToString(), lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, lblGSTApplicable.Text, lblTaxbility.Text }, "dataset");

                    }
                    //DataSet DS_GridViewParticulars = (DataSet)ViewState["DS_GridViewParticulars"];
                    //for (int i = 0; i < DS_GridViewParticulars.Tables.Count; i++)
                    //{
                    //    for (int j = 0; j < DS_GridViewParticulars.Tables[i].Rows.Count; j++)
                    //    {
                    //        string ParticularID = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularID"].ToString();
                    //        string Amount = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularAmt"].ToString();
                    //        string Item_id = DS_GridViewParticulars.Tables[i].Rows[j]["Item_ID"].ToString();
                    //        objdb.ByProcedure("SpFinLedgerTx",
                    //        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
                    //        new string[] { "0", ParticularID, "Item Ledger", ViewState["VoucherTx_ID"].ToString(), Item_id, "CreditSale Voucher", Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (j + 1).ToString() }, "dataset");
                    //    }
                    //}
                    objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "4", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                    objdb.ByProcedure("SpFinChequeTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                    int LedgerTable = GridViewDebtor.Rows.Count;
                    for (int i = 0; i < LedgerTable; i++)
                    {
                        Label lbltype = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("lblMaintainType");
                        if (lbltype.Text == "BillByBill")
                        {
                            Label Ledger_ID = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("Ledger_ID");

                            Label LedgerTx_Amount = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("LedgerTx_Amount");
                            string LedgerTxAmount = LedgerTx_Amount.Text;
                            if (lblGrandTotal.Text.Contains("-"))
                            {
                                LedgerTxAmount = LedgerTxAmount.Replace(@"-", string.Empty);
                            }
                            else
                            {
                                LedgerTxAmount = "-" + LedgerTxAmount;
                            }


                            int TableId = int.Parse(Ledger_ID.Text);

                            objdb.ByProcedure("SpFinLedgerTx",
                            new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                            new string[] { "0", Ledger_ID.Text, "Main Ledger", ViewState["VoucherTx_ID"].ToString(), "CreditSale Voucher", LedgerTxAmount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (i + 1).ToString(), "BillByBill" }, "dataset");
                            DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                            DataSet dsBillByBillTemp = new DataSet();
                            dsBillByBillTemp = dsBillByBill;
                            for (int j = 0; j < dsBillByBillTemp.Tables.Count; j++)
                            {
                                if (dsBillByBillTemp.Tables[j].TableName == TableId.ToString())
                                {
                                    for (int k = 0; k < dsBillByBillTemp.Tables[j].Rows.Count; k++)
                                    {
                                        string Type = dsBillByBillTemp.Tables[j].Rows[k]["Type"].ToString();
                                        string BillByBillTx_RefType = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_RefType"].ToString();
                                        string BillByBillTx_Ref = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_Ref"].ToString();
                                        string BillByBillTx_Amount = dsBillByBillTemp.Tables[j].Rows[k]["BillByBillTx_Amount"].ToString();
                                        string BillByBillTx_OrderBy = dsBillByBillTemp.Tables[j].Rows[k]["RowNo"].ToString();
                                        if (Type == "Dr")
                                        {
                                            BillByBillTx_Amount = "-" + BillByBillTx_Amount;
                                        }

                                        if (BillByBillTx_RefType.ToString() == "New Ref")
                                        {
                                            objdb.ByProcedure("SpFinBillByBillTx",
                                            new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy", "BillByBillTx_OrderNo", "SchemeTx_ID", "BillByBillTx_OrderDate", "BillByBillTx_ConsigneeName", "LedgerTx_OrderBy" },
                                            new string[] { "3", ViewState["VoucherTx_ID"].ToString(), Ledger_ID.Text, BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", BillByBillTx_OrderBy.ToString(), txtVoucherTx_OrderNo.Text, ddlScheme.SelectedValue.ToString(), VoucherTx_OrderDate, txtNameofConsignee.Text, (i + 1).ToString() }, "dataset");
                                        }
                                        else
                                        {
                                            objdb.ByProcedure("SpFinBillByBillTx",
                                            new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy", "LedgerTx_OrderBy" },
                                            new string[] { "3", ViewState["VoucherTx_ID"].ToString(), Ledger_ID.Text, BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", BillByBillTx_OrderBy.ToString(), (i + 1).ToString() }, "dataset");
                                        }
                                    }
                                }
                            }
                        }
                        else if (lbltype.Text == "Cheque")
                        {
                            Label Ledger_ID = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("Ledger_ID");

                            Label LedgerTx_Amount = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("LedgerTx_Amount");
                            string LedgerTxAmount = LedgerTx_Amount.Text;
                            if (lblGrandTotal.Text.Contains("-"))
                            {
                                LedgerTxAmount = LedgerTxAmount.Replace(@"-", string.Empty);
                            }
                            else
                            {
                                LedgerTxAmount = "-" + LedgerTxAmount;
                            }


                            int TableId = int.Parse(Ledger_ID.Text);

                            objdb.ByProcedure("SpFinLedgerTx",
                            new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                            new string[] { "0", Ledger_ID.Text, "Main Ledger", ViewState["VoucherTx_ID"].ToString(), "CreditSale Voucher", LedgerTxAmount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (i + 1).ToString(), "Cheque" }, "dataset");
                            DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                            DataSet dsBillByBillTemp = new DataSet();
                            dsBillByBillTemp = dsBillByBill;
                            for (int j = 0; j < dsBillByBillTemp.Tables.Count; j++)
                            {
                                if (dsBillByBillTemp.Tables[j].TableName == TableId.ToString())
                                {
                                    for (int k = 0; k < dsBillByBillTemp.Tables[j].Rows.Count; k++)
                                    {
                                        string ChequeTx_No = dsBillByBill.Tables[j].Rows[k]["ChequeTx_No"].ToString();
                                        if (ChequeTx_No == "")
                                        {
                                            ChequeTx_No = null;
                                        }
                                        else
                                        {

                                        }
                                        string ChequeTx_Date = dsBillByBill.Tables[j].Rows[k]["ChequeTx_Date"].ToString();
                                        if (ChequeTx_Date == "")
                                        {
                                            ChequeTx_Date = null;
                                        }
                                        else
                                        {
                                            ChequeTx_Date = Convert.ToDateTime(ChequeTx_Date, cult).ToString("yyyy/MM/dd");
                                        }
                                        string ChequeTx_Amount = dsBillByBill.Tables[j].Rows[k]["ChequeTx_Amount"].ToString();
                                        objdb.ByProcedure("SpFinChequeTx",
                                        new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy" },
                                        new string[] { "1", ViewState["VoucherTx_ID"].ToString(), Ledger_ID.Text, "CreditSale Voucher", ChequeTx_No, ChequeTx_Date, ChequeTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), (k + 1).ToString() }, "dataset");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Label Ledger_ID = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("Ledger_ID");

                            Label LedgerTx_Amount = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("LedgerTx_Amount");
                            string LedgerTxAmount = LedgerTx_Amount.Text;
                            if (lblGrandTotal.Text.Contains("-"))
                            {
                                LedgerTxAmount = LedgerTxAmount.Replace(@"-", string.Empty);
                            }
                            else
                            {
                                LedgerTxAmount = "-" + LedgerTxAmount;
                            }


                            // int TableId = int.Parse(Ledger_ID.Text);

                            objdb.ByProcedure("SpFinLedgerTx",
                            new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                            new string[] { "0", Ledger_ID.Text, "Main Ledger", ViewState["VoucherTx_ID"].ToString(), "CreditSale Voucher", LedgerTxAmount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (i + 1).ToString(), "None" }, "dataset");
                        }

                    }
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully."); 

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Voucher No is already exist.');", true);
                    //ClearData();
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
        try
        {
            txtVoucherTx_No.Text = "";
            txtVoucherTx_Ref.Text = "";
            ddlItemName.ClearSelection();
            ddlWarehouse.ClearSelection();
            ddlWarehouse.SelectedIndex = 1;
            txtQuantity.Text = "";
            txtRate.Text = "";
            txtTotalAmount.Text = "";
            GridViewLedger.DataSource = new string[] { };
            GridViewLedger.DataBind();
            GridViewItem.DataSource = new string[] { };
            GridViewItem.DataBind();
            GridViewBillByBillDetail.DataSource = new string[] { };
            GridViewBillByBillDetail.DataBind();
            lblGrandTotal.Text = "0";
            ddlLedger.ClearSelection();
            txtLedgerAmt.Text = "";
            ddlDebitLedger.ClearSelection();
            // ddlSalesCenter.ClearSelection();
            ddlScheme.ClearSelection();
            txtNameofConsignee.Text = "";
            txtVoucherTx_RegNo.Text = "";
            txtVoucherTx_OrderNo.Text = "";
            txtVoucherTx_Narration.Text = "";
            CreateLedgerTable();
            CreateBillByBillDataSet();
            FillVoucherNo();
            AddItem("NA");
            FillItem();
            btnAccept.Enabled = false;
            //btnAdd.Enabled = false;
            chkitem.Checked = false;
            hfvalue.Value = "";
            GetPreviousVoucherNo();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Voucher Detail
    protected void FillDetail()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "8", ViewState["VoucherTx_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string Fstring = "";
                    string Lstring = "";
                    string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
                    if (ds.Tables[0].Rows[0]["Office_ID"].ToString() == "1")
                    {

                        Fstring = str.Substring(0, 9);
                        Lstring = str.Substring(9, str.Length - 9);


                    }
                    else
                    {
                        Fstring = str.Substring(0, 10);
                        Lstring = str.Substring(10, str.Length - 10);
                    }
                    //txtVoucherTx_No.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    txtVoucherTx_No.Text = Lstring;
                    lblVoucherTx_No.Text = Fstring;
                    //var rx = new System.Text.RegularExpressions.Regex("BN");
                    //string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //var array = rx.Split(str);
                    //lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //txtVoucherTx_No.Text = array[1];
                    //lblVoucherTx_No.Text = array[0] + "BN";
                    ViewState["VoucherTx_Date"] = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    ViewState["FY"] = ds.Tables[0].Rows[0]["VoucherTx_FY"].ToString();
                    txtVoucherTx_Ref.Text = ds.Tables[0].Rows[0]["VoucherTx_Ref"].ToString();
                    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    lblGrandTotal.Text = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                    //ddlSalesCenter.ClearSelection();
                    //ddlSalesCenter.Items.FindByValue(ds.Tables[0].Rows[0]["VoucherTx_SalesCenterID"].ToString()).Selected = true;
                    ddlScheme.ClearSelection();
                    ddlScheme.Items.FindByValue(ds.Tables[0].Rows[0]["SchemeTx_ID"].ToString()).Selected = true;
                    txtNameofConsignee.Text = ds.Tables[0].Rows[0]["VoucherTx_SoldTo"].ToString();
                    txtVoucherTx_RegNo.Text = ds.Tables[0].Rows[0]["VoucherTx_RegNo"].ToString();
                    txtVoucherTx_OrderNo.Text = ds.Tables[0].Rows[0]["VoucherTx_OrderNo"].ToString();
                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
                    txtVoucherTx_OrderDate.Text = ds.Tables[0].Rows[0]["VoucherTx_OrderDate"].ToString();

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    chkitem.Checked = true;
                    btnAdd.Enabled = true;
                    GridViewItem.DataSource = ds.Tables[1];
                    GridViewItem.DataBind();
                    if (GridViewItem.Rows.Count > 0)
                    {
                        decimal TAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                        decimal CGSTAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                        decimal SGSTTAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                        GridViewItem.FooterRow.Cells[4].Text = "<b>Total : </b>";
                        GridViewItem.FooterRow.Cells[5].Text = "<b>" + TAmount.ToString() + "</b>";
                        GridViewItem.FooterRow.Cells[6].Text = "<b>" + CGSTAmount.ToString() + "</b>";
                        GridViewItem.FooterRow.Cells[7].Text = "<b>" + SGSTTAmount.ToString() + "</b>";
                        GridViewItem.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                        GridViewItem.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                        GridViewItem.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    }
                    string OfficeID = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                    if (OfficeID.ToString() == Session["Office_ID"].ToString())
                    {
                        ddlWarehouse.ClearSelection();
                        ddlWarehouse.Items.FindByValue(ds.Tables[1].Rows[0]["Warehouse_id"].ToString()).Selected = true;
                        ddlWarehouse.Visible = true;
                        txtWareHouse.Visible = false;
                    }
                    else
                    {
                        ddlWarehouse.Visible = false;
                        txtWareHouse.Visible = true;
                        txtWareHouse.Text = ds.Tables[1].Rows[0]["WarehouseName"].ToString();
                    }

                }
                if (ds.Tables[2].Rows.Count > 0)
                {

                    GridViewDebtor.DataSource = ds.Tables[2];
                    GridViewDebtor.DataBind();

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        decimal Amount = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));
                        ViewState["Amount"] = Amount;
                        //hfvalue.Value = ViewState["Amount"].ToString();

                        GridViewDebtor.FooterRow.Cells[2].Text = "<b>| TOTAL |</b> ";
                        GridViewDebtor.FooterRow.Cells[3].Text = "<b>" + Amount.ToString() + "</b>";
                        GridViewDebtor.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    }

                    DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                    int rowcount = ds.Tables[2].Rows.Count;
                    for (int i = 0; i < rowcount; i++)
                    {
                        string Ledger_ID = ds.Tables[2].Rows[i]["Ledger_ID"].ToString();
                        string Ledger_Name = ds.Tables[2].Rows[i]["Ledger_Name"].ToString();
                        string LedgerTx_Amount = ds.Tables[2].Rows[i]["LedgerTx_Amount"].ToString();
                        string LedgerTx_MaintainType = ds.Tables[2].Rows[i]["LedgerTx_MaintainType"].ToString();


                        dt_LedgerTable.Rows.Add(Ledger_ID, Ledger_Name, LedgerTx_Amount, LedgerTx_MaintainType);
                        ViewState["LedgerTable"] = dt_LedgerTable;
                        decimal LedgerTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));
                        ViewState["LedgerTotal"] = LedgerTotal;
                        hfvalue.Value = ViewState["LedgerTotal"].ToString();
                    }

                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    DataTable dt_GridViewLedger = (DataTable)ViewState["LedgerAmount"];
                    dt_GridViewLedger = ds.Tables[3];
                    ViewState["LedgerAmount"] = dt_GridViewLedger;
                    GridViewLedger.DataSource = ds.Tables[3];
                    GridViewLedger.DataBind();



                }
                if (ds.Tables[5].Rows.Count > 0)
                {


                    //DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                    //int rowscount = ds.Tables[5].Rows.Count;
                    //for (int i = 0; i < rowscount; i++)
                    //{

                    //    string Ledger_ID = ds.Tables[5].Rows[i]["Ledger_ID"].ToString();
                    //    string BillByBillTx_RefType = ds.Tables[5].Rows[i]["BillByBillTx_RefType"].ToString();
                    //    string BillByBillTx_Ref = ds.Tables[5].Rows[i]["BillByBillTx_Ref"].ToString();
                    //    string BillByBillTx_Amount = ds.Tables[5].Rows[i]["BillByBillTx_Amount"].ToString();
                    //    string Type = ds.Tables[5].Rows[i]["BillByBillTxType"].ToString();
                    //    string TNO = Ledger_ID.ToString();
                    //    DataTable dt_BillByBillTable = new DataTable(TNO);
                    //    DataColumn RowNo = dt_BillByBillTable.Columns.Add("RowNo", typeof(int));
                    //    dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
                    //    dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
                    //    dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
                    //    dt_BillByBillTable.Columns.Add(new DataColumn("Type", typeof(string)));
                    //    dt_BillByBillTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
                    //    RowNo.AutoIncrement = true;
                    //    RowNo.AutoIncrementSeed = 1;
                    //    RowNo.AutoIncrementStep = 1;

                    //    dt_BillByBillTable.Rows.Add(null,BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Type, Ledger_ID);
                    //    dsBillByBill.Merge(dt_BillByBillTable);
                    //    ViewState["BillByBillTable"] = dt_BillByBillTable;
                    //    ViewState["dsBillByBill"] = dsBillByBill;


                    //}
                    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];

                    int Legrowcount = ds.Tables[2].Rows.Count;
                    for (int Leg = 0; Leg < Legrowcount; Leg++)
                    {
                        string TNO = ds.Tables[2].Rows[Leg]["Ledger_ID"].ToString();
                        if (ds.Tables[2].Rows[Leg]["LedgerTx_MaintainType"].ToString() == "BillByBill")
                        {

                            DataTable dt_BillByBillTable = new DataTable(TNO);
                            DataColumn RowNo = dt_BillByBillTable.Columns.Add("RowNo", typeof(int));
                            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
                            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
                            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
                            dt_BillByBillTable.Columns.Add(new DataColumn("Type", typeof(string)));
                            dt_BillByBillTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));

                            RowNo.AutoIncrement = true;
                            RowNo.AutoIncrementSeed = 1;
                            RowNo.AutoIncrementStep = 1;

                            int rowscount = ds.Tables[5].Rows.Count;
                            for (int i = 0; i < rowscount; i++)
                            {
                                if (TNO == ds.Tables[5].Rows[i]["Ledger_ID"].ToString())
                                {
                                    string Ledger_ID = ds.Tables[5].Rows[i]["Ledger_ID"].ToString();
                                    string BillByBillTx_RefType = ds.Tables[5].Rows[i]["BillByBillTx_RefType"].ToString();
                                    string BillByBillTx_Ref = ds.Tables[5].Rows[i]["BillByBillTx_Ref"].ToString();
                                    string BillByBillTx_Amount = ds.Tables[5].Rows[i]["BillByBillTx_Amount"].ToString();
                                    string Type = ds.Tables[5].Rows[i]["BillByBillTxType"].ToString();


                                    // dt_BillByBillTable.Rows.Add(null, Ledger_ID, BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Type);
                                    dt_BillByBillTable.Rows.Add(null, BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Type, Ledger_ID);

                                }
                            }
                            if (dt_BillByBillTable.Rows.Count > 0)
                            {
                                dsBillByBill.Merge(dt_BillByBillTable);
                                ViewState["BillByBillTable"] = dt_BillByBillTable;
                                ViewState["dsBillByBill"] = dsBillByBill;


                            }
                        }
                    }
                }
                if (ds.Tables[6].Rows.Count > 0)
                {

                    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                    int rowscount = ds.Tables[6].Rows.Count;
                    for (int i = 0; i < rowscount; i++)
                    {

                        string Ledger_ID = ds.Tables[6].Rows[i]["Ledger_ID"].ToString();
                        string ChequeTx_No = ds.Tables[6].Rows[i]["ChequeTx_No"].ToString();
                        string ChequeTx_Date = ds.Tables[6].Rows[i]["ChequeTx_Date"].ToString();
                        string ChequeTx_Amount = ds.Tables[6].Rows[i]["ChequeTx_Amount"].ToString();
                        string TNO = Ledger_ID.ToString();
                        DataTable dt_FinChequeTx = new DataTable(TNO);
                        dt_FinChequeTx.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
                        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_No", typeof(string)));
                        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Date", typeof(string)));
                        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Amount", typeof(decimal)));
                        dt_FinChequeTx.Rows.Add(Ledger_ID, ChequeTx_No, ChequeTx_Date, ChequeTx_Amount);
                        dsBillByBill.Merge(dt_FinChequeTx);
                        ViewState["FinChequeTx"] = dt_FinChequeTx;
                        ViewState["dsBillByBill"] = dsBillByBill;
                    }


                }
                if (ds.Tables[7].Rows.Count > 0)
                {
                    txtVoucherTx_Sado.Text = ds.Tables[7].Rows[0]["Sado"].ToString();
                    txtVoucherTx_Farmer.Text = ds.Tables[7].Rows[0]["Farmer"].ToString();
                    txtVoucherTx_Gram.Text = ds.Tables[7].Rows[0]["Gram"].ToString();
                    txtVoucherTx_OrderNo.Text = ds.Tables[7].Rows[0]["OrderNo"].ToString();
                    txtVoucherTx_OrderDate.Text = ds.Tables[7].Rows[0]["OrderDate"].ToString();
                    ddlScheme.Items.FindByText(ds.Tables[7].Rows[0]["OrderScheme"].ToString()).Selected = true;
                    txtVoucherTx_AdvanceAmt.Text = ds.Tables[7].Rows[0]["AdvanceAmount"].ToString();
                    ddlAdvanceCr.Items.FindByText(ds.Tables[7].Rows[0]["Advance_Cr_Type"].ToString()).Selected = true;
                    txtVoucherTx_MRDetails.Text = ds.Tables[7].Rows[0]["Advance_Cr_Details"].ToString();
                    txtVoucherTx_RegNo.Text = ds.Tables[7].Rows[0]["RegNo"].ToString();
                }
                btnAccept.Text = "Update";
                btn_Clear.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                lnkPreviousVoucher.Visible = false;
                //btnAccept.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Add New Ledger
    protected void lbkbtnAddLedger_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["Office_ID"].ToString() == "1")
            {
                Response.Redirect("LedgerMasterB.aspx");
            }
            else
            {
                Response.Redirect("LedgerMaster_Forotherofc.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //View VoucherDetail
    protected void ViewVoucher()
    {
        try
        {
            lblVoucherTx_No.Visible = false;
            txtVoucherTx_No.Visible = false;
            lblVoucherNo.Visible = true;
            btnAccept.Visible = false;
            btn_Clear.Visible = false;
            divamount.Visible = false;
            divitem.Visible = false;
            divdebtor.Visible = false;
            panel1.Enabled = false;
            panel2.Enabled = false;
            lbkbtnAddLedger.Visible = false;
            GridViewItem.Columns[10].Visible = false;
            GridViewDebtor.Columns[4].Visible = true;
            GridViewDebtor.Columns[5].Visible = false;
            foreach (GridViewRow rows in GridViewLedger.Rows)
            {
                LinkButton delete = (LinkButton)rows.FindControl("Delete");
                delete.Visible = false;
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void txtVoucherTx_Date_TextChanged(object sender, EventArgs e)
    {
        string ValidStatus = ValidDate();
        if (ValidStatus == "No")
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('You are not allowed to choose this date, please contact to head office.');", true);
            FillVoucherDate();
        }
        else
        {
            if (ViewState["VoucherTx_ID"].ToString() == "0")
            {
                FillVoucherNo();
            }
            else
            {
                string sDate = (Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd")).ToString();
                DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
                int Month = int.Parse(datevalue.Month.ToString());
                int Year = int.Parse(datevalue.Year.ToString());
                int FY = Year;
                string FinancialYear = Year.ToString();
                string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
                FinancialYear = "";
                if (Month <= 3)
                {
                    FY = Year - 1;
                    FinancialYear = FY.ToString() + "-" + LFY.ToString();
                }
                else
                {

                    FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
                }
                if (ViewState["FY"].ToString() == FinancialYear.ToString())
                {
                    FillVoucherNo();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Date selection should be according to Financial Year(" + ViewState["FY"].ToString() + ")');", true);
                    txtVoucherTx_Date.Text = ViewState["VoucherTx_Date"].ToString();
                }
            }
        }

    }
    //protected void txtQuantity_TextChanged(object sender, EventArgs e)
    //{
    //    decimal Quantity = decimal.Parse(txtQuantity.Text);
    //    ds = objdb.ByProcedure("Proc_tblPuSalesOrder", new string[] { "flag", "Office_ID", "Item_id", "Warehouse_id" }, new string[] { "10", ViewState["Office_ID"].ToString(), ddlItemName.SelectedValue.ToString(), ddlWarehouse.SelectedValue.ToString() }, "dataset");
    //    if (ds != null)
    //    {
    //        Decimal AvailableStock = decimal.Parse(ds.Tables[0].Rows[0]["AvailableStock"].ToString());
    //        if (AvailableStock < Quantity)
    //        {
    //            decimal NegativeSale = AvailableStock - Quantity;
    //            string UQCCode = ds.Tables[1].Rows[0]["UQCCode"].ToString();
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Warning Negative Stock (" + NegativeSale + ")" + UQCCode + "');", true);
    //        }

    //    }
    //}
    //protected void chkitem_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (chkitem.Checked == true)
    //        {
    //            pnlitem.Enabled = true;
    //        }
    //        else
    //        {
    //            pnlitem.Enabled = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void lnkPreviousVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "VoucherTx_Type" }, new string[] { "33", ViewState["Office_ID"].ToString(), "CreditSale Voucher" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                CreateLedgerTable();
                string VoucherID = ds.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
                ViewState["VoucherTx_ID"] = VoucherID.ToString();
                FillPreviousDetail();
                ViewState["VoucherTx_ID"] = "0";



            }
            lnkPreviousVoucher.Visible = true;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Fill Previous VoucherNarration
    protected void btnNarration_Click(object sender, EventArgs e)
    {
        ds = objdb.ByProcedure("SpFinVoucherTx",
                 new string[] { "flag", "VoucherTx_Type", "Office_ID" },
                 new string[] { "14", "CreditSale Voucher", ViewState["Office_ID"].ToString() }, "dataset");

        if (ds.Tables[0].Rows.Count != 0)
        {
            txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
        }
    }

    //Fill Previous Voucher Detail
    protected void FillPreviousDetail()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "8", ViewState["VoucherTx_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    var rx = new System.Text.RegularExpressions.Regex("BN");
                    string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    var array = rx.Split(str);
                    lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    txtVoucherTx_No.Text = array[1];
                    lblVoucherTx_No.Text = array[0] + "BN";
                    ViewState["VoucherTx_Date"] = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    ViewState["FY"] = ds.Tables[0].Rows[0]["VoucherTx_FY"].ToString();
                    txtVoucherTx_Ref.Text = ds.Tables[0].Rows[0]["VoucherTx_Ref"].ToString();
                    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    lblGrandTotal.Text = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                    ddlScheme.ClearSelection();
                    ddlScheme.Items.FindByValue(ds.Tables[0].Rows[0]["SchemeTx_ID"].ToString()).Selected = true;
                    txtNameofConsignee.Text = ds.Tables[0].Rows[0]["VoucherTx_SoldTo"].ToString();
                    txtVoucherTx_RegNo.Text = ds.Tables[0].Rows[0]["VoucherTx_RegNo"].ToString();
                    txtVoucherTx_OrderNo.Text = ds.Tables[0].Rows[0]["VoucherTx_OrderNo"].ToString();
                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
                    txtVoucherTx_OrderDate.Text = ds.Tables[0].Rows[0]["VoucherTx_OrderDate"].ToString();

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    chkitem.Checked = true;
                    pnlitem.Enabled = true;
                    GridViewItem.DataSource = ds.Tables[1];
                    GridViewItem.DataBind();
                    if (GridViewItem.Rows.Count > 0)
                    {
                        decimal TAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                        decimal CGSTAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                        decimal SGSTTAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                        GridViewItem.FooterRow.Cells[4].Text = "<b>Total : </b>";
                        GridViewItem.FooterRow.Cells[5].Text = "<b>" + TAmount.ToString() + "</b>";
                        GridViewItem.FooterRow.Cells[6].Text = "<b>" + CGSTAmount.ToString() + "</b>";
                        GridViewItem.FooterRow.Cells[7].Text = "<b>" + SGSTTAmount.ToString() + "</b>";
                        GridViewItem.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                        GridViewItem.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                        GridViewItem.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    }
                    string OfficeID = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                    if (OfficeID.ToString() == Session["Office_ID"].ToString())
                    {
                        ddlWarehouse.ClearSelection();
                        ddlWarehouse.Items.FindByValue(ds.Tables[1].Rows[0]["Warehouse_id"].ToString()).Selected = true;
                        ddlWarehouse.Visible = true;
                        txtWareHouse.Visible = false;
                    }
                    else
                    {
                        ddlWarehouse.Visible = false;
                        txtWareHouse.Visible = true;
                        txtWareHouse.Text = ds.Tables[1].Rows[0]["WarehouseName"].ToString();
                    }

                }

                if (ds.Tables[3].Rows.Count > 0)
                {
                    DataTable dt_GridViewLedger = (DataTable)ViewState["LedgerAmount"];
                    dt_GridViewLedger = ds.Tables[3];
                    ViewState["LedgerAmount"] = dt_GridViewLedger;
                    GridViewLedger.DataSource = ds.Tables[3];
                    GridViewLedger.DataBind();

                }

                btnAccept.Text = "Accept";
                btnAccept.Enabled = true;
                btn_Clear.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);

                //btnAccept.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //GetPreviousVoucherNo
    protected void GetPreviousVoucherNo()
    {
        try
        {
            //lblMsg.Text = "";
            lblPreviousVoucherNo.Text = "";
            string sDate = (Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd")).ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            int Month = int.Parse(datevalue.Month.ToString());
            int Year = int.Parse(datevalue.Year.ToString());
            int FY = Year;
            string FinancialYear = Year.ToString();
            string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
            FinancialYear = "";
            if (Month <= 3)
            {
                FY = Year - 1;
                FinancialYear = FY.ToString() + "-" + LFY.ToString();
            }
            else
            {

                FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
            }
            string VoucherTx_Type = "CreditSale Voucher";
            DataSet ds = objdb.ByProcedure("SpFinVoucherTx",
                new string[] { "flag", "Office_ID", "VoucherTx_FY", "VoucherTx_Type" },
                new string[] { "39", ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_Type }, "dataset");
            //ds = objdb.ByProcedure("", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblPreviousVoucherNo.Text = "(Previous VoucherNo :" + " " + ds.Tables[0].Rows[0]["VoucherTx_No"].ToString() + ")";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected string ValidDate()
    {
        string validDays = "No";
        if (txtVoucherTx_Date.Text != "")
        {
            string sDate = (Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd")).ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            int Month = int.Parse(datevalue.Month.ToString());
            int Year = int.Parse(datevalue.Year.ToString());
            int FY = Year;
            string FinancialYear = Year.ToString();
            string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
            FinancialYear = "";
            if (Month <= 3)
            {
                FY = Year - 1;
                FinancialYear = FY.ToString() + "-" + LFY.ToString();
            }
            else
            {

                FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
            }

            DataSet dsValidDate = objdb.ByProcedure("SpFinEditRight", new string[] { "flag", "Office_ID", "VoucherDate", "FinancialYear" }, new string[] { "3", ViewState["Office_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), FinancialYear }, "dataset");
            if (dsValidDate.Tables.Count != 0 && dsValidDate.Tables[0].Rows.Count != 0)
            {
                validDays = dsValidDate.Tables[0].Rows[0]["ValidStatus"].ToString();
            }
        }
        return validDays;
    }
    protected string ItemAvailableStock(string Qauntity, string OfficeID, string ItemID, string WarehouseID)
    {
        string ItemAvailableStock = "";


        if (Qauntity == "")
        {
            Qauntity = "0";
        }
        decimal ItemQuantity = decimal.Parse(Qauntity);
        ds = objdb.ByProcedure("Proc_tblPuSalesOrder",
                               new string[] { "flag", "Office_ID", "Item_id", "Warehouse_id" },
                               new string[] { "10", OfficeID, ItemID, WarehouseID }
                               , "dataset");
        if (ds != null)
        {
            Decimal AvailableStock = decimal.Parse(ds.Tables[0].Rows[0]["AvailableStock"].ToString());
            if (AvailableStock < ItemQuantity)
            {
                decimal NegativeSale = AvailableStock - ItemQuantity;
                string UQCCode = ds.Tables[1].Rows[0]["UQCCode"].ToString();
                ItemAvailableStock = "Warning Negative Stock (" + NegativeSale + UQCCode + ")";

            }

        }
        return ItemAvailableStock;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string Availablestock(string Qauntity, string OfficeID, string ItemID, string WarehouseID)
    {
        mis_Finance_VoucherSaleCreditQR OBJ = new mis_Finance_VoucherSaleCreditQR();
        string Availablestock = OBJ.ItemAvailableStock(Qauntity, OfficeID, ItemID, WarehouseID);
        return Availablestock;
    }
    protected void GridViewBillByBillDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ID = e.CommandArgument.ToString();
        if (e.CommandName == "BillByBillDelete")
        {
            string LedgerAmount = BillAmount(ID);
            ManageBillByBill(LedgerAmount);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);

        }
    }
    protected void ManageBillByBill(string LedgerAmount)
    {
        decimal Status = decimal.Parse(ViewState["DebtorAmount"].ToString()) - decimal.Parse(LedgerAmount.ToString());
        if (Status == 0)
        {
            ViewState["GrandTotal"] = lblGrandTotal.Text;
            DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
            if (ViewState["action"].ToString() == "Edit")
            {
                DataSet dsBillByBillTemp = new DataSet();
                dsBillByBillTemp = dsBillByBill;
                for (int i = 0; i < dsBillByBillTemp.Tables.Count; i++)
                {
                    if (dsBillByBillTemp.Tables[i].TableName == ViewState["RowNo"].ToString())
                    {
                        dsBillByBill.Tables.Remove(dsBillByBillTemp.Tables[i].TableName);
                        //dsBillByBill.Tables[i].Merge(dsBillByBillTemp.Tables[i]);
                    }
                }

            }
            dsBillByBill.Merge((DataTable)ViewState["BillByBillTable"]);
            ViewState["dsBillByBill"] = dsBillByBill;

            if (ViewState["action"].ToString() != "Edit")
            {
                DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                dt_LedgerTable.Rows.Add(ddlDebitLedger.SelectedValue.ToString(), ddlDebitLedger.SelectedItem.Text, txDebtorAmt.Text, "BillByBill");

                GridViewDebtor.DataSource = dt_LedgerTable;
                GridViewDebtor.DataBind();

                if (dt_LedgerTable.Rows.Count > 0)
                {
                    decimal Amount = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));

                    GridViewDebtor.FooterRow.Cells[2].Text = "<b>| TOTAL |</b>";
                    GridViewDebtor.FooterRow.Cells[3].Text = "<b>" + Amount.ToString() + "</b>";
                    GridViewDebtor.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                }


                decimal LedgerTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Amount"));
                ViewState["LedgerTotal"] = LedgerTotal;
                hfvalue.Value = ViewState["LedgerTotal"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);

                ClearBillByBillModal();
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);
            txtBillByBillTx_Ref.Visible = true;
            txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
            if (Status.ToString().Contains("-"))
            {

                txtBillByBillTx_Amount.Text = Status.ToString();
                txtBillByBillTx_Amount.Text = txtBillByBillTx_Amount.Text.Replace(@"-", string.Empty);

            }
            else
            {
                txtBillByBillTx_Amount.Text = Status.ToString();
            }
            //BindBillByBillData();
            //ddlRefType.ClearSelection();
            //ddlBillByBillTx_crdr.ClearSelection();
            if (Status.ToString().Contains("-"))
            {

                ddlBillByBillTx_crdr.SelectedValue = "Dr";


            }
            else
            {
                ddlBillByBillTx_crdr.SelectedValue = "Cr";
            }

            ddlBillByBillTx_Ref.ClearSelection();
            if (ddlRefType.SelectedValue == "1")
            {
                ddlRefType.SelectedValue = "1";
                lnkView.Visible = true;
                txtBillByBillTx_Ref.Visible = false;
                ddlBillByBillTx_Ref.Visible = true;
            }
            else
            {
                lnkView.Visible = false;
                txtBillByBillTx_Ref.Visible = true;
                ddlBillByBillTx_Ref.Visible = false;
            }
        }
    }
    protected void lnkbtnView_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "View";
    }
    protected void lnkbtnEdit_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "Edit";
    }


    protected void CreateTableFinSubItem()
    {
        try
        {
            DataTable dt_GridViewItem_Ingredient = new DataTable();
            dt_GridViewItem_Ingredient.Columns.Add(new DataColumn("Item_id", typeof(string)));
            dt_GridViewItem_Ingredient.Columns.Add(new DataColumn("Ingredient_id", typeof(string)));
            dt_GridViewItem_Ingredient.Columns.Add(new DataColumn("Quantity", typeof(decimal)));
            dt_GridViewItem_Ingredient.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt_GridViewItem_Ingredient.Columns.Add(new DataColumn("Rate", typeof(decimal)));
            dt_GridViewItem_Ingredient.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            ViewState["dt_GridViewItem_Ingredient"] = dt_GridViewItem_Ingredient;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnAddSubItem_Click(object sender, EventArgs e)
    {
        try
        {
            //AddSubItem();
            btnAdd_Click_Action();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    private void AddSubItem()
    {
        try
        {
            //DataSet dsTableFinSubItem = (DataSet)ViewState["dsTableFinSubItem"];
            string TNO = ddlItemName.SelectedValue.ToString();
            DataTable dt_GridViewItem_Ingredient = (DataTable)ViewState["dt_GridViewItem_Ingredient"];
            foreach (GridViewRow row in GVFinItemIngredientTx.Rows)
            {
                Label Ingredient_Name = (Label)row.FindControl("lblIngredient_Name");
                string Ingredient_ID = Ingredient_Name.ToolTip.ToString();
                TextBox SubItem_Quantity = (TextBox)row.FindControl("txtSubItem_Quantity");
                Label SubItem_Unit = (Label)row.FindControl("lblSubItem_Unit");
                TextBox SubItem_Rate = (TextBox)row.FindControl("txtSubItem_Rate");
                TextBox SubItem_Amount = (TextBox)row.FindControl("txtSubItem_Amount");
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect.Checked == true)
                {
                    dt_GridViewItem_Ingredient.Rows.Add(TNO, Ingredient_ID, SubItem_Quantity.Text, SubItem_Unit.Text, SubItem_Rate.Text, SubItem_Amount.Text);
                }
            }
            ViewState["dt_GridViewItem_Ingredient"] = dt_GridViewItem_Ingredient;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    private void DeleteSubItem(string itemid)
    {
        try
        {
            DataTable newdt_GridViewItem_Ingredient = new DataTable();
            newdt_GridViewItem_Ingredient.Columns.Add(new DataColumn("Item_id", typeof(string)));
            newdt_GridViewItem_Ingredient.Columns.Add(new DataColumn("Ingredient_id", typeof(string)));
            newdt_GridViewItem_Ingredient.Columns.Add(new DataColumn("Quantity", typeof(decimal)));
            newdt_GridViewItem_Ingredient.Columns.Add(new DataColumn("Unit", typeof(string)));
            newdt_GridViewItem_Ingredient.Columns.Add(new DataColumn("Rate", typeof(decimal)));
            newdt_GridViewItem_Ingredient.Columns.Add(new DataColumn("Amount", typeof(decimal)));

            /***************/
            DataTable dt_GridViewItem_Ingredient = (DataTable)ViewState["dt_GridViewItem_Ingredient"];  
            foreach (DataRow row in dt_GridViewItem_Ingredient.Rows)
            {
                string TNO = row["Item_id"].ToString();
                string Ingredient_ID = row["Ingredient_id"].ToString();
                string SubItem_Quantity = row["Quantity"].ToString();
                string SubItem_Unit = row["Unit"].ToString();
                string SubItem_Rate = row["Rate"].ToString();
                string SubItem_Amount = row["Amount"].ToString();
                if (TNO.ToString() != itemid.Trim())
                {
                    newdt_GridViewItem_Ingredient.Rows.Add(TNO, Ingredient_ID, SubItem_Quantity, SubItem_Unit, SubItem_Rate, SubItem_Amount);
                }
            }
            ViewState["dt_GridViewItem_Ingredient"] = newdt_GridViewItem_Ingredient;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    private void InsertSubItemDB(string VoucherTx_ID, string VoucherTx_Name, string VoucherTx_Type,string Edit)
    {
        try
        {
            //DataTable dt_GridViewItem_Ingredient = (DataTable)ViewState["dt_GridViewItem_Ingredient"];
            //foreach (DataRow row in dt_GridViewItem_Ingredient.Rows)
            //{
            //    string item_id = row["Item_id"].ToString();
            //    string Ingredient_id = row["Ingredient_id"].ToString();
            //    string Quantity = row["Quantity"].ToString();
            //    string Unit = row["Unit"].ToString();
            //    string Rate = row["Rate"].ToString();
            //    string Amount = row["Amount"].ToString();
            //    /******Call Insert Query*****/           
            //         ds = objdb.ByProcedure("SpFinItemIngredientTx",
            //                   new string[] { "flag", "VoucherTx_ID", "VoucherTx_Name", "VoucherTx_Type", "Item_id", "Ingredient_id", "Quantity", "Rate", "Amount", "Office_ID", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_Unit" },
            //                   new string[] { "0", VoucherTx_ID.ToString(), VoucherTx_Name.ToString(), VoucherTx_Type.ToString(), item_id.ToString(), Ingredient_id.ToString(), Quantity.ToString(), Rate.ToString(), Amount.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), Unit.ToString() }
            //                   , "dataset");
               

            //}

            /****************************/
            string SADO = txtVoucherTx_Sado.Text;
            string Farmer = txtVoucherTx_Farmer.Text;
            string Gram = txtVoucherTx_Gram.Text;
            string OrderNo = txtVoucherTx_OrderNo.Text;
            string OrderDate = txtVoucherTx_OrderDate.Text;
            string scheme = ddlScheme.SelectedItem.ToString();
            string advanceAmount = txtVoucherTx_AdvanceAmt.Text;
            string Advance_Cr_Type = ddlAdvanceCr.SelectedItem.ToString();
            string Advance_Cr_Details = txtVoucherTx_MRDetails.Text;
            string regNo = txtVoucherTx_RegNo.Text;

            if (Edit == "0")
            {
                ds = objdb.ByProcedure("SpFinItemIngredientTx",
                              new string[] { "flag", "VoucherTx_ID", "Gram", "Sado", "OrderDate", "OrderNo", "OrderScheme", "RegNo", "Advance_Cr_Type", "Advance_Cr_Details", "AdvanceAmount", "Farmer" },
                              new string[] { "2", VoucherTx_ID.ToString(), Gram.ToString(), SADO.ToString(), Convert.ToDateTime(OrderDate, cult).ToString("yyyy/MM/dd"), OrderNo, scheme.ToString(), regNo.ToString(), Advance_Cr_Type.ToString(), Advance_Cr_Details, advanceAmount.ToString(), Farmer }
                              , "dataset");

            }
            else if (Edit == "1")
            {
                ds = objdb.ByProcedure("SpFinItemIngredientTx",
                              new string[] { "flag", "VoucherTx_ID", "Gram", "Sado", "OrderDate", "OrderNo", "OrderScheme", "RegNo", "Advance_Cr_Type", "Advance_Cr_Details", "AdvanceAmount", "Farmer" },
                              new string[] { "11", VoucherTx_ID.ToString(), Gram.ToString(), SADO.ToString(), Convert.ToDateTime(OrderDate, cult).ToString("yyyy/MM/dd"), OrderNo, scheme.ToString(), regNo.ToString(), Advance_Cr_Type.ToString(), Advance_Cr_Details, advanceAmount.ToString(), Farmer }
                              , "dataset");

            }

            /****************************/

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

}