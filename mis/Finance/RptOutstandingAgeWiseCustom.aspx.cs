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

public partial class mis_Finance_RptOutstandingAgeWiseCustom : System.Web.UI.Page
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
                    divExcel.Visible = false;
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    string FY = GetCurrentFinancialYear();
                    string[] YEAR = FY.Split('-');
                    DateTime FromDate = new DateTime(int.Parse(YEAR[0]), 4, 1);
                    txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    HeadList();
                    FillVoucherDate();
                    FillDropdown();
                    GetCommonSearch();

                    btnHeadExcel.Visible = false;
                    spnAltB.Visible = false;
                    spnAltW.Visible = false;
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);
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

            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            if (Office != "")
            {
                DataSet ds1 = objdb.ByProcedure("SpFinRptOutstanding",
                    new string[] { "flag", "Head_Id" },
                    new string[] { "8", "51" }, "dataset");
                if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                {
                    ddlGroup.DataSource = ds1;
                    ddlGroup.DataTextField = "Head_Name";
                    ddlGroup.DataValueField = "Head_ID";
                    ddlGroup.DataBind();
                    ddlGroup.Items.Insert(0, new ListItem("Select", "0"));
                    ddlGroup.Items.Insert(1, new ListItem("Sundry Creditors", "51"));

                }
                ddlBillType.SelectedValue = "Cr";
                ddlBillType.Enabled = false;
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
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOutstandingBillWise(string Head_ID)
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


            
            string strAgeingList = hdnAgeingList.Value;
            string[] AgeingListvalues = strAgeingList.Split(',');

            string[] AgeingColumns = new string[((AgeingListvalues.Length)/2)+1];
            decimal[] AgeingTotalAgewise = new decimal[((AgeingListvalues.Length) / 2) + 1];
            int y=0;

            //string AgeingQuery = "SELECT *";
            //for (int i=0; i <= AgeingListvalues.Length - 1; i++)
            //{                    
            //    if(i%2!=0){                   
            //        AgeingQuery += ",(CASE WHEN BillByBillTx_Date BETWEEN DATEADD(DD,-" + AgeingListvalues[i] + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "') AND DATEADD(DD,-" + (AgeingListvalues[i - 1]+1) + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "')  THEN billbybill_amount END) as Days_" + AgeingListvalues[i - 1] + "_" + AgeingListvalues[i] + "";
            //        AgeingColumns[y] = "Days_" + AgeingListvalues[i - 1] + "_" + AgeingListvalues[i];
            //        y++;
            //    }else{
            //    }
                
            //}
            //AgeingColumns[y] = "Days_Olderthan_" + AgeingListvalues[AgeingListvalues.Length - 1];
            //AgeingQuery += ",(CASE WHEN BillByBillTx_Date <=  DATEADD(DD,-" + AgeingListvalues[AgeingListvalues.Length - 1] + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "') THEN billbybill_amount END) as Days_Olderthan_" + AgeingListvalues[AgeingListvalues.Length - 1];
            //AgeingQuery += " FROM #bybf2199 ORDER BY duedays DESC";


            if (ddlBillType.SelectedValue.ToString() == "Cr")
            {
                /*************/
                string AgeingQuery = "SELECT *";
                for (int i = 0; i <= AgeingListvalues.Length - 1; i++)
                {
                    if (i % 2 != 0)
                    {
                        AgeingQuery += ",(CASE WHEN BillByBillTx_Date BETWEEN DATEADD(DD,-" + AgeingListvalues[i] + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "') AND DATEADD(DD,-" + (AgeingListvalues[i - 1]) + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "')  THEN billbybill_amount END) as Days_" + AgeingListvalues[i - 1] + "_" + AgeingListvalues[i] + "";
                        AgeingColumns[y] = "Days_" + AgeingListvalues[i - 1] + "_" + AgeingListvalues[i];
                        y++;
                    }
                    else
                    {
                    }

                }
                AgeingColumns[y] = "Days_Olderthan_" + AgeingListvalues[AgeingListvalues.Length - 1];
                AgeingQuery += ",(CASE WHEN BillByBillTx_Date <=  DATEADD(DD,-" + AgeingListvalues[AgeingListvalues.Length - 1] + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "') THEN billbybill_amount END) as Days_Olderthan_" + AgeingListvalues[AgeingListvalues.Length - 1];
               // AgeingQuery += " FROM #bybf2199 ORDER BY duedays DESC";
                if (chk0TxnOpen.Checked == true)
                {
                    AgeingQuery += " FROM #bybf2199 ORDER BY duedays DESC";
                }
                else
                {
                    AgeingQuery += " FROM #bybf2199  WHERE ISNULL(billbybill_amount,0) !=0 ORDER BY duedays DESC";
                }
                /************/
                ds = objdb.ByProcedure("SpFinRptOutstandingCustom",
                   new string[] { "flag", "Office_ID_Mlt", "ToDate", "FromDate", "Head_ID", "BillType","AgeingQuery" },
                   new string[] { "9", Office, Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Head_ID, ddlBillType.SelectedValue.ToString(), AgeingQuery.ToString() }, "dataset");
            }
            else if (ddlBillType.SelectedValue.ToString() == "Dr")
            {
                /*******************/
                string AgeingQuery = "SELECT *";
                for (int i = 0; i <= AgeingListvalues.Length - 1; i++)
                {
                    if (i % 2 != 0)
                    {
                        AgeingQuery += ",(CASE WHEN BillByBillTx_Date BETWEEN DATEADD(DD,-" + AgeingListvalues[i] + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "') AND DATEADD(DD,-" + (AgeingListvalues[i - 1]) + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "')  THEN billbybill_amount END) as Days_" + AgeingListvalues[i - 1] + "_" + AgeingListvalues[i] + "";
                        AgeingColumns[y] = "Days_" + AgeingListvalues[i - 1] + "_" + AgeingListvalues[i];
                        y++;
                    }
                    else
                    {
                    }

                }
                AgeingColumns[y] = "Days_Olderthan_" + AgeingListvalues[AgeingListvalues.Length - 1];
                AgeingQuery += ",(CASE WHEN BillByBillTx_Date <=  DATEADD(DD,-" + AgeingListvalues[AgeingListvalues.Length - 1] + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "') THEN billbybill_amount END) as Days_Olderthan_" + AgeingListvalues[AgeingListvalues.Length - 1];
                //AgeingQuery += " FROM #bybf211010 ORDER BY duedays DESC";
                if (chk0TxnOpen.Checked == true)
                {
                    AgeingQuery += " FROM #bybf211010 ORDER BY duedays DESC";
                }
                else
                {
                    AgeingQuery += " FROM #bybf211010  WHERE ISNULL(billbybill_amount,0) !=0  ORDER BY duedays DESC";
                   
                }
                /*******************/
                ds = objdb.ByProcedure("SpFinRptOutstandingCustom",
                   new string[] { "flag", "Office_ID_Mlt", "ToDate", "FromDate", "Head_ID", "BillType", "AgeingQuery" },
                   new string[] { "10", Office, Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Head_ID, ddlBillType.SelectedValue.ToString(), AgeingQuery.ToString() }, "dataset");
            }
            else if (ddlBillType.SelectedValue.ToString() == "AllBill")
            {
                /*******************/
                string AgeingQuery = "SELECT *";
                for (int i = 0; i <= AgeingListvalues.Length - 1; i++)
                {
                    if (i % 2 != 0)
                    {
                        AgeingQuery += ",(CASE WHEN BillByBillTx_Date BETWEEN DATEADD(DD,-" + AgeingListvalues[i] + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "') AND DATEADD(DD,-" + (AgeingListvalues[i - 1]) + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "')  THEN billbybill_amount END) as Days_" + AgeingListvalues[i - 1] + "_" + AgeingListvalues[i] + "";
                        AgeingColumns[y] = "Days_" + AgeingListvalues[i - 1] + "_" + AgeingListvalues[i];
                        y++;
                    }
                    else
                    {
                    }

                }
                AgeingColumns[y] = "Days_Olderthan_" + AgeingListvalues[AgeingListvalues.Length - 1];
                AgeingQuery += ",(CASE WHEN BillByBillTx_Date <=  DATEADD(DD,-" + AgeingListvalues[AgeingListvalues.Length - 1] + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "') THEN billbybill_amount END) as Days_Olderthan_" + AgeingListvalues[AgeingListvalues.Length - 1];
                //AgeingQuery += " FROM #bybf211111 ORDER BY duedays DESC";
                if (chk0TxnOpen.Checked == true)
                {
                    AgeingQuery += " FROM #bybf211111 ORDER BY duedays DESC";
                }
                else
                {
                    AgeingQuery += " FROM #bybf211111  WHERE ISNULL(billbybill_amount,0) !=0  ORDER BY duedays DESC";
                }
                /*******************/
                ds = objdb.ByProcedure("SpFinRptOutstandingCustom",
                   new string[] { "flag", "Office_ID_Mlt", "ToDate", "FromDate", "Head_ID", "BillType", "AgeingQuery" },
                   new string[] { "11", Office, Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Head_ID, ddlBillType.SelectedValue.ToString(), AgeingQuery.ToString() }, "dataset");
            }

            StringBuilder htmlStr = new StringBuilder();
            decimal totalBillAmount = 0;
            htmlStr.Append("<table  id='DetailGrid' class='table table-bordered table-hover GridView2 datatable' style='font-family:verdana; font-size:12px; width:100%'>");
            htmlStr.Append("<thead>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<th style='width:120px !important;'>Date</th>");
            htmlStr.Append("<th style='min-width:350px !important;'>Ref. No.</th>");
            htmlStr.Append("<th style='min-width:150px !important;'>Party Name</th>");
            htmlStr.Append("<th>Office Name</th>");
            htmlStr.Append("<th>Pending Amount (INR)</th>");
            htmlStr.Append("<th>Dr/Cr</th>");
            htmlStr.Append("<th>OverDue By Days</th>");
            for (int pp = 0; pp < AgeingColumns.Length; pp++)
            {
                htmlStr.Append("<th>" + AgeingColumns[pp].ToString() + "</th>");
            }
            htmlStr.Append("</tr>");
            htmlStr.Append("</thead>");
            htmlStr.Append("<tbody>");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                spnAltW.Visible = true;
                divExcel.Visible = true;
                int Count = ds.Tables[0].Rows.Count;
                int ColumnCount = ds.Tables[0].Columns.Count;
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<tr style='background: #79554840 !important;  class='GroupHeading'>");
                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["BillByBillTx_Date2"].ToString() + "<br/><i class='fa fa-plus explode'></i> &nbsp;&nbsp; <i class='fa fa-minus implode'></i></td>");            
                    htmlStr.Append("<td style='padding:0px'><table style='width:100%'><tr><td>" + ds.Tables[0].Rows[i]["BillByBillTx_Ref"].ToString() + "</td>");
                    /***************/
                    htmlStr.Append("<table><tr style='background:#e2e2e2 !important;width:100%; font-style:italic; display:none;' class='GroupChildren'><td colspan='4'>" + ds.Tables[0].Rows[i]["VoucherDetail"].ToString() + "</td></tr>");
                    //*****//
                    int TotalEntry = Int32.Parse(ds.Tables[0].Rows[i]["TotalEntry"].ToString());
                    if (TotalEntry > 1)
                    {

                        ds2 = objdb.ByProcedure("SpFinRptOutstanding",
                                               new string[] { "flag", "Office_ID_Mlt", "ToDate", "FromDate", "Ledger_ID", "BillByBillTx_Ref" },
                                               new string[] { "2", Office, Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), ds.Tables[0].Rows[i]["Ledger_ID"].ToString(), ds.Tables[0].Rows[i]["BillByBillTx_Ref"].ToString() }, "dataset");

                        if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                        {
                            int Count2 = ds2.Tables[0].Rows.Count;
                            for (int p = 0; p < Count2; p++)
                            {
                                string view_edit_link = "<a class='view_link' target='_blank' href='" + ds2.Tables[0].Rows[p]["VoucherURL"].ToString() + "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(ds2.Tables[0].Rows[p]["VoucherTx_ID"].ToString()) + "&Action=" + APIProcedure.Client_Encrypt("1") + "'><i class='fa fa-eye'></i>View</a>&nbsp;&nbsp;";
                                if (ds.Tables[0].Rows[i]["Office_ID"].ToString() == ViewState["Office_ID"].ToString())
                                {
                                    view_edit_link += "<a class='edit_link'  target='_blank' href='" + ds2.Tables[0].Rows[p]["VoucherURL"].ToString() + "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(ds2.Tables[0].Rows[p]["VoucherTx_ID"].ToString()) + "&Action=" + APIProcedure.Client_Encrypt("2") + "'><i class='fa fa-edit'></i>Edit</a>&nbsp;&nbsp;";
                                }

                                htmlStr.Append("<tr style='background:#e2e2e2 !important;width:100%; font-style:italic; display:none;' class='GroupChildren'>");
                                htmlStr.Append("<td style='width:25%' title='" + ds2.Tables[0].Rows[p]["VoucherTx_ID"].ToString() + "'>" + view_edit_link.ToString() + " " + ds2.Tables[0].Rows[p]["BillByBillTx_Date"].ToString() + "</td>");
                                htmlStr.Append("<td style='width:25%'>" + ds2.Tables[0].Rows[p]["VoucherTx_Type"].ToString() + "</td>");
                                htmlStr.Append("<td style='width:25%'>" + ds2.Tables[0].Rows[p]["VoucherTx_No"].ToString() + "</td>");
                                htmlStr.Append("<td style='width:25%'>" + ds2.Tables[0].Rows[p]["BillByBillTx_Amount"].ToString() + "&nbsp;&nbsp;" + ds2.Tables[0].Rows[p]["BillByBillTxType"].ToString() + "</td>");
                                htmlStr.Append("</tr>");

                                /*******Start - Items & Narration*******/
                                if (ds2.Tables[0].Rows[p]["VoucherTx_Type"].ToString() == "CashSale Voucher" || ds2.Tables[0].Rows[p]["VoucherTx_Type"].ToString() == "GSTGoods Purchase" || ds2.Tables[0].Rows[p]["VoucherTx_Type"].ToString() == "DebitNote Voucher" || ds2.Tables[0].Rows[p]["VoucherTx_Type"].ToString() == "CreditNote Voucher" || ds2.Tables[0].Rows[p]["VoucherTx_Type"].ToString() == "CreditSale Voucher")
                                {

                                    ds22 = objdb.ByProcedure("SpFinRptOutstanding",
                                                 new string[] { "flag", "VoucherTx_ID" },
                                                 new string[] { "15", ds2.Tables[0].Rows[p]["VoucherTx_ID"].ToString() }, "dataset");

                                    if (ds22.Tables.Count > 0 && ds22.Tables[0].Rows.Count > 0)
                                    {
                                        int Count22 = ds22.Tables[0].Rows.Count;
                                        for (int pp = 0; pp < Count22; pp++)
                                        {

                                            htmlStr.Append("<tr style='background:#e2e2e2 !important;width:100%; font-style:italic; display:none; font-size:10px;  font-weight:600;' class='GroupChildren'>");
                                            htmlStr.Append("<td style='width:25%'>" + ds22.Tables[0].Rows[pp]["Quantity"].ToString() + "&nbsp;&nbsp;" + ds22.Tables[0].Rows[pp]["UnitName"].ToString() + "</td>");

                                            htmlStr.Append("<td style='width:25%'>" + ds22.Tables[0].Rows[pp]["ItemName"].ToString() + "</td>");
                                            htmlStr.Append("<td style='width:25%'></td>");
                                            htmlStr.Append("<td style='width:25%'>" + ds22.Tables[0].Rows[pp]["Rate"].ToString() + "&nbsp;/&nbsp;" + ds22.Tables[0].Rows[pp]["UnitName"].ToString() + "</td>");
                                            htmlStr.Append("</tr>");

                                        }
                                    }
                                }
                                /*******END - Items & Narration*******/

                                htmlStr.Append("<tr style='background:#e2e2e2 !important;width:100%; font-style:italic; display:none; font-size:10px;' class='GroupChildren'>");
                                htmlStr.Append("<td style='width:25%' colspan='4'>Narration: " + ds2.Tables[0].Rows[p]["VoucherTx_Narration"].ToString() + "</td>");
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
                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["duedays"].ToString() + "</td>");

                    /**********************/
                    
                    for (int k = 15; k < ColumnCount; k++)
                    {
                        htmlStr.Append("<td>");
                        htmlStr.Append("" + ds.Tables[0].Rows[i][k].ToString() + "");
                        htmlStr.Append("</td>");
                        if (ds.Tables[0].Rows[i][k].ToString()!="")
                        {
                            AgeingTotalAgewise[k - 15] += decimal.Parse(ds.Tables[0].Rows[i][k].ToString());
                        }
                        
                    }
                    
                    /**********************/
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
            htmlStr.Append("<th></th>");
            for (int qq = 0; qq < AgeingColumns.Length; qq++)
            {
                htmlStr.Append("<th>" + AgeingTotalAgewise[qq].ToString() + "</th>");
                
            }
            //for (int pp = 0; pp < AgeingColumns.Length; pp++)
            //{
            //    htmlStr.Append("<th>" + AgeingColumns[pp].ToString() + "</th>");AgeingTotalAgewise
            //}
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

    protected void FillOutstandingBillPartyWise(string Head_ID)
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

            string strAgeingList = hdnAgeingList.Value;
            string[] AgeingListvalues = strAgeingList.Split(',');

            string[] AgeingColumns = new string[((AgeingListvalues.Length) / 2) + 1];
            decimal[] AgeingTotalAgewise = new decimal[((AgeingListvalues.Length) / 2) + 1];
            int y = 0;

            if (ddlBillType.SelectedValue.ToString() == "Cr")
            {
                /*************/
                string AgeingQuery = "SELECT *";
                string AgeingSubQuery = "";

                for (int i = 0; i <= AgeingListvalues.Length - 1; i++)
                {
                    if (i % 2 != 0)
                    {
                        AgeingQuery += ",(CASE WHEN BillByBillTx_Date BETWEEN DATEADD(DD,-" + AgeingListvalues[i] + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "') AND DATEADD(DD,-" + (AgeingListvalues[i - 1]) + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "')  THEN billbybill_amount END) as Days_" + AgeingListvalues[i - 1] + "_" + AgeingListvalues[i] + "";
                        AgeingSubQuery += " ,0 as Days_" + AgeingListvalues[i - 1] + "_" + AgeingListvalues[i] + "";
                        AgeingColumns[y] = "Days_" + AgeingListvalues[i - 1] + "_" + AgeingListvalues[i];
                        y++;
                    }
                    else
                    {
                    }

                }

                AgeingColumns[y] = "Days_Olderthan_" + AgeingListvalues[AgeingListvalues.Length - 1];
                AgeingQuery += ",(CASE WHEN BillByBillTx_Date <=  DATEADD(DD,-" + AgeingListvalues[AgeingListvalues.Length - 1] + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "') THEN billbybill_amount END) as Days_Olderthan_" + AgeingListvalues[AgeingListvalues.Length - 1];
                AgeingSubQuery += ",0 as Days_Olderthan_" + AgeingListvalues[AgeingListvalues.Length - 1];


                AgeingQuery += " FROM #bybf211212";
                if (chk0TxnOpen.Checked == false)
                {
                    AgeingQuery += "  WHERE ISNULL(billbybill_amount,0) !=0  ";
                }
                AgeingQuery += " union all SELECT 0 as BillByBillTx_ID,0 as VoucherTx_ID,'-' as BillByBillTx_Ref,0 as BillByBillTx_Amount,0 as TotalEntry,billbybill_amount,'-' as BillByBillTx_Date,'-' as Ledger_ID,'-' as old_BillByBillTx_Ref,'-' as Office_Name,'-' as Office_ID,Ledger_Name,'-' as duedays,'-' as VoucherDetail";

                AgeingQuery += AgeingSubQuery.ToString();	

                //AgeingQuery += " FROM #distinctLedger1212 ORDER BY Ledger_Name asc, Ledger_ID asc,duedays DESC";
                AgeingQuery += " FROM #distinctLedger1212 ";
                if (chk0TxnOpen.Checked == false)
                {
                    AgeingQuery += "  WHERE ISNULL(billbybill_amount,0) !=0  ";
                }
                AgeingQuery += " ORDER BY Ledger_Name asc, Ledger_ID asc,duedays DESC";
		
                
                /************/
                ds = objdb.ByProcedure("SpFinRptOutstandingCustom",
                   new string[] { "flag", "Office_ID_Mlt", "ToDate", "FromDate", "Head_ID","AgeingQuery" },
                   new string[] { "12", Office, Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Head_ID, AgeingQuery.ToString() }, "dataset");
            }
            else if (ddlBillType.SelectedValue.ToString() == "Dr")
            {
                /*************/
                string AgeingQuery = "SELECT *";
                string AgeingSubQuery = "";

                for (int i = 0; i <= AgeingListvalues.Length - 1; i++)
                {
                    if (i % 2 != 0)
                    {
                        AgeingQuery += ",(CASE WHEN BillByBillTx_Date BETWEEN DATEADD(DD,-" + AgeingListvalues[i] + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "') AND DATEADD(DD,-" + (AgeingListvalues[i - 1]) + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "')  THEN billbybill_amount END) as Days_" + AgeingListvalues[i - 1] + "_" + AgeingListvalues[i] + "";
                        AgeingSubQuery += " ,0 as Days_" + AgeingListvalues[i - 1] + "_" + AgeingListvalues[i] + "";
                        AgeingColumns[y] = "Days_" + AgeingListvalues[i - 1] + "_" + AgeingListvalues[i];
                        y++;
                    }
                    else
                    {
                    }

                }

                AgeingColumns[y] = "Days_Olderthan_" + AgeingListvalues[AgeingListvalues.Length - 1];
                AgeingQuery += ",(CASE WHEN BillByBillTx_Date <=  DATEADD(DD,-" + AgeingListvalues[AgeingListvalues.Length - 1] + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "') THEN billbybill_amount END) as Days_Olderthan_" + AgeingListvalues[AgeingListvalues.Length - 1];
                AgeingSubQuery += ",0 as Days_Olderthan_" + AgeingListvalues[AgeingListvalues.Length - 1];


                AgeingQuery += " FROM #bybf211313";
                if (chk0TxnOpen.Checked == false)
                {
                    AgeingQuery += "  WHERE ISNULL(billbybill_amount,0) !=0  ";
                }
                AgeingQuery += " union all SELECT 0 as BillByBillTx_ID,0 as VoucherTx_ID,'-' as BillByBillTx_Ref,0 as BillByBillTx_Amount,0 as TotalEntry,billbybill_amount,'-' as BillByBillTx_Date,'-' as Ledger_ID,'-' as old_BillByBillTx_Ref,'-' as Office_Name,'-' as Office_ID,Ledger_Name,'-' as duedays,'-' as VoucherDetail";

                AgeingQuery += AgeingSubQuery.ToString();

                //AgeingQuery += " FROM #distinctLedger1313 ORDER BY Ledger_Name asc, Ledger_ID asc,duedays DESC";
                AgeingQuery += " FROM #distinctLedger1313 ";
                if (chk0TxnOpen.Checked == false)
                {
                    AgeingQuery += "  WHERE ISNULL(billbybill_amount,0) !=0  ";
                }
                AgeingQuery += " ORDER BY Ledger_Name asc, Ledger_ID asc,duedays DESC";
                /************/
                ds = objdb.ByProcedure("SpFinRptOutstandingCustom",
                   new string[] { "flag", "Office_ID_Mlt", "ToDate", "FromDate", "Head_ID", "AgeingQuery" },
                   new string[] { "13", Office, Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Head_ID, AgeingQuery.ToString() }, "dataset");
            }
            else if (ddlBillType.SelectedValue.ToString() == "AllBill")
            {
                /*************/
                string AgeingQuery = "SELECT *";
                string AgeingSubQuery = "";

                for (int i = 0; i <= AgeingListvalues.Length - 1; i++)
                {
                    if (i % 2 != 0)
                    {
                        AgeingQuery += ",(CASE WHEN BillByBillTx_Date BETWEEN DATEADD(DD,-" + AgeingListvalues[i] + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "') AND DATEADD(DD,-" + (AgeingListvalues[i - 1]) + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "')  THEN billbybill_amount END) as Days_" + AgeingListvalues[i - 1] + "_" + AgeingListvalues[i] + "";
                        AgeingSubQuery += " ,0 as Days_" + AgeingListvalues[i - 1] + "_" + AgeingListvalues[i] + "";
                        AgeingColumns[y] = "Days_" + AgeingListvalues[i - 1] + "_" + AgeingListvalues[i];
                        y++;
                    }
                    else
                    {
                    }

                }

                AgeingColumns[y] = "Days_Olderthan_" + AgeingListvalues[AgeingListvalues.Length - 1];
                AgeingQuery += ",(CASE WHEN BillByBillTx_Date <=  DATEADD(DD,-" + AgeingListvalues[AgeingListvalues.Length - 1] + ",'" + Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") + "') THEN billbybill_amount END) as Days_Olderthan_" + AgeingListvalues[AgeingListvalues.Length - 1];
                AgeingSubQuery += ",0 as Days_Olderthan_" + AgeingListvalues[AgeingListvalues.Length - 1];


                AgeingQuery += " FROM #bybf211414";
                AgeingQuery += " union all SELECT 0 as BillByBillTx_ID,0 as VoucherTx_ID,'-' as BillByBillTx_Ref,0 as BillByBillTx_Amount,0 as TotalEntry,billbybill_amount,'-' as BillByBillTx_Date,'-' as Ledger_ID,'-' as old_BillByBillTx_Ref,'-' as Office_Name,'-' as Office_ID,Ledger_Name,'-' as duedays,'-' as VoucherDetail";

                AgeingQuery += AgeingSubQuery.ToString();

                AgeingQuery += " FROM #distinctLedger1414 ORDER BY Ledger_Name asc, Ledger_ID asc,duedays DESC";


                /************/
                ds = objdb.ByProcedure("SpFinRptOutstandingCustom",
                   new string[] { "flag", "Office_ID_Mlt", "ToDate", "FromDate", "Head_ID", "AgeingQuery" },
                   new string[] { "14", Office, Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Head_ID, AgeingQuery.ToString() }, "dataset");
            }

            StringBuilder htmlStr = new StringBuilder();
            decimal totalBillAmount = 0;
            htmlStr.Append("<table  id='DetailGrid' class='table table-bordered table-hover GridView2 datatable' style='font-family:verdana; font-size:12px;'>");
            htmlStr.Append("<thead>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<th style='width:120px !important;'>Date</th>");
            htmlStr.Append("<th style='min-width:350px !important;'>Ref. No.</th>");
            htmlStr.Append("<th style='min-width:350px !important;'>Party Name</th>");
            htmlStr.Append("<th>Office Name</th>");
            htmlStr.Append("<th>Pending Amount (INR)</th>");
            htmlStr.Append("<th>Dr/Cr</th>");
            htmlStr.Append("<th>OverDue By Days</th>");
            for (int pp = 0; pp < AgeingColumns.Length; pp++)
            {
                htmlStr.Append("<th>" + AgeingColumns[pp].ToString() + "</th>");
            }
            htmlStr.Append("</tr>");
            htmlStr.Append("</thead>");
            htmlStr.Append("<tbody>");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                spnAltW.Visible = true;
                divExcel.Visible = true;
                int Count = ds.Tables[0].Rows.Count;
                int ColumnCount = ds.Tables[0].Columns.Count;
                for (int i = 0; i < Count; i++)
                {
                    string billbybill_amount_type = "";
                    if (decimal.Parse(ds.Tables[0].Rows[i]["billbybill_amount"].ToString()) < 0)
                    {
                        billbybill_amount_type = "Dr";
                    }
                    else
                    {
                        billbybill_amount_type = "Cr";
                    }

                    if (ds.Tables[0].Rows[i]["TotalEntry"].ToString() == "0")
                    {
                        htmlStr.Append("<tr style='background: #9e9e9e63 !important; font-weight:900;' class='GroupHeading'>");
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td></td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
                        htmlStr.Append("<td></td>");


                        htmlStr.Append("<td style='text-align:right'>" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[i]["billbybill_amount"].ToString())) + "</td>");

                        htmlStr.Append("<td>" + billbybill_amount_type + "</td>");
                        htmlStr.Append("<td></td>");
                        for (int k = 14; k < ColumnCount; k++)
                        {
                            htmlStr.Append("<td></td>");
                        }
                        htmlStr.Append("</tr>");
                    }
                    else
                    {
                        htmlStr.Append("<tr style='background: #79554840 !important;  class='GroupHeading'>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["BillByBillTx_Date"].ToString() + "<br/><i class='fa fa-plus explode'></i> &nbsp;&nbsp; <i class='fa fa-minus implode'></i></td>");
                        //htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["BillByBillTx_Date"].ToString() + "</td>");
                        htmlStr.Append("<td style='padding:0px'><table style='width:100%'><tr><td>" + ds.Tables[0].Rows[i]["BillByBillTx_Ref"].ToString() + "</td>");
                        htmlStr.Append("<table><tr style='background:#e2e2e2 !important;width:100%; font-style:italic; display:none;' class='GroupChildren'><td colspan='4'>" + ds.Tables[0].Rows[i]["VoucherDetail"].ToString() + "</td></tr>");
                        int TotalEntry = Int32.Parse(ds.Tables[0].Rows[i]["TotalEntry"].ToString());
                        if (TotalEntry > 1)
                        {
                            ds2 = objdb.ByProcedure("SpFinRptOutstanding",
                                                   new string[] { "flag", "Office_ID_Mlt", "ToDate", "FromDate", "Ledger_ID", "BillByBillTx_Ref" },
                                                   new string[] { "2", Office, Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), ds.Tables[0].Rows[i]["Ledger_ID"].ToString(), ds.Tables[0].Rows[i]["BillByBillTx_Ref"].ToString() }, "dataset");

                            if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                            {
                                int Count2 = ds2.Tables[0].Rows.Count;
                                for (int p = 0; p < Count2; p++)
                                {
                                    string view_edit_link = "<a class='view_link' target='_blank' href='" + ds2.Tables[0].Rows[p]["VoucherURL"].ToString() + "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(ds2.Tables[0].Rows[p]["VoucherTx_ID"].ToString()) + "&Action=" + APIProcedure.Client_Encrypt("1") + "'><i class='fa fa-eye'></i>View</a>&nbsp;&nbsp;";
                                    if (ds.Tables[0].Rows[i]["Office_ID"].ToString() == ViewState["Office_ID"].ToString())
                                    {
                                        view_edit_link += "<a class='edit_link'  target='_blank' href='" + ds2.Tables[0].Rows[p]["VoucherURL"].ToString() + "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(ds2.Tables[0].Rows[p]["VoucherTx_ID"].ToString()) + "&Action=" + APIProcedure.Client_Encrypt("2") + "'><i class='fa fa-edit'></i>Edit</a>&nbsp;&nbsp;";
                                    }


                                    htmlStr.Append("<tr style='background:#e2e2e2 !important;width:100%; font-style:italic; display:none;' class='GroupChildren'>");
                                    htmlStr.Append("<td style='width:25%' title='" + ds2.Tables[0].Rows[p]["VoucherTx_ID"].ToString() + "'>" + view_edit_link.ToString() + " " + ds2.Tables[0].Rows[p]["BillByBillTx_Date"].ToString() + "</td>");
                                    //htmlStr.Append("<td style='width:25%'>" + ds2.Tables[0].Rows[p]["BillByBillTx_Date"].ToString() + "</td>");
                                    htmlStr.Append("<td style='width:25%'>" + ds2.Tables[0].Rows[p]["VoucherTx_Type"].ToString() + "</td>");
                                    htmlStr.Append("<td style='width:25%'>" + ds2.Tables[0].Rows[p]["VoucherTx_No"].ToString() + "</td>");
                                    htmlStr.Append("<td style='width:25%'>" + ds2.Tables[0].Rows[p]["BillByBillTx_Amount"].ToString() + "&nbsp;&nbsp;" + ds2.Tables[0].Rows[p]["BillByBillTxType"].ToString() + "</td>");
                                    htmlStr.Append("</tr>");

                                    /*******Start - Items & Narration*******/
                                    if (ds2.Tables[0].Rows[p]["VoucherTx_Type"].ToString() == "CashSale Voucher" || ds2.Tables[0].Rows[p]["VoucherTx_Type"].ToString() == "GSTGoods Purchase" || ds2.Tables[0].Rows[p]["VoucherTx_Type"].ToString() == "DebitNote Voucher" || ds2.Tables[0].Rows[p]["VoucherTx_Type"].ToString() == "CreditNote Voucher" || ds2.Tables[0].Rows[p]["VoucherTx_Type"].ToString() == "CreditSale Voucher")
                                    {

                                        ds22 = objdb.ByProcedure("SpFinRptOutstanding",
                                                     new string[] { "flag", "VoucherTx_ID" },
                                                     new string[] { "15", ds2.Tables[0].Rows[p]["VoucherTx_ID"].ToString() }, "dataset");

                                        if (ds22.Tables.Count > 0 && ds22.Tables[0].Rows.Count > 0)
                                        {
                                            int Count22 = ds22.Tables[0].Rows.Count;
                                            for (int pp = 0; pp < Count22; pp++)
                                            {

                                                htmlStr.Append("<tr style='background:#e2e2e2 !important;width:100%; font-style:italic; display:none; font-size:10px;  font-weight:600;' class='GroupChildren'>");
                                                htmlStr.Append("<td style='width:25%'>" + ds22.Tables[0].Rows[pp]["Quantity"].ToString() + "&nbsp;&nbsp;" + ds22.Tables[0].Rows[pp]["UnitName"].ToString() + "</td>");

                                                htmlStr.Append("<td style='width:25%'>" + ds22.Tables[0].Rows[pp]["ItemName"].ToString() + "</td>");
                                                htmlStr.Append("<td style='width:25%'></td>");
                                                htmlStr.Append("<td style='width:25%'>" + ds22.Tables[0].Rows[pp]["Rate"].ToString() + "&nbsp;/&nbsp;" + ds22.Tables[0].Rows[pp]["UnitName"].ToString() + "</td>");
                                                htmlStr.Append("</tr>");

                                            }
                                        }
                                    }
                                    /*******END - Items & Narration*******/
                                    htmlStr.Append("<tr style='background:#e2e2e2 !important;width:100%; font-style:italic; display:none; font-size:10px;' class='GroupChildren'>");
                                    htmlStr.Append("<td style='width:25%' colspan='4'>Narration: " + ds2.Tables[0].Rows[p]["VoucherTx_Narration"].ToString() + "</td>");
                                    htmlStr.Append("</tr>");
                                }
                            }
                        }
                        htmlStr.Append("</table></td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                        htmlStr.Append("<td style='text-align:right'>" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[i]["billbybill_amount"].ToString())) + "</td>");

                        htmlStr.Append("<td style='text-align:right'>" + billbybill_amount_type + "</td>");
                        
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["duedays"].ToString() + "</td>");
                        /**********************/

                        for (int k = 14; k < ColumnCount; k++)
                        {
                            htmlStr.Append("<td>");
                            htmlStr.Append("" + ds.Tables[0].Rows[i][k].ToString() + "");
                            htmlStr.Append("</td>");
                            if (ds.Tables[0].Rows[i][k].ToString() != "")
                            {
                                AgeingTotalAgewise[k - 14] += decimal.Parse(ds.Tables[0].Rows[i][k].ToString());
                            }

                        }

                        /**********************/
                        htmlStr.Append("</tr>");
                        totalBillAmount += decimal.Parse(ds.Tables[0].Rows[i]["billbybill_amount"].ToString());
                    }

                }
            }
            else
            {
                htmlStr.Append("<tr>");
                htmlStr.Append("<th colspan='12'>No Record Found</th>");
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
            
            htmlStr.Append("<th></th>");
            for (int qq = 0; qq < AgeingColumns.Length; qq++)
            {
                htmlStr.Append("<th>" + AgeingTotalAgewise[qq].ToString() + "</th>");

            }
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
        hdnCurrentRow.Value = "0";
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

            if (ddlPartywise.SelectedValue.ToString() == "BillWise")
            {
                //headingFirst = "<p class='text-center' style='font-weight:600'>Payable Outstanding (Bill Wise) <br /> M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd, <br/> [ " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
                headingFirst = "<p class='text-center' style='font-weight:600'>Outstanding (Bill Wise) <br /> M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd, <br/> [ " + OfficeName + " ] <br />  As On " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
            }
            else if (ddlPartywise.SelectedValue.ToString() == "BillPartyWise")
            {
                //headingFirst = "<p class='text-center' style='font-weight:600'>Payable Outstanding (Bill Party Wise) <br /> M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd, <br/> [ " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
                headingFirst = "<p class='text-center' style='font-weight:600'>Outstanding (Bill Party Wise) <br /> M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd, <br/> [ " + OfficeName + " ] <br />  As On " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p>";
            }

            lblheadingFirst.Text = headingFirst;

            if (ddlGroup.SelectedIndex > 0)
            {
                /*****************SET Search Option Start********************/
                #region SetSearchOption
                Session["CommonOffice"] = Office;
                Session["CommonFromDate"] = txtFromDate.Text;
                Session["CommonToDate"] = txtToDate.Text;
                #endregion
                /*****************SET Search Option End********************/
                if (ddlPartywise.SelectedValue.ToString() == "BillWise")
                {
                    FillOutstandingBillWise(ddlGroup.SelectedValue.ToString());
                }
                else if (ddlPartywise.SelectedValue.ToString() == "BillPartyWise")
                {
                    FillOutstandingBillPartyWise(ddlGroup.SelectedValue.ToString());
                }

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
    protected void ddlOutstandingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOutstandingType.SelectedValue.ToString() == "Payable")
        {
            DataSet ds1 = objdb.ByProcedure("SpFinRptOutstanding",
                    new string[] { "flag", "Head_Id" },
                    new string[] { "8", "51" }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                ddlGroup.DataSource = ds1;
                ddlGroup.DataTextField = "Head_Name";
                ddlGroup.DataValueField = "Head_ID";
                ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, new ListItem("Select", "0"));
                ddlGroup.Items.Insert(1, new ListItem("Sundry Creditors", "51"));

            }
            ddlBillType.SelectedValue = "Cr";
            ddlBillType.Enabled = false;
        }
        else if (ddlOutstandingType.SelectedValue.ToString() == "Receivable")
        {
            DataSet ds1 = objdb.ByProcedure("SpFinRptOutstanding",
                    new string[] { "flag", "Head_Id" },
                    new string[] { "8", "52" }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                ddlGroup.DataSource = ds1;
                ddlGroup.DataTextField = "Head_Name";
                ddlGroup.DataValueField = "Head_ID";
                ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, new ListItem("Select", "0"));
                ddlGroup.Items.Insert(1, new ListItem("Sundry Debtors", "52"));

            }
            ddlBillType.SelectedValue = "Dr";
            ddlBillType.Enabled = false;
        }
        else if (ddlOutstandingType.SelectedValue.ToString() == "Groups")
        {
            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            DataSet ds1 = objdb.ByProcedure("SpFinRptGroupSummarysNew", new string[] { "flag", "Office_ID_Mlt", "FromDate", "ToDate" }, new string[] { "1", Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                ddlGroup.DataSource = ds1;
                ddlGroup.DataTextField = "Head_Name";
                ddlGroup.DataValueField = "Head_ID";
                ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, new ListItem("Select", "0"));
            }
            ddlBillType.SelectedValue = "AllBill";
            ddlBillType.Enabled = true;
        }
    }
    protected void GetCommonSearch()
    {
        try
        {
            if (Session["CommonOffice"] != null)
            {
                string Office = Session["CommonOffice"].ToString();
                string[] OfficeList = Office.Split(new Char[] { ',' });
                if (OfficeList.Count() > 0)
                {
                    ddlOffice.ClearSelection();
                    foreach (string OfficeID in OfficeList)
                    {
                        // ddlOffice.SelectedValue = OfficeID.ToString();
                        foreach (ListItem item in ddlOffice.Items)
                        {
                            if (item.Value == OfficeID)
                                item.Selected = true;
                        }
                    }
                }

            }
            if (Session["CommonFromDate"] != null)
            {
                string FromDate = Session["CommonFromDate"].ToString();
                txtFromDate.Text = FromDate.ToString();
            }
            if (Session["CommonToDate"] != null)
            {
                string ToDate = Session["CommonToDate"].ToString();
                txtToDate.Text = ToDate.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
   
}
