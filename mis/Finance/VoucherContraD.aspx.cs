using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
public partial class mis_Finance_VoucherContraD : System.Web.UI.Page
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
                    ViewState["LedgerTotal"] = "0";

                    ViewState["VoucherTx_ID"] = "0";
                    txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
                    ViewState["Action"] = "";
                    FillAccountDropDown();
                    CreateLedgerTable();
                    CreateDataSetFinChequeTx();
                    FillVoucherDate();
                    FillVoucherNo();

                    ddlcreditdebit.Enabled = false;
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
    protected void FillAccountDropDown()
    {
        try
        {

            ds = objdb.ByProcedure("SpFinLedgerMaster",
               new string[] { "flag", "Office_ID", "MultipleHeadIDs" },
               new string[] { "22", ViewState["Office_ID"].ToString(), "117,121,124" }, "dataset");//Bank /Bank O.D /Cash-in-hand
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

                int VoucherTx_SNo = 0;
                if (ds1.Tables[0].Rows.Count != 0)
                {
                    VoucherTx_SNo = Convert.ToInt32(ds1.Tables[0].Rows[0]["VoucherTx_SNo"].ToString());

                }

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
    protected void btnAddLedger_Click(object sender, EventArgs e)
    {
        CreatTableFinChequeTx();
        string msg = "";
        string LedgerId = ddlLedger_ID.SelectedValue;
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
            //int status = 0;
            //int rowIndex = 0;
            //int gridRows = GridViewLedgerDetail.Rows.Count;
            //if (gridRows > 0)
            //{
            //    for (rowIndex = 0; rowIndex < gridRows; rowIndex++)
            //    {
            //        Label lblLedgerID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_ID");
            //        if (lblLedgerID.Text == ddlLedger_ID.SelectedValue.ToString())
            //        {

            //            status = 1;
            //        }
            //        else
            //        {
            //            //status = 0;
            //        }
            //    }
            //}
            //if (status == 1)
            //{
            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Ledger already exists');", true);
            //}
            //else 
            //{
            //    ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", LedgerId, ViewState["Office_ID"].ToString() }, "dataset");
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //       if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "BankLedger")
            //       {
            //           txtChequeTx_Amount.Text = txtLedgerTx_Amount.Text;
            //           Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
            //       }
            //       else
            //       {
            //           DataSet dsFinChequeTx = (DataSet)ViewState["dsFinChequeTx"];

            //           dsFinChequeTx.Merge((DataTable)ViewState["FinChequeTx"]);

            //           ViewState["dsFinChequeTx"] = dsFinChequeTx;

            //           DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];
            //           decimal LedgerTx_Credit;
            //           decimal LedgerTx_Debit;
            //           if (ddlcreditdebit.SelectedItem.Text == "Credit")
            //           {
            //               LedgerTx_Credit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
            //               LedgerTx_Debit = 0;
            //           }
            //           else
            //           {
            //               LedgerTx_Credit = 0;
            //               LedgerTx_Debit = Convert.ToDecimal(txtLedgerTx_Amount.Text);
            //           }

            //           dt_LedgerTable.Rows.Add(null,ddlLedger_ID.SelectedValue.ToString(), ddlLedger_ID.SelectedItem.Text, ddlcreditdebit.SelectedValue.ToString(), "None", LedgerTx_Credit, LedgerTx_Debit);

            //           GridViewLedgerDetail.DataSource = dt_LedgerTable;
            //           GridViewLedgerDetail.DataBind();

            //           decimal LedgerCreditTotal = 0;
            //           decimal LedgerDebitTotal = 0;
            //           LedgerCreditTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
            //           LedgerDebitTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));
            //           GridViewLedgerDetail.FooterRow.Cells[3].Text = "<b>Total : </b>";
            //           GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
            //           GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";
            //           GridViewLedgerDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            //           GridViewLedgerDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            //           ViewState["LedgerCreditTotal"] = LedgerCreditTotal;
            //           ViewState["LedgerDebitTotal"] = LedgerDebitTotal;

            //           if (LedgerCreditTotal == LedgerDebitTotal && LedgerDebitTotal != 0)
            //           {
            //               btnAccept.Enabled = true;
            //           }
            //           else
            //           {
            //               btnAccept.Enabled = false;
            //           }
            //           ViewState["LedgerTable"] = dt_LedgerTable;

            //           ClearFinChequeTxModal();
            //           txtCurrentBalance.Text = "";
            //           decimal ReaminingBal = LedgerDebitTotal - LedgerCreditTotal;
            //           if (ReaminingBal.ToString().Contains("-"))
            //           {
            //               ReaminingBal = decimal.Parse(ReaminingBal.ToString().Replace(@"-", string.Empty));
            //               txtLedgerTx_Amount.Text = ReaminingBal.ToString();
            //               ddlcreditdebit.SelectedValue = "Dr";
            //           }
            //           else
            //           {
            //               txtLedgerTx_Amount.Text = ReaminingBal.ToString();
            //               ddlcreditdebit.SelectedValue = "Cr";

            //           }
            //           ddlcreditdebit.Enabled = true;

            //       }
            //    }


            //    //btnAddChequeDetail.Enabled = false;
            //}
            ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", LedgerId, ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "BankLedger")
                {
                    txtChequeTx_Amount.Text = txtLedgerTx_Amount.Text;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
                }
                else
                {
                    DataSet dsFinChequeTx = (DataSet)ViewState["dsFinChequeTx"];

                    dsFinChequeTx.Merge((DataTable)ViewState["FinChequeTx"]);

                    ViewState["dsFinChequeTx"] = dsFinChequeTx;

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
                    GridViewLedgerDetail.FooterRow.Cells[3].Text = "<b>Total : </b>";
                    GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
                    GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";
                    GridViewLedgerDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    GridViewLedgerDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
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


            //btnAddChequeDetail.Enabled = false;



        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }

    }
    protected void CreateLedgerTable()
    {
        ViewState["LedgerTable"] = "";

        //DataTable dt_LedgerTable = new DataTable(ViewState["TableId"].ToString());
        DataTable dt_LedgerTable = new DataTable();
        DataColumn RowNo = dt_LedgerTable.Columns.Add("RowNo", typeof(int));
        dt_LedgerTable.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("Type", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_MaintainType", typeof(string)));
        dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_Credit", typeof(decimal)));
        dt_LedgerTable.Columns.Add(new DataColumn("LedgerTx_Debit", typeof(decimal)));
        // dt_LedgerTable.Columns.Add(new DataColumn("Ledger_TableID", typeof(decimal)));
        RowNo.AutoIncrement = true;
        RowNo.AutoIncrementSeed = 1;
        RowNo.AutoIncrementStep = 1;
        ViewState["LedgerTable"] = dt_LedgerTable;


        GridViewLedgerDetail.DataSource = dt_LedgerTable;
        GridViewLedgerDetail.DataBind();
    }
    protected void GridViewLedgerDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsFinChequeTx = (DataSet)ViewState["dsFinChequeTx"];
        int RowNo = int.Parse(GridViewLedgerDetail.SelectedDataKey.Value.ToString());

        GVViewFinChequeTx.DataSource = dsFinChequeTx.Tables[RowNo.ToString()];
        GVViewFinChequeTx.DataBind();

        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetailView();", true);



    }
    protected void GridViewLedgerDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int RowNo = int.Parse(GridViewLedgerDetail.DataKeys[e.RowIndex].Value.ToString());
        DataSet dsFinChequeTx = (DataSet)ViewState["dsFinChequeTx"];
        DataSet dsFinChequeTxTemp = new DataSet();
        dsFinChequeTxTemp = dsFinChequeTx;
        for (int i = 0; i < dsFinChequeTxTemp.Tables.Count; i++)
        {
            if (dsFinChequeTxTemp.Tables[i].TableName == RowNo.ToString())
            {
                dsFinChequeTx.Tables[i].Clear();
                //dsBillByBill.Tables[i].Merge(dsBillByBillTemp.Tables[i]);
            }
        }



        DataTable dt_LedgerTableTemp = new DataTable();
        DataColumn tempRowNo = dt_LedgerTableTemp.Columns.Add("RowNo", typeof(int));
        dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
        dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_Name", typeof(string)));
        dt_LedgerTableTemp.Columns.Add(new DataColumn("Type", typeof(string)));
        dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_MaintainType", typeof(string)));
        dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_Credit", typeof(decimal)));
        dt_LedgerTableTemp.Columns.Add(new DataColumn("LedgerTx_Debit", typeof(decimal)));
        tempRowNo.AutoIncrement = true;
        tempRowNo.AutoIncrementSeed = 1;
        tempRowNo.AutoIncrementStep = 1;
        //dt_LedgerTableTemp.Columns.Add(new DataColumn("Ledger_TableID", typeof(decimal)));


        int gridRows = GridViewLedgerDetail.Rows.Count;
        for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
        {
            Label lblRowNumber = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("lblRowNumber");
            Label Ledger_ID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_ID");
            Label Type = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Type");
            Label LedgerTx_MaintainType = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_MaintainType");
            Label Ledger_Name = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_Name");
            Label LedgerTx_Credit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Credit");
            Label LedgerTx_Debit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Debit");
            //Label Ledger_TableID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_TableID");
            if (lblRowNumber.Text != RowNo.ToString())
            {

                dt_LedgerTableTemp.Rows.Add(lblRowNumber.Text, Ledger_ID.Text, Ledger_Name.Text, Type.Text, LedgerTx_MaintainType.Text, LedgerTx_Credit.Text, LedgerTx_Debit.Text);
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
        GridViewLedgerDetail.FooterRow.Cells[3].Text = "<b>Total : </b>";
        GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
        GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";
        GridViewLedgerDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
        GridViewLedgerDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
        ViewState["LedgerCreditTotal"] = LedgerCreditTotal;
        ViewState["LedgerDebitTotal"] = LedgerDebitTotal;


        ViewState["LedgerTable"] = dt_LedgerTableTemp;
        if (dt_LedgerTableTemp.Rows.Count == 0)
        {
            ddlcreditdebit.Enabled = false;
            ddlcreditdebit.SelectedValue = "Cr";

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
        FillNarration();
    }
    protected void GridViewLedgerDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (ViewState["Action"].ToString() != "")
            {
                int Action = int.Parse(ViewState["Action"].ToString());
                if (Action == 1)
                {
                    e.Row.Cells[7].Visible = false;
                }
                else
                {
                    e.Row.Cells[7].Visible = true;

                }


            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //AddChequeDetail Event & Function
    protected void CreateDataSetFinChequeTx()
    {
        DataSet dsFinChequeTx = new DataSet();
        ViewState["dsFinChequeTx"] = dsFinChequeTx;

    }
    protected void CreatTableFinChequeTx()
    {
        //ViewState["TableId"] = Convert.ToInt32(ViewState["TableId"].ToString()) + 1;
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
        ViewState["FinChequeTx"] = dt_FinChequeTx;

        GVFinChequeTx.DataSource = dt_FinChequeTx;
        GVFinChequeTx.DataBind();
    }
    protected void btnAddCheque_Click(object sender, EventArgs e)
    {
        string msg = "";
        if (txtChequeTx_Amount.Text == "")
        {
            msg += "Enter Amount \\n";
        }
        if (txtChequeTx_Amount.Text != "")
        {
            if (float.Parse(txtChequeTx_Amount.Text) > float.Parse(txtLedgerTx_Amount.Text))
            {
                msg += "Enter Valid Amount \\n";
            }
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
                DataSet dsFinChequeTx = (DataSet)ViewState["dsFinChequeTx"];

                dsFinChequeTx.Merge((DataTable)ViewState["FinChequeTx"]);

                ViewState["dsFinChequeTx"] = dsFinChequeTx;

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
                FillNarration();
                decimal LedgerCreditTotal = 0;
                decimal LedgerDebitTotal = 0;
                LedgerCreditTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
                LedgerDebitTotal = dt_LedgerTable.AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));
                GridViewLedgerDetail.FooterRow.Cells[3].Text = "<b>Total : </b>";
                GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
                GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";
                GridViewLedgerDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                GridViewLedgerDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
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
            string ChequeTx_Date;
            if (txtChequeTx_Date.Text != "")
            {
                ChequeTx_Date = txtChequeTx_Date.Text;
            }
            else
            {
                ChequeTx_Date = null;
            }
            DataTable dt_FinChequeTx = (DataTable)ViewState["FinChequeTx"];

            dt_FinChequeTx.Rows.Add(ddlLedger_ID.SelectedValue.ToString(), txtChequeTx_No.Text, ChequeTx_Date, txtChequeTx_Amount.Text);


            GVFinChequeTx.DataSource = dt_FinChequeTx;
            GVFinChequeTx.DataBind();


            decimal ChequeTx_AmountTotal = 0;

            ChequeTx_AmountTotal = dt_FinChequeTx.AsEnumerable().Sum(row => row.Field<decimal>("ChequeTx_Amount"));

            //GVFinChequeTx.FooterRow.Cells[2].Text = "<b>Total : </b>";
            //GVFinChequeTx.FooterRow.Cells[3].Text = "<b>" + ChequeTx_AmountTotal.ToString() + "</b>";

            txtChequeTx_Amount.Text = (Convert.ToDecimal(txtLedgerTx_Amount.Text) - ChequeTx_AmountTotal).ToString();
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
        //txtChequeTx_No.Text = "";
        //txtChequeTx_Date.Text = "";
        txtChequeTx_Amount.Text = "";

        ViewState["FinChequeTx"] = "";

        GVFinChequeTx.DataSource = new string[] { };
        GVFinChequeTx.DataBind();


        ddlLedger_ID.ClearSelection();
        txtLedgerTx_Amount.Text = "";


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
            // else
            // {
                // string ValidStatus = ValidDate();
                // if (ValidStatus == "No")
                // {
                    // Response.Redirect("~/mis/Login.aspx");
                // }
            // }
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

                int Status = 0;
				DataSet ds11 = new DataSet();
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    ds11 = objdb.ByProcedure("SpFinVoucherTx",
                       new string[] { "flag", "VoucherTx_No", "VoucherTx_ID", "VoucherTx_Name" },
                       new string[] { "29", VoucherTx_No, ViewState["VoucherTx_ID"].ToString(), "Contra" }, "dataset");
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
                    //ds = objdb.ByProcedure("SpFinVoucherTx",
                    //new string[] { "flag", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy", "VoucherTx_SNo" },
                    //new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Contra", "Contra", txtVoucherTx_No.Text, "", txtVoucherTx_Narration.Text, ViewState["LedgerDebitTotal"].ToString(), Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", ViewState["Emp_ID"].ToString(), ViewState["VoucherTx_SNo"].ToString() }, "dataset");
                    ds = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy", "GSTVoucher" },
                    new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Contra", "Contra", VoucherTx_No.ToString(), "", txtVoucherTx_Narration.Text, ViewState["LedgerDebitTotal"].ToString(), Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "0", ViewState["Emp_ID"].ToString(), "No" }, "dataset");

                    string VoucherTx_ID = ds.Tables[0].Rows[0]["VoucherTx_ID"].ToString();


                    DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];


                    for (int i = 0; i < dt_LedgerTable.Rows.Count; i++)
                    {
                        string RowNo = dt_LedgerTable.Rows[i]["RowNo"].ToString();
                        string Ledger_ID = dt_LedgerTable.Rows[i]["Ledger_ID"].ToString();
                        string LedgerTx_MaintainType = dt_LedgerTable.Rows[i]["LedgerTx_MaintainType"].ToString();
                        string LedgerTx_Amount = "";

                        if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                        {
                            LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                            LedgerTx_Amount = "-" + LedgerTx_Amount;
                        }
                        else
                        {
                            LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                        }

                        int LedgerId = int.Parse(dt_LedgerTable.Rows[i]["Ledger_ID"].ToString());

                        objdb.ByProcedure("SpFinLedgerTx",
              new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType" },
              new string[] { "0", Ledger_ID, VoucherTx_ID, "Contra", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "0", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", LedgerTx_MaintainType.ToString() }, "dataset");
                        if (LedgerTx_MaintainType == "Cheque")
                        {
                            DataSet dsFinChequeTx = (DataSet)ViewState["dsFinChequeTx"];

                            for (int j = 0; j < dsFinChequeTx.Tables.Count; j++)
                            {
                                if (dsFinChequeTx.Tables[j].TableName == RowNo.ToString())
                                {
                                    for (int k = 0; k < dsFinChequeTx.Tables[j].Rows.Count; k++)
                                    {
                                        string ChequeTx_No = dsFinChequeTx.Tables[j].Rows[k]["ChequeTx_No"].ToString();
                                        if (ChequeTx_No == "")
                                        {
                                            ChequeTx_No = null;
                                        }
                                        else
                                        {

                                        }
                                        string ChequeTx_Date = dsFinChequeTx.Tables[j].Rows[k]["ChequeTx_Date"].ToString();
                                        if (ChequeTx_Date == "")
                                        {
                                            ChequeTx_Date = null;
                                        }
                                        else
                                        {
                                            ChequeTx_Date = Convert.ToDateTime(ChequeTx_Date, cult).ToString("yyyy/MM/dd");
                                        }
                                        string ChequeTx_Amount = dsFinChequeTx.Tables[j].Rows[k]["ChequeTx_Amount"].ToString();
                                        objdb.ByProcedure("SpFinChequeTx",
                                        new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy", "LedgerTx_OrderBy" },
                                        new string[] { "1", VoucherTx_ID, Ledger_ID, "Contra", ChequeTx_No, ChequeTx_Date, ChequeTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "0", ViewState["Emp_ID"].ToString(), (k + 1).ToString(), RowNo.ToString() }, "dataset");
                                    }
                                }
                            }

                        }
                        else
                        {

                        }

                    }
                    objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "40", VoucherTx_ID }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                    ClearData();
                }
                else if (btnAccept.Text == "Update" && ViewState["VoucherTx_ID"].ToString() != "0" && Status == 0)
                {
                    ds = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag", "VoucherTx_ID", "VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type", "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY", "VoucherTx_IsActive", "VoucherTx_InsertedBy" },
                    new string[] { "7", ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), "Contra", "Contra", VoucherTx_No.ToString(), "", txtVoucherTx_Narration.Text, ViewState["LedgerDebitTotal"].ToString(), Month.ToString(), Year.ToString(), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", ViewState["Emp_ID"].ToString() }, "dataset");
                    objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                    objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                    objdb.ByProcedure("SpFinChequeTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");

                    DataTable dt_LedgerTable = (DataTable)ViewState["LedgerTable"];


                    for (int i = 0; i < dt_LedgerTable.Rows.Count; i++)
                    {
                        string RowNo = dt_LedgerTable.Rows[i]["RowNo"].ToString();
                        string Ledger_ID = dt_LedgerTable.Rows[i]["Ledger_ID"].ToString();
                        string LedgerTx_MaintainType = dt_LedgerTable.Rows[i]["LedgerTx_MaintainType"].ToString();


                        string LedgerTx_Amount = "";

                        if (dt_LedgerTable.Rows[i]["Type"].ToString() == "Dr")
                        {
                            LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Debit"].ToString();
                            LedgerTx_Amount = "-" + LedgerTx_Amount;
                        }
                        else
                        {
                            LedgerTx_Amount = dt_LedgerTable.Rows[i]["LedgerTx_Credit"].ToString();
                        }

                        int LedgerId = int.Parse(dt_LedgerTable.Rows[i]["Ledger_ID"].ToString());

                        objdb.ByProcedure("SpFinLedgerTx",
              new string[] { "flag", "Ledger_ID", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "LedgerTx_MaintainType" },
              new string[] { "0", Ledger_ID, ViewState["VoucherTx_ID"].ToString(), "Contra", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), RowNo.ToString(), "Main Ledger", LedgerTx_MaintainType.ToString() }, "dataset");
                        if (LedgerTx_MaintainType == "Cheque")
                        {
                            DataSet dsFinChequeTx = (DataSet)ViewState["dsFinChequeTx"];

                            for (int j = 0; j < dsFinChequeTx.Tables.Count; j++)
                            {
                                if (dsFinChequeTx.Tables[j].TableName == RowNo.ToString())
                                {
                                    for (int k = 0; k < dsFinChequeTx.Tables[j].Rows.Count; k++)
                                    {
                                        string ChequeTx_No = dsFinChequeTx.Tables[j].Rows[k]["ChequeTx_No"].ToString();
                                        if (ChequeTx_No == "")
                                        {
                                            ChequeTx_No = null;
                                        }
                                        else
                                        {

                                        }
                                        string ChequeTx_Date = dsFinChequeTx.Tables[j].Rows[k]["ChequeTx_Date"].ToString();
                                        if (ChequeTx_Date == "")
                                        {
                                            ChequeTx_Date = null;
                                        }
                                        else
                                        {
                                            ChequeTx_Date = Convert.ToDateTime(ChequeTx_Date, cult).ToString("yyyy/MM/dd");
                                        }
                                        string ChequeTx_Amount = dsFinChequeTx.Tables[j].Rows[k]["ChequeTx_Amount"].ToString();
                                        objdb.ByProcedure("SpFinChequeTx",
                                        new string[] { "flag", "VoucherTx_ID", "Ledger_ID", "VoucherTx_Type", "ChequeTx_No", "ChequeTx_Date", "ChequeTx_Amount", "ChequeTx_Month", "ChequeTx_Year", "ChequeTx_FY", "Office_ID", "ChequeTx_IsActive", "ChequeTx_InsertedBy", "ChequeTx_OrderBy", "LedgerTx_OrderBy" },
                                        new string[] { "1", ViewState["VoucherTx_ID"].ToString(), Ledger_ID, "Contra", ChequeTx_No, ChequeTx_Date, ChequeTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), (k + 1).ToString(), RowNo.ToString() }, "dataset");
                                    }
                                }
                            }

                        }
                        else
                        {

                        }

                    }

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
            ddlcreditdebit.SelectedValue = "Cr";
            CreateLedgerTable();
            CreateDataSetFinChequeTx();
            ddlLedger_ID.ClearSelection();
            txtCurrentBalance.Text = "";
            txtLedgerTx_Amount.Text = "";
            btnAccept.Enabled = false;
            FillVoucherNo();
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
            ddlcreditdebit.Enabled = true;
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

                    //var rx = new System.Text.RegularExpressions.Regex("VR");
                    //string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //var array = rx.Split(str);
                    //lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //txtVoucherTx_No.Text = array[1];
                    //lblVoucherTx_No.Text = array[0] + "VR";
                    //txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    //txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
                  
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    GridViewLedgerDetail.DataSource = ds.Tables[1];
                    GridViewLedgerDetail.DataBind();
                    decimal LedgerCreditTotal = 0;
                    decimal LedgerDebitTotal = 0;
                    LedgerCreditTotal = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Credit"));
                    LedgerDebitTotal = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_Debit"));
                    GridViewLedgerDetail.FooterRow.Cells[3].Text = "<b>Total : </b>";
                    GridViewLedgerDetail.FooterRow.Cells[4].Text = "<b>" + LedgerDebitTotal.ToString() + "</b>";
                    GridViewLedgerDetail.FooterRow.Cells[5].Text = "<b>" + LedgerCreditTotal.ToString() + "</b>";
                    GridViewLedgerDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    GridViewLedgerDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
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
                    int gridRows = GridViewLedgerDetail.Rows.Count;
                    for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                    {
                        Label lblRowNumber = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("lblRowNumber");
                        Label Ledger_ID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_ID");
                        Label Type = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Type");
                        Label LedgerTx_MaintainType = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_MaintainType");
                        Label Ledger_Name = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_Name");
                        Label LedgerTx_Credit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Credit");
                        Label LedgerTx_Debit = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("LedgerTx_Debit");
                        //Label Ledger_TableID = (Label)GridViewLedgerDetail.Rows[rowIndex].Cells[0].FindControl("Ledger_TableID");

                        dt_LedgerTable.Rows.Add(lblRowNumber.Text, Ledger_ID.Text, Ledger_Name.Text, Type.Text, LedgerTx_MaintainType.Text, LedgerTx_Credit.Text, LedgerTx_Debit.Text);

                    }
                    ViewState["LedgerTable"] = dt_LedgerTable;



                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    DataSet dsFinChequeTx = (DataSet)ViewState["dsFinChequeTx"];
                    int rowcount = ds.Tables[2].Rows.Count;
                    for (int i = 0; i < rowcount; i++)
                    {
                        string TNO = ds.Tables[2].Rows[i]["LedgerTx_OrderBy"].ToString();
                        string Ledger_ID = ds.Tables[2].Rows[i]["Ledger_ID"].ToString();
                        string ChequeTx_No = ds.Tables[2].Rows[i]["ChequeTx_No"].ToString();
                        string ChequeTx_Date = ds.Tables[2].Rows[i]["ChequeTx_Date"].ToString();
                        string ChequeTx_Amount = ds.Tables[2].Rows[i]["ChequeTx_Amount"].ToString();
                        //string TNO = ds.Tables[2].Rows[i]["Ledger_ID"].ToString();

                        DataTable dt_FinChequeTx = new DataTable(TNO);
                        dt_FinChequeTx.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
                        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_No", typeof(string)));
                        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Date", typeof(string)));
                        dt_FinChequeTx.Columns.Add(new DataColumn("ChequeTx_Amount", typeof(decimal)));

                        dt_FinChequeTx.Rows.Add(Ledger_ID, ChequeTx_No, ChequeTx_Date, ChequeTx_Amount);
                        dsFinChequeTx.Merge(dt_FinChequeTx);
                        ViewState["dsFinChequeTx"] = dsFinChequeTx;

                    }



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
            GridViewLedgerDetail.Columns[6].Visible = true;
            GridViewLedgerDetail.Columns[7].Visible = false;
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
                new string[] { "14", "Contra", ViewState["Office_ID"].ToString() }, "dataset");

        if (ds.Tables[0].Rows.Count != 0)
        {
            txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
        }
    }

    protected void FillNarration()
    {
        try
        {
            //DataSet dsFinChequeTx = (DataSet)ViewState["dsFinChequeTx"];
            //if (dsFinChequeTx != null)
            //{
            //    int count = dsFinChequeTx.Tables.Count;
            //    if (count > 0)
            //    {
            //        for (int i = 0; i < count; i++)
            //        {
            //            if (dsFinChequeTx.Tables[i].Rows.Count > 0)
            //            {
            //                string ChequeTx_No = dsFinChequeTx.Tables[i].Rows[0]["ChequeTx_No"].ToString();
            //                string ChequeTx_Date = dsFinChequeTx.Tables[i].Rows[0]["ChequeTx_Date"].ToString();
            //                string ChequeTx_Amount = dsFinChequeTx.Tables[i].Rows[0]["ChequeTx_Amount"].ToString();
            //                if (ChequeTx_No == "")
            //                {
            //                    ChequeTx_No = "NA";
            //                }
            //                if (ChequeTx_Date == "")
            //                {
            //                    ChequeTx_Date = "NA";
            //                }
            //                txtVoucherTx_Narration.Text = "ChequeNo:" + ChequeTx_No + "  " + "ChequeDate:" + ChequeTx_Date + "  " + "ChequeAmount:" + ChequeTx_Amount;
            //                break;
            //            }
            //            else
            //            {
            //                txtVoucherTx_Narration.Text = "";
            //            }
            //        }
            //    }


            //}

            //else
            //{
            //    txtVoucherTx_Narration.Text = "";
            //}
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
                string[] LName = LedgerName.Split(new char[] { '&' });
                string LedgerTx_MaintainType = dt_LedgerTable.Rows[i]["LedgerTx_MaintainType"].ToString();
                decimal LedgerAmount = (LedgerTx_Credit + LedgerTx_Debit);
                Narration = Narration + "\r\n" + LName[0] + " Amount:" + LedgerAmount.ToString() + "\r\n";
                if (LedgerTx_MaintainType == "CostCentre")
                {
                    DataSet dsCostCentre = (DataSet)ViewState["dsCostCentre"];
                    int tcount = dsCostCentre.Tables[RowNo].Rows.Count;
                    for (int j = 0; j < tcount; j++)
                    {
                        string Category = dsCostCentre.Tables[RowNo].Rows[j]["CategoryName"].ToString();
                        string SubCateogry = dsCostCentre.Tables[RowNo].Rows[j]["SubCategoryName"].ToString();
                        string Amount = dsCostCentre.Tables[RowNo].Rows[j]["AmountShow"].ToString();
                        //Narration = Narration + "Category:" + Category + "  " + "SubCateogry:" + SubCateogry + "  " + "Amount:" + Amount + "\r\n";
                        Narration = Narration + (j + 1) + " " + Category + "  " + SubCateogry + "  " + "Amount : " + Amount + "\r\n";
                    }
                }
                else if (LedgerTx_MaintainType == "Cheque")
                {
                    DataSet dsFinChequeTx = (DataSet)ViewState["dsFinChequeTx"];
                    for (int k = 0; k < dsFinChequeTx.Tables[RowNo].Rows.Count; k++)
                    {
                        string ChequeTx_No = dsFinChequeTx.Tables[RowNo].Rows[k]["ChequeTx_No"].ToString();
                        if (ChequeTx_No == "")
                        {
                            ChequeTx_No = null;
                        }
                        else
                        {

                        }
                        string ChequeTx_Date = dsFinChequeTx.Tables[RowNo].Rows[k]["ChequeTx_Date"].ToString();
                        if (ChequeTx_Date == "")
                        {
                            ChequeTx_Date = null;
                        }
                        else
                        {
                            ChequeTx_Date = Convert.ToDateTime(ChequeTx_Date, cult).ToString("yyyy/MM/dd");
                        }
                        string ChequeTx_Amount = dsFinChequeTx.Tables[RowNo].Rows[k]["ChequeTx_Amount"].ToString();
                        Narration = Narration + ChequeTx_No + "  " + ChequeTx_Date + "  " + "Amount : " + ChequeTx_Amount + "\r\n";
                    }
                }
                txtVoucherTx_Narration.Text = Narration;
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
            // lblMsg.Text = "";
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
            if(ViewState["Office_ID"].ToString() == "1")
            {
                VoucherTx_Type = "Contra";
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

    protected void btnRefreshLedgerList_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            FillAccountDropDown();
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}