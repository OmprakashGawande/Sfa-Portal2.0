using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Finance_VoucherGoodsTransferInward : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    //static DataSet dsItem;
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {
                lblMsg.Text = "";
                if (!IsPostBack)
                {

                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    CreateItemDataSet();
                    CreateLedgerTable();
                    FillParticularsDropDown();
                    FillItem();
                    txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
                    ddlLedgerCr.Enabled = false;
                    ViewState["VoucherTx_ID"] = "0";
                    ViewState["Action"] = "";
                    txtLedgerCr_Amount.Attributes.Add("readonly", "readonly");
                    FillVoucherDate();

                    GridViewLedgerDetail.DataSource = new string[] { };
                    GridViewLedgerDetail.DataBind();
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
            else
            {
                Response.Redirect("~/mis/Login.aspx");
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
            ds = objdb.ByProcedure("SpItemMaster", new string[] { "flag", "Office_ID" }, new string[] { "24", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlItemName.Items.Clear();
                ddlItemName.DataSource = ds.Tables[0];
                ddlItemName.DataTextField = "ItemName";
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
            if (ViewState["VoucherTx_ID"].ToString() == "0")
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
                string VoucherTx_Names_ForSno = "Payment,Journal,Contra";

                DataSet ds1 = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "Office_ID", "VoucherTx_FY", "VoucherTx_Names_ForSno" },
                    new string[] { "13", ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_Names_ForSno }, "dataset");
                string Office_Code = "";
                if (ds1.Tables[1].Rows.Count != 0)
                {
                    Office_Code = ds1.Tables[1].Rows[0]["Office_Code"].ToString();
                }
                //int VoucherTx_SNo = 0;
                //if (ds1.Tables[0].Rows.Count != 0)
                //{
                //    VoucherTx_SNo = Convert.ToInt32(ds1.Tables[0].Rows[0]["VoucherTx_SNo"].ToString());

                //}

                //ViewState["PreVoucherNo"] = Office_Code + FinancialYear.ToString().Substring(2) + "VR" + VoucherTx_SNo.ToString();
                //VoucherTx_SNo++;

                //ViewState["VoucherTx_SNo"] = VoucherTx_SNo;

                //txtVoucherTx_No.Text = Office_Code + FinancialYear.ToString().Substring(2) + "VR" + VoucherTx_SNo.ToString();
                lblVoucherTx_No.Text = Office_Code + FinancialYear.ToString().Substring(2) + "VR";
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Fill LedgerDropdown
    protected void FillParticularsDropDown()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster",
               new string[] { "flag", "Office_ID", "MultipleHeadIDs" },
               new string[] { "61", ViewState["Office_ID"].ToString(), "159" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlLedgerDr.DataSource = ds;
                ddlLedgerDr.DataTextField = "Ledger_Name";
                ddlLedgerDr.DataValueField = "Ledger_ID";
                ddlLedgerDr.DataBind();
                ddlLedgerDr.Items.Insert(0, "Select");

            }
            else
            {

            }
            ds = objdb.ByProcedure("SpFinLedgerMaster",
               new string[] { "flag" },
               new string[] { "62" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlLedgerCr.DataSource = ds;
                ddlLedgerCr.DataTextField = "Ledger_Name";
                ddlLedgerCr.DataValueField = "Ledger_ID";
                ddlLedgerCr.DataBind();
                ds = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "1", ddlLedgerCr.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtCurrentBalanceCr.Text = ds.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
                }

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Ledger CurrentBalance
    protected void ddlLedgerDr_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            FillCurrentBalance();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Ledger CurrentBalance
    protected void FillCurrentBalance()
    {
        try
        {
            lblMsg.Text = "";
            txtCurrentBalanceDr.Text = "";
            if (ddlLedgerDr.SelectedIndex > 0)
            {

                ds = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "1", ddlLedgerDr.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtCurrentBalanceDr.Text = ds.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void CreateItemDataSet()
    {
        DataSet dsItem = new DataSet();
        ViewState["dsItem"] = dsItem;

    }
    protected void CreateLedgerTable()
    {
        ViewState["LedgerTable"] = "";
        DataTable dt_LedgerTable = new DataTable();
        DataColumn RowNo = dt_LedgerTable.Columns.Add("RowNo", typeof(int));
        dt_LedgerTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_Debit", typeof(decimal)));
        RowNo.AutoIncrement = true;
        RowNo.AutoIncrementSeed = 1;
        RowNo.AutoIncrementStep = 1;
        ViewState["LedgerTable"] = dt_LedgerTable;

        GridViewLedgerDetail.DataSource = dt_LedgerTable;
        GridViewLedgerDetail.DataBind();
    }
    protected void CreateItemTable()
    {
        int TNO = 0;
        int count = GridViewLedgerDetail.Rows.Count;
        if (count > 0)
        {
            foreach (GridViewRow rows in GridViewLedgerDetail.Rows)
            {
                Label rowno = (Label)rows.FindControl("lblRowNumber");
                TNO = int.Parse(rowno.Text);
            }
            TNO = TNO + 1;
        }
        else
        {
            TNO = TNO + 1;
        }
        DataTable dt_ItemTable = new DataTable(TNO.ToString());
        dt_ItemTable.Columns.Add(new DataColumn("Item_ID", typeof(string)));
        dt_ItemTable.Columns.Add(new DataColumn("Item", typeof(string)));
        dt_ItemTable.Columns.Add(new DataColumn("UnitID", typeof(string)));
        dt_ItemTable.Columns.Add(new DataColumn("UQCCode", typeof(string)));
        dt_ItemTable.Columns.Add(new DataColumn("Quantity", typeof(float)));
        dt_ItemTable.Columns.Add(new DataColumn("Rate", typeof(decimal)));
        dt_ItemTable.Columns.Add(new DataColumn("Amount", typeof(decimal)));
        ViewState["ItemTable"] = dt_ItemTable;

        GridViewItem.DataSource = dt_ItemTable;
        GridViewItem.DataBind();
    }
    //Fill Previous VoucherNarration
    protected void btnNarration_Click(object sender, EventArgs e)
    {
        ds = objdb.ByProcedure("SpFinVoucherTx",
                 new string[] { "flag", "VoucherTx_Type", "Office_ID" },
                 new string[] { "14", "Goods Transfer Inward", ViewState["Office_ID"].ToString() }, "dataset");

        if (ds.Tables[0].Rows.Count != 0)
        {
            txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
        }
    }

    //change voucher series according to FY
    protected void txtVoucherTx_Date_TextChanged(object sender, EventArgs e)
    {
      //  FillVoucherNo();
        string ValidStatus = ValidDate();
        if (ValidStatus == "No")
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('You are not allowed to choose this date, please contact to head office.');", true);
            FillVoucherDate();
        }
        else
        {
            FillVoucherNo();
        }
    }
    protected void btnAddLedger_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";

            if (ddlLedgerDr.SelectedIndex == 0)
            {
                msg = "Select Particulars.\\n";
            }
            if (txtLedgerDr_Amount.Text == "")
            {
                msg += "Enter Amount.\\n";
            }
            if (msg == "")
            {
                int Status = 0;
                foreach (GridViewRow gvrows in GridViewLedgerDetail.Rows)
                {
                    Label Ledger_ID = (Label)gvrows.FindControl("Ledger_ID");
                    if(Ledger_ID.Text == ddlLedgerDr.SelectedValue.ToString())
                    {
                        Status = 1;
                    }
                }
                if (Status == 0)
                {
                    ViewState["LedgerAmount"] = txtLedgerDr_Amount.Text;
                    ddlItemName.ClearSelection();
                    txtQuantity.Text = "";
                    txtRate.Text = "";
                    txtTotalAmount.Text = txtLedgerDr_Amount.Text;
                    CreateItemTable();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowAddItemModal();", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Ledger Already Exists');", true);
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            if (ddlItemName.SelectedIndex == 0)
            {
                msg += "Select Item Name.\\n";
            }
            if (txtQuantity.Text == "")
            {
                msg += "Enter Quantity.\\n";
            }
            if (txtRate.Text == "")
            {
                msg += "Enter Rate.\\n";
            }
            if (txtTotalAmount.Text == "")
            {
                msg += "Enter Amount.\\n";
            }
            if (msg == "")
            {
                string LedgerAmount = ItemAmount("0");
                decimal Status = decimal.Parse(ViewState["LedgerAmount"].ToString()) - decimal.Parse(LedgerAmount.ToString());
                if (Status == 0)
                {
                   
                    DataSet dsItem = (DataSet)ViewState["dsItem"];
                    dsItem.Merge((DataTable)ViewState["ItemTable"]);

                    ViewState["dsItem"] = dsItem;

                    DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                    string Ledger_Name = ddlLedgerDr.SelectedItem.Text + "&nbsp;&nbsp;<b>(Cur Bal: " + txtCurrentBalanceDr.Text + ")</b>";
                    dt_LedgerTable.Rows.Add(null, ddlLedgerDr.SelectedValue.ToString(), Ledger_Name, txtLedgerDr_Amount.Text);

                    GridViewLedgerDetail.DataSource = dt_LedgerTable;
                    GridViewLedgerDetail.DataBind();
                  
                    decimal LedgerDebitTotal = 0;
                    LedgerDebitTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));
                    txtLedgerCr_Amount.Text = LedgerDebitTotal.ToString();
                    GridViewLedgerDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
                    GridViewLedgerDetail.FooterRow.Cells[3].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
                    GridViewLedgerDetail.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    GridViewLedgerDetail.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                    ddlLedgerDr.ClearSelection();
                    txtLedgerDr_Amount.Text = "";
                    txtCurrentBalanceDr.Text = "";

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowAddItemModal();", true);
                    ddlItemName.ClearSelection();
                    txtQuantity.Text = "";
                    txtRate.Text = "";
                    txtTotalAmount.Text = Status.ToString();

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
    protected string ItemAmount(string ID)
    {
        decimal LedgerAmount = 0;
        try
        {

            DataTable dt_ItemTable = (DataTable)ViewState["ItemTable"];
            string Item_ID = ddlItemName.SelectedValue.ToString();
            string Unit_id = "";
            string UQCCode = "";
            ds = objdb.ByProcedure("SpItemMaster", new string[] { "flag", "ItemId" }, new string[] { "23", Item_ID }, "dataset");
            if(ds!= null)
            {
                Unit_id = ds.Tables[0].Rows[0]["Unit_id"].ToString();
                UQCCode = ds.Tables[0].Rows[0]["UQCCode"].ToString();
            }
            dt_ItemTable.Rows.Add(ddlItemName.SelectedValue.ToString(), ddlItemName.SelectedItem.Text,Unit_id,UQCCode, txtQuantity.Text, txtRate.Text, txtTotalAmount.Text);
            GridViewItem.DataSource = dt_ItemTable;
            GridViewItem.DataBind();
            foreach (GridViewRow rows in GridViewItem.Rows)
            {
                decimal Amount = 0;
                Label lblAmount = (Label)rows.FindControl("lblAmount");
                Amount = decimal.Parse(lblAmount.Text);
                LedgerAmount = LedgerAmount + Amount;
            }
            ViewState["ItemTable"] = dt_ItemTable;


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

        return LedgerAmount.ToString();
    }
    protected void GridViewLedgerDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet dsItem = (DataSet)ViewState["dsItem"];

            //int TableId = int.Parse(GridViewLedgerDetail.SelectedDataKey.Value.ToString());
            //int RowNo = int.Parse(GridViewLedgerDetail.SelectedDataKey.Value.ToString());
            //int rowindex = int.Parse(GridViewLedgerDetail.SelectedRow.RowIndex.ToString());

            //GridViewItemViewDetail.DataSource = dsItem.Tables[RowNo.ToString()];
            //GridViewItemViewDetail.DataBind();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowItemViewModal();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewLedgerDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            int RowNo = int.Parse(GridViewLedgerDetail.DataKeys[e.RowIndex].Value.ToString());
            DataSet dsItem = (DataSet)ViewState["dsItem"];
            DataSet dsItemTemp = new DataSet();
            dsItemTemp = dsItem;
            for (int i = 0; i < dsItemTemp.Tables.Count;


                i++)
            {
                if (dsItemTemp.Tables[i].TableName == (RowNo.ToString()))
                {
                    dsItem.Tables.Remove(dsItemTemp.Tables[i].TableName);

                }
            }

            DataTable dt_LedgerTableTemp = new DataTable();
            DataColumn TempRowNo = dt_LedgerTableTemp.Columns.Add("RowNo", typeof(int));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_Debit", typeof(decimal)));
            TempRowNo.AutoIncrement = true;
            TempRowNo.AutoIncrementSeed = 1;
            TempRowNo.AutoIncrementStep = 1;


            int gridRows = GridViewLedgerDetail.Rows.Count;
            for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {
                Label lblRowNumber = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("lblRowNumber");
                Label Ledger_ID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_ID");
                Label Ledger_Name = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_Name");
                Label LedgerTx_Debit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Debit");
                if (lblRowNumber.Text != RowNo.ToString())
                {

                    dt_LedgerTableTemp.Rows.Add(lblRowNumber.Text, Ledger_ID.Text, Ledger_Name.Text, LedgerTx_Debit.Text);
                }
            }
            GridViewLedgerDetail.DataSource = null;
            GridViewLedgerDetail.DataBind();
            GridViewLedgerDetail.DataSource = dt_LedgerTableTemp;
            GridViewLedgerDetail.DataBind();
            decimal LedgerDebitTotal = 0;
            LedgerDebitTotal = dt_LedgerTableTemp.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));
            GridViewLedgerDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
            GridViewLedgerDetail.FooterRow.Cells[3].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
            GridViewLedgerDetail.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            GridViewLedgerDetail.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            txtLedgerCr_Amount.Text = LedgerDebitTotal.ToString();
            ViewState["LedgerTable"] = dt_LedgerTableTemp;
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
            lblMsg.Text = "";
            string msg = "";
            if (txtVoucherTx_No.Text == "")
            {
                msg = "Enter Voucher/Bill No.\\n";
            }
            if (GridViewLedgerDetail.Rows.Count == 0)
            {
                msg += "Enter Particualr Detail.\\n";
            }
            if (txtVoucherTx_Narration.Text == "")
            {
                msg += "Enter Narration.\\n";
            }
            string ValidStatus = ValidDate();
            if (ValidStatus == "No")
            {
                Response.Redirect("~/mis/Login.aspx");
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
                    ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy" },
                                                             new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Goods Transfer Inward", "Goods Transfer Inward", VoucherTx_No, txtVoucherTx_Narration.Text, txtLedgerCr_Amount.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_IsActive, ViewState["Emp_ID"].ToString(), "0" }, "dataset");
                    string VoucherTx_ID = ds.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
                    DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dt_LedgerTable.Rows.Count; i++)
                        {
                            {
                                string RowNo = dt_LedgerTable.Rows[i]["RowNo"].ToString();
                                string Ledger_ID = dt_LedgerTable.Rows[i]["Ledger_ID"].ToString();
                                string LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString(); ;
                                LedgerTx_Amount = "-" + LedgerTx_Amount;
                                objdb.ByProcedure("SpFinLedgerTx",
                                new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType" },
                                new string[] { "0", Ledger_ID, VoucherTx_ID, "Goods Transfer Inward", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "None" }, "dataset");
                                DataSet dsItem = (DataSet)ViewState["dsItem"];
                                DataSet dsItemTemp = new DataSet();
                                dsItemTemp = dsItem;
                                for (int j = 0; j < dsItemTemp.Tables.Count; j++)
                                {
                                    if (dsItemTemp.Tables[j].TableName == RowNo.ToString())
                                    {
                                        for (int k = 0; k < dsItemTemp.Tables[j].Rows.Count; k++)
                                        {
                                            string Item_ID = dsItemTemp.Tables[j].Rows[k]["Item_ID"].ToString();
                                            string Unit_id = dsItemTemp.Tables[j].Rows[k]["UnitID"].ToString();
                                            string Quantity = dsItemTemp.Tables[j].Rows[k]["Quantity"].ToString();
                                            string Rate = dsItemTemp.Tables[j].Rows[k]["Rate"].ToString();
                                            string Amount = dsItemTemp.Tables[j].Rows[k]["Amount"].ToString();
                                            objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Name", "VoucherTx_Type", "Ledger_ID", "Item_id","Unit_id", "Quantity", "Rate", "Amount", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy" },
                                                                             new string[] { "0", VoucherTx_ID, "Goods Transfer Inward", "Goods Transfer Inward", Ledger_ID, Item_ID,Unit_id, Quantity, Rate, Amount, ViewState["Office_ID"].ToString(), FinancialYear.ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), (j + 1).ToString() }, "dataset");
                                            objdb.ByProcedure("SpFinItemTx",
                                        new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "TransactionFrom", "InvoiceNo", "Office_Id", "CreatedBy", "TranDt", "Amount" }
                                       , new string[] { "4", Item_ID, Quantity, "0", Rate, VoucherTx_ID, "Goods Transfer Inward", VoucherTx_No, ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), Amount }, "dataset");

                                        }
                                    }
                                }
                            }

                        }
                        int Count = GridViewLedgerDetail.Rows.Count;
                        objdb.ByProcedure("SpFinLedgerTx",
                                new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType" },
                                new string[] { "0", ddlLedgerCr.SelectedValue.ToString(), VoucherTx_ID, "Goods Transfer Inward", txtLedgerCr_Amount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), (Count + 1).ToString(), "Main Ledger", "None" }, "dataset");
                        objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "40", VoucherTx_ID }, "dataset");
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                        ClearText();
                    }
                }
                else if (btnAccept.Text == "Update" && ViewState["VoucherTx_ID"].ToString() != "0" && Status == 0)
                {
                    ds = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "VoucherTx_ID", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy" },
                    new string[] { "7", ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Goods Transfer Inward", "Goods Transfer Inward", VoucherTx_No, "", txtVoucherTx_Narration.Text, txtLedgerCr_Amount.Text, Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", ViewState["Emp_ID"].ToString() }, "dataset");
                    string VoucherTx_ID = ViewState["VoucherTx_ID"].ToString();
                    objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "35", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                    DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];                    
                    for (int i = 0; i < dt_LedgerTable.Rows.Count; i++)
                        {
                            {
                                string RowNo = dt_LedgerTable.Rows[i]["RowNo"].ToString();
                                string Ledger_ID = dt_LedgerTable.Rows[i]["Ledger_ID"].ToString();
                                string LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString(); ;
                                LedgerTx_Amount = "-" + LedgerTx_Amount;
                                objdb.ByProcedure("SpFinLedgerTx",
                                new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType" },
                                new string[] { "0", Ledger_ID, VoucherTx_ID, "Goods Transfer Inward", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "None" }, "dataset");
                                DataSet dsItem = (DataSet)ViewState["dsItem"];
                                DataSet dsItemTemp = new DataSet();
                                dsItemTemp = dsItem;
                                for (int j = 0; j < dsItemTemp.Tables.Count; j++)
                                {
                                    if (dsItemTemp.Tables[j].TableName == RowNo.ToString())
                                    {
                                        for (int k = 0; k < dsItemTemp.Tables[j].Rows.Count; k++)
                                        {
                                            string Item_ID = dsItemTemp.Tables[j].Rows[k]["Item_ID"].ToString();
                                            string Unit_id = dsItemTemp.Tables[j].Rows[k]["UnitID"].ToString();
                                            string Quantity = dsItemTemp.Tables[j].Rows[k]["Quantity"].ToString();
                                            string Rate = dsItemTemp.Tables[j].Rows[k]["Rate"].ToString();
                                            string Amount = dsItemTemp.Tables[j].Rows[k]["Amount"].ToString();
                                            objdb.ByProcedure("SpFinItemTx", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Name", "VoucherTx_Type", "Ledger_ID", "Item_id", "Unit_id", "Quantity", "Rate", "Amount", "Office_ID", "ItemTx_FY", "ItemTx_IsActive", "ItemTx_InsertedBy", "ItemTx_OrderBy" },
                                                                             new string[] { "0", VoucherTx_ID, "Goods Transfer Inward", "Goods Transfer Inward", Ledger_ID, Item_ID, Unit_id, Quantity, Rate, Amount, ViewState["Office_ID"].ToString(), FinancialYear.ToString(), ItemTx_IsActive, ViewState["Emp_ID"].ToString(), (j + 1).ToString() }, "dataset");
                                            objdb.ByProcedure("SpFinItemTx",
                                        new string[] { "flag", "Item_id", "Cr", "Dr", "Rate", "TransactionID", "TransactionFrom", "InvoiceNo", "Office_Id", "CreatedBy", "TranDt" }
                                       , new string[] { "4", Item_ID, Quantity, "0", Rate, VoucherTx_ID, "Goods Transfer Inward", VoucherTx_No, ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

                                        }
                                    }
                                }
                            }

                        }
                        int Count = GridViewLedgerDetail.Rows.Count;
                        objdb.ByProcedure("SpFinLedgerTx",
                                new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType" },
                                new string[] { "0", ddlLedgerCr.SelectedValue.ToString(), VoucherTx_ID, "Goods Transfer Inward", txtLedgerCr_Amount.Text, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), (Count + 1).ToString(), "Main Ledger", "None" }, "dataset");
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                       
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

    
    protected void GridViewLedgerDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRowNumber = (Label)e.Row.FindControl("lblRowNumber");
                GridView gvItem = (GridView)e.Row.FindControl("gvItem");
                string RowNo = lblRowNumber.Text;
                DataSet dsItem = (DataSet)ViewState["dsItem"];
                gvItem.DataSource = dsItem.Tables[RowNo.ToString()];
                gvItem.DataBind();
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
            GridViewLedgerDetail.DataSource = new string[] { };
            GridViewLedgerDetail.DataBind();
            CreateItemDataSet();
            CreateLedgerTable();
            FillVoucherDate();
            GetPreviousVoucherNo();
            txtVoucherTx_Narration.Text = "";
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
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "Office_ID" }, new string[] { "34", ViewState["VoucherTx_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            if(ds!= null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //txtVoucherTx_No.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    var rx = new System.Text.RegularExpressions.Regex("VR");
                    string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    var array = rx.Split(str);
                    lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    txtVoucherTx_No.Text = array[1];
                    lblVoucherTx_No.Text = array[0] + "VR";
                    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    DataSet dsItem = (DataSet)ViewState["dsItem"];
                    int rowscount = ds.Tables[3].Rows.Count;
                    for (int i = 0; i < rowscount; i++)
                    {

                        string Item_id = ds.Tables[3].Rows[i]["Item_id"].ToString();
                        string Unit_id = ds.Tables[3].Rows[i]["Unit_id"].ToString();
                        string Item = ds.Tables[3].Rows[i]["Item"].ToString();
                        string Quantity = ds.Tables[3].Rows[i]["Quantity"].ToString();
                        string Rate = ds.Tables[3].Rows[i]["Rate"].ToString();
                        string UQCCode = ds.Tables[3].Rows[i]["UQCCode"].ToString();
                        string Amount = ds.Tables[3].Rows[i]["Amount"].ToString();
                        string TNO = ds.Tables[3].Rows[i]["LedgerTx_OrderBy"].ToString();
                        DataTable dt_ItemTable = new DataTable(TNO.ToString());
                        dt_ItemTable.Columns.Add(new DataColumn("Item_ID", typeof(string)));
                        dt_ItemTable.Columns.Add(new DataColumn("Item", typeof(string)));
                        dt_ItemTable.Columns.Add(new DataColumn("UnitID", typeof(string)));
                        dt_ItemTable.Columns.Add(new DataColumn("UQCCode", typeof(string)));
                        dt_ItemTable.Columns.Add(new DataColumn("Quantity", typeof(float)));
                        dt_ItemTable.Columns.Add(new DataColumn("Rate", typeof(decimal)));
                        dt_ItemTable.Columns.Add(new DataColumn("Amount", typeof(decimal)));
                        dt_ItemTable.Rows.Add(Item_id, Item, Unit_id, UQCCode, Quantity, Rate, Amount);
                        ViewState["ItemTable"] = dt_ItemTable;

                        GridViewItem.DataSource = dt_ItemTable;
                        GridViewItem.DataBind();
                        dsItem.Merge((DataTable)ViewState["ItemTable"]);


                    }


                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    GridViewLedgerDetail.DataSource = ds.Tables[1];
                    GridViewLedgerDetail.DataBind();                  
                    decimal LedgerDebitTotal = 0;                   
                    LedgerDebitTotal = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));
                    GridViewLedgerDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
                    GridViewLedgerDetail.FooterRow.Cells[3].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
                    GridViewLedgerDetail.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    GridViewLedgerDetail.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                   
                    DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];                   
                    int gridRows = GridViewLedgerDetail.Rows.Count;
                    for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                    {
                        Label lblRowNumber = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("lblRowNumber");
                        Label Ledger_ID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_ID");                        
                        Label Ledger_Name = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_Name");                        
                        Label LedgerTx_Debit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Debit");                       
                        dt_LedgerTable.Rows.Add(lblRowNumber.Text, Ledger_ID.Text, Ledger_Name.Text,LedgerTx_Debit.Text);
                       
                    }
                    ViewState["LedgerTable"] = dt_LedgerTable;
                }
                
                 if (ds.Tables[2].Rows.Count > 0)
                 {
                     ddlLedgerCr.ClearSelection();
                     ddlLedgerCr.Items.FindByValue(ds.Tables[2].Rows[0]["Ledger_ID"].ToString()).Selected = true;
                     txtLedgerCr_Amount.Text = ds.Tables[2].Rows[0]["LedgerTx_Amount"].ToString();
                   
                 }
            }
            btnAccept.Text = "Update";
            btnClear.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //View Voucher
    protected void ViewVoucher()
    {
        try
        {
            lblVoucherTx_No.Visible = false;
            txtVoucherTx_No.Visible = false;
            lblVoucherNo.Visible = true;
            btnAccept.Visible = false;
            btnClear.Visible = false;           
            divparticular.Visible = false;
            txtVoucherTx_Narration.Attributes.Add("readonly", "readonly");
            txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
            txtVoucherTx_No.Attributes.Add("readonly", "readonly");
            GridViewLedgerDetail.Columns[5].Visible = false;

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
           
            txtTotalAmount.Text = "";

            
            if (ddlItemName.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpItemMaster",
                        new string[] { "flag", "ItemId" },
                        new string[] { "20", ddlItemName.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    lblUnit.Text = ds.Tables[0].Rows[0]["NoOfDecimalPlace"].ToString();

                }

                
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowAddItemModal();", true);
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
            string VoucherTx_Type = "Payment,Journal,Contra,GSTService Purchase,Goods Transfer Inward,Goods Transfer Outward";
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