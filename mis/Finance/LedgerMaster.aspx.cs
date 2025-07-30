using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_Finance_LedgerMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {

                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Ledger_ID"] = "0";
                    FillYear();
                    FillHead();
                    FillState();
                    FillOffice();
                    if (Request.QueryString["Ledger_ID"] != null)
                    {
                        ViewState["Ledger_ID"] = objdb.Decrypt(Request.QueryString["Ledger_ID"].ToString());
                        FillLedgerDetail();

                    }
                    if (Request.QueryString["Vendorid"] != null)
                    {
                        ViewState["Vendorid"] = objdb.Decrypt(Request.QueryString["Vendorid"].ToString());
                        FillVendorDetails();

                    }
                    else
                    {
                        ViewState["Vendorid"] = "";
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
    protected void FillYear()
    {
        try
        {
            ds = objdb.ByProcedure("SpHrYear_Master",
                          new string[] { "flag" },
                          new string[] { "2" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlFinancialYear.DataTextField = "Financial_Year";
                ddlFinancialYear.DataValueField = "Year";
                ddlFinancialYear.DataSource = ds;
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select", "0"));
                ddlFinancialYear.Items.FindByValue("2018").Selected = true;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillHead()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinMasterHead",
                          new string[] { "flag" },
                          new string[] { "7" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlHeadName.DataTextField = "Head_Name";
                ddlHeadName.DataValueField = "Head_ID";
                ddlHeadName.DataSource = ds;
                ddlHeadName.DataBind();
                ddlHeadName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
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
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "9" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                chkOffice.DataSource = ds;
                chkOffice.DataTextField = "Office_Name";
                chkOffice.DataValueField = "Office_ID";
                chkOffice.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        ds = objdb.ByProcedure("SpFinLedgerMaster",
              new string[] { "flag" },
              new string[] { "2" }, "dataset");

        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void ClearText()
    {
        ddlHeadName.ClearSelection();
        txtLedgerName.Text = "";
        txtLedgerAlias.Text = "";
        ddlState.ClearSelection();
        ddlInventoryAffected.ClearSelection();
        txtMailing_Name.Text = "";
        txtMailing_Address.Text = "";
        txtMailing_PanNo.Text = "";
        ddlFinancialYear.ClearSelection();
        txtGSTNo.Text = "";
        txtacntno.Text = "";
        txtpincode.Text = "";
        txtEffectivedate.Text = "";
        chkOffice.ClearSelection();
        chkOfficeAll.Checked = false;
        txtachlder_name.Text = "";
        txtacntno.Text = "";
        txtbankname.Text = "";
        txtbranch.Text = "";
        txtifsccode.Text = "";
        ddlRegistrationType.ClearSelection();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string Ledger_IsActive = "1";
            string EffectiveDate = "";
            if (ddlHeadName.SelectedIndex == 0)
            {
                msg += "Select Head Name";
            }
            if (txtLedgerName.Text == "")
            {
                msg += "Enter Ledger Name";
            }
            //if (ddlInventoryAffected.SelectedIndex == 0)
            //{
            //    msg += "Select Inventory Value Are Affect";
            //}
            if (txtMailing_Name.Text == "")
            {
                msg += "Enter Mailing Name";
            }
            if (txtMailing_Address.Text == "")
            {
                msg += "Enter Mailing Address";
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


            //if (txtMailing_PanNo.Text == "")
            //{
            //    msg += "Enter PAN No";
            //}
            //if (ddlFinancialYear.SelectedIndex == 0)
            //{
            //    msg += "Select Financial Year";
            //}
            if (txtEffectivedate.Text != "")
            {
                EffectiveDate = Convert.ToDateTime(txtEffectivedate.Text, cult).ToString("yyyy/MM/dd");
            }
            else
            {
                EffectiveDate = "";
            }

            string LedgerTx_Amount = "0";
            if (txtOpeningBalance.Text.Trim() != "")
            {
                if (ddlDrCr.SelectedItem.Text == "Credit")
                {
                    LedgerTx_Amount = txtOpeningBalance.Text;
                }
                else if (ddlDrCr.SelectedItem.Text == "Debit")
                {
                    LedgerTx_Amount = "-" + txtOpeningBalance.Text;
                }
            }
            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_Name", "Ledger_ID" }, new string[] { "10", txtLedgerName.Text, ViewState["Ledger_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (btnSave.Text == "Save" && ViewState["Ledger_ID"].ToString() == "0" && Status == 0)
                {

                    ds = objdb.ByProcedure("SpFinLedgerMaster",
                    new string[] { "flag", "Vendor_id", "Ledger_Name", "Ledger_HeadID", "Office_ID", "Ledger_Alias", "AccountHolderName", "IFSCCode", "BankName", "Branch", "InventoryAffected", "State_ID", "Mailing_Name", "Mailing_Address", "EffectiveDateforReconsilation", "Acnt_No", "PinCode", "Pan_No", "GST_No", "RegistrationTypes", "Ledger_IsActive", "Ledger_UpdatedBy", "MaintainBalancesBillByBill" },
                    new string[] { "0", ViewState["Vendorid"].ToString(), txtLedgerName.Text.Trim(), ddlHeadName.SelectedValue.ToString(), ViewState["Office_ID"].ToString(), txtLedgerAlias.Text, txtachlder_name.Text, txtifsccode.Text, txtbankname.Text, txtbranch.Text, ddlInventoryAffected.SelectedValue.ToString(), ddlState.SelectedValue.ToString(), txtMailing_Name.Text.Trim(), txtMailing_Address.Text.Trim(), EffectiveDate, txtacntno.Text, txtpincode.Text, txtMailing_PanNo.Text.Trim(), txtGSTNo.Text, ddlRegistrationType.SelectedItem.Text, Ledger_IsActive, ViewState["Emp_ID"].ToString(),ddlBalBillByBill.SelectedValue }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string Ledger_ID = ds.Tables[0].Rows[0]["Ledger_ID"].ToString();
                        if (Ledger_ID != "")
                        {
                            foreach (ListItem item in chkOffice.Items)
                            {
                                if (item.Selected == true)
                                {
                                    objdb.ByProcedure("SpFinLedgerChild", new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_IsActive", "LedgerChild_UpdatedBy" }, new string[] { "0", Ledger_ID, item.Value, "1", ViewState["Emp_ID"].ToString() }, "dataset");

                                }
                            }

                            string sDate = (Convert.ToDateTime("01/04/2018", cult).ToString("yyyy/MM/dd")).ToString();
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

                            objdb.ByProcedure("SpFinLedgerTx",
                                 new string[] { "flag", "Ledger_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type" },
                                 new string[] { "3", Ledger_ID, "Closing Balance", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), "0", "Sub Ledger" }, "dataset");


                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                            ClearText();
                        }

                    }
                }
                else if (btnSave.Text == "Update" && ViewState["Ledger_ID"].ToString() != "0" && Status == 0)
                {
                    objdb.ByProcedure("SpFinLedgerMaster",
                    new string[] { "flag", "Ledger_ID", "Ledger_Name", "Ledger_HeadID", "Ledger_Alias", "AccountHolderName", "IFSCCode", "BankName", "Branch", "InventoryAffected", "State_ID", "Mailing_Name", "Mailing_Address", "EffectiveDateforReconsilation", "Acnt_No", "PinCode", "Pan_No", "GST_No", "RegistrationTypes", "Ledger_UpdatedBy", "MaintainBalancesBillByBill" },
                    new string[] { "6", ViewState["Ledger_ID"].ToString(), txtLedgerName.Text.Trim(), ddlHeadName.SelectedValue.ToString(), txtLedgerAlias.Text, txtachlder_name.Text, txtifsccode.Text, txtbankname.Text, txtbranch.Text, ddlInventoryAffected.SelectedValue.ToString(), ddlState.SelectedValue.ToString(), txtMailing_Name.Text.Trim(), txtMailing_Address.Text.Trim(), EffectiveDate, txtacntno.Text, txtpincode.Text, txtMailing_PanNo.Text.Trim(), txtGSTNo.Text, ddlRegistrationType.SelectedItem.Text, ViewState["Emp_ID"].ToString(),ddlBalBillByBill.SelectedValue }, "dataset");
                    objdb.ByProcedure("SpFinLedgerChild", new string[] { "flag", "Ledger_ID" }, new string[] { "1", ViewState["Ledger_ID"].ToString()}, "dataset");
                    foreach (ListItem item in chkOffice.Items)
                    {
                        if (item.Selected == true)
                        {
                            objdb.ByProcedure("SpFinLedgerChild", new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_IsActive", "LedgerChild_UpdatedBy" }, new string[] { "0", ViewState["Ledger_ID"].ToString(), item.Value, "1", ViewState["Emp_ID"].ToString() }, "dataset");

                        }


                    }
                    string sDate = (Convert.ToDateTime("01/04/2018", cult).ToString("yyyy/MM/dd")).ToString();
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

                    objdb.ByProcedure("SpFinLedgerTx",
                         new string[] { "flag", "Ledger_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type" },
                         new string[] { "3", ViewState["Ledger_ID"].ToString(), "Closing Balance", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), "1", ViewState["Emp_ID"].ToString(), "0", "Sub Ledger" }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");


                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Ledger name is already exist.');", true);
                    lblMsg.Text = "";
                    ClearText();
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
    protected void ddlHeadName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    lblMsg.Text = "";
        //    chkOfficeAll.Enabled = true;
        //    chkOffice.ClearSelection();
        //    foreach (ListItem item in chkOffice.Items)
        //    {
        //        item.Enabled = true;
        //    }
        //    if (ddlHeadName.SelectedIndex > 0)
        //    {
        //        if (ddlHeadName.SelectedItem.Text == "Bank Accounts")
        //        {
        //            FillBankAccountDiv();
        //            string Value = ViewState["Office_ID"].ToString();
        //            foreach (ListItem item in chkOffice.Items)
        //            {
        //                chkOfficeAll.Enabled = false;
        //                if (item.Value == Value)
        //                {
        //                    item.Selected = true;
        //                    item.Enabled = false;

        //                }
        //                else
        //                {
        //                    item.Enabled = false;
        //                }

        //            }
        //        }
        //        else if (ddlHeadName.SelectedItem.Text == "Bank OD Accounts")
        //        {
        //            FillBankAccountDiv();

        //        }
        //        else if (ddlHeadName.SelectedItem.Text == "Cash In Hand")
        //        {
        //            FillCashInHandDiv();
        //        }
        //        else if (ddlHeadName.SelectedItem.Text == "Capital Accounts" || ddlHeadName.SelectedItem.Text == "Current Assets" || ddlHeadName.SelectedItem.Text == "Current Liablites" || ddlHeadName.SelectedItem.Text == "Deposits (Asset)" || ddlHeadName.SelectedItem.Text == "Direct Expenses" || ddlHeadName.SelectedItem.Text == "Direct Incomes" || ddlHeadName.SelectedItem.Text == "Duties & Taxes")
        //        {
        //            FillForOtherDiv();
        //        }
        //        else
        //        {
        //            FillDiv();

        //        }
        //    }

        //}
        //catch (Exception ex)
        //{
        //    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        //}


    }
    protected void FillBankAccountDiv()
    {
        effectivedate.Visible = true;
        //acntno.Visible = true;
        inventoryvalue.Visible = false;
        panno.Visible = false;
        gstno.Visible = false;
        pinno.Visible = false;

    }
    protected void FillCashInHandDiv()
    {
        effectivedate.Visible = false;
        //acntno.Visible = false;
        inventoryvalue.Visible = false;
        panno.Visible = true;
        gstno.Visible = true;
        pinno.Visible = true;
    }
    protected void FillForOtherDiv()
    {
        effectivedate.Visible = false;
        //acntno.Visible = false;
        inventoryvalue.Visible = true;
        panno.Visible = true;
        gstno.Visible = true;
        pinno.Visible = true;
    }
    protected void FillDiv()
    {
        effectivedate.Visible = true;
        //acntno.Visible = true;
        inventoryvalue.Visible = true;
        panno.Visible = true;
        gstno.Visible = true;
        pinno.Visible = true;
    }
    protected void FillLedgerDetail()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "5", ViewState["Ledger_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlHeadName.ClearSelection();
                ddlHeadName.Items.FindByValue(ds.Tables[0].Rows[0]["Ledger_HeadID"].ToString()).Selected = true;
                ddlHeadName.Enabled = false;
                txtLedgerAlias.Text = ds.Tables[0].Rows[0]["Ledger_Alias"].ToString();
                txtLedgerName.Text = ds.Tables[0].Rows[0]["Ledger_Name"].ToString();
                txtMailing_Name.Text = ds.Tables[0].Rows[0]["Mailing_Name"].ToString();
                txtMailing_Address.Text = ds.Tables[0].Rows[0]["Mailing_Address"].ToString();
                ddlState.ClearSelection();
                ddlState.Items.FindByValue(ds.Tables[0].Rows[0]["State_ID"].ToString()).Selected = true;
                txtacntno.Text = ds.Tables[0].Rows[0]["Acnt_No"].ToString();
                //ddlInventoryAffected.ClearSelection();
                //ddlInventoryAffected.Items.FindByText(ds.Tables[0].Rows[0]["InventoryAffected"].ToString()).Selected = true;
                txtMailing_PanNo.Text = ds.Tables[0].Rows[0]["Pan_No"].ToString();
                txtpincode.Text = ds.Tables[0].Rows[0]["PinCode"].ToString();
                txtGSTNo.Text = ds.Tables[0].Rows[0]["GST_No"].ToString();
                txtachlder_name.Text = ds.Tables[0].Rows[0]["AccountHolderName"].ToString();
                txtifsccode.Text = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                txtbankname.Text = ds.Tables[0].Rows[0]["BankName"].ToString();
                txtbranch.Text = ds.Tables[0].Rows[0]["Branch"].ToString();
                if (ds.Tables[0].Rows[0]["RegistrationTypes"].ToString() != "")
                {
                    ddlRegistrationType.ClearSelection();
                    ddlRegistrationType.Items.FindByText(ds.Tables[0].Rows[0]["RegistrationTypes"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["MaintainBalancesBillByBill"].ToString() != "")
                {
                    ddlBalBillByBill.ClearSelection();
                    ddlBalBillByBill.Items.FindByText(ds.Tables[0].Rows[0]["MaintainBalancesBillByBill"].ToString()).Selected = true;
                }
                


                //if (ddlHeadName.SelectedItem.Text == "Bank Accounts" )
                //{
                //    FillBankAccountDiv();
                //    foreach (ListItem item in chkOffice.Items)
                //    {

                //            item.Enabled = false;
                //            chkOfficeAll.Enabled = false;

                //    }
                //    txtEffectivedate.Text = ds.Tables[0].Rows[0]["EffectiveDateforReconsilation"].ToString();
                //    txtacntno.Text = ds.Tables[0].Rows[0]["Acnt_No"].ToString();
                //}
                //else if (ddlHeadName.SelectedItem.Text == "Bank OD Accounts")
                //{
                //    FillBankAccountDiv();
                //    txtEffectivedate.Text = ds.Tables[0].Rows[0]["EffectiveDateforReconsilation"].ToString();
                //    txtacntno.Text = ds.Tables[0].Rows[0]["Acnt_No"].ToString();
                //}
                //else if (ddlHeadName.SelectedItem.Text == "Cash In Hand")
                //{
                //    FillCashInHandDiv();
                //    txtMailing_PanNo.Text = ds.Tables[0].Rows[0]["Pan_No"].ToString();
                //    txtpincode.Text = ds.Tables[0].Rows[0]["PinCode"].ToString();
                //    txtGSTNo.Text = ds.Tables[0].Rows[0]["GST_No"].ToString();
                //}
                //else if (ddlHeadName.SelectedItem.Text == "Capital Accounts" || ddlHeadName.SelectedItem.Text == "Current Assets" || ddlHeadName.SelectedItem.Text == "Current Liablites" || ddlHeadName.SelectedItem.Text == "Deposits (Asset)" || ddlHeadName.SelectedItem.Text == "Direct Expenses" || ddlHeadName.SelectedItem.Text == "Direct Incomes" || ddlHeadName.SelectedItem.Text == "Duties & Taxes")
                //{
                //    FillForOtherDiv();
                //    ddlInventoryAffected.ClearSelection();
                //    ddlInventoryAffected.Items.FindByValue(ds.Tables[0].Rows[0]["InventoryAffected"].ToString()).Selected = true;
                //    txtMailing_PanNo.Text = ds.Tables[0].Rows[0]["Pan_No"].ToString();
                //    txtpincode.Text = ds.Tables[0].Rows[0]["PinCode"].ToString();
                //    txtGSTNo.Text = ds.Tables[0].Rows[0]["GST_No"].ToString();
                //}
                //else
                //{
                //    FillDiv();
                //    txtEffectivedate.Text = ds.Tables[0].Rows[0]["EffectiveDateforReconsilation"].ToString();
                //    txtacntno.Text = ds.Tables[0].Rows[0]["Acnt_No"].ToString();
                //    ddlInventoryAffected.ClearSelection();
                //    ddlInventoryAffected.Items.FindByText(ds.Tables[0].Rows[0]["InventoryAffected"].ToString()).Selected = true;
                //    txtMailing_PanNo.Text = ds.Tables[0].Rows[0]["Pan_No"].ToString();
                //    txtpincode.Text = ds.Tables[0].Rows[0]["PinCode"].ToString();
                //    txtGSTNo.Text = ds.Tables[0].Rows[0]["GST_No"].ToString();
                //}
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                txtOpeningBalance.Text = ds.Tables[2].Rows[0]["LedgerTx_Amount"].ToString();
                if (ds.Tables[2].Rows[0]["DrCr"].ToString() != "")
                {
                    ddlDrCr.ClearSelection();
                    ddlDrCr.Items.FindByValue(ds.Tables[2].Rows[0]["DrCr"].ToString()).Selected = true;
                }
            }
                
            if (ds.Tables[1].Rows.Count > 0)
            {
                chkOffice.ClearSelection();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    string Value = ds.Tables[1].Rows[i]["Office_ID"].ToString();
                    foreach (ListItem item in chkOffice.Items)
                    {

                        if (item.Value == Value)
                        {

                            item.Selected = true;
                        }

                    }

                }
                
            }
            btnSave.Text = "Update";
            clear.Visible = false;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void FillVendorDetails()
    {
        try
        {
            ds = objdb.ByProcedure("SpVendorMaster", new string[] { "flag", "Vendor_id" }, new string[] { "6", ViewState["Vendorid"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtachlder_name.Text = ds.Tables[0].Rows[0]["VendorName"].ToString();
                txtacntno.Text = ds.Tables[0].Rows[0]["AccountNo"].ToString();
                txtifsccode.Text = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                txtbankname.Text = ds.Tables[0].Rows[0]["Bank_Name"].ToString();
                txtMailing_Name.Text = ds.Tables[0].Rows[0]["VendorName"].ToString();
                ddlState.ClearSelection();
                ddlState.Items.FindByValue(ds.Tables[0].Rows[0]["State_ID"].ToString()).Selected = true;
                txtMailing_Address.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                txtpincode.Text = ds.Tables[0].Rows[0]["Pincode"].ToString();
                txtMailing_PanNo.Text = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                txtGSTNo.Text = ds.Tables[0].Rows[0]["GSTNumber"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

}