using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_VoucherSaleCash : System.Web.UI.Page
{
    DataSet ds;
    // static DataSet DS_GridViewParticulars = new DataSet();
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //ViewState["CGST"] = "1";
                //ViewState["SGST"] = "2";
                ViewState["RoundOff"] = "3";
                // txtTotalAmount.Attributes.Add("readonly", "readonly");
                txtTotalAmount.ReadOnly = true;
                txtQuantity.ReadOnly = true;
                txtRate.ReadOnly = true;
                if (Session["Emp_ID"] != null)
                {
                    ViewState["VoucherTx_ID"] = "0";
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Action"] = "";
                    FillDropDown();
                    FillItem();
                    FillVoucherDate();
                    
                    FillWareHouse();
                    AddItem("NA");
                    ViewState["RowNo"] = "0";
                    txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
                   
                    lblGrandTotal.Attributes.Add("readonly", "readonly");
                    
                    GridViewItem.DataSource = new string[] { };
                    GridViewItem.DataBind();
                   
                    FillSchemeDropDown();                  
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

                }

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
            ViewState["CGST"] = "1";
            ViewState["SGST"] = "2";
            ////start done changes by Mohini on 13April2022
            //string[] FinY = FinancialYear.Split('-');
            //if (int.Parse(FinY[0]) >= 2022)
            //{
            //    ViewState["CGST"] = "46721";
            //    ViewState["SGST"] = "46722";

            //}
            //else
            //{
            //    ViewState["CGST"] = "1";
            //    ViewState["SGST"] = "2";
            //}
            //end done changes by Mohini on 13April2022
            //string VoucherTx_Names_ForSno = "'Payment,Journal,Contra'";
            //string VoucherTx_Names_ForSno = "'Receipt'";
            //string VoucherTx_Names_ForSno = "'Purchase Voucher'";
            string VoucherTx_Names_ForSno = "Sales Voucher";

            DataSet ds1 = objdb.ByProcedure("SpFinVoucherTx",
                new string[] { "flag", "Office_ID", "VoucherTx_FY", "VoucherTx_Names_ForSno" },
                new string[] { "13", ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_Names_ForSno }, "dataset");

            int VoucherTx_SNo = 0;
            if (ds1.Tables[0].Rows.Count != 0)
            {
                VoucherTx_SNo = Convert.ToInt32(ds1.Tables[0].Rows[0]["VoucherTx_SNo"].ToString());

            }
            VoucherTx_SNo++;
            ViewState["VoucherTx_SNo"] = VoucherTx_SNo;
            string Office_Code = "";
            if (ds1.Tables[1].Rows.Count != 0)
            {
                Office_Code = ds1.Tables[1].Rows[0]["Office_Code"].ToString();
            }
            //lblVoucherTx_No.Text = Office_Code + FinancialYear.ToString().Substring(2) + "BN";
            lblVoucherTx_No.Text = Office_Code + FinancialYear.ToString().Substring(2) + "CM";
            //txtVoucherTx_No.Text = VoucherTx_SNo.ToString();
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
            //ds = objdb.ByProcedure("Proc_tblPuSalesOrder", new string[] { "flag", "Office_ID" }, new string[] { "8", ViewState["Office_ID"].ToString() }, "dataset");
			ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID" }, new string[] { "32", ViewState["Office_ID"].ToString() }, "dataset");
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
            DataSet ds1 = objdb.ByProcedure("SpFinLedgerMaster",
                new string[] { "flag", "Office_ID", "MultipleHeadIDs" },
                new string[] { "22", ViewState["Office_ID"].ToString(), "1,2,3,4" }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    ddlItemName.DataSource = ds.Tables[0];
                //    ddlItemName.DataTextField = "ItemName";
                //    ddlItemName.DataValueField = "Item_id";
                //    ddlItemName.DataBind();
                //    ddlItemName.Items.Insert(0, new ListItem("Select", "0"));

                //}
                ddlItemName.Items.Insert(0, new ListItem("Select", "0"));
                if (ds1.Tables[0].Rows.Count > 0)
                {
                   

                    ddlLedger.DataSource = ds1.Tables[0];
                    ddlLedger.DataTextField = "Ledger_Name";
                    ddlLedger.DataValueField = "Ledger_ID";
                    ddlLedger.DataBind();
                    ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

                    ddlDebitLedger.DataSource = ds1.Tables[0];
                    ddlDebitLedger.DataTextField = "Ledger_Name";
                    ddlDebitLedger.DataValueField = "Ledger_ID";
                    ddlDebitLedger.DataBind();
                    ddlDebitLedger.Items.Insert(0, new ListItem("Select", "0"));

                }

                //if (ds.Tables[2].Rows.Count > 0)
                //{
                //    ddlSalesCenter.DataSource = ds.Tables[2];
                //    ddlSalesCenter.DataTextField = "Office_Name";
                //    ddlSalesCenter.DataValueField = "Office_ID";
                //    ddlSalesCenter.DataBind();
                //    ddlSalesCenter.Items.Insert(0, new ListItem("Select", "0"));
                //}

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

    //Add ItemDetail Event & Function
    protected void btnAdd_Click(object sender, EventArgs e)
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
                if(decimal.Parse(txtRate.Text) == 0)
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
                dt_GridViewItem.Columns.Add(new DataColumn("Quantity", typeof(float)));
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
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e) 
    {
        try
        {
            lblMsg.Text = "";
            //ClearItem();
            //txtQuantity.Text = "";
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
                DataSet ds1 = objdb.ByProcedure("SpFinVoucherTx",
                       new string[] { "flag", "Item_id", "Office_ID" },
                       new string[] { "19", ddlItemName.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count != 0)
                {
                    txtRate.Text = ds1.Tables[0].Rows[0]["Rate"].ToString();

                }
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
                    ds = objdb.ByProcedure("SpFinVoucherSaleCredit", new string[] { "flag", "Item_id", "VoucherTx_Date" },
                       new string[] { "2", ddlItemName.SelectedValue.ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        gridRows = gridRows + 1;
                        string Item = ddlItemName.SelectedItem.ToString();
                        string UnitID = ds.Tables[0].Rows[0]["Unit_id"].ToString();
                        string Unit = ds.Tables[0].Rows[0]["UQCCode"].ToString();
                        string HSNCode = ds.Tables[0].Rows[0]["HSNCode"].ToString();
                        string CGSTPer = ds.Tables[0].Rows[0]["CGST"].ToString();
                        string SGSTPer = ds.Tables[0].Rows[0]["SGST"].ToString();
                        string Taxbility = ds.Tables[0].Rows[0]["Taxbility"].ToString();
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
                ds = objdb.ByProcedure("SpFinVoucherSaleCredit", new string[] { "flag", "Item_id", "VoucherTx_Date" },
                   new string[] { "2", ddlItemName.SelectedValue.ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
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
                    dt_GridViewItem.Rows.Add(null, ddlItemName.SelectedValue.ToString(), UnitID.ToString(), ddlWarehouse.SelectedValue.ToString(), ddlWarehouse.SelectedItem.Text, HSNCode.ToString(), Item.ToString(), txtQuantity.Text, txtRate.Text, Amount.ToString(), CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTPer.ToString(), SGSTPer.ToString(), IGSTPer.ToString(), Unit.ToString(), ViewState["ParticularName"].ToString(), ViewState["ParticularID"].ToString(),Taxbility);
                }

            }
            decimal TAmount = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
            decimal CGSTAmount = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
            decimal SGSTTAmount = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
            GridViewItem.DataSource = dt_GridViewItem;
            GridViewItem.DataBind();
            if (GridViewItem.Rows.Count > 0)
            {

                GridViewItem.FooterRow.Cells[3].Text = "<b>Total : </b>";
                GridViewItem.FooterRow.Cells[4].Text = "<b>" + TAmount.ToString() + "</b>";
                GridViewItem.FooterRow.Cells[5].Text = "<b>" + CGSTAmount.ToString() + "</b>";
                GridViewItem.FooterRow.Cells[6].Text = "<b>" + SGSTTAmount.ToString() + "</b>";
                GridViewItem.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                GridViewItem.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                GridViewItem.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
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
                    if (lblID.Text == "1" || lblID.Text == "2" || lblID.Text == "3" )
                    {
                        status = "0";
                    }
                    else
                    {
                        status = "1";
                        dt_GridViewLedger.Rows.Add(lblID.Text, lblLedgerName.Text, txtAmount.Text, lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, status, lblGSTApplicable.Text, lblTaxbility.Text);
                        foreach (DataRow dr in dt_GridViewLedger.Rows) // search whole table
                        {
                            if (dr["LedgerName"].ToString() == "CGST" || dr["LedgerName"].ToString() == "CGST OUTPUT") // if id==2
                            {
                                dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + decimal.Parse(lblCGSTAmt.Text); //change the name
                                //break; break or not depending on you
                            }
                            if (dr["LedgerName"].ToString() == "SGST" || dr["LedgerName"].ToString() == "SGST OUTPUT") // if id==2
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
                    if (ViewState["CGST"].ToString() == "1")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST", CGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    }
                    if (ViewState["CGST"].ToString() == "46721")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST OUTPUT", CGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    }
                    if (ViewState["SGST"].ToString() == "2")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST", SGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    }
                    if (ViewState["SGST"].ToString() == "46722")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST OUTPUT", SGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    }
                    
                   
                   
                }
                else
                {
                    decimal CGST =dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    decimal SGST = dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    if (ViewState["CGST"].ToString() == "1")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST", CGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    }
                    if (ViewState["CGST"].ToString() == "46721")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST OUTPUT", CGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    }
                    if (ViewState["SGST"].ToString() == "2")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST", SGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    }
                    if (ViewState["SGST"].ToString() == "46722")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST OUTPUT", SGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    }
                    //dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST", CGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    //dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST", SGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status,null,null);
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
                    if (ViewState["CGST"].ToString() == "1")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST", CGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    }
                    if (ViewState["CGST"].ToString() == "46721")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST OUTPUT", CGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    }
                    if (ViewState["SGST"].ToString() == "2")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST", SGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    }
                    if (ViewState["SGST"].ToString() == "46722")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST OUTPUT", SGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    }
                    //dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST", CGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status,null,null);
                    //dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST", SGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status,null,null);
                }
                else
                {
                    decimal CGST = dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    decimal SGST = dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    if (ViewState["CGST"].ToString() == "1")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST", CGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    }
                    if (ViewState["CGST"].ToString() == "46721")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST OUTPUT", CGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    }
                    if (ViewState["SGST"].ToString() == "2")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST", SGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    }
                    if (ViewState["SGST"].ToString() == "46722")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST OUTPUT", SGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                    }
                    
                    //dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST", CGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status,null,null);
                    //dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST", SGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status,null,null);
                }
                dt_GridViewLedger.Rows.Add(ViewState["RoundOff"].ToString(), "Round off", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status,null,null);
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
            AddItem(ID);

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
                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "true" && ds.Tables[0].Rows[0]["GSTApplicable"].ToString() == "Yes")
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

                                if (lblID.Text == "1" || lblID.Text == "2" || lblID.Text == "3" || lblID.Text == "46721" || lblID.Text == "46722")
                                {
                                    status = "0";
                                }
                                else
                                {
                                    status = "1";
                                }
                                dt_GridViewLedger.Rows.Add(lblID.Text, lblLedgerName.Text, txtAmount.Text, lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, status,lblGSTApplicable.Text, lblTaxbility.Text);
                            }
                            
                            foreach (DataRow dr in dt_GridViewLedger.Rows) // search whole table
                            {
                                if (dr["LedgerName"].ToString() == "CGST" || dr["LedgerName"].ToString() == "CGST OUTPUT") // if id==2
                                {
                                    dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + CGSTAmt; //change the name
                                    //break; break or not depending on you
                                }
                                if (dr["LedgerName"].ToString() == "SGST" || dr["LedgerName"].ToString() == "SGST OUTPUT") // if id==2
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
                            if (lblID.Text == "1" || lblID.Text == "2" || lblID.Text == "3" || lblID.Text == "4")
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
                            if (dr["LedgerName"].ToString() == "CGST" || dr["LedgerName"].ToString() == "CGST OUTPUT") // if id==2
                            {
                                dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) - decimal.Parse(lblCGSTAmt.Text); //change the name
                                //break; break or not depending on you
                            }
                            if (dr["LedgerName"].ToString() == "SGST" || dr["LedgerName"].ToString() == "SGST OUTPUT") // if id==2
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
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
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
            //if (txtVoucherTx_Ref.Text == "")
            //{
            //    msg += "Enter Reference No. \\n";
            //}
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
            if (ddlDebitLedger.SelectedIndex == 0)
            {
                msg += "Select Debit To Ledger . \\n";
            }
            if (txtVoucherTx_SoldTo.Text == "")
            {
                msg += "Enter Sold To. \\n";
            }
            //if (ddlSalesCenter.SelectedIndex == 0)
            //{
            //    msg += "Select Sales Center . \\n";
            //}
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
                    //ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy", "VoucherTx_SalesCenterID", "SchemeTx_ID", "VoucherTx_SoldTo", "VoucherTx_OrderNo", "VoucherTx_RegNo", "VoucherTx_SNo" }, new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Sales Voucher", "CashSale Voucher", txtVoucherTx_No.Text, txtVoucherTx_Ref.Text, txtVoucherTx_Narration.Text, lblGrandTotal.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_IsActive, ViewState["Emp_ID"].ToString(), ddlSalesCenter.SelectedValue.ToString(), ddlScheme.SelectedValue.ToString(), txtVoucherTx_SoldTo.Text, txtVoucherTx_OrderNo.Text, txtVoucherTx_RegNo.Text, ViewState["VoucherTx_SNo"].ToString() }, "dataset");
                    ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy", "VoucherTx_SalesCenterID", "SchemeTx_ID", "VoucherTx_SoldTo", "VoucherTx_OrderNo", "VoucherTx_RegNo", "GSTVoucher" }, new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Sales Voucher", "CashSale Voucher", VoucherTx_No, txtVoucherTx_Ref.Text, txtVoucherTx_Narration.Text, lblGrandTotal.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_IsActive, ViewState["Emp_ID"].ToString(), "0", Scheme, txtVoucherTx_SoldTo.Text, txtVoucherTx_OrderNo.Text, txtVoucherTx_RegNo.Text, "Yes" }, "dataset");
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
                            objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID","Ledger_ID", "VoucherTx_Name", "VoucherTx_Type", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "HSN_Code", "IGST_Per", "CGST_Per", "SGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Warehouse_id", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy","Taxbility" }, new string[] { "0", VoucherTx_ID,lblLedgerID.Text, "Sales Voucher", "CashSale Voucher", lblItemID.Text, lblUnit_id.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, lblHSNCode.Text, lblIGSTPer.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblCGST.Text, lblSGST.Text, lblIGST.Text, ddlWarehouse.SelectedValue.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), (rowItemIndex + 1).ToString(),lblTaxbility.Text }, "dataset");
                            ds = objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "Ledger_ID" }, new string[] { "3", lblLedgerID.Text }, "dataset");
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["InventoryAffected"].ToString() == "Yes")
                                {
                                    objdb.ByProcedure("SpFinItemTx",
                                        new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "TransactionFrom", "InvoiceNo", "Office_Id", "Warehouse_id", "CreatedBy", "TranDt", "Amount" }
                                       , new string[] { "4", lblItemID.Text,"0", lblQuantity.Text,lblRate.Text, VoucherTx_ID, "CashSale Voucher", VoucherTx_No, ViewState["Office_ID"].ToString(), lblWarehouse_id.Text, ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"),lblAmount.Text }, "dataset");
                                }
                            }
                            objdb.ByProcedure("SpFinLedgerTx",
                               new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
                               new string[] { "0", lblLedgerID.Text, "Item Ledger", VoucherTx_ID, lblItemID.Text, "CashSale Voucher", lblAmount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), lblItemRowNo.Text }, "dataset");
                        }
                            
                        string LedgerTxAmount = lblGrandTotal.Text;
                        if (LedgerTxAmount.Contains("-"))
                        {
                            LedgerTxAmount = LedgerTxAmount.Replace(@"-", string.Empty);
                        }
                        else
                        {
                            LedgerTxAmount = "-" + LedgerTxAmount;
                            objdb.ByProcedure("SpFinLedgerTx",
                                new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                                new string[] { "0", ddlDebitLedger.SelectedValue.ToString(), "Main Ledger", VoucherTx_ID, "CashSale Voucher", LedgerTxAmount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), "1", "None" }, "dataset");
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
                                new string[] { "0", lblID.Text, "Sub Ledger", VoucherTx_ID, "CashSale Voucher", txtAmount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 2).ToString(), lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, lblGSTApplicable.Text, lblTaxbility.Text }, "dataset");

                            }
                            objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "40", VoucherTx_ID }, "dataset");
                        }
                        
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                        ClearText();

                        string url = "VoucherSalepurchaseInvocieNew.aspx?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID).ToString();
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.open('");
                        sb.Append(url);
                        sb.Append("');");
                        sb.Append("</script>");
                        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());

                    }

                }
                else if (btnAccept.Text == "Update" && ViewState["VoucherTx_ID"].ToString() != "0" && Status == 0)
                {
                    //objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_InsertedBy", "VoucherTx_SalesCenterID", "SchemeTx_ID", "VoucherTx_SoldTo", "VoucherTx_OrderNo", "VoucherTx_RegNo" }, new string[] { "7", ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Sales Voucher", "CashSale Voucher", txtVoucherTx_No.Text, txtVoucherTx_Ref.Text, txtVoucherTx_Narration.Text, lblGrandTotal.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), ViewState["Emp_ID"].ToString(), ddlSalesCenter.SelectedValue.ToString(), ddlScheme.SelectedValue.ToString(), txtVoucherTx_SoldTo.Text, txtVoucherTx_OrderNo.Text, txtVoucherTx_RegNo.Text }, "dataset");
                    objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_InsertedBy", "VoucherTx_SalesCenterID", "SchemeTx_ID", "VoucherTx_SoldTo", "VoucherTx_OrderNo", "VoucherTx_RegNo" }, new string[] { "7", ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Sales Voucher", "CashSale Voucher", VoucherTx_No, txtVoucherTx_Ref.Text, txtVoucherTx_Narration.Text, lblGrandTotal.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), ViewState["Emp_ID"].ToString(), "0", Scheme, txtVoucherTx_SoldTo.Text, txtVoucherTx_OrderNo.Text, txtVoucherTx_RegNo.Text }, "dataset");
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
                        objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Name", "VoucherTx_Type", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "HSN_Code", "IGST_Per", "CGST_Per", "SGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Warehouse_id", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy","Taxbility" }, new string[] { "0", ViewState["VoucherTx_ID"].ToString(),lblLedgerID.Text, "Sales Voucher", "CashSale Voucher", lblItemID.Text, lblUnit_id.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, lblHSNCode.Text, lblIGSTPer.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblCGST.Text, lblSGST.Text, lblIGST.Text, ddlWarehouse.SelectedValue.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), (rowItemIndex + 1).ToString(),lblTaxbility.Text }, "dataset");
                        ds = objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "Ledger_ID" }, new string[] { "3", lblLedgerID.Text }, "dataset");
                        
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["InventoryAffected"].ToString() == "Yes")
                            {
                                objdb.ByProcedure("SpFinItemTx",
                                    new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "TransactionFrom", "InvoiceNo", "Office_Id", "Warehouse_id", "CreatedBy", "TranDt", "Amount" }
                                   , new string[] { "4", lblItemID.Text, "0", lblQuantity.Text, lblRate.Text, ViewState["VoucherTx_ID"].ToString(), "CashSale Voucher", VoucherTx_No, ViewState["Office_ID"].ToString(), lblWarehouse_id.Text, ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"),lblAmount.Text }, "dataset");
                            }
                        }
                        objdb.ByProcedure("SpFinLedgerTx",
                               new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
                               new string[] { "0", lblLedgerID.Text, "Item Ledger", ViewState["VoucherTx_ID"].ToString(), lblItemID.Text, "CashSale Voucher", lblAmount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), lblItemRowNo.Text }, "dataset");
                    }
                    
                    string LedgerTxAmount = lblGrandTotal.Text;
                    if (LedgerTxAmount.Contains("-"))
                    {
                        LedgerTxAmount = LedgerTxAmount.Replace(@"-", string.Empty);
                    }
                    else
                    {
                        LedgerTxAmount = "-" + LedgerTxAmount;
                    }
                    
                    objdb.ByProcedure("SpFinLedgerTx",
                        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                        new string[] { "0", ddlDebitLedger.SelectedValue.ToString(), "Main Ledger", ViewState["VoucherTx_ID"].ToString(), "CashSale Voucher", LedgerTxAmount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), "1", "None" }, "dataset");
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
                        new string[] { "0", lblID.Text, "Sub Ledger", ViewState["VoucherTx_ID"].ToString(), "CashSale Voucher", txtAmount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 2).ToString(), lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, lblGSTApplicable.Text, lblTaxbility.Text }, "dataset");

                    }
                    
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");

                    string url = "VoucherSalepurchaseInvocieNew.aspx?VoucherTx_ID=" + objdb.Encrypt(ViewState["VoucherTx_ID"].ToString()).ToString();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.open('");
                    sb.Append(url);
                    sb.Append("');");
                    sb.Append("</script>");
                    ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Voucher No already exists');", true);
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
            txtTotalAmount.ReadOnly = true;
            GridViewLedger.DataSource = new string[] { };
            GridViewLedger.DataBind();
            GridViewItem.DataSource = new string[] { };
            GridViewItem.DataBind();
            
            lblGrandTotal.Text = "0";
            ddlLedger.ClearSelection();
            txtLedgerAmt.Text = "";
            ddlDebitLedger.ClearSelection();
            //ddlSalesCenter.ClearSelection();
            ddlScheme.ClearSelection();
            txtVoucherTx_SoldTo.Text = "";
            txtVoucherTx_RegNo.Text = "";
            txtVoucherTx_OrderNo.Text = "";
            txtVoucherTx_Narration.Text = "";
           
            FillVoucherNo();
            GetPreviousVoucherNo();
            AddItem("NA");
            FillItem();

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
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "6", ViewState["VoucherTx_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //if (ds.Tables[0].Rows[0]["VoucherTx_No"].ToString().Contains("BN"))
                    //{
                    //    var rx = new System.Text.RegularExpressions.Regex("BN");
                    //    string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //    var array = rx.Split(str);
                    //    lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //    txtVoucherTx_No.Text = array[1];
                    //    lblVoucherTx_No.Text = array[0] + "BN";
                    //}
                    //else
                    //{
                    //    var rx = new System.Text.RegularExpressions.Regex("CM");
                    //    string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //    var array = rx.Split(str);
                    //    lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //    txtVoucherTx_No.Text = array[1];
                    //    lblVoucherTx_No.Text = array[0] + "CM";
                    //}
                    string Fstring = "";
                    string Lstring = "";
                    string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    ViewState["VoucherTx_Date"] = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    ViewState["FY"] = ds.Tables[0].Rows[0]["VoucherTx_FY"].ToString();
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
                    
                    txtVoucherTx_No.Text = Lstring;
                    lblVoucherTx_No.Text = Fstring;
                    
                    txtVoucherTx_Ref.Text = ds.Tables[0].Rows[0]["VoucherTx_Ref"].ToString();
                    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    lblGrandTotal.Text = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                    //ddlSalesCenter.ClearSelection();
                    //ddlSalesCenter.Items.FindByValue(ds.Tables[0].Rows[0]["VoucherTx_SalesCenterID"].ToString()).Selected = true;
                    ddlScheme.ClearSelection();
                    ddlScheme.Items.FindByValue(ds.Tables[0].Rows[0]["SchemeTx_ID"].ToString()).Selected = true;
                    txtVoucherTx_SoldTo.Text = ds.Tables[0].Rows[0]["VoucherTx_SoldTo"].ToString();
                    txtVoucherTx_RegNo.Text = ds.Tables[0].Rows[0]["VoucherTx_RegNo"].ToString();
                    txtVoucherTx_OrderNo.Text = ds.Tables[0].Rows[0]["VoucherTx_OrderNo"].ToString();
                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    GridViewItem.DataSource = ds.Tables[1];
                    GridViewItem.DataBind();
                    if (GridViewItem.Rows.Count > 0)
                    {
                        decimal TAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                        decimal CGSTAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                        decimal SGSTTAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                        GridViewItem.FooterRow.Cells[3].Text = "<b>Total : </b>";
                        GridViewItem.FooterRow.Cells[4].Text = "<b>" + TAmount.ToString() + "</b>";
                        GridViewItem.FooterRow.Cells[5].Text = "<b>" + CGSTAmount.ToString() + "</b>";
                        GridViewItem.FooterRow.Cells[6].Text = "<b>" + SGSTTAmount.ToString() + "</b>";
                        GridViewItem.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                        GridViewItem.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                        GridViewItem.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
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
                    string OfficeID = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                    if (OfficeID.ToString() == Session["Office_ID"].ToString())
                    {
                        ddlDebitLedger.ClearSelection();
                        ddlDebitLedger.Items.FindByValue(ds.Tables[2].Rows[0]["LedgerID"].ToString()).Selected = true;
                        ddlDebitLedger.Visible = true;
                        txtDebitLedger.Visible = false;
                    }
                    else
                    {
                        ddlDebitLedger.Visible = false;
                        txtDebitLedger.Visible = true;
                        txtDebitLedger.Text = ds.Tables[2].Rows[0]["Ledger_Name"].ToString();
                    }
                    
                }
                if (ds.Tables[3].Rows.Count > 0)
                {

                    GridViewLedger.DataSource = ds.Tables[3];
                    GridViewLedger.DataBind();
                    DataTable dt_GridViewLedger = (DataTable)ViewState["LedgerAmount"];
                    dt_GridViewLedger = ds.Tables[3];
                    ViewState["LedgerAmount"] = dt_GridViewLedger;
                }
               
                btnAccept.Text = "Update";
                btn_Clear.Visible = false;
                lnkPreviousVoucher.Visible = false;
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
            //btnClear.Visible = false;
            divamount.Visible = false;
            divitem.Visible = false;
            panel1.Enabled = false;
            lbkbtnAddLedger.Visible = false;
            GridViewItem.Columns[9].Visible = false;
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
        //FillVoucherNo();
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
                ViewState["Action"] = "";
                AddItem("NA");              
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
            //FillVoucherNo();
            //AddItem("NA");
        }
        
    }
    protected void lnkPreviousVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "VoucherTx_Type" }, new string[] { "33", ViewState["Office_ID"].ToString(), "CashSale Voucher" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
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
                ViewState["LedgerAmount"] = dt_GridViewLedger;
                string VoucherID = ds.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
                ViewState["VoucherTx_ID"] = VoucherID.ToString();
                FillDetail();
                btnAccept.Text = "Accept";
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
                 new string[] { "14", "CashSale Voucher", ViewState["Office_ID"].ToString() }, "dataset");

        if (ds.Tables[0].Rows.Count != 0)
        {
            txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
        }
    }

    //GetPreviousVoucherNo
    protected void GetPreviousVoucherNo()
    {
        try
        {
            
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
            string VoucherTx_Type = "CashSale Voucher";
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

}

