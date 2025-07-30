using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Collections.Generic;
using System.Web.UI;


public partial class mis_Finance_LedgerMasterB : System.Web.UI.Page
{
    DataSet ds;
    static DataSet ds5, ds6;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    static string prevPage = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {

                if (!IsPostBack)
                {
                    if (Request.UrlReferrer != null)
                    {
                        prevPage = Request.UrlReferrer.ToString();
                    }
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Current_Office_ID"] = Session["Office_ID"].ToString();
					ViewState["OfficeType_Title"] = Session["OfficeType_Title"].ToString();
                    ViewState["Ledger_ID"] = "0";
                    ViewState["ST"] = "0";
                    ViewState["Amount"] = "0";
                    txtIGST.Attributes.Add("readonly", "readonly");
                    txtCGST.Attributes.Add("readonly", "readonly");
                    txtSGST.Attributes.Add("readonly", "readonly");
                    txtDescription.Attributes.Add("readonly", "readonly");
                    txtachlder_name.Attributes.Add("readonly", "readonly");
                    txtVoucherTx_Date.Attributes.Add("readonly", "readonly");
                    txtapplicablefrom.Attributes.Add("readonly", "readonly");
                    FillHead();
                    FillState();
                    FillHSN();
                    GetVoucherDate();
                    setDate();
                    pnacndetails.Enabled = false;
                    ddlType.Enabled = false;
                    if (Request.QueryString["Ledger_ID"] != null)
                    {
                        string mode = objdb.Decrypt(Request.QueryString["Mode"].ToString());
                        {
                            if (mode == "View")
                            {
                                ViewState["Ledger_ID"] = objdb.Decrypt(Request.QueryString["Ledger_ID"].ToString());
                                pnbody.Enabled = false;
                                FillLedgerDetail();
                            }
                            else
                            {
                                ViewState["Ledger_ID"] = objdb.Decrypt(Request.QueryString["Ledger_ID"].ToString());
                                ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "17", ViewState["Ledger_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                                if (ds != null && ds.Tables[0].Rows.Count > 0)
                                {
                                    if (ds.Tables[0].Rows[0]["status"].ToString() == "true")
                                    {
                                        ViewState["ST"] = "2";
                                        ViewState["Office_ID"] = ds.Tables[1].Rows[0]["Office_ID"].ToString();
                                        if (ViewState["ST"].ToString() == "2")
                                        {
                                            FillLedgerDetail();
                                            pnlOB.Enabled = false;
                                            ddlBalBillByBill.Enabled = false;
                                            ddlInventoryAffected.Enabled = false;
                                            ddlCostCentre.Enabled = false;
                                        }
                                    }
                                    else
                                    {
                                        FillLedgerDetail();
                                    }
                                }
                                else
                                {

                                }

                            }
                        }

                    }

                    AcountDetailsHideShow();
                    GetLedgerName();

                }
                else
                {
                    WebControl wcICausedPostBack = (WebControl)GetControlThatCausedPostBack(sender as Page);
                    int indx = wcICausedPostBack.TabIndex;
                    var ctrl = from control in wcICausedPostBack.Parent.Controls.OfType<WebControl>()
                               where control.TabIndex > indx
                               select control;
                    ctrl.DefaultIfEmpty(wcICausedPostBack).First().Focus();
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
    protected Control GetControlThatCausedPostBack(Page page)
    {
        Control control = null;

        string ctrlname = page.Request.Params.Get("__EVENTTARGET");
        if (ctrlname != null && ctrlname != string.Empty)
        {
            control = page.FindControl(ctrlname);
        }
        else
        {
            foreach (string ctl in page.Request.Form)
            {
                Control c = page.FindControl(ctl);
                if (c is System.Web.UI.WebControls.Button || c is System.Web.UI.WebControls.ImageButton)
                {
                    control = c;
                    break;
                }
            }
        }
        return control;

    }
    protected void FillHead()
    {
        try
        {
            // ds = objdb.ByProcedure("SpFinMasterHead",
                          // new string[] { "flag" },
                          // new string[] { "14" }, "dataset");
			 ds = objdb.ByProcedure("SpFinMasterHead",
                          new string[] { "flag", "OfficeType_Title" },
                          new string[] { "17", ViewState["OfficeType_Title"].ToString() }, "dataset");			  
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
    protected void FillHSN()
    {
        try
        {
            ddlHSNCode.Items.Clear();
            ddlHSNCode.Items.Insert(0, "Select");
            txtDescription.Text = "";
            ddlTaxability.SelectedValue = "NA";
            txtIGST.Text = "0";
            txtCGST.Text = "0";
            txtSGST.Text = "0";
            lblMsg.Text = "";
            if (ddltypeofsupply.SelectedIndex > 0)
            {
                DataSet ds3 = objdb.ByProcedure("SpFinHSNMaster",
                                new string[] { "flag", "HSN_TypeOfSupply" },
                                new string[] { "13", ddltypeofsupply.SelectedValue.ToString() }, "dataset");
                if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
                {
                    ddlHSNCode.DataSource = ds3;
                    ddlHSNCode.DataTextField = "HSN_Code";
                    ddlHSNCode.DataValueField = "HSN_ID";
                    ddlHSNCode.DataBind();
                    ddlHSNCode.Items.Insert(0, "Select");
                }
                else
                {
                    ddlHSNCode.Items.Clear();
                    ddlHSNCode.Items.Insert(0, "Select");

                }
            }
            else
            {
                ddlHSNCode.Items.Clear();
                ddlHSNCode.Items.Insert(0, "Select");
                txtDescription.Text = "";
                ddlTaxability.SelectedValue = "NA";
                txtIGST.Text = "0";
                txtCGST.Text = "0";
                txtSGST.Text = "0";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        ddlHeadName.ClearSelection();
        ddlHeadName.Enabled = true;
        txtLedgerName.Text = "";
        txtLedgerNameHindi.Text = "";
        txtLedgerAlias.Text = "";
        ddlState.ClearSelection();
        ddlInventoryAffected.SelectedIndex = 1;
        txtMailing_Name.Text = "";
        txtMailing_Address.Text = "";
        txtMailing_PanNo.Text = "";
        txtGSTNo.Text = "";
        txtacntno.Text = "";
        txtpincode.Text = "";
        txtEffectivedate.Text = "";
        ddlType.SelectedValue = "Other";
        txtachlder_name.Text = "";
        hfachlder_name.Value = "";
        txtacntno.Text = "";
        txtbankname.Text = "";
        txtbranch.Text = "";
        txtifsccode.Text = "";
        txtOpeningBalance.Text = "0";
        ViewState["ST"] = "0";
        pnlOB.Enabled = true;
        ddlRegistrationType.ClearSelection();
        ddlHSNCode.ClearSelection();
        txtDescription.Text = "";
        ddlTaxability.ClearSelection();
        ddlineligibleforinputcredit.SelectedIndex = 1;
        txtIGST.Text = "0";
        txtCGST.Text = "0";
        txtSGST.Text = "0";
        pnacndetails.Enabled = false;
        ddltypeofsupply.SelectedValue = "Services";
        ddlGSTApplicable.SelectedValue = "No";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowhideGSTDetails()", true);

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string Active = "";
            string ExistMsg = "";
            string LedgerChild_IsActive = "1";
            string LedgerStatusActive = "1";
            string Ledger_IsActive = "1";
            string EffectiveDate = "";
            string LedgerTx_IsActive = "1";
            if (txtLedgerName.Text == "")
            {
                msg += "Enter Ledger Name.\\n";
            }
            if (ddlHeadName.SelectedIndex == 0)
            {
                msg += "Select Head Name. ";
            }
            if (ddlType.SelectedIndex == 0)
            {
                msg += "Select Type. ";
            }
            if (ddlRegistrationType.SelectedIndex > 0)
            {
                if (ddlRegistrationType.SelectedItem.Text == "Composition" || ddlRegistrationType.SelectedItem.Text == "Regular")
                {
                    //if (txtGSTNo.Text.Trim() == "")
                    //{
                    //    msg += "Enter GST No";
                    //    gstvisible.Visible = true;
                    //}

                }
                else
                {
                    gstvisible.Visible = false;
                }
            }
            if (txtOpeningBalance.Text == "")
            {
                msg += "Enter Opening Balance ";
            }
            if (txtEffectivedate.Text != "")
            {
                EffectiveDate = Convert.ToDateTime(txtEffectivedate.Text, cult).ToString("yyyy/MM/dd");
            }
            else
            {
                EffectiveDate = "";
            }


            if (msg.Trim() == "")
            {

                int Status = 0;
                txtLedgerName.Text = FirstLetterToUpper(txtLedgerName.Text);
                txtachlder_name.Text = FirstLetterToUpper(txtachlder_name.Text);
                //ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_Name", "Ledger_ID", "Ledger_HeadID" }, new string[] { "10", txtLedgerName.Text, ViewState["Ledger_ID"].ToString(), ddlHeadName.SelectedValue.ToString() }, "dataset");
                //if (ds.Tables[0].Rows.Count > 0)
                //{

                //    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                //}

                string sDate = (Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd")).ToString();
                DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
                int Month = int.Parse(datevalue.Month.ToString());
                int Year = int.Parse(datevalue.Year.ToString());
                int FY = Year;
                string FinancialYear = Year.ToString();
                string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
                FinancialYear = "";
                string AcholderName = hfachlder_name.Value;
                if (Month <= 3)
                {
                    FY = Year - 1;
                    FinancialYear = FY.ToString() + "-" + LFY.ToString();
                }
                else
                {

                    FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
                }

                //if (btnSave.Text == "Save & Next" && ViewState["Ledger_ID"].ToString() == "0" && Status == 0)
                if (btnSave.Text == "Save & Next" && ViewState["Ledger_ID"].ToString() == "0")
                {


                    ds = objdb.ByProcedure("SpFinLedgerMaster",
                       new string[] { "flag", "Ledger_Name", "Ledger_Name_Hindi", "Ledger_Code", "Ledger_HeadID", "Office_ID", "Ledger_Alias", "AccountHolderName", "IFSCCode", "BankName", "Branch", "InventoryAffected", "State_ID", "Mailing_Name", "Mailing_Address", "Acnt_No", "PinCode", "Pan_No", "GST_No", "RegistrationTypes", "Ledger_IsActive", "Ledger_UpdatedBy", "MaintainBalancesBillByBill", "MobileNo", "Email", "City", "FinancialYear", "Year", "GSTApplicable", "CostCentre" },
                       new string[] { "0", txtLedgerName.Text.Trim(), txtLedgerNameHindi.Text.Trim(),txtLedgerCode.Text, ddlHeadName.SelectedValue.ToString(),  ViewState["Office_ID"].ToString(), txtLedgerAlias.Text, AcholderName, txtifsccode.Text, txtbankname.Text, txtbranch.Text, ddlInventoryAffected.SelectedValue.ToString(), ddlState.SelectedValue.ToString(), txtMailing_Name.Text.Trim(), txtMailing_Address.Text.Trim(), txtacntno.Text, txtpincode.Text, txtMailing_PanNo.Text.Trim(), txtGSTNo.Text, ddlRegistrationType.SelectedItem.Text, Ledger_IsActive, ViewState["Emp_ID"].ToString(), ddlBalBillByBill.SelectedValue, txtMobileno.Text, txtEmail.Text, txtCity.Text, FinancialYear.ToString(), Year.ToString(), ddlGSTApplicable.SelectedValue,ddlCostCentre.SelectedValue }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if(ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string Ledger_ID = ds.Tables[0].Rows[0]["Ledger_ID"].ToString();
                            if (Ledger_ID != "")
                            {
                                if (ddlGSTApplicable.SelectedValue == "Yes")
                                {
                                    string IsActive = "1";

                                    objdb.ByProcedure("SpFinLedgerGSTDetails", new string[] { "flag", "Ledger_ID", "Description", "HSN_Code", "Taxbility", "HSN_IntegratedTax", "HSN_CGST", "HSN_SGST", "IsActive", "UpdatedBy", "Typeofsupply", "Isreversechargeapplicable", "GSTApplicable", "ApplicableFrom", "IsIneligibleforinputcredit" }, new string[] { "0", Ledger_ID, txtDescription.Text, ddlHSNCode.SelectedItem.Text, ddlTaxability.SelectedValue, txtIGST.Text, txtCGST.Text, txtSGST.Text, IsActive, ViewState["Emp_ID"].ToString(), ddltypeofsupply.SelectedValue, ddlrcm.SelectedValue.ToString(), ddlGSTApplicable.SelectedValue.ToString(), Convert.ToDateTime(txtapplicablefrom.Text, cult).ToString("yyyy/MM/dd"), ddlineligibleforinputcredit.SelectedItem.Text }, "dataset");

                                }
                                string LedgerTx_Amount = "0";

                                if (ddlBalBillByBill.SelectedValue == "Yes")
                                {
                                    if (decimal.Parse(txtOpeningBalance.Text) == 0)
                                    {
                                        LedgerStatusActive = "1";
                                        LedgerTx_IsActive = "1";
                                    }
                                    else
                                    {
                                        LedgerStatusActive = "0";
                                        LedgerTx_IsActive = "0";
                                    }

                                }
                                else
                                {
                                    LedgerStatusActive = "1";
                                    LedgerTx_IsActive = "1";
                                }
                                LedgerTx_Amount = txtOpeningBalance.Text;
                                if (ddlDrCr.SelectedItem.Text == "Credit")
                                {
                                    LedgerTx_Amount = LedgerTx_Amount;
                                }
                                else
                                {
                                    LedgerTx_Amount = "-" + LedgerTx_Amount;
                                }


                                objdb.ByProcedure("SpFinLedgerChild", new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_IsActive", "LedgerChild_UpdatedBy", "LedgerStatusActive" }, new string[] { "0", Ledger_ID, ViewState["Office_ID"].ToString(), LedgerChild_IsActive, ViewState["Emp_ID"].ToString(), LedgerStatusActive }, "dataset");

                                objdb.ByProcedure("SpFinLedgerTx",
                                     new string[] { "flag", "Ledger_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "VoucherTx_Date" },
                                     new string[] { "9", Ledger_ID, "Closing Balance", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), "0", "Sub Ledger", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

                                if (ddlBalBillByBill.SelectedValue == "Yes")
                                {

                                    Response.Redirect("AlterLedgerReference.aspx?Ledger_ID=" + objdb.Encrypt(Ledger_ID));


                                }
                                else
                                {
                                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation SuccessfullyCompleted");
                                    ClearText();

                                }
                            }
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString() + "');", true);
                            
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString() + "');", true);
                        }
                    }
                }
                else if (btnSave.Text == "Update" && ViewState["Ledger_ID"].ToString() != "0")
                {


                    ds = objdb.ByProcedure("SpFinLedgerMaster",
                    new string[] { "flag", "Ledger_ID", "Ledger_Name", "Ledger_Name_Hindi", "Ledger_Code", "Ledger_HeadID", "Ledger_Alias", "AccountHolderName", "IFSCCode", "BankName", "Branch", "InventoryAffected", "State_ID", "Mailing_Name", "Mailing_Address", "EffectiveDateforReconsilation", "Acnt_No", "PinCode", "Pan_No", "GST_No", "RegistrationTypes", "Ledger_UpdatedBy", "MaintainBalancesBillByBill", "MobileNo", "Email", "City", "FinancialYear", "Year", "GSTApplicable", "CostCentre", "Office_ID" },
                    new string[] { "6", ViewState["Ledger_ID"].ToString(), txtLedgerName.Text.Trim(), txtLedgerNameHindi.Text.Trim(), txtLedgerCode.Text, ddlHeadName.SelectedValue.ToString(), txtLedgerAlias.Text, AcholderName, txtifsccode.Text, txtbankname.Text, txtbranch.Text, ddlInventoryAffected.SelectedValue.ToString(), ddlState.SelectedValue.ToString(), txtMailing_Name.Text.Trim(), txtMailing_Address.Text.Trim(), EffectiveDate, txtacntno.Text, txtpincode.Text, txtMailing_PanNo.Text.Trim(), txtGSTNo.Text, ddlRegistrationType.SelectedItem.Text, ViewState["Emp_ID"].ToString(), ddlBalBillByBill.SelectedValue, txtMobileno.Text, txtEmail.Text, txtCity.Text, FinancialYear.ToString(), Year.ToString(), ddlGSTApplicable.SelectedValue, ddlCostCentre.SelectedValue, ViewState["Office_ID"].ToString() }, "dataset");
                    if(ds !=null && ds.Tables.Count > 0)
                    {
                        if(ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            if (ddlGSTApplicable.SelectedValue == "Yes")
                            {
                                string IsActive = "1";

                                objdb.ByProcedure("SpFinLedgerGSTDetails", new string[] { "flag", "Ledger_ID", "Description", "HSN_Code", "Taxbility", "HSN_IntegratedTax", "HSN_CGST", "HSN_SGST", "IsActive", "UpdatedBy", "Typeofsupply", "Isreversechargeapplicable", "GSTApplicable", "ApplicableFrom", "IsIneligibleforinputcredit" }, new string[] { "0", ViewState["Ledger_ID"].ToString(), txtDescription.Text, ddlHSNCode.SelectedItem.Text, ddlTaxability.SelectedValue, txtIGST.Text, txtCGST.Text, txtSGST.Text, IsActive, ViewState["Emp_ID"].ToString(), ddltypeofsupply.SelectedValue, ddlrcm.SelectedValue.ToString(), ddlGSTApplicable.SelectedValue.ToString(), Convert.ToDateTime(txtapplicablefrom.Text, cult).ToString("yyyy/MM/dd"), ddlineligibleforinputcredit.SelectedItem.Text }, "dataset");

                            }
                            else
                            {
                                objdb.ByProcedure("SpFinLedgerGSTDetails", new string[] { "flag", "Ledger_ID", "UpdatedBy", "GSTApplicable", "ApplicableFrom" }, new string[] { "2", ViewState["Ledger_ID"].ToString(), ViewState["Emp_ID"].ToString(), ddlGSTApplicable.SelectedValue.ToString(), Convert.ToDateTime(txtapplicablefrom.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                            }
                            if (ddlBalBillByBill.SelectedValue == "Yes")
                            {
                                if (decimal.Parse(txtOpeningBalance.Text) == 0)
                                {
                                    LedgerStatusActive = "1";
                                    LedgerTx_IsActive = "1";
                                }
                                else
                                {
                                    LedgerStatusActive = "0";
                                    LedgerTx_IsActive = "0";
                                }

                            }
                            else
                            {
                                LedgerStatusActive = "1";
                                LedgerTx_IsActive = "1";
                            }
                            objdb.ByProcedure("SpFinLedgerChild", new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_IsActive", "LedgerChild_UpdatedBy", "LedgerStatusActive" }, new string[] { "5", ViewState["Ledger_ID"].ToString(), ViewState["Current_Office_ID"].ToString(), LedgerChild_IsActive, ViewState["Emp_ID"].ToString(), LedgerStatusActive }, "dataset");



                            string LedgerTx_Amount = "0";
                            LedgerTx_Amount = txtOpeningBalance.Text;
                            if (ddlDrCr.SelectedItem.Text == "Credit")
                            {
                                LedgerTx_Amount = LedgerTx_Amount;
                            }
                            else
                            {
                                LedgerTx_Amount = "-" + LedgerTx_Amount;
                            }
                            objdb.ByProcedure("SpFinLedgerTx",
                             new string[] { "flag", "Ledger_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "VoucherTx_Date" },
                             new string[] { "3", ViewState["Ledger_ID"].ToString(), "Closing Balance", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), ViewState["Current_Office_ID"].ToString(), LedgerTx_IsActive, ViewState["Emp_ID"].ToString(), "0", "Sub Ledger", Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                            if (ddlBalBillByBill.SelectedValue == "Yes")
                            {
                                Response.Redirect("AlterLedgerReference.aspx?Ledger_ID=" + objdb.Encrypt(ViewState["Ledger_ID"].ToString()));
                            }
                            {
                                Response.Redirect("LedgerDetailB.aspx");
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                            }
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString() + "');", true);

                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString() + "');", true);
                        }
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
    protected void GetHeadDetail()
    {
        lblMsg.Text = "";
        try
        {
            if (ddlHeadName.SelectedIndex > 0)
            {
                txtachlder_name.Text = txtLedgerName.Text;
                string HeadID = ddlHeadName.SelectedValue.ToString();
                ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Head_ID" }, new string[] { "31", ddlHeadName.SelectedValue.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "true")
                    {
                        hfvalue.Value = "true";


                    }
                    else
                    {
                        hfvalue.Value = "false";

                    }
                }
                GetSalesPurchaseLedger();
                GetSundrycreditorDebitorLedger();
                GetIcomeExpenseLedger();
                DataView dv = new DataView();
                dv = ds5.Tables[0].DefaultView;
                dv.RowFilter = "Head_ID = '" + HeadID + "'";
                DataTable dt = dv.ToTable();
                if (dt != null && dt.Rows.Count > 0)
                {
                    pnacndetails.Enabled = true;
                    ddlType.Enabled = true;
                    txtachlder_name.Attributes.Add("readonly", "readonly");
                    GSTPanel.Enabled = false;
                    ddlGSTApplicable.SelectedValue = "No";
                }
                else
                {
                    pnacndetails.Enabled = false;
                    GSTPanel.Enabled = true;
                    ddlType.Enabled = false;
                }
            }

            else
            {
                pnacndetails.Enabled = false;
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlHeadName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlType.SelectedValue = "Other";
        GetHeadDetail();
    }
    protected void FillLedgerDetail()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID", "Cuurent_Office_ID"}, new string[] { "18", ViewState["Ledger_ID"].ToString(), ViewState["Office_ID"].ToString(), ViewState["Current_Office_ID"].ToString()}, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlHeadName.ClearSelection();
                ddlHeadName.Items.FindByValue(ds.Tables[0].Rows[0]["Ledger_HeadID"].ToString()).Selected = true;
                string HeadID = ds.Tables[0].Rows[0]["Ledger_HeadID"].ToString();
                // ddlHeadName.Enabled = false;
				// if(ds.Tables[5].Rows.Count > 0)
                // {
                    // if(ds.Tables[5].Rows[0]["Status"].ToString() == "1")
                    // {
                        // ddlHeadName.Enabled = true;
                    // }
                // }
                txtLedgerAlias.Text = ds.Tables[0].Rows[0]["Ledger_Alias"].ToString();
                txtLedgerCode.Text = ds.Tables[0].Rows[0]["Ledger_Code"].ToString();
                txtLedgerName.Text = ds.Tables[0].Rows[0]["Ledger_Name"].ToString();
                //if (ds.Tables[0].Rows[0]["Type"].ToString() != "" && ds.Tables[0].Rows[0]["Type"].ToString() != null)
                //{
                //    ddlType.ClearSelection();
                //    ddlType.Items.FindByValue(ds.Tables[0].Rows[0]["Type"].ToString()).Selected = true;
                //    //ddlType.Enabled = true;
                //}
                txtLedgerNameHindi.Text = ds.Tables[0].Rows[0]["Ledger_Name_Hindi"].ToString();
                txtMailing_Name.Text = ds.Tables[0].Rows[0]["Mailing_Name"].ToString();
                txtMailing_Address.Text = ds.Tables[0].Rows[0]["Mailing_Address"].ToString();
                ddlState.ClearSelection();
                ddlState.Items.FindByValue(ds.Tables[0].Rows[0]["State_ID"].ToString()).Selected = true;
                txtacntno.Text = ds.Tables[0].Rows[0]["Acnt_No"].ToString();
                txtMailing_PanNo.Text = ds.Tables[0].Rows[0]["Pan_No"].ToString();
                txtpincode.Text = ds.Tables[0].Rows[0]["PinCode"].ToString();
                txtGSTNo.Text = ds.Tables[0].Rows[0]["GST_No"].ToString();
                txtachlder_name.Text = ds.Tables[0].Rows[0]["AccountHolderName"].ToString();
                hfachlder_name.Value = ds.Tables[0].Rows[0]["AccountHolderName"].ToString();
                txtifsccode.Text = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                txtbankname.Text = ds.Tables[0].Rows[0]["BankName"].ToString();
                txtbranch.Text = ds.Tables[0].Rows[0]["Branch"].ToString();
                ddlInventoryAffected.ClearSelection();
                ddlInventoryAffected.Items.FindByValue(ds.Tables[0].Rows[0]["InventoryAffected"].ToString()).Selected = true;
                ddlBalBillByBill.ClearSelection();
                ddlBalBillByBill.Items.FindByValue(ds.Tables[0].Rows[0]["MaintainBalancesBillByBill"].ToString()).Selected = true;
                if (ds.Tables[0].Rows[0]["CostCentre"].ToString() != null && ds.Tables[0].Rows[0]["CostCentre"].ToString() != "")
                {
                    ddlCostCentre.ClearSelection();
                    ddlCostCentre.Items.FindByValue(ds.Tables[0].Rows[0]["CostCentre"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["City"].ToString() != "")
                {
                    txtCity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MobileNo"].ToString() != "")
                {
                    txtMobileno.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Email"].ToString() != "")
                {
                    txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                }
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

                ddlGSTApplicable.ClearSelection();
                ddlGSTApplicable.Items.FindByText(ds.Tables[0].Rows[0]["GSTApplicable"].ToString()).Selected = true;
                AcountDetailsHideShow();
                DataView dv = new DataView();
                dv = ds5.Tables[0].DefaultView;
                dv.RowFilter = "Head_ID = '" + HeadID + "'";
                DataTable dt = dv.ToTable();
                if (dt != null && dt.Rows.Count > 0)
                {
                    pnacndetails.Enabled = true;
                    GSTPanel.Enabled = false;
                    ddlType.Enabled = true;
                }
                else
                {
                    pnacndetails.Enabled = false;
                    GSTPanel.Enabled = true;
                    ddlType.Enabled = false;
                }


            } if (ds.Tables[4].Rows.Count > 0)
            {

                if (ds.Tables[4].Rows[0]["Typeofsupply"].ToString() != "")
                {
                    ddltypeofsupply.ClearSelection();
                    ddltypeofsupply.Items.FindByText(ds.Tables[4].Rows[0]["Typeofsupply"].ToString()).Selected = true;
                    FillHSN();
                    if (ds.Tables[4].Rows[0]["HSN_Code"].ToString() != "")
                    {
                        ddlHSNCode.ClearSelection();
                        ddlHSNCode.Items.FindByText(ds.Tables[4].Rows[0]["HSN_Code"].ToString()).Selected = true;
                    }

                }
                txtDescription.Text = ds.Tables[4].Rows[0]["Description"].ToString();
                if (ds.Tables[4].Rows[0]["Taxbility"].ToString() != "")
                {
                    ddlTaxability.ClearSelection();
                    ddlTaxability.Items.FindByValue(ds.Tables[4].Rows[0]["Taxbility"].ToString()).Selected = true;
                }
                txtIGST.Text = ds.Tables[4].Rows[0]["HSN_IntegratedTax"].ToString();
                txtCGST.Text = ds.Tables[4].Rows[0]["HSN_CGST"].ToString();
                txtSGST.Text = ds.Tables[4].Rows[0]["HSN_SGST"].ToString();
                ddlrcm.ClearSelection();
                ddlrcm.Items.FindByValue(ds.Tables[4].Rows[0]["Isreversechargeapplicable"].ToString()).Selected = true;
                if (ds.Tables[4].Rows[0]["IsIneligibleforinputcredit"].ToString() != "")
                {
                    ddlineligibleforinputcredit.ClearSelection();
                    ddlineligibleforinputcredit.Items.FindByValue(ds.Tables[4].Rows[0]["IsIneligibleforinputcredit"].ToString()).Selected = true;
                }
                if (ds.Tables[4].Rows[0]["ApplicableFrom"].ToString() != "")
                {
                    txtapplicablefrom.Text = ds.Tables[4].Rows[0]["ApplicableFrom"].ToString();
                }
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                if (ds.Tables[1].Rows[0]["LedgerTx_Amount"].ToString() != "")
                {
                    txtOpeningBalance.Text = ds.Tables[1].Rows[0]["LedgerTx_Amount"].ToString();
                    txtVoucherTx_Date.Text = ds.Tables[1].Rows[0]["VoucherTx_Date"].ToString();
                    if (ds.Tables[1].Rows[0]["DrCr"].ToString() != "")
                    {
                        ddlDrCr.ClearSelection();
                        ddlDrCr.Items.FindByValue(ds.Tables[1].Rows[0]["DrCr"].ToString()).Selected = true;
                    }
                }

            }
            btnSave.Text = "Update";
            clear.Visible = false;
           // ddlHeadName.Enabled = false;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void GetVoucherDate()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {

                ViewState["VoucherDate"] = ds.Tables[0].Rows[0]["VoucherDate"].ToString();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    public string FirstLetterToUpper(string str)
    {
        string txt = cult.TextInfo.ToTitleCase(str.ToLower());
        return txt;
    }
    protected void AcountDetailsHideShow()
    {
        lblMsg.Text = "";
        try
        {
            //if(ddlHeadName.SelectedIndex > 0)
            //{
                DataSet ds2 = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Head_ID" }, new string[] { "34", ddlHeadName.SelectedValue.ToString() }, "dataset");
                if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                {
                    ds5 = ds2;

                }
           // }
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlTaxability_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        try
        {
            if (ddlTaxability.SelectedIndex > 0)
            {
                FillTaxRate();
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
    protected void FillTaxRate()
    {
        lblMsg.Text = "";
        try
        {
            if (ddlHSNCode.SelectedIndex > 0)
            {
                if (ddlTaxability.SelectedValue == "Yes")
                {
                    ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "HSN_Code" }, new string[] { "40", ddlHSNCode.SelectedItem.Text }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        txtIGST.Text = ds.Tables[0].Rows[0]["HSN_IntegratedTax"].ToString();
                        txtCGST.Text = ds.Tables[0].Rows[0]["HSN_CGST"].ToString();
                        txtSGST.Text = ds.Tables[0].Rows[0]["HSN_SGST"].ToString();
                    }
                    else
                    {
                        txtIGST.Text = "0";
                        txtCGST.Text = "0";
                        txtSGST.Text = "0";
                    }
                }
                else
                {
                    txtIGST.Text = "0";
                    txtCGST.Text = "0";
                    txtSGST.Text = "0";
                }
            }
            else
            {
                txtIGST.Text = "0";
                txtCGST.Text = "0";
                txtSGST.Text = "0";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlHSNCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        try
        {
            if (ddlHSNCode.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpFinHSNMaster", new string[] { "flag", "HSN_ID" }, new string[] { "12", ddlHSNCode.SelectedValue }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtDescription.Text = ds.Tables[0].Rows[0]["HSN_Description"].ToString();
                    string TaxableType = ds.Tables[0].Rows[0]["Texiblity"].ToString();
                    if (TaxableType == "Taxable")
                    {
                        ddlTaxability.SelectedValue = "Yes";
                    }
                    else
                    {
                        ddlTaxability.SelectedValue = "No";
                    }

                }

            }
            else
            {
                txtDescription.Text = "";
                ddlTaxability.SelectedValue = "NA";

            }
            FillTaxRate();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetSalesPurchaseLedger()
    {
        try
        {
            DataSet ds1 = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Head_ID" }, new string[] { "42", ddlHeadName.SelectedValue.ToString() }, "dataset");
            {
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows[0]["Status"].ToString() == "true")
                    {
                        ddlInventoryAffected.SelectedValue = "Yes";
                        ddlBalBillByBill.SelectedValue = "No";

                    }
                    else
                    {
                        txtOpeningBalance.Enabled = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetSundrycreditorDebitorLedger()
    {
        try
        {
            DataSet ds1 = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Head_ID" }, new string[] { "41", ddlHeadName.SelectedValue.ToString() }, "dataset");
            {
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows[0]["Status"].ToString() == "true")
                    {
                        ddlInventoryAffected.SelectedValue = "No";
                        ddlBalBillByBill.SelectedValue = "Yes";


                    }
                    else
                    {
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetIcomeExpenseLedger()
    {
        try
        {
            DataSet ds1 = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Head_ID" }, new string[] { "45", ddlHeadName.SelectedValue.ToString() }, "dataset");
            {
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows[0]["Status"].ToString() == "true")
                    {
                        ddlInventoryAffected.SelectedValue = "No";
                        ddlBalBillByBill.SelectedValue = "No";

                    }
                    else
                    {
                        txtOpeningBalance.Enabled = true;

                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddltypeofsupply_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillHSN();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> SearchCustomers(string Ledger_Name)
    {
        List<string> customers = new List<string>();
        try
        {
            DataView dv = new DataView();
            dv = ds6.Tables[0].DefaultView;
            dv.RowFilter = "Ledger_Name like '%" + Ledger_Name + "%'";
            DataTable dt = dv.ToTable();

            foreach (DataRow rs in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    customers.Add(rs[col].ToString());
                }
            }

        }
        catch { }
        return customers;
    }
    private void GetLedgerName()
    {
        try
        {
            string Office_ID = Session["Office_ID"].ToString();
            DataSet ds4 = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Office_ID" }, new string[] { "50", Office_ID }, "dataset");
            ds6 = ds4;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void setDate()
    {
        try
        {
            string VoucherDate = ViewState["VoucherDate"].ToString();
            string sDate = (Convert.ToDateTime(VoucherDate, cult).ToString("yyyy/MM/dd")).ToString();
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
            txtVoucherTx_Date.Text = "01" + '/' + "04" + '/' + FY.ToString();
            txtapplicablefrom.Text = "01" + '/' + "04" + '/' + FY.ToString();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtVoucherTx_Date_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (txtVoucherTx_Date.Text != "")
            {

                ds = objdb.ByProcedure("SpFinVoucherDate",
                    new string[] { "flag", "Office_ID", "VoucherDate" },
                    new string[] { "5", ViewState["Office_ID"].ToString(), Convert.ToDateTime(txtVoucherTx_Date.Text, cult).ToString("yyyy/MM/dd") }, "dataset");


                if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "TRUE")
                {
                    //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please select Date according to Financial Year which has been set from Voucher Date.');", true);
                    //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert!", "Please select valid date.");
                    setDate();

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlBalBillByBill_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBalBillByBill.SelectedValue.ToString() == "Yes")
        {
            ddlCostCentre.SelectedValue = "No";
        }
    }
    protected void ddlCostCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCostCentre.SelectedValue.ToString() == "Yes")
        {
            ddlBalBillByBill.SelectedValue = "No";
        }
    }
}
