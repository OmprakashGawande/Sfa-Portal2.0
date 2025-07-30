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
using ClosedXML.Excel;

public partial class mis_Finance_RptGSTR_1F : System.Web.UI.Page
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
                    btnBack.Visible = false;
                    btnBackNext.Visible = false;
                    DivTable.Visible = false;
                    divExcel.Visible = false;
                    ddlOffice.Enabled = false;
                    btnShowDetailBook.Enabled = false;
                    FillDropdown();
                    FillVoucherDate();
                    // FillFromDate();
                    if (ViewState["OfficeType_Title"].ToString() != "Regional Office")
                    {
                        btnSearch_Click(sender, e);
                    }


                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }

            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt");

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
                    //ddlRegionalOffice.Items.Insert(ds.Tables[0].Rows.Count + 1, new ListItem("Production Unit", "010"));
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
            // ds = objdb.ByProcedure("SpFinRptTrialBalanceNew",
            //       new string[] { "flag" },
            //       new string[] { "0" }, "dataset");
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblDateRange.Text = "";


            ClearText();
            decimal TotalMVCount = 0;
            decimal TotalMTaxableValue = 0;
            decimal TotalMIntegratedTax = 0;
            decimal TotalMCentralTax = 0;
            decimal TotalMStateTax = 0;
            decimal TotalMCess = 0;
            decimal TotalMTaxAmount = 0;

            btnBack.Visible = false;
            btnBackNext.Visible = false;
            btnShowDetailBook.Enabled = false;

            DivTable.Visible = true;
            divExcel.Visible = false;
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

	    GridHSNItemWiseSummary.DataSource = null;
            GridHSNItemWiseSummary.DataBind();
            GridHSNSummeryDes.DataSource = null;
            GridHSNSummeryDes.DataBind();
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
                ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "Office_ID_Mlt", "FromDate", "ToDate" }, new string[] { "1", Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    // START TOP SECTION
                    DivTable.Visible = true;
                    divExcel.Visible = false;
                    lblTotalVoucher.Text = ds.Tables[0].Rows[0]["TotalVoucher"].ToString();
                    lblTotalVoucherSale.Text = ds.Tables[0].Rows[0]["IncludedRet"].ToString();
                    lblVoucherReturn.Text = ds.Tables[0].Rows[0]["IncludedHSNSAC"].ToString();
                    lblVoucherInComplete.Text = ds.Tables[0].Rows[0]["IncompleteHSNSAC"].ToString();
                    lblVoucherNotRelevent.Text = ds.Tables[0].Rows[0]["NotRelevant"].ToString();
                    lblVoucherMismatch.Text = ds.Tables[0].Rows[0]["Mismatch"].ToString();
                    // END TOP SECTION 

                    // START B2B
                    lblVB2BCount.Text = ds.Tables[1].Rows[0]["VB2BCount"].ToString();
                    lblTaxableValue.Text = ds.Tables[1].Rows[0]["TaxableValue"].ToString();
                    lblIntegratedTax.Text = ds.Tables[1].Rows[0]["IntegratedTax"].ToString();
                    lblCentralTax.Text = ds.Tables[1].Rows[0]["CentralTax"].ToString();
                    lblStateTax.Text = ds.Tables[1].Rows[0]["StateTax"].ToString();
                    lblCess.Text = "";
                    lblTaxAmount.Text = ds.Tables[1].Rows[0]["TaxAmount"].ToString();
                    lblInVoiceAmount.Text = ds.Tables[1].Rows[0]["InVoiceAmount"].ToString();
                    // END B2B

                    // START B2C (Large)
                    lblB2CLVCount.Text = ds.Tables[2].Rows[0]["VB2BCount"].ToString();
                    lblB2CLTaxableValue.Text = ds.Tables[2].Rows[0]["TaxableValue"].ToString();
                    lblB2CLIntegratedTax.Text = ds.Tables[2].Rows[0]["IntegratedTax"].ToString();
                    lblB2CLCentralTax.Text = ds.Tables[2].Rows[0]["CentralTax"].ToString();
                    lblB2CLStateTax.Text = ds.Tables[2].Rows[0]["StateTax"].ToString();
                    lblB2CLCess.Text = "";
                    lblB2CLTaxAmount.Text = ds.Tables[2].Rows[0]["TaxAmount"].ToString();
                    lblB2CLInVoiceAmount.Text = ds.Tables[2].Rows[0]["InVoiceAmount"].ToString();
                    // END B2C (Large)

                    // START B2C (Small)
                    lblB2CMVCount.Text = ds.Tables[3].Rows[0]["VB2BCount"].ToString();
                    lblB2CMTaxableValue.Text = ds.Tables[3].Rows[0]["TaxableValue"].ToString();
                    lblB2CMIntegratedTax.Text = ds.Tables[3].Rows[0]["IntegratedTax"].ToString();
                    lblB2CMCentralTax.Text = ds.Tables[3].Rows[0]["CentralTax"].ToString();
                    lblB2CMStateTax.Text = ds.Tables[3].Rows[0]["StateTax"].ToString();
                    lblB2CMCess.Text = "";
                    lblB2CMTaxAmount.Text = ds.Tables[3].Rows[0]["TaxAmount"].ToString();
                    lblB2CMInVoiceAmount.Text = ds.Tables[3].Rows[0]["InVoiceAmount"].ToString();
                    // END B2C (Small)

                    // START B2B CREADIT / DEBIT Registered
                    lblB2B_CDNoteVCount.Text = ds.Tables[4].Rows[0]["VB2BCount"].ToString();
                    lblB2B_CDNoteTaxableValue.Text = ds.Tables[4].Rows[0]["TaxableValue"].ToString();
                    lblB2B_CDNoteIntegratedTax.Text = ds.Tables[4].Rows[0]["IntegratedTax"].ToString();
                    lblB2B_CDNoteCentralTax.Text = ds.Tables[4].Rows[0]["CentralTax"].ToString();
                    lblB2B_CDNoteStateTax.Text = ds.Tables[4].Rows[0]["StateTax"].ToString();
                    lblB2B_CDNoteCess.Text = "";
                    lblB2B_CDNoteTaxAmount.Text = ds.Tables[4].Rows[0]["TaxAmount"].ToString();
                    lblB2B_CDNoteInVoiceAmount.Text = ds.Tables[4].Rows[0]["InVoiceAmount"].ToString();
                    // END B2B CREADIT / DEBIT Registered

                    // START B2B CREADIT / DEBIT UnRegistered
                    lblB2B_CDNoteUnRVCount.Text = ds.Tables[5].Rows[0]["VB2BCount"].ToString();
                    lblB2B_CDNoteUnRTaxableValue.Text = ds.Tables[5].Rows[0]["TaxableValue"].ToString();
                    lblB2B_CDNoteUnRIntegratedTax.Text = ds.Tables[5].Rows[0]["IntegratedTax"].ToString();
                    lblB2B_CDNoteUnRCentralTax.Text = ds.Tables[5].Rows[0]["CentralTax"].ToString();
                    lblB2B_CDNoteUnRStateTax.Text = ds.Tables[5].Rows[0]["StateTax"].ToString();
                    lblB2B_CDNoteUnRCess.Text = "";
                    lblB2B_CDNoteUnRTaxAmount.Text = ds.Tables[5].Rows[0]["TaxAmount"].ToString();
                    lblB2B_CDNoteUnRInVoiceAmount.Text = ds.Tables[5].Rows[0]["InVoiceAmount"].ToString();
                    // END B2B CREADIT / DEBIT UnRegistered

                    // START Nill Rated
                    lblNillRatedVCount.Text = ds.Tables[6].Rows[0]["VB2BCount"].ToString();
                    lblNillRatedTaxableValue.Text = ds.Tables[6].Rows[0]["TaxableValue"].ToString();
                    lblNillRatedIntegratedTax.Text = ds.Tables[6].Rows[0]["IntegratedTax"].ToString();
                    lblNillRatedCentralTax.Text = ds.Tables[6].Rows[0]["CentralTax"].ToString();
                    lblNillRatedStateTax.Text = ds.Tables[6].Rows[0]["StateTax"].ToString();
                    lblNillRatedCess.Text = "";
                    lblNillRatedTaxAmount.Text = ds.Tables[6].Rows[0]["TaxAmount"].ToString();
                    lblNillRatedInVoiceAmount.Text = ds.Tables[6].Rows[0]["InVoiceAmount"].ToString();
                    // END  Nill Rated

                    // START TOTAL 
                    lblTotalMVCount.Text = ds.Tables[7].Rows[0]["TotalMVCount"].ToString();
                    lblTotalMTaxableValue.Text = ds.Tables[7].Rows[0]["TotalMTaxableValue"].ToString();
                    lblTotalMIntegratedTax.Text = ds.Tables[7].Rows[0]["TotalMIntegratedTax"].ToString();
                    lblTotalMCentralTax.Text = ds.Tables[7].Rows[0]["TotalMCentralTax"].ToString();
                    lblTotalMStateTax.Text = ds.Tables[7].Rows[0]["TotalMStateTax"].ToString();
                    lblTotalMCess.Text = "";
                    lblTotalMTaxAmount.Text = ds.Tables[7].Rows[0]["TotalMTaxAmount"].ToString();
                    lblTotalMInVoiceAmount.Text = ds.Tables[7].Rows[0]["TotalMInVoiceAmount"].ToString();
                    // END TOTAL
                    // Multiple Voucher ID
                    ViewState["MltTotalVoucher"] = ds.Tables[8].Rows[0]["MltTotalVoucher"].ToString();
                    ViewState["MltIncludedRet"] = ds.Tables[8].Rows[0]["MltIncludedRet"].ToString();
                    ViewState["MltIncludedHSNSAC"] = ds.Tables[8].Rows[0]["MltIncludedHSNSAC"].ToString();
                    ViewState["MltIncompleteHSNSAC"] = ds.Tables[8].Rows[0]["MltIncompleteHSNSAC"].ToString();
                    ViewState["MltMismatch"] = ds.Tables[8].Rows[0]["MltMismatch"].ToString();

                    ViewState["MltB2BVoucher"] = ds.Tables[8].Rows[0]["MltB2BVoucher"].ToString();
                    ViewState["MltB2CLVoucher"] = ds.Tables[8].Rows[0]["MltB2CLVoucher"].ToString();
                    ViewState["MltB2CMVoucher"] = ds.Tables[8].Rows[0]["MltB2CMVoucher"].ToString();
                    ViewState["MltB2BCrCrRegVoucher"] = ds.Tables[8].Rows[0]["MltB2BCrCrRegVoucher"].ToString();
                    ViewState["MltB2BCrCrUnRegVoucher"] = ds.Tables[8].Rows[0]["MltB2BCrCrUnRegVoucher"].ToString();
                    ViewState["MltNilRatedVoucher"] = ds.Tables[8].Rows[0]["MltNilRatedVoucher"].ToString();


                    ViewState["MltVoucher"] = ds.Tables[8].Rows[0]["MltVoucher"].ToString();
                    //DataSet ds1 = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "9", ViewState["MltIncludedRet"].ToString(), "" }, "dataset");
                    DataSet ds1 = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "9", ViewState["MltVoucher"].ToString(), "" }, "dataset");
                    if (ds1.Tables.Count != 0 && ds.Tables[1].Rows.Count != 0)
                    {
                        lblHSNTaxableValue.Text = ds1.Tables[1].Rows[0]["TaxableValue"].ToString();
                        lblHSNIntegratedTaxAmount.Text = ds1.Tables[1].Rows[0]["IntegratedTaxAmount"].ToString();
                        lblHSNCentralTaxAmount.Text = ds1.Tables[1].Rows[0]["CentralTaxAmount"].ToString();
                        lblHSNStateUTTaxAmount.Text = ds1.Tables[1].Rows[0]["StateUTTaxAmount"].ToString();
                        lblHSNCessAmt.Text = ds1.Tables[1].Rows[0]["CessAmt"].ToString();
                        lblHSNTotalTaxAmt.Text = ds1.Tables[1].Rows[0]["TotalTaxAmt"].ToString();

			lblHSNTaxableValue_ItemWise.Text = ds1.Tables[1].Rows[0]["TaxableValue"].ToString();
                        lblHSNIntegratedTaxAmount_ItemWise.Text = ds1.Tables[1].Rows[0]["IntegratedTaxAmount"].ToString();
                        lblHSNCentralTaxAmount_ItemWise.Text = ds1.Tables[1].Rows[0]["CentralTaxAmount"].ToString();
                        lblHSNStateUTTaxAmount_ItemWise.Text = ds1.Tables[1].Rows[0]["StateUTTaxAmount"].ToString();
                        lblHSNCessAmt_ItemWise.Text = ds1.Tables[1].Rows[0]["CessAmt"].ToString();
                        lblHSNTotalTaxAmt_ItemWise.Text = ds1.Tables[1].Rows[0]["TotalTaxAmt"].ToString();
                    }
                    lblTTaxableValue.Text = ds.Tables[9].Rows[0]["TaxVal"].ToString();
                    lblSGST.Text = ds.Tables[9].Rows[0]["SGST"].ToString();
                    lblCGST.Text = ds.Tables[9].Rows[0]["CGST"].ToString();
                    lblIGST.Text = ds.Tables[9].Rows[0]["IGST"].ToString();
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
            ClearText();
            decimal TotalMVCount = 0;
            decimal TotalMTaxableValue = 0;
            decimal TotalMIntegratedTax = 0;
            decimal TotalMCentralTax = 0;
            decimal TotalMStateTax = 0;
            decimal TotalMCess = 0;
            decimal TotalMTaxAmount = 0;
            btnBack.Visible = false;
            DivTable.Visible = false;
            divExcel.Visible = false;
            lblParticulars.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    // TOTAL VOUCHER
    protected void btnTotalVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Total number of vouchers for the period " + txtFromDate.Text + " to " + txtToDate.Text;
            FILLGRID(ViewState["MltTotalVoucher"].ToString(), "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    // Total Voucher Sale
    protected void btnTotalVoucherSale_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Voucher Included in returns for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            FILLGRID(ViewState["MltIncludedRet"].ToString(), "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    //Included in HSN/SAC Summary
    protected void btnVoucherReturn_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Voucher Included in HSN/SAC Summary for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            FILLGRID(ViewState["MltIncludedHSNSAC"].ToString(), "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    // Incomplete HSN/SAC information (to be provided)
    protected void btnVoucherInComplete_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Voucher Incomplete HSN/SAC information (to be provided) for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            // FILLGRID("6");
            FILLGRID(ViewState["MltIncompleteHSNSAC"].ToString(), "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    //Not relevant for returns
    protected void btnVoucherNotRelevent_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Voucher Not relevant for returns for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            // FILLGRID("6");
            FILLGRID(ViewState["MltTotalVoucher"].ToString(), ViewState["MltIncludedRet"].ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    // Incomplete/Mismatch in information (to be resolved)
    protected void btnVoucherMismatch_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Voucher Incomplete/Mismatch in information (to be resolved) for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            FILLGRID(ViewState["MltMismatch"].ToString(), "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    // B2B Invoices - 4A, 4B, 4C, 6B, 6C
    protected void btnB2BVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Voucher B2B Invoices - 4A, 4B, 4C, 6B, 6C for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            //FILLGRID(ViewState["MltB2BVoucher"].ToString(), "");
            FILLRateWiseVoucher(ViewState["MltB2BVoucher"].ToString(), "", "B2B");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    protected void btnEB2BVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";


            // THE EXCEL FILE.
            string sFileName = "B2B.xls";
            sFileName = sFileName.Replace("/", "");

            // SEND OUTPUT TO THE CLIENT MACHINE USING "RESPONSE OBJECT".
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            Response.ContentType = "application/vnd.ms-excel";
            EnableViewState = false;

            System.IO.StringWriter objSW = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter objHTW = new System.Web.UI.HtmlTextWriter(objSW);

            //dg.HeaderStyle.Font.Bold = true;     // SET EXCEL HEADERS AS BOLD.
            //dg.RenderControl(objHTW);

            // ADD A ROW AT THE END OF THE SHEET SHOWING A RUNNING TOTAL OF PRICE.
            string HTMLTABLE = "";
            int sno = 0;
            decimal GrandInvoiceValue = 0;
            decimal GrandTaxableValue = 0;
            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {

                    Office += item.Value + ",";
                }
            }
            //if (ddlOffice.SelectedIndex > 0)
            //{
            ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "Office_ID_Mlt", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "3", Office, ViewState["MltB2BVoucher"].ToString(), "" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {
                    HTMLTABLE += "<tr>";
                    //HTMLTABLE += "<td>" + (i + 1).ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["GSTIN_UINofRecipient"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["InvoiceNumber"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["InvoiceDate"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["InvoiceValue"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["PlaceOfSupply"].ToString() + "</td>";
                    HTMLTABLE += "<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["ReverseCharge"].ToString() + "</td>";
                    HTMLTABLE += "<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["InvoiceType"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["Rate"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["TaxableValue"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["Branch"].ToString() + "</td>";
                    HTMLTABLE += "</tr>";

                    GrandInvoiceValue = GrandInvoiceValue + decimal.Parse(ds.Tables[0].Rows[i]["InvoiceValue"].ToString());
                    GrandTaxableValue = GrandTaxableValue + decimal.Parse(ds.Tables[0].Rows[i]["TaxableValue"].ToString());
                }

                HTMLTABLE += "<tr>";
                //HTMLTABLE += "<td colspan='2'><b>Grand Total</b></td>";
                //HTMLTABLE += "<td></td>";
                HTMLTABLE += "<td><b>Grand Total</b></td>";
                HTMLTABLE += "<td style='text-align:center;'>-</td>";
                HTMLTABLE += "<td style='text-align:center;'>-</td>";
                HTMLTABLE += "<td><b>" + GrandInvoiceValue.ToString() + "</b></td>";
                HTMLTABLE += "<td style='text-align:center;'>-</td>";
                HTMLTABLE += "<td style='text-align:center;'>-</td>";
                HTMLTABLE += "<td style='text-align:center;'>-</td>";
                HTMLTABLE += "<td style='text-align:center;'>-</td>";
                HTMLTABLE += "<td><b>" + GrandTaxableValue.ToString() + "</b></td>";
                HTMLTABLE += "<td style='text-align:center;'>-</td>";
                HTMLTABLE += "</tr>";
            }


            Response.Write("<table>" +
            "<tr></tr><tr></tr><tr></tr>"
                //+ "<th>S. No.</th>"
            + "<th style='background-color: peachpuff;'>GSTIN/UIN of Recipient</th>"
            + "<th style='background-color: peachpuff;'>Invoice Number</th>"
            + "<th style='background-color: peachpuff;'>Invoice date</th>"
            + "<th style='background-color: peachpuff;'>Invoice Value</th>"
            + "<th style='background-color: peachpuff;'>Place Of Supply</th>"
            + "<th style='background-color: peachpuff;'>Reverse Charge</th>"
            + "<th style='background-color: peachpuff;'>Invoice Type</th>"
            + "<th style='background-color: peachpuff;'>Rate</th>"
            + "<th style='background-color: peachpuff;'>Taxable Value</th>"
            + "<th style='background-color: peachpuff;'>Branch</th></tr>"
           + HTMLTABLE

            + "</table>");
            // STYLE THE SHEET AND WRITE DATA TO IT.
            Response.Write("<style> TABLE { border:dotted 1px #999; border-collapse: collapse; } " +
                "td { border:2px solid red; text-align:center }</style>");
            Response.Write(objSW.ToString());

            Response.End();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    // B2C(Large) Invoices - 5A, 5B Excel
    protected void btnB2CLVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Voucher B2C(Large) Invoices - 5A, 5B for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            // FILLGRID(ViewState["MltB2CLVoucher"].ToString(), "");
            FILLRateWiseVoucher(ViewState["MltB2CLVoucher"].ToString(), "", "");

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnEB2CLVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            // THE EXCEL FILE.
            string sFileName = "B2CL.xls";
            sFileName = sFileName.Replace("/", "");
            // SEND OUTPUT TO THE CLIENT MACHINE USING "RESPONSE OBJECT".
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            Response.ContentType = "application/vnd.ms-excel";
            EnableViewState = false;

            System.IO.StringWriter objSW = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter objHTW = new System.Web.UI.HtmlTextWriter(objSW);

            //dg.HeaderStyle.Font.Bold = true;     // SET EXCEL HEADERS AS BOLD.
            //dg.RenderControl(objHTW);

            // ADD A ROW AT THE END OF THE SHEET SHOWING A RUNNING TOTAL OF PRICE.
            string HTMLTABLE = "";
            int sno = 0;
            decimal GrandInvoiceValue = 0;
            decimal GrandTaxableValue = 0;
            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {

                    Office += item.Value + ",";
                }
            }

            //if (ddlOffice.SelectedIndex > 0)
            //{
            ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "Office_ID_Mlt", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "4", Office, ViewState["MltB2CLVoucher"].ToString(), "" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {

                    HTMLTABLE += "<tr>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["InvoiceNumber"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["InvoiceDate"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["InvoiceValue"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["PlaceOfSupply"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["Rate"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["TaxableValue"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["CessAmount"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["GSTIN_UINofRecipient"].ToString() + "</td>";
                    HTMLTABLE += "</tr>";

                    GrandInvoiceValue = GrandInvoiceValue + decimal.Parse(ds.Tables[0].Rows[i]["InvoiceValue"].ToString());
                    GrandTaxableValue = GrandTaxableValue + decimal.Parse(ds.Tables[0].Rows[i]["TaxableValue"].ToString());
                }


                HTMLTABLE += "<tr>";
                HTMLTABLE += "<td style='text-align:center;'><b>Grand Total</b></td>";

                HTMLTABLE += "<td style='text-align:center;'>-</td>";
                HTMLTABLE += "<td><b>" + GrandInvoiceValue.ToString() + "</b></td>";
                HTMLTABLE += "<td style='text-align:center;'>-</td>";
                HTMLTABLE += "<td style='text-align:center;'>-</td>";
                HTMLTABLE += "<td><b>" + GrandTaxableValue.ToString() + "</b></td>";
                HTMLTABLE += "<td style='text-align:center;'>-</td>";
                HTMLTABLE += "<td style='text-align:center;'>-</td>";
                HTMLTABLE += "<td style='text-align:center;'>-</td>";

                HTMLTABLE += "</tr>";


            }



            Response.Write("<table><tr></tr><tr></tr><tr></tr>" +
            "<tr>"
            + "<th style='background-color: peachpuff;'>Invoice Number</th>"
            + "<th style='background-color: peachpuff;'>Invoice date</th>"
            + "<th style='background-color: peachpuff;'>Invoice Value</th>"
            + "<th style='background-color: peachpuff;'>Place Of Supply</th>"
            + "<th style='background-color: peachpuff;'>Rate</th>"
            + "<th style='background-color: peachpuff;'>Taxable Value</th>"
            + "<th style='background-color: peachpuff;'>Cess Amount</th>"
            + "<th style='background-color: peachpuff;'>E-Commerce GSTIN</th>"
            + "</tr>"
            + HTMLTABLE

            + "</table>");
            // STYLE THE SHEET AND WRITE DATA TO IT.
            Response.Write("<style> TABLE { border:dotted 1px #999; border-collapse: collapse; } " +
                "td { border:2px solid red; text-align:center } </style>");
            Response.Write(objSW.ToString());

            Response.End();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    // B2C(Small) Invoices - 7
    protected void btnB2CMVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblParticulars.Text = "Voucher B2C(Small) Invoices - 7 for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            // FILLGRID(ViewState["MltB2CMVoucher"].ToString(), "");
            FILLRateWiseVoucher(ViewState["MltB2CMVoucher"].ToString(), "", "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void btnEB2CMVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";


            // THE EXCEL FILE.
            string sFileName = "B2CS.xls";
            sFileName = sFileName.Replace("/", "");

            // SEND OUTPUT TO THE CLIENT MACHINE USING "RESPONSE OBJECT".
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            Response.ContentType = "application/vnd.ms-excel";
            EnableViewState = false;

            System.IO.StringWriter objSW = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter objHTW = new System.Web.UI.HtmlTextWriter(objSW);

            //dg.HeaderStyle.Font.Bold = true;     // SET EXCEL HEADERS AS BOLD.
            //dg.RenderControl(objHTW);

            // ADD A ROW AT THE END OF THE SHEET SHOWING A RUNNING TOTAL OF PRICE.
            string HTMLTABLE = "";
            int sno = 0;
            decimal GrandInvoiceValue = 0;
            decimal GrandTaxableValue = 0;
            //string Office = "";
            //foreach (ListItem item in ddlOffice.Items)
            //{
            //    if (item.Selected)
            //    {

            //        Office += item.Value + ",";
            //    }
            //}
            //if (ddlOffice.SelectedIndex > 0)
            //{
            ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "12", ViewState["MltB2CMVoucher"].ToString(), "" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {
                    HTMLTABLE += "<tr>";

                    HTMLTABLE += "<td>OE</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["PlaceOfSupply"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["Rate"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["TaxableValue"].ToString() + "</td>";
                    HTMLTABLE += "<td>0.00</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["GSTIN_UINofRecipient"].ToString() + "</td>";

                    HTMLTABLE += "</tr>";

                    // GrandInvoiceValue = GrandInvoiceValue + decimal.Parse(ds.Tables[0].Rows[i]["InvoiceValue"].ToString());
                    GrandTaxableValue = GrandTaxableValue + decimal.Parse(ds.Tables[0].Rows[i]["TaxableValue"].ToString());
                }


                HTMLTABLE += "<tr>";
                HTMLTABLE += "<td ><b>Grand Total</b></td>";
                HTMLTABLE += "<td style='text-align:center;'>-</td>";
                HTMLTABLE += "<td style='text-align:center;'>-</td>";
                HTMLTABLE += "<td><b>" + GrandTaxableValue.ToString() + "</b></td>";
                HTMLTABLE += "<td>0</td>";
                HTMLTABLE += "<td style='text-align:center;'>-</td>";

                HTMLTABLE += "</tr>";


            }


            Response.Write("<table><tr></tr><tr></tr><tr></tr>" +
            "<tr>"
            + "<th style='background-color: peachpuff;'>Type</th>"
            + "<th style='background-color: peachpuff;'>Place Of Supply</th>"
            + "<th style='background-color: peachpuff;'>Rate</th>"
            + "<th style='background-color: peachpuff;'>Taxable Value</th>"
            + "<th style='background-color: peachpuff;'>Cess Amount</th>"
            + "<th style='background-color: peachpuff;'>E-Commerce GSTIN</th>"
            + "</tr>"
            + HTMLTABLE

            + "</table>");
            // STYLE THE SHEET AND WRITE DATA TO IT.
            Response.Write("<style> TABLE { border:dotted 1px #999; border-collapse: collapse; } " +
                "td { border:2px solid red; text-align:center } </style>");
            Response.Write(objSW.ToString());

            Response.End();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    // Credit/Debit Notes(Registered) - 9B
    protected void btnB2BCrCrRegVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Voucher Credit/Debit Notes(Registered) - 9B for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            //FILLGRID(ViewState["MltB2BCrCrRegVoucher"].ToString(), "");
            FILLRateWiseVoucher(ViewState["MltB2BCrCrRegVoucher"].ToString(), "", "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    // Credit/Debit Notes(Unregistered) - 9B
    protected void btnB2BCrCrUnRegVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Voucher Credit/Debit Notes(Unregistered) - 9B for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            // FILLGRID(ViewState["MltB2BCrCrUnRegVoucher"].ToString(), "");
            FILLRateWiseVoucher(ViewState["MltB2BCrCrUnRegVoucher"].ToString(), "", "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    // Nil Rated Invoices - 8A, 8B, 8C, 8D
    protected void btnNilRatedVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Voucher Nil Rated Invoices - 8A, 8B, 8C, 8D for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            //  FILLGRID(ViewState["MltNilRatedVoucher"].ToString(), "");
            //FILLRateWiseVoucher(ViewState["MltNilRatedVoucher"].ToString(), "","");
            FILLGRID(ViewState["MltNilRatedVoucher"].ToString(), "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnENilRatedVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            // THE EXCEL FILE.
            string sFileName = "EXEMP.xls";
            sFileName = sFileName.Replace("/", "");

            // SEND OUTPUT TO THE CLIENT MACHINE USING "RESPONSE OBJECT".
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            Response.ContentType = "application/vnd.ms-excel";
            EnableViewState = false;

            System.IO.StringWriter objSW = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter objHTW = new System.Web.UI.HtmlTextWriter(objSW);

            //dg.HeaderStyle.Font.Bold = true;     // SET EXCEL HEADERS AS BOLD.
            //dg.RenderControl(objHTW);

            // ADD A ROW AT THE END OF THE SHEET SHOWING A RUNNING TOTAL OF PRICE.
            string HTMLTABLE = "";

            decimal TotalQuantity = 0;
            decimal TotalValue = 0;
            decimal TaxableValue = 0;
            decimal IntegratedTaxAmount = 0;
            decimal CentralTaxAmount = 0;
            decimal StateUTTaxAmount = 0;

            HTMLTABLE += "<tr>";
            HTMLTABLE += "<td>Inter-State supplies to registered persons</td>";
            HTMLTABLE += "<td></td>";
            HTMLTABLE += "<td></td>";
            HTMLTABLE += "<td></td>";
            HTMLTABLE += "</tr>";

            HTMLTABLE += "<tr>";
            HTMLTABLE += "<td>Intra-State supplies to registered persons</td>";
            HTMLTABLE += "<td></td>";
            HTMLTABLE += "<td></td>";
            HTMLTABLE += "<td></td>";
            HTMLTABLE += "</tr>";

            HTMLTABLE += "<tr>";
            HTMLTABLE += "<td>Inter-State supplies to unregistered persons</td>";
            HTMLTABLE += "<td></td>";
            HTMLTABLE += "<td></td>";
            HTMLTABLE += "<td></td>";
            HTMLTABLE += "</tr>";

            HTMLTABLE += "<tr>";
            HTMLTABLE += "<td>Intra-State supplies to unregistered persons</td>";
            HTMLTABLE += "<td></td>";
            HTMLTABLE += "<td></td>";
            HTMLTABLE += "<td></td>";
            HTMLTABLE += "</tr>";

            Response.Write("<table>" +
            "<tr style='background-color: peachpuff;'>"

            + "<th>Description</th>"
            + "<th>Nil Rated Supplies</th>"
            + "<th>Exempted (other than nil rated/non GST supply )</th>"
             + "<th>Non-GST supplies</th>"
            + "</tr>"
            + HTMLTABLE

            + "</table>");
            // STYLE THE SHEET AND WRITE DATA TO IT.
            Response.Write("<style> TABLE { border:dotted 1px #999; border-collapse: collapse; } " +
                "td { border:2px solid red; text-align:center } </style>");
            Response.Write(objSW.ToString());

            Response.End();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        lblTotalVoucher.Text = "";
        lblTotalVoucherSale.Text = "";
        lblVoucherReturn.Text = "";
        lblVoucherInComplete.Text = "";
        lblVoucherNotRelevent.Text = "";
        lblVoucherMismatch.Text = "";
        // END TOP SECTION 

        // START B2B
        lblVB2BCount.Text = "";
        lblTaxableValue.Text = "";
        lblIntegratedTax.Text = "";
        lblCentralTax.Text = "";
        lblStateTax.Text = "";
        lblCess.Text = "";
        lblTaxAmount.Text = "";
        lblInVoiceAmount.Text = "";
        // END B2B

        // START B2C (Large)
        lblB2CLVCount.Text = "";
        lblB2CLTaxableValue.Text = "";
        lblB2CLIntegratedTax.Text = "";
        lblB2CLCentralTax.Text = "";
        lblB2CLStateTax.Text = "";
        lblB2CLCess.Text = "";
        lblB2CLTaxAmount.Text = "";
        lblB2CLInVoiceAmount.Text = "";
        // END B2C (Large)

        // START B2C (Small)
        lblB2CMVCount.Text = "";
        lblB2CMTaxableValue.Text = "";
        lblB2CMIntegratedTax.Text = "";
        lblB2CMCentralTax.Text = "";
        lblB2CMStateTax.Text = "";
        lblB2CMCess.Text = "";
        lblB2CMTaxAmount.Text = "";
        lblB2CMInVoiceAmount.Text = "";
        // END B2C (Small)

        // START B2B CREADIT / DEBIT Registered
        lblB2B_CDNoteVCount.Text = "";
        lblB2B_CDNoteTaxableValue.Text = "";
        lblB2B_CDNoteIntegratedTax.Text = "";
        lblB2B_CDNoteCentralTax.Text = "";
        lblB2B_CDNoteStateTax.Text = "";
        lblB2B_CDNoteCess.Text = "";
        lblB2B_CDNoteTaxAmount.Text = "";
        lblB2B_CDNoteInVoiceAmount.Text = "";
        // END B2B CREADIT / DEBIT Registered

        // START B2B CREADIT / DEBIT UnRegistered
        lblB2B_CDNoteUnRVCount.Text = "";
        lblB2B_CDNoteUnRTaxableValue.Text = "";
        lblB2B_CDNoteUnRIntegratedTax.Text = "";
        lblB2B_CDNoteUnRCentralTax.Text = "";
        lblB2B_CDNoteUnRStateTax.Text = "";
        lblB2B_CDNoteUnRCess.Text = "";
        lblB2B_CDNoteUnRTaxAmount.Text = "";
        lblB2B_CDNoteUnRInVoiceAmount.Text = "";
        // END B2B CREADIT / DEBIT UnRegistered

        // START Nill Rated
        lblNillRatedVCount.Text = "";
        lblNillRatedTaxableValue.Text = "";
        lblNillRatedIntegratedTax.Text = "";
        lblNillRatedCentralTax.Text = "";
        lblNillRatedStateTax.Text = "";
        lblNillRatedCess.Text = "";
        lblNillRatedTaxAmount.Text = "";
        lblNillRatedInVoiceAmount.Text = "";
        // END  Nill Rated

        // START TOTAL 
        lblTotalMVCount.Text = "";
        lblTotalMTaxableValue.Text = "";
        lblTotalMIntegratedTax.Text = "";
        lblTotalMCentralTax.Text = "";
        lblTotalMStateTax.Text = "";
        lblTotalMCess.Text = "";
        lblTotalMTaxAmount.Text = "";
        lblTotalMInVoiceAmount.Text = "";


        lblSGST.Text = "";
        lblCGST.Text = "";
        lblIGST.Text = "";
    }
    // HSN Summery
    protected void btnEHSN_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            // THE EXCEL FILE.
            string sFileName = "HSN.xls";
            sFileName = sFileName.Replace("/", "");

            // SEND OUTPUT TO THE CLIENT MACHINE USING "RESPONSE OBJECT".
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            Response.ContentType = "application/vnd.ms-excel";
            EnableViewState = false;

            System.IO.StringWriter objSW = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter objHTW = new System.Web.UI.HtmlTextWriter(objSW);

            //dg.HeaderStyle.Font.Bold = true;     // SET EXCEL HEADERS AS BOLD.
            //dg.RenderControl(objHTW);

            // ADD A ROW AT THE END OF THE SHEET SHOWING A RUNNING TOTAL OF PRICE.
            string HTMLTABLE = "";

            decimal TotalQuantity = 0;
            decimal TotalValue = 0;
            decimal TaxableValue = 0;
            decimal IntegratedTaxAmount = 0;
            decimal CentralTaxAmount = 0;
            decimal StateUTTaxAmount = 0;
            //ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "13", ViewState["MltIncludedRet"].ToString(), "" }, "dataset");
            ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" },
                new string[] { "13", ViewState["MltVoucher"].ToString(), "" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {
                    HTMLTABLE += "<tr>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["HSN"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["Description"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["UQC"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["TotalQuantity"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["TotalValue"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["TaxableValue"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["IntegratedTaxAmount"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["CentralTaxAmount"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["StateUTTaxAmount"].ToString() + "</td>";
                    HTMLTABLE += "<td>" + ds.Tables[0].Rows[i]["CessAmt"].ToString() + "</td>";
                    HTMLTABLE += "</tr>";

                    //TotalQuantity = TotalQuantity + decimal.Parse(ds.Tables[0].Rows[i]["TotalQuantity"].ToString());
                    //TotalValue = TotalValue + decimal.Parse(ds.Tables[0].Rows[i]["TotalValue"].ToString());
                    //TaxableValue = TaxableValue + decimal.Parse(ds.Tables[0].Rows[i]["TaxableValue"].ToString());
                    //IntegratedTaxAmount = IntegratedTaxAmount + decimal.Parse(ds.Tables[0].Rows[i]["IntegratedTaxAmount"].ToString());
                    //CentralTaxAmount = CentralTaxAmount + decimal.Parse(ds.Tables[0].Rows[i]["CentralTaxAmount"].ToString());
                    //StateUTTaxAmount = StateUTTaxAmount + decimal.Parse(ds.Tables[0].Rows[i]["StateUTTaxAmount"].ToString());
                }


                //HTMLTABLE += "<tr>";
                //HTMLTABLE += "<td style='text-align:center;'>-</td>";
                //HTMLTABLE += "<td style='text-align:center;'>-</td>";
                //HTMLTABLE += "<td style='text-align:center;'>-</td>";
                //HTMLTABLE += "<td><b>" + TotalQuantity.ToString() + "</b></td>";
                //HTMLTABLE += "<td><b>" + TotalValue.ToString() + "</b></td>";
                //HTMLTABLE += "<td><b>" + TaxableValue.ToString() + "</b></td>";
                //HTMLTABLE += "<td><b>" + IntegratedTaxAmount.ToString() + "</b></td>";
                //HTMLTABLE += "<td><b>" + CentralTaxAmount.ToString() + "</b></td>";
                //HTMLTABLE += "<td><b>" + StateUTTaxAmount.ToString() + "</b></td>";
                //HTMLTABLE += "</tr>";
            }

            Response.Write("<table><tr></tr><tr></tr><tr></tr>" +
            "<tr style='background-color: peachpuff;'>"
            + "<th>HSN</th>"
            + "<th>Description</th>"
            + "<th>UQC</th>"
            + "<th>Total Quantity</th>"
            + "<th>Total Value</th>"
            + "<th>Taxable Value</th>"
            + "<th>Integrated Tax Amount</th>"
            + "<th>Central Tax Amount</th>"
            + "<th>State/UT Tax Amount</th>"
             + "<th>Cess Amount</th>"
            + "</tr>"
            + HTMLTABLE

            + "</table>");
            // STYLE THE SHEET AND WRITE DATA TO IT.
            Response.Write("<style> TABLE { border:dotted 1px #999; border-collapse: collapse; } " +
                "td { border:2px solid red; text-align:center } </style>");
            Response.Write(objSW.ToString());

            Response.End();


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
                Label lblRateOfTax = (Label)row.Cells[2].FindControl("lblRateOfTax");
                Label lblParticulars1 = (Label)row.Cells[2].FindControl("lblParticulars1");


                lblParticularsRate.Text = lblParticulars1.Text + "[ Rate " + lblRateOfTax.Text + " % ]";

                FILLDayBook(ViewState["MltVoucherTx_DayBook"].ToString(), ViewState["MltVoucherTx_DayBookNotin"].ToString(), lblRateOfTax.Text);
            }
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
            divExcel.Visible = false;
            btnBack.Visible = false;
            btnBackNext.Visible = false;
            btnShowDetailBook.Enabled = false;

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridRateWise.DataSource = null;
            GridRateWise.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();

            GridHSNSummery.DataSource = null;
            GridHSNSummery.DataBind();
	    GridHSNItemWiseSummary.DataSource = null;
            GridHSNItemWiseSummary.DataBind();
            GridView1.Visible = false;
            GridRateWise.Visible = false;
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
            divExcel.Visible = true;
            btnBack.Visible = true;
            btnBackNext.Visible = false;
            btnShowDetailBook.Enabled = true;

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
             GridHSNItemWiseSummary.Visible = true;
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


            GridView1.Visible = true;

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();

            ViewState["DayMltVoucherTx_ID"] = MltVoucherTx_ID;
            ViewState["DayMltVoucherTx_IDNotin"] = MltVoucherTx_IDNotin;

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


                lblDateRange.Text = txtFromDate.Text + " to " + txtToDate.Text;
                //ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "Office_ID", "FromDate", "ToDate" }, new string[] { FflagNo, ddlOffice.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "FinancialYear", "Office_ID" },
                    new string[] { "2", MltVoucherTx_ID, MltVoucherTx_IDNotin, FinancialYear, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {

                    btnBack.Visible = true;
                    DivTable.Visible = false;
                    divExcel.Visible = true;
                    btnBackNext.Visible = false;
                    btnShowDetailBook.Enabled = false;
                    GridRateWise.Visible = false;
                    

                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();

                    GridView1.FooterRow.Cells[1].Text = "<b>Total</b>";
                    //GridView1.FooterRow.Cells[4].Text = "<b>" + ds.Tables[1].Rows[0]["DebitAmt"].ToString() + "</b>";
                    //GridView1.FooterRow.Cells[5].Text = "<b>" + ds.Tables[1].Rows[0]["CreditAmt"].ToString() + "</b>";
                    GridView1.FooterRow.Cells[5].Text = "<b>" + ds.Tables[1].Rows[0]["DebitAmt"].ToString() + "</b>";
                    GridView1.FooterRow.Cells[6].Text = "<b>" + ds.Tables[1].Rows[0]["CreditAmt"].ToString() + "</b>";
                }
                else
                {
                    lblParticulars.Text = "";
                }
            }
            // GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    // Rate Wise Voucher Details
    protected void FILLRateWiseVoucher(string MltVoucherTx_ID, string MltVoucherTx_IDNotin, string B2BType)
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
                //lblDateRange.Text = txtFromDate.Text + " to " + txtToDate.Text;
                ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "B2BType" }, new string[] { "6", MltVoucherTx_ID, MltVoucherTx_IDNotin, B2BType }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    btnBack.Visible = false;
                    DivTable.Visible = false;
                    divExcel.Visible = true;
                    btnBackNext.Visible = true;
                    btnShowDetailBook.Enabled = true;

                    ViewState["DaybookDetail"] = "True";

                    GridView1.Visible = false;

                    GridRateWise.DataSource = ds.Tables[0];
                    GridRateWise.DataBind();

                    GridRateWise.FooterRow.Cells[1].Text = "<b>Total</b>";
                    GridRateWise.FooterRow.Cells[3].Text = "<b>" + ds.Tables[1].Rows[0]["TaxableValue"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[4].Text = "<b>" + ds.Tables[1].Rows[0]["IGSTAmt"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[5].Text = "<b>" + ds.Tables[1].Rows[0]["CGSTAmt"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[6].Text = "<b>" + ds.Tables[1].Rows[0]["SGSTAmt"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[7].Text = "<b>" + ds.Tables[1].Rows[0]["CessAmount"].ToString() + "</b>";
                    GridRateWise.FooterRow.Cells[8].Text = "<b>" + ds.Tables[1].Rows[0]["TotalTaxAmt"].ToString() + "</b>";
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    protected void FILLDayBook(string MltVoucherTx_ID, string MltVoucherTx_IDNotin, string GSTRate)
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
                ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "GSTRate", "FinancialYear", "Office_ID" },
                    new string[] { "8", MltVoucherTx_ID, MltVoucherTx_IDNotin, GSTRate, FinancialYear, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    btnBack.Visible = false;
                    DivTable.Visible = false;
                    divExcel.Visible = true;
                    btnBackNext.Visible = true;
                    btnShowDetailBook.Enabled = false;
                    GridRateWise.Visible = false;

                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();

                    GridView2.FooterRow.Cells[1].Text = "<b>Total</b>";
                    //GridView2.FooterRow.Cells[5].Text = "<b>" + ds.Tables[1].Rows[0]["TaxableValue"].ToString() + "</b>";
                    //GridView2.FooterRow.Cells[6].Text = "<b>" + ds.Tables[1].Rows[0]["IGSTAmt"].ToString() + "</b>";
                    //GridView2.FooterRow.Cells[7].Text = "<b>" + ds.Tables[1].Rows[0]["CGSTAmt"].ToString() + "</b>";
                    //GridView2.FooterRow.Cells[8].Text = "<b>" + ds.Tables[1].Rows[0]["SGSTAmt"].ToString() + "</b>";
                    //GridView2.FooterRow.Cells[9].Text = "<b>" + ds.Tables[1].Rows[0]["CessAmt"].ToString() + "</b>";
                    //GridView2.FooterRow.Cells[10].Text = "<b>" + ds.Tables[1].Rows[0]["TotalTaxAmt"].ToString() + "</b>";
                    //GridView2.FooterRow.Cells[11].Text = "<b>" + ds.Tables[1].Rows[0]["InvoiceValue"].ToString() + "</b>";
                    GridView2.FooterRow.Cells[6].Text = "<b>" + ds.Tables[1].Rows[0]["TaxableValue"].ToString() + "</b>";
                    GridView2.FooterRow.Cells[7].Text = "<b>" + ds.Tables[1].Rows[0]["IGSTAmt"].ToString() + "</b>";
                    GridView2.FooterRow.Cells[8].Text = "<b>" + ds.Tables[1].Rows[0]["CGSTAmt"].ToString() + "</b>";
                    GridView2.FooterRow.Cells[9].Text = "<b>" + ds.Tables[1].Rows[0]["SGSTAmt"].ToString() + "</b>";
                    GridView2.FooterRow.Cells[10].Text = "<b>" + ds.Tables[1].Rows[0]["CessAmt"].ToString() + "</b>";
                    GridView2.FooterRow.Cells[11].Text = "<b>" + ds.Tables[1].Rows[0]["TotalTaxAmt"].ToString() + "</b>";
                    GridView2.FooterRow.Cells[12].Text = "<b>" + ds.Tables[1].Rows[0]["InvoiceValue"].ToString() + "</b>";
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
                //ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "9", ViewState["MltIncludedRet"].ToString(), "" }, "dataset");
                ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "9", ViewState["MltVoucher"].ToString(), "" }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    lblParticulars.Text = "HSN/SAC Summary - 12 for the period  " + txtFromDate.Text + " to " + txtToDate.Text;

                    btnBack.Visible = true;
                    DivTable.Visible = false;
                    divExcel.Visible = true;
                    btnBackNext.Visible = false;
                    btnShowDetailBook.Enabled = false;

                    GridHSNSummery.DataSource = ds.Tables[0];
                    GridHSNSummery.DataBind();

                    GridHSNSummery.FooterRow.Cells[4].Text = "<b>Total</b>";

                    GridHSNSummery.FooterRow.Cells[5].Text = ds.Tables[1].Rows[0]["TotalQuantity"].ToString();
                    GridHSNSummery.FooterRow.Cells[6].Text = ds.Tables[1].Rows[0]["TotalValue"].ToString();
                    GridHSNSummery.FooterRow.Cells[7].Text = ds.Tables[1].Rows[0]["TaxableValue"].ToString();
                    GridHSNSummery.FooterRow.Cells[8].Text = ds.Tables[1].Rows[0]["IntegratedTaxAmount"].ToString();
                    GridHSNSummery.FooterRow.Cells[9].Text = ds.Tables[1].Rows[0]["CentralTaxAmount"].ToString();
                    GridHSNSummery.FooterRow.Cells[10].Text = ds.Tables[1].Rows[0]["StateUTTaxAmount"].ToString();
                    GridHSNSummery.FooterRow.Cells[11].Text = ds.Tables[1].Rows[0]["CessAmt"].ToString();
                    GridHSNSummery.FooterRow.Cells[12].Text = ds.Tables[1].Rows[0]["TotalTaxAmt"].ToString();

                    //GridHSNSummery.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    //GridHSNSummery.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    //GridHSNSummery.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    //GridHSNSummery.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    //GridHSNSummery.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    //GridHSNSummery.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    //GridHSNSummery.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    //GridHSNSummery.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
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

                ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "HSN", "Description", "TypeOfSupply", "UQC", "FinancialYear", "Office_ID" },
                    new string[] { "10", ViewState["MltIncludedRet"].ToString(), "", lblHSN.Text, lblDescription.Text, lblTypeOfSupply.Text, lblUQC.Text, FinancialYear, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    btnBack.Visible = false;
                    DivTable.Visible = false;
                    divExcel.Visible = true;
                    btnBackNext.Visible = true;
                    btnShowDetailBook.Enabled = false;
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
    protected void btnShowDetailBook_Click(object sender, EventArgs e)
    {

        try
        {
            lblMsg.Text = "";
            if (ViewState["DaybookDetail"].ToString() == "True")
            {
                if (txtFromDate.Text != "" && txtToDate.Text != "")
                {
                    ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "11", ViewState["MltVoucherTx_DayBook"].ToString(), ViewState["MltVoucherTx_DayBookNotin"].ToString() }, "dataset");
                    if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        ViewState["DaybookDetail"] = "False";
                        btnBack.Visible = false;
                        DivTable.Visible = false;
                        btnBackNext.Visible = false;
                        GridRateWise.Visible = false;
                        btnShowDetailBook.Enabled = true;

                        GridView2.DataSource = ds;
                        GridView2.DataBind();
                    }
                    else
                    {
                        lblParticularsRate.Text = "";
                    }
                }
            }
            else
            {
                btnBack.Visible = false;
                DivTable.Visible = false;
                btnBackNext.Visible = true;
                GridRateWise.Visible = false;
                btnShowDetailBook.Enabled = false;

                FILLRateWiseVoucher(ViewState["MltVoucherTx_DayBook"].ToString(), ViewState["MltVoucherTx_DayBookNotin"].ToString(), "");

            }


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
            dt_b2b.Columns.Add(new DataColumn("GSTIN/UIN of Recipient", typeof(string)));
            dt_b2b.Columns.Add(new DataColumn("Invoice Number", typeof(string)));
            dt_b2b.Columns.Add(new DataColumn("Invoice date", typeof(string)));
            dt_b2b.Columns.Add(new DataColumn("Invoice Value", typeof(decimal)));
            dt_b2b.Columns.Add(new DataColumn("Place Of Supply", typeof(string)));
            dt_b2b.Columns.Add(new DataColumn("Reverse Charge", typeof(string)));
            dt_b2b.Columns.Add(new DataColumn("Invoice Type", typeof(string)));
            dt_b2b.Columns.Add(new DataColumn("E-Commerce GSTIN", typeof(string))); //E-Commerce GSTIN
            dt_b2b.Columns.Add(new DataColumn("Rate", typeof(decimal)));
            dt_b2b.Columns.Add(new DataColumn("Taxable Value", typeof(decimal)));
            dt_b2b.TableName = "b2b";

            decimal GrandInvoiceValue = 0;
            decimal GrandTaxableValue = 0;

            ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "Office_ID_Mlt", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "3", Office, ViewState["MltB2BVoucher"].ToString(), "" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {
                    dt_b2b.Rows.Add((i + 1)
                        , ds.Tables[0].Rows[i]["GSTIN_UINofRecipient"].ToString()
                        , ds.Tables[0].Rows[i]["InvoiceNumber"].ToString()
                        , ds.Tables[0].Rows[i]["InvoiceDate"].ToString()
                        , ds.Tables[0].Rows[i]["InvoiceValue"].ToString()
                        , ds.Tables[0].Rows[i]["PlaceOfSupply"].ToString()
                        , ds.Tables[0].Rows[i]["ReverseCharge"].ToString()
                        , ds.Tables[0].Rows[i]["InvoiceType"].ToString()
                        , ""     ////////E-Commerce GSTIN//////
                        , ds.Tables[0].Rows[i]["Rate"].ToString()
                        , ds.Tables[0].Rows[i]["TaxableValue"].ToString()
                        );

                    GrandInvoiceValue = GrandInvoiceValue + decimal.Parse(ds.Tables[0].Rows[i]["InvoiceValue"].ToString());
                    GrandTaxableValue = GrandTaxableValue + decimal.Parse(ds.Tables[0].Rows[i]["TaxableValue"].ToString());
                }
                //dt_b2b.Rows.Add(null
                //        , "Grand Total"
                //        , "-"
                //        , "-"
                //        , GrandInvoiceValue.ToString()
                //        , "-"
                //        , "-"
                //        , "-"
                //        , "-"
                //        , GrandTaxableValue.ToString()
                //        );
            }

            #endregion

            sno = 0;
            GrandInvoiceValue = 0;
            GrandTaxableValue = 0;

            // b2cl
            #region B2CL Export
            DataTable dt_b2cl = new DataTable("2");
            DataColumn b2clRowNo = dt_b2cl.Columns.Add("Invoice Number", typeof(string));
            dt_b2cl.Columns.Add(new DataColumn("Invoice date", typeof(string)));
            dt_b2cl.Columns.Add(new DataColumn("Invoice Value", typeof(decimal)));
            dt_b2cl.Columns.Add(new DataColumn("Place Of Supply", typeof(string)));
            dt_b2cl.Columns.Add(new DataColumn("Rate", typeof(decimal)));
            dt_b2cl.Columns.Add(new DataColumn("Taxable Value", typeof(decimal)));
            dt_b2cl.Columns.Add(new DataColumn("CGSTAmt", typeof(decimal)));
            dt_b2cl.Columns.Add(new DataColumn("SGSTAmt", typeof(decimal)));
            dt_b2cl.Columns.Add(new DataColumn("IGSTAmt", typeof(decimal)));
            dt_b2cl.Columns.Add(new DataColumn("Cess Amount", typeof(decimal)));
            dt_b2cl.Columns.Add(new DataColumn("E-Commerce GSTIN", typeof(string)));
            dt_b2cl.TableName = "b2cl";

            string VoucherB2cl = ViewState["MltB2CLVoucher"].ToString();// + ", " + ViewState["MltB2BCrCrRegVoucher"].ToString();

            ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "Office_ID_Mlt", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" },
                                new string[] { "4", Office, VoucherB2cl, "" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {
                    dt_b2cl.Rows.Add(
                        ds.Tables[0].Rows[i]["InvoiceNumber"].ToString()
                      , ds.Tables[0].Rows[i]["InvoiceDate"].ToString()
                      , ds.Tables[0].Rows[i]["InvoiceValue"].ToString()
                      , ds.Tables[0].Rows[i]["PlaceOfSupply"].ToString()
                      , ds.Tables[0].Rows[i]["Rate"].ToString()
                      , ds.Tables[0].Rows[i]["TaxableValue"].ToString()

                      , ds.Tables[0].Rows[i]["CessAmt"].ToString()
                      , ds.Tables[0].Rows[i]["CessAmt"].ToString()
                      , ds.Tables[0].Rows[i]["CessAmt"].ToString()

                      , ds.Tables[0].Rows[i]["CessAmt"].ToString()
                      , ds.Tables[0].Rows[i]["GSTIN_UINofRecipient"].ToString()
                      );
                    GrandInvoiceValue = GrandInvoiceValue + decimal.Parse(ds.Tables[0].Rows[i]["InvoiceValue"].ToString());
                    GrandTaxableValue = GrandTaxableValue + decimal.Parse(ds.Tables[0].Rows[i]["TaxableValue"].ToString());
                }

                //dt_b2cl.Rows.Add(null
                //       , "Grand Total"
                //       , "-"
                //       , GrandInvoiceValue.ToString()
                //       , "-"
                //       , "-"
                //       , GrandTaxableValue.ToString()
                //       , "-"
                //       , "-"
                //       );

            }

            #endregion
            sno = 0;
            GrandInvoiceValue = 0;
            GrandTaxableValue = 0;

            // b2cs
            #region B2CS Export
            DataTable dt_b2cs = new DataTable("3");
            DataColumn b2csRowNo = dt_b2cs.Columns.Add("S. No.", typeof(int));
            dt_b2cs.Columns.Add(new DataColumn("Type", typeof(string)));
            dt_b2cs.Columns.Add(new DataColumn("Place Of Supply", typeof(string)));
            dt_b2cs.Columns.Add(new DataColumn("Rate", typeof(decimal)));
            dt_b2cs.Columns.Add(new DataColumn("Taxable Value", typeof(decimal)));

            dt_b2cs.Columns.Add(new DataColumn("CGST", typeof(decimal)));
            dt_b2cs.Columns.Add(new DataColumn("SGST", typeof(decimal)));
            dt_b2cs.Columns.Add(new DataColumn("IGST", typeof(decimal)));

            dt_b2cs.Columns.Add(new DataColumn("Cess Amount", typeof(decimal)));
            dt_b2cs.Columns.Add(new DataColumn("E-Commerce GSTIN", typeof(string)));
            dt_b2cs.TableName = "b2cs";


            string VoucherB2cs = ViewState["MltB2CMVoucher"].ToString();// +", " + ViewState["MltB2BCrCrUnRegVoucher"].ToString();

            //ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "12", VoucherB2cs, "" }, "dataset");
            ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "15", VoucherB2cs, "" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {
                    dt_b2cs.Rows.Add((i + 1)
                     , "OE"
                     , ds.Tables[0].Rows[i]["PlaceOfSupply"].ToString()
                     , ds.Tables[0].Rows[i]["Rate"].ToString()
                     , ds.Tables[0].Rows[i]["TaxableValue"].ToString()

                     , ds.Tables[0].Rows[i]["CGSTAmt"].ToString()
                     , ds.Tables[0].Rows[i]["SGSTAmt"].ToString()
                     , ds.Tables[0].Rows[i]["IGSTAmt"].ToString()
                     , ds.Tables[0].Rows[i]["CessAmount"].ToString()
                    // , "0.00"
                     , ds.Tables[0].Rows[i]["GSTIN_UINofRecipient"].ToString()
                     );


                    GrandTaxableValue = GrandTaxableValue + decimal.Parse(ds.Tables[0].Rows[i]["TaxableValue"].ToString());
                }
                //dt_b2cs.Rows.Add(null
                //       , "Grand Total"
                //       , "-"
                //       , "-"
                //       , GrandTaxableValue.ToString()
                //       , "0.00"
                //       , "-"
                //       );

            }

            #endregion
            sno = 0;
            GrandInvoiceValue = 0;
            GrandTaxableValue = 0;
            // cdnr
            #region CDNR Export
            //done changes by mohini on 11FEB2022
            //DataTable dt_cdnr = new DataTable("4");
            //DataColumn cdnrRowNo = dt_cdnr.Columns.Add("Invoice/Advance Receipt date", typeof(string));
            //dt_cdnr.Columns.Add(new DataColumn("Note/Refund Voucher Number", typeof(string)));
            //dt_cdnr.Columns.Add(new DataColumn("Note/Refund Voucher date", typeof(string)));
            //dt_cdnr.Columns.Add(new DataColumn("Document Type", typeof(string)));
            //dt_cdnr.Columns.Add(new DataColumn("Reason For Issuing document", typeof(string)));
            //dt_cdnr.Columns.Add(new DataColumn("Place Of Supply", typeof(string)));
            //dt_cdnr.Columns.Add(new DataColumn("Note/Refund Voucher Value", typeof(string)));
            //dt_cdnr.Columns.Add(new DataColumn("Rate", typeof(string)));
            //dt_cdnr.Columns.Add(new DataColumn("Taxable Value", typeof(string)));
            //dt_cdnr.Columns.Add(new DataColumn("Cess Amount", typeof(string)));
            //dt_cdnr.Columns.Add(new DataColumn("Pre GST", typeof(string)));
            //dt_cdnr.TableName = "cdnr";
	    DataTable dt_cdnr = new DataTable("4");
            DataColumn cdnrRowNo = dt_cdnr.Columns.Add("S NO.", typeof(string));
            dt_cdnr.Columns.Add(new DataColumn("GSTIN", typeof(string)));
            dt_cdnr.Columns.Add(new DataColumn("RECEIVER NAME", typeof(string)));
            dt_cdnr.Columns.Add(new DataColumn("NOTE NO.", typeof(string)));
            dt_cdnr.Columns.Add(new DataColumn("NOTE DATE", typeof(string)));
            dt_cdnr.Columns.Add(new DataColumn("NOTE TYPE", typeof(string)));
            dt_cdnr.Columns.Add(new DataColumn("PLACE OF SUPPLY",typeof(string)));
            dt_cdnr.Columns.Add(new DataColumn("REVERSE CHARGE", typeof(string)));
            dt_cdnr.Columns.Add(new DataColumn("NOTE SUPPLY TYPE", typeof(string)));
            dt_cdnr.Columns.Add(new DataColumn("NOTE VALUE", typeof(string)));
            dt_cdnr.Columns.Add(new DataColumn("APPLICABLE % OF TAX", typeof(string)));
            dt_cdnr.Columns.Add(new DataColumn("RATE", typeof(string)));
            dt_cdnr.Columns.Add(new DataColumn("TAXABLE VALUE", typeof(string)));
            dt_cdnr.Columns.Add(new DataColumn("CESS AMOUNT", typeof(string)));
            dt_cdnr.Columns.Add(new DataColumn("ORIGINAL INVOICE NO.", typeof(string)));
            dt_cdnr.Columns.Add(new DataColumn("ORIGINAL INVOICE DATE", typeof(string)));
            dt_cdnr.TableName = "cdnr";

            string MltVoucherTx_IDCDNR = ViewState["MltB2BCrCrRegVoucher"].ToString();
            ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "16", MltVoucherTx_IDCDNR.ToString(), "" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {
                    dt_cdnr.Rows.Add((i + 1)
                        , ds.Tables[0].Rows[i]["GST_No"].ToString()
                        , ds.Tables[0].Rows[i]["Ledger_Name"].ToString()
                        , ds.Tables[0].Rows[i]["VoucherTx_No"].ToString()
                        , ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString()
                        ,ds.Tables[0].Rows[i]["NoteSupplyType"].ToString()
                        , ds.Tables[0].Rows[i]["Particulars"].ToString()
                        , ds.Tables[0].Rows[i]["ReverseCharge"].ToString()
                        , ds.Tables[0].Rows[i]["NoteSupplyType"].ToString()
                        , ds.Tables[0].Rows[i]["InvoiceValue"].ToString()
                        , ds.Tables[0].Rows[i]["RateOfTax"].ToString()
                        , ds.Tables[0].Rows[i]["RateOfTax"].ToString()
                        
                        , ds.Tables[0].Rows[i]["TaxableValue"].ToString()
                        ,"0"
                        , ds.Tables[0].Rows[i]["VoucherTx_Ref"].ToString()
                        , ds.Tables[0].Rows[i]["ORIGINALINVOICEDATE"].ToString()
                        );

                    //GrandInvoiceValue = GrandInvoiceValue + decimal.Parse(ds.Tables[0].Rows[i]["InvoiceValue"].ToString());
                    //GrandTaxableValue = GrandTaxableValue + decimal.Parse(ds.Tables[0].Rows[i]["TaxableValue"].ToString());
                }
                //dt_b2b.Rows.Add(null
                //        , "Grand Total"
                //        , "-"
                //        , "-"
                //        , GrandInvoiceValue.ToString()
                //        , "-"
                //        , "-"
                //        , "-"
                //        , "-"
                //        , GrandTaxableValue.ToString()
                //        );
            }


            #endregion

            // cdnur
            #region CDNUR Export
            //done changes by mohini on 11FEB2022
            //DataTable dt_cdnur = new DataTable("5");
            //DataColumn cdnurRowNo = dt_cdnur.Columns.Add("Document Type", typeof(string));
            //dt_cdnur.Columns.Add(new DataColumn("Invoice/Advance Receipt Number", typeof(string)));
            //dt_cdnur.Columns.Add(new DataColumn("Invoice/Advance Receipt date", typeof(string)));
            //dt_cdnur.Columns.Add(new DataColumn("Reason For Issuing document", typeof(string)));
            //dt_cdnur.Columns.Add(new DataColumn("Place Of Supplyt", typeof(string)));
            //dt_cdnur.Columns.Add(new DataColumn("Note/Refund Voucher Value", typeof(string)));
            //dt_cdnur.Columns.Add(new DataColumn("Rate", typeof(string)));
            //dt_cdnur.Columns.Add(new DataColumn("Taxable Value", typeof(string)));
            //dt_cdnur.Columns.Add(new DataColumn("Cess Amount", typeof(string)));
            //dt_cdnur.Columns.Add(new DataColumn("Pre GST", typeof(string)));
            //dt_cdnur.TableName = "cdnur";
            DataTable dt_cdnur = new DataTable("5");
            DataColumn cdnurRowNo = dt_cdnur.Columns.Add("S NO.", typeof(string));
            dt_cdnur.Columns.Add(new DataColumn("GSTIN", typeof(string)));
            dt_cdnur.Columns.Add(new DataColumn("RECEIVER NAME", typeof(string)));
            dt_cdnur.Columns.Add(new DataColumn("NOTE NO.", typeof(string)));
            dt_cdnur.Columns.Add(new DataColumn("NOTE DATE", typeof(string)));
            dt_cdnur.Columns.Add(new DataColumn("NOTE TYPE", typeof(string)));
            dt_cdnur.Columns.Add(new DataColumn("PLACE OF SUPPLY", typeof(string)));
            dt_cdnur.Columns.Add(new DataColumn("REVERSE CHARGE", typeof(string)));
            dt_cdnur.Columns.Add(new DataColumn("NOTE SUPPLY TYPE", typeof(string)));
            dt_cdnur.Columns.Add(new DataColumn("NOTE VALUE", typeof(string)));
            dt_cdnur.Columns.Add(new DataColumn("APPLICABLE % OF TAX", typeof(string)));
            dt_cdnur.Columns.Add(new DataColumn("RATE", typeof(string)));
            dt_cdnur.Columns.Add(new DataColumn("TAXABLE VALUE", typeof(string)));
            dt_cdnur.Columns.Add(new DataColumn("CESS AMOUNT", typeof(string)));
            dt_cdnur.Columns.Add(new DataColumn("ORIGINAL INVOICE NO.", typeof(string)));
            dt_cdnur.Columns.Add(new DataColumn("ORIGINAL INVOICE DATE", typeof(string)));
            dt_cdnur.TableName = "cdnur";

            string MltVoucherTx_IDCDNUR = ViewState["MltB2BCrCrUnRegVoucher"].ToString();
            ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "16", MltVoucherTx_IDCDNUR.ToString(), "" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {
                    dt_cdnur.Rows.Add((i + 1)
                        , ds.Tables[0].Rows[i]["GST_No"].ToString()
                        , ds.Tables[0].Rows[i]["Ledger_Name"].ToString()
                        , ds.Tables[0].Rows[i]["VoucherTx_No"].ToString()
                        , ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString()
                        , ds.Tables[0].Rows[i]["NoteSupplyType"].ToString()
                        , ds.Tables[0].Rows[i]["Particulars"].ToString()
                        , ds.Tables[0].Rows[i]["ReverseCharge"].ToString()
                        , ds.Tables[0].Rows[i]["NoteSupplyType"].ToString()
                        , ds.Tables[0].Rows[i]["InvoiceValue"].ToString()
                        , ds.Tables[0].Rows[i]["RateOfTax"].ToString()
                        , ds.Tables[0].Rows[i]["RateOfTax"].ToString()

                        , ds.Tables[0].Rows[i]["TaxableValue"].ToString()
                        , "0"
                        , ds.Tables[0].Rows[i]["VoucherTx_Ref"].ToString()
                        , ds.Tables[0].Rows[i]["ORIGINALINVOICEDATE"].ToString()
                        );

                    //GrandInvoiceValue = GrandInvoiceValue + decimal.Parse(ds.Tables[0].Rows[i]["InvoiceValue"].ToString());
                    //GrandTaxableValue = GrandTaxableValue + decimal.Parse(ds.Tables[0].Rows[i]["TaxableValue"].ToString());
                }
            }
            #endregion

            // exp
            DataTable dt_exp = new DataTable("6");
            DataColumn expRowNo = dt_exp.Columns.Add("Export Type", typeof(string));
            dt_exp.Columns.Add(new DataColumn("Invoice Number", typeof(string)));
            dt_exp.Columns.Add(new DataColumn("Invoice date", typeof(string)));
            dt_exp.Columns.Add(new DataColumn("Invoice Value", typeof(string)));
            dt_exp.Columns.Add(new DataColumn("Port Code", typeof(string)));
            dt_exp.Columns.Add(new DataColumn("Shipping Bill Number", typeof(string)));
            dt_exp.Columns.Add(new DataColumn("Shipping Bill Date", typeof(string)));
            dt_exp.Columns.Add(new DataColumn("Rate", typeof(string)));
            dt_exp.Columns.Add(new DataColumn("Taxable Value", typeof(string)));
            dt_exp.TableName = "exp";

            // at
            DataTable dt_at = new DataTable("7");
            DataColumn atRowNo = dt_at.Columns.Add("Place Of Supply", typeof(string));
            dt_at.Columns.Add(new DataColumn("Rate", typeof(string)));
            dt_at.Columns.Add(new DataColumn("Gross Advance Received", typeof(string)));
            dt_at.Columns.Add(new DataColumn("Cess Amount", typeof(string)));
            dt_at.TableName = "at";

            // atadj
            DataTable dt_atadj = new DataTable("8");
            DataColumn atadjRowNo = dt_atadj.Columns.Add("Place Of Supply", typeof(string));
            dt_atadj.Columns.Add(new DataColumn("Rate", typeof(string)));
            dt_atadj.Columns.Add(new DataColumn("Gross Advance Adjusted", typeof(string)));
            dt_atadj.Columns.Add(new DataColumn("Cess Amount", typeof(string)));
            dt_atadj.TableName = "atadj";

            // exemp
            #region EXEMP Export
            DataTable dt_exemp = new DataTable("9");
            DataColumn exempRowNo = dt_exemp.Columns.Add("S. No.", typeof(int));
            dt_exemp.Columns.Add(new DataColumn("Description", typeof(string)));
            dt_exemp.Columns.Add(new DataColumn("Nil Rated Supplies", typeof(string)));
            dt_exemp.Columns.Add(new DataColumn("Exempted (other than nil rated/non GST supply )", typeof(decimal)));
            dt_exemp.Columns.Add(new DataColumn("Non-GST supplies", typeof(string)));
            dt_exemp.TableName = "exemp";

            string InterReg = "";
            string InterUnReg = "";
            string IntraReg = "";
            string IntraUnReg = "";
            ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID" }, new string[] { "14", ViewState["MltNilRatedVoucher"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                InterReg = ds.Tables[0].Rows[0]["InterReg"].ToString();
                InterUnReg = ds.Tables[0].Rows[0]["InterUnReg"].ToString();
                IntraReg = ds.Tables[0].Rows[0]["IntraReg"].ToString();
                IntraUnReg = ds.Tables[0].Rows[0]["IntraUnReg"].ToString();
            }
            dt_exemp.Rows.Add(1, "Inter-State supplies to registered persons", "", decimal.Parse(InterReg), "");
            dt_exemp.Rows.Add(2, "Intra-State supplies to registered persons", "", decimal.Parse(IntraReg), "");
            dt_exemp.Rows.Add(3, "Inter-State supplies to unregistered persons", "", decimal.Parse(InterUnReg), "");
            dt_exemp.Rows.Add(4, "Intra-State supplies to unregistered persons", "", decimal.Parse(IntraUnReg), "");

            #endregion
            sno = 0;
            GrandInvoiceValue = 0;
            GrandTaxableValue = 0;

            // HSN
            #region HSN Export

            DataTable dt_hsn = new DataTable("11");
            DataColumn hsnRowNo = dt_hsn.Columns.Add("S. No.", typeof(int));
            dt_hsn.Columns.Add(new DataColumn("HSN", typeof(string)));
            dt_hsn.Columns.Add(new DataColumn("Description", typeof(string)));
            dt_hsn.Columns.Add(new DataColumn("UQC", typeof(string)));
            dt_hsn.Columns.Add(new DataColumn("Total Quantity", typeof(decimal)));
            dt_hsn.Columns.Add(new DataColumn("Total Value", typeof(decimal)));
            dt_hsn.Columns.Add(new DataColumn("Taxable Value", typeof(decimal)));
            dt_hsn.Columns.Add(new DataColumn("Central Tax Amount", typeof(decimal)));
            dt_hsn.Columns.Add(new DataColumn("State/UT Tax Amount", typeof(decimal)));
            dt_hsn.Columns.Add(new DataColumn("Integarated Tax Amount", typeof(decimal)));
            dt_hsn.TableName = "hsn";


            //ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "13", ViewState["MltIncludedRet"].ToString(), "" }, "dataset");
            ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" },
                new string[] { "13", ViewState["MltVoucher"].ToString(), "" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                string HSN = ds.Tables[0].Rows[0]["HSN"].ToString();
                decimal TotalQuantity = 0;
                decimal TotalValue = 0;
                decimal TaxableValue = 0;
                decimal CentralTaxAmount = 0;
                decimal StateUTTaxAmount = 0;
                decimal IntegratedUTTaxAmount = 0;
                for (int i = 0; i < rowcount; i++)
                {
                    // SUB TOTAL
                    if (HSN != ds.Tables[0].Rows[i]["HSN"].ToString())
                    {
                        dt_hsn.Rows.Add(null
                          , ""
                          , ""
                          , ""
                          , TotalQuantity.ToString()
                          , TotalValue.ToString()
                          , TaxableValue.ToString()
                          , CentralTaxAmount.ToString()
                          , StateUTTaxAmount.ToString()
                          , IntegratedUTTaxAmount.ToString()
                          );
                        TotalQuantity = 0;
                        TotalValue = 0;
                        TaxableValue = 0;
                        CentralTaxAmount = 0;
                        StateUTTaxAmount = 0;
                        IntegratedUTTaxAmount = 0;
                    }


                    dt_hsn.Rows.Add((i + 1)
                   , ds.Tables[0].Rows[i]["HSN"].ToString()
                   , ds.Tables[0].Rows[i]["Description"].ToString()
                   , ds.Tables[0].Rows[i]["UQC"].ToString()
                   , ds.Tables[0].Rows[i]["TotalQuantity"].ToString()
                   , ds.Tables[0].Rows[i]["TotalValue"].ToString()
                   , ds.Tables[0].Rows[i]["TaxableValue"].ToString()
                   , ds.Tables[0].Rows[i]["CentralTaxAmount"].ToString()
                   , ds.Tables[0].Rows[i]["StateUTTaxAmount"].ToString()
                   , ds.Tables[0].Rows[i]["IntegratedTaxAmount"].ToString()
                   );

                    // SUB TOTAL
                    HSN = ds.Tables[0].Rows[i]["HSN"].ToString();
                    TotalQuantity = TotalQuantity + decimal.Parse(ds.Tables[0].Rows[i]["TotalQuantity"].ToString());
                    TotalValue = TotalValue + decimal.Parse(ds.Tables[0].Rows[i]["TotalValue"].ToString());
                    TaxableValue = TaxableValue + decimal.Parse(ds.Tables[0].Rows[i]["TaxableValue"].ToString());
                    CentralTaxAmount = CentralTaxAmount + decimal.Parse(ds.Tables[0].Rows[i]["CentralTaxAmount"].ToString());
                    StateUTTaxAmount = StateUTTaxAmount + decimal.Parse(ds.Tables[0].Rows[i]["StateUTTaxAmount"].ToString());
                    IntegratedUTTaxAmount = IntegratedUTTaxAmount + decimal.Parse(ds.Tables[0].Rows[i]["IntegratedTaxAmount"].ToString());
                }
                if (rowcount > 0)
                {
                    dt_hsn.Rows.Add(null
                      , ""
                      , ""
                      , ""
                      , TotalQuantity.ToString()
                      , TotalValue.ToString()
                      , TaxableValue.ToString()
                      , CentralTaxAmount.ToString()
                      , StateUTTaxAmount.ToString()
                      , IntegratedUTTaxAmount.ToString()
                      );
                }
            }
            #endregion
            #region HSN Detail Export
            DataTable dt_hsnDetails = new DataTable("12");
            DataColumn hsnRowNoD = dt_hsnDetails.Columns.Add("S. No.", typeof(int));
            dt_hsnDetails.Columns.Add(new DataColumn("HSN", typeof(string)));
            dt_hsnDetails.Columns.Add(new DataColumn("Description", typeof(string)));
	    dt_hsnDetails.Columns.Add(new DataColumn("Rate%", typeof(string)));
            dt_hsnDetails.Columns.Add(new DataColumn("UQC", typeof(string)));
            dt_hsnDetails.Columns.Add(new DataColumn("Total Quantity", typeof(decimal)));
            dt_hsnDetails.Columns.Add(new DataColumn("Total Value", typeof(decimal)));
            dt_hsnDetails.Columns.Add(new DataColumn("Taxable Value", typeof(decimal)));
            dt_hsnDetails.Columns.Add(new DataColumn("Central Tax Amount", typeof(decimal)));
            dt_hsnDetails.Columns.Add(new DataColumn("State/UT Tax Amount", typeof(decimal)));
            dt_hsnDetails.Columns.Add(new DataColumn("Integrated Tax Amount", typeof(decimal)));
            dt_hsnDetails.TableName = "hsnItemWise";


            //ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "13", ViewState["MltIncludedRet"].ToString(), "" }, "dataset");
            ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" },
                new string[] { "13", ViewState["MltVoucher"].ToString(), "" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {

                    dt_hsnDetails.Rows.Add((i + 1)
                   , ds.Tables[0].Rows[i]["HSN"].ToString()
                   , ds.Tables[0].Rows[i]["Description"].ToString()
		        , ds.Tables[0].Rows[i]["Rate_Per"].ToString()
                   , ds.Tables[0].Rows[i]["UQC"].ToString()
                   , ds.Tables[0].Rows[i]["TotalQuantity"].ToString()
                   , ds.Tables[0].Rows[i]["TotalValue"].ToString()
                   , ds.Tables[0].Rows[i]["TaxableValue"].ToString()
                   , ds.Tables[0].Rows[i]["CentralTaxAmount"].ToString()
                   , ds.Tables[0].Rows[i]["StateUTTaxAmount"].ToString()
                   , ds.Tables[0].Rows[i]["IntegratedTaxAmount"].ToString()
                   );


                }

            }
            #endregion
	    #region HSN Detail Item Wise Export
            DataTable dt_hsnDetails_ItemWise = new DataTable("13");
            DataColumn hsnRowNoD_ItemWise = dt_hsnDetails_ItemWise.Columns.Add("S. No.", typeof(int));
            dt_hsnDetails_ItemWise.Columns.Add(new DataColumn("HSN", typeof(string)));
            dt_hsnDetails_ItemWise.Columns.Add(new DataColumn("Description", typeof(string)));
            dt_hsnDetails_ItemWise.Columns.Add(new DataColumn("Rate%", typeof(string)));
            dt_hsnDetails_ItemWise.Columns.Add(new DataColumn("UQC", typeof(string)));
            dt_hsnDetails_ItemWise.Columns.Add(new DataColumn("Total Quantity", typeof(decimal)));
            dt_hsnDetails_ItemWise.Columns.Add(new DataColumn("Total Value", typeof(decimal)));
            dt_hsnDetails_ItemWise.Columns.Add(new DataColumn("Taxable Value", typeof(decimal)));
            dt_hsnDetails_ItemWise.Columns.Add(new DataColumn("Central Tax Amount", typeof(decimal)));
            dt_hsnDetails_ItemWise.Columns.Add(new DataColumn("State/UT Tax Amount", typeof(decimal)));
            dt_hsnDetails_ItemWise.Columns.Add(new DataColumn("Integrated Tax Amount", typeof(decimal)));
            dt_hsnDetails_ItemWise.TableName = "hsnItemWiseDescription";


            //ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "13", ViewState["MltIncludedRet"].ToString(), "" }, "dataset");
            ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" },
                new string[] { "19", ViewState["MltVoucher"].ToString(), "" }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                int rowcount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < rowcount; i++)
                {

                    dt_hsnDetails_ItemWise.Rows.Add((i + 1)
                   , ds.Tables[0].Rows[i]["HSN"].ToString()
                   , ds.Tables[0].Rows[i]["Description"].ToString()
                   , ds.Tables[0].Rows[i]["Rate_Per"].ToString()
                   , ds.Tables[0].Rows[i]["UQC"].ToString()
                   , ds.Tables[0].Rows[i]["TotalQuantity"].ToString()
                   , ds.Tables[0].Rows[i]["TotalValue"].ToString()
                   , ds.Tables[0].Rows[i]["TaxableValue"].ToString()
                   , ds.Tables[0].Rows[i]["CentralTaxAmount"].ToString()
                   , ds.Tables[0].Rows[i]["StateUTTaxAmount"].ToString()
                   , ds.Tables[0].Rows[i]["IntegratedTaxAmount"].ToString()
                   );


                }

            }
            #endregion
            // docs
            #region Docs
            DataTable dt_docs = new DataTable("11");
            DataColumn docsRowNo = dt_docs.Columns.Add("Nature  of Document", typeof(string));
            dt_docs.Columns.Add(new DataColumn("Sr. No. From", typeof(string)));
            dt_docs.Columns.Add(new DataColumn("Sr. No. To", typeof(string)));
            dt_docs.Columns.Add(new DataColumn("Total Number", typeof(string)));
            dt_docs.Columns.Add(new DataColumn("Cancelled", typeof(string)));
            dt_docs.TableName = "docs";
            #endregion

            DataSet ds1 = new DataSet();
            ds1.Tables.Add(dt_b2b);
            ds1.Tables.Add(dt_b2cl);
            ds1.Tables.Add(dt_b2cs);
            ds1.Tables.Add(dt_cdnr);
            ds1.Tables.Add(dt_cdnur);
            ds1.Tables.Add(dt_exp);
            ds1.Tables.Add(dt_at);
            ds1.Tables.Add(dt_atadj);
            ds1.Tables.Add(dt_exemp);
            ds1.Tables.Add(dt_hsn);
            ds1.Tables.Add(dt_hsnDetails);
            ds1.Tables.Add(dt_docs);
 	   ds1.Tables.Add(dt_hsnDetails_ItemWise);
            using (XLWorkbook wb = new XLWorkbook())
            {

                foreach (DataTable dt in ds1.Tables)
                {
                    var ws = wb.Worksheets.Add(dt.TableName);
                    ws.Cell(4, 1).InsertTable(dt);
                    if (dt.TableName == "hsn")
                    {
                        int rowcount = dt_hsn.Rows.Count;
                        for (int i = 0; i <= rowcount; i++)
                        {
                            if (i < rowcount)
                            {
                                if (dt_hsn.Rows[i]["S. No."].ToString() == "")
                                {
                                    ws.Cell(i + 5, 5).Style.Fill.BackgroundColor = XLColor.Yellow;
                                    ws.Cell(i + 5, 6).Style.Fill.BackgroundColor = XLColor.Yellow;
                                    ws.Cell(i + 5, 7).Style.Fill.BackgroundColor = XLColor.Yellow;
                                    ws.Cell(i + 5, 8).Style.Fill.BackgroundColor = XLColor.Yellow;
                                    ws.Cell(i + 5, 9).Style.Fill.BackgroundColor = XLColor.Yellow;
                                }
                            }
                            //else
                            //{
                            //    ws.Cell(i + 5, 5).Style.Fill.BackgroundColor = XLColor.Yellow;
                            //    ws.Cell(i + 5, 6).Style.Fill.BackgroundColor = XLColor.Yellow;
                            //    ws.Cell(i + 5, 7).Style.Fill.BackgroundColor = XLColor.Yellow;
                            //    ws.Cell(i + 5, 8).Style.Fill.BackgroundColor = XLColor.Yellow;
                            //    ws.Cell(i + 5, 9).Style.Fill.BackgroundColor = XLColor.Yellow;
                            //}
                        }
                    }
                    ws.Columns().AdjustToContents();
                    ws.Tables.FirstOrDefault().SetShowAutoFilter(false);
                }


                //wb.Worksheets.Add(ds1);
                //var ws = wb;
                //ws.Worksheet(1).Row(1).InsertRowsAbove(3);
                //ws.Worksheet(2).Row(1).InsertRowsAbove(3);
                //ws.Worksheet(3).Row(1).InsertRowsAbove(3);
                //ws.Worksheet(4).Row(1).InsertRowsAbove(3);
                //ws.Worksheet(5).Row(1).InsertRowsAbove(3);
                //ws.Worksheet(6).Row(1).InsertRowsAbove(3);
                //ws.Worksheet(7).Row(1).InsertRowsAbove(3);
                //ws.Worksheet(8).Row(1).InsertRowsAbove(3);
                //ws.Worksheet(9).Row(1).InsertRowsAbove(3);
                //ws.Worksheet(10).Row(1).InsertRowsAbove(3);
                //ws.Worksheet(11).Row(1).InsertRowsAbove(3);





                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                wb.DefaultShowRowColHeaders.ToString();






                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= GSTR1.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
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
    protected void lnkbtnHSNItemWiseSummary_Click(object sender, EventArgs e)
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
            GridHSNSummery.Visible = false ;
            GridHSNItemWiseSummary.Visible =true;
            GridHSNSummery.DataSource = null;
            GridHSNSummery.DataBind();
            GridHSNItemWiseSummary.DataSource = null;
            GridHSNItemWiseSummary.DataBind();

            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                //ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "9", ViewState["MltIncludedRet"].ToString(), "" }, "dataset");
                ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin" }, new string[] { "17", ViewState["MltVoucher"].ToString(), "" }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    lblParticulars.Text = "HSN/SAC Summary - 12 for the period  " + txtFromDate.Text + " to " + txtToDate.Text;

                    btnBack.Visible = true;
                    DivTable.Visible = false;
                    divExcel.Visible = true;
                    btnBackNext.Visible = false;
                    btnShowDetailBook.Enabled = false;

                    GridHSNItemWiseSummary.DataSource = ds.Tables[0];
                    GridHSNItemWiseSummary.DataBind();

                    GridHSNItemWiseSummary.FooterRow.Cells[4].Text = "<b>Total</b>";

                    GridHSNItemWiseSummary.FooterRow.Cells[5].Text = ds.Tables[1].Rows[0]["TotalQuantity"].ToString();
                    GridHSNItemWiseSummary.FooterRow.Cells[6].Text = ds.Tables[1].Rows[0]["TotalValue"].ToString();
                    GridHSNItemWiseSummary.FooterRow.Cells[7].Text = ds.Tables[1].Rows[0]["TaxableValue"].ToString();
                    GridHSNItemWiseSummary.FooterRow.Cells[8].Text = ds.Tables[1].Rows[0]["IntegratedTaxAmount"].ToString();
                    GridHSNItemWiseSummary.FooterRow.Cells[9].Text = ds.Tables[1].Rows[0]["CentralTaxAmount"].ToString();
                    GridHSNItemWiseSummary.FooterRow.Cells[10].Text = ds.Tables[1].Rows[0]["StateUTTaxAmount"].ToString();
                    GridHSNItemWiseSummary.FooterRow.Cells[11].Text = ds.Tables[1].Rows[0]["CessAmt"].ToString();
                    GridHSNItemWiseSummary.FooterRow.Cells[12].Text = ds.Tables[1].Rows[0]["TotalTaxAmt"].ToString();

                    //GridHSNSummery.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    //GridHSNSummery.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    //GridHSNSummery.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    //GridHSNSummery.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    //GridHSNSummery.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    //GridHSNSummery.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                    //GridHSNSummery.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    //GridHSNSummery.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void GridHSNItemWiseSummary_RowCommand(object sender, GridViewCommandEventArgs e)
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
                GridViewRow row = GridHSNItemWiseSummary.Rows[index];
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

                ds = objdb.ByProcedure("SpFinRptGSTR_1F", new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "HSN", "Description", "TypeOfSupply", "UQC", "FinancialYear", "Office_ID" },
                    new string[] { "18", ViewState["MltIncludedRet"].ToString(), "", lblHSN.Text, lblDescription.Text, lblTypeOfSupply.Text, lblUQC.Text, FinancialYear, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    btnBack.Visible = false;
                    DivTable.Visible = false;
                    divExcel.Visible = true;
                    btnBackNext.Visible = true;
                    btnShowDetailBook.Enabled = false;
                    GridHSNItemWiseSummary.Visible = false;

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
