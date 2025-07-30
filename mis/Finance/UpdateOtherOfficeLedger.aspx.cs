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


public partial class mis_Finance_UpdateOtherOfficeLedger : System.Web.UI.Page
{
    DataSet ds;
    static DataSet ds5,ds6;
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
                    ViewState["Ledger_ID"] = "0";                   
                    txtachlder_name.Attributes.Add("readonly", "readonly");                  
                    //ddlGSTApplicable.Enabled=false;
                    FillHead();
                    FillState();                    
                    FillDropdown();
                    FillLedger();
                    FillHSN();
                    pnlbody.Visible = false;
                    AcountDetailsHideShow();
                }
                else
                {
                   
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
    protected void FillDropdown()
    {
        try
        {
            if (ViewState["Office_ID"].ToString() == "1")
            {
                ddlOffice.Enabled = true;
            }
            ds = objdb.ByProcedure("SpFinRptLedgerBillByBillRef",
                   new string[] { "flag" },
                   new string[] { "0" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                //ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlbody.Visible = false;
            lblMsg.Text = "";
            FillLedger();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void AcountDetailsHideShow()
    {
        lblMsg.Text = "";
        try
        {

            DataSet ds2 = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Head_ID" }, new string[] { "34", ddlHeadName.SelectedValue.ToString() }, "dataset");
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                ds5 = ds2;
              
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillLedger()
    {
        try
        {
            lblMsg.Text = "";
            ddlLedger.DataSource = null;
            ddlLedger.DataBind();
            ddlLedger.Items.Clear();
            ddlLedger.Items.Insert(0, new ListItem("Select", "0"));
            DataSet ds1 = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Office_ID" }, new string[] { "59", ddlOffice.SelectedValue.ToString() }, "dataset");
                if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                {
                    ddlLedger.DataSource = ds1;
                    ddlLedger.DataTextField = "Ledger_Name";
                    ddlLedger.DataValueField = "Ledger_ID";
                    ddlLedger.DataBind();
                    ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

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
    protected void FillHead()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinMasterHead",
                          new string[] { "flag" },
                          new string[] { "14" }, "dataset");
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if(msg == "")
            {
                int Status = 0;
                txtLedgerName.Text = FirstLetterToUpper(txtLedgerName.Text);
                txtachlder_name.Text = FirstLetterToUpper(txtachlder_name.Text);
                ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_Name", "Ledger_ID", "Ledger_HeadID", "Office_ID" }, new string[] { "10", txtLedgerName.Text, ViewState["Ledger_ID"].ToString(), ddlHeadName.SelectedValue.ToString(),ddlOffice.SelectedValue.ToString()}, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (btnSave.Text == "Update" && ViewState["Ledger_ID"].ToString() != "0" && Status == 0)
                {
                   
                  objdb.ByProcedure("SpFinLedgerMaster",
                  new string[] { "flag", "Ledger_ID", "Ledger_Name", "Ledger_HeadID", "Ledger_Alias", "AccountHolderName", "IFSCCode", "BankName", "Branch", "InventoryAffected", "State_ID", "Mailing_Name", "Mailing_Address", "Acnt_No", "PinCode", "Pan_No", "GST_No", "RegistrationTypes", "Ledger_UpdatedBy", "MaintainBalancesBillByBill", "MobileNo", "Email", "City",  "GSTApplicable" },
                  new string[] { "60", ViewState["Ledger_ID"].ToString(), txtLedgerName.Text.Trim(), ddlHeadName.SelectedValue.ToString(), txtLedgerAlias.Text, txtachlder_name.Text, txtifsccode.Text, txtbankname.Text, txtbranch.Text, ddlInventoryAffected.SelectedValue.ToString(), ddlState.SelectedValue.ToString(), txtMailing_Name.Text.Trim(), txtMailing_Address.Text.Trim(),txtacntno.Text, txtpincode.Text, txtMailing_PanNo.Text.Trim(), txtGSTNo.Text, ddlRegistrationType.SelectedItem.Text, ViewState["Emp_ID"].ToString(), ddlBalBillByBill.SelectedValue, txtMobileno.Text, txtEmail.Text, txtCity.Text,  ddlGSTApplicable.SelectedValue }, "dataset");
                  
                    if (ddlGSTApplicable.SelectedValue == "Yes")
                    {
                        string IsActive = "1";

                        objdb.ByProcedure("SpFinLedgerGSTDetails", new string[] { "flag", "Ledger_ID", "Description", "HSN_Code", "Taxability", "HSN_IntegratedTax", "HSN_CGST", "HSN_SGST", "IsActive", "UpdatedBy", "Typeofsupply", "Isreversechargeapplicable" }, new string[] { "0", ViewState["Ledger_ID"].ToString(), txtDescription.Text, ddlHSNCode.SelectedItem.Text, ddlTaxability.SelectedValue, txtIGST.Text, txtCGST.Text, txtSGST.Text, IsActive, ViewState["Emp_ID"].ToString(), ddltypeofsupply.SelectedValue, ddlrcm.SelectedValue.ToString() }, "dataset");

                    }
                    else
                    {
                        objdb.ByProcedure("SpFinLedgerGSTDetails", new string[] { "flag", "Ledger_ID", "UpdatedBy" }, new string[] { "2", ViewState["Ledger_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                    }
                   lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                   pnlbody.Visible = false;
                   ddlLedger.ClearSelection();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Ledger name is already exist.');", true);
                    lblMsg.Text = "";
                    
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
    protected void FillLedgerDetail()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID"}, new string[] { "58", ViewState["Ledger_ID"].ToString()}, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlHeadName.ClearSelection();
                ddlHeadName.Items.FindByValue(ds.Tables[0].Rows[0]["Ledger_HeadID"].ToString()).Selected = true;
                string HeadID = ds.Tables[0].Rows[0]["Ledger_HeadID"].ToString();

                if (ViewState["Emp_ID"].ToString() == "79" || ViewState["Emp_ID"].ToString() == "541")
                    ddlHeadName.Enabled = true;
                else
                    ddlHeadName.Enabled = false;

                
                txtLedgerAlias.Text = ds.Tables[0].Rows[0]["Ledger_Alias"].ToString();
                txtLedgerName.Text = ds.Tables[0].Rows[0]["Ledger_Name"].ToString();
                txtMailing_Name.Text = ds.Tables[0].Rows[0]["Mailing_Name"].ToString();
                txtMailing_Address.Text = ds.Tables[0].Rows[0]["Mailing_Address"].ToString();
                ddlState.ClearSelection();
                ddlState.Items.FindByValue(ds.Tables[0].Rows[0]["State_ID"].ToString()).Selected = true;
                txtacntno.Text = ds.Tables[0].Rows[0]["Acnt_No"].ToString();
                //ddlFinancialYear.ClearSelection();
                //ddlFinancialYear.Items.FindByText(ds.Tables[0].Rows[0]["FinancialYear"].ToString()).Selected = true;
                txtMailing_PanNo.Text = ds.Tables[0].Rows[0]["Pan_No"].ToString();
                txtpincode.Text = ds.Tables[0].Rows[0]["PinCode"].ToString();
                txtGSTNo.Text = ds.Tables[0].Rows[0]["GST_No"].ToString();
                txtachlder_name.Text = ds.Tables[0].Rows[0]["AccountHolderName"].ToString();
                txtifsccode.Text = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                txtbankname.Text = ds.Tables[0].Rows[0]["BankName"].ToString();
                txtbranch.Text = ds.Tables[0].Rows[0]["Branch"].ToString();
                ddlGSTApplicable.ClearSelection();
                ddlGSTApplicable.Items.FindByValue(ds.Tables[0].Rows[0]["GSTApplicable"].ToString()).Selected = true;
                ddlInventoryAffected.ClearSelection();
                ddlInventoryAffected.Items.FindByValue(ds.Tables[0].Rows[0]["InventoryAffected"].ToString()).Selected = true;
                ddlBalBillByBill.ClearSelection();
                ddlBalBillByBill.Items.FindByValue(ds.Tables[0].Rows[0]["MaintainBalancesBillByBill"].ToString()).Selected = true;
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
                DataSet ds1 = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Head_ID" }, new string[] { "31", ddlHeadName.SelectedValue.ToString() }, "dataset");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows[0]["Status"].ToString() == "true")
                    {
                        hfvalue.Value = "true";


                    }
                    else
                    {
                        hfvalue.Value = "false";

                    }
                }
                DataView dv = new DataView();
                dv = ds5.Tables[0].DefaultView;
                dv.RowFilter = "Head_ID = '" + HeadID + "'";
                DataTable dt = dv.ToTable();
                if (dt != null && dt.Rows.Count > 0)
                {
                    pnacndetails.Enabled = true;
                    GSTPanel.Enabled = false;
                }
                else
                {
                    pnacndetails.Enabled = false;
                    GSTPanel.Enabled = true;
                }
            }
            if (ds.Tables[1].Rows.Count > 0)
            {

                if (ds.Tables[1].Rows[0]["Typeofsupply"].ToString() != "")
                {
                    ddltypeofsupply.ClearSelection();
                    ddltypeofsupply.Items.FindByText(ds.Tables[1].Rows[0]["Typeofsupply"].ToString()).Selected = true;
                    FillHSN();
                    if (ds.Tables[1].Rows[0]["HSN_Code"].ToString() != "")
                    {
                        ddlHSNCode.ClearSelection();
                        ddlHSNCode.Items.FindByText(ds.Tables[1].Rows[0]["HSN_Code"].ToString()).Selected = true;
                    }

                }
                txtDescription.Text = ds.Tables[1].Rows[0]["Description"].ToString();
                //if (ds.Tables[4].Rows[0]["HSN_Code"].ToString() != "")
                //{
                //    ddlHSNCode.ClearSelection();
                //    ddlHSNCode.Items.FindByText(ds.Tables[4].Rows[0]["HSN_Code"].ToString()).Selected = true;
                //}
                if (ds.Tables[1].Rows[0]["Taxability"].ToString() != "")
                {
                    ddlTaxability.ClearSelection();
                    ddlTaxability.Items.FindByValue(ds.Tables[1].Rows[0]["Taxability"].ToString()).Selected = true;
                }
                txtIGST.Text = ds.Tables[1].Rows[0]["HSN_IntegratedTax"].ToString();
                txtCGST.Text = ds.Tables[1].Rows[0]["HSN_CGST"].ToString();
                txtSGST.Text = ds.Tables[1].Rows[0]["HSN_SGST"].ToString();
                ddlrcm.ClearSelection();
                ddlrcm.Items.FindByValue(ds.Tables[1].Rows[0]["Isreversechargeapplicable"].ToString()).Selected = true;
            }
                     
            btnSave.Text = "Update";

            if (ViewState["Emp_ID"].ToString() == "79" || ViewState["Emp_ID"].ToString() == "541")
                ddlHeadName.Enabled = true;
            else
                ddlHeadName.Enabled = false;
           // ddlHeadName.Enabled = false;
            
            

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

    protected void ddlLedger_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if(ddlLedger.SelectedIndex > 0)
            {
                pnlbody.Visible = true;
                ViewState["Ledger_ID"] = ddlLedger.SelectedValue.ToString();
                FillLedgerDetail();
            }
            else
            {
                pnlbody.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}
