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

public partial class mis_Finance_AlterLedgerReference : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(Session["Emp_ID"] != null)
            {
                if(!IsPostBack)
                {
                    lblMsg.Text = ""; 
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    GetVoucherDate();
                    FillLedger();
                    FillSchemeDropDown();
                    GridViewBillByBillDetail.DataSource = new string[] { };
                    GridViewBillByBillDetail.DataBind();
                    if (Request.QueryString["Ledger_ID"] != null)
                    {
                        string Ledger_ID = objdb.Decrypt(Request.QueryString["Ledger_ID"].ToString());
                        ViewState["Ledger_ID"] = Ledger_ID.ToString();
                        ddlLedger.ClearSelection();
                        ddlLedger.Items.FindByValue(Ledger_ID.ToString()).Selected = true;
                        ddlLedger.Enabled = false;
                        FillCurrentBalance();
                        FillGrid();
                        ViewState["BillByBillTx_ID"] = "0";
                    }
                }
                    
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillLedger()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinLedgerMaster",
                new string[] { "flag", "Office_ID", "MultipleHeadIDs" },
                new string[] { "35", ViewState["Office_ID"].ToString(), "1,2,3,4" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlLedger.DataTextField = "Ledger_Name";
                    ddlLedger.DataValueField = "Ledger_ID";
                    ddlLedger.DataSource = ds.Tables[0];
                    ddlLedger.DataBind();
                    ddlLedger.Items.Insert(0, new ListItem("Select", "0"));
                    
                }

            }
            
        }
        catch(Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillCurrentBalance()
    {
        try
        {
            lblMsg.Text = "";
            if (ddlLedger.SelectedIndex > 0)
            {

                DataSet ds1 = objdb.ByProcedure("SpFinLedgerTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "10", ddlLedger.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    txtOpeningBalance.Text = ds1.Tables[0].Rows[0]["SumLedgerTx_Amount"].ToString();
                    ViewState["Opening Balance"] = ds1.Tables[0].Rows[0]["AvailableAmount"].ToString();
                    hfvalue.Value = ViewState["Opening Balance"].ToString();
                }
            }
            else
            {

                txtOpeningBalance.Text = "";
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "13", ddlLedger.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {


                GridViewBillByBillDetail.DataSource = ds.Tables[0];
                GridViewBillByBillDetail.DataBind();
                //foreach(GridViewRow rows in GridViewBillByBillDetail.Rows)
                //{
                //    Label lblBillByBillTx_Ref = (Label)rows.FindControl("lblBillByBillTx_Ref");
                //    LinkButton lnkDelete = (LinkButton)rows.FindControl("lnkDelete");
                //    LinkButton lnkEdit = (LinkButton)rows.FindControl("lnkEdit");
                //    DataSet ds1 = objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "BillByBillTx_Ref", "Ledger_ID", "Office_ID" }, new string[] { "8", lblBillByBillTx_Ref.Text, ddlLedger.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                //    if(ds1!= null)
                //    {
                //        if(ds1.Tables[0].Rows[0]["status"].ToString() == "false")
                //        {
                //            lnkDelete.Visible = false;
                //            //lnkEdit.Visible = false;
                //        }
                //        else
                //        {
                //            lnkDelete.Visible = true;
                //            //lnkEdit.Visible = true;
                //        }
                //    }
                //}
                decimal Amount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                string Amt = Amount.ToString();
                ViewState["Amount"] = Amount;
                
                if (Amount == decimal.Parse(ViewState["Opening Balance"].ToString()))
                {
                    btnAddBillByBill.Enabled = false;
                    objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "36", ddlLedger.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                    if (Session["Office_ID"].ToString() == "1")
                    {
                        //Response.Redirect("LedgerDetailB.aspx");
                    }
                    else
                    {

                       //Response.Redirect("LedgerDetail.aspx");
                    }
                    
                }
                else
                {
                    btnAddBillByBill.Enabled = true;
                    decimal RemainingAmount = decimal.Parse(ViewState["Opening Balance"].ToString()) - Amount;
                    //txtBillByBillTx_Amount.Text = RemainingAmount.ToString();
                    if (RemainingAmount.ToString().Contains("-"))
                    {
                        txtBillByBillTx_Amount.Text = RemainingAmount.ToString().Replace(@"-", string.Empty);
                        ddlBillByBillTx_crdr.SelectedValue = "Dr";
                    }
                    else
                    {
                        txtBillByBillTx_Amount.Text = RemainingAmount.ToString();
                        ddlBillByBillTx_crdr.SelectedValue = "Cr";
                    }
                    objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "37", ddlLedger.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                }
                if (Amt.ToString().Contains("-"))
                {
                    Amt = Amt.Replace(@"-", string.Empty);
                    Amt = Amt + "Dr";
                }
                else
                {
                    Amt = Amt + "Cr";
                }
                lblremainingamnt.Text = Amt.ToString();
                //GridViewBillByBillDetail.FooterRow.Cells[1].Text = "<b>Total : </b>";
                //GridViewBillByBillDetail.FooterRow.Cells[2].Text = "<b>" + Amt.ToString() + "</b>";
                //GridViewBillByBillDetail.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            }
            else
            {
                GridViewBillByBillDetail.DataSource = new string[] { };
                GridViewBillByBillDetail.DataBind();
                btnAddBillByBill.Enabled = true;
                lblremainingamnt.Text = "0.00";
                if (ViewState["Opening Balance"].ToString().Contains("-"))
                {
                    txtBillByBillTx_Amount.Text = ViewState["Opening Balance"].ToString().Replace(@"-", string.Empty);
                    ddlBillByBillTx_crdr.SelectedValue = "Dr";
                }
                else
                {
                    txtBillByBillTx_Amount.Text = ViewState["Opening Balance"].ToString();
                    ddlBillByBillTx_crdr.SelectedValue = "Cr";
                }
                if (decimal.Parse(ViewState["Opening Balance"].ToString()) == 0)
                {
                    
                    objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "36", ddlLedger.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                    if (Session["Office_ID"].ToString() == "1")
                    {
                        Response.Redirect("LedgerDetailB.aspx");
                    }
                    else
                    {

                        Response.Redirect("LedgerDetail.aspx");
                    }

                }
                else
                {
                    objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "Ledger_ID", "Office_ID" }, new string[] { "37", ddlLedger.SelectedValue.ToString(), ViewState["Office_ID"].ToString() }, "dataset");
                }
               
                
            }
            

            GridViewBillByBillDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridViewBillByBillDetail.UseAccessibleHeader = true;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillSchemeDropDown()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinSchemeTx", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlSchemeTx_ID.DataSource = ds;
                ddlSchemeTx_ID.DataTextField = "SchemeTx_Name";
                ddlSchemeTx_ID.DataValueField = "SchemeTx_ID";
                ddlSchemeTx_ID.DataBind();
                ddlSchemeTx_ID.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlSchemeTx_ID.Items.Clear();
                ddlSchemeTx_ID.Items.Insert(0, new ListItem("Select", "0"));

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void ddlLedger_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            FillCurrentBalance();
            FillGrid();
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
            string Msg = "";
            if(ddlLedger.SelectedIndex == 0)
            {
                Msg += "Select Ledger Name. \\n";
            }
            if (txtBillByBillTx_Date.Text.Trim() == "")
            {
                Msg += "Enter Date. \\n";
            }
            if (txtBillByBillTx_Ref.Text.Trim() == "")
            {
                Msg += "Enter Name.\\n";
            }
            if (txtBillByBillTx_Amount.Text.Trim() == "")
            {
                Msg += "Enter Amount.\\n";
            }
            if (txtBillByBillTx_Amount.Text.Trim() != "")
            {
                decimal amount = decimal.Parse(txtBillByBillTx_Amount.Text);
                    if(amount == 0)
                    {
                        Msg += "Amount cannot be zero .\\n";
                    }
            }
            string sDate = (Convert.ToDateTime(txtBillByBillTx_Date.Text, cult).ToString("yyyy/MM/dd")).ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            int Month = int.Parse(datevalue.Month.ToString());
            int Year = int.Parse(datevalue.Year.ToString());
            int FY = Year;
            string FinancialYear = Year.ToString();
            string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
            FinancialYear = "";
            string BillByBillTx_OrderDate = "";
            if (txtBillByBillTx_OrderDate.Text != "")
            {
                BillByBillTx_OrderDate = Convert.ToDateTime(txtBillByBillTx_OrderDate.Text, cult).ToString("yyyy/MM/dd");
            }
            else
            {
                BillByBillTx_OrderDate = "";
            }
            if (Month <= 3)
            {
                FY = Year - 1;
                FinancialYear = FY.ToString() + "-" + LFY.ToString();
            }
            else
            {

                FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
            }
            if (ddlBillByBillTx_crdr.SelectedValue == "Dr")
            {
                txtBillByBillTx_Amount.Text = "-" + txtBillByBillTx_Amount.Text;
            }
            else
            {
                txtBillByBillTx_Amount.Text = txtBillByBillTx_Amount.Text;
            }
            if(Msg == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "BillByBillTx_Ref", "Office_ID", "Ledger_ID", "BillByBillTx_ID" }, new string[] { "6", txtBillByBillTx_Ref.Text, ViewState["Office_ID"].ToString(), ddlLedger.SelectedValue.ToString(), ViewState["BillByBillTx_ID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (Status == 0 && btnAddBillByBill.Text == "Add" && ViewState["BillByBillTx_ID"].ToString() == "0")
                {
                    
                    objdb.ByProcedure("SpFinBillByBillTx",
                                    new string[] { "flag", "Ledger_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Amount", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_IsActive", "BillByBillTx_OrderBy", "BillByBillTx_OrderNo", "SchemeTx_ID", "BillByBillTx_OrderDate", "BillByBillTx_ConsigneeName", "BillByBillTx_ItemGroup" },
                                    new string[] { "3", ddlLedger.SelectedValue.ToString(), ddlRefType.SelectedValue.ToString(), txtBillByBillTx_Ref.Text, txtBillByBillTx_Amount.Text, Convert.ToDateTime(txtBillByBillTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), "1", "1", txtBillByBillTx_OrderNo.Text, ddlSchemeTx_ID.SelectedValue.ToString(), BillByBillTx_OrderDate, txtBillByBillTx_ConsigneeName.Text, txtBillByBillTx_ItemGroup.Text }, "dataset");
                    //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillGrid();
                    decimal Amount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                    string Amt = Amount.ToString();
                    ViewState["Amount"] = Amount;
                    if (Amount == decimal.Parse(ViewState["Opening Balance"].ToString()))
                    {
                        
                        if (Session["Office_ID"].ToString() == "1")
                        {
                            Response.Redirect("LedgerDetailB.aspx");
                        }
                        else
                        {

                            Response.Redirect("LedgerDetail.aspx");
                        }

                    }
                    ClearText();
                }
                else if(Status == 0 && btnAddBillByBill.Text == "Update" && ViewState["BillByBillTx_ID"].ToString() != "0")
                {
                    objdb.ByProcedure("SpFinBillByBillTx",
                                    new string[] { "flag", "BillByBillTx_ID", "BillByBillTx_RefType", "BillByBillTx_Ref", "BillByBillTx_Date", "Office_ID", "BillByBillTx_FY", "BillByBillTx_OrderNo", "SchemeTx_ID", "BillByBillTx_OrderDate", "BillByBillTx_ConsigneeName", "BillByBillTx_ItemGroup" },
                                    new string[] { "10",ViewState["BillByBillTx_ID"].ToString(), ddlRefType.SelectedValue.ToString(), txtBillByBillTx_Ref.Text,Convert.ToDateTime(txtBillByBillTx_Date.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), FinancialYear.ToString(), txtBillByBillTx_OrderNo.Text, ddlSchemeTx_ID.SelectedValue.ToString(), BillByBillTx_OrderDate, txtBillByBillTx_ConsigneeName.Text, txtBillByBillTx_ItemGroup.Text }, "dataset");
                    //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillGrid();
                    decimal Amount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                    string Amt = Amount.ToString();
                    ViewState["Amount"] = Amount;
                    if (Amount == decimal.Parse(ViewState["Opening Balance"].ToString()))
                    {
                        txtBillByBillTx_Amount.Text = "";
                        btnAddBillByBill.Enabled = false;

                    }
                    ClearText();

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Name entered has been used by current ledger or other ledger of same office.');", true);
                    txtBillByBillTx_Ref.Text = "";
                }
                

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + Msg + "');", true);
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
    protected void ClearText()
    {
        try
        {
            txtBillByBillTx_Date.Text = "";
            txtBillByBillTx_Ref.Text = "";
            txtBillByBillTx_ConsigneeName.Text = "";
            ddlSchemeTx_ID.ClearSelection();
            txtBillByBillTx_OrderNo.Text = "";
            txtBillByBillTx_OrderDate.Text = "";
            txtBillByBillTx_ItemGroup.Text = "";
            btnAddBillByBill.Text = "Add";
            ViewState["BillByBillTx_ID"] = "0";
            txtBillByBillTx_Amount.Enabled = true;
            ddlBillByBillTx_crdr.Enabled = true;
            //txtBillByBillTx_Amount.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewBillByBillDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string ID = GridViewBillByBillDetail.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("SpFinLedgerMaster", new string[] { "flag", "BillByBillTx_ID" }, new string[] { "30", ID.ToString() }, "dataset");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    
    protected void btnAlterLedger_Click(object sender, EventArgs e)
    {
        try
        {
            string Ledger_ID = ViewState["Ledger_ID"].ToString();
            if (Session["Office_ID"].ToString() == "1")
            {
                Response.Redirect("LedgerMasterB.aspx?Ledger_ID=" + objdb.Encrypt(Ledger_ID) + "&Mode=" + objdb.Encrypt("Edit"));
            }
            else
            {
                
                Response.Redirect("LedgerMaster_Forotherofc.aspx?Ledger_ID=" + objdb.Encrypt(Ledger_ID) + "&Mode=" + objdb.Encrypt("Edit"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void GridViewBillByBillDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string BillByBillTx_ID = GridViewBillByBillDetail.SelectedDataKey.Value.ToString();
            ViewState["BillByBillTx_ID"] = BillByBillTx_ID.ToString();
            ds = objdb.ByProcedure("SpFinBillByBillTx", new string[] { "flag", "BillByBillTx_ID" }, new string[] { "9", BillByBillTx_ID.ToString() }, "dataset");
            if(ds!= null && ds.Tables[0].Rows.Count > 0)
            {
                txtBillByBillTx_ConsigneeName.Text = ds.Tables[0].Rows[0]["BillByBillTx_ConsigneeName"].ToString();
                txtBillByBillTx_OrderNo.Text = ds.Tables[0].Rows[0]["BillByBillTx_OrderNo"].ToString();
                txtBillByBillTx_ItemGroup.Text = ds.Tables[0].Rows[0]["BillByBillTx_ItemGroup"].ToString();
                txtBillByBillTx_OrderDate.Text = ds.Tables[0].Rows[0]["BillByBillTx_OrderDate"].ToString();
                txtBillByBillTx_Date.Text = ds.Tables[0].Rows[0]["BillByBillTx_Date"].ToString();
                txtBillByBillTx_Ref.Text = ds.Tables[0].Rows[0]["BillByBillTx_Ref"].ToString();
                txtBillByBillTx_Amount.Text = ds.Tables[0].Rows[0]["BillByBillTx_Amount"].ToString();
                ddlSchemeTx_ID.ClearSelection();
                ddlSchemeTx_ID.Items.FindByValue(ds.Tables[0].Rows[0]["SchemeTx_ID"].ToString()).Selected = true;
                ddlRefType.ClearSelection();
                ddlRefType.Items.FindByValue(ds.Tables[0].Rows[0]["BillByBillTx_RefType"].ToString()).Selected = true;
                ddlBillByBillTx_crdr.ClearSelection();
                ddlBillByBillTx_crdr.Items.FindByValue(ds.Tables[0].Rows[0]["Type"].ToString()).Selected = true;
                
            }
            btnAddBillByBill.Text = "Update";
            btnAddBillByBill.Enabled = true;
            txtBillByBillTx_Amount.Enabled = false; 
            ddlBillByBillTx_crdr.Enabled = false;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}