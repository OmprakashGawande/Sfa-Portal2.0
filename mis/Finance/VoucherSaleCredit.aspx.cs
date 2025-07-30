using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_VoucherSaleCredit : System.Web.UI.Page
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
                if (Session["Emp_ID"] != null)
                {
                    CreateCrCostCentreDataSet();
                    itemdetail.Enabled = false;
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["RowNo"] = "0";
                    ViewState["CGST"] = "1";
                    ViewState["SGST"] = "2";
                    ViewState["RoundOff"] = "3";
                    ViewState["IGST"] = "4";
                   
                    ViewState["VoucherTx_ID"] = "0";
                    ViewState["CNO"] = "0";
                    CreateCostCentreTable();
                    ViewState["EditVoucherTx_Amount"] = "0.00";
                    ViewState["CrCostCentre"] = "No";
                    lblGrandTotal.Attributes.Add("readonly", "readonly");
                    txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
                   
                    FillCategory();
                    FillDropDown();
                   
               
                    FillItem();
                    FillWareHouse();
                    AddItem("NA");
                    chkitem_CheckedChanged(sender, e);
                    txtUnitName.Attributes.Add("readonly", "readonly");
                   
                    GridViewRef.DataSource = new string[] { };
                    GridViewRef.DataBind();

                    ds = null;
                    ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
                    if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
                        FillVoucherNo();
                    }

                    if (Request.QueryString["VoucherTx_ID"] != null && Request.QueryString["Action"] != null)
                    {
                        string Action = objdb.Decrypt(Request.QueryString["Action"].ToString());
                        ViewState["Action"] = Action;
                        ViewState["VoucherTx_ID"] = objdb.Decrypt(Request.QueryString["VoucherTx_ID"].ToString());
                        if (Action == "2")
                        {
                            FillDetail();
                            //string ValidStatus = ValidDate();
                            //if (ValidStatus == "No")
                            //{
                            //    Response.Redirect("~/mis/Login.aspx");
                            //}
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
                else
                {
                    Response.Redirect("~/mis/Login.aspx");
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
                ddlItem.Items.Clear();
                ddlItem.DataSource = ds.Tables[0];
                ddlItem.DataTextField = "AvailableStock1";
                ddlItem.DataValueField = "Item_id";
                ddlItem.DataBind();
                ddlItem.Items.Insert(0, new ListItem("Select", "0"));

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
  
    //Fill Ledger DropDown in AMOUNT Section
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

                    ddlPartyName.DataSource = ds.Tables[0];
                    ddlPartyName.DataTextField = "Ledger_Name";
                    ddlPartyName.DataValueField = "Ledger_ID";
                    ddlPartyName.DataBind();
                    ddlPartyName.Items.Insert(0, new ListItem("Select", "0"));

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
    protected void FillCategory()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinCategoryMaster",
                 new string[] { "flag", "OfficeID" },
                 new string[] { "5", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataSource = ds;
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select", "0"));
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
            string VoucherTx_Names_ForSno = "Sales Voucher";
            //string VoucherTx_Names_ForSno = "Sales Voucher";

            DataSet ds1 = objdb.ByProcedure("SpFinVoucherTx",
                new string[] { "flag", "Office_ID", "VoucherTx_FY", "VoucherTx_Names_ForSno" },
                new string[] { "13", ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_Names_ForSno }, "dataset");

            string Office_Code = "";
            if (ds1.Tables[1].Rows.Count != 0)
            {
                Office_Code = ds1.Tables[1].Rows[0]["Office_Code"].ToString();
            }
            int VoucherTx_SNo = 0;
            if (ds1.Tables[0].Rows.Count != 0)
            {
                //VoucherTx_SNo = Convert.ToInt32(ds1.Tables[0].Rows[0]["VoucherTx_SNo"].ToString());

            }
            //ViewState["PreVoucherNo"] = Office_Code + FinancialYear.ToString().Substring(2) + "PV" + VoucherTx_SNo.ToString();
            VoucherTx_SNo++;
            ViewState["VoucherTx_SNo"] = VoucherTx_SNo;
            lblVoucherTx_No.Text = Office_Code + FinancialYear.ToString().Substring(2) + "BN";
            //txtVoucherTx_No.Text = VoucherTx_SNo.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //Ledger Current Balance    
    protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblPAN_Status.Text = "";
            
            FillCurrentBalance();
            //GridViewItem.DataSource = new string[] { };
            //GridViewItem.DataBind();
            GridViewBillByBillDetail.DataSource = new string[] { };
            GridViewBillByBillDetail.DataBind();
            GridViewChequeDetail.DataSource = new string[] { };
            GridViewChequeDetail.DataBind();
            GridCostCentreViewDetail.DataSource = new string[] { };
            GridCostCentreViewDetail.DataBind();
            AddItem("NA");
            
            ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", ddlPartyName.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "Yes")
                {
                    BillByBillDetail.Visible = true;
                    pnlChequeDetail.Visible = false;
                    pnlCostCentre.Visible = false;
                }
                else if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "BankLedger")
                {
                    BillByBillDetail.Visible = false;
                    pnlChequeDetail.Visible = true;
                    pnlCostCentre.Visible = false;
                }
                else if (ds.Tables[0].Rows[0]["CostCentre"].ToString() == "Yes")
                {
                    BillByBillDetail.Visible = false;
                    pnlChequeDetail.Visible = false;
                    pnlCostCentre.Visible = true;
                }
                else
                {
                    BillByBillDetail.Visible = false;
                    pnlChequeDetail.Visible = false;
                    pnlCostCentre.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
   
    protected void FillCurrentBalance()
    {
        try
        {
            lblMsg.Text = "";
            if (ddlPartyName.SelectedIndex > 0)
            {

                DataSet ds1 = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "1", ddlPartyName.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    txtCurrentBalance.Text = ds1.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
                }
            }
            else
            {

                txtCurrentBalance.Text = "";
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //Add ItemDetail Event & Function
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ClearItem();
            //txtQuantity.Text = "";
            lblUnit.Text = "0";
            txtAmount.Text = "";

            txtQuantity.Text = "";
            txtRate.Text = "";
            txtAmount.ReadOnly = true;
            txtQuantity.ReadOnly = true;
            txtRate.ReadOnly = true;
            if (ddlItem.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpItemMaster",
                        new string[] { "flag", "ItemId" },
                        new string[] { "20", ddlItem.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    lblUnit.Text = ds.Tables[0].Rows[0]["NoOfDecimalPlace"].ToString();
                    txtUnitName.Text = ds.Tables[0].Rows[0]["UQCCode"].ToString();

                }

                txtAmount.ReadOnly = false;
                txtQuantity.ReadOnly = false;
                txtRate.ReadOnly = false;
                DataSet ds1 = objdb.ByProcedure("SpFinVoucherTx",
                       new string[] { "flag", "Item_id", "Office_ID" },
                       new string[] { "20", ddlItem.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
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
    //Get Purchase Ledger Mapped With Item
    protected void GetItemSalesLedgerId()
    {
        try
        {
            string Item_id = ddlItem.SelectedValue.ToString();
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            int status = 0;
            DataSet DS_GridViewParticulars = (DataSet)ViewState["DS_GridViewParticulars"];
            lblMsg.Text = "";
            string msg = "";
            if (ddlPartyName.SelectedIndex == 0)
            {
                msg += "Select Party A/c Name. \\n";
            }
            if (ddlItem.SelectedIndex == 0)
            {
                msg += "Select Item Name. \\n";
            }
            if (txtQuantity.Text == "")
            {
                msg += "Enter Quantity. \\n";
            }
            if (txtRate.Text == "")
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
            if (txtAmount.Text == "")
            {
                msg += "Enter Amount. \\n";
            }
            if (ddlWarehouse.SelectedIndex == 0)
            {
                msg += "Select  Warehouse. \\n";
            }

            if (msg.Trim() == "")
            {
                //int rowIndex = 0;
                //int gridRows = GridViewItem.Rows.Count;
                //if (gridRows > 0)
                //{
                //    for (rowIndex = 0; rowIndex < gridRows; rowIndex++)
                //    {
                //        Label lblItem = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
                //        Label lblWarehouse_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_id");
                //        if (lblItem.Text == ddlItem.SelectedValue && lblWarehouse_id.Text == ddlWarehouse.SelectedValue)
                //        {
                //            status = 1;

                //        }
                //        else
                //        {

                //        }

                //    }
                //}
                //if (status == 1)
                //{
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Warehouse already exists');", true);
                //}
                //else
                //{
                //    GetItemSalesLedgerId();
                //    AddItem(ViewState["RowNo"].ToString());
                //    string TNo = ddlItem.SelectedValue.ToString();
                //    DataTable dt_GridViewParticulars = new DataTable(TNo);
                //    dt_GridViewParticulars.Columns.Add(new DataColumn("Item_ID", typeof(string)));
                //    dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularID", typeof(string)));
                //    dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularName", typeof(string)));
                //    dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularAmt", typeof(decimal)));
                //    dt_GridViewParticulars.Rows.Add(ddlItem.SelectedValue, ViewState["ParticularID"].ToString(), ViewState["ParticularName"].ToString(), txtAmount.Text);
                //    ClearItem();
                //    DS_GridViewParticulars.Merge(dt_GridViewParticulars);
                //    ViewState["DS_GridViewParticulars"] = DS_GridViewParticulars;
                //    ViewState["RowNo"] = "0";
                //}
                GetItemSalesLedgerId();
                AddItem(ViewState["RowNo"].ToString());
                ClearItem();
                ViewState["RowNo"] = "0";
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
    protected void AddItem(string ID)
    {
        try
        {

            DataTable dt_GridViewItem = new DataTable();
            DataColumn RowNo = dt_GridViewItem.Columns.Add("ID", typeof(int));
            dt_GridViewItem.Columns.Add(new DataColumn("ItemID", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Unit_id", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Warehouse_id", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("WarehouseName", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("HSN_Code", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Item", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Quantity", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Rate", typeof(string)));
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
            for (rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {
                Label lblItemRowNo = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemRowNo");
                Label lblItemID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
                Label lblUnit_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblUnit_id");
                Label lblWarehouse_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_id");
                Label lblWarehouse_Name = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_Name");
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
                    dt_GridViewItem.Rows.Add(lblItemRowNo.Text, lblItemID.Text, lblUnit_id.Text, lblWarehouse_id.Text, lblWarehouse_Name.Text, lblHSNCode.Text, lblItem.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, lblCGST.Text, lblSGST.Text, lblIGST.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblUnit.Text, lblLedgerName.Text, lblLedgerID.Text, lblTaxbility.Text);

                }

                else if (ViewState["RowNo"].ToString() != "0")
                {
                    ds = objdb.ByProcedure("SpFinVoucherSaleCredit", new string[] { "flag", "Item_id", "VoucherTx_Date" },
                      new string[] { "2", ddlItem.SelectedValue.ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        gridRows = gridRows + 1;
                        string Item = ddlItem.SelectedItem.ToString();
                        string UnitID = ds.Tables[0].Rows[0]["Unit_id"].ToString();
                        string Unit = ds.Tables[0].Rows[0]["UQCCode"].ToString();
                        string HSNCode = ds.Tables[0].Rows[0]["HSNCode"].ToString();
                        string Taxbility = ds.Tables[0].Rows[0]["Taxbility"].ToString();
                        decimal CGST = decimal.Parse(ds.Tables[0].Rows[0]["CGST"].ToString());
                        decimal SGST = decimal.Parse(ds.Tables[0].Rows[0]["SGST"].ToString());
                        string CGSTPer = ds.Tables[0].Rows[0]["CGST"].ToString();
                        string SGSTPer = ds.Tables[0].Rows[0]["SGST"].ToString();
                        string IGSTPer = ds.Tables[0].Rows[0]["IGST"].ToString();
                        decimal IGST = decimal.Parse(ds.Tables[0].Rows[0]["IGST"].ToString());
                        decimal Amount = decimal.Parse(txtAmount.Text);

                        DataSet ds1 = objdb.ByProcedure("SpFinVoucherSaleCredit", new string[] { "flag", "Ledger_ID" }, new string[] { "3", ddlPartyName.SelectedValue.ToString() }, "dataset");
                        {
                            if (ds1 != null && ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds1.Tables[0].Rows[0]["status"].ToString() == "true")
                                {
                                    CGST = 0;
                                    SGST = 0;
                                    CGSTPer = "0";
                                    SGSTPer = "0";

                                }
                                else
                                {
                                    IGST = 0;
                                }
                            }
                        }
                        CGST = Math.Round((Amount * CGST) / 100, 2);
                        SGST = Math.Round((Amount * SGST) / 100, 2);
                        IGST = Math.Round((Amount * IGST) / 100, 2);
                        decimal TotalAmount = Amount + CGST + SGST + IGST;
                       
                        decimal Quantity = decimal.Parse(txtQuantity.Text);

                        
                        dt_GridViewItem.Rows.Add(null, ddlItem.SelectedValue.ToString(), UnitID, ddlWarehouse.SelectedValue.ToString(), ddlWarehouse.SelectedItem.Text, HSNCode.ToString(), Item.ToString(), txtQuantity.Text, txtRate.Text, Amount.ToString(), CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTPer.ToString(), SGSTPer.ToString(), IGSTPer.ToString(), TotalAmount.ToString(), txtUnitName.Text, ViewState["ParticularName"].ToString(), ViewState["ParticularID"].ToString(), Taxbility);
                    }
                }

                // dt_GridViewItem.Rows.Add(rowIndex.ToString(), lblItem.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text);

            }
            if (ID == "0" && ViewState["RowNo"].ToString() == "0")
            {
                ds = objdb.ByProcedure("SpFinVoucherSaleCredit", new string[] { "flag", "Item_id", "VoucherTx_Date" },
                         new string[] { "2", ddlItem.SelectedValue.ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    gridRows = gridRows + 1;
                    string Item = ddlItem.SelectedItem.ToString();
                    string UnitID = ds.Tables[0].Rows[0]["Unit_id"].ToString();
                    string Unit = ds.Tables[0].Rows[0]["UQCCode"].ToString();
                    string HSNCode = ds.Tables[0].Rows[0]["HSNCode"].ToString();
                    string Taxbility = ds.Tables[0].Rows[0]["Taxbility"].ToString();
                    decimal CGST = decimal.Parse(ds.Tables[0].Rows[0]["CGST"].ToString());
                    decimal SGST = decimal.Parse(ds.Tables[0].Rows[0]["SGST"].ToString());
                    string CGSTPer = ds.Tables[0].Rows[0]["CGST"].ToString();
                    string SGSTPer = ds.Tables[0].Rows[0]["SGST"].ToString();
                    string IGSTPer = ds.Tables[0].Rows[0]["IGST"].ToString();
                    decimal IGST = decimal.Parse(ds.Tables[0].Rows[0]["IGST"].ToString());
                    DataSet ds1 = objdb.ByProcedure("SpFinVoucherSaleCredit", new string[] { "flag", "Ledger_ID" }, new string[] { "3", ddlPartyName.SelectedValue.ToString() }, "dataset");
                    {
                        if (ds1 != null && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds1.Tables[0].Rows[0]["status"].ToString() == "true")
                            {
                                CGST = 0;
                                SGST = 0;
                                CGSTPer = "0";
                                SGSTPer = "0";
                            }
                            else
                            {
                                IGST = 0;
                                IGSTPer = "0";
                            }
                        }
                    }
                    decimal Amount = decimal.Parse(txtAmount.Text);
                    CGST = Math.Round((Amount * CGST) / 100, 2);
                    SGST = Math.Round((Amount * SGST) / 100, 2);
                    IGST = Math.Round((Amount * IGST) / 100, 2);
                    decimal TotalAmount = Amount + CGST + SGST + IGST;
                    
                    dt_GridViewItem.Rows.Add(null, ddlItem.SelectedValue.ToString(), UnitID, ddlWarehouse.SelectedValue.ToString(), ddlWarehouse.SelectedItem.Text, HSNCode.ToString(), Item.ToString(), txtQuantity.Text, txtRate.Text, Amount.ToString(), CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTPer.ToString(), SGSTPer.ToString(), IGSTPer.ToString(), txtUnitName.Text, ViewState["ParticularName"].ToString(), ViewState["ParticularID"].ToString(),Taxbility);
                }
            }

            decimal TAmount = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
            decimal CGSTAmount = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
            decimal SGSTAmount = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
            decimal IGSTAmount = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("IGSTAmt"));
            
            GridViewItem.DataSource = dt_GridViewItem;
            GridViewItem.DataBind();
           
            if (GridViewItem.Rows.Count > 0)
            {

                GridViewItem.FooterRow.Cells[5].Text = "<b>Total : </b>";
                GridViewItem.FooterRow.Cells[6].Text = "<b>" + TAmount.ToString() + "</b>";
                GridViewItem.FooterRow.Cells[7].Text = "<b>" + CGSTAmount.ToString() + "</b>";
                GridViewItem.FooterRow.Cells[8].Text = "<b>" + SGSTAmount.ToString() + "</b>";
                GridViewItem.FooterRow.Cells[9].Text = "<b>" + IGSTAmount.ToString() + "</b>";
                GridViewItem.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                GridViewItem.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                GridViewItem.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                GridViewItem.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
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
            DataColumn RowNo1 = dt_GridViewLedger.Columns.Add("RowNo", typeof(int));
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
            dt_GridViewLedger.Columns.Add(new DataColumn("CostCenter", typeof(string)));

            RowNo1.AutoIncrement = true;
            RowNo1.AutoIncrementSeed = 1;
            RowNo1.AutoIncrementStep = 1;
            gridRows = GridViewLedger.Rows.Count;
            if (gridRows > 3)
            {
                for (rowIndex = 0; rowIndex < gridRows; rowIndex++)
                {

                    Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                    Label lblRowNo = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblRowNo");
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
                    Label lblCostCenter = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCostCenter");
                    if (lblID.Text == "1" || lblID.Text == "2" || lblID.Text == "3" || lblID.Text == "4" )
                    {
                        status = "0";
                    }
                    else
                    {
                        status = "1";
                        dt_GridViewLedger.Rows.Add(lblRowNo.Text, lblID.Text, lblLedgerName.Text, txtAmount.Text, lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, status, lblGSTApplicable.Text, lblTaxbility.Text, lblCostCenter.Text);
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
                            if (dr["LedgerName"].ToString() == "IGST") // if id==2
                            {
                                dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + decimal.Parse(lblIGSTAmt.Text); //change the name
                                //break; break or not depending on you
                            }
                        }
                    }

                }
                if (dt_GridViewItem.Rows.Count > 0)
                {

                    decimal CGST = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt")) + dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    decimal SGST = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt")) + dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    decimal IGST = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("IGSTAmt")) + dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("IGSTAmt"));

                    dt_GridViewLedger.Rows.Add(4, ViewState["CGST"].ToString(), "CGST", CGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null, "No");
                    dt_GridViewLedger.Rows.Add(3, ViewState["SGST"].ToString(), "SGST", SGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null, "No");
                    dt_GridViewLedger.Rows.Add(2, ViewState["IGST"].ToString(), "IGST", IGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null, "No");

                   
                }
                else
                {
                    decimal CGST = dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    decimal SGST = dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    decimal IGST = dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("IGSTAmt"));
                    dt_GridViewLedger.Rows.Add(4, ViewState["CGST"].ToString(), "CGST", CGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null, "No");
                    dt_GridViewLedger.Rows.Add(3, ViewState["SGST"].ToString(), "SGST", SGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null, "No");
                    dt_GridViewLedger.Rows.Add(2, ViewState["IGST"].ToString(), "IGST", IGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null, "No");

                   
                }
                dt_GridViewLedger.Rows.Add(1, ViewState["RoundOff"].ToString(), "Round off", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null, "No");
            }
            else
            {
                status = "0";
                if (dt_GridViewItem.Rows.Count > 0)
                {

                    decimal CGST = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt")) + dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    decimal SGST = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt")) + dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    decimal IGST = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("IGSTAmt")) + dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("IGSTAmt"));
                    dt_GridViewLedger.Rows.Add(4, ViewState["CGST"].ToString(), "CGST", CGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null, "No");
                    dt_GridViewLedger.Rows.Add(3, ViewState["SGST"].ToString(), "SGST", SGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null, "No");
                    dt_GridViewLedger.Rows.Add(2, ViewState["IGST"].ToString(), "IGST", IGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null, "No");
                  
                }
                else
                {
                    decimal CGST = dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    decimal SGST = dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    decimal IGST = dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("IGSTAmt"));
                    dt_GridViewLedger.Rows.Add(4, ViewState["CGST"].ToString(), "CGST", CGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null, "No");
                    dt_GridViewLedger.Rows.Add(3, ViewState["SGST"].ToString(), "SGST", SGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null, "No");
                    dt_GridViewLedger.Rows.Add(2, ViewState["IGST"].ToString(), "IGST", IGST.ToString(), HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null, "No");

                  
                }
                dt_GridViewLedger.Rows.Add(1, ViewState["RoundOff"].ToString(), "Round off", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null, "No");
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
            ddlItem.ClearSelection();
            // txtLedger.Text = "";
            txtQuantity.Text = "";
            txtRate.Text = "";
            txtAmount.Text = "";
            txtUnitName.Text = "";
            lblUnitName.Text = "";
            ddlWarehouse.SelectedIndex = 1;
            SetFocus(ddlItem);
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
                
                ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", ddlLedger.SelectedValue, ViewState["Office_ID"].ToString() }, "dataset");
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["CostCentre"].ToString() == "Yes")
                            {
                                GridCostCentreDetail.DataSource = null;
                                GridCostCentreDetail.DataBind();
                                CreateCrCostCentreTable();
                                ViewState["CrCostCentre"] = ds.Tables[0].Rows[0]["CostCentre"].ToString();
                                lblCostCentreModal.Text = "";
                                ddlCategory.ClearSelection();
                                ddlSubCategory.Items.Clear();
                                //ViewState["Amount"] = txtLedgerAmt.Text;
                                ViewState["Amount"] = Math.Abs(Convert.ToDecimal(txtLedgerAmt.Text)).ToString();
                                txtCostCentreAmount.Text = ViewState["Amount"].ToString();


                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
                            }
                            else
                            {
                                int CNO = 0;

                                DataTable dt1 = new DataTable();
                                dt1.Columns.Add("RowNo", typeof(int));
                                foreach (GridViewRow rows in GridViewLedger.Rows)
                                {
                                    Label rowno = (Label)rows.FindControl("lblRowNo");
                                    dt1.Rows.Add(rowno.Text);
                                }

                                int minLavel = Convert.ToInt32(dt1.Compute("max([RowNo])", string.Empty));
                                CNO = minLavel + 1;
                                ViewState["CNO"] = CNO;
                                ViewState["CrCostCentre"] = "No";
                                FillLedgerAmount("0");
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                                ViewState["GrandTotal"] = lblGrandTotal.Text;
                                //btnAcceptEnable();
                                ddlLedger.ClearSelection();
                                txtLedgerAmt.Text = "";
                            }
                        }
                    }
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
                DataColumn RowNo = dt_GridViewLedger.Columns.Add("RowNo", typeof(int));
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
                dt_GridViewLedger.Columns.Add(new DataColumn("CostCenter", typeof(string)));

                RowNo.AutoIncrement = true;
                RowNo.AutoIncrementSeed = 1;
                RowNo.AutoIncrementStep = 1;

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
                        DataSet ds1 = objdb.ByProcedure("SpFinVoucherSaleCredit", new string[] { "flag", "Ledger_ID" }, new string[] { "3", ddlPartyName.SelectedValue.ToString() }, "dataset");
                        {
                            if (ds1 != null && ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds1.Tables[0].Rows[0]["status"].ToString() == "true")
                                {
                                    CGST = 0;
                                    SGST = 0;
                                    IGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_IntegratedTax"].ToString());



                                }
                                else
                                {
                                    IGST = 0;
                                    CGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                                    SGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                                }
                            }
                        }
                        //CGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                        //SGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                        //IGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_IntegratedTax"].ToString());
                        decimal Amount = decimal.Parse(txtLedgerAmt.Text);
                        CGSTAmt = Math.Round((Amount * CGST) / 100, 2);
                        SGSTAmt = Math.Round((Amount * SGST) / 100, 2);
                        IGSTAmt = Math.Round((Amount * IGST) / 100, 2);
                        HSN_Code = ds.Tables[0].Rows[0]["HSN_Code"].ToString();
                        dt_GridViewLedger.Rows.Add(ViewState["CNO"].ToString(), ddlLedger.SelectedValue.ToString(), ddlLedger.SelectedItem.ToString(), txtLedgerAmt.Text, HSN_Code, CGST, SGST, IGST, CGSTAmt, SGSTAmt, IGSTAmt, "1", GSTApplicable, Taxbility, ViewState["CrCostCentre"].ToString());
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
                            Label lblCostCenter = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCostCenter");
                            Label lblRowNo = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblRowNo");
                            if (lblID.Text == "1" || lblID.Text == "2" || lblID.Text == "3" || lblID.Text == "4")
                            {
                                status = "0";
                            }
                            else
                            {
                                status = "1";
                            }
                            dt_GridViewLedger.Rows.Add(lblRowNo.Text, lblID.Text, lblLedgerName.Text, txtAmount.Text, lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, status, lblGSTApplicable.Text, lblTaxbility.Text, lblCostCenter.Text);
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
                            if (dr["LedgerName"].ToString() == "IGST") // if id==2
                            {
                                dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + IGSTAmt;//change the name
                                //break; break or not depending on you
                            }
                        }
                        ViewState["LedgerAmount"] = dt_GridViewLedger;
                        GridViewLedger.DataSource = dt_GridViewLedger;
                        GridViewLedger.DataBind();
                    }
                    else
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["CNO"].ToString(), ddlLedger.SelectedValue.ToString(), ddlLedger.SelectedItem.ToString(), txtLedgerAmt.Text, HSN_Code, CGST, SGST, IGST, CGSTAmt, SGSTAmt, IGSTAmt, "1", GSTApplicable, Taxbility, ViewState["CrCostCentre"].ToString());
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
                            Label lblCostCenter = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCostCenter");
                            Label lblRowNo = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblRowNo");
                            if (lblID.Text == "1" || lblID.Text == "2" || lblID.Text == "3" || lblID.Text == "4")
                            {
                                status = "0";
                            }
                            else
                            {
                                status = "1";
                            }
                            dt_GridViewLedger.Rows.Add(lblRowNo.Text, lblID.Text, lblLedgerName.Text, txtAmount.Text, lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, status, lblGSTApplicable.Text, lblTaxbility.Text, lblCostCenter.Text);
                        }
                        ViewState["LedgerAmount"] = dt_GridViewLedger;
                        GridViewLedger.DataSource = dt_GridViewLedger;
                        GridViewLedger.DataBind();
                        if(Convert.ToString(ViewState["CrCostCentre"]) == "Yes")
                        {
                            DataSet dsCrConstCentre = (DataSet)ViewState["dsCrConstCentre"];
                            dsCrConstCentre.Merge((DataTable)ViewState["CrCostCentreTable"]);
                            ViewState["CrCostCentre"]="No";
                        }
                        
                    }

                }


            }
            else
            {
                DataTable dt_CrCostCentreTable = (DataTable)ViewState["CrCostCentreTable"];
                //dt_CrCostCentreTable = dt_CostCentreTable;
                //ViewState["CrCostCentreTable"] = dt_CrCostCentreTable;
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
                    Label lblCostCenter = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCostCenter");
                    Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                    Label lblRowNo = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblRowNo");
                    if (int.Parse(lblRowNo.Text) == int.Parse(ID))
                    {
                        for (int i = dt_GridViewLedger.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow dr = dt_GridViewLedger.Rows[i];
                            if (dr["RowNo"].ToString() == ID)
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
                            if (dr["LedgerName"].ToString() == "IGST") // if id==2
                            {
                                dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) - decimal.Parse(lblIGSTAmt.Text); //change the name
                                //break; break or not depending on you
                            }

                        }

                        DataSet dsCrConstCentre = (DataSet)ViewState["dsCrConstCentre"];
                        DataSet dsCrConstCentreTemp = new DataSet();
                        dsCrConstCentreTemp = dsCrConstCentre;
                        for (int i = 0; i < dsCrConstCentreTemp.Tables.Count;


                            i++)
                        {
                            if (dsCrConstCentreTemp.Tables[i].TableName == ID.ToString())
                            {
                                dsCrConstCentre.Tables.Remove(dsCrConstCentreTemp.Tables[i].TableName);
                                //dsBillByBill.Tables[i].Merge(dsBillByBillTemp.Tables[i]);
                            }
                        }
                        //for (int i = dt_CrCostCentreTable.Rows.Count - 1; i >= 0; i--)
                        //{
                        //    DataRow dr = dt_CrCostCentreTable.Rows[i];
                        //    if (dr["Ledger_ID"].ToString() == ID)
                        //    {
                        //        dr.Delete();
                        //    }

                        //}
                        //dt_CrCostCentreTable.AcceptChanges();
                        //ViewState["CrCostCentreTable"] = dt_CrCostCentreTable;

                    }



                }
                DataTable tblFiltered = dt_GridViewLedger.AsEnumerable()
                              .Where(r => r.Field<string>("LedgerName") == "Round off")
                              .CopyToDataTable();
                int rn = tblFiltered.Rows[0].Field<int>("RowNo");
                if (rn == 6)
                {
                    dt_GridViewLedger.DefaultView.Sort = "LedgerTx_OrderBy ASC";
                }
                else if (rn > 6)
                {
                    dt_GridViewLedger.DefaultView.Sort = "LedgerTx_OrderBy ASC";
                }
                else
                {
                    dt_GridViewLedger.DefaultView.Sort = "RowNo DESC";
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
    protected void GridViewLedger_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
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
    //Add BillByBillDetail Event & Function
    protected void CreateBillByBillTable()
    {
        DataTable dt_BillByBillTable = new DataTable();
        dt_BillByBillTable.Columns.Add(new DataColumn("RID", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTxType", typeof(string)));

        ViewState["BillByBillTable"] = dt_BillByBillTable;

        //GridViewBillByBillDetail.DataSource = dt_BillByBillTable;
        //GridViewBillByBillDetail.DataBind();
    }
    protected void CreateCostCentreTable()
    {
        //DataTable dt_CostCentreTable = new DataTable();
        //int CNO = 0;
        //int count = GridViewLedgerDetail.Rows.Count;
        //if (count > 0)
        //{
        //    foreach (GridViewRow rows in GridViewLedgerDetail.Rows)
        //    {
        //        Label rowno = (Label)rows.FindControl("lblRowNumber");
        //        CNO = int.Parse(rowno.Text);
        //    }
        //    CNO = CNO + 1;
        //}
        //else
        //{
        //    CNO = CNO + 1;
        //}
        DataTable dt_CostCentreTable = new DataTable();
        DataColumn RowNo = dt_CostCentreTable.Columns.Add("RowNo", typeof(string));
        dt_CostCentreTable.Columns.Add(new DataColumn("Ledger_ID", typeof(decimal)));
        dt_CostCentreTable.Columns.Add(new DataColumn("Category_ID", typeof(string)));
        dt_CostCentreTable.Columns.Add(new DataColumn("CategoryName", typeof(string)));
        dt_CostCentreTable.Columns.Add(new DataColumn("SubCategory_ID", typeof(string)));
        dt_CostCentreTable.Columns.Add(new DataColumn("SubCategoryName", typeof(string)));
        dt_CostCentreTable.Columns.Add(new DataColumn("AmountShow", typeof(decimal)));
        dt_CostCentreTable.Columns.Add(new DataColumn("Amount", typeof(decimal)));
        RowNo.AutoIncrement = true;
        RowNo.AutoIncrementSeed = 1;
        RowNo.AutoIncrementStep = 1;
        ViewState["CostCentreTable"] = dt_CostCentreTable;

        GridCostCentreDetail.DataSource = dt_CostCentreTable;
        GridCostCentreDetail.DataBind();
    }
    protected void CreateCrCostCentreTable()
    {
        //DataTable dt_CostCentreTable = new DataTable();
        int CNO = 0;

        DataTable dt1 = new DataTable();
        dt1.Columns.Add("RowNo", typeof(int));
        foreach (GridViewRow rows in GridViewLedger.Rows)
        {
            Label rowno = (Label)rows.FindControl("lblRowNo");
            dt1.Rows.Add(rowno.Text);
        }

        int minLavel = Convert.ToInt32(dt1.Compute("max([RowNo])", string.Empty));
        CNO = minLavel + 1;
        //if (count > 0)
        //{
        //    foreach (GridViewRow rows in GridViewLedger.Rows)
        //    {
        //        Label rowno = (Label)rows.FindControl("lblRowNo");
        //        Label lblID = (Label)rows.FindControl("lblID");

        //            if (lblID.Text != "1" || lblID.Text != "2" || lblID.Text != "3" || lblID.Text != "737"
        //                || lblID.Text != "39751" || lblID.Text != "68033")
        //            {
        //                CNO = int.Parse(rowno.Text);
        //            }
        //    }
        //    CNO = CNO + 1;
        //}
        //else
        //{
        //    CNO = CNO + 1;
        //}

        ViewState["CNO"] = CNO;
        // DataTable dt_CrCostCentreTable = new DataTable();
        DataTable dt_CrCostCentreTable = new DataTable(CNO.ToString());
        DataColumn RowNo = dt_CrCostCentreTable.Columns.Add("RowNo", typeof(string));
        dt_CrCostCentreTable.Columns.Add(new DataColumn("Ledger_ID", typeof(decimal)));
        dt_CrCostCentreTable.Columns.Add(new DataColumn("Category_ID", typeof(string)));
        dt_CrCostCentreTable.Columns.Add(new DataColumn("CategoryName", typeof(string)));
        dt_CrCostCentreTable.Columns.Add(new DataColumn("SubCategory_ID", typeof(string)));
        dt_CrCostCentreTable.Columns.Add(new DataColumn("SubCategoryName", typeof(string)));
        dt_CrCostCentreTable.Columns.Add(new DataColumn("AmountShow", typeof(decimal)));
        dt_CrCostCentreTable.Columns.Add(new DataColumn("Amount", typeof(decimal)));
        RowNo.AutoIncrement = true;
        RowNo.AutoIncrementSeed = 1;
        RowNo.AutoIncrementStep = 1;
        ViewState["CrCostCentreTable"] = dt_CrCostCentreTable;

        gvCostCentreDetail.DataSource = dt_CrCostCentreTable;
        gvCostCentreDetail.DataBind();
    }
    //Bind Ledger AgstRef Detail in BillByBillModal
    protected void ddlRefType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBillByBillData();
        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);
            if (ddlRefType.SelectedValue.ToString() == "1")
            {
                txtBillByBillTx_Ref.Visible = false;
                ddlBillByBillTx_Ref.Visible = true;
                lnkView.Visible = true;
                //txtBillByBillTx_Ref.Enabled = false;

            }
            else if (ddlRefType.SelectedValue.ToString() == "3")
            {
                txtBillByBillTx_Ref.Visible = true;
                ddlBillByBillTx_Ref.Visible = false;
                txtBillByBillTx_Ref.Enabled = false;
                txtBillByBillTx_Ref.Text = "On Account";
            }
            else
            {
                //  txtBillByBillTx_Ref.Text = txtInvoice.Text;


                if (ddlRefType.SelectedValue == "2")
                {
                    txtBillByBillTx_Ref.Text = ViewState["VoucherTx_NoBill"].ToString();
                }
                txtBillByBillTx_Ref.Visible = true;
                ddlBillByBillTx_Ref.Visible = false;
                txtBillByBillTx_Ref.Enabled = true;
                lnkView.Visible = false;

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void BindBillByBillData()
    {
        try
        {
            if (btnAccept.Text == "Accept")
            {
                DataTable dt_BillByBillData = (DataTable)ViewState["dt_BillByBillData"];

                ddlBillByBillTx_Ref.Items.Clear();
                string LedgerID = ddlPartyName.SelectedValue.ToString();
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
                string LedgerID = ddlPartyName.SelectedValue.ToString();
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

            //ViewState["Amount"] = Math.Abs(Convert.ToDecimal(lblGrandTotal.Text));
            string LedgerAmount = FillRefAmount("0");
            decimal Status = decimal.Parse(ViewState["LedgerAmount"].ToString()) - decimal.Parse(LedgerAmount.ToString());
            if (Status == 0)
            {
                Save("BillByBill");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);
                txtBillByBillTx_Ref.Visible = true;

                if (ddlRefType.SelectedValue == "2")
                {
                    txtBillByBillTx_Ref.Text = ViewState["VoucherTx_NoBill"].ToString();
                }
                // txtBillByBillTx_Ref.Text = txtInvoice.Text;
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
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }


    }
    protected string FillRefAmount(string ID)
    {
        decimal LedgerAmount = 0;
        try
        {
            DataTable dt_BillByBillTable = new DataTable();
            dt_BillByBillTable.Columns.Add(new DataColumn("RID", typeof(string)));
            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTxType", typeof(string)));
            string Type = "";

            if (ddlBillByBillTx_crdr.SelectedValue == "Cr")
            {
                Type = "Cr";
            }
            else
            {
                Type = "Dr";
            }
            int gridRows = GridViewRef.Rows.Count;
            for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {
                Label lblID = (Label)GridViewRef.Rows[rowIndex].Cells[0].FindControl("lblID");
                Label lblTypeOfRef = (Label)GridViewRef.Rows[rowIndex].Cells[0].FindControl("lblTypeOfRef");
                Label lblRefNo = (Label)GridViewRef.Rows[rowIndex].Cells[1].FindControl("lblRefNo");
                Label lblAmount = (Label)GridViewRef.Rows[rowIndex].Cells[2].FindControl("lblAmount");
                Label lblType = (Label)GridViewRef.Rows[rowIndex].Cells[2].FindControl("lblType");
                if (lblID.Text != ID)
                {
                    dt_BillByBillTable.Rows.Add(lblID.Text, lblTypeOfRef.Text, lblRefNo.Text, lblAmount.Text, lblType.Text);
                    //if (lblType.Text == "Cr")
                    //{
                    //    if (lblGrandTotal.Text.Contains("-"))
                    //    {
                    //        Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"])+ Convert.ToDecimal(lblAmount.Text));
                    //        ViewState["Amount"] = Amount.ToString();
                    //        lblAmount.Text = ViewState["Amount"].ToString();
                    //    }
                    //    else
                    //    {
                    //        Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(lblAmount.Text));
                    //        ViewState["Amount"] = Amount.ToString();
                    //        lblAmount.Text = ViewState["Amount"].ToString();
                    //    }

                    //}
                    //else
                    //{
                    //    if (lblGrandTotal.Text.Contains("-"))
                    //    {
                    //        Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(lblAmount.Text));
                    //        ViewState["Amount"] = Amount.ToString();
                    //        lblAmount.Text = ViewState["Amount"].ToString();
                    //    }
                    //    else
                    //    {
                    //        Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) + Convert.ToDecimal(lblAmount.Text));
                    //        ViewState["Amount"] = Amount.ToString();
                    //        lblAmount.Text = ViewState["Amount"].ToString();
                    //    }

                    //}

                }

            }
            if (ID == "0")
            {
                int RefType = ddlRefType.SelectedIndex;
                string RefNo = "";
                if (RefType == 0 || RefType == 2)
                {
                    RefNo = txtBillByBillTx_Ref.Text;
                }
                else if (RefType == 3)
                {
                    RefNo = "OnAccount";
                }
                else
                {
                    RefNo = ddlBillByBillTx_Ref.SelectedValue;
                }
                dt_BillByBillTable.Rows.Add((gridRows + 1).ToString(), ddlRefType.SelectedItem.Text, RefNo, txtBillByBillTx_Amount.Text, Type);

            }


            DataTable dt_BillByBillData = (DataTable)ViewState["dt_BillByBillData"];
            //foreach (DataRow rows in dt_BillByBillData.Rows)
            //{
            //    if (rows["BillByBillTx_Ref"].ToString().Equals(ddlBillByBillTx_Ref.SelectedValue))
            //    {
            //        dt_BillByBillData.Rows.Remove(rows);
            //        dt_BillByBillData.AcceptChanges();
            //        break;
            //    }
            //}
            ViewState["dt_BillByBillData"] = dt_BillByBillData;
            ddlBillByBillTx_Ref.DataSource = dt_BillByBillData;
            ddlBillByBillTx_Ref.DataTextField = "AgnstRef";
            ddlBillByBillTx_Ref.DataValueField = "BillByBillTx_Ref";
            ddlBillByBillTx_Ref.DataBind();
            ddlBillByBillTx_Ref.Items.Insert(0, "Select");




            GridViewRef.DataSource = dt_BillByBillTable;
            GridViewRef.DataBind();
            foreach (GridViewRow rows in GridViewRef.Rows)
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
            //decimal RefTotal = 0;
            //RefTotal = dt_BillByBillTable.AsEnumerable().Sum(row => row.Field<decimal>("BillByBillTx_Amount"));

            //GridViewBillByBillDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
            //GridViewBillByBillDetail.FooterRow.Cells[3].Text = "<b>" + RefTotal.ToString() + "</b>";

            //txtBillByBillTx_Amount.Text = (Convert.ToDecimal(txtLedgerTx_Amount.Text) - RefTotal).ToString();
            txtBillByBillTx_Ref.Text = "";
            ddlRefType.ClearSelection();
            //if (decimal.Parse(ViewState["Amount"].ToString()) == 0)
            //{
            //    Save("BillByBill");
            //}
            //else
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);
            //}
            ViewState["BillByBillTable"] = dt_BillByBillTable;



            //txtBillByBillTx_Ref.Visible = true;
            //txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
            ////BindBillByBillData();
            //ddlRefType.ClearSelection();
            //ddlBillByBillTx_crdr.SelectedValue = "Cr";
            //txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();
            //txtBillByBillTx_Ref.Enabled = true;
            //ddlBillByBillTx_Ref.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        return LedgerAmount.ToString();
    }
    //AddChequeDetail Event & Function
    protected void CreatTableFinChequeTx()
    {

        DataTable dt_FinChequeTx = new DataTable();
        dt_FinChequeTx.Columns.Add(new DataColumn("RID", typeof(string)));
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
            ViewState["Amount"] = lblGrandTotal.Text;
            FillChequeAmount("0");
        }
        else
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }

    }
    protected void FillChequeAmount(string ID)
    {
        try
        {
            DataTable dt_FinChequeTx = new DataTable();
            dt_FinChequeTx.Columns.Add(new DataColumn("RID", typeof(string)));
            dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_No", typeof(string)));
            dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Date", typeof(string)));
            dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Amount", typeof(decimal)));


            int gridRows = GVFinChequeTx.Rows.Count;
            for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {
                Label RID = (Label)GVFinChequeTx.Rows[rowIndex].Cells[0].FindControl("lblID");
                Label ChequeTx_No = (Label)GVFinChequeTx.Rows[rowIndex].Cells[0].FindControl("lblChequeTx_No");
                Label ChequeTx_Date = (Label)GVFinChequeTx.Rows[rowIndex].Cells[0].FindControl("lblChequeTx_Date");
                Label ChequeTx_Amount = (Label)GVFinChequeTx.Rows[rowIndex].Cells[0].FindControl("lblChequeTx_Amount");

                if (RID.Text != ID)
                {
                    dt_FinChequeTx.Rows.Add(RID.Text, ChequeTx_No.Text, ChequeTx_Date.Text, ChequeTx_Amount.Text);

                }

            }
            if (ID == "0")
            {

                dt_FinChequeTx.Rows.Add((gridRows + 1).ToString(), txtChequeTx_No.Text, txtChequeTx_Date.Text, txtChequeTx_Amount.Text);
            }

            GVFinChequeTx.DataSource = dt_FinChequeTx;
            GVFinChequeTx.DataBind();
            ViewState["FinChequeTx"] = dt_FinChequeTx;

            decimal RefTotal = 0;
            RefTotal = dt_FinChequeTx.AsEnumerable().Sum(row => row.Field<decimal>("ChequeTx_Amount"));
            ViewState["Amount"] = decimal.Parse(ViewState["Amount"].ToString()) - RefTotal;
            //GridViewBillByBillDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
            //GridViewBillByBillDetail.FooterRow.Cells[3].Text = "<b>" + RefTotal.ToString() + "</b>";

            //txtBillByBillTx_Amount.Text = (Convert.ToDecimal(txtLedgerTx_Amount.Text) - RefTotal).ToString();
            txtChequeTx_No.Text = "";
            txtChequeTx_Amount.Text = "";
            txtChequeTx_Date.Text = "";
            if (decimal.Parse(ViewState["Amount"].ToString()) == 0)
            {
                Save("Cheque");

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
                txtChequeTx_No.Text = "";
                txtChequeTx_Amount.Text = ViewState["Amount"].ToString();
                txtChequeTx_Date.Text = "";
            }


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
            ViewState["CrCostCentre"] = "No";
            //txtBillByBillTx_Ref.Text = txtInvoice.Text;
            txtBillByBillTx_Ref.Visible = true;
            ddlBillByBillTx_Ref.Visible = false;
            txtBillByBillTx_Ref.Enabled = true;
            lnkView.Visible = false;
            //txtBillByBillTx_Ref.Text = txtInvoice.Text;
            txtBillByBillTx_Amount.Text = lblGrandTotal.Text;
            ViewState["BillByBillAmount"] = "0";
            if (lblGrandTotal.Text.Contains("-"))
            {
                ViewState["LedgerAmount"] = "-" + lblGrandTotal.Text;
            }
            else
            {
                ViewState["LedgerAmount"] = lblGrandTotal.Text;
            }
            ddlRefType.SelectedValue = "2";
            string VoucherTx_No = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
            ViewState["VoucherTx_NoBill"] = VoucherTx_No;
            lblMsg.Text = "";
            string msg = "";
            if (txtVoucherTx_No.Text == "")
            {
                msg += "Enter Voucher No. \\n";
            }
            //if (txtInvoice.Text == "")
            //{
            //    msg += "Enter Supplier / Invoice No. \\n";
            //}
            if (txtVoucherTx_Date.Text == "")
            {
                msg += "Enter Date. \\n";
            }
            else
            {
                //string ValidStatus = ValidDate();
                //if (ValidStatus == "No")
                //{
                //    Response.Redirect("~/mis/Login.aspx");
                //}
            }
            if (ddlPartyName.SelectedIndex == 0)
            {
                msg += "Select Party A/c Name. \\n";
            }
            if (chkitem.Checked == true)
            {
                if (GridViewItem.Rows.Count == 0)
                {
                    msg += "Enter item Detail. \\n";
                }
            }

            if (txtVoucherTx_Narration.Text == "")
            {
                msg += "Enter Narration. \\n";
            }

            if (msg.Trim() == "")
            {
                string LedgerId = ddlPartyName.SelectedValue.ToString();
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
                    GridViewRef.DataSource = new string[] { };
                    GridViewRef.DataBind();
                    ViewState["Amount"] = Math.Abs(Convert.ToDecimal(lblGrandTotal.Text));
                    ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", LedgerId, ViewState["Office_ID"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "Yes")
                        {
                            if (lblGrandTotal.Text.Contains("-"))
                            {
                                ddlBillByBillTx_crdr.SelectedValue = "Dr";
                            }
                            else
                            {
                                ddlBillByBillTx_crdr.SelectedValue = "Cr";
                            }
                            if (ddlRefType.SelectedValue == "2")
                            {
                                txtBillByBillTx_Ref.Text = VoucherTx_No;
                            }


                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);


                        }
                        else if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "BankLedger")
                        {
                            btnAddCheque.Enabled = true;
                            //btnAddChequeDetail.Enabled = false;
                            CreatTableFinChequeTx();
                            txtChequeTx_Amount.Text = ViewState["Amount"].ToString();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
                        }
                        else if (ds.Tables[0].Rows[0]["CostCentre"].ToString() == "Yes")
                        {
                            ViewState["CostCentre"] = ds.Tables[0].Rows[0]["CostCentre"].ToString();

                            CreateCostCentreTable();
                            ddlCategory.ClearSelection();
                            ddlSubCategory.Items.Clear();
                            ViewState["Amount"] = lblGrandTotal.Text;
                            txtCostCentreAmount.Text = ViewState["Amount"].ToString();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
                        }
                        else
                        {
                            Save("None");
                        }

                    }
                }
                else if (btnAccept.Text == "Update")
                {

                    ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", LedgerId, ViewState["Office_ID"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "Yes")
                        {
                            btnAddBillByBill.Enabled = true;
                            if (lblGrandTotal.Text.Contains("-"))
                            {
                                ddlBillByBillTx_crdr.SelectedValue = "Dr";
                            }
                            else
                            {
                                ddlBillByBillTx_crdr.SelectedValue = "Cr";
                            }

                            if (ddlRefType.SelectedValue == "2")
                            {
                                txtBillByBillTx_Ref.Text = VoucherTx_No;
                            }


                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);
                            if (GridViewRef.Rows.Count > 0)
                            {
                                DataTable dt_BillByBillTable = (DataTable)ViewState["BillByBillTable"];
                                decimal BillByBillAmount = dt_BillByBillTable.AsEnumerable().Sum(row => row.Field<decimal>("BillByBillTx_Amount"));
                                decimal Amount = decimal.Parse(lblGrandTotal.Text) - BillByBillAmount;
                                txtBillByBillTx_Amount.Text = Amount.ToString();
                                if (BillByBillAmount == decimal.Parse(lblGrandTotal.Text))
                                {
                                    btnAddBillByBill.Enabled = false;
                                }
                            }

                        }
                        else if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "BankLedger")
                        {
                            btnAddCheque.Enabled = true;
                            //btnAddChequeDetail.Enabled = false;
                            CreatTableFinChequeTx();
                            GVFinChequeTx.DataSource = new string[] { };
                            GVFinChequeTx.DataBind();
                            txtChequeTx_Amount.Text = (Math.Abs(Convert.ToDecimal(lblGrandTotal.Text))).ToString();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
                        }
                        else if (ds.Tables[0].Rows[0]["CostCentre"].ToString() == "Yes")
                        {
                            ViewState["CostCentre"] = ds.Tables[0].Rows[0]["CostCentre"].ToString();
                            CreateCostCentreTable();
                            lblCostCentreModal.Text = "";
                            ddlCategory.ClearSelection();
                            ddlSubCategory.Items.Clear();
                            ViewState["Amount"] = lblGrandTotal.Text;
                            txtCostCentreAmount.Text = ViewState["Amount"].ToString();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
                        }
                        else
                        {
                            Save("None");
                        }



                    }




                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Voucher No is already exist.');", true);
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
    protected void Save(string Type)
    {
        try
        {
            GridViewBillByBillDetail.DataSource = new string[] { };
            GridViewBillByBillDetail.DataBind();
            GridViewChequeDetail.DataSource = new string[] { };
            GridViewChequeDetail.DataBind();

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


            string VoucherTx_Type = "CreditSale Voucher";
            string VoucherTx_Name = "Sales Voucher";
            //string VoucherTx_Ref = txtInvoice.Text;
            string VoucherTx_Amount = lblGrandTotal.Text;
            string ItemTx_IsActive = "1";
            string LedgerTx_IsActive = "1";
            string VoucherTx_IsActive = "1";
            string SupplyTo = txtSupplyTo.Text;
            string Location = txtLocation.Text;

            //string TruckNo = txtTruckNo.Text;
            //string TransporterName = txtTransporterName.Text;
            //string MRNo = txtMRNo.Text;
            //string MRDate = txtMRDate.Text;

            if (btnAccept.Text == "Accept")
            {
                ItemTx_IsActive = "0";
                LedgerTx_IsActive = "0";
                VoucherTx_IsActive = "0";
                string VoucherTx_ID = "0";
                DataSet ds2 = objdb.ByProcedure("SpFinVoucherTx",
        new string[] { "flag","VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type"
                        , "VoucherTx_No",  "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY"
                        , "VoucherTx_IsActive", "VoucherTx_InsertedBy","GSTVoucher"
                        //,
                        //"SupplyTo","Location","TruckNo","TransporterName","MRNo","MRDate" 
        },
        new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), VoucherTx_Name, VoucherTx_Type,VoucherTx_No
                       ,txtVoucherTx_Narration.Text,lblGrandTotal.Text,Month.ToString(),Year.ToString(), ViewState["Office_ID"].ToString(),FinancialYear.ToString()
                        ,VoucherTx_IsActive,ViewState["Emp_ID"].ToString(),"Yes"
                        //,SupplyTo,Location,TruckNo,TransporterName,MRNo,MRDate
        }, "dataset");

                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    VoucherTx_ID = ds2.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
                    int gridRows = GridViewItem.Rows.Count;
                    for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                    {
                        Label lblItemRowNo = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemRowNo");
                        Label lblItemID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
                        Label lblUnit_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblUnit_id");
                        Label lblWarehouse_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_id");
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
                        Label lblLedgerID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblLedgerID");
                        //Label lblCessTaxApplicable = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblCessTaxApplicable");
                        //Label lblCessTaxCalType = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblCessTaxCalType");
                        //Label CessTax = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("CessTax");
                        //Label lblCessTaxRate = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblCessTaxRate");
                        Label lblTaxbility = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                        objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Name", "VoucherTx_Type", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "HSN_Code", "IGST_Per", "CGST_Per", "SGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Warehouse_id", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy", "Taxbility" },
                            new string[] { "0", VoucherTx_ID, lblLedgerID.Text, VoucherTx_Name.ToString(), VoucherTx_Type.ToString(), lblItemID.Text, lblUnit_id.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, lblHSNCode.Text, lblIGSTPer.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblCGST.Text, lblSGST.Text, lblIGST.Text, lblWarehouse_id.Text, ViewState["Office_ID"].ToString(), FinancialYear.ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), lblItemRowNo.Text, lblTaxbility.Text }, "dataset");
                        ds = objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "Ledger_ID" }, new string[] { "3", lblLedgerID.Text }, "dataset");
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["InventoryAffected"].ToString() == "Yes")
                            {
                                objdb.ByProcedure("SpFinItemTx",
                                    new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "TransactionFrom", "InvoiceNo", "Office_Id", "Warehouse_id", "CreatedBy", "TranDt" }
                                   , new string[] { "4", lblItemID.Text, "0", lblQuantity.Text, lblRate.Text, VoucherTx_ID, "CreditSale Voucher", VoucherTx_No, ViewState["Office_ID"].ToString(), lblWarehouse_id.Text, ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                            }
                        }
                        string Amount = lblAmount.Text;
                        //Amount = "-" + Amount;
                        objdb.ByProcedure("SpFinLedgerTx",
                        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
                        new string[] { "0", lblLedgerID.Text, "Item Ledger", VoucherTx_ID, lblItemID.Text, VoucherTx_Type, Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), lblItemRowNo.Text }, "dataset");
                    }
                    int gridLedgerRows = GridViewLedger.Rows.Count;
                    for (int rowIndex = 0; rowIndex < gridLedgerRows; rowIndex++)
                    {
                        string LedgerTx_Amount = "0";
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
                        Label lblCostCenter = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCostCenter");
                        Label lblRowNo = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblRowNo");
                        //if (txtAmount.Text.Contains("-"))
                        //{
                        //    LedgerTx_Amount = txtAmount.Text.Replace(@"-", string.Empty);
                        //}
                        //else
                        //{
                        //    LedgerTx_Amount = "-" + txtAmount.Text;
                        //}
                        LedgerTx_Amount = txtAmount.Text;
                        // string LedgerTx_Amount = "-" + txtAmount.Text;
                        if (lblCostCenter.Text == "Yes")
                        {
                            DataSet dsCrConstCentre = (DataSet)ViewState["dsCrConstCentre"];
                            DataTable dt_CrCostCentreTable = dsCrConstCentre.Tables[lblRowNo.Text];
                            // DataTable dt_CrCostCentreTable = (DataTable)ViewState["CrCostCentreTable"];
                            int Count = dt_CrCostCentreTable.Rows.Count;
                            for (int i = 0; i < Count; i++)
                            {
                                DataRow dr = dt_CrCostCentreTable.Rows[i];
                                if (dr["Ledger_ID"].ToString() == lblID.Text)
                                {
                                    string Category_ID = dr["Category_ID"].ToString();
                                    string CategoryName = dr["CategoryName"].ToString();
                                    string SubCategory_ID = dr["SubCategory_ID"].ToString();
                                    string SubCategoryName = dr["SubCategoryName"].ToString();
                                    string AmountShow = dr["AmountShow"].ToString();
                                    string Amount = dr["Amount"].ToString();
                                    if (txtAmount.Text.Contains("-"))
                                    {
                                        Amount = "-" + Amount;
                                        AmountShow = "-" + AmountShow;
                                    }
                                    objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "Voucher_Date", "Office_ID", "Category_ID", "SubCategory_ID", "Amount", "CostCentre_type", "LedgerTx_OrderBy", "UpdatedBy" },
                              new string[] { "1", VoucherTx_ID, lblID.Text, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), Category_ID, SubCategory_ID, Amount, "CreditSale Voucher", lblRowNo.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                                }
                            }

                            objdb.ByProcedure("SpFinLedgerTx",
                                  new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType","HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "GSTApplicable", "Taxbility" },
                                  new string[] { "0", lblID.Text,"Sub Ledger",VoucherTx_ID,VoucherTx_Type,LedgerTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),lblRowNo.Text,"CostCentre",lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, lblGSTApplicable.Text, lblTaxbility.Text}, "dataset");
                            //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                        }
                        else
                        {
                            objdb.ByProcedure("SpFinLedgerTx",
                        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "GSTApplicable", "Taxbility" },
                        new string[] { "0", lblID.Text, "Sub Ledger", VoucherTx_ID, VoucherTx_Type, LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), lblRowNo.Text, lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, lblGSTApplicable.Text, lblTaxbility.Text }, "dataset");
                        }





                    }
                   

                    
                    if (Type == "BillByBill")
                    {
                        objdb.ByProcedure("SpFinLedgerTx",
                   new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
                   new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,"-" + VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","BillByBill"}, "dataset");


                        int gridBillbyBillRows = GridViewRef.Rows.Count;
                        for (int k = 0; k < gridBillbyBillRows; k++)
                        {
                            Label BillByBillTx_RefType = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblTypeOfRef");
                            Label BillByBillTx_Ref = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblRefNo");
                            Label BillByBillTx_Amount = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblAmount");
                            Label BillType = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblType");
                            if (BillType.Text == "Dr")
                            {
                                BillByBillTx_Amount.Text = "-" + BillByBillTx_Amount.Text;
                            }

                            objdb.ByProcedure("SpFinBillByBillTx",
                                        new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy" },
                                        new string[] { "3", VoucherTx_ID, ddlPartyName.SelectedValue.ToString(), BillByBillTx_RefType.Text, BillByBillTx_Ref.Text, BillByBillTx_Amount.Text, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "0", (k + 1).ToString() }, "dataset");
                        }
                    }
                    else if (Type == "Cheque")
                    {
                        objdb.ByProcedure("SpFinLedgerTx",
                   new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
                   new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,"-" + VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","Cheque"}, "dataset");


                        int gridChequeRows = GVFinChequeTx.Rows.Count;
                        for (int k = 0; k < gridChequeRows; k++)
                        {
                            Label RID = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblID");
                            Label ChequeTx_No = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_No");
                            Label ChequeTx_Date = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_Date");
                            Label ChequeTx_Amount = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_Amount");

                            if (ChequeTx_No.Text == "")
                            {
                                ChequeTx_No.Text = "";
                            }
                            else
                            {

                            }
                            if (ChequeTx_Date.Text == "")
                            {
                                ChequeTx_Date.Text = "";
                            }
                            else
                            {
                                ChequeTx_Date.Text = Convert.ToDateTime(ChequeTx_Date.Text, cult).ToString("yyyy/MM/dd");
                            }

                            objdb.ByProcedure("SpFinChequeTx",
                                                 new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy" },
                                                 new string[] { "1", VoucherTx_ID, ddlPartyName.SelectedValue.ToString(), "CreditSale Voucher", ChequeTx_No.Text, ChequeTx_Date.Text, ChequeTx_Amount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "0", ViewState["Emp_ID"].ToString(), (k + 1).ToString() }, "dataset");
                        }
                    }
                    else if (Type == "CostCentre")
                    {
                        //  objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID" },
                        //new string[] { "3", VoucherTx_ID }, "dataset");
                        foreach (GridViewRow row in GridCostCentreDetail.Rows)
                        {
                            Label lblCategory_ID = (Label)row.FindControl("lblCategory_ID");
                            Label lblCategoryName = (Label)row.FindControl("lblCategoryName");
                            Label lblSubCategory_ID = (Label)row.FindControl("lblSubCategory_ID");
                            Label lblSubCategoryName = (Label)row.FindControl("lblSubCategoryName");
                            Label lblAmountShow = (Label)row.FindControl("lblAmountShow");
                            Label lblAmount = (Label)row.FindControl("lblAmount");

                            objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "Voucher_Date", "Office_ID", "Category_ID", "SubCategory_ID", "Amount", "CostCentre_type", "LedgerTx_OrderBy", "UpdatedBy" },
                          new string[] { "1", VoucherTx_ID, ddlPartyName.SelectedValue, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), lblCategory_ID.Text, lblSubCategory_ID.Text, "-" + lblAmount.Text, "CreditSale Voucher", "1", ViewState["Emp_ID"].ToString() }, "dataset");
                        }
                        VoucherTx_Amount = "-" + VoucherTx_Amount;
                        objdb.ByProcedure("SpFinLedgerTx",
                              new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
                              new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","CostCentre"}, "dataset");
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                    }
                    else
                    {
                        VoucherTx_Amount = "-" + VoucherTx_Amount;//06Mar2021

                        objdb.ByProcedure("SpFinLedgerTx",
                   new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
                   new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","None"}, "dataset");
                    }
                    objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "40", VoucherTx_ID }, "dataset");

                }

                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                ClearData();
                CreateCrCostCentreDataSet();
                if (rbtPrint.SelectedValue.ToString() == "Yes")
                {
                    rbtPrint.SelectedValue = "No";
                    string url = "VoucherSalepurchaseInvocie.aspx?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID).ToString();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.open('");
                    sb.Append(url);
                    sb.Append("');");
                    sb.Append("</script>");
                    ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
                }
            }
            else if (btnAccept.Text == "Update")
            {
                string VoucherTx_ID = ViewState["VoucherTx_ID"].ToString();
                objdb.ByProcedure("SpFinVoucherTx",
        new string[] { "flag","VoucherTx_ID","VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type"
                        , "VoucherTx_No", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY"
                        , "VoucherTx_IsActive", "VoucherTx_InsertedBy"
                        //,"SupplyTo","Location","TruckNo","TransporterName","MRNo","MRDate"
        },
        new string[] { "7",ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), VoucherTx_Name, VoucherTx_Type,VoucherTx_No
                       ,txtVoucherTx_Narration.Text,lblGrandTotal.Text,Month.ToString(),Year.ToString(), ViewState["Office_ID"].ToString(),FinancialYear.ToString()
                        ,VoucherTx_IsActive,ViewState["Emp_ID"].ToString(),SupplyTo,Location
                        //,TruckNo,TransporterName,MRNo,MRDate
                        }, "dataset");

                objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "1", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "3", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                int gridRows = GridViewItem.Rows.Count;
                for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                {
                    Label lblItemRowNo = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemRowNo");
                    Label lblItemID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
                    Label lblUnit_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblUnit_id");
                    Label lblWarehouse_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_id");
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
                    Label lblLedgerID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblLedgerID");
                    //Label lblCessTaxApplicable = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblCessTaxApplicable");
                    //Label lblCessTaxCalType = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblCessTaxCalType");
                    //Label CessTax = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("CessTax");
                    //Label lblCessTaxRate = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblCessTaxRate");
                    Label lblTaxbility = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                    objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Name", "VoucherTx_Type", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "HSN_Code", "IGST_Per", "CGST_Per", "SGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Warehouse_id", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy","Taxbility" }, new string[] { "0", ViewState["VoucherTx_ID"].ToString(), lblLedgerID.Text, VoucherTx_Name.ToString(), VoucherTx_Type.ToString(), lblItemID.Text, lblUnit_id.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, lblHSNCode.Text, lblIGSTPer.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblCGST.Text, lblSGST.Text, lblIGST.Text, lblWarehouse_id.Text, ViewState["Office_ID"].ToString(), FinancialYear.ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), lblItemRowNo.Text,lblTaxbility.Text }, "dataset");
                    ds = objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "Ledger_ID" }, new string[] { "3", lblLedgerID.Text }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["InventoryAffected"].ToString() == "Yes")
                        {
                            objdb.ByProcedure("SpFinItemTx",
                                new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "TransactionFrom", "InvoiceNo", "Office_Id", "Warehouse_id", "CreatedBy", "TranDt" }
                               , new string[] { "4", lblItemID.Text, "0", lblQuantity.Text, lblRate.Text, ViewState["VoucherTx_ID"].ToString(), "CreditSale Voucher", VoucherTx_No, ViewState["Office_ID"].ToString(), lblWarehouse_id.Text, ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                        }
                    }
                    string Amount = lblAmount.Text;
                    // Amount = "-" + Amount;
                    objdb.ByProcedure("SpFinLedgerTx",
                    new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
                    new string[] { "0", lblLedgerID.Text, "Item Ledger", ViewState["VoucherTx_ID"].ToString(), lblItemID.Text, VoucherTx_Type, Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), lblItemRowNo.Text }, "dataset");
                }

                int gridLedgerRows = GridViewLedger.Rows.Count;
                for (int rowIndex = 0; rowIndex < gridLedgerRows; rowIndex++)
                {
                    string LedgerTx_Amount = "0";
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
                    Label lblCostCenter = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCostCenter");
                    Label lblRowNo = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblRowNo");
                    //if (txtAmount.Text.Contains("-"))
                    //{
                    //    LedgerTx_Amount = txtAmount.Text.Replace(@"-", string.Empty);
                    //}
                    //else
                    //{
                    //    LedgerTx_Amount = "-" + txtAmount.Text;
                    //}
                    LedgerTx_Amount = txtAmount.Text;
                    // string LedgerTx_Amount = "-" + txtAmount.Text;
                    if (lblCostCenter.Text == "Yes")
                    {
                        DataSet dsCrConstCentre = (DataSet)ViewState["dsCrConstCentre"];
                        DataTable dt_CrCostCentreTable = dsCrConstCentre.Tables[lblRowNo.Text];
                        //DataTable dt_CrCostCentreTable = (DataTable)ViewState["CrCostCentreTable"];
                        int Count = dt_CrCostCentreTable.Rows.Count;
                        for (int i = 0; i < Count; i++)
                        {
                            DataRow dr = dt_CrCostCentreTable.Rows[i];
                            if (dr["Ledger_ID"].ToString() == lblID.Text)
                            {
                                string Category_ID = dr["Category_ID"].ToString();
                                string CategoryName = dr["CategoryName"].ToString();
                                string SubCategory_ID = dr["SubCategory_ID"].ToString();
                                string SubCategoryName = dr["SubCategoryName"].ToString();
                                string AmountShow = dr["AmountShow"].ToString();
                                string Amount = dr["Amount"].ToString();
                                if (txtAmount.Text.Contains("-"))
                                {
                                    Amount = "-" + Amount;
                                    AmountShow = "-" + AmountShow;
                                }
                                objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "Voucher_Date", "Office_ID", "Category_ID", "SubCategory_ID", "Amount", "CostCentre_type", "LedgerTx_OrderBy", "UpdatedBy" },
                          new string[] { "1", VoucherTx_ID, lblID.Text, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), Category_ID, SubCategory_ID, Amount, "CreditSale Voucher", lblRowNo.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                            }
                        }

                        objdb.ByProcedure("SpFinLedgerTx",
                              new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType","HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "GSTApplicable", "Taxbility" },
                              new string[] { "0", lblID.Text,"Sub Ledger",VoucherTx_ID,VoucherTx_Type,LedgerTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),lblRowNo.Text,"CostCentre",lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, lblGSTApplicable.Text, lblTaxbility.Text}, "dataset");
                        //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                    }
                    else
                    {
                        objdb.ByProcedure("SpFinLedgerTx",
                    new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "GSTApplicable", "Taxbility" },
                    new string[] { "0", lblID.Text, "Sub Ledger", VoucherTx_ID, VoucherTx_Type, LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), lblRowNo.Text, lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, lblGSTApplicable.Text, lblTaxbility.Text }, "dataset");
                    }
                    //objdb.ByProcedure("SpFinLedgerTx",
                    //new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "GSTApplicable", "Taxbility" },
                    //new string[] { "0", lblID.Text, "Sub Ledger", ViewState["VoucherTx_ID"].ToString(), VoucherTx_Type, LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 1).ToString(), lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, lblGSTApplicable.Text, lblTaxbility.Text }, "dataset");

                }
                
              
                objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "4", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                objdb.ByProcedure("SpFinChequeTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");

                if (Type == "BillByBill")
                {

                    DataTable dt_BillByBillTable = (DataTable)ViewState["BillByBillTable"];
                    GridViewBillByBillDetail.DataSource = dt_BillByBillTable;
                    GridViewBillByBillDetail.DataBind();
                    objdb.ByProcedure("SpFinLedgerTx",
               new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
               new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",ViewState["VoucherTx_ID"].ToString(),VoucherTx_Type,"-" + VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","BillByBill"}, "dataset");


                    int gridBillbyBillRows = GridViewRef.Rows.Count;
                    for (int k = 0; k < gridBillbyBillRows; k++)
                    {


                        Label BillByBillTx_RefType = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblTypeOfRef");
                        Label BillByBillTx_Ref = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblRefNo");
                        Label BillByBillTx_Amount = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblAmount");
                        Label BillType = (Label)GridViewRef.Rows[k].Cells[0].FindControl("lblType");
                        if (BillType.Text == "Dr")
                        {
                            BillByBillTx_Amount.Text = "-" + BillByBillTx_Amount.Text;
                        }

                        objdb.ByProcedure("SpFinBillByBillTx",
                                    new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy" },
                                    new string[] { "3", ViewState["VoucherTx_ID"].ToString(), ddlPartyName.SelectedValue.ToString(), BillByBillTx_RefType.Text, BillByBillTx_Ref.Text, BillByBillTx_Amount.Text, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", (k + 1).ToString() }, "dataset");

                    }
                }
                else if (Type == "Cheque")
                {
                    DataTable dt_FinChequeTx = (DataTable)ViewState["FinChequeTx"];
                    GridViewChequeDetail.DataSource = dt_FinChequeTx;
                    GridViewChequeDetail.DataBind();
                    objdb.ByProcedure("SpFinLedgerTx",
               new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
               new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",ViewState["VoucherTx_ID"].ToString(),VoucherTx_Type,"-" + VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","Cheque"}, "dataset");


                    int gridChequeRows = GVFinChequeTx.Rows.Count;
                    for (int k = 0; k < gridChequeRows; k++)
                    {


                        Label RID = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblID");
                        Label ChequeTx_No = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_No");
                        Label ChequeTx_Date = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_Date");
                        Label ChequeTx_Amount = (Label)GVFinChequeTx.Rows[k].Cells[0].FindControl("lblChequeTx_Amount");

                        if (ChequeTx_No.Text == "")
                        {
                            ChequeTx_No.Text = "";
                        }
                        else
                        {

                        }

                        if (ChequeTx_Date.Text == "")
                        {
                            ChequeTx_Date.Text = "";
                        }
                        else
                        {
                            ChequeTx_Date.Text = Convert.ToDateTime(ChequeTx_Date.Text, cult).ToString("yyyy/MM/dd");
                        }

                        objdb.ByProcedure("SpFinChequeTx",
                                             new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy" },
                                             new string[] { "1", ViewState["VoucherTx_ID"].ToString(), ddlPartyName.SelectedValue.ToString(), "CreditSale Voucher", ChequeTx_No.Text, ChequeTx_Date.Text, ChequeTx_Amount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), (k + 1).ToString() }, "dataset");

                    }

                }
                else if (Type == "CostCentre")
                {
                    foreach (GridViewRow row in GridCostCentreDetail.Rows)
                    {
                        Label lblCategory_ID = (Label)row.FindControl("lblCategory_ID");
                        Label lblCategoryName = (Label)row.FindControl("lblCategoryName");
                        Label lblSubCategory_ID = (Label)row.FindControl("lblSubCategory_ID");
                        Label lblSubCategoryName = (Label)row.FindControl("lblSubCategoryName");
                        Label lblAmountShow = (Label)row.FindControl("lblAmountShow");
                        Label lblAmount = (Label)row.FindControl("lblAmount");

                        objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "Voucher_Date", "Office_ID", "Category_ID", "SubCategory_ID", "Amount", "CostCentre_type", "LedgerTx_OrderBy", "UpdatedBy" },
          new string[] { "1", VoucherTx_ID, ddlPartyName.SelectedValue, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), lblCategory_ID.Text, lblSubCategory_ID.Text, "-" + lblAmount.Text, "CreditSale Voucher", "1", ViewState["Emp_ID"].ToString() }, "dataset");
                    }

                    objdb.ByProcedure("SpFinLedgerTx",
              new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
              new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type, VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","CostCentre"}, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                }
                else
                {
                    VoucherTx_Amount = "-" + VoucherTx_Amount;//06Mar2021
                    objdb.ByProcedure("SpFinLedgerTx",
               new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
               new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",ViewState["VoucherTx_ID"].ToString(),VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","None"}, "dataset");
                }
                
                CreateCrCostCentreDataSet();
                FillDetail();
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                //ClearData();
                if (rbtPrint.SelectedValue.ToString() == "Yes")
                {
                    rbtPrint.SelectedValue = "No";
                    string url = "VoucherSalepurchaseInvocie.aspx?VoucherTx_ID=" + objdb.Encrypt(VoucherTx_ID).ToString();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.open('");
                    sb.Append(url);
                    sb.Append("');");
                    sb.Append("</script>");
                    ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearData()
    {
        try
        {
            txtVoucherTx_No.Text = "";
            //txtInvoice.Text = "";
            txtSupplyTo.Text = "";
            txtLocation.Text = "";

            txtTruckNo.Text = "";
            txtTransporterName.Text = "";
            txtMRNo.Text = "";
            txtMRDate.Text = "";


            ddlPartyName.ClearSelection();
            txtCurrentBalance.Text = "";
            GridViewItem.DataSource = new string[] { };
            GridViewItem.DataBind();
            ddlLedger.ClearSelection();
            txtLedgerAmt.Text = "";
            GridViewRef.DataSource = new string[] { };
            GridViewRef.DataBind();
            GridViewLedger.DataSource = new string[] { };
            GridViewLedger.DataBind();
            AddItem("NA");
            FillVoucherNo();
            txtVoucherTx_Narration.Text = "";
            //itemdetail.Enabled = false;
            FillItem();
            GetPreviousVoucherNo();
            CreateCrCostCentreTable();

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
            ds = objdb.ByProcedure("Usp_GetVoucherSaleCreditDetail", new string[] { "VoucherTx_ID" }, new string[] {ViewState["VoucherTx_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    //var rx = new System.Text.RegularExpressions.Regex("PV");
                    //string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //var array = rx.Split(str);
                    //lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //txtVoucherTx_No.Text = array[1];
                    //lblVoucherTx_No.Text = array[0] + "PV";
                    //txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    string Fstring = "";
                    string Lstring = "";
                    string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();

                    //txtSupplyTo.Text = ds.Tables[0].Rows[0]["SupplyTo"].ToString();
                    //txtLocation.Text = ds.Tables[0].Rows[0]["Location"].ToString();

                    //txtTruckNo.Text = ds.Tables[0].Rows[0]["TruckNo"].ToString();
                    //txtTransporterName.Text = ds.Tables[0].Rows[0]["TransporterName"].ToString();
                    //txtMRNo.Text = ds.Tables[0].Rows[0]["MRNo"].ToString();
                    //txtMRDate.Text = ds.Tables[0].Rows[0]["MRDate"].ToString();

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
                    ViewState["VoucherTx_Date"] = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    ViewState["FY"] = ds.Tables[0].Rows[0]["VoucherTx_FY"].ToString();
                    lblGrandTotal.Text = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                    if (Request.QueryString["VoucherTx_ID"] != null && Request.QueryString["Action"] != null)
                    {
                        ViewState["EditVoucherTx_Amount"] = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                    }

                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
                    //txtInvoice.Text = ds.Tables[0].Rows[0]["VoucherTx_Ref"].ToString();
                    ViewState["Amount"] = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                    //if (ds.Tables[0].Rows[0]["VoucherTx_SupplierinvoiceDate"].ToString() != "")
                    //{
                    //    txtSupplierInvoiceDate.Text = ds.Tables[0].Rows[0]["VoucherTx_SupplierinvoiceDate"].ToString();
                    //}

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    GridViewItem.DataSource = ds.Tables[1];
                    GridViewItem.DataBind();
                    
                    
                    if (GridViewItem.Rows.Count > 0)
                    {
                        decimal TAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                        decimal CGSTAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                        decimal SGSTAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                        decimal IGSTAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("IGSTAmt"));
                        GridViewItem.FooterRow.Cells[5].Text = "<b>Total : </b>";
                        GridViewItem.FooterRow.Cells[6].Text = "<b>" + TAmount.ToString() + "</b>";
                        GridViewItem.FooterRow.Cells[7].Text = "<b>" + CGSTAmount.ToString() + "</b>";
                        GridViewItem.FooterRow.Cells[8].Text = "<b>" + SGSTAmount.ToString() + "</b>";
                        GridViewItem.FooterRow.Cells[9].Text = "<b>" + IGSTAmount.ToString() + "</b>";
 
                        GridViewItem.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                        GridViewItem.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                        GridViewItem.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                        GridViewItem.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;

                    }
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    string OfficeID = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                    if (OfficeID.ToString() == Session["Office_ID"].ToString())
                    {
                        ddlPartyName.ClearSelection();
                        ddlPartyName.Items.FindByValue(ds.Tables[2].Rows[0]["Ledger_ID"].ToString()).Selected = true;
                        ViewState["Ledger_ID"] = ds.Tables[2].Rows[0]["Ledger_ID"].ToString();
                        ddlPartyName.Visible = true;
                        txtPartyName.Visible = false;
                        FillCurrentBalance();

                        
                    }
                    else
                    {
                        ddlPartyName.Visible = false;
                        txtPartyName.Visible = true;
                        txtPartyName.Text = ds.Tables[2].Rows[0]["Ledger_Name"].ToString();
                        ViewState["Ledger_ID"] = ds.Tables[2].Rows[0]["Ledger_ID"].ToString();
                        ViewState["Office_ID"] = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                        DataSet ds1 = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "1", ViewState["Ledger_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                        if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                        {
                            txtCurrentBalance.Text = ds1.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
                        }
                    }
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    // DataTable dt_GridViewLedger = (DataTable)ViewState["LedgerAmount"];
                    DataTable dt_GridViewLedger = new DataTable();
                    dt_GridViewLedger = ds.Tables[3];

                    DataTable tblFiltered = dt_GridViewLedger.AsEnumerable()
                              .Where(r => r.Field<string>("LedgerName") == "Round off")
                              .CopyToDataTable();
                    int rn = tblFiltered.Rows[0].Field<int>("RowNo");
                    if (rn == 6)
                    {
                        dt_GridViewLedger.DefaultView.Sort = "LedgerTx_OrderBy ASC";
                    }
                    else if (rn > 6)
                    {
                        dt_GridViewLedger.DefaultView.Sort = "LedgerTx_OrderBy ASC";
                    }
                    else
                    {
                        dt_GridViewLedger.DefaultView.Sort = "RowNo DESC";
                        //dt_GridViewLedger.DefaultView.Sort = "RowNo";
                    }
                    GridViewLedger.DataSource = dt_GridViewLedger;
                    GridViewLedger.DataBind();
                    //GridViewLedger.DataSource = ds.Tables[3];
                    //GridViewLedger.DataBind();
                    ViewState["LedgerAmount"] = dt_GridViewLedger;
                }
                if (ds.Tables[5].Rows.Count > 0)
                {
                    BillByBillDetail.Visible = true;
                    pnlChequeDetail.Visible = false;
                    pnlCostCentre.Visible = false;
                    GridViewBillByBillDetail.DataSource = ds.Tables[5];
                    GridViewBillByBillDetail.DataBind();

                }
                if (ds.Tables[6].Rows.Count > 0)
                {
                    BillByBillDetail.Visible = false;
                    pnlChequeDetail.Visible = true;
                    pnlCostCentre.Visible = false;
                    GridViewChequeDetail.DataSource = ds.Tables[6];
                    GridViewChequeDetail.DataBind();

                }
                btnAccept.Text = "Update";
                btnAccept.Enabled = true;
                lnkPreviousVoucher.Visible = false;
            }

            ds = objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID" },
              new string[] { "6", ViewState["VoucherTx_ID"].ToString() }, "dataset");
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{

            //    GridCostCentreViewDetail.DataSource = ds;
            //    GridCostCentreViewDetail.DataBind();
            //    BillByBillDetail.Visible = false;
            //    pnlChequeDetail.Visible = false;
            //    pnlCostCentre.Visible = true;
            //}
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridCostCentreViewDetail.DataSource = ds;
                    GridCostCentreViewDetail.DataBind();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    // DataTable dt_CrCostCentreTable = (DataTable)ViewState["CrCostCentreTable"];
                    //dt_CrCostCentreTable = ds.Tables[1];
                    //ViewState["CrCostCentreTable"] = dt_CrCostCentreTable;

                    DataSet dsCrConstCentre = (DataSet)ViewState["dsCrConstCentre"];
                    int rowcount = ds.Tables[1].Rows.Count;
                    int pretno = 0;
                    for (int i = 0; i < rowcount; i++)
                    {
                        string Ledger_ID = ds.Tables[1].Rows[i]["Ledger_ID"].ToString();
                        string Category_ID = ds.Tables[1].Rows[i]["Category_ID"].ToString();
                        string CategoryName = ds.Tables[1].Rows[i]["CategoryName"].ToString();
                        string SubCategory_ID = ds.Tables[1].Rows[i]["SubCategory_ID"].ToString();
                        string SubCategoryName = ds.Tables[1].Rows[i]["SubCategoryName"].ToString();
                        string AmountShow = ds.Tables[1].Rows[i]["AmountShow"].ToString();
                        string Amount = ds.Tables[1].Rows[i]["Amount"].ToString();
                        string TNO = ds.Tables[1].Rows[i]["LedgerTx_OrderBy"].ToString();



                        DataTable dt_CrCostCentreTable = new DataTable(TNO.ToString());
                        DataColumn RowNo = dt_CrCostCentreTable.Columns.Add("RowNo", typeof(string));
                        dt_CrCostCentreTable.Columns.Add(new DataColumn("Ledger_ID", typeof(decimal)));
                        dt_CrCostCentreTable.Columns.Add(new DataColumn("Category_ID", typeof(string)));
                        dt_CrCostCentreTable.Columns.Add(new DataColumn("CategoryName", typeof(string)));
                        dt_CrCostCentreTable.Columns.Add(new DataColumn("SubCategory_ID", typeof(string)));
                        dt_CrCostCentreTable.Columns.Add(new DataColumn("SubCategoryName", typeof(string)));
                        dt_CrCostCentreTable.Columns.Add(new DataColumn("AmountShow", typeof(decimal)));
                        dt_CrCostCentreTable.Columns.Add(new DataColumn("Amount", typeof(decimal)));

                        dt_CrCostCentreTable.Rows.Add(null, Ledger_ID, Category_ID, CategoryName, SubCategory_ID, SubCategoryName, AmountShow, Amount);
                        dsCrConstCentre.Merge(dt_CrCostCentreTable);
                        ViewState["dsCrConstCentre"] = dsCrConstCentre;
                       
                    }
                }
                BillByBillDetail.Visible = false;
                pnlChequeDetail.Visible = false;
                pnlCostCentre.Visible = true;
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
            //panel1.Enabled = false;
            rbtPrint.Enabled = false;
            chkitem.Enabled = false;
            ddlPartyName.Enabled = false;


            GridViewItem.Columns[11].Visible = false;
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
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowRefDetailModal();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtVoucherTx_Date_TextChanged(object sender, EventArgs e)
    {
        //string ValidStatus = ValidDate();
        string ValidStatus = "Yes";
        if (ValidStatus == "No")
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('You are not allowed to choose this date, please contact to head office.');", true);
            ds = null;
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
                FillVoucherNo();
            }
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
                    //FillVoucherNo();
                    
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Date selection should be according to Financial Year(" + ViewState["FY"].ToString() + ")');", true);
                    txtVoucherTx_Date.Text = ViewState["VoucherTx_Date"].ToString();
                }
            }
        }

    }
    protected void GridViewBillByBillDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string BillByBillTx_ID = GridViewBillByBillDetail.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "BillByBillTx_ID" }, new string[] { "11", BillByBillTx_ID }, "dataset");
            FillDetail();
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
    protected void lnkPreviousVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "VoucherTx_Type" }, new string[] { "33", ViewState["Office_ID"].ToString(), "CreditSale Voucher" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string VoucherID = ds.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
                ViewState["VoucherTx_ID"] = VoucherID.ToString();
                // FillPreviousDetail();
                FillDetail();
                ViewState["VoucherTx_ID"] = "0";
                btnAccept.Text = "Accept";
            }
            lnkPreviousVoucher.Visible = true;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //Fill Previous Voucher Detail
    protected void FillPreviousDetail()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "16", ViewState["VoucherTx_ID"].ToString() }, "dataset");
            // ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "43", ViewState["VoucherTx_ID"].ToString() }, "dataset");
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
                    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    ViewState["VoucherTx_Date"] = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    ViewState["FY"] = ds.Tables[0].Rows[0]["VoucherTx_FY"].ToString();
                    lblGrandTotal.Text = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
                    //txtInvoice.Text = ds.Tables[0].Rows[0]["VoucherTx_Ref"].ToString();
                    ViewState["Amount"] = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                    //if (ds.Tables[0].Rows[0]["VoucherTx_SupplierinvoiceDate"].ToString() != "")
                    //{
                    //    txtSupplierInvoiceDate.Text = ds.Tables[0].Rows[0]["VoucherTx_SupplierinvoiceDate"].ToString();
                    //}

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    GridViewItem.DataSource = ds.Tables[1];
                    GridViewItem.DataBind();


                    if (GridViewItem.Rows.Count > 0)
                    {
                        decimal TAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                        decimal CGSTAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                        decimal SGSTAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                        decimal IGSTAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("IGSTAmt"));
                        GridViewItem.FooterRow.Cells[5].Text = "<b>Total : </b>";
                        GridViewItem.FooterRow.Cells[6].Text = "<b>" + TAmount.ToString() + "</b>";
                        GridViewItem.FooterRow.Cells[7].Text = "<b>" + CGSTAmount.ToString() + "</b>";
                        GridViewItem.FooterRow.Cells[8].Text = "<b>" + SGSTAmount.ToString() + "</b>";
                        GridViewItem.FooterRow.Cells[9].Text = "<b>" + IGSTAmount.ToString() + "</b>";
                        GridViewItem.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                        GridViewItem.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                        GridViewItem.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                        GridViewItem.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    }
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    string OfficeID = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                    if (OfficeID.ToString() == Session["Office_ID"].ToString())
                    {
                        ddlPartyName.ClearSelection();
                        ddlPartyName.Items.FindByValue(ds.Tables[2].Rows[0]["Ledger_ID"].ToString()).Selected = true;
                        ViewState["Ledger_ID"] = ds.Tables[2].Rows[0]["Ledger_ID"].ToString();
                        ddlPartyName.Visible = true;
                        txtPartyName.Visible = false;
                        FillCurrentBalance();

                    }
                    else
                    {
                        ddlPartyName.Visible = false;
                        txtPartyName.Visible = true;
                        txtPartyName.Text = ds.Tables[2].Rows[0]["Ledger_Name"].ToString();
                        ViewState["Ledger_ID"] = ds.Tables[2].Rows[0]["Ledger_ID"].ToString();
                        ViewState["Office_ID"] = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                        DataSet ds1 = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "1", ViewState["Ledger_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                        if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                        {
                            txtCurrentBalance.Text = ds1.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
                        }
                    }


                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    DataTable dt_GridViewLedger = (DataTable)ViewState["LedgerAmount"];
                    dt_GridViewLedger = ds.Tables[3];
                    GridViewLedger.DataSource = ds.Tables[3];
                    GridViewLedger.DataBind();



                }

                btnAccept.Text = "Accept";
                btnAccept.Enabled = true;

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
    protected void chkitem_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkitem.Checked == true)
            {
                itemdetail.Enabled = true;
            }
            else
            {
                itemdetail.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnRefreshLedgerList_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            FillDropDown();
            FillItem();


           
                
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblCostCentreModal.Text = "";
            ddlSubCategory.Items.Clear();
            ds = objdb.ByProcedure("SpFinSubCategoryMaster",
                 new string[] { "flag", "CategoryId" },
                 new string[] { "8", ddlCategory.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlSubCategory.DataTextField = "SubCategoryName";
                ddlSubCategory.DataValueField = "SubCategoryId";
                ddlSubCategory.DataSource = ds;
                ddlSubCategory.DataBind();
            }
            ddlSubCategory.Items.Insert(0, new ListItem("Select", "0"));
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnCostCentreAdd_Click(object sender, EventArgs e)
    {
        string msg = "";
        lblCostCentreModal.Text = "";
        if (ddlCategory.SelectedIndex == 0)
        {
            msg += "Select Category \\n";
        }
        if (ddlSubCategory.SelectedIndex == 0)
        {
            msg += "Select Sub Category \\n";
        }
        if (txtCostCentreAmount.Text == "")
        {
            msg += "Enter Amount \\n";
        }
        if (msg == "")
        {
            if (ViewState["CrCostCentre"].ToString() == "Yes")
            {
                AddCostCentre();
            }
            else
            {
                DataTable dt_CostCentreTable = (DataTable)ViewState["CostCentreTable"];
                int status = 0;
                decimal CostCentreAmount = 0;
                foreach (GridViewRow row in GridCostCentreDetail.Rows)
                {
                    Label lblCategory_ID = (Label)row.FindControl("lblCategory_ID");
                    Label lblSubCategory_ID = (Label)row.FindControl("lblSubCategory_ID");
                    if (lblCategory_ID.Text == ddlCategory.SelectedValue.ToString() && lblSubCategory_ID.Text == ddlSubCategory.SelectedValue.ToString())
                    {
                        status = 1;
                    }
                }
                if (status == 0)
                {
                    if (Convert.ToDecimal(ViewState["Amount"].ToString()) > Convert.ToDecimal(txtCostCentreAmount.Text) || Convert.ToDecimal(ViewState["Amount"].ToString()) == Convert.ToDecimal(txtCostCentreAmount.Text))
                    {
                        ViewState["Amount"] = Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(txtCostCentreAmount.Text);
                        dt_CostCentreTable.Rows.Add(null, ddlPartyName.SelectedValue.ToString(), ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue.ToString(), ddlSubCategory.SelectedItem.Text, Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"), Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"));


                        ddlCategory.ClearSelection();
                        ddlSubCategory.ClearSelection();
                        txtCostCentreAmount.Text = ViewState["Amount"].ToString();
                        ViewState["CostCentreTable"] = dt_CostCentreTable;

                        GridCostCentreDetail.DataSource = dt_CostCentreTable;
                        GridCostCentreDetail.DataBind();


                        CostCentreAmount = dt_CostCentreTable.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                        if ((Convert.ToDecimal(lblGrandTotal.Text) - Math.Abs(CostCentreAmount)) != Convert.ToDecimal("0"))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
                        }
                        else
                        {
                            Save("CostCentre");
                        }
                    }
                    else
                    {
                        lblCostCentreModal.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Amount Can't Be Greater Than Pending Amount.");
                        txtCostCentreAmount.Text = ViewState["Amount"].ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
                    }
                }
                else
                {
                    lblCostCentreModal.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Catgeory & Sub-Category Is Already Exists.");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
                }
            }

        }
        else
        {
            lblCostCentreModal.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", msg);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
        }
    }
    protected void GridCostCentreDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblCostCentreModal.Text = "";
        string ID = e.CommandArgument.ToString();
        if (e.CommandName == "RecordDelete")
        {
            DataTable dt_CostCentreTable = (DataTable)ViewState["CostCentreTable"];
            int Count = dt_CostCentreTable.Rows.Count;
            for (int i = 0; i < Count; i++)
            {
                DataRow dr = dt_CostCentreTable.Rows[i];
                if (dr["RowNo"].ToString() == ID.ToString())
                {
                    decimal Amount = Math.Abs(Convert.ToDecimal(dr["Amount"].ToString()));
                    ViewState["Amount"] = Convert.ToDecimal(ViewState["Amount"]) + Amount;
                    txtCostCentreAmount.Text = ViewState["Amount"].ToString();
                    dr.Delete();
                    break;
                }
            }
            dt_CostCentreTable.AcceptChanges();
            ViewState["CostCentreTable"] = dt_CostCentreTable;

            GridCostCentreDetail.DataSource = dt_CostCentreTable;
            GridCostCentreDetail.DataBind();

            decimal CostCentreAmount = 0;
            CostCentreAmount = dt_CostCentreTable.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
            if ((Convert.ToDecimal(lblGrandTotal.Text) - Math.Abs(CostCentreAmount)) != Convert.ToDecimal("0"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
            }
            else
            {
                Save("CostCentre");
            }
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
        }
    }
    protected void AddCostCentre()
    {

        DataTable dt_CostCentreTable = (DataTable)ViewState["CrCostCentreTable"];
        int status = 0;
        decimal CostCentreAmount = 0;
        foreach (GridViewRow row in GridCostCentreDetail.Rows)
        {
            Label lblCategory_ID = (Label)row.FindControl("lblCategory_ID");
            Label lblSubCategory_ID = (Label)row.FindControl("lblSubCategory_ID");
            if (lblCategory_ID.Text == ddlCategory.SelectedValue.ToString() && lblSubCategory_ID.Text == ddlSubCategory.SelectedValue.ToString())
            {
                status = 1;
            }
        }
        if (status == 0)
        {
            if (Convert.ToDecimal(ViewState["Amount"].ToString()) > Convert.ToDecimal(txtCostCentreAmount.Text) || Convert.ToDecimal(ViewState["Amount"].ToString()) == Convert.ToDecimal(txtCostCentreAmount.Text))
            {
                ViewState["Amount"] = Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(txtCostCentreAmount.Text);
               // dt_CostCentreTable.Rows.Add(null, ddlLedger.SelectedValue.ToString(), ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue.ToString(), ddlSubCategory.SelectedItem.Text, Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"), Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"));

               // ViewState["CostCentreTable"] = dt_CostCentreTable;
                DataTable dt_CrCostCentreTable = (DataTable)ViewState["CrCostCentreTable"];
                dt_CrCostCentreTable.Rows.Add(null, ddlLedger.SelectedValue.ToString(), ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue.ToString(), ddlSubCategory.SelectedItem.Text, Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"), Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"));
                //dt_CrCostCentreTable.Merge(dt_CostCentreTable);
                ViewState["CrCostCentreTable"] = dt_CrCostCentreTable;
                GridCostCentreDetail.DataSource = dt_CrCostCentreTable;
                GridCostCentreDetail.DataBind();

                ddlCategory.ClearSelection();
                ddlSubCategory.ClearSelection();
                txtCostCentreAmount.Text = ViewState["Amount"].ToString();
                CostCentreAmount = dt_CostCentreTable.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                if ((Math.Abs(Convert.ToDecimal(txtLedgerAmt.Text)) - Math.Abs(CostCentreAmount)) != Convert.ToDecimal("0"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
                }
                else
                {
                   // CreateCostCentreTable();
                     FillLedgerAmount("0");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                    ViewState["GrandTotal"] = lblGrandTotal.Text;
                    //btnAcceptEnable();
                    ddlLedger.ClearSelection();
                    txtLedgerAmt.Text = "";
                }
            }
            else
            {
                lblCostCentreModal.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Amount Can't Be Greater Than Pending Amount.");
                txtCostCentreAmount.Text = ViewState["Amount"].ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
            }
        }
        else
        {
            lblCostCentreModal.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Catgeory & Sub-Category Is Already Exists.");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
        }
    }
    protected void GridViewLedger_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewRecord")
            {
                DataSet dsCrConstCentre = (DataSet)ViewState["dsCrConstCentre"];

                gvCostCentreDetail.DataSource = dsCrConstCentre.Tables[e.CommandArgument.ToString()];
                gvCostCentreDetail.DataBind();
                //string LedgerID = e.CommandArgument.ToString();
                //DataTable dt_CrCostCentreTable = (DataTable)ViewState["CrCostCentreTable"];
                //DataView dv = new DataView();

                //dv = dt_CrCostCentreTable.DefaultView;
                //dv.RowFilter = "Ledger_ID = '" + LedgerID + "'";
                //DataTable dt = dv.ToTable();
                //gvCostCentreDetail.DataSource = dt;
                //gvCostCentreDetail.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCrCostCentreModal();", true);



            }
        }
        catch (Exception ex)
        {

            throw;
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

            DataSet dsValidDate = objdb.ByProcedure("SpFinEditRight", new string[] { "flag", "Office_ID", "VoucherDate", "FinancialYear", "VoucherTx_Type" }, new string[] { "3", ViewState["Office_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), FinancialYear, "CreditSale Voucher" }, "dataset");
            if (dsValidDate.Tables.Count != 0 && dsValidDate.Tables[0].Rows.Count != 0)
            {
                validDays = dsValidDate.Tables[0].Rows[0]["ValidStatus"].ToString();
            }
        }
        return validDays;
    }

    protected void CreateCrCostCentreDataSet()
    {
        DataSet dsCrConstCentre = new DataSet();
        ViewState["dsCrConstCentre"] = dsCrConstCentre;

    }
}