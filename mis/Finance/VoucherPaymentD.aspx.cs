using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Finance_VoucherPaymentD : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    //static DataSet dsBillByBill;
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {
                lblMsg.Text = "";
                ddlBillByBillTx_Ref.Visible = false;
                btnFSubmit.Visible = false;
                if (!IsPostBack)
                {

                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    if (ViewState["Office_ID"].ToString() == "1")
                    {
                        vname.InnerHtml = "Payment Voucher";

                    }
                    else
                    {
                        vname.InnerHtml = "Payment Voucher";
                    }

                    if (Session["Office_ID"].ToString() == "1")
                    {

                        HL_AddLEdger.NavigateUrl = "LedgerMasterB.aspx";
                    }
                    else
                    {
                        HL_AddLEdger.NavigateUrl = "LedgerMasterB.aspx";
                    }



                    ViewState["LedgerTotal"] = "0";
                    ViewState["VoucherTx_ID"] = "0";
                    ViewState["Action"] = "";
                    txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
                    txtBillDate.Attributes.Add("readonly", "readonly");
                    txtCGST.Attributes.Add("readonly", "readonly");
                    txtSGST.Attributes.Add("readonly", "readonly");
                    txtIGST.Attributes.Add("readonly", "readonly");
                    txtTdsGSTNo.Attributes.Add("readonly", "readonly");
                    ddlcreditdebit.Enabled = false;
                    FillParticularsDropDown();
                    CreateLedgerTable();
                    CreateBillByBillDataSet();
                    CreateCostCentreDataSet();
                    FillVoucherDate();
                    FillVoucherNo();
                    FillState();
                    FillCategory();
                    GridViewBillByBillDetail.DataSource = new string[] { };
                    GridViewBillByBillDetail.DataBind();

                    GridViewLedgerDetail.DataSource = new string[] { };
                    GridViewLedgerDetail.DataBind();

                    DataTable dt_BillByBillData = new DataTable();
                    dt_BillByBillData.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
                    dt_BillByBillData.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
                    ViewState["dt_BillByBillData"] = dt_BillByBillData;
                    btnAccept.Enabled = false;

                    if (Request.QueryString["VoucherTx_ID"] != null && Request.QueryString["Action"] != null)
                    {
                        string Action = objdb.Decrypt(Request.QueryString["Action"].ToString());
                        ViewState["Action"] = Action;
                        ViewState["VoucherTx_ID"] = objdb.Decrypt(Request.QueryString["VoucherTx_ID"].ToString());
                        if (Action == "2")
                        {
                            FillDetail();
                            // string ValidStatus = ValidDate();
                            // if (ValidStatus == "No")
                            // {
                                // Response.Redirect("~/mis/Login.aspx");
                            // }
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

    //Fill Ledger DropDown
    protected void FillParticularsDropDown()
    {
        try
        {
			 if (ViewState["Office_ID"].ToString() == "1" || ViewState["Office_ID"].ToString() == "81")
            {

                ds = objdb.ByProcedure("SpFinLedgerMaster",
                  new string[] { "flag", "Office_ID", "MultipleHeadIDs" },
                  new string[] { "22", ViewState["Office_ID"].ToString(), "1,2,3,4" }, "dataset");

            }
            else
            {
                ds = objdb.ByProcedure("Usp_GetExpenseLedger",
                    new string[] { "Office_ID" },
                    new string[] { ViewState["Office_ID"].ToString() }, "dataset");


            }
             //ds = objdb.ByProcedure("SpFinLedgerMaster",
            //    new string[] { "flag", "Office_ID", "MultipleHeadIDs" },
            //    new string[] { "22", ViewState["Office_ID"].ToString(), "1,2,3,4" }, "dataset");
            // ds = objdb.ByProcedure("Usp_GetExpenseLedger",
                // new string[] {  "Office_ID"},
                // new string[] {ViewState["Office_ID"].ToString()}, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlLedger_ID.DataSource = ds;
                ddlLedger_ID.DataTextField = "Ledger_Name";
                ddlLedger_ID.DataValueField = "Ledger_ID";
                ddlLedger_ID.DataBind();
                ddlLedger_ID.Items.Insert(0, "Select");

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

    //Fill State Dropdown in Party Detail
    protected void FillState()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminState",
                          new string[] { "flag" },
                          new string[] { "6" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlState.DataTextField = "State_Name";
                ddlState.DataValueField = "State_ID";
                ddlState.DataSource = ds;
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("Select", "0"));
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

    //FillCostCentre Category
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

    //Fill LedgerCurrentBalance
    protected void ddlLedger_ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlLedger_ID.SelectedIndex > 0)
            {

                //ds = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "1", ddlLedger_ID.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                //if (ds != null && ds.Tables[0].Rows.Count > 0)
                //{
                //    txtCurrentBalance.Text = ds.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
                //}
                string sDate = (Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd")).ToString();
                DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
                int Month = int.Parse(datevalue.Month.ToString());
                int Year = int.Parse(datevalue.Year.ToString());
                int FY = Year;
                string FinancialYear = Year.ToString();
                string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
                FinancialYear = "";
                int FY_Start = FY;
                if (Month <= 3)
                {
                    FY = Year - 1;
                    FinancialYear = FY.ToString() + "-" + LFY.ToString();
                    FY_Start = FY;
                }
                else
                {

                    FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
                }

                DateTime StartDate = new DateTime(FY_Start, 4, 1);
                string FY_StartDate = StartDate.ToString("dd/MM/yyyy");


                DataSet ds1 = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Office_ID", "Ledger_ID", "FromDate", "ToDate", "FinancialYear", "FY_StartDate" },
                                new string[] { "11", ViewState["Office_ID"].ToString(), ddlLedger_ID.SelectedValue.ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), FinancialYear, Convert.ToDateTime(FY_StartDate, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    txtCurrentBalance.Text = ds1.Tables[0].Rows[0]["OpeningBalance"].ToString();
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

    //Add Ledger/Amount Detail Event & Function
    protected void CreateLedgerTable()
    {
        try
        {
            //lblMsg.Text = "";
            ViewState["LedgerTable"] = "";
            DataTable dt_LedgerTable = new DataTable();
            DataColumn RowNo = dt_LedgerTable.Columns.Add("RowNo", typeof(int));
            dt_LedgerTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
            dt_LedgerTable.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
            dt_LedgerTable.Columns.Add(new DataColumn("Type", typeof(string)));
            dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_MaintainType", typeof(string)));
            dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_Credit", typeof(decimal)));
            dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_Debit", typeof(decimal)));
            //dt_LedgerTable.Columns.Add(new DataColumn("Ledger_TableID", typeof(decimal)));
            RowNo.AutoIncrement = true;
            RowNo.AutoIncrementSeed = 1;
            RowNo.AutoIncrementStep = 1;
            ViewState["LedgerTable"] = dt_LedgerTable;


            GridViewLedgerDetail.DataSource = dt_LedgerTable;
            GridViewLedgerDetail.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void SaveLedgerDetail()
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlLedger_ID.SelectedIndex <= 0)
            {
                msg = "Select Particulars.\\n";
            }
            if (txtLedgerTx_Amount.Text == "")
            {
                msg += "Enter Amount.\\n";
            }

            if (msg == "")
            {
                ViewState["action"] = "Add";
                ViewState["LedgerIDModel"] = ddlLedger_ID.SelectedValue;
                ViewState["LedgerType"] = ddlcreditdebit.SelectedValue;
                string LedgerId = ddlLedger_ID.SelectedValue;

                int status = 0;
                txtBillByBillTx_Ref.Visible = true;
                ddlBillByBillTx_Ref.Visible = false;
                lnkView.Visible = false;
                txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
                BindBillByBillData();
                if (ddlcreditdebit.SelectedValue == "Dr")
                {
                    ViewState["LedgerAmount"] = "-" + txtLedgerTx_Amount.Text;
                }
                else
                {
                    ViewState["LedgerAmount"] = txtLedgerTx_Amount.Text;
                }
                ViewState["Amount"] = txtLedgerTx_Amount.Text;
                ViewState["BillByBillAmount"] = "0";
                txtBillByBillTx_Amount.Text = txtLedgerTx_Amount.Text;
                ddlRefType.ClearSelection();
                ddlBillByBillTx_crdr.SelectedValue = ddlcreditdebit.SelectedValue;
                txtBillByBillTx_Ref.Enabled = true;
                txtChequeTx_Amount.Text = txtLedgerTx_Amount.Text;

                ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", LedgerId, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "Yes")
                    {
                        CreateBillByBillTable();

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);
                    }
                    else if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "BankLedger")
                    {
                        btnAddCheque.Enabled = true;
                        txtSuplierName.Text = ddlLedger_ID.SelectedItem.Text;
                        CreatTableFinChequeTx();

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
                    }
                    else if (ds.Tables[0].Rows[0]["CostCentre"].ToString() == "Yes")
                    {
                        ViewState["CostCentre"] = ds.Tables[0].Rows[0]["CostCentre"].ToString();
                        CreateCostCentreTable();
                        lblCostCentreModal.Text = "";
                        ddlCategory.ClearSelection();
                        ddlSubCategory.Items.Clear();
                        ViewState["Amount"] = txtLedgerTx_Amount.Text;
                        ViewState["LedgerAmountforCostCentre"] = txtLedgerTx_Amount.Text;
                        txtCostCentreAmount.Text = ViewState["Amount"].ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
                    }
                    else
                    {
                        ViewState["CostCentre"] = "";
                        CreateBillByBillTable();
                        DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                        decimal LedgerTx_Credit;
                        decimal LedgerTx_Debit;
                        if (ddlcreditdebit.SelectedItem.Text == "Credit")
                        {
                            LedgerTx_Credit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
                            LedgerTx_Debit = 0;
                        }
                        else
                        {
                            LedgerTx_Credit = 0;
                            LedgerTx_Debit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
                        }
                        string Ledger_Name = ddlLedger_ID.SelectedItem.Text + "&nbsp;&nbsp;<b>(Cur Bal: " + txtCurrentBalance.Text + ")</b>";
                        //dt_LedgerTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), Ledger_Name, ddlcreditdebit.SelectedValue.ToString(), "None", LedgerTx_Credit, LedgerTx_Debit);
                        if (ddlLedger_ID.SelectedValue.ToString() == "6")
                        {
                            dt_LedgerTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), Ledger_Name, ddlcreditdebit.SelectedValue.ToString(), "GSTTDS", LedgerTx_Credit, LedgerTx_Debit);
                        }
                        else
                        {
                            dt_LedgerTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), Ledger_Name, ddlcreditdebit.SelectedValue.ToString(), "None", LedgerTx_Credit, LedgerTx_Debit);
                        }

                        GridViewLedgerDetail.DataSource = dt_LedgerTable;
                        GridViewLedgerDetail.DataBind();
                       
                        decimal LedgerCreditTotal = 0;
                        decimal LedgerDebitTotal = 0;

                        LedgerCreditTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
                        LedgerDebitTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));

                        GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>Total : </b>";
                        GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
                        GridViewLedgerDetail.FooterRow.Cells[6].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";


                        ViewState["LedgerCreditTotal"] = LedgerCreditTotal;
                        ViewState["LedgerDebitTotal"] = LedgerDebitTotal;
                        if (LedgerCreditTotal == LedgerDebitTotal && LedgerDebitTotal != 0)
                        {
                            btnAccept.Enabled = true;
                            FillNarration();
                        }
                        else
                        {
                            btnAccept.Enabled = false;
                        }
                        ViewState["LedgerTable"] = dt_LedgerTable;
                        ClearBillByBillModal();
                        txtCurrentBalance.Text = "";
                        decimal ReaminingBal = LedgerDebitTotal - LedgerCreditTotal;
                        if (ReaminingBal.ToString().Contains("-"))
                        {
                            ReaminingBal = decimal.Parse(ReaminingBal.ToString().Replace(@"-", string.Empty));
                            txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                            ddlcreditdebit.SelectedValue = "Dr";
                        }
                        else
                        {
                            txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                            ddlcreditdebit.SelectedValue = "Cr";

                        }
                        ddlcreditdebit.Enabled = true;
                    }
                }
                //foreach (GridViewRow row in GridViewLedgerDetail.Rows)
                //{
                //    Label Ledger_ID = (Label)row.FindControl("Ledger_ID");
                //    LinkButton lnkbtnEdit = (LinkButton)row.FindControl("lnkbtnEdit");
                //    if (Ledger_ID.Text == "813")
                //    {
                //        lnkbtnEdit.Visible = false;
                //    }
                //    //else
                //    //{
                //    //    lnkbtnEdit.Visible = true;
                //    //}
                //}
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
    protected void btnAddLedger_Click(object sender, EventArgs e)
    {
        try
        {
            lblGSTModal.Text = "";
            int tdsGST = 0;
            string TDS = "0";
            
            if (ddlLedger_ID.SelectedIndex > 0)
            {
                if (ddlLedger_ID.SelectedValue.ToString() == "6" && ddlcreditdebit.SelectedValue.ToString() == "Cr")
                {
                    foreach (GridViewRow row in GridViewLedgerDetail.Rows)
                    {
                        Label lblLedger_ID = (Label)row.FindControl("Ledger_ID");
                       // LinkButton lnkbtnEdit = (LinkButton)row.FindControl("lnkbtnEdit");
                        
                        if (lblLedger_ID.Text == "6")
                        {
                            TDS = "1";
                           // lnkbtnEdit.Visible = false;
                            break;
                        }
                    }
                    if (TDS == "0")
                    {
                        CreateTDSGSTTable();
                        ClearTDSGST();
                        if (GridViewLedgerDetail.Rows.Count > 0)
                        {
                            if (ViewState["Office_ID"].ToString() == "1")
                            {
                                ds = objdb.ByProcedure("SpFinTDSGSTEntry", new string[] { "flag", "Head_ID", "Office_ID" }, new string[] { "11", "115", "1" }, "dataset");
                                if (ds != null && ds.Tables[0].Rows.Count > 0)
                                {
                                    ddlPartyName.DataTextField = "Ledger_Name";
                                    ddlPartyName.DataValueField = "Ledger_ID";
                                    ddlPartyName.DataSource = ds;
                                    ddlPartyName.DataBind();
                                    ddlPartyName.Items.Insert(0, new ListItem("Select", "0"));
                                }
                                tdsGST = 1;
                            }
                            else
                            {
                                string Ledger_ID = "";
                                foreach (GridViewRow row in GridViewLedgerDetail.Rows)
                                {
                                    Label lblLedger_ID = (Label)row.FindControl("Ledger_ID");
                                    Label Type = (Label)row.FindControl("Type");
                                    if (Type.Text == "Dr")
                                    {
                                        tdsGST = 1;
                                        Ledger_ID += lblLedger_ID.Text + ",";
                                    }
                                }
                                ds = objdb.ByProcedure("SpFinTDSGSTEntry", new string[] { "flag", "ChildLedger_ID" }, new string[] { "3", Ledger_ID }, "dataset");
                                if (ds != null && ds.Tables[0].Rows.Count > 0)
                                {
                                    ddlPartyName.DataTextField = "Ledger_Name";
                                    ddlPartyName.DataValueField = "Ledger_ID";
                                    ddlPartyName.DataSource = ds;
                                    ddlPartyName.DataBind();
                                    ddlPartyName.Items.Insert(0, new ListItem("Select", "0"));
                                }
                                if (gvTdsGSTDetail.Rows.Count > 0)
                                {
                                    btnFSubmit.Visible = true;
                                }
                            }
                        }
                    }
                    if (tdsGST == 0 && TDS == "0")
                    {
                        ddlLedger_ID.ClearSelection();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please select ledger other than Tds-Gst(Cr) in Debit section.');", true);
                    }
                    if (TDS == "0" && tdsGST != 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowTDSDetailModal();", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Tds-Gst(Cr) is already exists.');", true);
                    }

                }
                else
                {
                    SaveLedgerDetail();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewLedgerDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
            DataSet dsCostCentre = (DataSet)ViewState["dsCostCentre"];
            //int TableId = int.Parse(GridViewLedgerDetail.SelectedDataKey.Value.ToString());
            int RowNo = int.Parse(GridViewLedgerDetail.SelectedDataKey.Value.ToString());
            int rowindex = int.Parse(GridViewLedgerDetail.SelectedRow.RowIndex.ToString());
            Label lbl = (Label)GridViewLedgerDetail.Rows[rowindex].FindControl("lblMaintainType");

            Label lblTypeledger = (Label)GridViewLedgerDetail.Rows[rowindex].FindControl("Type");
            Label lblLedgerTx_Debit = (Label)GridViewLedgerDetail.Rows[rowindex].FindControl("LedgerTx_Debit");
            Label lblLedgerTx_Credit = (Label)GridViewLedgerDetail.Rows[rowindex].FindControl("LedgerTx_Credit");
            Label lblLedger_IDMod = (Label)GridViewLedgerDetail.Rows[rowindex].FindControl("Ledger_ID");
            ViewState["LedgerIDModel"] = lblLedger_IDMod.Text;
            ViewState["LedgerType"] = lblTypeledger.Text;


           

            if (ViewState["action"].ToString() == "View")
            {

                if (lbl.Text == "BillByBill")
                {
                    GridViewBillByBillViewDetail.DataSource = dsBillByBill.Tables[RowNo.ToString()];
                    GridViewBillByBillViewDetail.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillByBillViewModal();", true);
                }

                else if (lbl.Text == "Cheque")
                {
                    GVViewFinChequeTx.DataSource = dsBillByBill.Tables[RowNo.ToString()];
                    GVViewFinChequeTx.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetailView();", true);
                }
                else if (lbl.Text == "CostCentre")
                {
                    
                    GridCostCentreViewDetail.DataSource = dsCostCentre.Tables[RowNo.ToString()];
                    GridCostCentreViewDetail.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreDetailModal();", true);
                }
                else if (lbl.Text == "GSTTDS" && lblTypeledger.Text == "Cr")
                {
                    DataTable ds_TDSGSTDetail = (DataTable)ViewState["TdsGSTTable"];
                    gvTDSDetail.DataSource = ds_TDSGSTDetail;
                    gvTDSDetail.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowTDSDetail();", true);
                }
            }
            else if (ViewState["action"].ToString() == "Edit")
            {
                //if (lblLedger_IDMod.Text != "813" && lblTypeledger.Text != "Cr")
                //{

                //    ViewState["RowNo"] = RowNo.ToString();
                //    ViewState["BillByBillTable"] = dsBillByBill.Tables[RowNo.ToString()];

                //    txtBillByBillTx_Ref.Text = "";
                //    txtBillByBillTx_Ref.Visible = true;
                //    ddlBillByBillTx_Ref.Visible = false;
                //    ddlRefType.ClearSelection();
                //    txtBillByBillTx_Amount.Text = "";
                //    BindBillByBillData();

                //    GridViewBillByBillDetail.DataSource = dsBillByBill.Tables[RowNo.ToString()];
                //    GridViewBillByBillDetail.DataBind();

                //    decimal LedgerAmount = 0;
                //    foreach (GridViewRow rows in GridViewBillByBillDetail.Rows)
                //    {
                //        decimal Amount = 0;
                //        Label lblAmount = (Label)rows.FindControl("lblAmount");
                //        Label lblType = (Label)rows.FindControl("lblType");
                //        if (lblType.Text == "Dr")
                //        {
                //            Amount = decimal.Parse("-" + lblAmount.Text);
                //        }
                //        else
                //        {
                //            Amount = decimal.Parse(lblAmount.Text);
                //        }
                //        LedgerAmount = LedgerAmount + Amount;
                //    }

                //    if (lblTypeledger.Text == "Dr")
                //    {
                //        ViewState["LedgerAmount"] = "-" + lblLedgerTx_Debit.Text;
                //    }
                //    else
                //    {
                //        ViewState["LedgerAmount"] = lblLedgerTx_Credit.Text;
                //    }

                //    //ViewState["Amount"] = txtLedgerTx_Amount.Text;
                //    ViewState["BillByBillAmount"] = LedgerAmount;


                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);

                //}
                ViewState["RowNo"] = RowNo.ToString();

                ViewState["CostCentreTable"] = dsCostCentre.Tables[RowNo.ToString()];

                lblCostCentreModal.Text = "";
                ddlCategory.ClearSelection();
                ddlSubCategory.Items.Clear();


                GridCostCentreDetail.DataSource = dsCostCentre.Tables[RowNo.ToString()];
                GridCostCentreDetail.DataBind();

                decimal LedgerAmount = 0;
                foreach (GridViewRow rows in GridCostCentreDetail.Rows)
                {
                    decimal Amount = 0;
                    Label lblAmount = (Label)rows.FindControl("lblAmount");
                    Amount = decimal.Parse(lblAmount.Text);
                    LedgerAmount = LedgerAmount + Amount;
                }

                ViewState["Amount"] = "0";
                ViewState["LedgerAmountforCostCentre"] = LedgerAmount;

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
            }
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
            DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
            DataSet dsBillByBillTemp = new DataSet();
            dsBillByBillTemp = dsBillByBill;
            for (int i = 0; i < dsBillByBillTemp.Tables.Count; i++)
            {
                if (dsBillByBillTemp.Tables[i].TableName == RowNo.ToString())
                {
                    dsBillByBill.Tables.Remove(dsBillByBillTemp.Tables[i].TableName);
                    //dsBillByBill.Tables[i].Merge(dsBillByBillTemp.Tables[i]);
                }
            }

            DataSet dsCostCentre = (DataSet)ViewState["dsCostCentre"];
            DataSet dsCostCentreTemp = new DataSet();
            dsCostCentreTemp = dsCostCentre;
            for (int i = 0; i < dsCostCentreTemp.Tables.Count; i++)
            {
                if (dsCostCentreTemp.Tables[i].TableName == RowNo.ToString())
                {
                    dsCostCentre.Tables.Remove(dsCostCentreTemp.Tables[i].TableName);
                    //dsBillByBill.Tables[i].Merge(dsBillByBillTemp.Tables[i]);
                }
            }
            //FillNarration();
            DataTable dt_LedgerTableTemp = new DataTable();
            DataColumn TempRowNo = dt_LedgerTableTemp.Columns.Add("RowNo", typeof(int));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("Type", typeof(string)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_MaintainType", typeof(string)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_Credit", typeof(decimal)));
            dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_Debit", typeof(decimal)));
            //dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_TableID", typeof(decimal)));
            TempRowNo.AutoIncrement = true;
            TempRowNo.AutoIncrementSeed = 1;
            TempRowNo.AutoIncrementStep = 1;

            int gridRows = GridViewLedgerDetail.Rows.Count;
            for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {
                Label lblRowNumber = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("lblRowNumber");
                Label Ledger_ID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_ID");
                Label Type = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Type");
                Label lblMaintainType = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("lblMaintainType");
                Label Ledger_Name = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_Name");
                Label LedgerTx_Credit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Credit");
                Label LedgerTx_Debit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Debit");
                //Label Ledger_TableID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_TableID");
                if (lblRowNumber.Text != RowNo.ToString())
                {

                    dt_LedgerTableTemp.Rows.Add(lblRowNumber.Text, Ledger_ID.Text, Ledger_Name.Text, Type.Text, lblMaintainType.Text, LedgerTx_Credit.Text, LedgerTx_Debit.Text);
                }
                else
                {
                    if (Ledger_ID.Text == "6" && Type.Text == "Cr")
                        CreateTDSGSTTable();
                }
            }
            GridViewLedgerDetail.DataSource = null;
            GridViewLedgerDetail.DataBind();
            GridViewLedgerDetail.DataSource = dt_LedgerTableTemp;
            GridViewLedgerDetail.DataBind();
            decimal LedgerCreditTotal = 0;
            decimal LedgerDebitTotal = 0;

            LedgerCreditTotal = dt_LedgerTableTemp.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
            LedgerDebitTotal = dt_LedgerTableTemp.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));

            GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>Total : </b>";
            GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
            GridViewLedgerDetail.FooterRow.Cells[6].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";
            GridViewLedgerDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            GridViewLedgerDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;


            ViewState["LedgerCreditTotal"] = LedgerCreditTotal;
            ViewState["LedgerDebitTotal"] = LedgerDebitTotal;
            if (ViewState["LedgerCreditTotal"] == "0")
            {
                ddlcreditdebit.Enabled = false;
            }
            else
            {
                ddlcreditdebit.Enabled = true;
            }
            if (LedgerCreditTotal == LedgerDebitTotal && LedgerDebitTotal != 0)
            {
                btnAccept.Enabled = true;
                FillNarration();
            }
            else
            {
                btnAccept.Enabled = false;
            }

            ViewState["LedgerTable"] = dt_LedgerTableTemp;
            if (dt_LedgerTableTemp.Rows.Count == 0)
            {
                ddlcreditdebit.Enabled = false;
                ddlcreditdebit.SelectedValue = "Dr";
                FillParticularsDropDown();
                txtLedgerTx_Amount.Text = "";

            }
            else
            {
                decimal ReaminingBal = LedgerDebitTotal - LedgerCreditTotal;
                if (ReaminingBal.ToString().Contains("-"))
                {
                    ReaminingBal = decimal.Parse(ReaminingBal.ToString().Replace(@"-", string.Empty));
                    txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                    ddlcreditdebit.SelectedValue = "Dr";
                }
                else
                {
                    txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                    ddlcreditdebit.SelectedValue = "Cr";

                }
            }
            
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
        //ViewState["TableId"] = Convert.ToInt32(ViewState["TableId"].ToString()) + 1;
        DataTable dt_BillByBillTable = new DataTable(TNO.ToString());
        DataColumn RowNo = dt_BillByBillTable.Columns.Add("RowNo", typeof(int));
        dt_BillByBillTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
        dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
        dt_BillByBillTable.Columns.Add(new DataColumn("Type", typeof(string)));

        RowNo.AutoIncrement = true;
        RowNo.AutoIncrementSeed = 1;
        RowNo.AutoIncrementStep = 1;

        ViewState["BillByBillTable"] = dt_BillByBillTable;

        GridViewBillByBillDetail.DataSource = dt_BillByBillTable;
        GridViewBillByBillDetail.DataBind();
    }
    protected void ddlRefType_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);
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
                txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
                if (GridViewBillByBillDetail.Rows.Count < 1)
                {
                    txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();
                    ddlBillByBillTx_crdr.SelectedValue = ddlcreditdebit.SelectedValue;
                }
                else
                {

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
    //Bind Ledger AgstRef Detail in BillByBillModal
    protected void BindBillByBillData()
    {
        try
        {
            if (btnAccept.Text == "Accept")
            {
                DataTable dt_BillByBillData = (DataTable)ViewState["dt_BillByBillData"];

                ddlBillByBillTx_Ref.Items.Clear();
                string LedgerID = ViewState["LedgerIDModel"].ToString();

                //ds = objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "2", LedgerID, ViewState["Office_ID"].ToString() }, "dataset");
                ds = objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "2", ViewState["LedgerIDModel"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
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

                    ddlBillByBillTx_Ref.Items.Insert(0, "Select");
                    GridViewRefDetail.DataSource = new string[] { };
                    GridViewRefDetail.DataBind();
                }
            }
            else
            {
                DataTable dt_BillByBillData = (DataTable)ViewState["dt_BillByBillData"];

                ddlBillByBillTx_Ref.Items.Clear();
                //string LedgerID = ddlLedger_ID.SelectedValue.ToString();
                //ds = objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "Ledger_ID", "Office_ID", "VoucherTx_ID" }, new string[] { "12", LedgerID, ViewState["Office_ID"].ToString(), ViewState["VoucherTx_ID"].ToString() }, "dataset");
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
        try
        {
            lblMsg.Text = "";
            string LedgerAmount = BillAmount("0");
            ManageBillByBill(LedgerAmount);
            // if (float.Parse(txtLedgerTx_Amount.Text) != float.Parse(LedgerAmount))

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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

                    //dt_BillByBillTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), ddlRefType.SelectedItem.Text, ddlBillByBillTx_Ref.SelectedValue, txtBillByBillTx_Amount.Text, Type);
                    dt_BillByBillTable.Rows.Add(null, ViewState["LedgerIDModel"].ToString(), ddlRefType.SelectedItem.Text, ddlBillByBillTx_Ref.SelectedValue.Trim(), txtBillByBillTx_Amount.Text, Type);
                    DataTable dt_BillByBillData = (DataTable)ViewState["dt_BillByBillData"];

                }
                else
                {
                    dt_BillByBillTable.Rows.Add(null, ViewState["LedgerIDModel"].ToString(), ddlRefType.SelectedItem.Text, txtBillByBillTx_Ref.Text.Trim(), txtBillByBillTx_Amount.Text, Type);
                    //dt_BillByBillTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), ddlRefType.SelectedItem.Text, txtBillByBillTx_Ref.Text, txtBillByBillTx_Amount.Text, Type);
                }
                GridViewBillByBillDetail.DataSource = dt_BillByBillTable;
                GridViewBillByBillDetail.DataBind();
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
                //DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                //dsBillByBill.Merge((DataTable)ViewState["BillByBillTable"]);

                //ViewState["dsBillByBill"] = dsBillByBill;
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
        //return ViewState["BillByBillAmount"].ToString();
        return LedgerAmount.ToString();
    }
    protected void ClearBillByBillModal()
    {
        ddlRefType.ClearSelection();
        ddlBillByBillTx_Ref.ClearSelection();
        txtBillByBillTx_Ref.Text = "";
        txtBillByBillTx_Ref.Visible = true;
        ddlLedger_ID.ClearSelection();
        txtLedgerTx_Amount.Text = "";
        ViewState["BillByBillTable"] = "";
        txtCurrentBalance.Text = "";

    }

    //Add CostCentreDetail Event & Function
    protected void CreateCostCentreTable()
    {
        //DataTable dt_CostCentreTable = new DataTable();
        int CNO = 0;
        int count = GridViewLedgerDetail.Rows.Count;
        if (count > 0)
        {
            foreach (GridViewRow rows in GridViewLedgerDetail.Rows)
            {
                Label rowno = (Label)rows.FindControl("lblRowNumber");
                CNO = int.Parse(rowno.Text);
            }
            CNO = CNO + 1;
        }
        else
        {
            CNO = CNO + 1;
        }
        DataTable dt_CostCentreTable = new DataTable(CNO.ToString());
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
    protected void CreateCostCentreDataSet()
    {
        DataSet dsCostCentre = new DataSet();
        ViewState["dsCostCentre"] = dsCostCentre;

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
                    //if (ddlcreditdebit.SelectedValue == "Cr")
                    //{
                    //    dt_CostCentreTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue.ToString(), ddlSubCategory.SelectedItem.Text, Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"), Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"));
                    //}
                    //else
                    //{
                    //    dt_CostCentreTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue.ToString(), ddlSubCategory.SelectedItem.Text, Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"), ("-" + Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00")));
                    //}
                    if (ViewState["LedgerType"].ToString() == "Cr")
                    {
                        dt_CostCentreTable.Rows.Add(null, ViewState["LedgerIDModel"].ToString(), ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue.ToString(), ddlSubCategory.SelectedItem.Text, Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"), Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"));
                    }
                    else
                    {
                        dt_CostCentreTable.Rows.Add(null, ViewState["LedgerIDModel"].ToString(), ddlCategory.SelectedValue.ToString(), ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue.ToString(), ddlSubCategory.SelectedItem.Text, Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00"), ("-" + Convert.ToDecimal(txtCostCentreAmount.Text).ToString("0.00")));
                    }
                    //ddlCategory.ClearSelection();
                    ddlSubCategory.ClearSelection();
                    txtCostCentreAmount.Text = ViewState["Amount"].ToString();
                    ViewState["CostCentreTable"] = dt_CostCentreTable;

                    GridCostCentreDetail.DataSource = dt_CostCentreTable;
                    GridCostCentreDetail.DataBind();


                    CostCentreAmount = dt_CostCentreTable.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                    if ((Math.Abs(Convert.ToDecimal(ViewState["LedgerAmountforCostCentre"].ToString())) - Math.Abs(CostCentreAmount)) != Convert.ToDecimal("0"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
                    }
                    else
                    {
                        DataSet dsCostCentre = (DataSet)ViewState["dsCostCentre"];
                        if (ViewState["action"].ToString() == "Edit")
                        {

                            DataSet dsCostCentreTemp = new DataSet();
                            dsCostCentreTemp = dsCostCentre;
                            for (int i = 0; i < dsCostCentreTemp.Tables.Count; i++)
                            {
                                if (dsCostCentreTemp.Tables[i].TableName == ViewState["RowNo"].ToString())
                                {
                                    dsCostCentre.Tables.Remove(dsCostCentreTemp.Tables[i].TableName);
                                    //dsBillByBill.Tables[i].Merge(dsBillByBillTemp.Tables[i]);
                                }
                            }
                            dsCostCentre.Merge((DataTable)ViewState["CostCentreTable"]);

                            ViewState["dsCostCentre"] = dsCostCentre;
                            

                        }
                        else
                        {
                            dsCostCentre.Merge((DataTable)ViewState["CostCentreTable"]);
                            ViewState["dsCostCentre"] = dsCostCentre;
                            //SaveLedgerDetail();
                            CreateBillByBillTable();
                            DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                            decimal LedgerTx_Credit;
                            decimal LedgerTx_Debit;
                            if (ddlcreditdebit.SelectedItem.Text == "Credit")
                            {
                                LedgerTx_Credit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
                                LedgerTx_Debit = 0;
                            }
                            else
                            {
                                LedgerTx_Credit = 0;
                                LedgerTx_Debit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
                            }
                            string Ledger_Name = ddlLedger_ID.SelectedItem.Text + "&nbsp;&nbsp;<b>(Cur Bal: " + txtCurrentBalance.Text + ")</b>";
                            //dt_LedgerTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), Ledger_Name, ddlcreditdebit.SelectedValue.ToString(), "None", LedgerTx_Credit, LedgerTx_Debit);
                            if (ddlLedger_ID.SelectedValue.ToString() == "6")
                            {
                                dt_LedgerTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), Ledger_Name, ddlcreditdebit.SelectedValue.ToString(), "GSTTDS", LedgerTx_Credit, LedgerTx_Debit);
                            }
                            else if (ViewState["CostCentre"].ToString() == "Yes")
                            {
                                dt_LedgerTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), Ledger_Name, ddlcreditdebit.SelectedValue.ToString(), "CostCentre", LedgerTx_Credit, LedgerTx_Debit);
                            }
                            else
                            {
                                dt_LedgerTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), Ledger_Name, ddlcreditdebit.SelectedValue.ToString(), "None", LedgerTx_Credit, LedgerTx_Debit);
                            }

                            GridViewLedgerDetail.DataSource = dt_LedgerTable;
                            GridViewLedgerDetail.DataBind();

                            decimal LedgerCreditTotal = 0;
                            decimal LedgerDebitTotal = 0;

                            LedgerCreditTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
                            LedgerDebitTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));

                            GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>Total : </b>";
                            GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
                            GridViewLedgerDetail.FooterRow.Cells[6].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";


                            ViewState["LedgerCreditTotal"] = LedgerCreditTotal;
                            ViewState["LedgerDebitTotal"] = LedgerDebitTotal;
                            if (LedgerCreditTotal == LedgerDebitTotal && LedgerDebitTotal != 0)
                            {
                                btnAccept.Enabled = true;
                                FillNarration();
                            }
                            else
                            {
                                btnAccept.Enabled = false;
                            }
                            ViewState["LedgerTable"] = dt_LedgerTable;
                            ClearBillByBillModal();
                            txtCurrentBalance.Text = "";
                            decimal ReaminingBal = LedgerDebitTotal - LedgerCreditTotal;
                            if (ReaminingBal.ToString().Contains("-"))
                            {
                                ReaminingBal = decimal.Parse(ReaminingBal.ToString().Replace(@"-", string.Empty));
                                txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                                ddlcreditdebit.SelectedValue = "Dr";
                            }
                            else
                            {
                                txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                                ddlcreditdebit.SelectedValue = "Cr";

                            }
                            ddlcreditdebit.Enabled = true;
                        }
                        int LedgerCount = GridViewLedgerDetail.Rows.Count;
                        if (LedgerCount > 0)
                        {
                            foreach (GridViewRow row in GridViewLedgerDetail.Rows)
                            {
                                Label lblMaintainType = (Label)row.FindControl("lblMaintainType");
                                //if (lblMaintainType.Text == "Cheque")
                                //{
                                //    FillNarration();
                                //}
                                if (lblMaintainType.Text == "CostCentre")
                                {
                                    //FillNarration();
                                }
                            }
                        }
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
            if ((Math.Abs(Convert.ToDecimal(ViewState["LedgerAmountforCostCentre"].ToString())) - Math.Abs(CostCentreAmount)) != Convert.ToDecimal("0"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
            }
            else
            {
                if (ViewState["action"].ToString() != "Edit")
                {
                    //SaveLedgerDetail();
                    CreateBillByBillTable();
                    DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                    decimal LedgerTx_Credit;
                    decimal LedgerTx_Debit;
                    if (ddlcreditdebit.SelectedItem.Text == "Credit")
                    {
                        LedgerTx_Credit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
                        LedgerTx_Debit = 0;
                    }
                    else
                    {
                        LedgerTx_Credit = 0;
                        LedgerTx_Debit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
                    }
                    string Ledger_Name = ddlLedger_ID.SelectedItem.Text + "&nbsp;&nbsp;<b>(Cur Bal: " + txtCurrentBalance.Text + ")</b>";
                    //dt_LedgerTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), Ledger_Name, ddlcreditdebit.SelectedValue.ToString(), "None", LedgerTx_Credit, LedgerTx_Debit);
                    if (ddlLedger_ID.SelectedValue.ToString() == "6")
                    {
                        dt_LedgerTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), Ledger_Name, ddlcreditdebit.SelectedValue.ToString(), "GSTTDS", LedgerTx_Credit, LedgerTx_Debit);
                    }
                    else if (ViewState["CostCentre"].ToString() == "Yes")
                    {
                        dt_LedgerTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), Ledger_Name, ddlcreditdebit.SelectedValue.ToString(), "CostCentre", LedgerTx_Credit, LedgerTx_Debit);
                    }
                    else
                    {
                        dt_LedgerTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), Ledger_Name, ddlcreditdebit.SelectedValue.ToString(), "None", LedgerTx_Credit, LedgerTx_Debit);
                    }

                    GridViewLedgerDetail.DataSource = dt_LedgerTable;
                    GridViewLedgerDetail.DataBind();

                    decimal LedgerCreditTotal = 0;
                    decimal LedgerDebitTotal = 0;

                    LedgerCreditTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
                    LedgerDebitTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));

                    GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>Total : </b>";
                    GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
                    GridViewLedgerDetail.FooterRow.Cells[6].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";


                    ViewState["LedgerCreditTotal"] = LedgerCreditTotal;
                    ViewState["LedgerDebitTotal"] = LedgerDebitTotal;
                    if (LedgerCreditTotal == LedgerDebitTotal && LedgerDebitTotal != 0)
                    {
                        btnAccept.Enabled = true;
                        FillNarration();
                    }
                    else
                    {
                        btnAccept.Enabled = false;
                    }
                    ViewState["LedgerTable"] = dt_LedgerTable;
                    ClearBillByBillModal();
                    txtCurrentBalance.Text = "";
                    decimal ReaminingBal = LedgerDebitTotal - LedgerCreditTotal;
                    if (ReaminingBal.ToString().Contains("-"))
                    {
                        ReaminingBal = decimal.Parse(ReaminingBal.ToString().Replace(@"-", string.Empty));
                        txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                        ddlcreditdebit.SelectedValue = "Dr";
                    }
                    else
                    {
                        txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                        ddlcreditdebit.SelectedValue = "Cr";

                    }
                    ddlcreditdebit.Enabled = true;
                }
            }
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowCostCentreModal();", true);
        }
    }

    //AddChequeDetail Event & Function
    protected void CreatTableFinChequeTx()
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
        DataTable dt_FinChequeTx = new DataTable(TNO.ToString());
        dt_FinChequeTx.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_No", typeof(string)));
        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Date", typeof(string)));
        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Amount", typeof(decimal)));
        dt_FinChequeTx.Columns.Add(new DataColumn("IfCheque", typeof(string)));
        dt_FinChequeTx.Columns.Add(new DataColumn("AcPayee", typeof(string)));
        dt_FinChequeTx.Columns.Add(new DataColumn("FavouringName", typeof(string)));

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
        if (chkIfCheque.Checked == true)
        {
            if (txtFavouringName.Text == "")
            {
                msg += "Enter Beneficiary Name \\n";
            }
            if (txtChequeTx_Date.Text == "")
            {
                msg += "Enter Cheque/ DD Date \\n";
            }
        }
        if (txtChequeTx_Amount.Text == "")
        {
            msg += "Enter Amount \\n";
        }
        if (msg == "")
        {
            string CheqAmount = ChequeAmount("0");
            if (float.Parse(txtLedgerTx_Amount.Text) != float.Parse(CheqAmount))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);

            }
            else
            {
                DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];

                dsBillByBill.Merge((DataTable)ViewState["FinChequeTx"]);

                ViewState["dsBillByBill"] = dsBillByBill;

                DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                decimal LedgerTx_Credit;
                decimal LedgerTx_Debit;
                if (ddlcreditdebit.SelectedItem.Text == "Credit")
                {
                    LedgerTx_Credit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
                    LedgerTx_Debit = 0;
                }
                else
                {
                    LedgerTx_Credit = 0;
                    LedgerTx_Debit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
                }
                string Ledger_Name = ddlLedger_ID.SelectedItem.Text + "&nbsp;&nbsp;<b>(Cur Bal: " + txtCurrentBalance.Text + ")</b>";
                dt_LedgerTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), Ledger_Name, ddlcreditdebit.SelectedValue.ToString(), "Cheque", LedgerTx_Credit, LedgerTx_Debit);

                GridViewLedgerDetail.DataSource = dt_LedgerTable;
                GridViewLedgerDetail.DataBind();

                decimal LedgerCreditTotal = 0;
                decimal LedgerDebitTotal = 0;
                LedgerCreditTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
                LedgerDebitTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));
                GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>Total : </b>";
                GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
                GridViewLedgerDetail.FooterRow.Cells[6].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";
                GridViewLedgerDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                GridViewLedgerDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                ViewState["LedgerCreditTotal"] = LedgerCreditTotal;
                ViewState["LedgerDebitTotal"] = LedgerDebitTotal;

                if (LedgerCreditTotal == LedgerDebitTotal && LedgerDebitTotal != 0)
                {
                    btnAccept.Enabled = true;
                    FillNarration();
                }
                else
                {
                    btnAccept.Enabled = false;
                }



                ViewState["LedgerTable"] = dt_LedgerTable;

                ClearFinChequeTxModal();
                //LedgerDebitTotal = decimal.Parse("-" + LedgerDebitTotal);
                decimal ReaminingBal = LedgerDebitTotal - LedgerCreditTotal;
                if (ReaminingBal.ToString().Contains("-"))
                {
                    ReaminingBal = decimal.Parse(ReaminingBal.ToString().Replace(@"-", string.Empty));
                    txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                    ddlcreditdebit.SelectedValue = "Dr";
                }
                else
                {
                    txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                    ddlcreditdebit.SelectedValue = "Cr";

                }
                txtCurrentBalance.Text = "";
                //if (ddlcreditdebit.SelectedValue == "Cr")
                //{
                //    ddlcreditdebit.SelectedValue = "Dr";
                //}
                //else
                //{
                //    ddlcreditdebit.SelectedValue = "Cr";

                //}
                ddlcreditdebit.Enabled = true;
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

            string IfCheque = "No";
            string AcPayee = "No";

            if (chkIfCheque.Checked == true)
                IfCheque = "Yes";

            if (chkAcPayee.Checked == true)
                AcPayee = "Yes";


            dt_FinChequeTx.Rows.Add(ddlLedger_ID.SelectedValue.ToString(), txtChequeTx_No.Text, txtChequeTx_Date.Text, txtChequeTx_Amount.Text, IfCheque, AcPayee, txtFavouringName.Text);


            GVFinChequeTx.DataSource = dt_FinChequeTx;
            GVFinChequeTx.DataBind();

            decimal ChequeTx_AmountTotal = 0;

            ChequeTx_AmountTotal = dt_FinChequeTx.AsEnumerable().Sum(row => row.Field<decimal>("ChequeTx_Amount"));

            //GVFinChequeTx.FooterRow.Cells[2].Text = "<b>Total : </b>";
            //GVFinChequeTx.FooterRow.Cells[3].Text = "<b>" + ChequeTx_AmountTotal.ToString() + "</b>";

            txtChequeTx_Amount.Text = (Convert.ToDecimal(txtLedgerTx_Amount.Text) - ChequeTx_AmountTotal).ToString();

            txtChequeTx_No.Text = "";
            txtChequeTx_Date.Text = "";
            chkIfCheque.Checked = false;
            chkAcPayee.Checked = false;
            txtFavouringName.Text = "";

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
        chkIfCheque.Checked = false;
        chkAcPayee.Checked = false;
        txtFavouringName.Text = "";

        ViewState["FinChequeTx"] = "";

        GVFinChequeTx.DataSource = new string[] { };
        GVFinChequeTx.DataBind();


        ddlLedger_ID.ClearSelection();
        txtLedgerTx_Amount.Text = "";
        txtCurrentBalance.Text = "";



    }

    //Save Data   
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            decimal CGST = 0;
            decimal SGST = 0;
            decimal IGST = 0;
            decimal CGSTAmt = 0;
            decimal SGSTAmt = 0;
            decimal IGSTAmt = 0;
            string HSN_Code = "";
            string Isreversechargeapplicable = "";
            string GSTApplicable = "No";
            string Taxbility = "";
            string IsIneligibleforinputcredit = "";
            if (txtVoucherTx_No.Text == "")
            {
                msg += "Enter Voucher No. \\n";
            }
            if (txtVoucherTx_Date.Text == "")
            {
                msg += "Enter Date. \\n";
            }
            // else
            // {
                // string ValidStatus = ValidDate();
                // if (ValidStatus == "No")
                // {
                    // Response.Redirect("~/mis/Login.aspx");
                // }
            // }

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

                int Status = 0;
				DataSet ds11 = new DataSet();
                if(ViewState["Office_ID"].ToString() == "1")
                {
                    ds11 = objdb.ByProcedure("SpFinVoucherTx",
                       new string[] { "flag", "VoucherTx_No", "VoucherTx_ID", "VoucherTx_Name" },
                       new string[] { "29", VoucherTx_No, ViewState["VoucherTx_ID"].ToString(), "Payment" }, "dataset");
                }
                else
                {
                    ds11 = objdb.ByProcedure("SpFinVoucherTx",
                   new string[] { "flag", "VoucherTx_No", "VoucherTx_ID" },
                   new string[] { "9", VoucherTx_No, ViewState["VoucherTx_ID"].ToString() }, "dataset");
                }
                // DataSet ds11 = objdb.ByProcedure("SpFinVoucherTx",
                    // new string[] { "flag", "VoucherTx_No", "VoucherTx_ID" },
                    // new string[] { "9", VoucherTx_No, ViewState["VoucherTx_ID"].ToString() }, "dataset");
                if (ds11.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds11.Tables[0].Rows[0]["Status"].ToString());

                }
                if (btnAccept.Text == "Accept" && ViewState["VoucherTx_ID"].ToString() == "0" && Status == 0)
                {
                    string FinDocs = "";
                    if (FU_UploadDocs.HasFile)
                    {
                        FinDocs = "Docs/" + Guid.NewGuid() + "-" + FU_UploadDocs.FileName;
                        FU_UploadDocs.PostedFile.SaveAs(Server.MapPath(FinDocs));
                    }
                    string GSTVoucher = "No";
                    string Validate = VoucherValidate("0");
                    string Accept = VoucherAccept("0");
                    if (Validate == "0")
                    {
                        if (Accept == "1")
                        {
                            //     ds = objdb.ByProcedure("SpFinVoucherTx",
                            //new string[] { "flag", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy", "VoucherTx_SNo" },
                            //new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Payment", "Payment", txtVoucherTx_No.Text, "", txtVoucherTx_Narration.Text, ViewState["LedgerDebitTotal"].ToString(), Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", ViewState["Emp_ID"].ToString(), ViewState["VoucherTx_SNo"].ToString() }, "dataset");
                            ds = objdb.ByProcedure("SpFinVoucherTx",
                       new string[] { "flag", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy", "GSTVoucher", "UploadDocs" },
                       new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Payment", "Payment", VoucherTx_No, "", txtVoucherTx_Narration.Text, ViewState["LedgerDebitTotal"].ToString(), Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "0", ViewState["Emp_ID"].ToString(), "No", FinDocs }, "dataset");

                            string VoucherTx_ID = ds.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
                            if (chkbox.Checked == true)
                            {
                                objdb.ByProcedure("SpFinServiceSupplierDetail", new string[] { "flag", "VoucherTx_ID", "SupplierName", "SupplierAddress", "State_ID", "RegistrationTypes", "GST_No", "Supplier_IsActive", "UpdatedBy" }, new string[] { "0", VoucherTx_ID, txtSuplierName.Text, txtsupplieraddress.Text, ddlState.SelectedValue, ddlRegistrationType.SelectedItem.Text, txtGSTNo.Text, "0", ViewState["Emp_ID"].ToString() }, "dataset");
                            }
                            DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];


                            for (int i = 0; i < dt_LedgerTable.Rows.Count; i++)
                            {
                                string LedgerTx_Amount = "";
                                string RowNo = dt_LedgerTable.Rows[i]["RowNo"].ToString();
                                string Ledger_ID = dt_LedgerTable.Rows[i]["Ledger_ID"].ToString();
                                ds = objdb.ByProcedure("SpFinLedgerGSTDetails", new string[] { "flag", "Ledger_ID", "VoucherTx_Date" }, new string[] { "3", Ledger_ID.ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                                if (ds != null && ds.Tables[0].Rows.Count > 0)
                                {
                                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "true" && ds.Tables[0].Rows[0]["GSTApplicable"].ToString() == "Yes")
                                    {
                                        GSTVoucher = "Yes";

                                        if (ds.Tables[0].Rows[0]["GSTApplicable"].ToString() != "")
                                        {
                                            GSTApplicable = ds.Tables[0].Rows[0]["GSTApplicable"].ToString();
                                        }
                                        Taxbility = ds.Tables[0].Rows[0]["Taxbility"].ToString();
                                        CGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                                        SGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                                        IGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_IntegratedTax"].ToString());
                                        Isreversechargeapplicable = ds.Tables[0].Rows[0]["Isreversechargeapplicable"].ToString();
                                        IsIneligibleforinputcredit = ds.Tables[0].Rows[0]["IsIneligibleforinputcredit"].ToString();
                                        if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                                        {
                                            LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();

                                        }
                                        else
                                        {
                                            LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                                        }
                                        DataSet ds1 = objdb.ByProcedure("SpFinVoucherSaleCredit", new string[] { "flag", "Ledger_ID" }, new string[] { "3", Ledger_ID.ToString() }, "dataset");
                                        {
                                            if (ds1 != null && ds.Tables[0].Rows.Count > 0)
                                            {
                                                if (ds1.Tables[0].Rows[0]["status"].ToString() == "true")
                                                {
                                                    CGST = 0;
                                                    SGST = 0;

                                                }
                                                else
                                                {
                                                    IGST = 0;

                                                }
                                            }
                                        }


                                        decimal Amount = decimal.Parse(LedgerTx_Amount);
                                        CGSTAmt = Math.Round((Amount * CGST) / 100, 2);
                                        SGSTAmt = Math.Round((Amount * SGST) / 100, 2);
                                        IGSTAmt = 0;
                                        HSN_Code = ds.Tables[0].Rows[0]["HSN_Code"].ToString();
                                    }
                                    else
                                    {
                                        CGST = 0;
                                        SGST = 0;
                                        IGST = 0;
                                        CGSTAmt = 0;
                                        SGSTAmt = 0;
                                        IGSTAmt = 0;
                                        HSN_Code = "";
                                        Isreversechargeapplicable = "";
                                        GSTApplicable = "No";
                                        Taxbility = "";
                                        IsIneligibleforinputcredit = "";
                                        objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "GSTVoucher" }, new string[] { "37", VoucherTx_ID, "No" }, "dataset");

                                    }
                                }

                                if (dt_LedgerTable.Rows[i]["LedgerTx_MaintainType"].ToString() == "BillByBill")
                                {
                                    if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                                        LedgerTx_Amount = "-" + LedgerTx_Amount;
                                    }
                                    else
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                                    }

                                    int TableId = int.Parse(dt_LedgerTable.Rows[i]["Ledger_ID"].ToString());

                                    objdb.ByProcedure("SpFinLedgerTx",
                                    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "Taxbility", "IsIneligibleforinputcredit" },
                                    new string[] { "0", Ledger_ID, VoucherTx_ID, "Payment", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "0", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "BillByBill", HSN_Code, CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), Isreversechargeapplicable, GSTApplicable, Taxbility, IsIneligibleforinputcredit }, "dataset");
                                    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                                    DataSet dsBillByBillTemp = new DataSet();
                                    dsBillByBillTemp = dsBillByBill;
                                    for (int j = 0; j < dsBillByBillTemp.Tables.Count; j++)
                                    {
                                        if (dsBillByBillTemp.Tables[j].TableName == RowNo.ToString())
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

                                                objdb.ByProcedure("SpFinBillByBillTx",
                                                new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy", "LedgerTx_OrderBy" },
                                                new string[] { "3", VoucherTx_ID, Ledger_ID, BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "0", BillByBillTx_OrderBy.ToString(), RowNo.ToString() }, "dataset");
                                            }
                                        }
                                    }
                                }

                                else if (dt_LedgerTable.Rows[i]["LedgerTx_MaintainType"].ToString() == "Cheque")
                                {
                                    if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                                        LedgerTx_Amount = "-" + LedgerTx_Amount;
                                    }
                                    else
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                                    }

                                    int TableId = int.Parse(dt_LedgerTable.Rows[i]["Ledger_ID"].ToString());

                                    objdb.ByProcedure("SpFinLedgerTx",
                                    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "Taxbility", "IsIneligibleforinputcredit" },
                                    new string[] { "0", Ledger_ID, VoucherTx_ID, "Payment", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "0", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "Cheque", HSN_Code, CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), Isreversechargeapplicable, GSTApplicable, Taxbility, IsIneligibleforinputcredit }, "dataset");

                                    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];

                                    for (int j = 0; j < dsBillByBill.Tables.Count; j++)
                                    {
                                        if (dsBillByBill.Tables[j].TableName == RowNo.ToString())
                                        {
                                            for (int k = 0; k < dsBillByBill.Tables[j].Rows.Count; k++)
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

                                                string IfCheque = dsBillByBill.Tables[j].Rows[k]["IfCheque"].ToString();
                                                string AcPayee = dsBillByBill.Tables[j].Rows[k]["AcPayee"].ToString();
                                                string FavouringName = dsBillByBill.Tables[j].Rows[k]["FavouringName"].ToString();

                                                objdb.ByProcedure("SpFinChequeTx",
                                                new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy", "LedgerTx_OrderBy", "IfCheque", "AcPayee", "FavouringName" },
                                                new string[] { "1", VoucherTx_ID, Ledger_ID, "Payment", ChequeTx_No, ChequeTx_Date, ChequeTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "0", ViewState["Emp_ID"].ToString(), (k + 1).ToString(), RowNo.ToString(), IfCheque, AcPayee, FavouringName }, "dataset");
                                            }
                                        }
                                    }
                                }
                                else if (dt_LedgerTable.Rows[i]["LedgerTx_MaintainType"].ToString() == "GSTTDS")
                                {
                                    if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                                        LedgerTx_Amount = "-" + LedgerTx_Amount;
                                    }
                                    else
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                                    }
                                    objdb.ByProcedure("SpFinLedgerTx",
                                    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "Taxbility", "IsIneligibleforinputcredit" },
                                    new string[] { "0", Ledger_ID, VoucherTx_ID, "Payment", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "GSTTDS", HSN_Code, CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), Isreversechargeapplicable, GSTApplicable, Taxbility, IsIneligibleforinputcredit }, "dataset");
                                }
                                else if (dt_LedgerTable.Rows[i]["LedgerTx_MaintainType"].ToString() == "CostCentre")
                                {
                                    if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                                        LedgerTx_Amount = "-" + LedgerTx_Amount;
                                    }
                                    else
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                                    }
                                    objdb.ByProcedure("SpFinLedgerTx",
                                    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "Taxbility", "IsIneligibleforinputcredit" },
                                    new string[] { "0", Ledger_ID, VoucherTx_ID, "Payment", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "CostCentre", HSN_Code, CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), Isreversechargeapplicable, GSTApplicable, Taxbility, IsIneligibleforinputcredit }, "dataset");
                                }
                                else
                                {
                                    if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                                        LedgerTx_Amount = "-" + LedgerTx_Amount;
                                    }
                                    else
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                                    }
                                    objdb.ByProcedure("SpFinLedgerTx",
                                    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "Taxbility", "IsIneligibleforinputcredit" },
                                    new string[] { "0", Ledger_ID, VoucherTx_ID, "Payment", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "None", HSN_Code, CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), Isreversechargeapplicable, GSTApplicable, Taxbility, IsIneligibleforinputcredit }, "dataset");
                                }
                                objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "GSTVoucher" }, new string[] { "37", VoucherTx_ID, GSTVoucher }, "dataset");

                            }
                            SaveCostCentre(VoucherTx_ID, "Save");
                            SaveTDSGSTData(VoucherTx_ID);
                            objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "40", VoucherTx_ID }, "dataset");
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                            ClearData();
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please Select at least one Bank Accounts/Cash ledger at Credit');", true);
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('if the ledger is Cr then Same ledger should not be come in Dr.');", true);
                    }


                }
                else if (btnAccept.Text == "Update" && ViewState["VoucherTx_ID"].ToString() != "0" && Status == 0)
                {
                    string GSTVoucher = "No";
                    string Validate = VoucherValidate("0");
                    string Accept = VoucherAccept("0");
                    string FinDocs = hpView.NavigateUrl;
                    if (FU_UploadDocs.HasFile)
                    {
                        FinDocs = "Docs/" + Guid.NewGuid() + "-" + FU_UploadDocs.FileName;
                        FU_UploadDocs.PostedFile.SaveAs(Server.MapPath(FinDocs));
                    }
                   
                    if (Validate == "0")
                    {
                        if (Accept == "1")
                        {
                            //     ds = objdb.ByProcedure("SpFinVoucherTx",
                            //new string[] { "flag", "VoucherTx_ID", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy" },
                            //new string[] { "7", ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Payment", "Payment", txtVoucherTx_No.Text, "", txtVoucherTx_Narration.Text, ViewState["LedgerDebitTotal"].ToString(), Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", ViewState["Emp_ID"].ToString() }, "dataset");
                            ds = objdb.ByProcedure("SpFinVoucherTx",
                      new string[] { "flag", "VoucherTx_ID", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy", "GSTVoucher", "UploadDocs" },
                      new string[] { "7", ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Payment", "Payment", VoucherTx_No, "", txtVoucherTx_Narration.Text, ViewState["LedgerDebitTotal"].ToString(), Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", ViewState["Emp_ID"].ToString(), "No", FinDocs }, "dataset");

                            objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                            objdb.ByProcedure("SpFinChequeTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                            objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "4", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                            if (chkbox.Checked == true)
                            {
                                objdb.ByProcedure("SpFinServiceSupplierDetail", new string[] { "flag", "VoucherTx_ID", "SupplierName", "SupplierAddress", "State_ID", "RegistrationTypes", "GST_No", "UpdatedBy" }, new string[] { "4", ViewState["VoucherTx_ID"].ToString(), txtSuplierName.Text, txtsupplieraddress.Text, ddlState.SelectedValue, ddlRegistrationType.SelectedItem.Text, txtGSTNo.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                            }
                            else
                            {
                                objdb.ByProcedure("SpFinServiceSupplierDetail", new string[] { "flag", "VoucherTx_ID", "UpdatedBy" }, new string[] { "5", ViewState["VoucherTx_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                            }
                            DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];


                            for (int i = 0; i < dt_LedgerTable.Rows.Count; i++)
                            {
                                string RowNo = dt_LedgerTable.Rows[i]["RowNo"].ToString();
                                string Ledger_ID = dt_LedgerTable.Rows[i]["Ledger_ID"].ToString();
                                string LedgerTx_Amount = "";
                                ds = objdb.ByProcedure("SpFinLedgerGSTDetails", new string[] { "flag", "Ledger_ID", "VoucherTx_Date" }, new string[] { "3", Ledger_ID.ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                                if (ds != null && ds.Tables[0].Rows.Count > 0)
                                {
                                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "true" && ds.Tables[0].Rows[0]["GSTApplicable"].ToString() == "Yes")
                                    {
                                        GSTVoucher = "Yes";

                                        if (ds.Tables[0].Rows[0]["GSTApplicable"].ToString() != "")
                                        {
                                            GSTApplicable = ds.Tables[0].Rows[0]["GSTApplicable"].ToString();
                                        }
                                        Taxbility = ds.Tables[0].Rows[0]["Taxbility"].ToString();
                                        CGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                                        SGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                                        IGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_IntegratedTax"].ToString());
                                        Isreversechargeapplicable = ds.Tables[0].Rows[0]["Isreversechargeapplicable"].ToString();
                                        if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                                        {
                                            LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();

                                        }
                                        else
                                        {
                                            LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                                        }

                                        DataSet ds1 = objdb.ByProcedure("SpFinVoucherSaleCredit", new string[] { "flag", "Ledger_ID" }, new string[] { "3", Ledger_ID.ToString() }, "dataset");
                                        {
                                            if (ds1 != null && ds.Tables[0].Rows.Count > 0)
                                            {
                                                if (ds1.Tables[0].Rows[0]["status"].ToString() == "true")
                                                {
                                                    CGST = 0;
                                                    SGST = 0;

                                                }
                                                else
                                                {
                                                    IGST = 0;

                                                }
                                            }
                                        }


                                        decimal Amount = decimal.Parse(LedgerTx_Amount);
                                        CGSTAmt = Math.Round((Amount * CGST) / 100, 2);
                                        SGSTAmt = Math.Round((Amount * SGST) / 100, 2);
                                        IGSTAmt = 0;
                                        HSN_Code = ds.Tables[0].Rows[0]["HSN_Code"].ToString();
                                    }
                                    else
                                    {
                                        CGST = 0;
                                        SGST = 0;
                                        IGST = 0;
                                        CGSTAmt = 0;
                                        SGSTAmt = 0;
                                        IGSTAmt = 0;
                                        HSN_Code = "";
                                        Isreversechargeapplicable = "";
                                        GSTApplicable = "No";
                                        Taxbility = "";
                                        IsIneligibleforinputcredit = "";
                                        objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "GSTVoucher" }, new string[] { "37", ViewState["VoucherTx_ID"].ToString(), "No" }, "dataset");

                                    }
                                }


                                if (dt_LedgerTable.Rows[i]["LedgerTx_MaintainType"].ToString() == "Cheque")
                                {
                                    if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                                        LedgerTx_Amount = "-" + LedgerTx_Amount;
                                    }
                                    else
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                                    }
                                    int TableId = int.Parse(dt_LedgerTable.Rows[i]["Ledger_ID"].ToString());

                                    objdb.ByProcedure("SpFinLedgerTx",
                                    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "Taxbility", "IsIneligibleforinputcredit" },
                                    new string[] { "0", Ledger_ID, ViewState["VoucherTx_ID"].ToString(), "Payment", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "Cheque", HSN_Code, CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), Isreversechargeapplicable, GSTApplicable, Taxbility, IsIneligibleforinputcredit }, "dataset");

                                    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];

                                    for (int j = 0; j < dsBillByBill.Tables.Count; j++)
                                    {
                                        if (dsBillByBill.Tables[j].TableName == RowNo.ToString())
                                        {
                                            for (int k = 0; k < dsBillByBill.Tables[j].Rows.Count; k++)
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

                                                string IfCheque = dsBillByBill.Tables[j].Rows[k]["IfCheque"].ToString();
                                                string AcPayee = dsBillByBill.Tables[j].Rows[k]["AcPayee"].ToString();
                                                string FavouringName = dsBillByBill.Tables[j].Rows[k]["FavouringName"].ToString();

                                                objdb.ByProcedure("SpFinChequeTx",
                                                new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy", "LedgerTx_OrderBy", "IfCheque", "AcPayee", "FavouringName" },
                                                new string[] { "1", ViewState["VoucherTx_ID"].ToString(), Ledger_ID, "Payment", ChequeTx_No, ChequeTx_Date, ChequeTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), (k + 1).ToString(), RowNo.ToString(), IfCheque, AcPayee, FavouringName }, "dataset");
                                            }
                                        }
                                    }
                                }
                                else if (dt_LedgerTable.Rows[i]["LedgerTx_MaintainType"].ToString() == "BillByBill")
                                {
                                    if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                                        LedgerTx_Amount = "-" + LedgerTx_Amount;
                                    }
                                    else
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                                    }

                                    int TableId = int.Parse(dt_LedgerTable.Rows[i]["Ledger_ID"].ToString());

                                    objdb.ByProcedure("SpFinLedgerTx",
                                    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "Taxbility", "IsIneligibleforinputcredit" },
                                    new string[] { "0", Ledger_ID, ViewState["VoucherTx_ID"].ToString(), "Payment", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "BillByBill", HSN_Code, CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), Isreversechargeapplicable, GSTApplicable, Taxbility, IsIneligibleforinputcredit }, "dataset");
                                    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                                    DataSet dsBillByBillTemp = new DataSet();
                                    dsBillByBillTemp = dsBillByBill;
                                    for (int j = 0; j < dsBillByBillTemp.Tables.Count; j++)
                                    {
                                        if (dsBillByBillTemp.Tables[j].TableName == RowNo.ToString())
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

                                                objdb.ByProcedure("SpFinBillByBillTx",
                                                new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy", "LedgerTx_OrderBy" },
                                                new string[] { "3", ViewState["VoucherTx_ID"].ToString(), Ledger_ID, BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", BillByBillTx_OrderBy.ToString(), RowNo.ToString() }, "dataset");
                                            }
                                        }
                                    }
                                }
                                else if (dt_LedgerTable.Rows[i]["LedgerTx_MaintainType"].ToString() == "GSTTDS")
                                {
                                    if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                                        LedgerTx_Amount = "-" + LedgerTx_Amount;
                                    }
                                    else
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                                    }
                                    objdb.ByProcedure("SpFinLedgerTx",
                                    new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "Taxbility", "IsIneligibleforinputcredit" },
                                    new string[] { "0", Ledger_ID, ViewState["VoucherTx_ID"].ToString(), "Payment", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "GSTTDS", HSN_Code, CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), Isreversechargeapplicable, GSTApplicable, Taxbility, IsIneligibleforinputcredit }, "dataset");
                                }
                                else if (dt_LedgerTable.Rows[i]["LedgerTx_MaintainType"].ToString() == "CostCentre")
                                {
                                    if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                                        LedgerTx_Amount = "-" + LedgerTx_Amount;
                                    }
                                    else
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                                    }
                                    objdb.ByProcedure("SpFinLedgerTx",
                                   new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "Taxbility", "IsIneligibleforinputcredit" },
                                   new string[] { "0", Ledger_ID, ViewState["VoucherTx_ID"].ToString(), "Payment", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "CostCentre", HSN_Code, CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), Isreversechargeapplicable, GSTApplicable, Taxbility, IsIneligibleforinputcredit }, "dataset");
                                }
                                else
                                {
                                    if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                                        LedgerTx_Amount = "-" + LedgerTx_Amount;
                                    }
                                    else
                                    {
                                        LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                                    }
                                    objdb.ByProcedure("SpFinLedgerTx",
                                   new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "Taxbility", "IsIneligibleforinputcredit" },
                                   new string[] { "0", Ledger_ID, ViewState["VoucherTx_ID"].ToString(), "Payment", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "None", HSN_Code, CGST.ToString(), SGST.ToString(), IGST.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), Isreversechargeapplicable, GSTApplicable, Taxbility, IsIneligibleforinputcredit }, "dataset");
                                }

                                objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "GSTVoucher" }, new string[] { "37", ViewState["VoucherTx_ID"].ToString(), GSTVoucher }, "dataset");
                            }
                            SaveCostCentre(ViewState["VoucherTx_ID"].ToString(), "Edit");
                            SaveTDSGSTData(ViewState["VoucherTx_ID"].ToString());
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please Select at least one Bank Accounts/Cash ledger at Credit');", true);
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('if the ledger is Cr then Same ledger should not be come in Dr.');", true);
                    }
                  
                    //ClearData();
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
    protected void SaveCostCentre(string VoucherTx_ID, string ActionType)
    {
        objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID" },
               new string[] { "3", VoucherTx_ID }, "dataset");
        DataSet dsCostCentre = (DataSet)ViewState["dsCostCentre"];
        DataSet dsCostCentreTmp = new DataSet();
        dsCostCentreTmp = dsCostCentre;
        DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
        for (int i = 0; i < dt_LedgerTable.Rows.Count; i++)
        {
            string RowNo = dt_LedgerTable.Rows[i]["RowNo"].ToString();
            string Ledger_ID = dt_LedgerTable.Rows[i]["Ledger_ID"].ToString();
            for (int j = 0; j < dsCostCentreTmp.Tables.Count; j++)
            {
                if (dsCostCentreTmp.Tables[j].TableName == RowNo.ToString())
                {
                    for (int k = 0; k < dsCostCentreTmp.Tables[j].Rows.Count; k++)
                    {
                        string Category_ID = dsCostCentreTmp.Tables[j].Rows[k]["Category_ID"].ToString();
                        string SubCategory_ID = dsCostCentreTmp.Tables[j].Rows[k]["SubCategory_ID"].ToString();
                        string Amount = dsCostCentreTmp.Tables[j].Rows[k]["Amount"].ToString();
                        objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "Voucher_Date", "Office_ID", "Category_ID", "SubCategory_ID", "Amount", "CostCentre_type", "LedgerTx_OrderBy", "UpdatedBy" },
           new string[] { "1", VoucherTx_ID, Ledger_ID, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), Category_ID, SubCategory_ID, Amount, "Payment", RowNo, ViewState["Emp_ID"].ToString() }, "dataset");
                    }
                }
            }
        }
        if (ActionType == "Save")
        {
            CreateCostCentreDataSet();
        }
    }
    protected string VoucherAccept(string ID)
    {
        string Accept = "0";
        try
        {
            DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
            for (int i = 0; i < dt_LedgerTable.Rows.Count; i++)
            {
                string Ledger_ID = dt_LedgerTable.Rows[i]["Ledger_ID"].ToString();

                string Type = dt_LedgerTable.Rows[i]["Type"].ToString();
                if (Type.ToString() == "Cr")
                {
                    ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID" }, new string[] { "16", Ledger_ID }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        Accept = "1";

                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        return Accept.ToString();
    }
    protected string VoucherValidate(string ID)
    {
        string Accept = "0";
        try
        {
			if(ViewState["Office_ID"].ToString() == "1")
            {

            }
			else
			{
				DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
            for (int i = 0; i < dt_LedgerTable.Rows.Count; i++)
            {
				//Accept = "0";
                string Ledger_ID = dt_LedgerTable.Rows[i]["Ledger_ID"].ToString();
                string Type = dt_LedgerTable.Rows[i]["Type"].ToString();
                if(Type == "Cr")
                {
                    Type = "Dr";
                }
				else
				{
					Type = "Cr";
				}
                DataTable dt = new DataTable();
                dt_LedgerTable.DefaultView.RowFilter = "Ledger_ID = '" + Ledger_ID + "' AND Type='" + Type + "'";
                dt = dt_LedgerTable.DefaultView.ToTable();
                if (dt.Rows.Count > 0)
                {
                    Accept = "1";
					break;
                }
               
            }
			}
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        return Accept.ToString();
    }
    protected void ClearData()
    {
        try
        {
            CreateTDSGSTTable();
            txtVoucherTx_No.Text = "";
            ddlLedger_ID.ClearSelection();
            txtVoucherTx_Narration.Text = "";
            GridViewLedgerDetail.DataSource = new string[] { };
            GridViewLedgerDetail.DataBind();
            ddlcreditdebit.Enabled = false;
            ddlcreditdebit.SelectedValue = "Dr";
            CreateLedgerTable();
            ddlLedger_ID.ClearSelection();
            txtCurrentBalance.Text = "";
            txtLedgerTx_Amount.Text = "";
            btnAccept.Enabled = false;
            FillVoucherNo();
            GetPreviousVoucherNo();
            FillParticularsDropDown();
            CreateBillByBillDataSet();
            chkbox.Checked = false;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnNarration_Click(object sender, EventArgs e)
    {
        ds = objdb.ByProcedure("SpFinVoucherTx",
                new string[] { "flag", "VoucherTx_Type", "Office_ID" },
                new string[] { "14", "Payment", ViewState["Office_ID"].ToString() }, "dataset");

        if (ds.Tables[0].Rows.Count != 0)
        {
            txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
        }
    }

    //View RefDetail
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


    //Add New Ledger
    protected void lbkbtnAddLedger_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("LedgerMasterB.aspx");
			//if (Session["Office_ID"].ToString() == "1")
			//{
			//    Response.Redirect("LedgerMasterB.aspx");
			//}
			//else
			//{
			//    Response.Redirect("LedgerMaster_Forotherofc.aspx");
			//}
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
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "Office_ID" }, new string[] { "11", ViewState["VoucherTx_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hpView.Visible = true;
                    string Fstring = "";
                    string Lstring = "";
                    string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
                    if (ds.Tables[0].Rows[0]["UploadDocs"].ToString() != "")
                    {
                        hpView.NavigateUrl = ds.Tables[0].Rows[0]["UploadDocs"].ToString();
                        hpView.Text = "View";
                    }
                    else
                    {
                        hpView.NavigateUrl = "";
                        hpView.Text = "NA";
                    }
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


                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    GridViewLedgerDetail.DataSource = ds.Tables[1];
                    GridViewLedgerDetail.DataBind();
                    decimal LedgerCreditTotal = 0;
                    decimal LedgerDebitTotal = 0;
                    LedgerCreditTotal = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
                    LedgerDebitTotal = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));
                    GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>Total : </b>";
                    GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
                    GridViewLedgerDetail.FooterRow.Cells[6].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";
                    GridViewLedgerDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    GridViewLedgerDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                    ViewState["LedgerCreditTotal"] = LedgerCreditTotal;
                    ViewState["LedgerDebitTotal"] = LedgerDebitTotal;

                    if (LedgerCreditTotal == LedgerDebitTotal && LedgerDebitTotal != 0)
                    {
                        btnAccept.Enabled = true;
                        FillNarration();
                    }
                    else
                    {
                        btnAccept.Enabled = false;
                    }
                    DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
                    //GridViewLedgerDetail.DataSource = ds.Tables[1];
                    //GridViewLedgerDetail.DataBind();
                    int gridRows = GridViewLedgerDetail.Rows.Count;
                    for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                    {
                        Label lblRowNumber = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("lblRowNumber");
                        Label Ledger_ID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_ID");
                        Label Type = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Type");
                        Label lblMaintainType = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("lblMaintainType");
                        Label Ledger_Name = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_Name");
                        Label LedgerTx_Credit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Credit");
                        Label LedgerTx_Debit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Debit");
                        //Label Ledger_TableID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_TableID");

                        //dt_LedgerTable.Rows.Add(lblRowNumber.Text, Ledger_ID.Text, Ledger_Name.Text, Type.Text, lblMaintainType.Text, LedgerTx_Credit.Text, LedgerTx_Debit.Text);
                        if (Ledger_ID.Text == "6")
                        {
                            dt_LedgerTable.Rows.Add(lblRowNumber.Text, Ledger_ID.Text, Ledger_Name.Text, Type.Text, "GSTTDS", LedgerTx_Credit.Text, LedgerTx_Debit.Text);
                        }
                        else
                        {
                            dt_LedgerTable.Rows.Add(lblRowNumber.Text, Ledger_ID.Text, Ledger_Name.Text, Type.Text, lblMaintainType.Text, LedgerTx_Credit.Text, LedgerTx_Debit.Text);
                        }

                    }

                    ViewState["LedgerTable"] = dt_LedgerTable;
                    //GridViewLedgerDetail.DataSource = dt_LedgerTable;
                    //GridViewLedgerDetail.DataBind();
                    //foreach (GridViewRow row in GridViewLedgerDetail.Rows)
                    //{
                    //    Label Ledger_ID = (Label)row.FindControl("Ledger_ID");
                    //    LinkButton lnkbtnEdit = (LinkButton)row.FindControl("lnkbtnEdit");
                    //    if (Ledger_ID.Text == "813")
                    //    {
                    //        lnkbtnEdit.Visible = false;
                    //    }
                    //    else
                    //    {
                    //        lnkbtnEdit.Visible = true;
                    //    }
                    //}

                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                    int rowcount = ds.Tables[2].Rows.Count;
                    for (int i = 0; i < rowcount; i++)
                    {
                        string Ledger_ID = ds.Tables[2].Rows[i]["Ledger_ID"].ToString();
                        string ChequeTx_No = ds.Tables[2].Rows[i]["ChequeTx_No"].ToString();
                        string ChequeTx_Date = ds.Tables[2].Rows[i]["ChequeTx_Date"].ToString();
                        string ChequeTx_Amount = ds.Tables[2].Rows[i]["ChequeTx_Amount"].ToString();
                        string IfCheque = ds.Tables[2].Rows[i]["IfCheque"].ToString();
                        string AcPayee = ds.Tables[2].Rows[i]["AcPayee"].ToString();
                        string FavouringName = ds.Tables[2].Rows[i]["FavouringName"].ToString();

                        string TNO = ds.Tables[2].Rows[i]["LedgerTx_OrderBy"].ToString();

                        DataTable dt_FinChequeTx = new DataTable(TNO);
                        dt_FinChequeTx.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
                        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_No", typeof(string)));
                        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Date", typeof(string)));
                        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Amount", typeof(decimal)));
                        dt_FinChequeTx.Columns.Add(new DataColumn("IfCheque", typeof(string)));
                        dt_FinChequeTx.Columns.Add(new DataColumn("AcPayee", typeof(string)));
                        dt_FinChequeTx.Columns.Add(new DataColumn("FavouringName", typeof(string)));

                        dt_FinChequeTx.Rows.Add(Ledger_ID, ChequeTx_No, ChequeTx_Date, ChequeTx_Amount, IfCheque, AcPayee, FavouringName);

                        dsBillByBill.Merge(dt_FinChequeTx);
                        ViewState["FinChequeTx"] = dt_FinChequeTx;
                        ViewState["dsBillByBill"] = dsBillByBill;

                    }
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];

                    int Legrowcount = ds.Tables[1].Rows.Count;
                    for (int Leg = 0; Leg < Legrowcount; Leg++)
                    {
                        string TNO = ds.Tables[1].Rows[Leg]["RowNo"].ToString();
                        if (ds.Tables[1].Rows[Leg]["LedgerTx_MaintainType"].ToString() == "BillByBill")
                        {

                            DataTable dt_BillByBillTable = new DataTable(TNO);
                            DataColumn RowNo = dt_BillByBillTable.Columns.Add("RowNo", typeof(int));
                            dt_BillByBillTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
                            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_RefType", typeof(string)));
                            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Ref", typeof(string)));
                            dt_BillByBillTable.Columns.Add(new DataColumn("BillByBillTx_Amount", typeof(decimal)));
                            dt_BillByBillTable.Columns.Add(new DataColumn("Type", typeof(string)));

                            RowNo.AutoIncrement = true;
                            RowNo.AutoIncrementSeed = 1;
                            RowNo.AutoIncrementStep = 1;

                            int rowscount = ds.Tables[3].Rows.Count;
                            for (int i = 0; i < rowscount; i++)
                            {
                                if (TNO == ds.Tables[3].Rows[i]["LedgerTx_OrderBy"].ToString())
                                {
                                    string Ledger_ID = ds.Tables[3].Rows[i]["Ledger_ID"].ToString();
                                    string BillByBillTx_RefType = ds.Tables[3].Rows[i]["BillByBillTx_RefType"].ToString();
                                    string BillByBillTx_Ref = ds.Tables[3].Rows[i]["BillByBillTx_Ref"].ToString();
                                    string BillByBillTx_Amount = ds.Tables[3].Rows[i]["BillByBillTx_Amount"].ToString();
                                    string Type = ds.Tables[3].Rows[i]["BillByBillTxType"].ToString();

                                    //string TNO = ds.Tables[3].Rows[i]["LedgerTx_OrderBy"].ToString();

                                    dt_BillByBillTable.Rows.Add(null, Ledger_ID, BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount, Type);

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
                    chkbox.Checked = true;
                    txtSuplierName.Text = ds.Tables[6].Rows[0]["SupplierName"].ToString();
                    txtsupplieraddress.Text = ds.Tables[6].Rows[0]["SupplierAddress"].ToString();
                    ddlState.ClearSelection();
                    ddlState.Items.FindByValue(ds.Tables[6].Rows[0]["State_ID"].ToString()).Selected = true;
                    ddlRegistrationType.ClearSelection();
                    ddlRegistrationType.Items.FindByText(ds.Tables[6].Rows[0]["RegistrationTypes"].ToString()).Selected = true;
                    txtGSTNo.Text = ds.Tables[6].Rows[0]["GST_No"].ToString();

                }

                ds = objdb.ByProcedure("SpFinTDSGSTEntry", new string[] { "flag", "Voucher_Id", "Office_ID" }, new string[] { "7", ViewState["VoucherTx_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    CreateTDSGSTTable();
                    DataTable dt_TdsGSTTable = (DataTable)ViewState["TdsGSTTable"];
                    dt_TdsGSTTable = ds.Tables[0];
                    ViewState["TdsGSTTable"] = dt_TdsGSTTable;
                }
                ds = objdb.ByProcedure("SpFinCostCentretx", new string[] { "flag", "VoucherTx_ID" },
             new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataSet dsCostCentre = (DataSet)ViewState["dsCostCentre"];
                    int rowscount = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < rowscount; i++)
                    {
                        string Ledger_ID = ds.Tables[0].Rows[i]["Ledger_ID"].ToString();
                        string Category_ID = ds.Tables[0].Rows[i]["Category_ID"].ToString();
                        string CategoryName = ds.Tables[0].Rows[i]["CategoryName"].ToString();
                        string SubCategory_ID = ds.Tables[0].Rows[i]["SubCategory_ID"].ToString();
                        string SubCategoryName = ds.Tables[0].Rows[i]["SubCategoryName"].ToString();
                        string AmountShow = ds.Tables[0].Rows[i]["AmountShow"].ToString();
                        string Amount = ds.Tables[0].Rows[i]["Amount"].ToString();
                        string CNO = ds.Tables[0].Rows[i]["LedgerTx_OrderBy"].ToString();
                        string RowNo = ds.Tables[0].Rows[i]["RowNo"].ToString();
                        DataTable dt_CostCentreTable = new DataTable(CNO);
                        //DataColumn RowNo = dt_CostCentreTable.Columns.Add("RowNo", typeof(string));
                        dt_CostCentreTable.Columns.Add(new DataColumn("RowNo", typeof(string)));
                        dt_CostCentreTable.Columns.Add(new DataColumn("Ledger_ID", typeof(decimal)));
                        dt_CostCentreTable.Columns.Add(new DataColumn("Category_ID", typeof(string)));
                        dt_CostCentreTable.Columns.Add(new DataColumn("CategoryName", typeof(string)));
                        dt_CostCentreTable.Columns.Add(new DataColumn("SubCategory_ID", typeof(string)));
                        dt_CostCentreTable.Columns.Add(new DataColumn("SubCategoryName", typeof(string)));
                        dt_CostCentreTable.Columns.Add(new DataColumn("AmountShow", typeof(decimal)));
                        dt_CostCentreTable.Columns.Add(new DataColumn("Amount", typeof(decimal)));
                        //RowNo.AutoIncrement = true;
                        //RowNo.AutoIncrementSeed = 1;
                        //RowNo.AutoIncrementStep = 1;

                        dt_CostCentreTable.Rows.Add(RowNo, Ledger_ID, Category_ID, CategoryName, SubCategory_ID, SubCategoryName, AmountShow, Amount);
                        dsCostCentre.Merge(dt_CostCentreTable);
                        ViewState["dt_CostCentreTable"] = dt_CostCentreTable;
                        ViewState["dsCostCentre"] = dsCostCentre;
                    }
                    //GridCostCentreViewDetail.DataSource = dt_CostCentreTable;
                    //GridCostCentreViewDetail.DataBind();
                }
                btnAccept.Text = "Update";
                btnClear.Visible = false;

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Fill Narration
    protected void FillNarration()
    {
        try
        {
            string Narration = "";
            DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
            int Count = dt_LedgerTable.Rows.Count;
            for (int i = 0; i < Count; i++)
            {
                decimal LedgerTx_Credit = decimal.Parse(dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString());
                decimal LedgerTx_Debit = decimal.Parse(dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString());
                string RowNo = dt_LedgerTable.Rows[i]["RowNo"].ToString();
                string Ledger_ID = dt_LedgerTable.Rows[i]["Ledger_ID"].ToString();
                string LedgerName = dt_LedgerTable.Rows[i]["Ledger_Name"].ToString();
                string[] LName = LedgerName.Split(new char[]{'&'});
                string LedgerTx_MaintainType = dt_LedgerTable.Rows[i]["LedgerTx_MaintainType"].ToString();
                decimal LedgerAmount = (LedgerTx_Credit + LedgerTx_Debit);
                Narration =  Narration + "\r\n" + LName[0]  + " Amount:" + LedgerAmount.ToString() + "\r\n";
                if(LedgerTx_MaintainType== "CostCentre")
                {
                    DataSet dsCostCentre = (DataSet)ViewState["dsCostCentre"];
                    int tcount = dsCostCentre.Tables[RowNo].Rows.Count;
                    for (int j = 0; j < tcount; j++)
                    {
                        string Category = dsCostCentre.Tables[RowNo].Rows[j]["CategoryName"].ToString();
                        string SubCateogry = dsCostCentre.Tables[RowNo].Rows[j]["SubCategoryName"].ToString();
                        string Amount = dsCostCentre.Tables[RowNo].Rows[j]["AmountShow"].ToString();
                        //Narration = Narration + "Category:" + Category + "  " + "SubCateogry:" + SubCateogry + "  " + "Amount:" + Amount + "\r\n";
                        Narration = Narration + (j+1) + " " + Category + "  " + SubCateogry + "  " + "Amount : " + Amount + "\r\n";
                    }
                }
                else if (LedgerTx_MaintainType == "Cheque")
                {
                      DataSet dsBillByBill = (DataSet)ViewState["dsBillByBill"];
                      for (int k = 0; k < dsBillByBill.Tables[RowNo].Rows.Count; k++)
                   {
                       string ChequeTx_No = dsBillByBill.Tables[RowNo].Rows[k]["ChequeTx_No"].ToString();
                       if (ChequeTx_No == "")
                       {
                           ChequeTx_No = null;
                       }
                       else
                       {

                       }
                       string ChequeTx_Date = dsBillByBill.Tables[RowNo].Rows[k]["ChequeTx_Date"].ToString();
                       if (ChequeTx_Date == "")
                       {
                           ChequeTx_Date = null;
                       }
                       else
                       {
                           ChequeTx_Date = Convert.ToDateTime(ChequeTx_Date, cult).ToString("yyyy/MM/dd");
                       }
                       string ChequeTx_Amount = dsBillByBill.Tables[RowNo].Rows[k]["ChequeTx_Amount"].ToString();
                       Narration = Narration + ChequeTx_No + "  " + ChequeTx_Date + "  " + "Amount : " + ChequeTx_Amount + "\r\n";
                   }
                }
                txtVoucherTx_Narration.Text = Narration;
            }
            //DataSet dsCostCentre = (DataSet)ViewState["dsCostCentre"];
            //if (dsCostCentre != null)
            //{
            //    int count = dsCostCentre.Tables.Count;
            //    string Narration = "";
            //    if (count > 0)
            //    {
            //        for (int i = 0; i < count; i++)
            //        {
            //            int tcount = dsCostCentre.Tables[i].Rows.Count;
            //            for (int j = 0; j < tcount; j++)
            //            {
            //                string Category = dsCostCentre.Tables[i].Rows[j]["CategoryName"].ToString();
            //                string SubCateogry = dsCostCentre.Tables[i].Rows[j]["SubCategoryName"].ToString();
            //                string Amount = dsCostCentre.Tables[i].Rows[j]["AmountShow"].ToString();
            //                //Narration = Narration + "Category:" + Category + "  " + "SubCateogry:" + SubCateogry + "  " + "Amount:" + Amount + "\r\n";
            //                Narration = Narration + Category + "  " + SubCateogry + "  " + "Amount : " + Amount + "\r\n";
            //            }

            //        }
            //    }
            //    txtVoucherTx_Narration.Text = Narration;
            //}
            //else
            //{
            //    txtVoucherTx_Narration.Text = "";
            //}



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
            ddlcreditdebit.Visible = false;
            txtLedgerTx_Amount.Visible = false;
            divparticular.Visible = false;
            txtVoucherTx_Narration.Attributes.Add("readonly", "readonly");
            txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
            txtVoucherTx_No.Attributes.Add("readonly", "readonly");
            lbkbtnAddLedger.Visible = false;
            GridViewLedgerDetail.Columns[7].Visible = true;
            GridViewLedgerDetail.Columns[8].Visible = false;
            chkbox.Enabled = false;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //change voucher series according to FY
    protected void txtVoucherTx_Date_TextChanged(object sender, EventArgs e)
    {
         FillVoucherNo();
        // string ValidStatus = ValidDate();
        // if (ValidStatus == "No")
        // {

            // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('You are not allowed to choose this date, please contact to head office.');", true);
            // FillVoucherDate();
        // }
        // else
        // {
            // FillVoucherNo();
        // }
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
            if (ViewState["Office_ID"].ToString() == "1")
            {
                VoucherTx_Type = "Payment";
                DataSet ds = objdb.ByProcedure("SpFinVoucherTx",
                                new string[] { "flag", "Office_ID", "VoucherTx_FY", "VoucherTx_Type" },
                                new string[] { "39", ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_Type }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    lblPreviousVoucherNo.Text = "(Previous VoucherNo :" + " " + ds.Tables[0].Rows[0]["VoucherTx_No"].ToString() + ")";
                }
            }
            else
            {
                DataSet ds = objdb.ByProcedure("SpFinVoucherTx",
                new string[] { "flag", "Office_ID", "VoucherTx_FY", "VoucherTx_Type" },
                new string[] { "39", ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_Type }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    lblPreviousVoucherNo.Text = "(Previous VoucherNo :" + " " + ds.Tables[0].Rows[0]["VoucherTx_No"].ToString() + ")";
                }
            }
            // string VoucherTx_Type = "Payment,Journal,Contra,GSTService Purchase,Goods Transfer Inward,Goods Transfer Outward";
            // DataSet ds = objdb.ByProcedure("SpFinVoucherTx",
                // new string[] { "flag", "Office_ID", "VoucherTx_FY", "VoucherTx_Type" },
                // new string[] { "39", ViewState["Office_ID"].ToString(), FinancialYear.ToString(), VoucherTx_Type }, "dataset");
            // //ds = objdb.ByProcedure("", new string[] { }, new string[] { }, "dataset");
            // if (ds != null && ds.Tables[0].Rows.Count > 0)
            // {
                // lblPreviousVoucherNo.Text = "(Previous VoucherNo :" + " " + ds.Tables[0].Rows[0]["VoucherTx_No"].ToString() + ")";
            // }

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
        decimal Status = decimal.Parse(ViewState["LedgerAmount"].ToString()) - decimal.Parse(LedgerAmount.ToString());
        if (Status == 0)
        {
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
                decimal LedgerTx_Credit;
                decimal LedgerTx_Debit;
                if (ddlcreditdebit.SelectedItem.Text == "Credit")
                {
                    LedgerTx_Credit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
                    LedgerTx_Debit = 0;
                }
                else
                {
                    LedgerTx_Credit = 0;
                    LedgerTx_Debit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
                }
                string Ledger_Name = ddlLedger_ID.SelectedItem.Text + "&nbsp;&nbsp;<b>(Cur Bal: " + txtCurrentBalance.Text + ")</b>";
                dt_LedgerTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), Ledger_Name, ddlcreditdebit.SelectedValue.ToString(), "BillByBill", LedgerTx_Credit, LedgerTx_Debit);

                GridViewLedgerDetail.DataSource = dt_LedgerTable;
                GridViewLedgerDetail.DataBind();

                decimal LedgerCreditTotal = 0;
                decimal LedgerDebitTotal = 0;

                LedgerCreditTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
                LedgerDebitTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));

                GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>Total : </b>";
                GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
                GridViewLedgerDetail.FooterRow.Cells[6].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";
                GridViewLedgerDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                GridViewLedgerDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                ViewState["LedgerCreditTotal"] = LedgerCreditTotal;
                ViewState["LedgerDebitTotal"] = LedgerDebitTotal;

                ViewState["LedgerTable"] = dt_LedgerTable;
                if (LedgerCreditTotal == LedgerDebitTotal && LedgerDebitTotal != 0)
                {
                    btnAccept.Enabled = true;
                    FillNarration();
                }
                else
                {
                    btnAccept.Enabled = false;
                }
                ClearBillByBillModal();


                //LedgerDebitTotal = decimal.Parse("-" + LedgerDebitTotal);
                decimal ReaminingBal = LedgerDebitTotal - LedgerCreditTotal;
                if (ReaminingBal.ToString().Contains("-"))
                {
                    ReaminingBal = decimal.Parse(ReaminingBal.ToString().Replace(@"-", string.Empty));
                    txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                    ddlcreditdebit.SelectedValue = "Dr";
                }
                else
                {
                    txtLedgerTx_Amount.Text = ReaminingBal.ToString();
                    ddlcreditdebit.SelectedValue = "Cr";

                }
                txtCurrentBalance.Text = "";

                ddlcreditdebit.Enabled = true;

            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);
            txtBillByBillTx_Ref.Visible = true;
            txtBillByBillTx_Ref.Text = lblVoucherNo.Text + txtVoucherTx_No.Text;
            if (Status.ToString().Contains("-"))
            {

                txtBillByBillTx_Amount.Text = Status.ToString();
                txtBillByBillTx_Amount.Text = txtBillByBillTx_Amount.Text.Replace(@"-", string.Empty);

            }
            else
            {
                txtBillByBillTx_Amount.Text = Status.ToString();
            }

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
    protected void CreateTDSGSTTable()
    {
        ViewState["TdsGSTTable"] = "";
        DataTable dt_TdsGSTTable = new DataTable();
        //,,,,,,,,,,,
        DataColumn RowNo = dt_TdsGSTTable.Columns.Add("RowNo", typeof(int));
        dt_TdsGSTTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
        dt_TdsGSTTable.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
        dt_TdsGSTTable.Columns.Add(new DataColumn("GSTNo", typeof(string)));
        dt_TdsGSTTable.Columns.Add(new DataColumn("BillNo", typeof(string)));
        dt_TdsGSTTable.Columns.Add(new DataColumn("BillDate", typeof(string)));
        dt_TdsGSTTable.Columns.Add(new DataColumn("TotalBillAmount", typeof(decimal)));
        dt_TdsGSTTable.Columns.Add(new DataColumn("PaymentAmount", typeof(decimal)));
        dt_TdsGSTTable.Columns.Add(new DataColumn("BasicAmount", typeof(decimal)));
        dt_TdsGSTTable.Columns.Add(new DataColumn("GSTTDSAmount", typeof(decimal)));
        dt_TdsGSTTable.Columns.Add(new DataColumn("CGST", typeof(decimal)));
        dt_TdsGSTTable.Columns.Add(new DataColumn("SGST", typeof(decimal)));
        dt_TdsGSTTable.Columns.Add(new DataColumn("IGST", typeof(decimal)));
        //dt_LedgerTable.Columns.Add(new DataColumn("Ledger_TableID", typeof(decimal)));
        RowNo.AutoIncrement = true;
        RowNo.AutoIncrementSeed = 1;
        RowNo.AutoIncrementStep = 1;
        ViewState["TdsGSTTable"] = dt_TdsGSTTable;


        gvTdsGSTDetail.DataSource = dt_TdsGSTTable;
        gvTdsGSTDetail.DataBind();
    }
    protected void btnTDSSave_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            lblGSTModal.Text = "";
            DataTable dt_TdsGSTTable = (DataTable)ViewState["TdsGSTTable"];
            if (ddlPartyName.SelectedIndex == 0)
            {
                msg += "Select Party Name. \\n";
            }
            if (txtBillNo.Text == "")
            {
                msg += "Enter Bill NO. \\n";
            }
            if (txtBillDate.Text == "")
            {
                msg += "Select Bill Date. \\n";
            }
            if (txtTdsBillAmount.Text == "")
            {
                msg += "Enter Bill Amount. \\n";
            }
            if (txtPaymentAmountNTBD.Text == "")
            {
                msg += "Enter Payment Amount. \\n";
            }
            if (txtBasicAmount.Text == "")
            {
                msg += "Enter Basic Amount. \\n";
            }
            if (txtGSTTDSAmount.Text == "")
            {
                msg += "Enter GST TDS Amt. \\n";
            }
            if (msg == "")
            {
                //int status = 0;
                //foreach (GridViewRow row in gvTdsGSTDetail.Rows)
                //{
                //    Label lblLedger_ID = (Label)row.FindControl("lblLedger_ID");
                //    if (lblLedger_ID.Text == ddlPartyName.SelectedValue.ToString())
                //    {
                //        status = 1;
                //    }
                //}
                //if (status == 0)
                //{
                dt_TdsGSTTable.Rows.Add(null, ddlPartyName.SelectedValue.ToString(), ddlPartyName.SelectedItem.Text, txtTdsGSTNo.Text, txtBillNo.Text, Convert.ToDateTime(txtBillDate.Text, cult).ToString("dd/MM/yyyy"), Convert.ToDecimal(txtTdsBillAmount.Text).ToString("0.00"), Convert.ToDecimal(txtPaymentAmountNTBD.Text).ToString("0.00"), Convert.ToDecimal(txtBasicAmount.Text).ToString("0.00"), Convert.ToDecimal(txtGSTTDSAmount.Text).ToString("0.00"), Convert.ToDecimal(txtCGST.Text).ToString("0.00"), Convert.ToDecimal(txtSGST.Text).ToString("0.00"), Convert.ToDecimal(txtIGST.Text).ToString("0.00"));

                ViewState["TdsGSTTable"] = dt_TdsGSTTable;

                gvTdsGSTDetail.DataSource = dt_TdsGSTTable;
                gvTdsGSTDetail.DataBind();
                ClearTDSGST();
                //}
                //else
                //{
                //    lblGSTModal.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Party Name is already exists.");
                //}
                if (gvTdsGSTDetail.Rows.Count > 0)
                {
                    decimal GSTTDSAmount = 0;
                    foreach (GridViewRow rows in gvTdsGSTDetail.Rows)
                    {
                        Label lblGSTTDSAmount = (Label)rows.FindControl("lblGSTTDSAmount");
                        GSTTDSAmount += decimal.Parse(lblGSTTDSAmount.Text);

                    }
                    if (decimal.Parse(GSTTDSAmount.ToString()) == decimal.Parse(txtLedgerTx_Amount.Text))
                    {
                        btnFSubmit.Visible = true;
                    }
                    else
                    {
                        btnFSubmit.Visible = false;
                    }
                    //btnFSubmit.Visible = true;
                }
                else
                {
                    btnFSubmit.Visible = false;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowTDSDetailModal();", true);
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
    protected void gvTdsGSTDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblGSTModal.Text = "";
        string ID = e.CommandArgument.ToString();
        if (e.CommandName == "TDSGSTDelete")
        {
            DataTable dt_TdsGSTTable = (DataTable)ViewState["TdsGSTTable"];
            int Count = dt_TdsGSTTable.Rows.Count;
            for (int i = 0; i < Count; i++)
            {
                DataRow dr = dt_TdsGSTTable.Rows[i];
                if (dr["RowNo"].ToString() == ID.ToString())
                {
                    dr.Delete();
                    break;
                }
            }
            dt_TdsGSTTable.AcceptChanges();
            ViewState["TdsGSTTable"] = dt_TdsGSTTable;

            gvTdsGSTDetail.DataSource = dt_TdsGSTTable;
            gvTdsGSTDetail.DataBind();


            if (gvTdsGSTDetail.Rows.Count > 0)
            {
		decimal GSTTDSAmount = 0;
                foreach (GridViewRow rows in gvTdsGSTDetail.Rows)
                {
                    Label lblGSTTDSAmount = (Label)rows.FindControl("lblGSTTDSAmount");
                    GSTTDSAmount += decimal.Parse(lblGSTTDSAmount.Text);

                }
                if (decimal.Parse(GSTTDSAmount.ToString()) == decimal.Parse(txtLedgerTx_Amount.Text))
                {
                    btnFSubmit.Visible = true;
                }
                else
                {
                    btnFSubmit.Visible = false;
                }
                //btnFSubmit.Visible = true;
            }
            else
            {
                btnFSubmit.Visible = false;
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowTDSDetailModal();", true);
        }
    }
    protected void SaveTDSGSTData(string VoucherTx_ID)
    {
        objdb.ByProcedure("SpFinTDSGSTEntry", new string[] { "flag", "Voucher_Id" },
                new string[] { "4", VoucherTx_ID }, "dataset");
        DataTable dt_TdsGSTTable = (DataTable)ViewState["TdsGSTTable"];
        gvTdsGSTDetail.DataSource = dt_TdsGSTTable;
        gvTdsGSTDetail.DataBind();
        foreach (GridViewRow row in gvTdsGSTDetail.Rows)
        {
            Label lblLedger_ID = (Label)row.FindControl("lblLedger_ID");
            Label lblGSTNo = (Label)row.FindControl("lblGSTNo");
            Label lblBillNo = (Label)row.FindControl("lblBillNo");
            Label lblBillDate = (Label)row.FindControl("lblBillDate");
            Label lblTotalBillAmount = (Label)row.FindControl("lblTotalBillAmount");
            Label lblPaymentAmount = (Label)row.FindControl("lblPaymentAmount");
            Label lblBasicAmount = (Label)row.FindControl("lblBasicAmount");
            Label lblGSTTDSAmount = (Label)row.FindControl("lblGSTTDSAmount");
            Label lblCGST = (Label)row.FindControl("lblCGST");
            Label lblSGST = (Label)row.FindControl("lblSGST");
            Label lblIGST = (Label)row.FindControl("lblIGST");
            objdb.ByProcedure("SpFinTDSGSTEntry", new string[] { "flag", "Voucher_Id", "ParentLedgerId", "ChildLedger_ID", "GSTNo", "BillNo", "BillDate", "TotalBillAmount", "PaymentAmount", "BasicAmount", "GSTTDSAmount", "CGST", "SGST", "IGST", "Office_ID", "CreatedBy" },
                new string[] { "1", VoucherTx_ID, "6", lblLedger_ID.Text, lblGSTNo.Text, lblBillNo.Text, Convert.ToDateTime(lblBillDate.Text, cult).ToString("yyyy/MM/dd"), lblTotalBillAmount.Text, lblPaymentAmount.Text, lblBasicAmount.Text, lblGSTTDSAmount.Text, lblCGST.Text, lblSGST.Text, lblIGST.Text, ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
        }
    }
    protected void btnFSubmit_Click(object sender, EventArgs e)
    {
        SaveLedgerDetail();
    }
    protected void ClearTDSGST()
    {
        ddlPartyName.ClearSelection();
        txtTdsGSTNo.Text = "";
        txtBillNo.Text = "";
        txtBillDate.Text = "";
        txtTdsBillAmount.Text = "";
        txtPaymentAmountNTBD.Text = "";
        txtBasicAmount.Text = "";
        txtGSTTDSAmount.Text = "";
        txtCGST.Text = "";
        txtSGST.Text = "";
        txtIGST.Text = "";
    }
    protected void txtBasicAmount_TextChanged(object sender, EventArgs e)
    {
        if (txtBasicAmount.Text != "")
        {
            decimal BasicAmt = Convert.ToDecimal(txtBasicAmount.Text);
            decimal GSTTDS = (BasicAmt * 2) / 100;
            txtGSTTDSAmount.Text = GSTTDS.ToString("0.00");

            ds = objdb.ByProcedure("SpFinTDSGSTEntry", new string[] { "flag", "ChildLedger_ID" }, new string[] { "5", ddlPartyName.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Status"].ToString() == "True")
                {
                    txtCGST.Text = Convert.ToDecimal(Convert.ToDecimal(txtGSTTDSAmount.Text) / 2).ToString("0.00");
                    txtSGST.Text = Convert.ToDecimal(Convert.ToDecimal(txtGSTTDSAmount.Text) / 2).ToString("0.00");
                    txtIGST.Text = "0";
                }
                else
                {
                    txtIGST.Text = txtGSTTDSAmount.Text;
                    txtCGST.Text = "0";
                    txtSGST.Text = "0";
                }
            }
        }
        else
        {
            txtGSTTDSAmount.Text = "0";
        }
        if (gvTdsGSTDetail.Rows.Count > 0)
        {
            //btnFSubmit.Visible = true;
        }
    }
    protected void txtGSTTDSAmount_TextChanged(object sender, EventArgs e)
    {
        if (txtBasicAmount.Text != "" && txtGSTTDSAmount.Text != "")
        {
            decimal TDSVal = Convert.ToDecimal(txtGSTTDSAmount.Text);
            ds = objdb.ByProcedure("SpFinTDSGSTEntry", new string[] { "flag", "ChildLedger_ID" }, new string[] { "5", ddlPartyName.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Status"].ToString() == "True")
                {
                    txtCGST.Text = (TDSVal / 2).ToString("0.00");
                    txtSGST.Text = (TDSVal / 2).ToString("0.00");
                    txtIGST.Text = "0";
                }
                else
                {
                    txtIGST.Text = txtGSTTDSAmount.Text;
                    txtCGST.Text = "0";
                    txtSGST.Text = "0";
                }
            }
        }
        else
        {
            txtIGST.Text = "0";
            txtCGST.Text = "0";
            txtSGST.Text = "0";
        }
        if (gvTdsGSTDetail.Rows.Count > 0)
        {
            //btnFSubmit.Visible = true;
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowTDSDetailModal();", true);
    }
    protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblGSTModal.Text = "";
            txtTdsGSTNo.Text = "";
            txtBillNo.Text = "";
            txtBillDate.Text = "";
            txtTdsBillAmount.Text = "";
            txtPaymentAmountNTBD.Text = "";
            txtBasicAmount.Text = "";
            txtGSTTDSAmount.Text = "";
            txtCGST.Text = "";
            txtSGST.Text = "";
            txtIGST.Text = "";
            ds = objdb.ByProcedure("SpFinTDSGSTEntry", new string[] { "flag", "ChildLedger_ID" }, new string[] { "6", ddlPartyName.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtTdsGSTNo.Text = ds.Tables[0].Rows[0]["GST_No"].ToString();
            }
            if (gvTdsGSTDetail.Rows.Count > 0)
            {
                //btnFSubmit.Visible = true;
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowTDSDetailModal();", true);
        }
        catch (Exception)
        {
            lblGSTModal.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Party Name is alrady exists.");
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
    protected void btnRefreshLedger_Click(object sender, EventArgs e)
    {
        try
        {
            ddlPartyName.DataSource = null;
            ddlPartyName.DataBind();
            if (ViewState["Office_ID"].ToString() == "1")
            {
                ds = objdb.ByProcedure("SpFinTDSGSTEntry", new string[] { "flag", "Head_ID", "Office_ID" }, new string[] { "11", "115", "1" }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlPartyName.DataTextField = "Ledger_Name";
                    ddlPartyName.DataValueField = "Ledger_ID";
                    ddlPartyName.DataSource = ds;
                    ddlPartyName.DataBind();
                    ddlPartyName.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                string Ledger_ID = "";
                foreach (GridViewRow row in GridViewLedgerDetail.Rows)
                {
                    Label lblLedger_ID = (Label)row.FindControl("Ledger_ID");
                    Label Type = (Label)row.FindControl("Type");
                    if (Type.Text == "Dr")
                    {
                        Ledger_ID += lblLedger_ID.Text + ",";
                    }
                }
                ds = objdb.ByProcedure("SpFinTDSGSTEntry", new string[] { "flag", "ChildLedger_ID" }, new string[] { "3", Ledger_ID }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlPartyName.DataTextField = "Ledger_Name";
                    ddlPartyName.DataValueField = "Ledger_ID";
                    ddlPartyName.DataSource = ds;
                    ddlPartyName.DataBind();
                    ddlPartyName.Items.Insert(0, new ListItem("Select", "0"));
                }
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowTDSDetailModal();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "Delete";
    }
    protected void btnRefreshLedgerList_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            FillParticularsDropDown();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}