using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_VoucherPurchaseService : System.Web.UI.Page
{
    DataSet ds;
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
                   // ViewState["IGST"] = "737";
                     ViewState["IGST"] = "4";
                    ViewState["VoucherTx_ID"] = "0";
                    lblGrandTotal.Attributes.Add("readonly", "readonly");
                    //itempanel.Visible = false;
                    txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
                    //txtVoucherTx_Date.Enabled = false;
                    CreateTDSGSTTable();
                    FillDropDown();
                    FillPartyLedger();
                    FillState();
                    AddItem("NA");


                    ViewState["HSN_CGST"] = "0";
                    ViewState["HSN_SGST"] = "0";
                    ViewState["HSN_Code"] = "";
                    ViewState["HSN_IntegratedTax"] = "";
                    ViewState["Ledger_ID"] = "0";
                    ViewState["Typeofsupply"] = "";
                    ViewState["Type"] = "NA";
                    //txtVoucherTx_No.Attributes.Add("readonly", "readonly");
                    GridViewRef.DataSource = new string[] { };
                    GridViewRef.DataBind();
                    btnTDSView.Visible = false;
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

    //Fill PatryLedger DropDown
    protected void FillPartyLedger()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster",
                new string[] { "flag", "Office_ID" },
                new string[] { "44", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPartyName.DataSource = ds;
                ddlPartyName.DataTextField = "Ledger_Name";
                ddlPartyName.DataValueField = "Ledger_ID";
                ddlPartyName.DataBind();
                ddlPartyName.Items.Insert(0, "Select");


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

    //Fill Ledger DropDown in AMOUNT Section
    protected void FillDropDown()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster",
                new string[] { "flag", "Office_ID", "MultipleHeadIDs" },
                new string[] { "22", ViewState["Office_ID"].ToString(), "1,2,3,4" }, "dataset");
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {

                    ddlLedger.DataTextField = "Ledger_Name";
                    ddlLedger.DataValueField = "Ledger_ID";
                    ddlLedger.DataSource = ds.Tables[1];
                    ddlLedger.DataBind();
                    ddlLedger.Items.Insert(0, new ListItem("Select", "0"));


                }

            }
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

    //Ledger Current Balance
    protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillCurrentBalance();
            txtSuplierName.Text = ddlPartyName.SelectedItem.Text;
            DataSet ds1 = objdb.ByProcedure("SpFinServiceSupplierDetail", new string[] { "flag", "Ledger_ID" }, new string[] { "3", ddlPartyName.SelectedValue.ToString() }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                txtsupplieraddress.Text = ds1.Tables[0].Rows[0]["Mailing_Address"].ToString();
                ddlState.ClearSelection();
                ddlState.Items.FindByValue(ds1.Tables[0].Rows[0]["State_ID"].ToString()).Selected = true;
                txtCity.Text = ds1.Tables[0].Rows[0]["City"].ToString();
                ddlRegistrationType.ClearSelection();
                ddlRegistrationType.Items.FindByText(ds1.Tables[0].Rows[0]["RegistrationTypes"].ToString()).Selected = true;
                txtGSTNo.Text = ds1.Tables[0].Rows[0]["GST_No"].ToString();

            }
            GridViewBillByBillDetail.DataSource = new string[] { };
            GridViewBillByBillDetail.DataBind();
            GridViewChequeDetail.DataSource = new string[] { };
            GridViewChequeDetail.DataBind();
            ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "21", ddlPartyName.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "Yes")
                {
                    BillByBillDetail.Visible = true;
                    pnlChequeDetail.Visible = false;
                }
                else if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "BankLedger")
                {
                    BillByBillDetail.Visible = false;
                    pnlChequeDetail.Visible = true;
                }
                else
                {
                    BillByBillDetail.Visible = false;
                    pnlChequeDetail.Visible = false;
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

                //DataSet ds1 = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "1", ddlPartyName.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                //if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                //{
                //    txtCurrentBalance.Text = ds1.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
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
                                new string[] { "11", ViewState["Office_ID"].ToString(), ddlPartyName.SelectedValue.ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), FinancialYear, Convert.ToDateTime(FY_StartDate, cult).ToString("yyyy/MM/dd") }, "dataset");
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

    //Fill State Dropdown in Supplier Detail
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
            string[] FinY = FinancialYear.Split('-');
            // if (int.Parse(FinY[0]) >= 2022)
            // {
                // ViewState["CGST"] = "46718";
                // ViewState["SGST"] = "46719";
                // ViewState["IGST"] = "46720";

            // }
            // else
            // {
                // ViewState["CGST"] = "1";
                // ViewState["SGST"] = "2";
                // ViewState["IGST"] = "737";
            // }
			ViewState["CGST"] = "1";
            ViewState["SGST"] = "2";
            ViewState["IGST"] = "4";
            //string VoucherTx_Names_ForSno = "'Payment,Journal,Contra'";
            //string VoucherTx_Names_ForSno = "'Receipt'";
            string VoucherTx_Names_ForSno = "Purchase Voucher";
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
            //VoucherTx_SNo++;
            ViewState["VoucherTx_SNo"] = VoucherTx_SNo;
            lblVoucherTx_No.Text = Office_Code + FinancialYear.ToString().Substring(2) + "VR";
            // txtVoucherTx_No.Text = VoucherTx_SNo.ToString();
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

            if (ID != "0")
            {

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
                dt_GridViewLedger.Columns.Add(new DataColumn("Isreversechargeapplicable", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("GSTApplicable", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("IsIneligibleforinputcredit", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("Taxbility", typeof(string)));


                int gridRows = GridViewLedger.Rows.Count;
                if (gridRows > 3)
                {
                    for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
                    {
                        //if (rowIndex > 3)
                        //{
                        //    status = "1";
                        //}
                        //else
                        //{
                        //    status = "0";
                        //}
                        Label lblID = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblID");
                        Label lblLedgerName = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblLedgerName");
                        Label lblHSNCode = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblHSNCode");
                        Label lblCGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTPer");
                        Label lblSGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTPer");
                        Label lblIGSTPer = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTPer");
                        Label lblCGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblCGSTAmt");
                        Label lblSGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblSGSTAmt");
                        Label lblIGSTAmt = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblIGSTAmt");
                        Label lblrcm = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblrcm");
                        TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                        Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                        Label lblneligibleforinputcredit = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblneligibleforinputcredit");
                        Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                        if (lblID.Text == "1" || lblID.Text == "2" || lblID.Text == "3" || lblID.Text == "4")
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
                                if (dr["LedgerName"].ToString() == "IGST" || dr["LedgerName"].ToString() == "IGST OUTPUT") // if id==2
                                {
                                    dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + decimal.Parse(lblIGSTAmt.Text); //change the name
                                    //break; break or not depending on you
                                }
                            }
                        }
                        //    dt_GridViewLedger.Rows.Add(lblID.Text, lblLedgerName.Text, txtAmount.Text, lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, status, lblrcm.Text, lblGSTApplicable.Text, lblneligibleforinputcredit.Text, lblTaxbility.Text);
                        //    foreach (DataRow dr in dt_GridViewLedger.Rows) // search whole table
                        //    {
                        //        if (dr["LedgerName"].ToString() == "CGST" || dr["LedgerName"].ToString() == "CGST INPUT") // if id==2
                        //        {
                        //            dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + decimal.Parse(lblCGSTAmt.Text); //change the name
                        //            //break; break or not depending on you
                        //        }
                        //        if (dr["LedgerName"].ToString() == "SGST" || dr["LedgerName"].ToString() == "SGST INPUT") // if id==2
                        //        {
                        //            dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + decimal.Parse(lblSGSTAmt.Text); //change the name
                        //            //break; break or not depending on you
                        //        }
                        //        if (dr["LedgerName"].ToString() == "IGST" || dr["LedgerName"].ToString() == "IGST INPUT") // if id==2
                        //        {
                        //            dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + decimal.Parse(lblIGSTAmt.Text); //change the name
                        //            //break; break or not depending on you
                        //        }
                        //    }
                        //}
                        
                        
                    }
                    decimal CGST = dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    decimal SGST = dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    decimal IGST = dt_GridViewLedger.AsEnumerable().Sum(row => row.Field<decimal>("IGSTAmt"));
                    if (ViewState["CGST"].ToString() == "46718")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST INPUT", CGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "");
                    }
                    else
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST", CGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "");
                    }
                    if (ViewState["SGST"].ToString() == "46719")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST INPUT", SGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "");
                    }
                    else
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST", SGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "");
                    }
                    if (ViewState["IGST"].ToString() == "46720")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["IGST"].ToString(), "IGST INPUT", IGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "");
                    }
                    else
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["IGST"].ToString(), "IGST", IGST, HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "");
                    }
                    dt_GridViewLedger.Rows.Add(ViewState["RoundOff"].ToString(), "Round off", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, null, null);
                }
                else
                {
                    if (ViewState["CGST"].ToString() == "46718")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST INPUT", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "");
                    }
                    else
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["CGST"].ToString(), "CGST", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "");
                    }
                    if (ViewState["SGST"].ToString() == "46719")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST INPUT", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "");
                    }
                    else
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["SGST"].ToString(), "SGST", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "");
                    }
                    if (ViewState["IGST"].ToString() == "46720")
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["IGST"].ToString(), "IGST INPUT", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "");
                    }
                    else
                    {
                        dt_GridViewLedger.Rows.Add(ViewState["IGST"].ToString(), "IGST", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "");
                    }
                   
                   
                    

                    dt_GridViewLedger.Rows.Add(ViewState["RoundOff"].ToString(), "Round off", "0", HSN_Code, CGST_Per, SGST_Per, IGST_Per, CGSTAmt, SGSTAmt, IGSTAmt, status, "", "", "", "");
                }

                GridViewLedger.DataSource = dt_GridViewLedger;
                GridViewLedger.DataBind();


                ViewState["RowNo"] = "0";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                // GridViewLedger
            }
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

            string msg = "";
            int LedgerStatus = 0;
            lblMsg.Text = "";
            lblGSTModal.Text = "";
            string TDS = "0";
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
                        if (lblLedgerID.Text == "6")
                        {
                            TDS = "1";
                        }

                    }
                }
                if (LedgerStatus == 0)
                {
                    if (ddlLedger.SelectedValue.ToString() == "6")
                    {

                        CreateTDSGSTTable();
                        ClearTDSGST();
                        ds = objdb.ByProcedure("SpFinTDSGSTEntry", new string[] { "flag", "Head_ID", "Office_ID" }, new string[] { "11", "115", ViewState["Office_ID"].ToString() }, "dataset");
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            ddlPartyNameTDS.DataTextField = "Ledger_Name";
                            ddlPartyNameTDS.DataValueField = "Ledger_ID";
                            ddlPartyNameTDS.DataSource = ds;
                            ddlPartyNameTDS.DataBind();
                            ddlPartyNameTDS.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        if (gvTdsGSTDetail.Rows.Count > 0)
                        {
                            //btnFSubmit.Visible = true;
                        }

                        if (TDS == "0")
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
                        FillLedgerAmount("0");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                        ViewState["GrandTotal"] = lblGrandTotal.Text;
                        //btnAcceptEnable();
                        ddlLedger.ClearSelection();
                        txtLedgerAmt.Text = "";
                    }

                    //FillLedgerAmount("0");
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
                    //ViewState["GrandTotal"] = lblGrandTotal.Text;
                    ////btnAcceptEnable();
                    //ddlLedger.ClearSelection();
                    //txtLedgerAmt.Text = "";


                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Ledger already exists');", true);
                }
                //FillLedgerAmount("0");
                //ddlLedger.ClearSelection();
                //txtLedgerAmt.Text = "";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
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

        btnTDSView.Visible = false;
        gvTdsGSTDetail.DataSource = dt_TdsGSTTable;
        gvTdsGSTDetail.DataBind();
    }
    protected void ClearTDSGST()
    {
        ddlPartyNameTDS.ClearSelection();
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
    protected void btnTDSSave_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            lblGSTModal.Text = "";
            DataTable dt_TdsGSTTable = (DataTable)ViewState["TdsGSTTable"];
            if (ddlPartyNameTDS.SelectedIndex == 0)
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
                dt_TdsGSTTable.Rows.Add(null, ddlPartyNameTDS.SelectedValue.ToString(), ddlPartyNameTDS.SelectedItem.Text, txtTdsGSTNo.Text, txtBillNo.Text, Convert.ToDateTime(txtBillDate.Text, cult).ToString("dd/MM/yyyy"), Convert.ToDecimal(txtTdsBillAmount.Text).ToString("0.00"), Convert.ToDecimal(txtPaymentAmountNTBD.Text).ToString("0.00"), Convert.ToDecimal(txtBasicAmount.Text).ToString("0.00"), Convert.ToDecimal(txtGSTTDSAmount.Text).ToString("0.00"), Convert.ToDecimal(txtCGST.Text).ToString("0.00"), Convert.ToDecimal(txtSGST.Text).ToString("0.00"), Convert.ToDecimal(txtIGST.Text).ToString("0.00"));

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
                    if (decimal.Parse(GSTTDSAmount.ToString()) == decimal.Parse(txtLedgerAmt.Text))
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
    protected void btnFSubmit_Click(object sender, EventArgs e)
    {
        FillLedgerAmount("0");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CalculateGrandTotal();", true);
        ViewState["GrandTotal"] = lblGrandTotal.Text;
        //btnAcceptEnable();
        ddlLedger.ClearSelection();
        txtLedgerAmt.Text = "";

        btnTDSView.Visible = true;
    }
    protected void FillLedgerAmount(string ID)
    {

        //string Type = "NA";
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
                dt_GridViewLedger.Columns.Add(new DataColumn("Isreversechargeapplicable", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("GSTApplicable", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("IsIneligibleforinputcredit", typeof(string)));
                dt_GridViewLedger.Columns.Add(new DataColumn("Taxbility", typeof(string)));
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
                ds = objdb.ByProcedure("SpFinLedgerGSTDetails", new string[] { "flag", "Ledger_ID", "VoucherTx_Date" }, new string[] { "3", ddlLedger.SelectedValue, Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "true" && ds.Tables[0].Rows[0]["GSTApplicable"].ToString() == "Yes")
                    {
                        Isreversechargeapplicable = ds.Tables[0].Rows[0]["Isreversechargeapplicable"].ToString();
                        if (ds.Tables[0].Rows[0]["GSTApplicable"].ToString() != "")
                        {
                            GSTApplicable = ds.Tables[0].Rows[0]["GSTApplicable"].ToString();
                        }
                        Taxbility = ds.Tables[0].Rows[0]["Taxbility"].ToString();
                        IsIneligibleforinputcredit = ds.Tables[0].Rows[0]["IsIneligibleforinputcredit"].ToString();
                        if (ddlState.SelectedIndex == 0)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select State First');", true);
                        }
                        else
                        {
                            CGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                            SGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                            IGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_IntegratedTax"].ToString());
                            if (int.Parse(ddlState.SelectedValue.ToString()) != 12)
                            {
                                CGST = 0;
                                SGST = 0;
                            }
                            else
                            {
                                IGST = 0;
                            }
                            decimal Amount = decimal.Parse(txtLedgerAmt.Text);
                            CGSTAmt = Math.Round((Amount * CGST) / 100, 2);
                            SGSTAmt = Math.Round((Amount * SGST) / 100, 2);
                            IGSTAmt = Math.Round((Amount * IGST) / 100, 2);

                            //if (ds.Tables[0].Rows[0]["Isreversechargeapplicable"].ToString() == "Yes")
                            //{
                            //    CGST = 0;
                            //    SGST = 0;
                            //    IGST = 0;
                            //}
                            //else
                            //{
                            //    CGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                            //    SGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_CGST"].ToString());
                            //    IGST = decimal.Parse(ds.Tables[0].Rows[0]["HSN_IntegratedTax"].ToString());
                            //    if (int.Parse(ddlState.SelectedValue.ToString()) != 12)
                            //    {
                            //        CGST = 0;
                            //        SGST = 0;
                            //    }
                            //    else
                            //    {
                            //        IGST = 0;
                            //    }
                            //}


                            HSN_Code = ds.Tables[0].Rows[0]["HSN_Code"].ToString();
                            dt_GridViewLedger.Rows.Add(ddlLedger.SelectedValue.ToString(), ddlLedger.SelectedItem.ToString(), txtLedgerAmt.Text, HSN_Code, CGST, SGST, IGST, CGSTAmt, SGSTAmt, IGSTAmt, "1", Isreversechargeapplicable.ToString(), GSTApplicable, IsIneligibleforinputcredit, Taxbility);

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
                                Label lblrcm = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblrcm");
                                Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                                Label lblneligibleforinputcredit = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblneligibleforinputcredit");
                                Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                                if (lblID.Text == "1" || lblID.Text == "2" || lblID.Text == "3" || lblID.Text == "4")
                                {
                                    status = "0";
                                }
                                else
                                {
                                    status = "1";
                                }
                                dt_GridViewLedger.Rows.Add(lblID.Text, lblLedgerName.Text, txtAmount.Text, lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, status, lblrcm.Text, lblGSTApplicable.Text, lblneligibleforinputcredit.Text, lblTaxbility.Text);
                            }

                            foreach (DataRow dr in dt_GridViewLedger.Rows) // search whole table
                            {
                                if (dr["LedgerName"].ToString() == "CGST" || dr["LedgerName"].ToString() == "CGST INPUT") // if id==2
                                {
                                    if (Isreversechargeapplicable == "Yes")
                                    {

                                    }
                                    else
                                    {
                                        dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + CGSTAmt;
                                    }
                                    //change the name
                                    //break; break or not depending on you
                                }
                                if (dr["LedgerName"].ToString() == "SGST" || dr["LedgerName"].ToString() == "SGST INPUT") // if id==2
                                {
                                    if (Isreversechargeapplicable == "Yes")
                                    {

                                    }
                                    else
                                    {
                                        dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + SGSTAmt; //change the name
                                        //break; break or not depending on you
                                    }

                                }
                                if (dr["LedgerName"].ToString() == "IGST" || dr["LedgerName"].ToString() == "IGST INPUT") // if id==2
                                {
                                    if (Isreversechargeapplicable == "Yes")
                                    {

                                    }
                                    else
                                    {
                                        dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) + IGSTAmt; //change the name
                                        //break; break or not depending on you
                                    }

                                }
                            }
                            ViewState["LedgerAmount"] = dt_GridViewLedger;
                            GridViewLedger.DataSource = dt_GridViewLedger;
                            GridViewLedger.DataBind();
                        }

                    }
                    else
                    {
                        dt_GridViewLedger.Rows.Add(ddlLedger.SelectedValue.ToString(), ddlLedger.SelectedItem.ToString(), txtLedgerAmt.Text, HSN_Code, CGST, SGST, IGST, CGSTAmt, SGSTAmt, IGSTAmt, "1", Isreversechargeapplicable.ToString(), GSTApplicable, IsIneligibleforinputcredit, Taxbility);
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
                            Label lblrcm = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblrcm");
                            Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                            Label lblneligibleforinputcredit = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblneligibleforinputcredit");
                            Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                            if (lblID.Text == "1" || lblID.Text == "2" || lblID.Text == "3" || lblID.Text == "4")
                            {
                                status = "0";
                            }
                            else
                            {
                                status = "1";
                            }
                            dt_GridViewLedger.Rows.Add(lblID.Text, lblLedgerName.Text, txtAmount.Text, lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, status, lblrcm.Text, lblGSTApplicable.Text, lblneligibleforinputcredit.Text, lblTaxbility.Text);
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
                    Label lblrcm = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblrcm");
                    Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                    Label lblneligibleforinputcredit = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblneligibleforinputcredit");
                    Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");


                  

                    if (int.Parse(lblID.Text) == int.Parse(ID))
                    {
                        for (int i = dt_GridViewLedger.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow dr = dt_GridViewLedger.Rows[i];
                            if (dr["LedgerID"].ToString() == ID)
                            {
                                if (dr["LedgerID"].ToString() == "6")
                                {
                                    CreateTDSGSTTable();
                                }
                                   
                                dr.Delete();

                                
                            }

                        }
                        dt_GridViewLedger.AcceptChanges();
                        foreach (DataRow dr in dt_GridViewLedger.Rows) // search whole table
                        {
                            if (dr["LedgerName"].ToString() == "CGST" || dr["LedgerName"].ToString() == "CGST INPUT") // if id==2
                            {
                                if (lblrcm.Text == "Yes")
                                {

                                }
                                else
                                {
                                    dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) - decimal.Parse(lblCGSTAmt.Text); //change the name
                                    //break; break or not depending on you
                                }

                            }
                            if (dr["LedgerName"].ToString() == "SGST" || dr["LedgerName"].ToString() == "SGST INPUT") // if id==2
                            {
                                if (lblrcm.Text == "Yes")
                                {

                                }
                                else
                                {
                                    dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) - decimal.Parse(lblSGSTAmt.Text); //change the name
                                    //break; break or not depending on you
                                }

                            }
                            if (dr["LedgerName"].ToString() == "IGST" || dr["LedgerName"].ToString() == "IGST INPUT") // if id==2
                            {
                                if (lblrcm.Text == "Yes")
                                {

                                }
                                else
                                {
                                    dr["Amount"] = decimal.Parse(dr["Amount"].ToString()) - decimal.Parse(lblIGSTAmt.Text); //change the name
                                    //break; break or not depending on you
                                }

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
                txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
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
            ViewState["Amount"] = Math.Abs(Convert.ToDecimal(lblGrandTotal.Text));
            FillRefAmount("0");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }


    }
    protected void FillRefAmount(string ID)
    {
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
                    if (lblType.Text == "Cr")
                    {
                        if (lblGrandTotal.Text.Contains("-"))
                        {
                            Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) + Convert.ToDecimal(lblAmount.Text));
                            ViewState["Amount"] = Amount.ToString();
                            lblAmount.Text = ViewState["Amount"].ToString();
                        }
                        else
                        {
                            Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(lblAmount.Text));
                            ViewState["Amount"] = Amount.ToString();
                            lblAmount.Text = ViewState["Amount"].ToString();
                        }

                    }
                    else
                    {
                        if (lblGrandTotal.Text.Contains("-"))
                        {
                            Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(lblAmount.Text));
                            ViewState["Amount"] = Amount.ToString();
                            lblAmount.Text = ViewState["Amount"].ToString();
                        }
                        else
                        {
                            Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) + Convert.ToDecimal(lblAmount.Text));
                            ViewState["Amount"] = Amount.ToString();
                            lblAmount.Text = ViewState["Amount"].ToString();
                        }

                    }
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
                if (ddlBillByBillTx_crdr.SelectedValue == "Cr")
                {
                    if (lblGrandTotal.Text.Contains("-"))
                    {
                        Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) + Convert.ToDecimal(txtBillByBillTx_Amount.Text));
                        ViewState["Amount"] = Amount.ToString();
                        txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();
                    }
                    else
                    {
                        Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(txtBillByBillTx_Amount.Text));
                        ViewState["Amount"] = Amount.ToString();
                        txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();
                    }

                }
                else
                {

                    if (lblGrandTotal.Text.Contains("-"))
                    {
                        Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(txtBillByBillTx_Amount.Text));
                        ViewState["Amount"] = Amount.ToString();
                        txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();
                    }
                    else
                    {
                        Decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) + Convert.ToDecimal(txtBillByBillTx_Amount.Text));
                        ViewState["Amount"] = Amount.ToString();
                        txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();
                    }


                }
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

            //decimal RefTotal = 0;
            //RefTotal = dt_BillByBillTable.AsEnumerable().Sum(row => row.Field<decimal>("BillByBillTx_Amount"));

            //GridViewBillByBillDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
            //GridViewBillByBillDetail.FooterRow.Cells[3].Text = "<b>" + RefTotal.ToString() + "</b>";

            //txtBillByBillTx_Amount.Text = (Convert.ToDecimal(txtLedgerTx_Amount.Text) - RefTotal).ToString();
            txtBillByBillTx_Ref.Text = "";
            ddlRefType.ClearSelection();
            ViewState["BillByBillTable"] = dt_BillByBillTable;
            if (decimal.Parse(ViewState["Amount"].ToString()) == 0)
            {
                Save("BillByBill");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);
            }
            //ViewState["BillByBillTable"] = dt_BillByBillTable;



            txtBillByBillTx_Ref.Visible = true;
            txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
            //BindBillByBillData();
            ddlRefType.ClearSelection();
            ddlBillByBillTx_crdr.SelectedValue = "Cr";
            txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();
            txtBillByBillTx_Ref.Enabled = true;
            ddlBillByBillTx_Ref.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
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
            //ViewState["BillByBillTable"] = dt_BillByBillTable;




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
            txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
            txtBillByBillTx_Ref.Visible = true;
            ddlBillByBillTx_Ref.Visible = false;
            txtBillByBillTx_Ref.Enabled = true;
            lnkView.Visible = false;
            string VoucherTx_No = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
            lblMsg.Text = "";
            string msg = "";
            string msg1 = "";
            string GSTApplicable = "";
            if (txtVoucherTx_No.Text == "")
            {
                msg += "Enter Voucher No. \\n";
            }
            if (txtSupplierInvoiceDate.Text == "")
            {
                msg += "Enter Supplier's Invoice Date. \\n";
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
            if (ddlPartyName.SelectedIndex == 0)
            {
                msg += "Select Party A/c Name. \\n";
            }

            if (txtVoucherTx_Narration.Text == "")
            {
                msg += "Enter Narration. \\n";
            }
            if (ddlRegistrationType.SelectedIndex > 0)
            {
                if (ddlRegistrationType.SelectedItem.Text == "Composition" || ddlRegistrationType.SelectedItem.Text == "Regular")
                {
                    if (txtGSTNo.Text.Trim() == "")
                    {
                        msg += "Enter GST No";
                        gstvisible.Visible = true;
                    }

                }
                else
                {
                    gstvisible.Visible = false;
                }
            }
            /*if(GridViewLedger.Rows.Count < 4)
            {
                msg += "Enter Ledger Detail. \\n";
            }
            if (GridViewLedger.Rows.Count >3)
            {
                foreach (GridViewRow rows in GridViewLedger.Rows)
                {
                    Label lblID = (Label)rows.FindControl("lblID");
                    ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID" }, new string[] { "49", lblID.Text }, "dataset");
                    {
                        if(ds.Tables[0].Rows[0]["GSTApplicable"].ToString() == "Yes")
                        {
                            GSTApplicable = "Yes";
                            break;
                        }
                    }
                }
                if(GSTApplicable == "Yes")
                {

                }
                else
                {
                    msg += "Select One Service Ledger. \\n";
                }

            }*/
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
                            txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;
                            txtBillByBillTx_Amount.Text = ViewState["Amount"].ToString();

                            ddlRefType.SelectedValue = "2";
                            //    txtRefAmount.Text = lblGrandTotal.Text;
                            // CreateBillByBillTable();
                            GridViewRef.DataSource = new string[] { };
                            GridViewRef.DataBind();
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
                        else
                        {
                            Save("None");
                        }

                    }



                }
                else if (btnAccept.Text == "Update")
                {
                    if (ViewState["Amount"].ToString() == (Math.Abs(Convert.ToDecimal(lblGrandTotal.Text))).ToString() && ViewState["Ledger_ID"].ToString() == ddlPartyName.SelectedValue.ToString())
                    {
                        txtBillByBillTx_Amount.Text = "0.00";
                        btnAddBillByBill.Enabled = false;
                    }
                    else
                    {
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
                                GridViewRef.DataSource = new string[] { };
                                GridViewRef.DataBind();
                                //decimal Amount = Math.Abs(Convert.ToDecimal(ViewState["Amount"]) - Convert.ToDecimal(lblGrandTotal.Text));
                                txtBillByBillTx_Amount.Text = lblGrandTotal.Text;
                                btnAddBillByBill.Enabled = true;
                                txtBillByBillTx_Ref.Text = lblVoucherTx_No.Text + txtVoucherTx_No.Text;

                                //    txtRefAmount.Text = lblGrandTotal.Text;
                                // CreateBillByBillTable();

                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);


                            }
                            else if (ds.Tables[0].Rows[0]["LedgerType"].ToString() == "BankLedger")
                            {
                                btnAddCheque.Enabled = true;
                                //btnAddChequeDetail.Enabled = false;
                                CreatTableFinChequeTx();
                                GVFinChequeTx.DataSource = new string[] { };
                                GVFinChequeTx.DataBind();
                                txtChequeTx_Amount.Text = lblGrandTotal.Text;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalChequeDetail();", true);
                            }
                            else
                            {
                                Save("None");
                            }



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


            string VoucherTx_Type = "GSTService Purchase";
            string VoucherTx_Name = "Purchase Voucher";
            string VoucherTx_Ref = txtInvoice.Text;
            string VoucherTx_Amount = lblGrandTotal.Text;
            string ItemTx_IsActive = "1";
            string LedgerTx_IsActive = "1";
            string VoucherTx_IsActive = "1";
            string Supplier_IsActive = "1";

            if (btnAccept.Text == "Accept")
            {
                ItemTx_IsActive = "0";
                LedgerTx_IsActive = "0";
                VoucherTx_IsActive = "0";
                Supplier_IsActive = "0";
                string VoucherTx_ID = "0";
                DataSet ds2 = objdb.ByProcedure("SpFinVoucherTx",
        new string[] { "flag","VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type"
                        , "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY"
                        , "VoucherTx_IsActive", "VoucherTx_InsertedBy","VoucherTx_SupplierinvoiceDate","GSTVoucher" },
        new string[] { "0", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), VoucherTx_Name, VoucherTx_Type,VoucherTx_No
                       ,VoucherTx_Ref,txtVoucherTx_Narration.Text,lblGrandTotal.Text,Month.ToString(),Year.ToString(), ViewState["Office_ID"].ToString(),FinancialYear.ToString()
                        ,VoucherTx_IsActive,ViewState["Emp_ID"].ToString(),Convert.ToDateTime(txtSupplierInvoiceDate.Text, cult).ToString("yyyy/MM/dd"),"Yes"}, "dataset");


                if (ds2.Tables[0].Rows.Count > 0)
                {
                    VoucherTx_ID = ds2.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
                    objdb.ByProcedure("SpFinServiceSupplierDetail", new string[] { "flag", "VoucherTx_ID", "SupplierName", "SupplierAddress", "State_ID", "City", "RegistrationTypes", "GST_No", "Supplier_IsActive", "UpdatedBy" }, new string[] { "0", VoucherTx_ID, txtSuplierName.Text, txtsupplieraddress.Text, ddlState.SelectedValue, txtCity.Text, ddlRegistrationType.SelectedItem.Text, txtGSTNo.Text, Supplier_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");
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
                        Label lblrcm = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblrcm");
                        TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                        Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                        Label lblneligibleforinputcredit = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblneligibleforinputcredit");
                        Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                        if (txtAmount.Text.Contains("-"))
                        {
                            LedgerTx_Amount = txtAmount.Text.Replace(@"-", string.Empty);
                        }
                        else
                        {
                            LedgerTx_Amount = "-" + txtAmount.Text;
                        }

                        // string LedgerTx_Amount = "-" + txtAmount.Text;
                        objdb.ByProcedure("SpFinLedgerTx",
                        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "IsIneligibleforinputcredit", "Taxbility" },
                        new string[] { "0", lblID.Text, "Sub Ledger", VoucherTx_ID, VoucherTx_Type, LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 1).ToString(), lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, lblrcm.Text, lblGSTApplicable.Text, lblneligibleforinputcredit.Text, lblTaxbility.Text }, "dataset");

                    }
                    //DataSet DS_GridViewParticulars = (DataSet)ViewState["DS_GridViewParticulars"];
                    //for (int i = 0; i < DS_GridViewParticulars.Tables.Count; i++)
                    //{
                    //    for (int j = 0; j < DS_GridViewParticulars.Tables[i].Rows.Count; j++)
                    //    {
                    //        string ParticularID = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularID"].ToString();
                    //        string Amount = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularAmt"].ToString();
                    //        string Item_id = DS_GridViewParticulars.Tables[i].Rows[j]["Item_ID"].ToString();
                    //        Amount = "-" + Amount;
                    //        objdb.ByProcedure("SpFinLedgerTx",
                    //        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
                    //        new string[] { "0", ParticularID, "Item Ledger", VoucherTx_ID, Item_id, VoucherTx_Type, Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (j + 1).ToString() }, "dataset");
                    //    }
                    //}
                    if (Type == "BillByBill")
                    {

                        objdb.ByProcedure("SpFinLedgerTx",
                   new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
                   new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
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
                   new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
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
                                ChequeTx_No.Text = null;
                            }
                            else
                            {

                            }

                            if (ChequeTx_Date.Text == "")
                            {
                                ChequeTx_Date.Text = null;
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
                    else
                    {

                        objdb.ByProcedure("SpFinLedgerTx",
                   new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
                   new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",VoucherTx_ID,VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","None"}, "dataset");
                    }
                    objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "40", VoucherTx_ID }, "dataset");

                }

                SaveTDSGSTData(VoucherTx_ID);

                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                ClearData();
                CreateTDSGSTTable();
                ClearTDSGST();
            }
            else if (btnAccept.Text == "Update")
            {

                objdb.ByProcedure("SpFinVoucherTx",
        new string[] { "flag","VoucherTx_ID","VoucherTx_Date", "VoucherTx_Name", "VoucherTx_Type"
                        , "VoucherTx_No", "VoucherTx_Ref", "VoucherTx_Narration", "VoucherTx_Amount", "VoucherTx_Month", "VoucherTx_Year", "Office_ID", "VoucherTx_FY"
                        , "VoucherTx_IsActive", "VoucherTx_InsertedBy","VoucherTx_SupplierinvoiceDate" },
        new string[] { "7",ViewState["VoucherTx_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd"), VoucherTx_Name, VoucherTx_Type,VoucherTx_No
                       ,VoucherTx_Ref,txtVoucherTx_Narration.Text,lblGrandTotal.Text,Month.ToString(),Year.ToString(), ViewState["Office_ID"].ToString(),FinancialYear.ToString()
                        ,VoucherTx_IsActive,ViewState["Emp_ID"].ToString(),Convert.ToDateTime(txtSupplierInvoiceDate.Text, cult).ToString("yyyy/MM/dd")}, "dataset");
                objdb.ByProcedure("SpFinServiceSupplierDetail", new string[] { "flag", "VoucherTx_ID", "SupplierName", "SupplierAddress", "State_ID", "City", "RegistrationTypes", "GST_No", "Supplier_IsActive", "UpdatedBy" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString(), txtSuplierName.Text, txtsupplieraddress.Text, ddlState.SelectedValue, txtCity.Text, ddlRegistrationType.SelectedItem.Text, txtGSTNo.Text, Supplier_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");
                objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
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
                    Label lblrcm = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblrcm");
                    TextBox txtAmount = (TextBox)GridViewLedger.Rows[rowIndex].Cells[1].FindControl("txtAmount");
                    Label lblGSTApplicable = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblGSTApplicable");
                    Label lblneligibleforinputcredit = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblneligibleforinputcredit");
                    Label lblTaxbility = (Label)GridViewLedger.Rows[rowIndex].Cells[0].FindControl("lblTaxbility");
                    if (txtAmount.Text.Contains("-"))
                    {
                        LedgerTx_Amount = txtAmount.Text.Replace(@"-", string.Empty);
                    }
                    else
                    {
                        LedgerTx_Amount = "-" + txtAmount.Text;
                    }

                    // string LedgerTx_Amount = "-" + txtAmount.Text;
                    objdb.ByProcedure("SpFinLedgerTx",
                    new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "HSN_Code", "CGST_Per", "SGST_Per", "IGST_Per", "CGSTAmt", "SGSTAmt", "IGSTAmt", "Isreversechargeapplicable", "GSTApplicable", "IsIneligibleforinputcredit", "Taxbility" },
                    new string[] { "0", lblID.Text, "Sub Ledger", ViewState["VoucherTx_ID"].ToString(), VoucherTx_Type, LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (rowIndex + 1).ToString(), lblHSNCode.Text, lblCGSTPer.Text, lblSGSTPer.Text, lblIGSTPer.Text, lblCGSTAmt.Text, lblSGSTAmt.Text, lblIGSTAmt.Text, lblrcm.Text, lblGSTApplicable.Text, lblneligibleforinputcredit.Text, lblTaxbility.Text }, "dataset");

                }

                //DataSet DS_GridViewParticulars = (DataSet)ViewState["DS_GridViewParticulars"];
                //for (int i = 0; i < DS_GridViewParticulars.Tables.Count; i++)
                //{
                //    for (int j = 0; j < DS_GridViewParticulars.Tables[i].Rows.Count; j++)
                //    {
                //        string ParticularID = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularID"].ToString();
                //        string Amount = DS_GridViewParticulars.Tables[i].Rows[j]["ParticularAmt"].ToString();
                //        string Item_id = DS_GridViewParticulars.Tables[i].Rows[j]["Item_ID"].ToString();
                //        Amount = "-" + Amount;
                //        objdb.ByProcedure("SpFinLedgerTx",
                //        new string[] { "flag", "Ledger_ID", "LedgerTx_Type", "VoucherTx_ID", "Item_id", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy" },
                //        new string[] { "0", ParticularID, "Item Ledger", ViewState["VoucherTx_ID"].ToString(), Item_id, VoucherTx_Type, Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), (j + 1).ToString() }, "dataset");
                //    }
                //}
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
               new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",ViewState["VoucherTx_ID"].ToString(),VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
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
               new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",ViewState["VoucherTx_ID"].ToString(),VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
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
                            ChequeTx_No.Text = null;
                        }
                        else
                        {

                        }

                        if (ChequeTx_Date.Text == "")
                        {
                            ChequeTx_Date.Text = null;
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
                else
                {

                    objdb.ByProcedure("SpFinLedgerTx",
               new string[] { "flag", "Ledger_ID","LedgerTx_Type", "VoucherTx_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year"
                            , "LedgerTx_FY", "Office_ID","LedgerTx_IsActive", "LedgerTx_InsertedBy","LedgerTx_OrderBy","LedgerTx_MaintainType" },
               new string[] { "0", ddlPartyName.SelectedValue.ToString(),"Main Ledger",ViewState["VoucherTx_ID"].ToString(),VoucherTx_Type,VoucherTx_Amount ,Month.ToString(),Year.ToString()
                            ,FinancialYear.ToString(),ViewState["Office_ID"].ToString() , LedgerTx_IsActive,ViewState["Emp_ID"].ToString(),"1","None"}, "dataset");
                }

                SaveTDSGSTData(ViewState["VoucherTx_ID"].ToString());

                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                //ClearData();
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
            ddlPartyName.ClearSelection();
            txtCurrentBalance.Text = "";

            ddlLedger.ClearSelection();
            txtLedgerAmt.Text = "";
            //GridViewParticulars.DataSource = new string[] { };
            //GridViewParticulars.DataBind();
            GridViewRef.DataSource = new string[] { };
            GridViewRef.DataBind();
            GridViewLedger.DataSource = new string[] { };
            GridViewLedger.DataBind();
            GridViewChequeDetail.DataSource = new string[] { };
            GridViewChequeDetail.DataBind();
            AddItem("NA");
            FillVoucherNo();
            GetPreviousVoucherNo();
            txtVoucherTx_Narration.Text = "";
            txtSuplierName.Text = "";
            txtsupplieraddress.Text = "";
            ddlState.ClearSelection();
            txtCity.Text = "";
            ddlRegistrationType.ClearSelection();
            txtGSTNo.Text = "";
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
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "VoucherTx_ID" }, new string[] { "16", ViewState["VoucherTx_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //if (ds.Tables[0].Rows[0]["VoucherTx_No"].ToString().Contains("VR"))
                    //{
                    //    var rx = new System.Text.RegularExpressions.Regex("VR");
                    //    string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //    var array = rx.Split(str);
                    //    lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //    txtVoucherTx_No.Text = array[1];
                    //    lblVoucherTx_No.Text = array[0] + "VR";
                    //}
                    //else
                    //{
                    //    var rx = new System.Text.RegularExpressions.Regex("PV");
                    //    string str = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //    var array = rx.Split(str);
                    //    lblVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    //    txtVoucherTx_No.Text = array[1];
                    //    lblVoucherTx_No.Text = array[0] + "PV";
                    //}
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
                    txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    ViewState["VoucherTx_Date"] = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    ViewState["FY"] = ds.Tables[0].Rows[0]["VoucherTx_FY"].ToString();
                    lblGrandTotal.Text = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                    txtVoucherTx_Narration.Text = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
                    txtInvoice.Text = ds.Tables[0].Rows[0]["VoucherTx_Ref"].ToString();
                    ViewState["Amount"] = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                    if (ds.Tables[0].Rows[0]["VoucherTx_SupplierinvoiceDate"].ToString() != "")
                    {
                        txtSupplierInvoiceDate.Text = ds.Tables[0].Rows[0]["VoucherTx_SupplierinvoiceDate"].ToString();
                    }
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    string OfficeID = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                    if (OfficeID.ToString() == Session["Office_ID"].ToString())
                    {
                        ddlPartyName.ClearSelection();
                        ddlPartyName.Items.FindByValue(ds.Tables[2].Rows[0]["Ledger_ID"].ToString()).Selected = true;
                        ddlPartyName.Visible = true;
                        txtPartyName.Visible = false;
                        FillCurrentBalance();
                    }
                    else
                    {
                        ddlPartyName.Visible = false;
                        txtPartyName.Visible = true;

                        txtPartyName.Text = ds.Tables[2].Rows[0]["Ledger_Name"].ToString();
                        string LedgerID = ds.Tables[2].Rows[0]["Ledger_ID"].ToString();
                        string Office_ID = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                        DataSet ds1 = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "1", LedgerID.ToString(), Office_ID.ToString() }, "dataset");
                        if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                        {
                            txtCurrentBalance.Text = ds1.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
                        }
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

                if (ds.Tables[5].Rows.Count > 0)
                {
                    BillByBillDetail.Visible = true;
                    pnlChequeDetail.Visible = false;
                    GridViewBillByBillDetail.DataSource = ds.Tables[5];
                    GridViewBillByBillDetail.DataBind();
                }
                if (ds.Tables[6].Rows.Count > 0)
                {
                    BillByBillDetail.Visible = false;
                    pnlChequeDetail.Visible = true;
                    GridViewChequeDetail.DataSource = ds.Tables[6];
                    GridViewChequeDetail.DataBind();
                }
                if (ds.Tables[7].Rows.Count > 0)
                {

                    txtSuplierName.Text = ds.Tables[7].Rows[0]["SupplierName"].ToString();
                    txtsupplieraddress.Text = ds.Tables[7].Rows[0]["SupplierAddress"].ToString();
                    ddlState.ClearSelection();
                    ddlState.Items.FindByValue(ds.Tables[7].Rows[0]["State_ID"].ToString()).Selected = true;
                    txtCity.Text = ds.Tables[7].Rows[0]["City"].ToString();
                    ddlRegistrationType.ClearSelection();
                    ddlRegistrationType.Items.FindByText(ds.Tables[7].Rows[0]["RegistrationTypes"].ToString()).Selected = true;
                    txtGSTNo.Text = ds.Tables[7].Rows[0]["GST_No"].ToString();
                }
                ds = objdb.ByProcedure("SpFinTDSGSTEntry", new string[] { "flag", "Voucher_Id", "Office_ID" }, new string[] { "7", ViewState["VoucherTx_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    CreateTDSGSTTable();
                    DataTable dt_TdsGSTTable = (DataTable)ViewState["TdsGSTTable"];
                    dt_TdsGSTTable = ds.Tables[0];
                    ViewState["TdsGSTTable"] = dt_TdsGSTTable;
                    btnTDSView.Visible = true;
                }
                btnAccept.Text = "Update";
                btnAccept.Enabled = true;
                //FillCurrentBalance();
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

            panel1.Enabled = false;
            lbkbtnAddLedger.Visible = false;
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

    protected void txtVoucherTx_Date_TextChanged(object sender, EventArgs e)
    {
        //string ValidStatus = ValidDate();
		string ValidStatus = "Yes";
        if (ValidStatus == "No")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('You are not allowed to choose this date, please contact to head office.');", true);
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtVoucherTx_Date.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }
        }
        else
        {
            if (ViewState["VoucherTx_ID"].ToString() == "0")
            {
                FillVoucherNo();
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
        }

    }

    //Fill Previous VoucherNarration
    protected void btnNarration_Click(object sender, EventArgs e)
    {
        ds = objdb.ByProcedure("SpFinVoucherTx",
                 new string[] { "flag", "VoucherTx_Type", "Office_ID" },
                 new string[] { "14", "GSTService Purchase", ViewState["Office_ID"].ToString() }, "dataset");

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
    protected void GridViewRef_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string RID = e.CommandArgument.ToString();
        if (e.CommandName == "BillByBillDelete")
        {
            ViewState["Amount"] = Math.Abs(Convert.ToDecimal(lblGrandTotal.Text));
            FillRefAmount(RID);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);

        }

    }
    //protected void ManageBillByBill(string LedgerAmount)
    //{
    //    decimal Status = decimal.Parse(ViewState["LedgerAmount"].ToString()) - decimal.Parse(LedgerAmount.ToString());
    //    if (Status == 0)
    //    {
    //        Save("BillByBill");
    //    }
    //    else
    //    {
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowReferanceModal();", true);
    //        txtBillByBillTx_Ref.Visible = true;
    //        txtBillByBillTx_Ref.Text = txtInvoice.Text;
    //        if (Status.ToString().Contains("-"))
    //        {

    //            txtBillByBillTx_Amount.Text = Status.ToString();
    //            txtBillByBillTx_Amount.Text = txtBillByBillTx_Amount.Text.Replace(@"-", string.Empty);

    //        }
    //        else
    //        {
    //            txtBillByBillTx_Amount.Text = Status.ToString();
    //        }
    //        //BindBillByBillData();
    //        //ddlRefType.ClearSelection();
    //        //ddlBillByBillTx_crdr.ClearSelection();
    //        if (Status.ToString().Contains("-"))
    //        {

    //            ddlBillByBillTx_crdr.SelectedValue = "Dr";


    //        }
    //        else
    //        {
    //            ddlBillByBillTx_crdr.SelectedValue = "Cr";
    //        }

    //        ddlBillByBillTx_Ref.ClearSelection();
    //        if (ddlRefType.SelectedValue == "1")
    //        {
    //            ddlRefType.SelectedValue = "1";
    //            lnkView.Visible = true;
    //            txtBillByBillTx_Ref.Visible = false;
    //            ddlBillByBillTx_Ref.Visible = true;
    //        }
    //        else
    //        {
    //            lnkView.Visible = false;
    //            txtBillByBillTx_Ref.Visible = true;
    //            ddlBillByBillTx_Ref.Visible = false;
    //        }
    //    }

    //}
    protected void ddlPartyNameTDS_SelectedIndexChanged(object sender, EventArgs e)
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
            if (ddlPartyNameTDS.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpFinTDSGSTEntry", new string[] { "flag", "ChildLedger_ID" }, new string[] { "6", ddlPartyNameTDS.SelectedValue.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtTdsGSTNo.Text = ds.Tables[0].Rows[0]["GST_No"].ToString();
                }
                if (gvTdsGSTDetail.Rows.Count > 0)
                {
                    //btnFSubmit.Visible = true;
                }
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowTDSDetailModal();", true);
        }
        catch (Exception)
        {
            lblGSTModal.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Party Name is alrady exists.");
        }
    }
    protected void btnRefreshLedger_Click(object sender, EventArgs e)
    {
        try
        {
            ddlPartyName.DataSource = null;
            ddlPartyName.DataBind();
            ds = objdb.ByProcedure("SpFinTDSGSTEntry", new string[] { "flag", "Head_ID", "Office_ID" }, new string[] { "11", "115", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPartyName.DataTextField = "Ledger_Name";
                ddlPartyName.DataValueField = "Ledger_ID";
                ddlPartyName.DataSource = ds;
                ddlPartyName.DataBind();
                ddlPartyName.Items.Insert(0, new ListItem("Select", "0"));
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowTDSDetailModal();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtGSTTDSAmount_TextChanged(object sender, EventArgs e)
    {
        if (txtBasicAmount.Text != "" && txtGSTTDSAmount.Text != "")
        {
            decimal TDSVal = Convert.ToDecimal(txtGSTTDSAmount.Text);
            ds = objdb.ByProcedure("SpFinTDSGSTEntry", new string[] { "flag", "ChildLedger_ID" }, new string[] { "5", ddlPartyNameTDS.SelectedValue.ToString() }, "dataset");
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
    protected void txtBasicAmount_TextChanged(object sender, EventArgs e)
    {
        if (txtBasicAmount.Text != "" && ddlPartyNameTDS.SelectedIndex > 0)
        {
            decimal BasicAmt = Convert.ToDecimal(txtBasicAmount.Text);
            decimal GSTTDS = (BasicAmt * 2) / 100;
            txtGSTTDSAmount.Text = GSTTDS.ToString("0.00");

            ds = objdb.ByProcedure("SpFinTDSGSTEntry", new string[] { "flag", "ChildLedger_ID" }, new string[] { "5", ddlPartyNameTDS.SelectedValue.ToString() }, "dataset");
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
                if (decimal.Parse(GSTTDSAmount.ToString()) == decimal.Parse(txtLedgerAmt.Text))
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
                new string[] { "1", VoucherTx_ID, "813", lblLedger_ID.Text, lblGSTNo.Text, lblBillNo.Text, Convert.ToDateTime(lblBillDate.Text, cult).ToString("yyyy/MM/dd"), lblTotalBillAmount.Text, lblPaymentAmount.Text, lblBasicAmount.Text, lblGSTTDSAmount.Text, lblCGST.Text, lblSGST.Text, lblIGST.Text, ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
        }
    }
    protected void btnTDSView_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            DataTable ds_TDSGSTDetail = (DataTable)ViewState["TdsGSTTable"];
            gvTDSDetail.DataSource = ds_TDSGSTDetail;
            gvTDSDetail.DataBind();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowTDSDetail();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
}