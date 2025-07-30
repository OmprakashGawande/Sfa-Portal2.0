using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;


public partial class mis_Finance_RptOutstandingAgeWiseLedger_New : System.Web.UI.Page
{
    DataSet ds, ds2, ds22;
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
                    ddlOffice.Enabled = false;

                    string FY = GetCurrentFinancialYear();
                    string[] YEAR = FY.Split('-');
                    DateTime FromDate = new DateTime(int.Parse(YEAR[0]), 4, 1);

                    txtToDate.Text = FromDate.ToString("dd/MM/yyyy");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    // HeadList();
                    //FillVoucherDate();
                    FillDropdown();

                    btnHeadExcel.Visible = false;
                    spnAltB.Visible = false;
                    spnAltW.Visible = false;
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
    protected void HeadList()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Head_ID", typeof(string)));
            dt.Columns.Add(new DataColumn("Ledger_ID", typeof(string)));
            dt.Columns.Add(new DataColumn("MonthID", typeof(string)));
            ViewState["Heads"] = dt;
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
            ds = objdb.ByProcedure("SpFinRptTrialBalanceNew",
                   new string[] { "flag" },
                   new string[] { "0" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
            }
            FillLedger();
            //string Office = "";
            //foreach (ListItem item in ddlOffice.Items)
            //{
            //    if (item.Selected)
            //    {
            //        Office += item.Value + ",";
            //    }
            //}
            //if (Office != "")
            //{
            //    DataSet ds1 = objdb.ByProcedure("SpFinRptGroupSummarysNew", new string[] { "flag", "Office_ID_Mlt", "FromDate", "ToDate" }, new string[] { "1", Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            //    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            //    {
            //        ddlGroup.DataSource = ds1;
            //        ddlGroup.DataTextField = "Head_Name";
            //        ddlGroup.DataValueField = "Head_ID";
            //        ddlGroup.DataBind();
            //        ddlGroup.Items.Insert(0, new ListItem("Select", "0"));
            //    }

            //    ddlBillType.SelectedValue = "AllBill";
            //    ddlBillType.Enabled = true;
            //}
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
            string Office = "";
            ddlLedger.DataSource = new string[] { };
            ddlLedger.DataBind();
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            if (Office != "")
            {

                ds = objdb.ByProcedure("SpFinRptOutstandingLedgers_New",
                       new string[] { "flag", "Office_ID_Mlt" },
                       new string[] { "8", Office }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlLedger.DataSource = ds;
                    ddlLedger.DataTextField = "Ledger_Name";
                    ddlLedger.DataValueField = "Ledger_ID";
                    ddlLedger.DataBind();
                    ddlLedger.Items.Insert(0, new ListItem("Select", "0"));
                }

                ddlBillType.SelectedValue = "AllBill";
                ddlBillType.Enabled = true;
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
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtToDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
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
            lblHeadName.Text = "";
            lblheadingFirst.Text = "";
            DivTable.InnerHtml = "";
            btnHeadExcel.Visible = false;
            spnAltB.Visible = false;
            spnAltW.Visible = false;
            FillLedger();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblHeadName.Text = "";
            lblheadingFirst.Text = "";
            DivTable.InnerHtml = "";
            btnHeadExcel.Visible = false;
            spnAltB.Visible = false;
            spnAltW.Visible = false;

            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }

            ddlLedger.DataSource = new string[] { };
            ddlLedger.DataBind();
            ds = objdb.ByProcedure("SpFinRptOutstandingLedgers_New",
                   new string[] { "flag", "Head_ID", "Office_ID_Mlt" },
                   new string[] { "16", ddlGroup.SelectedValue.ToString(), Office }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlLedger.DataSource = ds;
                ddlLedger.DataTextField = "Ledger_Name";
                ddlLedger.DataValueField = "Ledger_ID";
                ddlLedger.DataBind();
                ddlLedger.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOutstandingBillWise()
    {
        try
        {
            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            if (ddlBillType.SelectedValue.ToString() == "Cr")
            {
                ds = objdb.ByProcedure("SpFinRptOutstandingLedgers_New",
                   new string[] { "flag", "Office_ID_Mlt", "ToDate", "BillType", "Ledger_ID" },
                   new string[] { "9", Office, Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ddlBillType.SelectedValue.ToString(), ddlLedger.SelectedValue.ToString() }, "dataset");
            }
            else if (ddlBillType.SelectedValue.ToString() == "Dr")
            {
                ds = objdb.ByProcedure("SpFinRptOutstandingLedgers_New",
                   new string[] { "flag", "Office_ID_Mlt", "ToDate", "BillType", "Ledger_ID" },
                   new string[] { "10", Office, Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ddlBillType.SelectedValue.ToString(), ddlLedger.SelectedValue.ToString() }, "dataset");
            }
            else if (ddlBillType.SelectedValue.ToString() == "AllBill")
            {
                ds = objdb.ByProcedure("SpFinRptOutstandingLedgers_New",
                   new string[] { "flag", "Office_ID_Mlt", "ToDate", "BillType", "Ledger_ID" },
                   new string[] { "11", Office, Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ddlBillType.SelectedValue.ToString(), ddlLedger.SelectedValue.ToString() }, "dataset");
            }
            else if (ddlBillType.SelectedValue.ToString() == "ClearedBill")
            {
                ds = objdb.ByProcedure("SpFinRptOutstandingLedgers_New",
                   new string[] { "flag", "Office_ID_Mlt", "ToDate", "BillType", "Ledger_ID" },
                   new string[] { "17", Office, Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ddlBillType.SelectedValue.ToString(), ddlLedger.SelectedValue.ToString() }, "dataset");
            }


            StringBuilder htmlStr = new StringBuilder();
            decimal totalBillAmount = 0;
            decimal totalBillAmount_Days_0_30 = 0;
            decimal totalBillAmount_Days_30_60 = 0;
            decimal totalBillAmount_Days_60_90 = 0;
            decimal totalBillAmount_Days_90_180 = 0;
            decimal totalBillAmount_Days_180_365 = 0;
            decimal totalBillAmount_Days_365_More = 0;
            htmlStr.Append("<table  id='DetailGrid' class='datatable table table-bordered table-hover GridView2' style='font-family:verdana; font-size:12px; width:100%'>");
            htmlStr.Append("<thead>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<th style='width:120px !important;'>Date</th>");
            htmlStr.Append("<th style='min-width:350px !important;'>Ref. No.</th>");
            htmlStr.Append("<th style='min-width:150px !important;'>Party Name</th>");
            htmlStr.Append("<th>Office Name</th>");
            htmlStr.Append("<th>Pending Amount (INR)</th>");
            htmlStr.Append("<th>Dr/Cr</th>");

            if (chk0_30.Checked == true)
            {
                htmlStr.Append("<th>( < 30 Days)</th>");
            }
            if (chk30_60.Checked == true)
            {
                htmlStr.Append("<th>30 To 60 Days</th>");
            }
            if (chk60_90.Checked == true)
            {
                htmlStr.Append("<th>60 To 90 Days</th>");
            }
            if (chk90_180.Checked == true)
            {
                htmlStr.Append("<th>90 To 180 Days</th>");
            }
            if (chk180_365.Checked == true)
            {
                htmlStr.Append("<th>180 To 365 Days</th>");
            }
            if (chk365_0.Checked == true)
            {
                htmlStr.Append("<th>( > 365  Days)</th>");
            }
            htmlStr.Append("<th>OverDue By Days</th>");
            htmlStr.Append("</tr>");
            htmlStr.Append("</thead>");
            htmlStr.Append("<tbody>");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                spnAltW.Visible = true;

                DataView dvBill = new DataView();


                DataView dvVoucher = new DataView();


                int Count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<tr style='background: #79554840 !important;  class='GroupHeading'>");
                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["BillByBillTx_Date"].ToString() + "<br/><i class='fa fa-plus explode'></i> &nbsp;&nbsp; <i class='fa fa-minus implode'></i></td>");
                    //htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["BillByBillTx_Date"].ToString() + "</td>");
                    htmlStr.Append("<td style='padding:0px'><table style='width:100%'><tr><td>" + ds.Tables[0].Rows[i]["BillByBillTx_Ref"].ToString() + "</td>");
                    /***************/
                    htmlStr.Append("<table><tr style='background:#e2e2e2 !important;width:100%; font-style:italic; display:none;' class='GroupChildren'><td colspan='4'>" + ds.Tables[0].Rows[i]["VoucherDetail"].ToString() + "</td></tr>");
                    //*****//
                    int TotalEntry = Int32.Parse(ds.Tables[0].Rows[i]["TotalEntry"].ToString());
                    if (TotalEntry > 1)
                    {

                        //ds2 = objdb.ByProcedure("SpFinRptOutstanding",
                        //                       new string[] { "flag", "Office_ID_Mlt", "ToDate", "FromDate", "Ledger_ID", "BillByBillTx_Ref" },
                        //                       new string[] { "2", Office, Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), ds.Tables[0].Rows[i]["Ledger_ID"].ToString(), ds.Tables[0].Rows[i]["BillByBillTx_Ref"].ToString() }, "dataset");

                        dvBill = ds.Tables[1].DefaultView;
                        dvBill.RowFilter = "BillByBillDate <= '" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "'	AND Office_ID IN (" + Office + ") AND BillByBillTx_Ref = '" + ds.Tables[0].Rows[i]["BillByBillTx_Ref"].ToString() + "'"; // Order by VoucherTx_ID";
                        DataTable dt = dvBill.ToTable();

                        //if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                        if (dt.Rows.Count > 0)
                        {
                            int Count2 = dt.Rows.Count;
                            for (int p = 0; p < Count2; p++)
                            {
                                string view_edit_link = "";
                                if (dt.Rows[p]["VoucherTx_ID"].ToString() != "")
                                {
                                    view_edit_link = "<a class='view_link' target='_blank' href='" + dt.Rows[p]["VoucherURL"].ToString() + "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(dt.Rows[p]["VoucherTx_ID"].ToString()) + "&Action=" + APIProcedure.Client_Encrypt("1") + "'><i class='fa fa-eye'></i>View</a>&nbsp;&nbsp;";
                                    if (ds.Tables[0].Rows[i]["Office_ID"].ToString() == ViewState["Office_ID"].ToString())
                                    {
                                        view_edit_link += "<a class='edit_link'  target='_blank' href='" + dt.Rows[p]["VoucherURL"].ToString() + "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(dt.Rows[p]["VoucherTx_ID"].ToString()) + "&Action=" + APIProcedure.Client_Encrypt("2") + "'><i class='fa fa-edit'></i>Edit</a>&nbsp;&nbsp;";
                                    }
                                }
                                htmlStr.Append("<tr style='background:#e2e2e2 !important;width:100%; font-style:italic; display:none;' class='GroupChildren'>");
                                htmlStr.Append("<td style='width:25%' title='" + dt.Rows[p]["VoucherTx_ID"].ToString() + "'>" + view_edit_link.ToString() + " " + dt.Rows[p]["BillByBillTx_Date"].ToString() + "</td>");
                                //htmlStr.Append("<td style='width:25%'>" + dt.Rows[p]["BillByBillTx_Date"].ToString() + "</td>");
                                htmlStr.Append("<td style='width:25%'>" + dt.Rows[p]["VoucherTx_Type"].ToString() + "</td>");
                                htmlStr.Append("<td style='width:25%'>" + dt.Rows[p]["VoucherTx_No"].ToString() + "</td>");
                                htmlStr.Append("<td style='width:25%'>" + dt.Rows[p]["BillByBillTx_Amount"].ToString() + "&nbsp;&nbsp;" + dt.Rows[p]["BillByBillTxType"].ToString() + "</td>");
                                htmlStr.Append("</tr>");

                                /*******Start - Items & Narration*******/
                                if (dt.Rows[p]["VoucherTx_Type"].ToString() == "CashSale Voucher" || dt.Rows[p]["VoucherTx_Type"].ToString() == "GSTGoods Purchase" || dt.Rows[p]["VoucherTx_Type"].ToString() == "DebitNote Voucher" || dt.Rows[p]["VoucherTx_Type"].ToString() == "CreditNote Voucher" || dt.Rows[p]["VoucherTx_Type"].ToString() == "CreditSale Voucher")
                                {

                                    //ds22 = objdb.ByProcedure("SpFinRptOutstanding",
                                    //             new string[] { "flag", "VoucherTx_ID" },
                                    //             new string[] { "15", dt.Rows[p]["VoucherTx_ID"].ToString() }, "dataset");


                                    dvVoucher = ds.Tables[2].DefaultView;
                                    dvVoucher.RowFilter = "VoucherTx_ID <= '" + dt.Rows[p]["VoucherTx_ID"].ToString() + "'";
                                    DataTable dt2 = dvVoucher.ToTable();
                                    // if (ds22.Tables.Count > 0 && ds22.Tables[0].Rows.Count > 0)
                                    if (dt2.Rows.Count > 0)
                                    {
                                        //int Count22 = ds22.Tables[0].Rows.Count;
                                        int Count22 = dt2.Rows.Count;
                                        for (int pp = 0; pp < Count22; pp++)
                                        {

                                            htmlStr.Append("<tr style='background:#e2e2e2 !important;width:100%; font-style:italic; display:none; font-size:10px;  font-weight:600;' class='GroupChildren'>");
                                            htmlStr.Append("<td style='width:25%'>" + dt2.Rows[pp]["Quantity"].ToString() + "&nbsp;&nbsp;" + dt2.Rows[pp]["UnitName"].ToString() + "</td>");

                                            htmlStr.Append("<td style='width:25%'>" + dt2.Rows[pp]["ItemName"].ToString() + "</td>");
                                            htmlStr.Append("<td style='width:25%'></td>");
                                            htmlStr.Append("<td style='width:25%'>" + dt2.Rows[pp]["Rate"].ToString() + "&nbsp;/&nbsp;" + dt2.Rows[pp]["UnitName"].ToString() + "</td>");
                                            htmlStr.Append("</tr>");

                                        }
                                    }
                                }
                                /*******END - Items & Narration*******/
                                htmlStr.Append("<tr style='background:#e2e2e2 !important;width:100%; font-style:italic; display:none; font-size:10px;' class='GroupChildren'>");
                                htmlStr.Append("<td style='width:25%' colspan='4'>Narration: " + dt.Rows[p]["VoucherTx_Narration"].ToString() + "</td>");
                                htmlStr.Append("</tr>");

                            }
                        }
                    }
                    /***************/
                    htmlStr.Append("</table></td>");
                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                    htmlStr.Append("<td style='text-align:right'>" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[i]["billbybill_amount"].ToString())) + "</td>");
                    string billbybill_amount_type = "";
                    if (decimal.Parse(ds.Tables[0].Rows[i]["billbybill_amount"].ToString()) < 0)
                    {
                        billbybill_amount_type = "Dr";
                    }
                    else
                    {
                        billbybill_amount_type = "Cr";
                    }
                    htmlStr.Append("<td style='text-align:right'>" + billbybill_amount_type + "</td>");
                    /********PPPPP*******/

                    if (chk0_30.Checked == true)
                    {
                        if (ds.Tables[0].Rows[i]["Days_0_30"].ToString() != "")
                        {
                            string Days_0_30_type = "";
                            if (decimal.Parse(ds.Tables[0].Rows[i]["Days_0_30"].ToString()) < 0)
                            {
                                Days_0_30_type = "Dr";
                            }
                            else
                            {
                                Days_0_30_type = "Cr";
                            }
                            htmlStr.Append("<td>" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[i]["Days_0_30"].ToString())) + " (" + Days_0_30_type + ")</td>");
                            totalBillAmount_Days_0_30 += decimal.Parse(ds.Tables[0].Rows[i]["Days_0_30"].ToString());
                        }
                        else
                        {
                            htmlStr.Append("<td></td>");
                        }

                    }
                    if (chk30_60.Checked == true)
                    {
                        if (ds.Tables[0].Rows[i]["Days_30_60"].ToString() != "")
                        {
                            string Days_30_60_type = "";
                            if (decimal.Parse(ds.Tables[0].Rows[i]["Days_30_60"].ToString()) < 0)
                            {
                                Days_30_60_type = "Dr";
                            }
                            else
                            {
                                Days_30_60_type = "Cr";
                            }
                            htmlStr.Append("<td>" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[i]["Days_30_60"].ToString())) + " (" + Days_30_60_type + ")</td>");
                            totalBillAmount_Days_30_60 += decimal.Parse(ds.Tables[0].Rows[i]["Days_30_60"].ToString());
                        }
                        else
                        {
                            htmlStr.Append("<td></td>");
                        }
                    }
                    if (chk60_90.Checked == true)
                    {
                        if (ds.Tables[0].Rows[i]["Days_60_90"].ToString() != "")
                        {
                            string Days_60_90_type = "";
                            if (decimal.Parse(ds.Tables[0].Rows[i]["Days_60_90"].ToString()) < 0)
                            {
                                Days_60_90_type = "Dr";
                            }
                            else
                            {
                                Days_60_90_type = "Cr";
                            }
                            htmlStr.Append("<td>" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[i]["Days_60_90"].ToString())) + " (" + Days_60_90_type + ")</td>");
                            totalBillAmount_Days_60_90 += decimal.Parse(ds.Tables[0].Rows[i]["Days_60_90"].ToString());
                        }
                        else
                        {
                            htmlStr.Append("<td></td>");
                        }
                    }
                    if (chk90_180.Checked == true)
                    {
                        if (ds.Tables[0].Rows[i]["Days_90_180"].ToString() != "")
                        {
                            string Days_90_180_type = "";
                            if (decimal.Parse(ds.Tables[0].Rows[i]["Days_90_180"].ToString()) < 0)
                            {
                                Days_90_180_type = "Dr";
                            }
                            else
                            {
                                Days_90_180_type = "Cr";
                            }
                            htmlStr.Append("<td>" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[i]["Days_90_180"].ToString())) + " (" + Days_90_180_type + ")</td>");
                            totalBillAmount_Days_90_180 += decimal.Parse(ds.Tables[0].Rows[i]["Days_90_180"].ToString());
                        }
                        else
                        {
                            htmlStr.Append("<td></td>");
                        }

                    }
                    if (chk180_365.Checked == true)
                    {
                        if (ds.Tables[0].Rows[i]["Days_180_365"].ToString() != "")
                        {
                            string Days_180_365_type = "";
                            if (decimal.Parse(ds.Tables[0].Rows[i]["Days_180_365"].ToString()) < 0)
                            {
                                Days_180_365_type = "Dr";
                            }
                            else
                            {
                                Days_180_365_type = "Cr";
                            }
                            htmlStr.Append("<td>" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[i]["Days_180_365"].ToString())) + " (" + Days_180_365_type + ")</td>");
                            totalBillAmount_Days_180_365 += decimal.Parse(ds.Tables[0].Rows[i]["Days_180_365"].ToString());
                        }
                        else
                        {
                            htmlStr.Append("<td></td>");
                        }
                    }
                    if (chk365_0.Checked == true)
                    {
                        if (ds.Tables[0].Rows[i]["Days_365_More"].ToString() != "")
                        {
                            string Days_365_More_type = "";
                            if (decimal.Parse(ds.Tables[0].Rows[i]["Days_365_More"].ToString()) < 0)
                            {
                                Days_365_More_type = "Dr";
                            }
                            else
                            {
                                Days_365_More_type = "Cr";
                            }
                            htmlStr.Append("<td>" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[i]["Days_365_More"].ToString())) + " (" + Days_365_More_type + ")</td>");
                            totalBillAmount_Days_365_More += decimal.Parse(ds.Tables[0].Rows[i]["Days_365_More"].ToString());
                        }
                        else
                        {
                            htmlStr.Append("<td></td>");
                        }
                    }
                    /********PPPP*******/
                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["duedays"].ToString() + "</td>");
                    htmlStr.Append("</tr>");
                    totalBillAmount += decimal.Parse(ds.Tables[0].Rows[i]["billbybill_amount"].ToString());
                }
            }
            else
            {
                htmlStr.Append("<tr>");
                htmlStr.Append("<th colspan='18'>No Record Found</th>");
                htmlStr.Append("</tr>");
            }
            htmlStr.Append("</tbody>");
            htmlStr.Append("<tfoot>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<th>Total: </th>");
            htmlStr.Append("<th></th>");
            htmlStr.Append("<th></th>");
            htmlStr.Append("<th></th>");
            htmlStr.Append("<th style='text-align:right'>" + Math.Abs(totalBillAmount) + "</th>");
            string totalBillAmount_type = "";
            if (totalBillAmount < 0)
            {
                totalBillAmount_type = "Dr";
            }
            else
            {
                totalBillAmount_type = "Cr";
            }
            htmlStr.Append("<th>" + totalBillAmount_type + "</th>");
            if (chk0_30.Checked == true)
            {
                htmlStr.Append("<th>" + Math.Abs(totalBillAmount_Days_0_30));
                if (totalBillAmount < 0)
                {
                    htmlStr.Append("(Dr)");
                }
                else
                {
                    htmlStr.Append("(Cr)");
                }
                htmlStr.Append("</th>");
            }
            if (chk30_60.Checked == true)
            {
                htmlStr.Append("<th>" + Math.Abs(totalBillAmount_Days_30_60));
                if (totalBillAmount < 0)
                {
                    htmlStr.Append("(Dr)");
                }
                else
                {
                    htmlStr.Append("(Cr)");
                }
                htmlStr.Append("</th>");
            }
            if (chk60_90.Checked == true)
            {
                htmlStr.Append("<th>" + Math.Abs(totalBillAmount_Days_60_90));
                if (totalBillAmount < 0)
                {
                    htmlStr.Append("(Dr)");
                }
                else
                {
                    htmlStr.Append("(Cr)");
                }
                htmlStr.Append("</th>");
            }
            if (chk90_180.Checked == true)
            {
                htmlStr.Append("<th>" + Math.Abs(totalBillAmount_Days_90_180));
                if (totalBillAmount < 0)
                {
                    htmlStr.Append("(Dr)");
                }
                else
                {
                    htmlStr.Append("(Cr)");
                }
                htmlStr.Append("</th>");
            }
            if (chk180_365.Checked == true)
            {
                htmlStr.Append("<th>" + Math.Abs(totalBillAmount_Days_180_365));
                if (totalBillAmount < 0)
                {
                    htmlStr.Append("(Dr)");
                }
                else
                {
                    htmlStr.Append("(Cr)");
                }
                htmlStr.Append("</th>");
            }
            if (chk365_0.Checked == true)
            {
                htmlStr.Append("<th>" + Math.Abs(totalBillAmount_Days_365_More));
                if (totalBillAmount < 0)
                {
                    htmlStr.Append("(Dr)");
                }
                else
                {
                    htmlStr.Append("(Cr)");
                }
                htmlStr.Append("</th>");
            }

            htmlStr.Append("<th></th>");
            htmlStr.Append("</tr>");
            htmlStr.Append("</tfoot>");
            htmlStr.Append("</table>");

            DivTable.InnerHtml = htmlStr.ToString();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    public static string GetCurrentFinancialYear()
    {
        int CurrentYear = DateTime.Today.Year;
        int PreviousYear = DateTime.Today.Year - 1;
        int NextYear = DateTime.Today.Year + 1;
        string PreYear = PreviousYear.ToString();
        string NexYear = NextYear.ToString();
        string CurYear = CurrentYear.ToString();
        string FinYear = null;

        if (DateTime.Today.Month > 3)
            FinYear = CurYear + "-" + NexYear;
        else
            FinYear = PreYear + "-" + CurYear;
        return FinYear.Trim();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();

        lblMsg.Text = "";
        DivTable.InnerHtml = "";

        btnHeadExcel.Visible = false;

        string Office = "";
        string OfficeName = "";
        int SerialNo = 0;
        int totalListItem = ddlOffice.Items.Count;
        foreach (ListItem item in ddlOffice.Items)
        {
            if (item.Selected)
            {
                SerialNo++;
                Office += item.Value + ",";
                OfficeName += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
            }
        }

        if (Office != "")
        {
            if (totalListItem == SerialNo)
            {
                OfficeName = "All Offices";
            }
            else if (SerialNo == 0)
            {
                OfficeName = "---Office Not Selected---";
            }
            else
            {
                OfficeName = OfficeName.Remove(OfficeName.Length - 1, 1);
            }
            string headingFirst = "";
            headingFirst = "<p class='text-center' style='font-weight:600'>Outstanding (Bill Wise) <br /> MP State Agro Industries Development Corporation, <br/> [ " + OfficeName + " ] <br />  As On " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";

            lblheadingFirst.Text = headingFirst;

            if (ddlLedger.SelectedIndex > 0)
            {
                FillOutstandingBillWise();

            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span>";
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select atleast one Office.');", true);
        }


    }
    protected void btnHeadExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string ExcelName = "MainHead";
            if (lblHeadName.Text != "")
            {
                ExcelName = lblHeadName.Text;
            }
            string FileName = ExcelName + "_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            //if (GridView3.Rows.Count > 0)
            //{
            //    GridView3.GridLines = GridLines.Both;
            //    GridView3.HeaderStyle.Font.Bold = true;
            //    GridView3.RenderControl(htmltextwrtter);
            //}
            Response.Write(strwritter.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    protected void ddlLedger_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

}
