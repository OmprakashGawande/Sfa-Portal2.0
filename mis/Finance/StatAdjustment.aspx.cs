using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Finance_StatAdjustment : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
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
                    if (ViewState["Office_ID"].ToString() == "1")
                    {
                        txtVoucherTx_No.MaxLength = 9;
                    }
                    else
                    {
                        txtVoucherTx_No.MaxLength = 8;
                    }
                    ViewState["LedgerTotal"] = "0";
                    FillParticularsDropDown();
                    txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
                    ddlcreditdebit.SelectedValue = "Dr";
                    ddlcreditdebit.Enabled = false;
                    ViewState["VoucherTx_ID"] = "0";
                    ViewState["Action"] = "";
                    CreateLedgerTable();
                    FillVoucherDate();
                    GridViewLedgerDetail.DataSource = new string[] { };
                    GridViewLedgerDetail.DataBind();
                    btnAccept.Enabled = false;
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
                //lblVoucherTx_No.Text = Office_Code + FinancialYear.ToString().Substring(2) + "VR";
                lblVoucherTx_No.Text = Office_Code + FinancialYear.ToString().Substring(2);
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
               new string[] { "23", ViewState["Office_ID"].ToString(), "6,11,8" }, "dataset");

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

    //Ledger CurrentBalance
    protected void ddlLedger_ID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlLedger_ID.SelectedIndex > 0)
            {

                ds = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "1", ddlLedger_ID.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtCurrentBalance.Text = ds.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    //LedgerDetail Event & Function
    protected void CreateLedgerTable()
    {
        ViewState["LedgerTable"] = "";
        DataTable dt_LedgerTable = new DataTable();
        DataColumn RowNo = dt_LedgerTable.Columns.Add("RowNo", typeof(int));
        dt_LedgerTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("Type", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_MaintainType", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_Credit", typeof(decimal)));
        dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_Debit", typeof(decimal)));
        RowNo.AutoIncrement = true;
        RowNo.AutoIncrementSeed = 1;
        RowNo.AutoIncrementStep = 1;
        ViewState["LedgerTable"] = dt_LedgerTable;


        GridViewLedgerDetail.DataSource = dt_LedgerTable;
        GridViewLedgerDetail.DataBind();
    }
    protected void btnAddLedger_Click(object sender, EventArgs e)
    {

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
            string LedgerId = ddlLedger_ID.SelectedValue;

            if (ddlcreditdebit.SelectedValue == "Dr")
            {
                ViewState["LedgerAmount"] = "-" + txtLedgerTx_Amount.Text;
            }
            else
            {
                ViewState["LedgerAmount"] = txtLedgerTx_Amount.Text;
            }
            ViewState["Amount"] = txtLedgerTx_Amount.Text;


            ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", LedgerId, ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "Yes")
                {

                }
                else if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "BankLedger")
                {

                }
                else
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
                    dt_LedgerTable.Rows.Add(null, ddlLedger_ID.SelectedValue.ToString(), Ledger_Name, ddlcreditdebit.SelectedValue.ToString(), "None", LedgerTx_Credit, LedgerTx_Debit);

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
                    }
                    else
                    {
                        btnAccept.Enabled = false;
                    }


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
                    ddlLedger_ID.ClearSelection();
                }

            }


        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }

    }
    protected void GridViewLedgerDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int RowNo = int.Parse(GridViewLedgerDetail.DataKeys[e.RowIndex].Value.ToString());

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
            Label Ledger_Name = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_Name");
            Label lblMaintainType = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("lblMaintainType");
            Label LedgerTx_Credit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Credit");
            Label LedgerTx_Debit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Debit");
            //Label Ledger_TableID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_TableID");
            if (lblRowNumber.Text != RowNo.ToString())
            {

                dt_LedgerTableTemp.Rows.Add(lblRowNumber.Text, Ledger_ID.Text, Ledger_Name.Text, Type.Text, lblMaintainType.Text, LedgerTx_Credit.Text, LedgerTx_Debit.Text);
            }
        }
        GridViewLedgerDetail.DataSource = null;
        GridViewLedgerDetail.DataBind();
        GridViewLedgerDetail.DataSource = dt_LedgerTableTemp;
        GridViewLedgerDetail.DataBind();
        decimal LedgerCreditTotal = 0;
        decimal LedgerDebitTotal = 0;
        decimal GrandTotal = 0;
        LedgerCreditTotal = dt_LedgerTableTemp.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
        LedgerDebitTotal = dt_LedgerTableTemp.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));
        GrandTotal = LedgerCreditTotal + LedgerDebitTotal;
        GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>Total : </b>";
        GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
        GridViewLedgerDetail.FooterRow.Cells[6].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";



        ViewState["LedgerCreditTotal"] = LedgerCreditTotal;
        ViewState["LedgerDebitTotal"] = LedgerDebitTotal;
        if (ViewState["LedgerCreditTotal"] == "0" || ViewState["LedgerCreditTotal"] == "0.00")
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
        }
        else
        {
            btnAccept.Enabled = false;
        }

        ViewState["LedgerTable"] = dt_LedgerTableTemp;
        if (dt_LedgerTableTemp.Rows.Count > 0)
        {
            ddlcreditdebit.Enabled = true;
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
        else
        {
            ddlcreditdebit.SelectedValue = "Dr";
            ddlcreditdebit.Enabled = false;
            txtLedgerTx_Amount.Text = "";
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
                msg += "Enter Voucher No. \\n";
            }
            if (txtVoucherTx_Date.Text == "")
            {
                msg += "Enter Date. \\n";
            }
            else
            {
                string ValidStatus = ValidDate();
                //if (ValidStatus == "No")
                //{
                //    Response.Redirect("~/mis/Login.aspx");
                //}
            }
            if (txtVoucherTx_Narration.Text == "")
            {
                msg += "Enter Narration. \\n";
            }

            if (ddlNatofadj.SelectedIndex == 0)
            {
                msg += "Select Nature Of Adjustment. \\n";
            }
            if (ddlAdddetls.SelectedIndex == 0)
            {
                msg += "Select Additional Details. \\n";
            }


            if (msg == "")
            {
                decimal CGST_Per = 0;
                decimal SGST_Per = 0;
                decimal IGST_Per = 0;
                decimal CGSTAmt = 0;
                decimal SGSTAmt = 0;
                decimal IGSTAmt = 0;
                decimal TaxableValue = 0;
                decimal Per = decimal.Parse(ddlPer.SelectedValue.ToString());
                string Voucher_NO = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
                string sDate = (Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd")).ToString();
                DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
                int Month = int.Parse(datevalue.Month.ToString());
                int Year = int.Parse(datevalue.Year.ToString());
                int FY = Year;
                string FinancialYear = Year.ToString();
                string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
                FinancialYear = "";
                string GSTVoucher = "Yes";
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
                DataSet ds11 = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "VoucherTx_No", "VoucherTx_ID" },
                    new string[] { "9", Voucher_NO, ViewState["VoucherTx_ID"].ToString() }, "dataset");
                if (ds11.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds11.Tables[0].Rows[0]["Status"].ToString());

                }
                if (btnAccept.Text == "Accept" && ViewState["VoucherTx_ID"].ToString() == "0" && Status == 0)
                {
                    ds = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy", "GSTVoucher" },
                    new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Journal GST", "StatAdjustment", Voucher_NO.ToString(), "", txtVoucherTx_Narration.Text, ViewState["LedgerDebitTotal"].ToString(), Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "0", ViewState["Emp_ID"].ToString(), GSTVoucher }, "dataset");

                    string VoucherTx_ID = ds.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
                    string IsActive = "0";

                    DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];


                    for (int i = 0; i < dt_LedgerTable.Rows.Count; i++)
                    {
                        string RowNo = dt_LedgerTable.Rows[i]["RowNo"].ToString();
                        string Ledger_ID = dt_LedgerTable.Rows[i]["Ledger_ID"].ToString();
                        string LedgerTx_Amount = "";
                        decimal Amount = 0;


                        if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                        {
                            LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                            Amount = decimal.Parse(dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString());
                            LedgerTx_Amount = "-" + LedgerTx_Amount;
                        }
                        else
                        {
                            Amount = decimal.Parse(dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString());
                            LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                        }
                        if (Ledger_ID == "1" || Ledger_ID == "46718" || Ledger_ID == "46721")
                        {
                            CGST_Per = Per / 2;
                            CGSTAmt = Amount;

                        }
                        else if (Ledger_ID == "2" || Ledger_ID == "46719" || Ledger_ID == "46722")
                        {
                            SGST_Per = Per / 2;
                            SGSTAmt = Amount;
                        }
                        else if (Ledger_ID == "737" || Ledger_ID == "46720" || Ledger_ID == "46723")
                        {
                            IGST_Per = Per;
                            IGSTAmt = Amount;
                        }
                        objdb.ByProcedure("SpFinLedgerTx",
                        new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType" },
                        new string[] { "0", Ledger_ID, VoucherTx_ID, "StatAdjustment", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "0", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "None", }, "dataset");

                    }
                    objdb.ByProcedure("SpFinStatAdjustment", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Date", "Typeofdutytax", "Natureofadjustment", "AdditionalDetail", "IsActive", "UpdatedBy", "Office_ID", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "TaxableValue" }, new string[] { "0", VoucherTx_ID, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ddldutytax.SelectedItem.Text, ddlNatofadj.SelectedItem.Text, ddlAdddetls.SelectedItem.Text, IsActive, ViewState["Emp_ID"].ToString(), ViewState["Office_ID"].ToString(), CGST_Per.ToString(), SGST_Per.ToString(), IGST_Per.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), txtTaxableValue.Text }, "dataset");
                    objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "40", VoucherTx_ID }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                    ClearData();

                }
                else if (btnAccept.Text == "Update" && ViewState["VoucherTx_ID"].ToString() != "0" && Status == 0)
                {
                    //  ds = objdb.ByProcedure("SpFinVoucherTx",
                    //new string[] { "flag","VoucherTx_ID", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy" },
                    //new string[] { "7", ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Journal", "Journal", txtVoucherTx_No.Text, "", txtVoucherTx_Narration.Text, ViewState["LedgerDebitTotal"].ToString(), Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", ViewState["Emp_ID"].ToString() }, "dataset");
                    ds = objdb.ByProcedure("SpFinVoucherTx",
                  new string[] { "flag", "VoucherTx_ID", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy" },
                  new string[] { "7", ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Journal GST", "StatAdjustment", Voucher_NO.ToString(), "", txtVoucherTx_Narration.Text, ViewState["LedgerDebitTotal"].ToString(), Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", ViewState["Emp_ID"].ToString() }, "dataset");


                    objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                    objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "4", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                    DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];


                    for (int i = 0; i < dt_LedgerTable.Rows.Count; i++)
                    {
                        string RowNo = dt_LedgerTable.Rows[i]["RowNo"].ToString();
                        string Ledger_ID = dt_LedgerTable.Rows[i]["Ledger_ID"].ToString();
                        string LedgerTx_Amount = "";
                        decimal Amount = 0;
                        if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                        {
                            Amount = decimal.Parse(dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString());
                            LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                            LedgerTx_Amount = "-" + LedgerTx_Amount;
                        }
                        else
                        {
                            Amount = decimal.Parse(dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString());
                            LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                        }
                        if (Ledger_ID == "1" || Ledger_ID == "46718" || Ledger_ID == "46721")
                        {

                            CGST_Per = Per / 2;
                            CGSTAmt = Amount;

                        }
                        else if (Ledger_ID == "2" || Ledger_ID == "46719" || Ledger_ID == "46722")
                        {
                            SGST_Per = Per / 2;
                            SGSTAmt = Amount;
                        }
                        else if (Ledger_ID == "737" || Ledger_ID == "46720" || Ledger_ID == "46723")
                        {
                            IGST_Per = Per / 2;
                            IGSTAmt = Amount;
                        }

                        objdb.ByProcedure("SpFinLedgerTx",
                        new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType" },
                        new string[] { "0", Ledger_ID, ViewState["VoucherTx_ID"].ToString(), "StatAdjustment", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", "None" }, "dataset");

                    }
                    objdb.ByProcedure("SpFinStatAdjustment", new string[] { "flag", "VoucherTx_ID", "VoucherTx_Date", "Typeofdutytax", "Natureofadjustment", "AdditionalDetail", "UpdatedBy", "Office_ID", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "TaxableValue" }, new string[] { "1", ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), ddldutytax.SelectedItem.Text, ddlNatofadj.SelectedItem.Text, ddlAdddetls.SelectedItem.Text, ViewState["Emp_ID"].ToString(), ViewState["Office_ID"].ToString(), CGST_Per.ToString(), SGST_Per.ToString(), IGST_Per.ToString(), CGSTAmt.ToString(), SGSTAmt.ToString(), IGSTAmt.ToString(), txtTaxableValue.Text }, "dataset");


                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
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
    protected void ClearData()
    {
        try
        {
            txtVoucherTx_No.Text = "";
            ddlLedger_ID.ClearSelection();
            txtVoucherTx_Narration.Text = "";
            GridViewLedgerDetail.DataSource = new string[] { };
            GridViewLedgerDetail.DataBind();
            ddlcreditdebit.Enabled = false;
            ddlcreditdebit.SelectedValue = "Dr";
            CreateLedgerTable();
            ddlNatofadj.ClearSelection();
            ddlAdddetls.ClearSelection();
            ddlPer.ClearSelection();
            txtTaxableValue.Text = "";
            ddlLedger_ID.ClearSelection();
            txtCurrentBalance.Text = "";
            txtLedgerTx_Amount.Text = "";
            btnAccept.Enabled = false;
            FillVoucherNo();
            txtCurrentBalance.Text = "";
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
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID", "Office_ID" }, new string[] { "11", ViewState["VoucherTx_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
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

                        Fstring = str.Substring(0, 7);
                        Lstring = str.Substring(7, str.Length - 7);


                    }
                    else
                    {
                        Fstring = str.Substring(0, 8);
                        Lstring = str.Substring(8, str.Length - 8);
                    }

                    txtVoucherTx_No.Text = Lstring;
                    lblVoucherTx_No.Text = Fstring;
                    DataSet dsstatadj = objdb.ByProcedure("SpFinStatAdjustment", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                    if (dsstatadj != null && dsstatadj.Tables[0].Rows.Count > 0)
                    {
                        ddldutytax.ClearSelection();
                        ddldutytax.Items.FindByText(dsstatadj.Tables[0].Rows[0]["Typeofdutytax"].ToString()).Selected = true;
                        ddlNatofadj.ClearSelection();
                        ddlNatofadj.Items.FindByText(dsstatadj.Tables[0].Rows[0]["Natureofadjustment"].ToString()).Selected = true;
                        ddlAdddetls.ClearSelection();
                        ddlAdddetls.Items.FindByText(dsstatadj.Tables[0].Rows[0]["AdditionalDetail"].ToString()).Selected = true;
                        int Percent = int.Parse(dsstatadj.Tables[0].Rows[0]["Per"].ToString());
                        ddlPer.ClearSelection();
                        ddlPer.Items.FindByValue(Percent.ToString()).Selected = true;
                        txtTaxableValue.Text = dsstatadj.Tables[0].Rows[0]["TaxableValue"].ToString();
                    }

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

                        dt_LedgerTable.Rows.Add(lblRowNumber.Text, Ledger_ID.Text, Ledger_Name.Text, Type.Text, lblMaintainType.Text, LedgerTx_Credit.Text, LedgerTx_Debit.Text);

                    }

                    ViewState["LedgerTable"] = dt_LedgerTable;



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

    //Fill Previous VoucherNarration
    protected void btnNarration_Click(object sender, EventArgs e)
    {
        ds = objdb.ByProcedure("SpFinVoucherTx",
                 new string[] { "flag", "VoucherTx_Type", "Office_ID" },
                 new string[] { "14", "StatAdjustment", ViewState["Office_ID"].ToString() }, "dataset");

        if (ds.Tables[0].Rows.Count != 0)
        {
            txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
        }
    }

    //View BillbyBillRefDetail   
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
            btnClear.Visible = false;
            ddlcreditdebit.Visible = false;
            txtLedgerTx_Amount.Visible = false;
            divparticular.Visible = false;
            txtVoucherTx_Narration.Attributes.Add("readonly", "readonly");
            txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
            txtVoucherTx_No.Attributes.Add("readonly", "readonly");
            lbkbtnAddLedger.Visible = false;
            GridViewLedgerDetail.Columns[7].Visible = false;
            //GridViewLedgerDetail.Columns[8].Visible = false;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //change voucher series according to FY
    protected void txtVoucherTx_Date_TextChanged(object sender, EventArgs e)
    {
        // FillVoucherNo();
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
            string VoucherTx_Type = "StatAdjustment";
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