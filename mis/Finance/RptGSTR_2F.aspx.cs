using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using ClosedXML.Excel;
using System.IO;


public partial class mis_Finance_RptGSTR_2F : System.Web.UI.Page
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
                    ViewState["OfficeType_Title"] = Session["OfficeType_Title"].ToString();
                    ViewState["Division_ID"] = Session["Division_ID"].ToString();
                    ViewState["RateType"] = "";
                    btnBack.Visible = false;
                    btnBackNext.Visible = false;
                    btnBackDayBook.Visible = false;
                    DivTable.Visible = false;
                    FillDropdown();
                    FillVoucherDate();
                    // FillFromDate();
                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        ddlOffice.Enabled = false;
                    }
                    if (ViewState["OfficeType_Title"].ToString() != "Regional Office")
                    {
                        btnSearch_Click(sender, e);
                    }
                }
                lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt");
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
    protected void FillVoucherDate()
    {
        try
        {
            ds = null;
            //ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            //if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            //{
            //    txtToDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            //}
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "7", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtFromDate.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["EndDate"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillFromDate()
    {
        try
        {
            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd")));
            //String dy = datevalue.Day.ToString();
            int mn = datevalue.Month;
            int yy = datevalue.Year;
            if (mn < 4)
            {
                txtFromDate.Text = "01/04/" + (yy - 1).ToString();
            }
            else
            {
                txtFromDate.Text = "01/04/" + (yy).ToString();
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
            ddlOffice.Enabled = false;
            ddlRegionalOffice.Enabled = false;
            divRegionalOffice.Visible = false;
            if (ViewState["OfficeType_Title"].ToString() != "Division Office (DFO)" && ViewState["OfficeType_Title"].ToString() != "Production Unit")
            {
                ddlOffice.Enabled = true;
                ddlRegionalOffice.Enabled = true;
                divRegionalOffice.Visible = true;
                ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "21" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlRegionalOffice.DataSource = ds;
                    ddlRegionalOffice.DataTextField = "Office_Name";
                    ddlRegionalOffice.DataValueField = "Office_ID";
                    ddlRegionalOffice.DataBind();
                    ddlRegionalOffice.Items.Insert(0, new ListItem("All", "0"));
                   // ddlRegionalOffice.Items.Insert(ds.Tables[0].Rows.Count + 1, new ListItem("Production Unit", "010"));
                    if (ViewState["OfficeType_Title"].ToString() == "Regional Office")
                    {
                        ddlRegionalOffice.Enabled = false;
                        ddlRegionalOffice.SelectedValue = ViewState["Division_ID"].ToString();
                    }
                }
            }

            FillOffice();
            //if (ViewState["Office_ID"].ToString() == "1")
            //{
            //    ddlOffice.Enabled = true;
            //}
            //ds = objdb.ByProcedure("SpFinRptTrialBalanceNew",
            //      new string[] { "flag" },
            //      new string[] { "0" }, "dataset");
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlOffice.DataSource = ds;
            //    ddlOffice.DataTextField = "Office_Name";
            //    ddlOffice.DataValueField = "Office_ID";
            //    ddlOffice.DataBind();
            //    //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
            //    ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            //}
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

            //ddlOffice.Enabled = false;
            //ddlRegionalOffice.Enabled = false;

            ddlOffice.Items.Clear();
            if (ddlRegionalOffice.SelectedIndex > 0)
            {
                if (ddlRegionalOffice.SelectedValue.ToString() != "010" && ddlRegionalOffice.SelectedItem.ToString() != "Production Unit")
                {
                    ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag", "Office_ID" },
                           new string[] { "23", ddlRegionalOffice.SelectedValue.ToString() }, "dataset");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlOffice.DataSource = ds;
                        ddlOffice.DataTextField = "Office_Name";
                        ddlOffice.DataValueField = "Office_ID";
                        ddlOffice.DataBind();
                        //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                        // ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                    }
                }
                else
                {
                    ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag" },
                           new string[] { "24" }, "dataset");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlOffice.DataSource = ds;
                        ddlOffice.DataTextField = "Office_Name";
                        ddlOffice.DataValueField = "Office_ID";
                        ddlOffice.DataBind();
                        //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                        // ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                    }

                }
            }
            else
            {
                ds = objdb.ByProcedure("SpAdminOffice",
                        new string[] { "flag" },
                        new string[] { "22" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOffice.DataSource = ds;
                    ddlOffice.DataTextField = "Office_Name";
                    ddlOffice.DataValueField = "Office_ID";
                    ddlOffice.DataBind();
                    //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                    ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void ClearText()
    {
        lblTotalVoucher.Text = "";
        lblIncludedRet.Text = "";

        lblNotIncludeRetInCompInfo.Text = "";
        lblNotReleventRet.Text = "";
        lblIncompleteHSNSAC.Text = "";
        //// END TOP SECTION 
        // **********1*********
        // START B2BR   

        lblB2BRVCount.Text = "";
        lblB2BVTaxableValue.Text = "";
        lblB2BVTIntegratedTax.Text = "";
        lblB2BVTCentarlTax.Text = "";
        lblB2BVTStateTax.Text = "";
        lblB2BVTCessAmt.Text = "";
        lblB2BVIIntegratedTax.Text = "";
        lblB2BVICentarlTax.Text = "";
        lblB2BVIStateTax.Text = "";
        lblB2BVICessAmt.Text = "";
        lblB2BVReconciliationStatus.Text = "";
        // END B2BR

        // **********2*********
        // Credit/Debit Notes Regular - 6C 
        lblDebitCount.Text = "";
        lblDebitTaxableValue.Text = "";
        lblDebitTIntegratedTax.Text = "";
        lblDebitTCentarlTax.Text = "";
        lblDebitTStateTax.Text = "";
        lblDebitTCessAmt.Text = "";
        lblDebitIIntegratedTax.Text = "";
        lblDebitICentarlTax.Text = "";
        lblDebitIStateTax.Text = "";
        lblDebitICessAmt.Text = "";
        lblDebitReconciliationStatus.Text = "";
        // END Credit/Debit Notes Regular - 6C

        // **********3*********
        // START B2BUR Invoices - 4B

        lblB2BURURVCount.Text = "";
        lblB2BURVTaxableValue.Text = "";
        lblB2BURVTIntegratedTax.Text = "";
        lblB2BURVTCentarlTax.Text = "";
        lblB2BURVTStateTax.Text = "";
        lblB2BURVTCessAmt.Text = "";
        lblB2BURVIIntegratedTax.Text = "";
        lblB2BURVICentarlTax.Text = "";
        lblB2BURVIStateTax.Text = "";
        lblB2BURVICessAmt.Text = "";
        lblB2BURVReconciliationStatus.Text = "";
        // END B2BUR Invoices - 4B
        // **********4*********
        // Credit/Debit Notes Unregistered - 6C
        lblDebitUCount.Text = "";
        lblDebitUTaxableValue.Text = "";
        lblDebitUTIntegratedTax.Text = "";
        lblDebitUTCentarlTax.Text = "";
        lblDebitUTStateTax.Text = "";
        lblDebitUTCessAmt.Text = "";
        lblDebitUIIntegratedTax.Text = "";
        lblDebitUICentarlTax.Text = "";
        lblDebitUIStateTax.Text = "";
        lblDebitUICessAmt.Text = "";
        lblDebitUReconciliationStatus.Text = "";
        // END Credit/Debit Notes Unregistered - 6C

        // **********5*********
        // START Nill Rated

        lblVNCount.Text = "";
        lblVNTaxableValue.Text = "";
        lblVNTIntegratedTax.Text = "";
        lblVNTCentarlTax.Text = "";
        lblVNTStateTax.Text = "";
        lblVNTCessAmt.Text = "";
        lblVNIIntegratedTax.Text = "";
        lblVNICentarlTax.Text = "";
        lblVNIStateTax.Text = "";
        lblVNICessAmt.Text = "";
        lblVNReconciliationStatus.Text = "";

        // END  Nill Rated

        // START TOTAL 
        lblTotalMVCount.Text = "";
        lblTotalMTaxableValue.Text = "";
        lblTotalMIntegratedTax.Text = "";
        lblTotalMCentralTax.Text = "";
        lblTotalMStateTax.Text = "";
        lblTotalMCess.Text = "";



        lblTotalInIntegratedTax.Text = "";
        lblTotalInCentralTax.Text = "";
        lblTotalInStateTax.Text = "";
        lblTotalInCess.Text = "";

        // **********6 & 7*********
        // START Nill Rated

        lblB2B_RevRMltVCount.Text = "";
        lblB2B_RevTaxableValue.Text = "";
        lblB2B_RevVTIntegratedTax.Text = "";
        lblB2B_RevVTCentarlTax.Text = "";
        lblB2B_RevVTStateTax.Text = "";
        lblB2B_RevVTCessAmt.Text = "";
        lblB2B_RevVIIntegratedTax.Text = "";
        lblB2B_RevVICentarlTax.Text = "";
        lblB2B_RevVIStateTax.Text = "";
        lblB2B_RevVICessAmt.Text = "";
        lblB2B_RevVReconciliationStatus.Text = "";

        // Taxable Purchase//                   
        lblB2B_TaxPurTaxableValue.Text = "";
        lblB2B_TaxPurVTIntegratedTax.Text = "";
        lblB2B_TaxPurVTCentarlTax.Text = "";
        lblB2B_TaxPurVTStateTax.Text = "";
        lblB2B_TaxPurVTCessAmt.Text = "";
        lblB2B_TaxPurVIIntegratedTax.Text = "";
        lblB2B_TaxPurVICentarlTax.Text = "";
        lblB2B_TaxPurVIStateTax.Text = "";
        lblB2B_TaxPurVICessAmt.Text = "";




        lblB2BUR_RevRMltVCount.Text = "";
        lblB2BUR_RevTaxableValue.Text = "";
        lblB2BUR_RevVTIntegratedTax.Text = "";
        lblB2BUR_RevVTCentarlTax.Text = "";
        lblB2BUR_RevVTStateTax.Text = "";
        lblB2BUR_RevVTCessAmt.Text = "";
        lblB2BUR_RevVIIntegratedTax.Text = "";
        lblB2BUR_RevVICentarlTax.Text = "";
        lblB2BUR_RevVIStateTax.Text = "";
        lblB2BUR_RevVICessAmt.Text = "";
        lblB2BUR_RevVReconciliationStatus.Text = "";


        // Taxable Purchase//                   
        lblB2BUR_TaxPurTaxableValue.Text = "";
        lblB2BUR_TaxPurVTIntegratedTax.Text = "";
        lblB2BUR_TaxPurVTCentarlTax.Text = "";
        lblB2BUR_TaxPurVTStateTax.Text = "";
        lblB2BUR_TaxPurVTCessAmt.Text = "";
        lblB2BUR_TaxPurVIIntegratedTax.Text = "";
        lblB2BUR_TaxPurVICentarlTax.Text = "";
        lblB2BUR_TaxPurVIStateTax.Text = "";
        lblB2BUR_TaxPurVICessAmt.Text = "";


        lblRCM.Text = "";
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblDateRange.Text = "";

            ClearText();
            //decimal TotalMVCount = 0;
            //decimal TotalMTaxableValue = 0;
            //decimal TotalMIntegratedTax = 0;
            //decimal TotalMCentralTax = 0;
            //decimal TotalMStateTax = 0;
            //decimal TotalMCess = 0;
            //decimal TotalMTaxAmount = 0;

            lblTTaxableValue.Text = "";
            lblSGST.Text = "";
            lblCGST.Text = "";
            lblIGST.Text = "";

            btnBack.Visible = false;
            btnBackNext.Visible = false;

            DivTable.Visible = true;
            lblParticulars.Text = "";
            lblParticularsRate.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridRateWise.DataSource = null;
            GridRateWise.DataBind();

		
            GridHSNSummery.DataSource = null;
            GridHSNSummery.DataBind();
	    GridHSNSummery_ItemWise.DataSource = null;
            GridHSNSummery_ItemWise.DataBind();
            GridHSNSummeryDes.DataSource = null;
            GridHSNSummeryDes.DataBind();

            GV_Statsticts.DataSource = null;
            GV_Statsticts.DataBind();

            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {

                    Office += item.Value + ",";
                }
            }
            if (txtFromDate.Text != "" && txtToDate.Text != "" && Office != "")
            {
                lblDateRange.Text = txtFromDate.Text + " to " + txtToDate.Text;
                ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF", new string[] { "flag", "Office_ID_Mlt", "FromDate", "ToDate" }, new string[] { "1", Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    // START TOP SECTION
                    DivTable.Visible = true;
                    lblTotalVoucher.Text = ds.Tables[0].Rows[0]["TotalVoucher"].ToString();
                    lblIncludedRet.Text = ds.Tables[0].Rows[0]["IncludedRet"].ToString();

                    //lblReadyToReturn.Text = ds.Tables[0].Rows[0]["ReadyToReturn"].ToString();
                    //lblMismatchInfo.Text = ds.Tables[0].Rows[0]["MismatchInfo"].ToString();
                    lblNotIncludeRetInCompInfo.Text = ds.Tables[0].Rows[0]["NotIncludeRetInCompInfo"].ToString();
                    lblNotReleventRet.Text = ds.Tables[0].Rows[0]["NotReleventRet"].ToString();
                    lblIncompleteHSNSAC.Text = ds.Tables[0].Rows[0]["IncompleteHSNSAC"].ToString();
                    ViewState["MltIncompleteHSNSAC"] = ds.Tables[0].Rows[0]["MltIncompleteHSNSAC"].ToString();

                    ViewState["MTotalVoucher"] = ds.Tables[0].Rows[0]["MTotalVoucher"].ToString();
                    ViewState["MIncludedRet"] = ds.Tables[0].Rows[0]["MIncludedRet"].ToString();
                    ViewState["MNotReleventRet"] = ds.Tables[0].Rows[0]["MNotReleventRet"].ToString();
                    ViewState["IncludeStatAdjustment"] = ds.Tables[0].Rows[0]["IncludeStatAdjustment"].ToString();

                    //// END TOP SECTION 
                    // **********1*********
                    // START B2BR   

                    lblB2BRVCount.Text = ds.Tables[1].Rows[0]["VCount"].ToString();
                    lblB2BVTaxableValue.Text = ds.Tables[1].Rows[0]["VTaxableValue"].ToString();
                    lblB2BVTIntegratedTax.Text = ds.Tables[1].Rows[0]["VTIntegratedTax"].ToString();
                    lblB2BVTCentarlTax.Text = ds.Tables[1].Rows[0]["VTCentarlTax"].ToString();
                    lblB2BVTStateTax.Text = ds.Tables[1].Rows[0]["VTStateTax"].ToString();
                    lblB2BVTCessAmt.Text = ds.Tables[1].Rows[0]["VTCessAmt"].ToString();
                    lblB2BVIIntegratedTax.Text = ds.Tables[1].Rows[0]["VIIntegratedTax"].ToString();
                    lblB2BVICentarlTax.Text = ds.Tables[1].Rows[0]["VICentarlTax"].ToString();
                    lblB2BVIStateTax.Text = ds.Tables[1].Rows[0]["VIStateTax"].ToString();
                    lblB2BVICessAmt.Text = ds.Tables[1].Rows[0]["VICessAmt"].ToString();
                    lblB2BVReconciliationStatus.Text = ds.Tables[1].Rows[0]["VReconciliationStatus"].ToString();
                    ViewState["B2BMltVCount"] = ds.Tables[1].Rows[0]["MltVCount"].ToString();
                    // END B2BR

                    // **********2*********
                    // Credit/Debit Notes Regular - 6C 
                    lblDebitCount.Text = ds.Tables[2].Rows[0]["VCount"].ToString();
                    lblDebitTaxableValue.Text = ds.Tables[2].Rows[0]["VTaxableValue"].ToString();
                    lblDebitTIntegratedTax.Text = ds.Tables[2].Rows[0]["VTIntegratedTax"].ToString();
                    lblDebitTCentarlTax.Text = ds.Tables[2].Rows[0]["VTCentarlTax"].ToString();
                    lblDebitTStateTax.Text = ds.Tables[2].Rows[0]["VTStateTax"].ToString();
                    lblDebitTCessAmt.Text = ds.Tables[2].Rows[0]["VTCessAmt"].ToString();
                    lblDebitIIntegratedTax.Text = ds.Tables[2].Rows[0]["VIIntegratedTax"].ToString();
                    lblDebitICentarlTax.Text = ds.Tables[2].Rows[0]["VICentarlTax"].ToString();
                    lblDebitIStateTax.Text = ds.Tables[2].Rows[0]["VIStateTax"].ToString();
                    lblDebitICessAmt.Text = ds.Tables[2].Rows[0]["VICessAmt"].ToString();
                    lblDebitReconciliationStatus.Text = ds.Tables[2].Rows[0]["VReconciliationStatus"].ToString();
                    ViewState["DebitMltVCount"] = ds.Tables[2].Rows[0]["MltVCount"].ToString();
                    // END Credit/Debit Notes Regular - 6C

                    // **********3*********
                    // START B2BUR Invoices - 4B

                    lblB2BURURVCount.Text = ds.Tables[3].Rows[0]["VCount"].ToString();
                    lblB2BURVTaxableValue.Text = ds.Tables[3].Rows[0]["VTaxableValue"].ToString();
                    lblB2BURVTIntegratedTax.Text = ds.Tables[3].Rows[0]["VTIntegratedTax"].ToString();
                    lblB2BURVTCentarlTax.Text = ds.Tables[3].Rows[0]["VTCentarlTax"].ToString();
                    lblB2BURVTStateTax.Text = ds.Tables[3].Rows[0]["VTStateTax"].ToString();
                    lblB2BURVTCessAmt.Text = ds.Tables[3].Rows[0]["VTCessAmt"].ToString();
                    lblB2BURVIIntegratedTax.Text = ds.Tables[3].Rows[0]["VIIntegratedTax"].ToString();
                    lblB2BURVICentarlTax.Text = ds.Tables[3].Rows[0]["VICentarlTax"].ToString();
                    lblB2BURVIStateTax.Text = ds.Tables[3].Rows[0]["VIStateTax"].ToString();
                    lblB2BURVICessAmt.Text = ds.Tables[3].Rows[0]["VICessAmt"].ToString();
                    lblB2BURVReconciliationStatus.Text = ds.Tables[3].Rows[0]["VReconciliationStatus"].ToString();
                    ViewState["B2BURMltVCount"] = ds.Tables[3].Rows[0]["MltVCount"].ToString();
                    // END B2BUR Invoices - 4B
                    // **********4*********
                    // Credit/Debit Notes Unregistered - 6C
                    lblDebitUCount.Text = ds.Tables[4].Rows[0]["VCount"].ToString();
                    lblDebitUTaxableValue.Text = ds.Tables[4].Rows[0]["VTaxableValue"].ToString();
                    lblDebitUTIntegratedTax.Text = ds.Tables[4].Rows[0]["VTIntegratedTax"].ToString();
                    lblDebitUTCentarlTax.Text = ds.Tables[4].Rows[0]["VTCentarlTax"].ToString();
                    lblDebitUTStateTax.Text = ds.Tables[4].Rows[0]["VTStateTax"].ToString();
                    lblDebitUTCessAmt.Text = ds.Tables[4].Rows[0]["VTCessAmt"].ToString();
                    lblDebitUIIntegratedTax.Text = ds.Tables[4].Rows[0]["VIIntegratedTax"].ToString();
                    lblDebitUICentarlTax.Text = ds.Tables[4].Rows[0]["VICentarlTax"].ToString();
                    lblDebitUIStateTax.Text = ds.Tables[4].Rows[0]["VIStateTax"].ToString();
                    lblDebitUICessAmt.Text = ds.Tables[4].Rows[0]["VICessAmt"].ToString();
                    lblDebitUReconciliationStatus.Text = ds.Tables[4].Rows[0]["VReconciliationStatus"].ToString();
                    ViewState["DebitMltUCount"] = ds.Tables[4].Rows[0]["MltVCount"].ToString();
                    // END Credit/Debit Notes Unregistered - 6C

                    // **********5*********
                    // START Nill Rated

                    lblVNCount.Text = ds.Tables[5].Rows[0]["VCount"].ToString();
                    lblVNTaxableValue.Text = ds.Tables[5].Rows[0]["VTaxableValue"].ToString();
                    lblVNTIntegratedTax.Text = ds.Tables[5].Rows[0]["VTIntegratedTax"].ToString();
                    lblVNTCentarlTax.Text = ds.Tables[5].Rows[0]["VTCentarlTax"].ToString();
                    lblVNTStateTax.Text = ds.Tables[5].Rows[0]["VTStateTax"].ToString();
                    lblVNTCessAmt.Text = ds.Tables[5].Rows[0]["VTCessAmt"].ToString();
                    lblVNIIntegratedTax.Text = ds.Tables[5].Rows[0]["VIIntegratedTax"].ToString();
                    lblVNICentarlTax.Text = ds.Tables[5].Rows[0]["VICentarlTax"].ToString();
                    lblVNIStateTax.Text = ds.Tables[5].Rows[0]["VIStateTax"].ToString();
                    lblVNICessAmt.Text = ds.Tables[5].Rows[0]["VICessAmt"].ToString();
                    lblVNReconciliationStatus.Text = ds.Tables[5].Rows[0]["VReconciliationStatus"].ToString();
                    ViewState["MNltVCount"] = ds.Tables[5].Rows[0]["MltVCount"].ToString();

                    // END  Nill Rated

                    // START TOTAL 
                    lblTotalMVCount.Text = (int.Parse(lblB2BRVCount.Text) + int.Parse(lblDebitCount.Text) + int.Parse(lblB2BURURVCount.Text) + int.Parse(lblDebitUCount.Text) + int.Parse(lblVNCount.Text)).ToString();
                    lblTotalMTaxableValue.Text = (decimal.Parse(lblB2BVTaxableValue.Text) + decimal.Parse(lblDebitTaxableValue.Text) + decimal.Parse(lblB2BURVTaxableValue.Text) + decimal.Parse(lblDebitUTaxableValue.Text) + decimal.Parse(lblVNTaxableValue.Text)).ToString();
                    lblTotalMIntegratedTax.Text = (decimal.Parse(lblB2BVTIntegratedTax.Text) + decimal.Parse(lblDebitTIntegratedTax.Text) + decimal.Parse(lblB2BURVTIntegratedTax.Text) + decimal.Parse(lblDebitUTIntegratedTax.Text) + decimal.Parse(lblVNTIntegratedTax.Text)).ToString();
                    lblTotalMCentralTax.Text = (decimal.Parse(lblB2BVTCentarlTax.Text) + decimal.Parse(lblDebitTCentarlTax.Text) + decimal.Parse(lblB2BURVTCentarlTax.Text) + decimal.Parse(lblDebitUTCentarlTax.Text) + decimal.Parse(lblVNTCentarlTax.Text)).ToString();
                    lblTotalMStateTax.Text = (decimal.Parse(lblB2BVTStateTax.Text) + decimal.Parse(lblDebitTStateTax.Text) + decimal.Parse(lblB2BURVTStateTax.Text) + decimal.Parse(lblDebitUTStateTax.Text) + decimal.Parse(lblVNTStateTax.Text)).ToString();
                    lblTotalMCess.Text = (decimal.Parse(lblB2BVTCessAmt.Text) + decimal.Parse(lblDebitTCessAmt.Text) + decimal.Parse(lblB2BURVTCessAmt.Text) + decimal.Parse(lblDebitUTCessAmt.Text) + decimal.Parse(lblVNTCessAmt.Text)).ToString();


                    lblTotalInIntegratedTax.Text = (decimal.Parse(lblB2BVIIntegratedTax.Text) + decimal.Parse(lblDebitIIntegratedTax.Text) + decimal.Parse(lblB2BURVIIntegratedTax.Text) + decimal.Parse(lblDebitUIIntegratedTax.Text) + decimal.Parse(lblVNIIntegratedTax.Text)).ToString();
                    lblTotalInCentralTax.Text = (decimal.Parse(lblB2BVICentarlTax.Text) + decimal.Parse(lblDebitICentarlTax.Text) + decimal.Parse(lblB2BURVICentarlTax.Text) + decimal.Parse(lblDebitUICentarlTax.Text) + decimal.Parse(lblVNICentarlTax.Text)).ToString();
                    lblTotalInStateTax.Text = (decimal.Parse(lblB2BVIStateTax.Text) + decimal.Parse(lblDebitIStateTax.Text) + decimal.Parse(lblB2BURVIStateTax.Text) + decimal.Parse(lblDebitUIStateTax.Text) + decimal.Parse(lblVNIStateTax.Text)).ToString();
                    lblTotalInCess.Text = (decimal.Parse(lblB2BVICessAmt.Text) + decimal.Parse(lblDebitICessAmt.Text) + decimal.Parse(lblB2BURVICessAmt.Text) + decimal.Parse(lblDebitUICessAmt.Text) + decimal.Parse(lblVNICessAmt.Text)).ToString();


                    //lblTotalMTaxAmount.Text = "0";
                    //lblTotalMInVoiceAmount.Text = "0";


                    // AS Per Trail Balance 
                    lblTTaxableValue.Text = (decimal.Parse(lblB2BVTaxableValue.Text) + decimal.Parse(lblDebitTaxableValue.Text) + decimal.Parse(lblB2BURVTaxableValue.Text) + decimal.Parse(lblDebitUTaxableValue.Text) + decimal.Parse(lblVNTaxableValue.Text)).ToString();
                    lblIGST.Text = (decimal.Parse(lblB2BVTIntegratedTax.Text) + decimal.Parse(lblDebitTIntegratedTax.Text) + decimal.Parse(lblB2BURVTIntegratedTax.Text) + decimal.Parse(lblDebitUTIntegratedTax.Text) + decimal.Parse(lblVNTIntegratedTax.Text)).ToString();
                    lblCGST.Text = (decimal.Parse(lblB2BVTCentarlTax.Text) + decimal.Parse(lblDebitTCentarlTax.Text) + decimal.Parse(lblB2BURVTCentarlTax.Text) + decimal.Parse(lblDebitUTCentarlTax.Text) + decimal.Parse(lblVNTCentarlTax.Text)).ToString();
                    lblSGST.Text = (decimal.Parse(lblB2BVTStateTax.Text) + decimal.Parse(lblDebitTStateTax.Text) + decimal.Parse(lblB2BURVTStateTax.Text) + decimal.Parse(lblDebitUTStateTax.Text) + decimal.Parse(lblVNTStateTax.Text)).ToString();

                    //END TOTAL

                    // Multiple Voucher ID
                    //ViewState["MltTotalVoucher"] = ds.Tables[8].Rows[0]["MltTotalVoucher"].ToString();
                    //ViewState["MltIncludedRet"] = ds.Tables[8].Rows[0]["MltIncludedRet"].ToString();
                    //ViewState["MltReadyToReturn"] = ds.Tables[8].Rows[0]["MltReadyToReturn"].ToString();
                    //ViewState["MltMismatch"] = ds.Tables[8].Rows[0]["MltMismatch"].ToString();
                    //ViewState["MltIncompleteHSNSAC"] = ds.Tables[8].Rows[0]["MltIncompleteHSNSAC"].ToString();
                    //ViewState["MltIncludedHSNSAC"] = ds.Tables[8].Rows[0]["MltIncludedHSNSAC"].ToString();



                    //ViewState["MltB2BVoucher"] = ds.Tables[8].Rows[0]["MltB2BVoucher"].ToString();
                    //ViewState["MltB2CLVoucher"] = ds.Tables[8].Rows[0]["MltB2CLVoucher"].ToString();
                    //ViewState["MltB2CMVoucher"] = ds.Tables[8].Rows[0]["MltB2CMVoucher"].ToString();
                    //ViewState["MltB2BCrCrRegVoucher"] = ds.Tables[8].Rows[0]["MltB2BCrCrRegVoucher"].ToString();
                    //ViewState["MltB2BCrCrUnRegVoucher"] = ds.Tables[8].Rows[0]["MltB2BCrCrUnRegVoucher"].ToString();
                    //ViewState["MltNilRatedVoucher"] = ds.Tables[8].Rows[0]["MltNilRatedVoucher"].ToString();


                    // **********6 & 7*********
                    // START Nill Rated

                    lblB2B_RevRMltVCount.Text = ds.Tables[6].Rows[0]["VCount"].ToString();
                    lblB2B_RevTaxableValue.Text = ds.Tables[6].Rows[0]["VTaxableValue"].ToString();
                    lblB2B_RevVTIntegratedTax.Text = ds.Tables[6].Rows[0]["VTIntegratedTax"].ToString();
                    lblB2B_RevVTCentarlTax.Text = ds.Tables[6].Rows[0]["VTCentarlTax"].ToString();
                    lblB2B_RevVTStateTax.Text = ds.Tables[6].Rows[0]["VTStateTax"].ToString();
                    lblB2B_RevVTCessAmt.Text = ds.Tables[6].Rows[0]["VTCessTax"].ToString();
                    lblB2B_RevVIIntegratedTax.Text = ds.Tables[6].Rows[0]["InIntegratedTax"].ToString();
                    lblB2B_RevVICentarlTax.Text = ds.Tables[6].Rows[0]["InCentarlTax"].ToString();
                    lblB2B_RevVIStateTax.Text = ds.Tables[6].Rows[0]["InStateTax"].ToString();
                    lblB2B_RevVICessAmt.Text = ds.Tables[6].Rows[0]["InCessTax"].ToString();
                    lblB2B_RevVReconciliationStatus.Text = ""; //ds.Tables[6].Rows[0]["VReconciliationStatus"].ToString();
                    ViewState["B2B_RevMltVCount"] = ds.Tables[6].Rows[0]["MltVCount"].ToString();

                    // Taxable Purchase//                   
                    lblB2B_TaxPurTaxableValue.Text = (decimal.Parse(lblB2BVTaxableValue.Text) - decimal.Parse(lblB2B_RevTaxableValue.Text)).ToString();
                    lblB2B_TaxPurVTIntegratedTax.Text = (decimal.Parse(lblB2BVTIntegratedTax.Text) - decimal.Parse(lblB2B_RevVTIntegratedTax.Text)).ToString();
                    lblB2B_TaxPurVTCentarlTax.Text = (decimal.Parse(lblB2BVTCentarlTax.Text) - decimal.Parse(lblB2B_RevVTCentarlTax.Text)).ToString();
                    lblB2B_TaxPurVTStateTax.Text = (decimal.Parse(lblB2BVTStateTax.Text) - decimal.Parse(lblB2B_RevVTStateTax.Text)).ToString();
                    lblB2B_TaxPurVTCessAmt.Text = (decimal.Parse(lblB2BVTCessAmt.Text) - decimal.Parse(lblB2B_RevVTCessAmt.Text)).ToString();
                    lblB2B_TaxPurVIIntegratedTax.Text = (decimal.Parse(lblB2BVIIntegratedTax.Text) - decimal.Parse(lblB2B_RevVIIntegratedTax.Text)).ToString();
                    lblB2B_TaxPurVICentarlTax.Text = (decimal.Parse(lblB2BVICentarlTax.Text) - decimal.Parse(lblB2B_RevVICentarlTax.Text)).ToString();
                    lblB2B_TaxPurVIStateTax.Text = (decimal.Parse(lblB2BVIStateTax.Text) - decimal.Parse(lblB2B_RevVIStateTax.Text)).ToString();
                    lblB2B_TaxPurVICessAmt.Text = (decimal.Parse(lblB2BVICessAmt.Text) - decimal.Parse(lblB2B_RevVICessAmt.Text)).ToString();




                    lblB2BUR_RevRMltVCount.Text = ds.Tables[7].Rows[0]["VCount"].ToString();
                    lblB2BUR_RevTaxableValue.Text = ds.Tables[7].Rows[0]["VTaxableValue"].ToString();
                    lblB2BUR_RevVTIntegratedTax.Text = ds.Tables[7].Rows[0]["VTIntegratedTax"].ToString();
                    lblB2BUR_RevVTCentarlTax.Text = ds.Tables[7].Rows[0]["VTCentarlTax"].ToString();
                    lblB2BUR_RevVTStateTax.Text = ds.Tables[7].Rows[0]["VTStateTax"].ToString();
                    lblB2BUR_RevVTCessAmt.Text = ds.Tables[7].Rows[0]["VTCessTax"].ToString();
                    lblB2BUR_RevVIIntegratedTax.Text = ds.Tables[7].Rows[0]["InIntegratedTax"].ToString();
                    lblB2BUR_RevVICentarlTax.Text = ds.Tables[7].Rows[0]["InCentarlTax"].ToString();
                    lblB2BUR_RevVIStateTax.Text = ds.Tables[7].Rows[0]["InStateTax"].ToString();
                    lblB2BUR_RevVICessAmt.Text = ds.Tables[7].Rows[0]["InCessTax"].ToString();
                    lblB2BUR_RevVReconciliationStatus.Text = ""; //ds.Tables[6].Rows[0]["VReconciliationStatus"].ToString();
                    ViewState["B2BUR_RevMltVCount"] = ds.Tables[7].Rows[0]["MltVCount"].ToString();


                    // Taxable Purchase//                   
                    lblB2BUR_TaxPurTaxableValue.Text = (decimal.Parse(lblB2BURVTaxableValue.Text) - decimal.Parse(lblB2BUR_RevTaxableValue.Text)).ToString();
                    lblB2BUR_TaxPurVTIntegratedTax.Text = (decimal.Parse(lblB2BURVTIntegratedTax.Text) - decimal.Parse(lblB2BUR_RevVTIntegratedTax.Text)).ToString();
                    lblB2BUR_TaxPurVTCentarlTax.Text = (decimal.Parse(lblB2BURVTCentarlTax.Text) - decimal.Parse(lblB2BUR_RevVTCentarlTax.Text)).ToString();
                    lblB2BUR_TaxPurVTStateTax.Text = (decimal.Parse(lblB2BURVTStateTax.Text) - decimal.Parse(lblB2BUR_RevVTStateTax.Text)).ToString();
                    lblB2BUR_TaxPurVTCessAmt.Text = (decimal.Parse(lblB2BURVTCessAmt.Text) - decimal.Parse(lblB2BUR_RevVTCessAmt.Text)).ToString();
                    lblB2BUR_TaxPurVIIntegratedTax.Text = (decimal.Parse(lblB2BURVIIntegratedTax.Text) - decimal.Parse(lblB2BUR_RevVIIntegratedTax.Text)).ToString();
                    lblB2BUR_TaxPurVICentarlTax.Text = (decimal.Parse(lblB2BURVICentarlTax.Text) - decimal.Parse(lblB2BUR_RevVICentarlTax.Text)).ToString();
                    lblB2BUR_TaxPurVIStateTax.Text = (decimal.Parse(lblB2BURVIStateTax.Text) - decimal.Parse(lblB2BUR_RevVIStateTax.Text)).ToString();
                    lblB2BUR_TaxPurVICessAmt.Text = (decimal.Parse(lblB2BURVICessAmt.Text) - decimal.Parse(lblB2BUR_RevVICessAmt.Text)).ToString();

                    if (ds.Tables[7].Rows.Count > 0)
                    {
                        StringBuilder SumrytaxLiability = new StringBuilder();
                        decimal TotalLiabilityTaxAmt = 0;
                        decimal LiabilityBookedTaxAmt = 0;
                        decimal BalLiabilityTaxAmt = 0;
                        SumrytaxLiability.Append("<table class='table table-hover no-border cssborder' >");
                        SumrytaxLiability.Append("<tr>");
                        SumrytaxLiability.Append("<th class='cssborder' colspan='2' style='text-align:center'>Tax Details</th>");
                        SumrytaxLiability.Append("<th class='cssborder' colspan='2' style='text-align:center'>Total Liability</th>");
                        SumrytaxLiability.Append("<th class='cssborder' colspan='2' style='text-align:center'>Liability/ITC Booked</th>");
                        SumrytaxLiability.Append("<th class='cssborder' colspan='2' style='text-align:center'>Balance Liability</th>");
                        SumrytaxLiability.Append("</tr>");
                        SumrytaxLiability.Append("<tr>");
                        SumrytaxLiability.Append("<th class='cssborder'>Tax Rate</th>");
                        SumrytaxLiability.Append("<th class='cssborder'>Tax Type</th>");
                        SumrytaxLiability.Append("<th class='cssborder'>Taxable  Value</th>");
                        SumrytaxLiability.Append("<th class='cssborder'>Tax Amount</th>");
                        SumrytaxLiability.Append("<th class='cssborder'>Taxable  Value</th>");
                        SumrytaxLiability.Append("<th class='cssborder'>Tax Amount</th>");
                        SumrytaxLiability.Append("<th class='cssborder'>Taxable  Value</th>");
                        SumrytaxLiability.Append("<th class='cssborder'>Tax Amount</th>");
                        SumrytaxLiability.Append("</tr>");


                        int count = ds.Tables[8].Rows.Count;
                        decimal TotalRCM = 0;
                        for (int i = 0; i < count; i++)
                        {
                            SumrytaxLiability.Append("<tr>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[8].Rows[i]["CGSTPer"] + "</td>");
                            SumrytaxLiability.Append("<td class='rightborder'>Central Tax</td>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[8].Rows[i]["TaxableValue"] + "</td>");
                            SumrytaxLiability.Append("<td class='rightborder'>" + ds.Tables[8].Rows[i]["CGSTAmt"] + "</td>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[8].Rows[i]["RCMTaxableValue"] + "</td>");
                            SumrytaxLiability.Append("<td class='rightborder'>" + ds.Tables[8].Rows[i]["RCMCGSTAmt"] + "</td>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[8].Rows[i]["BalTaxableValue"] + "</td>");
                            SumrytaxLiability.Append("<td >" + ds.Tables[8].Rows[i]["BalCGSTAmt"] + "</td>");
                            SumrytaxLiability.Append("</tr>");
                            TotalRCM += decimal.Parse(ds.Tables[8].Rows[i]["BalCGSTAmt"].ToString());
                            TotalLiabilityTaxAmt += decimal.Parse(ds.Tables[8].Rows[i]["CGSTAmt"].ToString());
                            LiabilityBookedTaxAmt += decimal.Parse(ds.Tables[8].Rows[i]["RCMCGSTAmt"].ToString());
                            BalLiabilityTaxAmt += decimal.Parse(ds.Tables[8].Rows[i]["BalSGSTAmt"].ToString());

                        }
                        for (int i = 0; i < count; i++)
                        {
                            SumrytaxLiability.Append("<tr>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[8].Rows[i]["SGSTPer"] + "</td>");
                            SumrytaxLiability.Append("<td class='rightborder'>State Tax</td>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[8].Rows[i]["TaxableValue"] + "</td>");
                            SumrytaxLiability.Append("<td class='rightborder'>" + ds.Tables[8].Rows[i]["SGSTAmt"] + "</td>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[8].Rows[i]["RCMTaxableValue"] + "</td>");
                            SumrytaxLiability.Append("<td class='rightborder'>" + ds.Tables[8].Rows[i]["RCMSGSTAmt"] + "</td>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[8].Rows[i]["BalTaxableValue"] + "</td>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[8].Rows[i]["BalSGSTAmt"] + "</td>");
                            SumrytaxLiability.Append("</tr>");
                            TotalRCM += decimal.Parse(ds.Tables[8].Rows[i]["BalSGSTAmt"].ToString());
                            TotalLiabilityTaxAmt += decimal.Parse(ds.Tables[8].Rows[i]["CGSTAmt"].ToString());
                            LiabilityBookedTaxAmt += decimal.Parse(ds.Tables[8].Rows[i]["RCMCGSTAmt"].ToString());
                            BalLiabilityTaxAmt += decimal.Parse(ds.Tables[8].Rows[i]["BalSGSTAmt"].ToString());

                        }
                        SumrytaxLiability.Append("<tr>");
                        SumrytaxLiability.Append("<td class='cssborder'></td>");
                        SumrytaxLiability.Append("<td class='cssborder'>Total</td>");
                        SumrytaxLiability.Append("<td class='cssborder'></td>");
                        SumrytaxLiability.Append("<td class='cssborder'>" + TotalLiabilityTaxAmt.ToString() + "</td>");
                        SumrytaxLiability.Append("<td class='cssborder'></td>");
                        SumrytaxLiability.Append("<td class='cssborder'>" + LiabilityBookedTaxAmt.ToString() + "</td>");
                        SumrytaxLiability.Append("<td class='cssborder'></td>");
                        SumrytaxLiability.Append("<td class='cssborder'>" + BalLiabilityTaxAmt.ToString() + "</td>");
                        SumrytaxLiability.Append("</tr>");
                        SumrytaxLiability.Append("</table>");
                        divSumrytaxLiability.InnerHtml = SumrytaxLiability.ToString();
                        pnlSumrytaxLiability.Visible = false;
                        lblRCM.Text = TotalRCM.ToString();




                    }




                    Get_HSN_Summary();

                }

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
            lblMsg.Text = "";
            lblDateRange.Text = "";
            //  ClearText();
            decimal TotalMVCount = 0;
            decimal TotalMTaxableValue = 0;
            decimal TotalMIntegratedTax = 0;
            decimal TotalMCentralTax = 0;
            decimal TotalMStateTax = 0;
            decimal TotalMCess = 0;
            decimal TotalMTaxAmount = 0;
            btnBack.Visible = false;
            DivTable.Visible = false;

            lblParticulars.Text = "";
            GridRateWise.DataSource = null;
            GridRateWise.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblParticulars.Text = "";
            lblParticularsRate.Text = "";
            DivTable.Visible = true;
            btnBack.Visible = false;
            btnBackNext.Visible = false;

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridRateWise.DataSource = null;
            GridRateWise.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
	     GridHSNSummeryDes.DataSource = null;
            GridHSNSummeryDes.DataBind();
            GridHSNSummery.DataSource = null;
            GridHSNSummery.DataBind();
	    GridHSNSummery_ItemWise.DataSource = null;
            GridHSNSummery_ItemWise.DataBind();
            GridView1.Visible = false;
            GridRateWise.Visible = false;

            GV_Statsticts.DataSource = null;
            GV_Statsticts.DataBind();
            pnlSumrytaxLiability.Visible = false;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnBackNext_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            //lblParticulars.Text = "";
            lblParticularsRate.Text = "";
            DivTable.Visible = false;
            btnBack.Visible = true;
            btnBackNext.Visible = false;

            GridView1.Visible = true;
            GridRateWise.Visible = true;

            GridView1.DataSource = null;
            GridView1.DataBind();

            GridView2.DataSource = null;
            GridView2.DataBind();

            GridHSNSummeryDes.DataSource = null;
            GridHSNSummeryDes.DataBind();

            GridView1.Visible = false;
            GridHSNSummery.Visible = true;
	    GridHSNSummery_ItemWise.Visible = true;
            btnBackDayBook.Visible = false;
            //  pnlSumrytaxLiability.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnTotalVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Total number of vouchers for the period " + txtFromDate.Text + " to " + txtToDate.Text;
            FILL_Statstics(ViewState["MTotalVoucher"].ToString(), "");
            // FILLGRID(ViewState["MTotalVoucher"].ToString(), "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnTotalVoucherIncludedRet_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Summary Of Included Vouchers for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            FILL_Statstics(ViewState["MIncludedRet"].ToString(), "");

            //  FILLGRID(ViewState["MIncludedRet"].ToString(), "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    protected void FILL_Statstics(string VoucherList, string MltVoucherTx_IDNotin)
    {
        try
        {
            btnBack.Visible = true;

            GV_Statsticts.DataSource = null;
            GV_Statsticts.DataBind();
            ViewState["VoucherList"] = VoucherList;
            ViewState["NotVoucherList"] = MltVoucherTx_IDNotin;
            ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF", new string[] { "flag", "MIncludedRet", "MltVoucherTx_IDNotin" }, new string[] { "11", VoucherList, MltVoucherTx_IDNotin }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                GV_Statsticts.DataSource = ds.Tables[0];
                GV_Statsticts.DataBind();

                GV_Statsticts.FooterRow.Cells[1].Text = "<b>Total</b>";
                GV_Statsticts.FooterRow.Cells[2].Text = "<b>" + ds.Tables[1].Rows[0]["VCount"].ToString() + "</b>";

                GV_Statsticts.Visible = true;
                DivTable.Visible = false;
                btnBackNext.Visible = false;
                btnBackDayBook.Visible = false;
                GridRateWise.Visible = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    protected void btnVoucherNotRelevent_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Voucher Not relevant for returns for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            FILL_Statstics(ViewState["MNotReleventRet"].ToString(), ViewState["IncludeStatAdjustment"].ToString());
            // FILLGRID(ViewState["MNotReleventRet"].ToString(), "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnReadyToReturn_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Invoices ready for returns for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            // FILLGRID("6");
            FILLGRID(ViewState["MltReadyToReturn"].ToString(), "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnMismatchInfo_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Invoices with mismatch in information for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            // FILLGRID("6");
            FILLGRID(ViewState["MltMismatch"].ToString(), "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnIncompleteHSNSAC_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Incomplete HSN/SAC information (to be provided) for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            // FILLGRID("6");
            FILLGRID(ViewState["MltIncompleteHSNSAC"].ToString(), "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnB2BRMltVCount_Click(object sender, EventArgs e)
    {

        try
        {
            lblParticulars.Text = "B2B Invoices - 3, 4A for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
           // FILLRateWiseVoucher(ViewState["B2BMltVCount"].ToString(), "","");


            string MltVoucherTx_ID = ViewState["B2BMltVCount"].ToString();
            string RateType = "B2B";
            string MltVoucherTx_IDNotin = "";


            lblMsg.Text = "";
            lblParticularsRate.Text = "";
            //lblDateRange.Text = "";           
            GridView1.Visible = true;
            GridRateWise.Visible = true;

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridRateWise.DataSource = null;
            GridRateWise.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();

            ViewState["MltVoucherTx_DayBook"] = MltVoucherTx_ID.ToString();
            ViewState["MltVoucherTx_DayBookNotin"] = MltVoucherTx_IDNotin.ToString();
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                ViewState["RateType"] = RateType;
                //lblDateRange.Text = txtFromDate.Text + " to " + txtToDate.Text;
                ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "RateType" }, new string[] { "16", MltVoucherTx_ID, MltVoucherTx_IDNotin, RateType }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    btnBack.Visible = false;
                    DivTable.Visible = false;
                    btnBackNext.Visible = true;

                    GridView1.Visible = false;

                    GridRateWise.DataSource = ds.Tables[0];
                    GridRateWise.DataBind();

                    GridRateWise.FooterRow.Cells[1].Text = "<b>Total</b>";
                    GridRateWise.FooterRow.Cells[4].Text = "<b>" + ds.Tables[1].Rows[0]["No_of_Invoices"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[5].Text = "<b>" + ds.Tables[1].Rows[0]["TaxableValue"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[6].Text = "<b>" + ds.Tables[1].Rows[0]["IGSTAmt"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[7].Text = "<b>" + ds.Tables[1].Rows[0]["CGSTAmt"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[8].Text = "<b>" + ds.Tables[1].Rows[0]["SGSTAmt"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[9].Text = "<b>" + ds.Tables[1].Rows[0]["CessAmount"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[10].Text = "<b>" + ds.Tables[1].Rows[0]["TotalTaxAmt"].ToString() + "</b>";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    protected void btnB2BURMltVCount_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "B2BUR Invoices - 4B for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            FILLRateWiseVoucher(ViewState["B2BURMltVCount"].ToString(), "","");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnDebitUMltVCount_Click(object sender, EventArgs e)
    {
        lblParticulars.Text = "Credit/Debit Notes Unregistered - 6C for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
        FILLRateWiseVoucher(ViewState["DebitMltUCount"].ToString(), "","");
    }
    protected void btnDebitMltVCount_Click(object sender, EventArgs e)
    {
        lblParticulars.Text = "Voucher Credit/Debit Notes Regular - 6C for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
        FILLRateWiseVoucher(ViewState["DebitMltVCount"].ToString(), "","");
    }
    protected void btnVNMltVCount_Click(object sender, EventArgs e)
    {
        lblParticulars.Text = "Nil Rated Invoices - 7 - (Summary) for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
        FILLRateWiseVoucher(ViewState["MNltVCount"].ToString(), "","Nill");
    }
    protected void Get_HSN_Summary()
    {
        try
        {
            lblHSNTaxableValue.Text = "";
            lblHSNTIntegratedTax.Text = "";
            lblHSNTCentarlTax.Text = "";
            lblHSNTStateTax.Text = "";
            lblHSNTCessAmt.Text = "";
	    lblHSNTaxableValue_ItemWise.Text = "";
            lblHSNTIntegratedTax_ItemWise.Text = "";
            lblHSNTCentarlTax_ItemWise.Text = "";
            lblHSNTStateTax_ItemWise.Text = "";
            lblHSNTCessAmt_ItemWise.Text = "";


            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "13", ViewState["MIncludedRet"].ToString(), "" }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    lblHSNTaxableValue.Text = ds.Tables[0].Rows[0]["TaxableValue"].ToString();
                    lblHSNTIntegratedTax.Text = ds.Tables[0].Rows[0]["IntegratedTaxAmount"].ToString();
                    lblHSNTCentarlTax.Text = ds.Tables[0].Rows[0]["CentralTaxAmount"].ToString();
                    lblHSNTStateTax.Text = ds.Tables[0].Rows[0]["StateUTTaxAmount"].ToString();
                    lblHSNTCessAmt.Text = ds.Tables[0].Rows[0]["CessAmt"].ToString();

		    lblHSNTaxableValue_ItemWise.Text = ds.Tables[0].Rows[0]["TaxableValue"].ToString();
                    lblHSNTIntegratedTax_ItemWise.Text = ds.Tables[0].Rows[0]["IntegratedTaxAmount"].ToString();
                    lblHSNTCentarlTax_ItemWise.Text = ds.Tables[0].Rows[0]["CentralTaxAmount"].ToString();
                    lblHSNTStateTax_ItemWise.Text = ds.Tables[0].Rows[0]["StateUTTaxAmount"].ToString();
                    lblHSNTCessAmt_ItemWise.Text = ds.Tables[0].Rows[0]["CessAmt"].ToString();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnHSNSummary_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridRateWise.DataSource = null;
            GridRateWise.DataBind();
            GridHSNSummery.Visible = true;
            GridHSNSummery.DataSource = null;
            GridHSNSummery.DataBind();

            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "9", ViewState["MIncludedRet"].ToString(), "" }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    lblParticulars.Text = "HSN/SAC Summary - 13 for the period  " + txtFromDate.Text + " to " + txtToDate.Text;

                    btnBack.Visible = true;
                    DivTable.Visible = false;
                    btnBackNext.Visible = false;

                    GridHSNSummery.DataSource = ds;
                    GridHSNSummery.DataBind();

                    GridHSNSummery.FooterRow.Cells[3].Text = "Total";
                    GridHSNSummery.FooterRow.Cells[3].Font.Bold = true;



                    GridHSNSummery.FooterRow.Cells[5].Text = ds.Tables[1].Rows[0]["TotalQuantity"].ToString();
                    GridHSNSummery.FooterRow.Cells[6].Text = ds.Tables[1].Rows[0]["TotalValue"].ToString();
                    GridHSNSummery.FooterRow.Cells[7].Text = ds.Tables[1].Rows[0]["TaxableValue"].ToString();
                    GridHSNSummery.FooterRow.Cells[8].Text = ds.Tables[1].Rows[0]["IntegratedTaxAmount"].ToString();
                    GridHSNSummery.FooterRow.Cells[9].Text = ds.Tables[1].Rows[0]["CentralTaxAmount"].ToString();
                    GridHSNSummery.FooterRow.Cells[10].Text = ds.Tables[1].Rows[0]["StateUTTaxAmount"].ToString();
                    GridHSNSummery.FooterRow.Cells[11].Text = ds.Tables[1].Rows[0]["TotalTaxAmt"].ToString();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void GridRateWise_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblParticularsRate.Text = "";
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridRateWise.Rows[index];
                Label lblRateOfTax = (Label)row.FindControl("lblRateOfTax");
                Label lblParticulars1 = (Label)row.FindControl("lblParticulars1");
                Label lblTotalLedger = (Label)row.FindControl("lblTotalLedger");

                Label lblTotalVoucherO = (Label)row.FindControl("lblTotalVoucherO");

                lblParticularsRate.Text = lblParticulars1.Text;// +"[ Rate " + lblRateOfTax.Text + " % ]";

                FILLDayBook(lblTotalVoucherO.Text, "", lblRateOfTax.Text, lblTotalLedger.Text, lblParticulars1.Text);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void GV_Statsticts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblParticularsRate.Text = "";
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GV_Statsticts.Rows[index];
                Label lblParticulars1 = (Label)row.FindControl("lblParticulars1");

                lblParticularsRate.Text = lblParticulars1.Text;
                btnBack.Visible = false;
                btnBackDayBook.Visible = true;

                FILLStatsticsDayBook(ViewState["VoucherList"].ToString(), lblParticulars1.Text, ViewState["NotVoucherList"].ToString());
                //FILLDayBook(ViewState["MltVoucherTx_DayBook"].ToString(), ViewState["MltVoucherTx_DayBookNotin"].ToString(), lblRateOfTax.Text, lblTotalLedger.Text, lblParticulars1.Text);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FILLStatsticsDayBook(string MltVoucherTx_ID, string VoucherTx_Type, string MltVoucherTx_IDNotin)
    {
        try
        {
            lblMsg.Text = "";
            GridView1.Visible = true;
            btnBack.Visible = false;

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();

            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                string sDate = (Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd")).ToString();
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

                // lblDateRange.Text = txtFromDate.Text + " to " + txtToDate.Text;
                ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF",
                    new string[] { "flag", "MltVoucherTx_ID", "VoucherTx_Type", "MltVoucherTx_IDNotin", "FinancialYear", "Office_ID" },
                    new string[] { "12", MltVoucherTx_ID, VoucherTx_Type, MltVoucherTx_IDNotin, FinancialYear, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    btnBackDayBook.Visible = true;
                    DivTable.Visible = false;
                    btnBackNext.Visible = false;
                    GridRateWise.Visible = false;

                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                    GridView1.FooterRow.Cells[1].Text = "<b>Total</b>";
                    GridView1.FooterRow.Cells[4].Text = "<b>" + ds.Tables[1].Rows[0]["DebitAmt"].ToString() + "</b>";
                    GridView1.FooterRow.Cells[5].Text = "<b>" + ds.Tables[1].Rows[0]["CreditAmt"].ToString() + "</b>";


                    GV_Statsticts.Visible = false;
                }
                else
                {
                    lblParticularsRate.Text = "";
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    protected void GridHSNSummery_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblParticularsRate.Text = "";
            GridHSNSummeryDes.DataSource = null;
            GridHSNSummeryDes.DataBind();
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridHSNSummery.Rows[index];
                Label lblHSN = (Label)row.Cells[2].FindControl("lblHSN");
                Label lblDescription = (Label)row.Cells[2].FindControl("lblDescription");
                Label lblTypeOfSupply = (Label)row.Cells[2].FindControl("lblTypeOfSupply");
                Label lblUQC = (Label)row.Cells[2].FindControl("lblUQC");


                lblParticularsRate.Text = "Voucher Of HSN/SAC :  " + lblHSN.Text;


                string sDate = (Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd")).ToString();
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


                ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "HSN", "Description", "TypeOfSupply", "UQC", "FinancialYear", "Office_ID" },
                    new string[] { "10", ViewState["MIncludedRet"].ToString(), "", lblHSN.Text, lblDescription.Text, lblTypeOfSupply.Text, lblUQC.Text, FinancialYear, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    btnBack.Visible = false;
                    DivTable.Visible = false;
                    btnBackNext.Visible = true;

                    GridHSNSummery.Visible = false;

                    GridHSNSummeryDes.DataSource = ds;
                    GridHSNSummeryDes.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FILLGRID(string MltVoucherTx_ID, string MltVoucherTx_IDNotin)
    {
        try
        {
            lblMsg.Text = "";
            lblDateRange.Text = "";
            lblParticularsRate.Text = "";


            GridRateWise.Visible = true;

            GridRateWise.DataSource = null;
            GridRateWise.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                lblDateRange.Text = txtFromDate.Text + " to " + txtToDate.Text;
                ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "2", MltVoucherTx_ID, MltVoucherTx_IDNotin }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {

                    btnBack.Visible = true;
                    DivTable.Visible = false;
                    btnBackNext.Visible = false;
                    GridRateWise.Visible = false;

                    GridView1.DataSource = ds;
                }
                else
                {
                    lblParticulars.Text = "";
                }
            }
            GridView1.DataBind();
            GridView1.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    // Rate Wise Voucher Details
    protected void FILLRateWiseVoucher(string MltVoucherTx_ID, string MltVoucherTx_IDNotin, string RateType)
    {
        try
        {
            lblMsg.Text = "";
            lblParticularsRate.Text = "";
            //lblDateRange.Text = "";           
            GridView1.Visible = true;
            GridRateWise.Visible = true;

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridRateWise.DataSource = null;
            GridRateWise.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();

            ViewState["MltVoucherTx_DayBook"] = MltVoucherTx_ID.ToString();
            ViewState["MltVoucherTx_DayBookNotin"] = MltVoucherTx_IDNotin.ToString();
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                ViewState["RateType"] = RateType;
                //lblDateRange.Text = txtFromDate.Text + " to " + txtToDate.Text;
                
                ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "RateType" }, new string[] { "6", MltVoucherTx_ID, MltVoucherTx_IDNotin, RateType }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    btnBack.Visible = false;
                    DivTable.Visible = false;
                    btnBackNext.Visible = true;

                    GridView1.Visible = false;

                    GridRateWise.DataSource = ds.Tables[0];
                    GridRateWise.DataBind();

                    GridRateWise.FooterRow.Cells[1].Text = "<b>Total</b>";
                    GridRateWise.FooterRow.Cells[4].Text = "<b>" + ds.Tables[1].Rows[0]["No_of_Invoices"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[5].Text = "<b>" + ds.Tables[1].Rows[0]["TaxableValue"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[6].Text = "<b>" + ds.Tables[1].Rows[0]["IGSTAmt"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[7].Text = "<b>" + ds.Tables[1].Rows[0]["CGSTAmt"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[8].Text = "<b>" + ds.Tables[1].Rows[0]["SGSTAmt"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[9].Text = "<b>" + ds.Tables[1].Rows[0]["CessAmount"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[10].Text = "<b>" + ds.Tables[1].Rows[0]["TotalTaxAmt"].ToString() + "</b>";
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    protected void FILLDayBook(string MltVoucherTx_ID, string MltVoucherTx_IDNotin, string GSTRate, string TotalLedger, string Particulars)
    {
        try
        {
            lblMsg.Text = "";

            GridView1.Visible = true;

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();

            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                string sDate = (Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd")).ToString();
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

                // lblDateRange.Text = txtFromDate.Text + " to " + txtToDate.Text;
                if (ViewState["RateType"].ToString() == "B2B")
                {
                    ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF",
                    new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "GSTRate", "TotalLedger", "Particulars", "FinancialYear", "Office_ID", "RateType" },
                    new string[] { "17", MltVoucherTx_ID, MltVoucherTx_IDNotin, GSTRate, TotalLedger, Particulars, FinancialYear, ViewState["Office_ID"].ToString(), ViewState["RateType"].ToString() }, "dataset");
                }
                else
                {
                    ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF",
                    new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "GSTRate", "TotalLedger", "Particulars", "FinancialYear", "Office_ID", "RateType" },
                    new string[] { "8", MltVoucherTx_ID, MltVoucherTx_IDNotin, GSTRate, TotalLedger, Particulars, FinancialYear, ViewState["Office_ID"].ToString(), ViewState["RateType"].ToString() }, "dataset");
                }
               
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    btnBack.Visible = false;
                    DivTable.Visible = false;
                    btnBackNext.Visible = true;

                    GridRateWise.Visible = false;

                    GridView2.DataSource = ds;
                    GridView2.DataBind();

                    GridView2.FooterRow.Cells[1].Text = "<b>Total</b>";
                    //GridView2.FooterRow.Cells[4].Text = "<b>" + ds.Tables[1].Rows[0]["TaxableValue"].ToString() + "</b>";
                    //GridView2.FooterRow.Cells[5].Text = "<b>" + ds.Tables[1].Rows[0]["IGSTAmt"].ToString() + "</b>";
                    //GridView2.FooterRow.Cells[6].Text = "<b>" + ds.Tables[1].Rows[0]["CGSTAmt"].ToString() + "</b>";
                    //GridView2.FooterRow.Cells[7].Text = "<b>" + ds.Tables[1].Rows[0]["SGSTAmt"].ToString() + "</b>";
                    //GridView2.FooterRow.Cells[8].Text = "<b>" + ds.Tables[1].Rows[0]["CessAmt"].ToString() + "</b>";
                    //GridView2.FooterRow.Cells[9].Text = "<b>" + ds.Tables[1].Rows[0]["TotalTaxAmt"].ToString() + "</b>";
                    //GridView2.FooterRow.Cells[10].Text = "<b>" + ds.Tables[1].Rows[0]["InvoiceValue"].ToString() + "</b>";
                    GridView2.FooterRow.Cells[5].Text = "<b>" + ds.Tables[1].Rows[0]["TaxableValue"].ToString() + "</b>";
                    GridView2.FooterRow.Cells[6].Text = "<b>" + ds.Tables[1].Rows[0]["IGSTAmt"].ToString() + "</b>";
                    GridView2.FooterRow.Cells[7].Text = "<b>" + ds.Tables[1].Rows[0]["CGSTAmt"].ToString() + "</b>";
                    GridView2.FooterRow.Cells[8].Text = "<b>" + ds.Tables[1].Rows[0]["SGSTAmt"].ToString() + "</b>";
                    GridView2.FooterRow.Cells[9].Text = "<b>" + ds.Tables[1].Rows[0]["CessAmt"].ToString() + "</b>";
                    GridView2.FooterRow.Cells[10].Text = "<b>" + ds.Tables[1].Rows[0]["TotalTaxAmt"].ToString() + "</b>";
                    GridView2.FooterRow.Cells[11].Text = "<b>" + ds.Tables[1].Rows[0]["InvoiceValue"].ToString() + "</b>";
                }
                else
                {
                    lblParticularsRate.Text = "";
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }


    protected void btnB2BR_RevMltVCoun_Click(object sender, EventArgs e)
    {
        try
        {
            pnlSumrytaxLiability.Visible = false;
            lblParticulars.Text = "B2B Invoices - 3, 4A Reverse Charges Supplies  " + txtFromDate.Text + " to " + txtToDate.Text;
            FILLRateWiseVoucher(ViewState["B2B_RevMltVCount"].ToString(), "","");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnB2BUR_RevMltVCount_Click(object sender, EventArgs e)
    {
        try
        {
            pnlSumrytaxLiability.Visible = false;
            lblParticulars.Text = "B2B UR Invoices - 4B Reverse Charges Supplies " + txtFromDate.Text + " to " + txtToDate.Text;
            FILLRateWiseVoucher(ViewState["B2BUR_RevMltVCount"].ToString(), "","");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnB2BR_TaxPurMltVCount_Click(object sender, EventArgs e)
    {
        try
        {
            pnlSumrytaxLiability.Visible = false;
            lblParticulars.Text = "B2B Invoices - 3, 4A Taxable Purchases  " + txtFromDate.Text + " to " + txtToDate.Text;
           // FILLRateWiseVoucher(ViewState["B2BMltVCount"].ToString(), ViewState["B2B_RevMltVCount"].ToString(),"B2B");


            lblMsg.Text = "";
            lblParticularsRate.Text = "";
            //lblDateRange.Text = "";           
            GridView1.Visible = true;
            GridRateWise.Visible = true;

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridRateWise.DataSource = null;
            GridRateWise.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();


            string MltVoucherTx_ID = ViewState["B2BMltVCount"].ToString();

            string MltVoucherTx_IDNotin = ViewState["B2B_RevMltVCount"].ToString();
            string RateType = "B2B";

            ViewState["MltVoucherTx_DayBook"] = MltVoucherTx_ID.ToString();
            ViewState["MltVoucherTx_DayBookNotin"] = MltVoucherTx_IDNotin.ToString();
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                ViewState["RateType"] = RateType;
                //lblDateRange.Text = txtFromDate.Text + " to " + txtToDate.Text;

                ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "RateType" }, new string[] { "16", MltVoucherTx_ID, MltVoucherTx_IDNotin, RateType }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    btnBack.Visible = false;
                    DivTable.Visible = false;
                    btnBackNext.Visible = true;

                    GridView1.Visible = false;

                    GridRateWise.DataSource = ds.Tables[0];
                    GridRateWise.DataBind();

                    GridRateWise.FooterRow.Cells[1].Text = "<b>Total</b>";
                    GridRateWise.FooterRow.Cells[4].Text = "<b>" + ds.Tables[1].Rows[0]["No_of_Invoices"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[5].Text = "<b>" + ds.Tables[1].Rows[0]["TaxableValue"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[6].Text = "<b>" + ds.Tables[1].Rows[0]["IGSTAmt"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[7].Text = "<b>" + ds.Tables[1].Rows[0]["CGSTAmt"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[8].Text = "<b>" + ds.Tables[1].Rows[0]["SGSTAmt"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[9].Text = "<b>" + ds.Tables[1].Rows[0]["CessAmount"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[10].Text = "<b>" + ds.Tables[1].Rows[0]["TotalTaxAmt"].ToString() + "</b>";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnB2BUR_TaxPurMltVCount_Click(object sender, EventArgs e)
    {
        try
        {
            pnlSumrytaxLiability.Visible = false;
            lblParticulars.Text = "B2BUR Invoices - 4B Reverse Charges Supplies  " + txtFromDate.Text + " to " + txtToDate.Text;
            FILLRateWiseVoucher(ViewState["B2BURMltVCount"].ToString(), ViewState["B2BUR_RevMltVCount"].ToString(),"");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnRCM_Click(object sender, EventArgs e)
    {
        try
        {
            string Voucherlist = ViewState["B2B_RevMltVCount"].ToString() + "," + ViewState["B2BUR_RevMltVCount"].ToString();
            lblParticulars.Text = "List Of Reverse Charge Supplies  " + txtFromDate.Text + " to " + txtToDate.Text;
            FILLRateWiseVoucher(Voucherlist, "","");
            pnlSumrytaxLiability.Visible = true;



            btnBack.Visible = false;
            DivTable.Visible = false;
            btnBackNext.Visible = true;

            GridView1.Visible = false;


            //ViewState["B2BUR_RevMltVCount"].ToString()
            //ViewState["B2B_RevMltVCount"].ToString()


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    protected void btnBackDayBook_Click(object sender, EventArgs e)
    {
        try
        {
            GV_Statsticts.Visible = true;
            DivTable.Visible = false;
            btnBackNext.Visible = false;
            btnBackDayBook.Visible = false;
            GridRateWise.Visible = false;
            btnBack.Visible = true;
            GridView1.DataSource = null;
            GridView1.DataBind();

            lblParticularsRate.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnAllExcel_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            int sno = 0;
            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {

                    Office += item.Value + ",";
                }
            }
            // b2b
            #region B2B Export
            DataTable dt_b2b = new DataTable("1");
            DataColumn b2bRowNo = dt_b2b.Columns.Add("S. No.", typeof(int));
            dt_b2b.Columns.Add(new DataColumn("Supplier Name", typeof(string)));
            dt_b2b.Columns.Add(new DataColumn("Supplier GSTIN/UIN", typeof(string)));
            dt_b2b.Columns.Add(new DataColumn("Invoice No", typeof(string)));
            dt_b2b.Columns.Add(new DataColumn("Invoice Date", typeof(string)));
            dt_b2b.Columns.Add(new DataColumn("Total Invoice Value", typeof(double)));
            dt_b2b.Columns.Add(new DataColumn("Place of Supply", typeof(string)));
            dt_b2b.Columns.Add(new DataColumn("Status", typeof(string)));

            dt_b2b.Columns.Add(new DataColumn("Total Taxable Value", typeof(double)));
            dt_b2b.Columns.Add(new DataColumn("Integrated Tax Amount", typeof(double)));
            dt_b2b.Columns.Add(new DataColumn("ITC Available Integrated Tax", typeof(double)));
            dt_b2b.Columns.Add(new DataColumn("Central Tax Amount", typeof(double)));
            dt_b2b.Columns.Add(new DataColumn("ITC Available Central Tax", typeof(double)));
            dt_b2b.Columns.Add(new DataColumn("State Tax Amount", typeof(double)));
            dt_b2b.Columns.Add(new DataColumn("ITC Available State Tax", typeof(double)));
            dt_b2b.Columns.Add(new DataColumn("Cess Amount", typeof(double)));
            dt_b2b.Columns.Add(new DataColumn("ITC Available Cess", typeof(double)));
            dt_b2b.Columns.Add(new DataColumn("Voucher No", typeof(string)));
            dt_b2b.Columns.Add(new DataColumn("Voucher Date", typeof(string)));
            dt_b2b.TableName = "b2b";



            decimal Total_Invoice_Value = 0;
            decimal Total_Taxable_Value = 0;
            decimal Integrated_Tax_Amount = 0;
            decimal ITC_Available_Integrated_Tax = 0;
            decimal Central_Tax_Amount = 0;
            decimal ITC_Available_Central_Tax = 0;
            decimal State_Tax_Amount = 0;
            decimal ITC_Available_State_Tax = 0;
            decimal Cess_Amount = 0;
            decimal ITC_Available_Cess = 0;

            ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "14", ViewState["B2BMltVCount"].ToString(), "" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {
                    dt_b2b.Rows.Add((i + 1)
                        , ds.Tables[0].Rows[i]["Supplier Name"].ToString()
                        , ds.Tables[0].Rows[i]["Supplier GSTIN/UIN"].ToString()
                        , ds.Tables[0].Rows[i]["Invoice No"].ToString()
                        , ds.Tables[0].Rows[i]["Invoice Date"].ToString()
                        , ds.Tables[0].Rows[i]["Total Invoice Value"].ToString()
                        , ds.Tables[0].Rows[i]["Place of Supply"].ToString()
                        , ds.Tables[0].Rows[i]["Status"].ToString()
                        , ds.Tables[0].Rows[i]["Total Taxable Value"].ToString()
                        , ds.Tables[0].Rows[i]["Integrated Tax Amount"].ToString()
                        , ds.Tables[0].Rows[i]["ITC Available Integrated Tax"].ToString()
                        , ds.Tables[0].Rows[i]["Central Tax Amount"].ToString()
                        , ds.Tables[0].Rows[i]["ITC Available Central Tax"].ToString()
                        , ds.Tables[0].Rows[i]["State Tax Amount"].ToString()
                        , ds.Tables[0].Rows[i]["ITC Available State Tax"].ToString()
                        , ds.Tables[0].Rows[i]["Cess Amount"].ToString()
                        , ds.Tables[0].Rows[i]["ITC Available Cess"].ToString()
                         , ds.Tables[0].Rows[i]["Voucher No"].ToString()
                        , ds.Tables[0].Rows[i]["Voucher Date"].ToString()

                        );

                    Total_Invoice_Value = Total_Invoice_Value + decimal.Parse(ds.Tables[0].Rows[i]["Total Invoice Value"].ToString());
                    Total_Taxable_Value = Total_Taxable_Value + decimal.Parse(ds.Tables[0].Rows[i]["Total Taxable Value"].ToString());
                    Integrated_Tax_Amount = Integrated_Tax_Amount + decimal.Parse(ds.Tables[0].Rows[i]["Integrated Tax Amount"].ToString());
                    ITC_Available_Integrated_Tax = ITC_Available_Integrated_Tax + decimal.Parse(ds.Tables[0].Rows[i]["ITC Available Integrated Tax"].ToString());
                    Central_Tax_Amount = Central_Tax_Amount + decimal.Parse(ds.Tables[0].Rows[i]["Central Tax Amount"].ToString());
                    ITC_Available_Central_Tax = ITC_Available_Central_Tax + decimal.Parse(ds.Tables[0].Rows[i]["ITC Available Central Tax"].ToString());
                    State_Tax_Amount = State_Tax_Amount + decimal.Parse(ds.Tables[0].Rows[i]["State Tax Amount"].ToString());
                    ITC_Available_State_Tax = ITC_Available_State_Tax + decimal.Parse(ds.Tables[0].Rows[i]["ITC Available State Tax"].ToString());
                    Cess_Amount = Cess_Amount + decimal.Parse(ds.Tables[0].Rows[i]["Cess Amount"].ToString());
                    ITC_Available_Cess = ITC_Available_Cess + decimal.Parse(ds.Tables[0].Rows[i]["ITC Available Cess"].ToString());

                    //GrandInvoiceValue = GrandInvoiceValue + decimal.Parse(ds.Tables[0].Rows[i]["InvoiceValue"].ToString());
                    //GrandTaxableValue = GrandTaxableValue + decimal.Parse(ds.Tables[0].Rows[i]["TaxableValue"].ToString());
                }
                dt_b2b.Rows.Add(null
                        , "Grand Total"
                        , "-"
                        , "-"
                        , "-"
                        , Total_Invoice_Value.ToString()
                         , "-"
                        , "-"
                        , Total_Taxable_Value.ToString()
                        , Integrated_Tax_Amount.ToString()
                        , ITC_Available_Integrated_Tax.ToString()
                        , Central_Tax_Amount.ToString()
                        , ITC_Available_Central_Tax.ToString()
                        , State_Tax_Amount.ToString()
                        , ITC_Available_State_Tax.ToString()
                        , Cess_Amount.ToString()
                        , ITC_Available_Cess.ToString()
                        , "-"
                        , "-"
                        );
            }

            #endregion

            // b2b_UR


            #region b2b_UR Export
            DataTable dt_b2b_UR = new DataTable("1");
            DataColumn b2b_URRowNo = dt_b2b_UR.Columns.Add("S. No.", typeof(int));
            dt_b2b_UR.Columns.Add(new DataColumn("Supplier Name", typeof(string)));
            dt_b2b_UR.Columns.Add(new DataColumn("Supplier GSTIN/UIN", typeof(string)));
            dt_b2b_UR.Columns.Add(new DataColumn("Invoice No", typeof(string)));
            dt_b2b_UR.Columns.Add(new DataColumn("Invoice Date", typeof(string)));
            dt_b2b_UR.Columns.Add(new DataColumn("Total Invoice Value", typeof(double)));
            dt_b2b_UR.Columns.Add(new DataColumn("Place of Supply", typeof(string)));
            dt_b2b_UR.Columns.Add(new DataColumn("Status", typeof(string)));

            dt_b2b_UR.Columns.Add(new DataColumn("Total Taxable Value", typeof(double)));
            dt_b2b_UR.Columns.Add(new DataColumn("Integrated Tax Amount", typeof(double)));
            dt_b2b_UR.Columns.Add(new DataColumn("ITC Available Integrated Tax", typeof(double)));
            dt_b2b_UR.Columns.Add(new DataColumn("Central Tax Amount", typeof(double)));
            dt_b2b_UR.Columns.Add(new DataColumn("ITC Available Central Tax", typeof(double)));
            dt_b2b_UR.Columns.Add(new DataColumn("State Tax Amount", typeof(double)));
            dt_b2b_UR.Columns.Add(new DataColumn("ITC Available State Tax", typeof(double)));
            dt_b2b_UR.Columns.Add(new DataColumn("Cess Amount", typeof(double)));
            dt_b2b_UR.Columns.Add(new DataColumn("ITC Available Cess", typeof(double)));
            dt_b2b_UR.Columns.Add(new DataColumn("Voucher No", typeof(string)));
            dt_b2b_UR.Columns.Add(new DataColumn("Voucher Date", typeof(string)));
            dt_b2b_UR.TableName = "b2b_UR";



            Total_Invoice_Value = 0;
            Total_Taxable_Value = 0;
            Integrated_Tax_Amount = 0;
            ITC_Available_Integrated_Tax = 0;
            Central_Tax_Amount = 0;
            ITC_Available_Central_Tax = 0;
            State_Tax_Amount = 0;
            ITC_Available_State_Tax = 0;
            Cess_Amount = 0;
            ITC_Available_Cess = 0;

            ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "14", ViewState["B2BURMltVCount"].ToString(), "" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {
                    dt_b2b_UR.Rows.Add((i + 1)
                        , ds.Tables[0].Rows[i]["Supplier Name"].ToString()
                        , ds.Tables[0].Rows[i]["Supplier GSTIN/UIN"].ToString()
                        , ds.Tables[0].Rows[i]["Invoice No"].ToString()
                        , ds.Tables[0].Rows[i]["Invoice Date"].ToString()
                        , ds.Tables[0].Rows[i]["Total Invoice Value"].ToString()
                        , ds.Tables[0].Rows[i]["Place of Supply"].ToString()
                        , ds.Tables[0].Rows[i]["Status"].ToString()
                        , ds.Tables[0].Rows[i]["Total Taxable Value"].ToString()
                        , ds.Tables[0].Rows[i]["Integrated Tax Amount"].ToString()
                        , ds.Tables[0].Rows[i]["ITC Available Integrated Tax"].ToString()
                        , ds.Tables[0].Rows[i]["Central Tax Amount"].ToString()
                        , ds.Tables[0].Rows[i]["ITC Available Central Tax"].ToString()
                        , ds.Tables[0].Rows[i]["State Tax Amount"].ToString()
                        , ds.Tables[0].Rows[i]["ITC Available State Tax"].ToString()
                        , ds.Tables[0].Rows[i]["Cess Amount"].ToString()
                        , ds.Tables[0].Rows[i]["ITC Available Cess"].ToString()
                        , ds.Tables[0].Rows[i]["Voucher No"].ToString()
                        , ds.Tables[0].Rows[i]["Voucher Date"].ToString()
                        );

                    Total_Invoice_Value = Total_Invoice_Value + decimal.Parse(ds.Tables[0].Rows[i]["Total Invoice Value"].ToString());
                    Total_Taxable_Value = Total_Taxable_Value + decimal.Parse(ds.Tables[0].Rows[i]["Total Taxable Value"].ToString());
                    Integrated_Tax_Amount = Integrated_Tax_Amount + decimal.Parse(ds.Tables[0].Rows[i]["Integrated Tax Amount"].ToString());
                    ITC_Available_Integrated_Tax = ITC_Available_Integrated_Tax + decimal.Parse(ds.Tables[0].Rows[i]["ITC Available Integrated Tax"].ToString());
                    Central_Tax_Amount = Central_Tax_Amount + decimal.Parse(ds.Tables[0].Rows[i]["Central Tax Amount"].ToString());
                    ITC_Available_Central_Tax = ITC_Available_Central_Tax + decimal.Parse(ds.Tables[0].Rows[i]["ITC Available Central Tax"].ToString());
                    State_Tax_Amount = State_Tax_Amount + decimal.Parse(ds.Tables[0].Rows[i]["State Tax Amount"].ToString());
                    ITC_Available_State_Tax = ITC_Available_State_Tax + decimal.Parse(ds.Tables[0].Rows[i]["ITC Available State Tax"].ToString());
                    Cess_Amount = Cess_Amount + decimal.Parse(ds.Tables[0].Rows[i]["Cess Amount"].ToString());
                    ITC_Available_Cess = ITC_Available_Cess + decimal.Parse(ds.Tables[0].Rows[i]["ITC Available Cess"].ToString());

                    //GrandInvoiceValue = GrandInvoiceValue + decimal.Parse(ds.Tables[0].Rows[i]["InvoiceValue"].ToString());
                    //GrandTaxableValue = GrandTaxableValue + decimal.Parse(ds.Tables[0].Rows[i]["TaxableValue"].ToString());
                }
                dt_b2b_UR.Rows.Add(null
                        , "Grand Total"
                        , "-"
                        , "-"
                        , "-"
                        , Total_Invoice_Value.ToString()
                         , "-"
                        , "-"
                        , Total_Taxable_Value.ToString()
                        , Integrated_Tax_Amount.ToString()
                        , ITC_Available_Integrated_Tax.ToString()
                        , Central_Tax_Amount.ToString()
                        , ITC_Available_Central_Tax.ToString()
                        , State_Tax_Amount.ToString()
                        , ITC_Available_State_Tax.ToString()
                        , Cess_Amount.ToString()
                        , ITC_Available_Cess.ToString()
                        , "-"
                        , "-"
                        );
            }

            #endregion

            // b2b_Reverse Charges Supplies

            #region b2b_RCM (Reverse_Charges_Supplies) Export

            DataTable dt_b2b_RCM = new DataTable("1");
            DataColumn b2b_RCMRowNo = dt_b2b_RCM.Columns.Add("S. No.", typeof(int));
            dt_b2b_RCM.Columns.Add(new DataColumn("Supplier Name", typeof(string)));
            dt_b2b_RCM.Columns.Add(new DataColumn("Supplier GSTIN/UIN", typeof(string)));
            dt_b2b_RCM.Columns.Add(new DataColumn("Invoice No", typeof(string)));
            dt_b2b_RCM.Columns.Add(new DataColumn("Invoice Date", typeof(string)));
            dt_b2b_RCM.Columns.Add(new DataColumn("Total Invoice Value", typeof(double)));
            dt_b2b_RCM.Columns.Add(new DataColumn("Place of Supply", typeof(string)));
            dt_b2b_RCM.Columns.Add(new DataColumn("Status", typeof(string)));

            dt_b2b_RCM.Columns.Add(new DataColumn("Total Taxable Value", typeof(double)));
            dt_b2b_RCM.Columns.Add(new DataColumn("Integrated Tax Amount", typeof(double)));
            dt_b2b_RCM.Columns.Add(new DataColumn("ITC Available Integrated Tax", typeof(double)));
            dt_b2b_RCM.Columns.Add(new DataColumn("Central Tax Amount", typeof(double)));
            dt_b2b_RCM.Columns.Add(new DataColumn("ITC Available Central Tax", typeof(double)));
            dt_b2b_RCM.Columns.Add(new DataColumn("State Tax Amount", typeof(double)));
            dt_b2b_RCM.Columns.Add(new DataColumn("ITC Available State Tax", typeof(double)));
            dt_b2b_RCM.Columns.Add(new DataColumn("Cess Amount", typeof(double)));
            dt_b2b_RCM.Columns.Add(new DataColumn("ITC Available Cess", typeof(double)));

            dt_b2b_RCM.Columns.Add(new DataColumn("Voucher No", typeof(string)));
            dt_b2b_RCM.Columns.Add(new DataColumn("Voucher Date", typeof(string)));
            dt_b2b_RCM.TableName = "b2b_Reverse_Charges_Supplies";



            Total_Invoice_Value = 0;
            Total_Taxable_Value = 0;
            Integrated_Tax_Amount = 0;
            ITC_Available_Integrated_Tax = 0;
            Central_Tax_Amount = 0;
            ITC_Available_Central_Tax = 0;
            State_Tax_Amount = 0;
            ITC_Available_State_Tax = 0;
            Cess_Amount = 0;
            ITC_Available_Cess = 0;

            ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "14", ViewState["B2B_RevMltVCount"].ToString(), "" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {
                    dt_b2b_RCM.Rows.Add((i + 1)
                        , ds.Tables[0].Rows[i]["Supplier Name"].ToString()
                        , ds.Tables[0].Rows[i]["Supplier GSTIN/UIN"].ToString()
                        , ds.Tables[0].Rows[i]["Invoice No"].ToString()
                        , ds.Tables[0].Rows[i]["Invoice Date"].ToString()
                        , ds.Tables[0].Rows[i]["Total Invoice Value"].ToString()
                        , ds.Tables[0].Rows[i]["Place of Supply"].ToString()
                        , ds.Tables[0].Rows[i]["Status"].ToString()
                        , ds.Tables[0].Rows[i]["Total Taxable Value"].ToString()
                        , ds.Tables[0].Rows[i]["Integrated Tax Amount"].ToString()
                        , ds.Tables[0].Rows[i]["ITC Available Integrated Tax"].ToString()
                        , ds.Tables[0].Rows[i]["Central Tax Amount"].ToString()
                        , ds.Tables[0].Rows[i]["ITC Available Central Tax"].ToString()
                        , ds.Tables[0].Rows[i]["State Tax Amount"].ToString()
                        , ds.Tables[0].Rows[i]["ITC Available State Tax"].ToString()
                        , ds.Tables[0].Rows[i]["Cess Amount"].ToString()
                        , ds.Tables[0].Rows[i]["ITC Available Cess"].ToString()
                        , ds.Tables[0].Rows[i]["Voucher No"].ToString()
                        , ds.Tables[0].Rows[i]["Voucher Date"].ToString()
                        );

                    Total_Invoice_Value = Total_Invoice_Value + decimal.Parse(ds.Tables[0].Rows[i]["Total Invoice Value"].ToString());
                    Total_Taxable_Value = Total_Taxable_Value + decimal.Parse(ds.Tables[0].Rows[i]["Total Taxable Value"].ToString());
                    Integrated_Tax_Amount = Integrated_Tax_Amount + decimal.Parse(ds.Tables[0].Rows[i]["Integrated Tax Amount"].ToString());
                    ITC_Available_Integrated_Tax = ITC_Available_Integrated_Tax + decimal.Parse(ds.Tables[0].Rows[i]["ITC Available Integrated Tax"].ToString());
                    Central_Tax_Amount = Central_Tax_Amount + decimal.Parse(ds.Tables[0].Rows[i]["Central Tax Amount"].ToString());
                    ITC_Available_Central_Tax = ITC_Available_Central_Tax + decimal.Parse(ds.Tables[0].Rows[i]["ITC Available Central Tax"].ToString());
                    State_Tax_Amount = State_Tax_Amount + decimal.Parse(ds.Tables[0].Rows[i]["State Tax Amount"].ToString());
                    ITC_Available_State_Tax = ITC_Available_State_Tax + decimal.Parse(ds.Tables[0].Rows[i]["ITC Available State Tax"].ToString());
                    Cess_Amount = Cess_Amount + decimal.Parse(ds.Tables[0].Rows[i]["Cess Amount"].ToString());
                    ITC_Available_Cess = ITC_Available_Cess + decimal.Parse(ds.Tables[0].Rows[i]["ITC Available Cess"].ToString());

                    //GrandInvoiceValue = GrandInvoiceValue + decimal.Parse(ds.Tables[0].Rows[i]["InvoiceValue"].ToString());
                    //GrandTaxableValue = GrandTaxableValue + decimal.Parse(ds.Tables[0].Rows[i]["TaxableValue"].ToString());
                }
                dt_b2b_RCM.Rows.Add(null
                        , "Grand Total"
                        , "-"
                        , "-"
                        , "-"
                        , Total_Invoice_Value.ToString()
                         , "-"
                        , "-"
                        , Total_Taxable_Value.ToString()
                        , Integrated_Tax_Amount.ToString()
                        , ITC_Available_Integrated_Tax.ToString()
                        , Central_Tax_Amount.ToString()
                        , ITC_Available_Central_Tax.ToString()
                        , State_Tax_Amount.ToString()
                        , ITC_Available_State_Tax.ToString()
                        , Cess_Amount.ToString()
                        , ITC_Available_Cess.ToString()
                        , "-"
                        , "-"
                        );
            }

            #endregion

            // b2b_UR_Reverse Charges Supplies

            #region b2b_RCM (Reverse_Charges_Supplies) Export

            DataTable dt_b2b_UR_RCM = new DataTable("1");
            DataColumn b2b_UR_RCMRowNo = dt_b2b_UR_RCM.Columns.Add("S. No.", typeof(int));
            dt_b2b_UR_RCM.Columns.Add(new DataColumn("Supplier Name", typeof(string)));
            dt_b2b_UR_RCM.Columns.Add(new DataColumn("Supplier GSTIN/UIN", typeof(string)));
            dt_b2b_UR_RCM.Columns.Add(new DataColumn("Invoice No", typeof(string)));
            dt_b2b_UR_RCM.Columns.Add(new DataColumn("Invoice Date", typeof(string)));
            dt_b2b_UR_RCM.Columns.Add(new DataColumn("Total Invoice Value", typeof(double)));
            dt_b2b_UR_RCM.Columns.Add(new DataColumn("Place of Supply", typeof(string)));
            dt_b2b_UR_RCM.Columns.Add(new DataColumn("Status", typeof(string)));

            dt_b2b_UR_RCM.Columns.Add(new DataColumn("Total Taxable Value", typeof(double)));
            dt_b2b_UR_RCM.Columns.Add(new DataColumn("Integrated Tax Amount", typeof(double)));
            dt_b2b_UR_RCM.Columns.Add(new DataColumn("ITC Available Integrated Tax", typeof(double)));
            dt_b2b_UR_RCM.Columns.Add(new DataColumn("Central Tax Amount", typeof(double)));
            dt_b2b_UR_RCM.Columns.Add(new DataColumn("ITC Available Central Tax", typeof(double)));
            dt_b2b_UR_RCM.Columns.Add(new DataColumn("State Tax Amount", typeof(double)));
            dt_b2b_UR_RCM.Columns.Add(new DataColumn("ITC Available State Tax", typeof(double)));
            dt_b2b_UR_RCM.Columns.Add(new DataColumn("Cess Amount", typeof(double)));
            dt_b2b_UR_RCM.Columns.Add(new DataColumn("ITC Available Cess", typeof(double)));

            dt_b2b_UR_RCM.Columns.Add(new DataColumn("Voucher No", typeof(string)));
            dt_b2b_UR_RCM.Columns.Add(new DataColumn("Voucher Date", typeof(string)));
            dt_b2b_UR_RCM.TableName = "b2b_UR_Reverse_Charges_Supplies";



            Total_Invoice_Value = 0;
            Total_Taxable_Value = 0;
            Integrated_Tax_Amount = 0;
            ITC_Available_Integrated_Tax = 0;
            Central_Tax_Amount = 0;
            ITC_Available_Central_Tax = 0;
            State_Tax_Amount = 0;
            ITC_Available_State_Tax = 0;
            Cess_Amount = 0;
            ITC_Available_Cess = 0;

            ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "14", ViewState["B2BUR_RevMltVCount"].ToString(), "" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {
                    dt_b2b_UR_RCM.Rows.Add((i + 1)
                        , ds.Tables[0].Rows[i]["Supplier Name"].ToString()
                        , ds.Tables[0].Rows[i]["Supplier GSTIN/UIN"].ToString()
                        , ds.Tables[0].Rows[i]["Invoice No"].ToString()
                        , ds.Tables[0].Rows[i]["Invoice Date"].ToString()
                        , ds.Tables[0].Rows[i]["Total Invoice Value"].ToString()
                        , ds.Tables[0].Rows[i]["Place of Supply"].ToString()
                        , ds.Tables[0].Rows[i]["Status"].ToString()
                        , ds.Tables[0].Rows[i]["Total Taxable Value"].ToString()
                        , ds.Tables[0].Rows[i]["Integrated Tax Amount"].ToString()
                        , ds.Tables[0].Rows[i]["ITC Available Integrated Tax"].ToString()
                        , ds.Tables[0].Rows[i]["Central Tax Amount"].ToString()
                        , ds.Tables[0].Rows[i]["ITC Available Central Tax"].ToString()
                        , ds.Tables[0].Rows[i]["State Tax Amount"].ToString()
                        , ds.Tables[0].Rows[i]["ITC Available State Tax"].ToString()
                        , ds.Tables[0].Rows[i]["Cess Amount"].ToString()
                        , ds.Tables[0].Rows[i]["ITC Available Cess"].ToString()
                        , ds.Tables[0].Rows[i]["Voucher No"].ToString()
                        , ds.Tables[0].Rows[i]["Voucher Date"].ToString()
                        );

                    Total_Invoice_Value = Total_Invoice_Value + decimal.Parse(ds.Tables[0].Rows[i]["Total Invoice Value"].ToString());
                    Total_Taxable_Value = Total_Taxable_Value + decimal.Parse(ds.Tables[0].Rows[i]["Total Taxable Value"].ToString());
                    Integrated_Tax_Amount = Integrated_Tax_Amount + decimal.Parse(ds.Tables[0].Rows[i]["Integrated Tax Amount"].ToString());
                    ITC_Available_Integrated_Tax = ITC_Available_Integrated_Tax + decimal.Parse(ds.Tables[0].Rows[i]["ITC Available Integrated Tax"].ToString());
                    Central_Tax_Amount = Central_Tax_Amount + decimal.Parse(ds.Tables[0].Rows[i]["Central Tax Amount"].ToString());
                    ITC_Available_Central_Tax = ITC_Available_Central_Tax + decimal.Parse(ds.Tables[0].Rows[i]["ITC Available Central Tax"].ToString());
                    State_Tax_Amount = State_Tax_Amount + decimal.Parse(ds.Tables[0].Rows[i]["State Tax Amount"].ToString());
                    ITC_Available_State_Tax = ITC_Available_State_Tax + decimal.Parse(ds.Tables[0].Rows[i]["ITC Available State Tax"].ToString());
                    Cess_Amount = Cess_Amount + decimal.Parse(ds.Tables[0].Rows[i]["Cess Amount"].ToString());
                    ITC_Available_Cess = ITC_Available_Cess + decimal.Parse(ds.Tables[0].Rows[i]["ITC Available Cess"].ToString());

                    //GrandInvoiceValue = GrandInvoiceValue + decimal.Parse(ds.Tables[0].Rows[i]["InvoiceValue"].ToString());
                    //GrandTaxableValue = GrandTaxableValue + decimal.Parse(ds.Tables[0].Rows[i]["TaxableValue"].ToString());
                }
                dt_b2b_UR_RCM.Rows.Add(null
                        , "Grand Total"
                        , "-"
                        , "-"
                        , "-"
                        , Total_Invoice_Value.ToString()
                         , "-"
                        , "-"
                        , Total_Taxable_Value.ToString()
                        , Integrated_Tax_Amount.ToString()
                        , ITC_Available_Integrated_Tax.ToString()
                        , Central_Tax_Amount.ToString()
                        , ITC_Available_Central_Tax.ToString()
                        , State_Tax_Amount.ToString()
                        , ITC_Available_State_Tax.ToString()
                        , Cess_Amount.ToString()
                        , ITC_Available_Cess.ToString()
                        , "-"
                        , "-"
                        );
            }

            #endregion

            // b2b_Nil_Rated_INTRA

            #region b2b_RCM_INTRA (Reverse_Charges_Supplies) Export

            DataTable dt_b2b_Nil_INTRA = new DataTable("1");
            DataColumn b2b_Nil_INTRAMRowNo = dt_b2b_Nil_INTRA.Columns.Add("S. No.", typeof(int));
            dt_b2b_Nil_INTRA.Columns.Add(new DataColumn("Supplier Name", typeof(string)));
            dt_b2b_Nil_INTRA.Columns.Add(new DataColumn("Supplier GSTIN/UIN", typeof(string)));
            dt_b2b_Nil_INTRA.Columns.Add(new DataColumn("Invoice No", typeof(string)));
            dt_b2b_Nil_INTRA.Columns.Add(new DataColumn("Invoice Date", typeof(string)));
            dt_b2b_Nil_INTRA.Columns.Add(new DataColumn("Voucher No", typeof(string)));
            dt_b2b_Nil_INTRA.Columns.Add(new DataColumn("Voucher Date", typeof(string)));
            dt_b2b_Nil_INTRA.Columns.Add(new DataColumn("Place of Supply", typeof(string)));
            dt_b2b_Nil_INTRA.Columns.Add(new DataColumn("Total Taxable Value", typeof(double)));
            dt_b2b_Nil_INTRA.Columns.Add(new DataColumn("Branch Name", typeof(string)));
            dt_b2b_Nil_INTRA.TableName = "EXEMPT INTRA";



            Total_Invoice_Value = 0;
            Total_Taxable_Value = 0;
            Integrated_Tax_Amount = 0;
            ITC_Available_Integrated_Tax = 0;
            Central_Tax_Amount = 0;
            ITC_Available_Central_Tax = 0;
            State_Tax_Amount = 0;
            ITC_Available_State_Tax = 0;
            Cess_Amount = 0;
            ITC_Available_Cess = 0;

            ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "PlaceofSupply" }, new string[] { "15", ViewState["MNltVCount"].ToString(), "", "INTRA" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {
                    dt_b2b_Nil_INTRA.Rows.Add((i + 1)
                        , ds.Tables[0].Rows[i]["Supplier Name"].ToString()
                        , ds.Tables[0].Rows[i]["Supplier GSTIN/UIN"].ToString()
                        , ds.Tables[0].Rows[i]["Invoice No"].ToString()
                        , ds.Tables[0].Rows[i]["Invoice Date"].ToString()
                         , ds.Tables[0].Rows[i]["Voucher No"].ToString()
                        , ds.Tables[0].Rows[i]["Voucher Date"].ToString()
                        , ds.Tables[0].Rows[i]["Place of Supply"].ToString()
                        , ds.Tables[0].Rows[i]["Total Taxable Value"].ToString()
                         , ds.Tables[0].Rows[i]["Status"].ToString()
                        );


                    Total_Taxable_Value = Total_Taxable_Value + decimal.Parse(ds.Tables[0].Rows[i]["Total Taxable Value"].ToString());

                    //GrandInvoiceValue = GrandInvoiceValue + decimal.Parse(ds.Tables[0].Rows[i]["InvoiceValue"].ToString());
                    //GrandTaxableValue = GrandTaxableValue + decimal.Parse(ds.Tables[0].Rows[i]["TaxableValue"].ToString());
                }
                dt_b2b_Nil_INTRA.Rows.Add(null
                        , "Grand Total"
                        , "-"
                        , "-"
                        , "-"
                         , "-"
                        , "-"
                         , "-"
                        , Total_Taxable_Value.ToString()
                         , "-"

                        );
            }

            #endregion

            // b2b_Nil_Rated_INTER

            #region b2b_RCM_INTER (Reverse_Charges_Supplies) Export

            DataTable dt_b2b_Nil_INTER = new DataTable("1");
            DataColumn b2b_Nil_INTERMRowNo = dt_b2b_Nil_INTER.Columns.Add("S. No.", typeof(int));
            dt_b2b_Nil_INTER.Columns.Add(new DataColumn("Supplier Name", typeof(string)));
            dt_b2b_Nil_INTER.Columns.Add(new DataColumn("Supplier GSTIN/UIN", typeof(string)));
            dt_b2b_Nil_INTER.Columns.Add(new DataColumn("Invoice No", typeof(string)));
            dt_b2b_Nil_INTER.Columns.Add(new DataColumn("Invoice Date", typeof(string)));
            dt_b2b_Nil_INTER.Columns.Add(new DataColumn("Voucher No", typeof(string)));
            dt_b2b_Nil_INTER.Columns.Add(new DataColumn("Voucher Date", typeof(string)));
            dt_b2b_Nil_INTER.Columns.Add(new DataColumn("Place of Supply", typeof(string)));
            dt_b2b_Nil_INTER.Columns.Add(new DataColumn("Total Taxable Value", typeof(double)));
            dt_b2b_Nil_INTER.Columns.Add(new DataColumn("Branch Name", typeof(string)));
            dt_b2b_Nil_INTER.TableName = "EXEMPT INTER";



            Total_Invoice_Value = 0;
            Total_Taxable_Value = 0;
            Integrated_Tax_Amount = 0;
            ITC_Available_Integrated_Tax = 0;
            Central_Tax_Amount = 0;
            ITC_Available_Central_Tax = 0;
            State_Tax_Amount = 0;
            ITC_Available_State_Tax = 0;
            Cess_Amount = 0;
            ITC_Available_Cess = 0;

            ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "PlaceofSupply" }, new string[] { "15", ViewState["MNltVCount"].ToString(), "", "INTER" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {
                    dt_b2b_Nil_INTER.Rows.Add((i + 1)
                        , ds.Tables[0].Rows[i]["Supplier Name"].ToString()
                        , ds.Tables[0].Rows[i]["Supplier GSTIN/UIN"].ToString()
                        , ds.Tables[0].Rows[i]["Invoice No"].ToString()
                        , ds.Tables[0].Rows[i]["Invoice Date"].ToString()
                         , ds.Tables[0].Rows[i]["Voucher No"].ToString()
                        , ds.Tables[0].Rows[i]["Voucher Date"].ToString()
                        , ds.Tables[0].Rows[i]["Place of Supply"].ToString()
                        , ds.Tables[0].Rows[i]["Total Taxable Value"].ToString()
                         , ds.Tables[0].Rows[i]["Status"].ToString()
                        );


                    Total_Taxable_Value = Total_Taxable_Value + decimal.Parse(ds.Tables[0].Rows[i]["Total Taxable Value"].ToString());

                    //GrandInvoiceValue = GrandInvoiceValue + decimal.Parse(ds.Tables[0].Rows[i]["InvoiceValue"].ToString());
                    //GrandTaxableValue = GrandTaxableValue + decimal.Parse(ds.Tables[0].Rows[i]["TaxableValue"].ToString());
                }
                dt_b2b_Nil_INTER.Rows.Add(null
                        , "Grand Total"
                        , "-"
                        , "-"
                        , "-"
                         , "-"
                        , "-"
                         , "-"
                        , Total_Taxable_Value.ToString()
                         , "-"

                        );
            }

            #endregion

            // Group Table Export Excel
            #region roup Table Export Excel
            DataSet ds1 = new DataSet();
            ds1.Tables.Add(dt_b2b);
            ds1.Tables.Add(dt_b2b_UR);
            ds1.Tables.Add(dt_b2b_RCM);
            ds1.Tables.Add(dt_b2b_UR_RCM);
            ds1.Tables.Add(dt_b2b_Nil_INTRA);
            ds1.Tables.Add(dt_b2b_Nil_INTER);



            using (XLWorkbook wb = new XLWorkbook())
            {

                foreach (DataTable dt in ds1.Tables)
                {
                    var ws = wb.Worksheets.Add(dt.TableName);
                    ws.Cell(4, 1).InsertTable(dt);
                    //if (dt.TableName == "hsn")
                    //{
                    //    int rowcount = dt_hsn.Rows.Count;
                    //    for (int i = 0; i <= rowcount; i++)
                    //    {
                    //        if (i < rowcount)
                    //        {
                    //            if (dt_hsn.Rows[i]["S. No."].ToString() == "")
                    //            {
                    //                ws.Cell(i + 5, 5).Style.Fill.BackgroundColor = XLColor.Yellow;
                    //                ws.Cell(i + 5, 6).Style.Fill.BackgroundColor = XLColor.Yellow;
                    //                ws.Cell(i + 5, 7).Style.Fill.BackgroundColor = XLColor.Yellow;
                    //                ws.Cell(i + 5, 8).Style.Fill.BackgroundColor = XLColor.Yellow;
                    //                ws.Cell(i + 5, 9).Style.Fill.BackgroundColor = XLColor.Yellow;
                    //            }
                    //        }

                    //    }
                    //}
                    ws.Columns().AdjustToContents();
                    ws.Tables.FirstOrDefault().SetShowAutoFilter(false);
                }







                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                wb.DefaultShowRowColHeaders.ToString();






                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= GSTR2.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            #endregion
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void ddlRegionalOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillOffice();
    }
    protected void btnHSNItemWiseSummary_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridRateWise.DataSource = null;
            GridRateWise.DataBind();
            GridHSNSummery.Visible = false;
            GridHSNSummery_ItemWise.Visible = true;
            GridHSNSummery.DataSource = null;
            GridHSNSummery.DataBind();
            GridHSNSummery_ItemWise.DataSource = null;
            GridHSNSummery_ItemWise.DataBind();


            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "18", ViewState["MIncludedRet"].ToString(), "" }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    lblParticulars.Text = "HSN/SAC Summary - 13 for the period  " + txtFromDate.Text + " to " + txtToDate.Text;

                    btnBack.Visible = true;
                    DivTable.Visible = false;
                    btnBackNext.Visible = false;

                    GridHSNSummery_ItemWise.DataSource = ds;
                    GridHSNSummery_ItemWise.DataBind();

                    GridHSNSummery_ItemWise.FooterRow.Cells[3].Text = "Total";
                    GridHSNSummery_ItemWise.FooterRow.Cells[3].Font.Bold = true;



                    GridHSNSummery_ItemWise.FooterRow.Cells[5].Text = ds.Tables[1].Rows[0]["TotalQuantity"].ToString();
                    GridHSNSummery_ItemWise.FooterRow.Cells[6].Text = ds.Tables[1].Rows[0]["TotalValue"].ToString();
                    GridHSNSummery_ItemWise.FooterRow.Cells[7].Text = ds.Tables[1].Rows[0]["TaxableValue"].ToString();
                    GridHSNSummery_ItemWise.FooterRow.Cells[8].Text = ds.Tables[1].Rows[0]["IntegratedTaxAmount"].ToString();
                    GridHSNSummery_ItemWise.FooterRow.Cells[9].Text = ds.Tables[1].Rows[0]["CentralTaxAmount"].ToString();
                    GridHSNSummery_ItemWise.FooterRow.Cells[10].Text = ds.Tables[1].Rows[0]["StateUTTaxAmount"].ToString();
                    GridHSNSummery_ItemWise.FooterRow.Cells[11].Text = ds.Tables[1].Rows[0]["TotalTaxAmt"].ToString();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void GridHSNSummery_ItemWise_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblParticularsRate.Text = "";
            GridHSNSummeryDes.DataSource = null;
            GridHSNSummeryDes.DataBind();
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridHSNSummery_ItemWise.Rows[index];
                Label lblHSN = (Label)row.Cells[2].FindControl("lblHSN");
                Label lblDescription = (Label)row.Cells[2].FindControl("lblDescription");
                Label lblTypeOfSupply = (Label)row.Cells[2].FindControl("lblTypeOfSupply");
                Label lblUQC = (Label)row.Cells[2].FindControl("lblUQC");


                lblParticularsRate.Text = "Voucher Of HSN/SAC :  " + lblHSN.Text;


                string sDate = (Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd")).ToString();
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


                ds = objdb.ByProcedure("SpFinRptGSTR_2_NewF", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "HSN", "Description", "TypeOfSupply", "UQC", "FinancialYear", "Office_ID" },
                    new string[] { "19", ViewState["MIncludedRet"].ToString(), "", lblHSN.Text, lblDescription.Text, lblTypeOfSupply.Text, lblUQC.Text, FinancialYear, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    btnBack.Visible = false;
                    DivTable.Visible = false;
                    btnBackNext.Visible = true;

                    GridHSNSummery_ItemWise.Visible = false;

                    GridHSNSummeryDes.DataSource = ds;
                    GridHSNSummeryDes.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
}
