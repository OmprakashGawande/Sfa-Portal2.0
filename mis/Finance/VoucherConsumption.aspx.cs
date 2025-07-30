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

public partial class mis_Finance_VoucherConsumption : System.Web.UI.Page
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
                    AddItem("NA");
                    ViewState["RowNo"] = "0";
                    lblGrandTotal.Attributes.Add("readonly", "readonly");
                    GridViewItem.DataSource = new string[] { };
                    GridViewItem.DataBind();
                    //ViewState["TableId"] = "-1";
                    ViewState["LedgerTotal"] = "0";
                    CreateLedgerTable();
                    txtVoucherTx_Date.Attributes.Add("readonly", "readonly");

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
            string VoucherTx_Names_ForSno = "Consumption Voucher";

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
            lblVoucherTx_No.Text = Office_Code + FinancialYear.ToString().Substring(2) + "CV";
            //txtVoucherTx_No.Text = VoucherTx_SNo.ToString();
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

            if (txtQuantity.Text.Trim() == "")
            {
                msg += "Enter Quantity. \\n";
            }
            if (txtRate.Text.Trim() == "")
            {
                msg += "Enter Rate. \\n";
            }
            //if (txtRate.Text.Trim() != "")
            //{
            //    if (decimal.Parse(txtRate.Text) == 0)
            //    {
            //        msg += "Rate Should not be zero. \\n";
            //    }
            //}
            if (txtTotalAmount.Text.Trim() == "")
            {
                msg += "Enter Amount. \\n";
            }
            if (msg == "")
            {



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

    protected void AddItem(string ID)
    {
        try
        {

            DataTable dt_GridViewItem = new DataTable();
            DataColumn RowNo = dt_GridViewItem.Columns.Add("ID", typeof(int));
            //dt_GridViewItem.Columns.Add(new DataColumn("ID", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("ItemID", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Unit_id", typeof(string)));
            //dt_GridViewItem.Columns.Add(new DataColumn("HSN_Code", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Item", typeof(string)));
            dt_GridViewItem.Columns.Add(new DataColumn("Quantity", typeof(float)));
            dt_GridViewItem.Columns.Add(new DataColumn("Rate", typeof(decimal)));
            dt_GridViewItem.Columns.Add(new DataColumn("Amount", typeof(decimal)));
            //dt_GridViewItem.Columns.Add(new DataColumn("CGSTAmt", typeof(decimal)));
            //dt_GridViewItem.Columns.Add(new DataColumn("SGSTAmt", typeof(decimal)));
            //dt_GridViewItem.Columns.Add(new DataColumn("IGSTAmt", typeof(decimal)));
            //dt_GridViewItem.Columns.Add(new DataColumn("CGST_Per", typeof(string)));
            //dt_GridViewItem.Columns.Add(new DataColumn("SGST_Per", typeof(string)));
            //dt_GridViewItem.Columns.Add(new DataColumn("IGST_Per", typeof(string)));

            dt_GridViewItem.Columns.Add(new DataColumn("Unit", typeof(string)));
            //dt_GridViewItem.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
            //dt_GridViewItem.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
            //dt_GridViewItem.Columns.Add(new DataColumn("Taxbility", typeof(string)));
            RowNo.AutoIncrement = true;
            RowNo.AutoIncrementSeed = 1;
            RowNo.AutoIncrementStep = 1;
            int rowIndex = 0;
            int gridRows = GridViewItem.Rows.Count;
            int RID = 0;
            for (rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {

                Label lblItemRowNo = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemRowNo");
                Label lblItemID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");
                Label lblUnit_id = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblUnit_id");
                //Label lblHSNCode = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblHSNCode");
                Label lblItem = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItem");
                Label lblQuantity = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblQuantity");
                Label lblRate = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblRate");
                Label lblAmount = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblAmount");
                //Label lblCGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("CGST");
                //Label lblSGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("SGST");
                //Label lblIGST = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("IGST");
                //Label lblCGSTPer = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblCGSTPer");
                //Label lblSGSTPer = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblSGSTPer");
                //Label lblIGSTPer = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblIGSTPer");

                Label lblUnit = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblUnit");
                //Label lblLedgerName = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblLedgerName");
                //Label lblLedgerID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblLedgerID");
                // Label lblTaxbility = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");

                if (lblItemRowNo.Text != ID && ViewState["RowNo"].ToString() == "0")
                {
                    RID++;
                    dt_GridViewItem.Rows.Add(lblItemRowNo.Text, lblItemID.Text, lblUnit_id.Text, lblItem.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, lblUnit.Text);
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
                        //string HSNCode = ds.Tables[0].Rows[0]["HSNCode"].ToString();
                        //string Taxbility = ds.Tables[0].Rows[0]["Taxbility"].ToString();
                        //string CGSTPer = ds.Tables[0].Rows[0]["CGST"].ToString();
                        //string SGSTPer = ds.Tables[0].Rows[0]["SGST"].ToString();
                        //string IGSTPer = "0";
                        //decimal CGST = decimal.Parse(ds.Tables[0].Rows[0]["CGST"].ToString());
                        //decimal SGST = decimal.Parse(ds.Tables[0].Rows[0]["SGST"].ToString());
                        //decimal IGST;
                        decimal Amount = decimal.Parse(txtTotalAmount.Text);
                        //CGST = Math.Round((Amount * CGST) / 100, 2);
                        //SGST = Math.Round((Amount * SGST) / 100, 2);
                        //IGST = 0;

                        dt_GridViewItem.Rows.Add(null, ddlItemName.SelectedValue.ToString(), UnitID.ToString(), Item.ToString(), txtQuantity.Text, txtRate.Text, Amount.ToString(), Unit.ToString());
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
                    //string HSNCode = ds.Tables[0].Rows[0]["HSNCode"].ToString();
                    //string Taxbility = ds.Tables[0].Rows[0]["Taxbility"].ToString();
                    //string CGSTPer = ds.Tables[0].Rows[0]["CGST"].ToString();
                    //string SGSTPer = ds.Tables[0].Rows[0]["SGST"].ToString();
                    //string IGSTPer = ds.Tables[0].Rows[0]["IGST"].ToString();
                    //IGSTPer = "0";
                    //decimal CGST = decimal.Parse(ds.Tables[0].Rows[0]["CGST"].ToString());
                    //decimal SGST = decimal.Parse(ds.Tables[0].Rows[0]["SGST"].ToString());
                    //decimal IGST;
                    decimal Amount = decimal.Parse(txtTotalAmount.Text);
                    //CGST = Math.Round((Amount * CGST) / 100, 2);
                    //SGST = Math.Round((Amount * SGST) / 100, 2);
                    //IGST = 0;
                    decimal TotalAmount = Amount;
                    RID++;
                    dt_GridViewItem.Rows.Add(null, ddlItemName.SelectedValue.ToString(), UnitID.ToString(), Item.ToString(), txtQuantity.Text, txtRate.Text, Amount.ToString(), Unit.ToString());
                }

            }
            decimal TAmount = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
            //decimal CGSTAmount = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
            //decimal SGSTTAmount = dt_GridViewItem.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
            GridViewItem.DataSource = dt_GridViewItem;
            GridViewItem.DataBind();
            if (GridViewItem.Rows.Count > 0)
            {

                GridViewItem.FooterRow.Cells[3].Text = "<b>Total : </b>";
                GridViewItem.FooterRow.Cells[4].Text = "<b>" + TAmount.ToString() + "</b>";
                //GridViewItem.FooterRow.Cells[6].Text = "<b>" + CGSTAmount.ToString() + "</b>";
                //GridViewItem.FooterRow.Cells[7].Text = "<b>" + SGSTTAmount.ToString() + "</b>";
                GridViewItem.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                //GridViewItem.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                //GridViewItem.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            }



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


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    //Add Ledger/Amount Detail Event & Function


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
            lblMsg.Text = "";
            int status = 0;
            string LedgerId = ddlDebitLedger.SelectedValue.ToString();


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
                        ddlDebitLedger.ClearSelection();
                        txDebtorAmt.Text = "";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);



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

    protected void GridViewDebtor_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            int LedgerID = int.Parse(GridViewDebtor.DataKeys[e.RowIndex].Value.ToString());



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


    //Save Data
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        try
        {

            string msg = "";

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
            if (ddlLedger.SelectedIndex == 0)
            {
                msg += "Select By Dr. \\n";
            }
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

                    ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "GSTVoucher" }, new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Consumption Voucher", "Consumption Voucher", VoucherTx_No, txtVoucherTx_Ref.Text, txtVoucherTx_Narration.Text, lblGrandTotal.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_IsActive, ViewState["Emp_ID"].ToString(), "0", "No" }, "dataset");

                    
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string VoucherTx_ID = ds.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
                        string LedgerAmount = "-" + lblGrandTotal.Text;
                        objdb.ByProcedure("SpFinLedgerTx",
                           new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                           new string[] { "0", ddlLedger.SelectedValue, "Main Ledger", VoucherTx_ID, "Consumption Voucher", LedgerAmount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), "1", "None" }, "dataset");
                        int rowItemIndex = 0;
                        int gridItemRows = GridViewItem.Rows.Count;
                        for (rowItemIndex = 0; rowItemIndex < gridItemRows; rowItemIndex++)
                        {
                            Label lblItemRowNo = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblItemRowNo");
                            Label lblItemID = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblItemID");
                            Label lblUnit_id = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblUnit_id");
                            Label lblItem = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblItem");
                            Label lblQuantity = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblQuantity");
                            Label lblRate = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblRate");
                            Label lblAmount = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblAmount");



                            objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Name", "VoucherTx_Type", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy" }
                                , new string[] { "0", VoucherTx_ID, "Consumption Voucher", "Consumption Voucher", lblItemID.Text, lblUnit_id.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, ViewState["Office_ID"].ToString(), FinancialYear.ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), lblItemRowNo.Text }, "dataset");

                            objdb.ByProcedure("SpFinItemTx",
                                        new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "TransactionFrom", "InvoiceNo", "Office_Id", "CreatedBy", "TranDt", "Amount" }
                                       , new string[] { "4", lblItemID.Text, "0", lblQuantity.Text, lblRate.Text, VoucherTx_ID, "Consumption Voucher", VoucherTx_No, ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), lblAmount.Text }, "dataset");


                        }


                        int LedgerTable = GridViewDebtor.Rows.Count;
                        for (int i = 0; i < LedgerTable; i++)
                        {

                            Label Ledger_ID = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("Ledger_ID");

                            Label LedgerTx_Amount = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("LedgerTx_Amount");

                            string LedgerTxAmount = LedgerTx_Amount.Text;
                            //if (lblGrandTotal.Text.Contains("-"))
                            //{
                            //    LedgerTxAmount = LedgerTxAmount.Replace(@"-", string.Empty);
                            //}
                            //else
                            //{
                            //    LedgerTxAmount = "-" + LedgerTxAmount;
                            //}



                            // int TableId = int.Parse(Ledger_ID.Text);

                            objdb.ByProcedure("SpFinLedgerTx",
                            new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                            new string[] { "0", Ledger_ID.Text, "Main Ledger", VoucherTx_ID, "Consumption Voucher", LedgerTxAmount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (i + 1).ToString(), "None" }, "dataset");


                        }
                        objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "40", VoucherTx_ID }, "dataset");
                    }

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                    ClearText();

                }
                else if (btnAccept.Text == "Update" && ViewState["VoucherTx_ID"].ToString() != "0" && Status == 0)
                {

                    objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_InsertedBy", "VoucherTx_SalesCenterID" }
                        , new string[] { "7", ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Consumption Voucher", "Consumption Voucher", VoucherTx_No, txtVoucherTx_Ref.Text, txtVoucherTx_Narration.Text, lblGrandTotal.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), ViewState["Emp_ID"].ToString(), "0" }, "dataset");
                    objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "1", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                    objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");

                    // DR
                    string LedgerAmount = "-" + lblGrandTotal.Text;
                    objdb.ByProcedure("SpFinLedgerTx",
                       new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                       new string[] { "0", ddlLedger.SelectedValue, "Main Ledger", ViewState["VoucherTx_ID"].ToString(), "Consumption Voucher", LedgerAmount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), "1", "None" }, "dataset");

                    int rowItemIndex = 0;
                    int gridItemRows = GridViewItem.Rows.Count;
                    for (rowItemIndex = 0; rowItemIndex < gridItemRows; rowItemIndex++)
                    {
                        Label lblItemRowNo = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblItemRowNo");
                        Label lblItemID = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblItemID");
                        Label lblUnit_id = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblUnit_id");
                        Label lblItem = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblItem");
                        Label lblQuantity = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblQuantity");
                        Label lblRate = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblRate");
                        Label lblAmount = (Label)GridViewItem.Rows[rowItemIndex].Cells[0].FindControl("lblAmount");



                        objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Name", "VoucherTx_Type", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy" }
                            , new string[] { "0", ViewState["VoucherTx_ID"].ToString(), "Consumption Voucher", "Consumption Voucher", lblItemID.Text, lblUnit_id.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text, ViewState["Office_ID"].ToString(), FinancialYear.ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), lblItemRowNo.Text }, "dataset");

                        objdb.ByProcedure("SpFinItemTx",
                                    new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "TransactionFrom", "InvoiceNo", "Office_Id", "CreatedBy", "TranDt", "Amount" }
                                   , new string[] { "4", lblItemID.Text, "0", lblQuantity.Text, lblRate.Text, ViewState["VoucherTx_ID"].ToString(), "Consumption Voucher", VoucherTx_No, ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), lblAmount.Text }, "dataset");


                    }




                   
                    int LedgerTable = GridViewDebtor.Rows.Count;
                    for (int i = 0; i < LedgerTable; i++)
                    {
                        
                            Label Ledger_ID = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("Ledger_ID");

                            Label LedgerTx_Amount = (Label)GridViewDebtor.Rows[i].Cells[0].FindControl("LedgerTx_Amount");
                            string LedgerTxAmount = LedgerTx_Amount.Text;
                            //if (lblGrandTotal.Text.Contains("-"))
                            //{
                            //    LedgerTxAmount = LedgerTxAmount.Replace(@"-", string.Empty);
                            //}
                            //else
                            //{
                            //    LedgerTxAmount = "-" + LedgerTxAmount;
                            //}


                            // int TableId = int.Parse(Ledger_ID.Text);

                            objdb.ByProcedure("SpFinLedgerTx",
                            new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_MaintainType" },
                            new string[] { "0", Ledger_ID.Text, "Main Ledger", ViewState["VoucherTx_ID"].ToString(), "Consumption Voucher", LedgerTxAmount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (i + 1).ToString(), "None" }, "dataset");
                      

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
            txtQuantity.Text = "";
            txtRate.Text = "";
            txtTotalAmount.Text = "";
            GridViewItem.DataSource = new string[] { };
            GridViewItem.DataBind();
            lblGrandTotal.Text = "0";
            ddlLedger.ClearSelection();
            ddlDebitLedger.ClearSelection();
            // ddlSalesCenter.ClearSelection();
            txtVoucherTx_Narration.Text = "";
            CreateLedgerTable();
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
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "43", ViewState["VoucherTx_ID"].ToString() }, "dataset");
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
                    txtVoucherTx_No.Text = Lstring;
                    lblVoucherTx_No.Text = Fstring;
                    ViewState["VoucherTx_Date"] = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    ViewState["FY"] = ds.Tables[0].Rows[0]["VoucherTx_FY"].ToString();
                    txtVoucherTx_Ref.Text = ds.Tables[0].Rows[0]["VoucherTx_Ref"].ToString();
                    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    lblGrandTotal.Text = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
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
                        //decimal CGSTAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                        //decimal SGSTTAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                        GridViewItem.FooterRow.Cells[3].Text = "<b>Total : </b>";
                        GridViewItem.FooterRow.Cells[4].Text = "<b>" + TAmount.ToString() + "</b>";
                        //GridViewItem.FooterRow.Cells[6].Text = "<b>" + CGSTAmount.ToString() + "</b>";
                        //GridViewItem.FooterRow.Cells[7].Text = "<b>" + SGSTTAmount.ToString() + "</b>";
                        GridViewItem.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                        //GridViewItem.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                        //GridViewItem.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
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
                    ddlLedger.SelectedValue = ds.Tables[3].Rows[0]["Ledger_ID"].ToString();

                }
                //if (ds.Tables[3].Rows.Count > 0)
                //{
                //    DataTable dt_GridViewLedger = (DataTable)ViewState["LedgerAmount"];
                //    dt_GridViewLedger = ds.Tables[3];
                //    ViewState["LedgerAmount"] = dt_GridViewLedger;

                //}

                //if (ds.Tables[5].Rows.Count > 0)
                //{


                //    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];

                //    int Legrowcount = ds.Tables[2].Rows.Count;
                //    for (int Leg = 0; Leg < Legrowcount; Leg++)
                //    {
                //        string TNO = ds.Tables[2].Rows[Leg]["Ledger_ID"].ToString();
                //        if (ds.Tables[2].Rows[Leg]["LedgerTx_MaintainType"].ToString() == "BillByBill")
                //        {

                //            DataTable dt_BillByBillTable = new DataTable(TNO);
                //            DataColumn RowNo = dt_BillByBillTable.Columns.Add("RowNo", typeof(int));
                //            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
                //            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
                //            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
                //            dt_BillByBillTable.Columns.Add(new DataColumn("Type", typeof(string)));
                //            dt_BillByBillTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));

                //            RowNo.AutoIncrement = true;
                //            RowNo.AutoIncrementSeed = 1;
                //            RowNo.AutoIncrementStep = 1;

                //            int rowscount = ds.Tables[5].Rows.Count;
                //            for (int i = 0; i < rowscount; i++)
                //            {
                //                if (TNO == ds.Tables[5].Rows[i]["Ledger_ID"].ToString())
                //                {
                //                    string Ledger_ID = ds.Tables[5].Rows[i]["Ledger_ID"].ToString();
                //                    string BillByBillTx_RefType = ds.Tables[5].Rows[i]["BillByBillTx_RefType"].ToString();
                //                    string BillByBillTx_Ref = ds.Tables[5].Rows[i]["BillByBillTx_Ref"].ToString();
                //                    string BillByBillTx_Amount = ds.Tables[5].Rows[i]["BillByBillTx_Amount"].ToString();
                //                    string Type = ds.Tables[5].Rows[i]["BillByBillTxType"].ToString();



                //                    dt_BillByBillTable.Rows.Add(null, BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Type, Ledger_ID);

                //                }
                //            }
                //            if (dt_BillByBillTable.Rows.Count > 0)
                //            {
                //                dsBillByBill.Merge(dt_BillByBillTable);
                //                ViewState["BillByBillTable"] = dt_BillByBillTable;
                //                ViewState["dsBillByBill"] = dsBillByBill;


                //            }
                //        }
                //    }
                //}
                //if (ds.Tables[6].Rows.Count > 0)
                //{

                //    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                //    int rowscount = ds.Tables[6].Rows.Count;
                //    for (int i = 0; i < rowscount; i++)
                //    {

                //        string Ledger_ID = ds.Tables[6].Rows[i]["Ledger_ID"].ToString();
                //        string ChequeTx_No = ds.Tables[6].Rows[i]["ChequeTx_No"].ToString();
                //        string ChequeTx_Date = ds.Tables[6].Rows[i]["ChequeTx_Date"].ToString();
                //        string ChequeTx_Amount = ds.Tables[6].Rows[i]["ChequeTx_Amount"].ToString();
                //        string TNO = Ledger_ID.ToString();
                //        DataTable dt_FinChequeTx = new DataTable(TNO);
                //        dt_FinChequeTx.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
                //        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_No", typeof(string)));
                //        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Date", typeof(string)));
                //        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Amount", typeof(decimal)));
                //        dt_FinChequeTx.Rows.Add(Ledger_ID, ChequeTx_No, ChequeTx_Date, ChequeTx_Amount);
                //        dsBillByBill.Merge(dt_FinChequeTx);
                //        ViewState["FinChequeTx"] = dt_FinChequeTx;
                //        ViewState["dsBillByBill"] = dsBillByBill;
                //    }


                //}
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
            divitem.Visible = false;
            divdebtor.Visible = false;
            panel1.Enabled = false;
            panel2.Enabled = false;
            lbkbtnAddLedger.Visible = false;
            //GridViewItem.Columns[10].Visible = false;
            GridViewItem.Columns[5].Visible = false;
            GridViewDebtor.Columns[4].Visible = false;
           // GridViewDebtor.Columns[5].Visible = false;

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

    protected void lnkPreviousVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID", "VoucherTx_Type" }, new string[] { "33", ViewState["Office_ID"].ToString(), "Consumption Voucher" }, "dataset");
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
                 new string[] { "14", "Consumption Voucher", ViewState["Office_ID"].ToString() }, "dataset");

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

                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();

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

                }

                if (ds.Tables[3].Rows.Count > 0)
                {
                    DataTable dt_GridViewLedger = (DataTable)ViewState["LedgerAmount"];
                    dt_GridViewLedger = ds.Tables[3];
                    ViewState["LedgerAmount"] = dt_GridViewLedger;


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
            string VoucherTx_Type = "Consumption Voucher";
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

        WarehouseID = "0";
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
        mis_Finance_VoucherConsumption OBJ = new mis_Finance_VoucherConsumption();
        string Availablestock = OBJ.ItemAvailableStock(Qauntity, OfficeID, ItemID, WarehouseID);
        return Availablestock;
    }

}