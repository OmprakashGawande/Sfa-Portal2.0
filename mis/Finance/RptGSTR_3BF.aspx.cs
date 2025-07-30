using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Finance_RptGSTR_3BF : System.Web.UI.Page
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
                    DivTable.Visible = false;
                    ddlOffice.Enabled = false;
                    divPurchaseExemptnillrated.Visible = false;
                    btnBack.Visible = false;
                    btnBackNext.Visible = false;
                    btnBackDayBook.Visible = false;
                    FillDropdown();
                    FillVoucherDate();
                    // FillFromDate();
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
            btnBack.Visible = false;
            btnBackNext.Visible = false;
            divPurchaseExemptnillrated.Visible = false;
            DivTable.Visible = true;
            lblParticulars.Text = "";
            lblParticularsRate.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();
            //GridView2.DataSource = null;
            //GridView2.DataBind();
            //GridRateWise.DataSource = null;
            //GridRateWise.DataBind();


            //GridHSNSummery.DataSource = null;
            //GridHSNSummery.DataBind();
            //GridHSNSummeryDes.DataSource = null;
            //GridHSNSummeryDes.DataBind();

            GV_Statsticts.DataSource = null;
            GV_Statsticts.DataBind();
            string Office = "";
            string OfficeName = "";
            int OfficeCount = 0;
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    OfficeCount += 1;
                    Office += item.Value + ",";
                    OfficeName += item.Text + " , ";
                }
            }

            if (txtFromDate.Text != "" && txtToDate.Text != "" && Office != "")
            {
                lblDateRange.Text = txtFromDate.Text + " to " + txtToDate.Text;
                spnfromdate.InnerHtml = txtFromDate.Text;
                spntodate.InnerHtml = txtToDate.Text;
                spnofcname.InnerHtml = "[ " + OfficeName + " ]";
                ds = objdb.ByProcedure("SpFinRptGSTR_3_NewF", new string[] { "flag", "Office_ID_Mlt", "FromDate", "ToDate" }, new string[] { "1", Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    //START Total number of vouchers for the period
                    lblTotalVoucher.Text = ds.Tables[0].Rows[0]["TotalVoucher"].ToString();
                    lblIncludedReturn.Text = ds.Tables[0].Rows[0]["IncludedRet"].ToString();
                    lblParticipatingReturn.Text = ds.Tables[0].Rows[0]["ParticaptingInRet"].ToString();
                    lblNoDirectReturn.Text = ds.Tables[0].Rows[0]["NoDirectImplicationInRet"].ToString();
                    lblNotRelevantReturn.Text = ds.Tables[0].Rows[0]["NotReleventRet"].ToString();
                    lblIncompleteMismatch.Text = ds.Tables[0].Rows[0]["MismatchInfo"].ToString();

                    ViewState["MTotalVoucher"] = ds.Tables[0].Rows[0]["MTotalVoucher"].ToString();
                    ViewState["MIncludedRet"] = ds.Tables[0].Rows[0]["MIncludedRet"].ToString();
                    ViewState["MParticaptingInRet"] = ds.Tables[0].Rows[0]["MParticaptingInRet"].ToString();
                    ViewState["MNoDirectImplicationInRet"] = ds.Tables[0].Rows[0]["MNoDirectImplicationInRet"].ToString();
                    ViewState["MNotReleventRet"] = ds.Tables[0].Rows[0]["MNotReleventRet"].ToString();

                    //END
                    //START  3.1 Outward supplies and inward supplies liable to reverse charge
                    lblTaxableValue.Text = ds.Tables[4].Rows[0]["TotalMTaxableValue"].ToString();
                    lblIntegratedTax.Text = ds.Tables[4].Rows[0]["TotalMIntegratedTax"].ToString();
                    lblCentralTax.Text = ds.Tables[4].Rows[0]["TotalMCentralTax"].ToString();
                    lblStateTax.Text = ds.Tables[4].Rows[0]["TotalMStateTax"].ToString();
                    //lblCessAmount.Text = ds.Tables[4].Rows[0]["CessAmount"].ToString();
                    lblTaxAmount.Text = ds.Tables[4].Rows[0]["TotalMTaxAmount"].ToString();


                    ////END
                    ////START  3.1 (A) Outward taxable supplies (other than zero rated, nil rated and exempted)
                    lblA_TaxableValue.Text = ds.Tables[1].Rows[0]["TaxableValue"].ToString();
                    lblA_IntegratedTax.Text = ds.Tables[1].Rows[0]["IntegratedTax"].ToString();
                    lblA_CentralTax.Text = ds.Tables[1].Rows[0]["CentralTax"].ToString();
                    lblA_StateTax.Text = ds.Tables[1].Rows[0]["StateTax"].ToString();
                    //lblA_CessAmount.Text = ds.Tables[1].Rows[0]["CessAmount"].ToString();
                    lblA_TaxAmount.Text = ds.Tables[1].Rows[0]["TaxAmount"].ToString();

                    ViewState["MltSalesCount"] = ds.Tables[1].Rows[0]["MltSalesCount"].ToString();
                    ////END
                    ////START  3.1 (B) Outward taxable supplies (zero rated)
                    //lblB_TaxableValue.Text = ds.Tables[1].Rows[0]["TaxableValue"].ToString();
                    //lblB_IntegratedTax.Text = ds.Tables[1].Rows[0]["IntegratedTax"].ToString();
                    //lblB_CentralTax.Text = ds.Tables[1].Rows[0]["CentralTax"].ToString();
                    //lblB_StateTax.Text = ds.Tables[1].Rows[0]["StateTax"].ToString();
                    ////lblB_CessAmount.Text = ds.Tables[3].Rows[0]["CessAmount"].ToString();
                    //lblB_TaxAmount.Text = ds.Tables[1].Rows[0]["TaxAmount"].ToString();
                    ////END
                    ////START  3.1 (C) Other Outward supplies (Nil rated, exempted)
                    lblC_TaxableValue.Text = ds.Tables[2].Rows[0]["TaxableValue"].ToString();
                    lblC_IntegratedTax.Text = ds.Tables[2].Rows[0]["IntegratedTax"].ToString();
                    lblC_CentralTax.Text = ds.Tables[2].Rows[0]["CentralTax"].ToString();
                    lblC_StateTax.Text = ds.Tables[2].Rows[0]["StateTax"].ToString();
                    //lblC_CessAmount.Text = ds.Tables[4].Rows[0]["CessAmount"].ToString();
                    lblC_TaxAmount.Text = ds.Tables[2].Rows[0]["TaxAmount"].ToString();
                    ViewState["MltNilRatedVoucher"] = ds.Tables[2].Rows[0]["MltNilRatedVoucher"].ToString();

                    ////END
                    ////START  3.1 (D) Inward supplies (liable to reverse charge)
                    lblD_TaxableValue.Text = ds.Tables[3].Rows[0]["TaxableValue"].ToString();
                    lblD_IntegratedTax.Text = ds.Tables[3].Rows[0]["IntegratedTax"].ToString();
                    lblD_CentralTax.Text = ds.Tables[3].Rows[0]["CentralTax"].ToString();
                    lblD_StateTax.Text = ds.Tables[3].Rows[0]["StateTax"].ToString();
                    //lblD_CessAmount.Text = ds.Tables[4].Rows[0]["CessAmount"].ToString();
                    lblD_TaxAmount.Text = ds.Tables[3].Rows[0]["TaxAmount"].ToString();

                    ViewState["MltAdjstmntVoucher"] = ds.Tables[3].Rows[0]["MltAdjstmntVoucher"].ToString();

                    ////END
                    ////START  3.1 (E) Non-GST outward supplies
                    //lblE_TaxableValue.Text = ds.Tables[5].Rows[0]["TaxableValue"].ToString();
                    //lblE_IntegratedTax.Text = ds.Tables[5].Rows[0]["IntegratedTax"].ToString();
                    //lblE_CentralTax.Text = ds.Tables[5].Rows[0]["CentralTax"].ToString();
                    //lblE_StateTax.Text = ds.Tables[5].Rows[0]["StateTax"].ToString();
                    //lblE_CessAmount.Text = ds.Tables[5].Rows[0]["CessAmount"].ToString();
                    //lblE_TaxAmount.Text = ds.Tables[5].Rows[0]["TaxAmount"].ToString();
                    ////END

                    //START  4 Eligible ITC
                    //lblITC_TaxableValue.Text = ds.Tables[7].Rows[0]["TaxableValue"].ToString();
                    lblITC_IntegratedTax.Text = ds.Tables[7].Rows[0]["TotalITCIntegratedTax"].ToString();
                    lblITC_CentralTax.Text = ds.Tables[7].Rows[0]["TotalITCCentralTax"].ToString();
                    lblITC_StateTax.Text = ds.Tables[7].Rows[0]["TotalITCStateTax"].ToString();
                    //lblITC_CessAmount.Text = ds.Tables[7].Rows[0]["TotalITCCess"].ToString();
                    lblITC_TaxAmount.Text = ds.Tables[7].Rows[0]["TotalITCTaxAmount"].ToString();
                    ////END


                    //START  4 (A)(3) Inward supplies liable to reverse charge (other than 1 & 2 above)
                    //lbl4A3_TaxableValue.Text = ds.Tables[6].Rows[0]["TaxableValue"].ToString();
                    lbl4A3_IntegratedTax.Text = ds.Tables[6].Rows[0]["IntegratedTax"].ToString();
                    lbl4A3_CentralTax.Text = ds.Tables[6].Rows[0]["CentralTax"].ToString();
                    lbl4A3_StateTax.Text = ds.Tables[6].Rows[0]["StateTax"].ToString();
                    //lbl4A3_CessAmount.Text = ds.Tables[6].Rows[0]["CessAmount"].ToString();
                    lbl4A3_TaxAmount.Text = ds.Tables[6].Rows[0]["TaxAmount"].ToString();

                    ViewState["MltOtherAdjstmntVoucher"] = ds.Tables[6].Rows[0]["MltOtherAdjstmntVoucher"].ToString();
                    ////END

                    //START  4 (A)(5) All other ITC
                    //lbl4A5_TaxableValue.Text = ds.Tables[5].Rows[0]["TaxableValue"].ToString();
                    lbl4A5_IntegratedTax.Text = ds.Tables[5].Rows[0]["IntegratedTax"].ToString();
                    lbl4A5_CentralTax.Text = ds.Tables[5].Rows[0]["CentralTax"].ToString();
                    lbl4A5_StateTax.Text = ds.Tables[5].Rows[0]["StateTax"].ToString();
                    //lbl4A5_CessAmount.Text = ds.Tables[5].Rows[0]["CessAmount"].ToString();
                    lbl4A5_TaxAmount.Text = ds.Tables[5].Rows[0]["TaxAmount"].ToString();

                    ViewState["MltPurchaseCount"] = ds.Tables[5].Rows[0]["MltPurchaseCount"].ToString();
                    ////END                  

                    //START   C	Net ITC Available (A) - (B)
                    //lblNETITC_TaxableValue.Text = ds.Tables[7].Rows[0]["TaxableValue"].ToString();
                    lblNETITC_IntegratedTax.Text = ds.Tables[7].Rows[0]["TotalITCIntegratedTax"].ToString();
                    lblNETITC_CentralTax.Text = ds.Tables[7].Rows[0]["TotalITCCentralTax"].ToString();
                    lblNETITC_StateTax.Text = ds.Tables[7].Rows[0]["TotalITCStateTax"].ToString();
                    //lblNETITC_CessAmount.Text = ds.Tables[7].Rows[0]["TotalITCCess"].ToString();
                    lblNETITC_TaxAmount.Text = ds.Tables[7].Rows[0]["TotalITCTaxAmount"].ToString();
                    ////END


                    //START  5 Value of exempt, nil rated and non-GST inward supplies	

                    lblNongst5_TaxableValue.Text = ds.Tables[8].Rows[0]["TaxableValue"].ToString();
                    lblNongst5_IntegratedTax.Text = ds.Tables[8].Rows[0]["IntegratedTax"].ToString();
                    lblNongst5_CentralTax.Text = ds.Tables[8].Rows[0]["CentralTax"].ToString();
                    lblNongst5_StateTax.Text = ds.Tables[8].Rows[0]["StateTax"].ToString();
                    //lblNongst5_CessAmount.Text = ds.Tables[8].Rows[0]["CessAmount"].ToString();
                    lblNongst5_TaxAmount.Text = ds.Tables[8].Rows[0]["TaxAmount"].ToString();
                    //START  A	From a supplier under composition scheme, exempt and nil rated supply	

                    lblNongst5A_TaxableValue.Text = ds.Tables[8].Rows[0]["TaxableValue"].ToString();
                    lblNongst5A_IntegratedTax.Text = ds.Tables[8].Rows[0]["IntegratedTax"].ToString();
                    lblNongst5A_CentralTax.Text = ds.Tables[8].Rows[0]["CentralTax"].ToString();
                    lblNongst5A_StateTax.Text = ds.Tables[8].Rows[0]["StateTax"].ToString();
                    //lblNongst5A_CessAmount.Text = ds.Tables[8].Rows[0]["CessAmount"].ToString();
                    lblNongst5A_TaxAmount.Text = ds.Tables[8].Rows[0]["TaxAmount"].ToString();
                    lblRCM.Text = "";
                    ViewState["MltPurchaseNilRatedVoucher"] = ds.Tables[8].Rows[0]["MltPurchaseNilRatedVoucher"].ToString();
                    ////END
                    if (ds.Tables[9].Rows.Count > 0)
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


                        int count = ds.Tables[9].Rows.Count;
                        decimal TotalRCM = 0;
                        for (int i = 0; i < count; i++)
                        {
                            SumrytaxLiability.Append("<tr>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[9].Rows[i]["CGSTPer"] + "</td>");
                            SumrytaxLiability.Append("<td class='rightborder'>Central Tax</td>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[9].Rows[i]["TaxableValue"] + "</td>");
                            SumrytaxLiability.Append("<td class='rightborder'>" + ds.Tables[9].Rows[i]["CGSTAmt"] + "</td>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[9].Rows[i]["RCMTaxableValue"] + "</td>");
                            SumrytaxLiability.Append("<td class='rightborder'>" + ds.Tables[9].Rows[i]["RCMCGSTAmt"] + "</td>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[9].Rows[i]["BalTaxableValue"] + "</td>");
                            SumrytaxLiability.Append("<td >" + ds.Tables[9].Rows[i]["BalCGSTAmt"] + "</td>");
                            SumrytaxLiability.Append("</tr>");
                            TotalRCM += decimal.Parse(ds.Tables[9].Rows[i]["BalCGSTAmt"].ToString());
                            TotalLiabilityTaxAmt += decimal.Parse(ds.Tables[9].Rows[i]["CGSTAmt"].ToString());
                            LiabilityBookedTaxAmt += decimal.Parse(ds.Tables[9].Rows[i]["RCMCGSTAmt"].ToString());
                            BalLiabilityTaxAmt += decimal.Parse(ds.Tables[9].Rows[i]["BalSGSTAmt"].ToString());

                        }
                        for (int i = 0; i < count; i++)
                        {
                            SumrytaxLiability.Append("<tr>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[9].Rows[i]["SGSTPer"] + "</td>");
                            SumrytaxLiability.Append("<td class='rightborder'>State Tax</td>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[9].Rows[i]["TaxableValue"] + "</td>");
                            SumrytaxLiability.Append("<td class='rightborder'>" + ds.Tables[9].Rows[i]["SGSTAmt"] + "</td>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[9].Rows[i]["RCMTaxableValue"] + "</td>");
                            SumrytaxLiability.Append("<td class='rightborder'>" + ds.Tables[9].Rows[i]["RCMSGSTAmt"] + "</td>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[9].Rows[i]["BalTaxableValue"] + "</td>");
                            SumrytaxLiability.Append("<td>" + ds.Tables[9].Rows[i]["BalSGSTAmt"] + "</td>");
                            SumrytaxLiability.Append("</tr>");
                            TotalRCM += decimal.Parse(ds.Tables[9].Rows[i]["BalSGSTAmt"].ToString());
                            TotalLiabilityTaxAmt += decimal.Parse(ds.Tables[9].Rows[i]["CGSTAmt"].ToString());
                            LiabilityBookedTaxAmt += decimal.Parse(ds.Tables[9].Rows[i]["RCMCGSTAmt"].ToString());
                            BalLiabilityTaxAmt += decimal.Parse(ds.Tables[9].Rows[i]["BalSGSTAmt"].ToString());

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

                }
            }

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
    protected void FILL_Statstics(string VoucherList, string MltVoucherTx_IDNotin)
    {
        try
        {
            btnBack.Visible = true;
            GridView2.Visible = false;
            GV_Statsticts.DataSource = null;
            GV_Statsticts.DataBind();
            ViewState["VoucherList"] = VoucherList;
            ViewState["NotVoucherList"] = MltVoucherTx_IDNotin;
            ds = objdb.ByProcedure("SpFinRptGSTR_3_NewF", new string[] { "flag", "MIncludedRet", "MltVoucherTx_IDNotin" }, new string[] { "2", VoucherList, MltVoucherTx_IDNotin }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                GV_Statsticts.DataSource = ds;
                GV_Statsticts.DataBind();
                GV_Statsticts.FooterRow.Cells[1].Text = "<b>Total</b>";
                GV_Statsticts.FooterRow.Cells[2].Text = "<b>" + ds.Tables[1].Rows[0]["VCount"].ToString() + "</b>";

                GV_Statsticts.Visible = true;
                DivTable.Visible = false;
                btnBackNext.Visible = false;
                btnBackDayBook.Visible = false;
                //GridRateWise.Visible = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    protected void btnIncludedReturn_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Summery Of Included Vouchers for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            FILL_Statstics(ViewState["MIncludedRet"].ToString(), "");

            //  FILLGRID(ViewState["MIncludedRet"].ToString(), "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnParticipatingReturn_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Summery Of Included Vouchers for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            FILL_Statstics(ViewState["MParticaptingInRet"].ToString(), "");

            //  FILLGRID(ViewState["MIncludedRet"].ToString(), "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnNoDirectReturn_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Summery Of Included Vouchers for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            FILL_Statstics(ViewState["MNoDirectImplicationInRet"].ToString(), "");

            //  FILLGRID(ViewState["MIncludedRet"].ToString(), "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnNotRelevantReturn_Click(object sender, EventArgs e)
    {
        try
        {
            lblParticulars.Text = "Summery Of Included Vouchers for the period  " + txtFromDate.Text + " to " + txtToDate.Text;
            FILL_Statstics(ViewState["MNotReleventRet"].ToString(), "");

            //  FILLGRID(ViewState["MIncludedRet"].ToString(), "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnIncompleteMismatch_Click(object sender, EventArgs e)
    {

    }

    protected void btn3_1A_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string MltVoucherTx_ID = ViewState["MltSalesCount"].ToString();
           // FILLDayBook(MltVoucherTx_ID, "");
            FILLDayBookOutwardTaxable(MltVoucherTx_ID, "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FILLDayBookOutwardTaxable(string MltVoucherTx_ID, string MltVoucherTx_IDNotin)
    {
        try
        {
            lblMsg.Text = "";
            decimal TotalTaxableValue = 0;
            decimal TotalCGST = 0;
            decimal TotalSGST = 0;
            decimal TotalIGST = 0;
            decimal TotalTax = 0;
            GridView2.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            //GridView2.DataSource = null;
            //GridView2.DataBind();

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
                ds = objdb.ByProcedure("SpFinRptGSTR_3_NewF",
                    new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "FinancialYear", "Office_ID" },
                    new string[] { "11", MltVoucherTx_ID, MltVoucherTx_IDNotin, FinancialYear, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.Visible = true;
                    btnBackDayBook.Visible = false;
                    btnBack.Visible = true;
                    DivTable.Visible = false;
                    btnBackNext.Visible = false;
                    //GridRateWise.Visible = false;

                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    TotalTaxableValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
                    TotalIGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("IGSTAmt"));
                    TotalCGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    TotalSGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    TotalTax = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmt"));

                    GridView1.FooterRow.Cells[1].Text = "<b>Grand Total</b>";
                    //GridView1.FooterRow.Cells[5].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[6].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[7].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[8].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[10].Text = "<b>" + TotalTax.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[6].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[7].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[8].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[9].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[11].Text = "<b>" + TotalTax.ToString() + "</b>";
                    GridView1.FooterRow.Cells[8].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    GridView1.FooterRow.Cells[9].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[10].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[11].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[12].Text = "<b>" + TotalTax.ToString() + "</b>";
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
    protected void btn3_1B_Click(object sender, EventArgs e)
    {

    }
    protected void btn3_1C_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string MltVoucherTx_ID = ViewState["MltNilRatedVoucher"].ToString();
            //FILLNillRatedDayBook(MltVoucherTx_ID, "");


            lblMsg.Text = "";
            decimal TotalTaxableValue = 0;
            decimal TotalCGST = 0;
            decimal TotalSGST = 0;
            decimal TotalIGST = 0;
            decimal TotalTax = 0;
            GridView2.Visible = false;
            divPurchaseExemptnillrated.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            //GridView2.DataSource = null;
            //GridView2.DataBind();

            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                // lblDateRange.Text = txtFromDate.Text + " to " + txtToDate.Text;
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

                string MltVoucherTx_IDNotin = "";
                ds = objdb.ByProcedure("SpFinRptGSTR_3_NewF",
                    new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "FinancialYear", "Office_ID" },
                    new string[] { "12", MltVoucherTx_ID, MltVoucherTx_IDNotin, FinancialYear, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.Visible = true;
                    btnBack.Visible = true;
                    btnBackDayBook.Visible = false;
                    DivTable.Visible = false;
                    btnBackNext.Visible = false;
                    //GridRateWise.Visible = false;

                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    TotalTaxableValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
                    TotalIGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("IGSTAmt"));
                    TotalCGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    TotalSGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    TotalTax = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmt"));

                    GridView1.FooterRow.Cells[1].Text = "<b>Grand Total</b>";
                    //GridView1.FooterRow.Cells[5].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[6].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[7].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[8].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[10].Text = "<b>" + TotalTax.ToString() + "</b>";
                   //GridView1.FooterRow.Cells[6].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[7].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[8].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[9].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[11].Text = "<b>" + TotalTax.ToString() + "</b>";
                    GridView1.FooterRow.Cells[8].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    GridView1.FooterRow.Cells[9].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[10].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[11].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[12].Text = "<b>" + TotalTax.ToString() + "</b>";
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
    protected void btn3_1D_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string MltVoucherTx_ID = ViewState["MltAdjstmntVoucher"].ToString();
            FILLStatAdjstTaxLiabilityDayBook(MltVoucherTx_ID, "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btn3_1E_Click(object sender, EventArgs e)
    {

    }
    protected void btn4A_1_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string MltVoucherTx_ID = ViewState["MltOtherAdjstmntVoucher"].ToString();
            FILLStatAdjstTaxCreditDayBook(MltVoucherTx_ID, "");
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
            //GridRateWise.DataSource = null;
            //GridRateWise.DataBind();
            //GridView2.DataSource = null;
            //GridView2.DataBind();

            //GridHSNSummery.DataSource = null;
            //GridHSNSummery.DataBind();

            GridView1.Visible = false;
            //GridRateWise.Visible = false;

            GV_Statsticts.DataSource = null;
            GV_Statsticts.DataBind();
            pnlSumrytaxLiability.Visible = false;
            divPurchaseExemptnillrated.Visible = false;


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
            //GridRateWise.Visible = true;

            GridView1.DataSource = null;
            GridView1.DataBind();

            //GridView2.DataSource = null;
            //GridView2.DataBind();

            //GridHSNSummeryDes.DataSource = null;
            //GridHSNSummeryDes.DataBind();

            GridView1.Visible = false;
            //GridHSNSummery.Visible = true;

            btnBackDayBook.Visible = false;
            pnlSumrytaxLiability.Visible = false;
            divPurchaseExemptnillrated.Visible = true;
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
            //GridRateWise.Visible = false;
            btnBack.Visible = true;
            GridView2.DataSource = null;
            GridView2.DataBind();

            lblParticularsRate.Text = "";
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
            GridView1.Visible = false;
            GridView2.Visible = true;
            btnBack.Visible = false;

            GridView2.DataSource = null;
            GridView2.DataBind();
            //GridView2.DataSource = null;
            //GridView2.DataBind();

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
                ds = objdb.ByProcedure("SpFinRptGSTR_3_NewF",
                    new string[] { "flag", "MltVoucherTx_ID", "VoucherTx_Type", "MltVoucherTx_IDNotin", "FinancialYear", "Office_ID" },
                    new string[] { "7", MltVoucherTx_ID, VoucherTx_Type, MltVoucherTx_IDNotin, FinancialYear, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    btnBackDayBook.Visible = true;
                    DivTable.Visible = false;
                    btnBackNext.Visible = false;
                    //GridRateWise.Visible = false;

                    GridView2.DataSource = ds;
                    GridView2.DataBind();

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
    protected void FILLDayBook(string MltVoucherTx_ID, string MltVoucherTx_IDNotin)
    {
        try
        {
            lblMsg.Text = "";
            decimal TotalTaxableValue = 0;
            decimal TotalCGST = 0;
            decimal TotalSGST = 0;
            decimal TotalIGST = 0;
            decimal TotalTax = 0;
            GridView2.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            //GridView2.DataSource = null;
            //GridView2.DataBind();

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
                ds = objdb.ByProcedure("SpFinRptGSTR_3_NewF",
                    new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "FinancialYear", "Office_ID" },
                    new string[] { "3", MltVoucherTx_ID, MltVoucherTx_IDNotin, FinancialYear, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.Visible = true;
                    btnBackDayBook.Visible = false;
                    btnBack.Visible = true;
                    DivTable.Visible = false;
                    btnBackNext.Visible = false;
                    //GridRateWise.Visible = false;

                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    TotalTaxableValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
                    TotalIGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("IGSTAmt"));
                    TotalCGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    TotalSGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    TotalTax = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmt"));

                    GridView1.FooterRow.Cells[1].Text = "<b>Grand Total</b>";
                    //GridView1.FooterRow.Cells[5].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[6].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[7].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[8].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[10].Text = "<b>" + TotalTax.ToString() + "</b>";
                     //GridView1.FooterRow.Cells[6].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[7].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[8].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[9].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[11].Text = "<b>" + TotalTax.ToString() + "</b>";
                    GridView1.FooterRow.Cells[8].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    GridView1.FooterRow.Cells[9].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[10].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[11].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[12].Text = "<b>" + TotalTax.ToString() + "</b>";
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
    protected void FILLNillRatedDayBook(string MltVoucherTx_ID, string MltVoucherTx_IDNotin)
    {
        try
        {
            lblMsg.Text = "";
            decimal TotalTaxableValue = 0;
            decimal TotalCGST = 0;
            decimal TotalSGST = 0;
            decimal TotalIGST = 0;
            decimal TotalTax = 0;
            GridView2.Visible = false;
            divPurchaseExemptnillrated.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            //GridView2.DataSource = null;
            //GridView2.DataBind();

            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                // lblDateRange.Text = txtFromDate.Text + " to " + txtToDate.Text;
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

                ds = objdb.ByProcedure("SpFinRptGSTR_3_NewF",
                    new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "FinancialYear", "Office_ID" },
                    new string[] { "6", MltVoucherTx_ID, MltVoucherTx_IDNotin, FinancialYear, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.Visible = true;
                    btnBack.Visible = true;
                    btnBackDayBook.Visible = false;
                    DivTable.Visible = false;
                    btnBackNext.Visible = false;
                    //GridRateWise.Visible = false;

                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    TotalTaxableValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
                    TotalIGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("IGSTAmt"));
                    TotalCGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    TotalSGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    TotalTax = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmt"));

                    GridView1.FooterRow.Cells[1].Text = "<b>Grand Total</b>";
                    //GridView1.FooterRow.Cells[5].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[6].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[7].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[8].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[10].Text = "<b>" + TotalTax.ToString() + "</b>";

                     //GridView1.FooterRow.Cells[6].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[7].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[8].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[9].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[11].Text = "<b>" + TotalTax.ToString() + "</b>";
                    GridView1.FooterRow.Cells[8].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    GridView1.FooterRow.Cells[9].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[10].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[11].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[12].Text = "<b>" + TotalTax.ToString() + "</b>";
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
    protected void FILLPurchaseNillRatedDayBook(string MltVoucherTx_ID, string MltVoucherTx_IDNotin)
    {
        try
        {
            lblMsg.Text = "";
            decimal TotalTaxableValue = 0;
            decimal TotalCGST = 0;
            decimal TotalSGST = 0;
            decimal TotalIGST = 0;
            decimal TotalTax = 0;
            GridView2.Visible = false;
            GridView1.Visible = true;
            btnBack.Visible = true;
            divPurchaseExemptnillrated.Visible = true;
            GridView1.DataSource = null;
            GridView1.DataBind();
            //GridView2.DataSource = null;
            //GridView2.DataBind();

            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                // lblDateRange.Text = txtFromDate.Text + " to " + txtToDate.Text;
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

                ds = objdb.ByProcedure("SpFinRptGSTR_3_NewF",
                    new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "FinancialYear", "Office_ID" },
                    new string[] { "6", MltVoucherTx_ID, MltVoucherTx_IDNotin, FinancialYear, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
                {
                    btnBackDayBook.Visible = false;
                    DivTable.Visible = false;
                    btnBackNext.Visible = true;
                    btnBack.Visible = false;
                    divPurchaseExemptnillrated.Visible = false;
                    //GridRateWise.Visible = false;

                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    TotalTaxableValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
                    TotalIGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("IGSTAmt"));
                    TotalCGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    TotalSGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    TotalTax = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmt"));

                    GridView1.FooterRow.Cells[1].Text = "<b>Grand Total</b>";
                    //GridView1.FooterRow.Cells[5].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[6].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[7].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[8].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[10].Text = "<b>" + TotalTax.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[6].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[7].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[8].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[9].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[11].Text = "<b>" + TotalTax.ToString() + "</b>";
                    GridView1.FooterRow.Cells[8].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    GridView1.FooterRow.Cells[9].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[10].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[11].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[12].Text = "<b>" + TotalTax.ToString() + "</b>";
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
    protected void FILLStatAdjstTaxLiabilityDayBook(string MltVoucherTx_ID, string MltVoucherTx_IDNotin)
    {
        try
        {
            decimal TotalTaxableValue = 0;
            decimal TotalCGST = 0;
            decimal TotalSGST = 0;
            decimal TotalIGST = 0;
            decimal TotalTax = 0;
            lblMsg.Text = "";
            GridView2.Visible = false;


            GridView1.DataSource = null;
            GridView1.DataBind();
            //GridView2.DataSource = null;
            //GridView2.DataBind();

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
                ds = objdb.ByProcedure("SpFinRptGSTR_3_NewF",
                    new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "FinancialYear", "Office_ID" },
                    new string[] { "4", MltVoucherTx_ID, MltVoucherTx_IDNotin, FinancialYear, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    GridView1.Visible = true;
                    btnBack.Visible = true;
                    btnBackDayBook.Visible = false;
                    DivTable.Visible = false;
                    btnBackNext.Visible = false;
                    //GridRateWise.Visible = false;

                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    TotalTaxableValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
                    TotalIGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("IGSTAmt"));
                    TotalCGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    TotalSGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    TotalTax = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmt"));

                    GridView1.FooterRow.Cells[1].Text = "<b>Grand Total</b>";
                    //GridView1.FooterRow.Cells[5].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[6].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[7].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[8].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[10].Text = "<b>" + TotalTax.ToString() + "</b>";
                     //GridView1.FooterRow.Cells[6].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[7].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[8].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[9].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[11].Text = "<b>" + TotalTax.ToString() + "</b>";
                    GridView1.FooterRow.Cells[8].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    GridView1.FooterRow.Cells[9].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[10].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[11].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[12].Text = "<b>" + TotalTax.ToString() + "</b>";
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
    protected void FILLStatAdjstTaxCreditDayBook(string MltVoucherTx_ID, string MltVoucherTx_IDNotin)
    {
        try
        {
            lblMsg.Text = "";
            decimal TotalTaxableValue = 0;
            decimal TotalCGST = 0;
            decimal TotalSGST = 0;
            decimal TotalIGST = 0;
            decimal TotalTax = 0;
            GridView2.Visible = false;
            GridView1.Visible = true;
            btnBack.Visible = true;

            GridView1.DataSource = null;
            GridView1.DataBind();
            //GridView2.DataSource = null;
            //GridView2.DataBind();

            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                // lblDateRange.Text = txtFromDate.Text + " to " + txtToDate.Text;
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

                ds = objdb.ByProcedure("SpFinRptGSTR_3_NewF",
                    new string[] { "flag", "MltVoucherTx_ID", "MltVoucherTx_IDNotin", "FinancialYear", "Office_ID" },
                    new string[] { "5", MltVoucherTx_ID, MltVoucherTx_IDNotin, FinancialYear, ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    btnBackDayBook.Visible = false;
                    DivTable.Visible = false;
                    btnBackNext.Visible = false;
                    //GridRateWise.Visible = false;

                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    TotalTaxableValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TaxableValue"));
                    TotalIGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("IGSTAmt"));
                    TotalCGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CGSTAmt"));
                    TotalSGST = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SGSTAmt"));
                    TotalTax = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalTaxAmt"));

                    GridView1.FooterRow.Cells[1].Text = "<b>Grand Total</b>";
                    //GridView1.FooterRow.Cells[5].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[6].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[7].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[8].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[10].Text = "<b>" + TotalTax.ToString() + "</b>";
                     //GridView1.FooterRow.Cells[6].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[7].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[8].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[9].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    //GridView1.FooterRow.Cells[11].Text = "<b>" + TotalTax.ToString() + "</b>";
                    GridView1.FooterRow.Cells[8].Text = "<b>" + TotalTaxableValue.ToString() + "</b>";
                    GridView1.FooterRow.Cells[9].Text = "<b>" + TotalIGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[10].Text = "<b>" + TotalCGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[11].Text = "<b>" + TotalSGST.ToString() + "</b>";
                    GridView1.FooterRow.Cells[12].Text = "<b>" + TotalTax.ToString() + "</b>";
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
    protected void btnRCM_Click(object sender, EventArgs e)
    {
        try
        {
            string Voucherlist = ViewState["MNoDirectImplicationInRet"].ToString();
            lblParticulars.Text = "List Of Reverse Charge Supplies  " + txtFromDate.Text + " to " + txtToDate.Text;
            //FILLRateWiseVoucher(Voucherlist, "");
            FILLDayBook(Voucherlist, "");
            pnlSumrytaxLiability.Visible = true;
            btnBack.Visible = false;
            DivTable.Visible = false;
            btnBack.Visible = true;

            //GridView1.Visible = false;


            //ViewState["B2BUR_RevMltVCount"].ToString()
            //ViewState["B2B_RevMltVCount"].ToString()


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    protected void btn4A_5_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string MltVoucherTx_ID = ViewState["MltPurchaseCount"].ToString();
            FILLDayBook(MltVoucherTx_ID, "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btn5_A_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DivTable.Visible = false;
            divPurchaseExemptnillrated.Visible = true;
            btnBack.Visible = true;
            string MltVoucherTx_ID = ViewState["MltPurchaseNilRatedVoucher"].ToString();
            ds = objdb.ByProcedure("SpFinRptGSTR_3_NewF", new string[] { "flag", "MltVoucherTx_ID" }, new string[] { "8", MltVoucherTx_ID }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblLocalNillRated.Text = ds.Tables[0].Rows[0]["TaxableValue"].ToString();
                    ViewState["MNillRatedLocalVoucher"] = ds.Tables[0].Rows[0]["PurchaseLocalNilRatedVoucher"].ToString();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    lblLocalExempted.Text = ds.Tables[1].Rows[0]["TaxableValue"].ToString();
                    ViewState["MExemptedLocalVoucher"] = ds.Tables[1].Rows[0]["PurchaseLocalExemptedVoucher"].ToString();
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    lblLocalTotalTax.Text = ds.Tables[2].Rows[0]["TaxableValue"].ToString();
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    lblInterStateNillRated.Text = ds.Tables[3].Rows[0]["TaxableValue"].ToString();
                    ViewState["MNillRatedInterstateVoucher"] = ds.Tables[3].Rows[0]["PurchaseInterStateNilRatedVoucher"].ToString();
                }
                if (ds.Tables[4].Rows.Count > 0)
                {
                    lblInterStateExempted.Text = ds.Tables[4].Rows[0]["TaxableValue"].ToString();
                    ViewState["MExemptedInterstateVoucher"] = ds.Tables[4].Rows[0]["PurchaseInterStateExemptedVoucher"].ToString();
                }
                if (ds.Tables[5].Rows.Count > 0)
                {
                    lblInterStateTotalTax.Text = ds.Tables[5].Rows[0]["TaxableValue"].ToString();
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnLocalExempted_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string MltVoucherTx_ID = ViewState["MExemptedLocalVoucher"].ToString();
            FILLPurchaseNillRatedDayBook(MltVoucherTx_ID, "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnLocalNillRated_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string MltVoucherTx_ID = ViewState["MNillRatedLocalVoucher"].ToString();
            FILLPurchaseNillRatedDayBook(MltVoucherTx_ID, "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnInterstateExempted_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string MltVoucherTx_ID = ViewState["MExemptedInterstateVoucher"].ToString();
            FILLPurchaseNillRatedDayBook(MltVoucherTx_ID, "");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnInterstateNillRated_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string MltVoucherTx_ID = ViewState["MNillRatedInterstateVoucher"].ToString();
            FILLPurchaseNillRatedDayBook(MltVoucherTx_ID, "");
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
}
