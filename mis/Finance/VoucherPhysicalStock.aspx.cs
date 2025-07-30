using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_VoucherPhysicalStock : System.Web.UI.Page
{
    DataSet ds, ds2;
    //static DataSet DS_GridViewParticulars = new DataSet();
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

                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["RowNo"] = "0";
                    ViewState["CGST"] = "1";
                    ViewState["SGST"] = "2";
                    ViewState["RoundOff"] = "3";
                    ViewState["IGST"] = "737";
                    ViewState["VoucherTx_ID"] = "0";

                    txtVoucherTx_Date.Attributes.Add("readonly", "readonly");

                    //txtVoucherTx_Date.Enabled = false;

                    CreateParticularDataset();

                    FillItem();
                    FillWareHouse();
                    AddItem("NA");
                    txtUnitName.Attributes.Add("readonly", "readonly");
                    //txtAmount.Attributes.Add("readonly", "readonly");
                    //txtVoucherTx_No.Attributes.Add("readonly", "readonly");

                    ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
                    if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
                        //ViewState["Voucher_FY"] = ds.Tables[0].Rows[0]["Voucher_FY"].ToString();
                        FillVoucherNo();
                    }
                    if (Request.QueryString["VoucherTx_ID"] != null && Request.QueryString["Action"] != null)
                    {
                        string Action = objdb.Decrypt(Request.QueryString["Action"].ToString());
                        ViewState["Action"] = Action;
                        ViewState["VoucherTx_ID"] = objdb.Decrypt(Request.QueryString["VoucherTx_ID"].ToString());
                        if (Action == "2")
                        {
                            //FillDetail();
                        }
                        else if (Action == "1")
                        {
                            if (Request.QueryString["Office_ID"] != null)
                            {
                                ViewState["Office_ID"] = objdb.Decrypt(Request.QueryString["Office_ID"].ToString());
                                //FillDetail();
                                ViewVoucher();
                            }
                            else
                            {
                                //FillDetail();
                                ViewVoucher();
                            }
                        }
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
    protected void FillItem()
    {
        try
        {
            ds = objdb.ByProcedure("Proc_tblPuSalesOrder", new string[] { "flag", "Office_ID" }, new string[] { "9", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlItem.Items.Clear();
                ddlItem.DataSource = ds.Tables[0];
                ddlItem.DataTextField = "AvailableStock1";
                ddlItem.DataValueField = "Item_id";
                ddlItem.DataBind();
                ddlItem.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlItem.Items.Clear();
                ddlItem.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

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
    protected void CreateParticularDataset()
    {
        DataSet DS_GridViewParticulars = new DataSet();
        ViewState["DS_GridViewParticulars"] = DS_GridViewParticulars;
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
                    //msg += "Rate Should not be zero. \\n";
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
                int rowIndex = 0;
                int gridRows = GridViewItem.Rows.Count;
                if (gridRows > 0)
                {
                    for (rowIndex = 0; rowIndex < gridRows; rowIndex++)
                    {
                        Label lblItem = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
                        Label lblWarehouse_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_id");
                        if (lblItem.Text == ddlItem.SelectedValue && lblWarehouse_id.Text == ddlWarehouse.SelectedValue)
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
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Item already selected');", true);
                }
                else
                {
                    GetItemPurchaseLedgerId();
                    AddItem(ViewState["RowNo"].ToString());
                    string TNo = ddlItem.SelectedValue.ToString() + ddlWarehouse.SelectedItem.Text;
                    DataTable dt_GridViewParticulars = new DataTable(TNo);
                    dt_GridViewParticulars.Columns.Add(new DataColumn("Item_ID", typeof(string)));
                    dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularID", typeof(string)));
                    dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularName", typeof(string)));
                    dt_GridViewParticulars.Columns.Add(new DataColumn("ParticularAmt", typeof(decimal)));

                    dt_GridViewParticulars.Rows.Add(ddlItem.SelectedValue, ViewState["ParticularID"].ToString(), ViewState["ParticularName"].ToString(), txtAmount.Text);
                    ClearItem();



                    DS_GridViewParticulars.Merge(dt_GridViewParticulars);
                    ViewState["DS_GridViewParticulars"] = DS_GridViewParticulars;


                    ViewState["RowNo"] = "0";

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
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
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
    protected void ClearItem()
    {
        try
        {
            lblMsg.Text = "";
            ddlItem.ClearSelection();
            txtQuantity.Text = "";
            txtRate.Text = "";
            txtAmount.Text = "";
            txtUnitName.Text = "";
            lblUnitName.Text = "";
            ddlWarehouse.SelectedIndex = 1;
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
            dt_GridViewItem.Columns.Add(new DataColumn("ID", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("ItemID", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Unit_id", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Warehouse_id", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("WarehouseName", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Item", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Quantity", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Rate", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Unit", typeof(string)));
            int rowIndex = 0;
            int gridRows = GridViewItem.Rows.Count;
            int RID = 0;
            for (rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {
                Label lblID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblID");
                Label lblItemID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
                Label lblUnit_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblUnit_id");
                Label lblWarehouse_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_id");
                Label lblWarehouse_Name = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_Name");
                Label lblItem = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItem");
                Label lblQuantity = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblQuantity");
                Label lblRate = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblRate");
                Label lblAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblAmount");
                Label lblUnit = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblUnit");
                if ((lblItemID.Text + lblWarehouse_Name.Text) != ID && ViewState["RowNo"].ToString() == "0")
                {
                    RID++;
                    dt_GridViewItem.Rows.Add(RID.ToString(), lblItemID.Text, lblUnit_id.Text, lblWarehouse_id.Text, lblWarehouse_Name.Text, lblItem.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, lblUnit.Text);
                }



            }
            if (ID == "0" && ViewState["RowNo"].ToString() == "0")
            {
                ds = objdb.ByProcedure("SpFinVoucherSaleCredit", new string[] { "flag", "Item_id" },
                      new string[] { "2", ddlItem.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    gridRows = gridRows + 1;
                    string Item = ddlItem.SelectedItem.ToString();
                    string UnitID = ds.Tables[0].Rows[0]["Unit_id"].ToString();
                    string Unit = ds.Tables[0].Rows[0]["UQCCode"].ToString();
                    decimal Amount = decimal.Parse(txtAmount.Text);

                    RID++;
                    dt_GridViewItem.Rows.Add(RID.ToString(), ddlItem.SelectedValue.ToString(), UnitID, ddlWarehouse.SelectedValue.ToString(), ddlWarehouse.SelectedItem.Text, Item.ToString(), txtQuantity.Text, txtRate.Text, Amount.ToString(), txtUnitName.Text);
                }

            }
            GridViewItem.DataSource = dt_GridViewItem;
            GridViewItem.DataBind();

            if (ID != "0")
            {
                DataSet ds1 = (DataSet)ViewState["DS_GridViewParticulars"];
                DataSet DS_GridViewParticulars = (DataSet)ViewState["DS_GridViewParticulars"];
                int TNo = GridViewItem.Rows.Count;
                DS_GridViewParticulars = new DataSet();
                for (rowIndex = 0; rowIndex < TNo; rowIndex++)
                {
                    Label lblID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
                    Label lblWarehouse_Name = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_Name");
                    string TabName = (lblID.Text + lblWarehouse_Name.Text);
                    DataTable dt = new DataTable(lblID.Text + lblWarehouse_Name.Text);
                    dt = ds1.Tables[TabName];
                    DS_GridViewParticulars.Merge(dt);

                }
                ViewState["DS_GridViewParticulars"] = DS_GridViewParticulars;
            }
            ViewState["RowNo"] = "0";
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
            Label lbl = (Label)GridViewItem.Rows[e.RowIndex].Cells[0].FindControl("lblWarehouse_Name");
            ID = ID + lbl.Text;
            AddItem(ID);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
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
            if (txtAmount.Text == "")
            {
                msg += "Enter Amount. \\n";
            }
            if (msg.Trim() == "")
            {
                AddItem("0");
                ClearItem();

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


    protected void btnAccept_Click(object sender, EventArgs e)
    {
        try
        {


            string VoucherTx_No = txtVoucherTx_No.Text;
            string Reference_No = txtInvoice.Text;
            string VoucherTx_Narration = txtVoucherTx_Narration.Text;


            ds = objdb.ByProcedure("SpFinVoucherStockJournal", new string[] { "flag", "TransactionID", "TransactionFrom", "Office_Id" },
                     new string[] { "1", VoucherTx_No.ToString(), "Physical Stock", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count == 0)
            {
                int gridRows = GridViewItem.Rows.Count;
                for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                {
                    Label lblItemID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
                    Label lblUnit_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblUnit_id");
                    Label lblWarehouse_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblWarehouse_id");
                    Label lblItem = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItem");
                    Label lblQuantity = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblQuantity");
                    Label lblRate = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblRate");
                    Label lblAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblAmount");

                    /************/


                    ds2 = objdb.ByProcedure("SpFinVoucherStockJournal", new string[] { "flag", "item_id", "ToDate", "Office_ID_Mlt" },
                     new string[] { "6", lblItemID.Text, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString() }, "dataset");
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        decimal ClosingQuantity = 0;
                        decimal AvailableQuantity = 0;
                        decimal AdjustedQuantity = 0;                        
                        ClosingQuantity = decimal.Parse(ds2.Tables[0].Rows[0]["ClosingQuantity"].ToString());
                        AvailableQuantity = decimal.Parse(lblQuantity.Text.ToString());
                        AdjustedQuantity = AvailableQuantity - (ClosingQuantity);


                        decimal ClosingQuantity_rate = 0;
                        decimal AvailableQuantity_rate = 0;
                        decimal AdjustedQuantity_rate = 0;
                        ClosingQuantity_rate = decimal.Parse(ds2.Tables[0].Rows[0]["ClosingRate"].ToString());
                        AvailableQuantity_rate = decimal.Parse(lblQuantity.Text.ToString());
                        AdjustedQuantity_rate = AvailableQuantity_rate - (ClosingQuantity_rate);


                        if (ClosingQuantity == AvailableQuantity)
                        {

                        }
                        else if (AdjustedQuantity > 0)
                        {
                            AdjustedQuantity = Math.Abs(AdjustedQuantity);
                            objdb.ByProcedure("SpFinVoucherStockJournal",
                                 new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "Remark", "TransactionFrom", "InvoiceNo", "Office_Id", "Warehouse_id", "CreatedBy", "TranDt" }
                                , new string[] { "0", lblItemID.Text, AdjustedQuantity.ToString(), "0", lblRate.Text, VoucherTx_No, "(+)Adjusted By Physical Stock Voucher", "Physical Stock", Reference_No, ViewState["Office_ID"].ToString(), lblWarehouse_id.Text, ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

                        }
                        else if (AdjustedQuantity < 0)
                        {
                            AdjustedQuantity = Math.Abs(AdjustedQuantity);
                            objdb.ByProcedure("SpFinVoucherStockJournal",
                                  new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "Remark", "TransactionFrom", "InvoiceNo", "Office_Id", "Warehouse_id", "CreatedBy", "TranDt" }
                                 , new string[] { "0", lblItemID.Text, "0", AdjustedQuantity.ToString(), lblRate.Text, VoucherTx_No, "(-)Adjusted By Physical Stock Voucher", "Physical Stock", Reference_No, ViewState["Office_ID"].ToString(), lblWarehouse_id.Text, ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                        }

                    }


                    /************/
                    if (lblQuantity.Text != "0")
                    {
                        objdb.ByProcedure("SpFinVoucherStockJournal",
                                    new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "Remark", "TransactionFrom", "InvoiceNo", "Office_Id", "Warehouse_id", "CreatedBy", "TranDt" }
                                   , new string[] { "0", lblItemID.Text, lblQuantity.Text, "0", lblRate.Text, VoucherTx_No, VoucherTx_Narration.ToString(), "Physical Stock", Reference_No, ViewState["Office_ID"].ToString(), lblWarehouse_id.Text, ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                        
                    }
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");

                }
                ClearData();
            }
            else
            {
                string msg = "Voucher No. already Exist";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
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
            txtInvoice.Text = "";
            GridViewItem.DataSource = new string[] { };
            GridViewItem.DataBind();
            CreateParticularDataset();
            AddItem("NA");
            FillVoucherNo();
            txtVoucherTx_Narration.Text = "";
            FillItem();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

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
            string VoucherTx_Names_ForSno = "Purchase Voucher";
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
            //lblVoucherTx_No.Text = Office_Code + FinancialYear.ToString().Substring(2) + "PV";
            lblVoucherTx_No.Text = "";
            //txtVoucherTx_No.Text = VoucherTx_SNo.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetItemPurchaseLedgerId()
    {
        try
        {
            string Item_id = ddlItem.SelectedValue.ToString();
            ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Item_id" }, new string[] { "20", Item_id }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["ParticularID"] = ds.Tables[0].Rows[0]["PurchaseLedger_id"].ToString();
                ViewState["ParticularName"] = ds.Tables[0].Rows[0]["Ledger_Name"].ToString();
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
        FillVoucherNo();
    }
    protected void ViewVoucher()
    {
        try
        {

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}