using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_Finance_FinLedgerOfficeMapping_New : System.Web.UI.Page
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
                    FillOfficeDropdown();
                    FillLedgerName();
                    GetVoucherDate();
                    FillOfficeType();
                   // FillOffice();
                    panel1.Visible = false;
                    btnSave.Visible = false;
                   
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
    protected void FillLedgerName()
    {
        try
        {
            ddlLedgerName.Items.Clear();
            ds = objdb.ByProcedure("SpFinMapUnMapLedger", new string[] { "flag", "Office_ID" }, new string[] { "5",ddlOffice.SelectedValue.ToString()}, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                ddlLedgerName.DataSource = ds;
                ddlLedgerName.DataTextField = "Ledger_Name";
                ddlLedgerName.DataValueField = "Ledger_ID";
                ddlLedgerName.DataBind();
                ddlLedgerName.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlLedgerName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOfficeDropdown()
    {
        try
        {
            if (ViewState["Office_ID"].ToString() == "1")
            {
                ddlOffice.Enabled = true;
            }
            ds = objdb.ByProcedure("SpFinVoucherTx",
                   new string[] { "flag" },
                   new string[] { "26" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillOfficeType()
    {
        try
        {
            ddlOfficeType.Items.Clear();
            ds = objdb.ByProcedure("USP_FinGetOfficeType", new string[] { }, new string[] { }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                ddlOfficeType.DataSource = ds;
                ddlOfficeType.DataTextField = "OfficeType_Title";
                ddlOfficeType.DataValueField = "OfficeType_ID";
                ddlOfficeType.DataBind();
                ddlOfficeType.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlOfficeType.Items.Insert(0, new ListItem("Select", "0"));
            }
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


                //Start For Voucher No

                //End

            }
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
            string Active = "";
            string ExistMsg = "";
            string Ledger_IsActive = "1";
            string LedgerTx_Amount = "0";
            string VoucherTx_Date = "";
            if (msg.Trim() == "")
            {
                
                

                foreach (ListItem item in chkOffice.Items)
                {

                    if (item.Selected == true)
                    {
                        objdb.ByProcedure("SpFinMapUnMapLedger", new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_IsActive", "LedgerChild_UpdatedBy", "Active" },
                            new string[] { "3", ddlLedgerName.SelectedValue.ToString(), item.Value, "1", ViewState["Emp_ID"].ToString(), "Yes" }, "dataset");
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
                        VoucherDate = "01" + '/' + "04" + '/' + FY.ToString();
                        objdb.ByProcedure("SpFinLedgerTx",
                             new string[] { "flag", "Ledger_ID", "VoucherTx_Type", "LedgerTx_Amount", "LedgerTx_Month", "LedgerTx_Year", "LedgerTx_FY", "Office_ID", "LedgerTx_IsActive", "LedgerTx_InsertedBy", "LedgerTx_OrderBy", "LedgerTx_Type", "VoucherTx_Date" },
                             new string[] { "6", ddlLedgerName.SelectedValue.ToString(), "Closing Balance", LedgerTx_Amount, Month.ToString(), Year.ToString(), FinancialYear.ToString(), item.Value, "1", ViewState["Emp_ID"].ToString(), "0", "Sub Ledger", Convert.ToDateTime(VoucherDate, cult).ToString("yyyy/MM/dd") }, "dataset");
                    }
                    else
                    {
                        objdb.ByProcedure("SpFinMapUnMapLedger", new string[] { "flag", "Ledger_ID", "Office_ID", "LedgerChild_IsActive", "LedgerChild_UpdatedBy", "Active" },
                            new string[] { "3", ddlLedgerName.SelectedValue.ToString(), item.Value, "1", ViewState["Emp_ID"].ToString(), "No" }, "dataset");
                    }
                }
          

                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                ddlLedgerName.ClearSelection();
                FillData();
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
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Usp_FinLedgerMappingOfficeWise",
                 new string[] { "flag", "OfficeType_ID" },
                 new string[] { "2",ddlOfficeType.SelectedValue}, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                chkOffice.DataSource = ds.Tables[0];
                chkOffice.DataTextField = "Office_Name";
                chkOffice.DataValueField = "Office_ID";
                chkOffice.DataBind();

              
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
    protected void FillData()
    {
        try
        {
            if (ddlLedgerName.SelectedIndex > 0)
            {
                panel1.Visible = true;
                lblMsg.Text = "";
                btnSave.Visible = true;
                chkOffice.ClearSelection();
                ds = objdb.ByProcedure("Usp_FinLedgerMappingOfficeWise",
                    new string[] { "flag", "Ledger_ID", "OfficeType_ID" },
                    new string[] { "1", ddlLedgerName.SelectedValue.ToString(),ddlOfficeType.SelectedValue }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string Value = ds.Tables[0].Rows[i]["Office_ID"].ToString();

                            foreach (ListItem item in chkOffice.Items)
                            {
                                if (item.Value == Value)
                                {
                                    item.Selected = true;
                                    item.Enabled = true;
                                    DataSet ds11 = objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "38", ddlLedgerName.SelectedValue.ToString(), item.Value }, "dataset");
                                    if (ds11 != null)
                                    {

                                        if (ds11.Tables[0].Rows[0]["Status"].ToString() == "True")
                                        {
                                            item.Enabled = false;
                                        }
                                        else
                                        {
                                            item.Enabled = true;
                                        }
                                    }
                                }
                            }
                            
                        }
                        btnSave.Visible = true;
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                panel1.Visible = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ShowHideDeleteBtn()
    {
        try
        {
            btnDel.Visible = true;
            if (chkOfficeAll.Checked == true)
            {
                btnDel.Visible = false;
                return;
            }
            foreach (ListItem item in chkOffice.Items)
            {
                if (item.Selected == true)
                {
                    btnDel.Visible = false;
                    return;
                }
            }
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void chkOfficeAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ShowHideDeleteBtn();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
    
    protected void chkOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ShowHideDeleteBtn();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
    protected void btnDel_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                
                objdb.ByProcedure("SpFinMapUnMapLedger", new string[] { "flag", "Ledger_ID", "Ledger_UpdatedBy", "Office_ID" }, new string[] { "4", ddlLedgerName.SelectedValue.ToString(), ViewState["Emp_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                FillLedgerName();
                FillData();
                
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
            FillLedgerName();
            panel1.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FillOffice();
            FillData();
            ShowHideDeleteBtn();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
}