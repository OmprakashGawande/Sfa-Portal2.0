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
using System.Web.Services;
using System.Web.Script.Services;

public partial class mis_Finance_RptTrialBalanceDetailed : System.Web.UI.Page
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
                    ddlOffice.Enabled = false;

                    string FY = GetCurrentFinancialYear();
                    string[] YEAR = FY.Split('-');
                    DateTime FromDate = new DateTime(int.Parse(YEAR[0]), 4, 1);
                    txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    txtFromDate.Attributes.Add("readonly", "readonly");

                    FillVoucherDate();
                    FillDropdown();
                    GetCommonSearch();
                    GetNotOpenLedgerList();
                }
                //lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt");
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

            ddlOffice.Enabled = false;
            ddlRegionalOffice.Enabled = false;
            divRegionalOffice.Visible = false;
            if (ViewState["OfficeType_Title"].ToString() != "District Office" && ViewState["OfficeType_Title"].ToString() != "Production Unit")
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
                    ddlRegionalOffice.Items.Insert(ds.Tables[0].Rows.Count + 1, new ListItem("Production Unit", "010"));
                    if (ViewState["OfficeType_Title"].ToString() == "Regional Office")
                    {
                        ddlRegionalOffice.Enabled = false;
                        ddlRegionalOffice.SelectedValue = ViewState["Division_ID"].ToString();
                    }
                }
            }

            FillOffice();
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
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string Session = ddlSession.SelectedValue.ToString();
            if (Session == "Quarter 1")
            {
                txtFromDate.Text = "01/04/" + ViewState["CurYear"].ToString();
                txtToDate.Text = "30/06/" + ViewState["CurYear"].ToString();
            }
            else if (Session == "Quarter 2")
            {
                txtFromDate.Text = "01/07/" + ViewState["CurYear"].ToString();
                txtToDate.Text = "30/09/" + ViewState["CurYear"].ToString();
            }
            else if (Session == "Quarter 3")
            {
                txtFromDate.Text = "01/10/" + ViewState["CurYear"].ToString();
                txtToDate.Text = "31/12/" + ViewState["CurYear"].ToString();
            }
            else if (Session == "Quarter 4")
            {
                txtFromDate.Text = "01/01/" + ViewState["NexYear"].ToString();
                txtToDate.Text = "31/03/" + ViewState["NexYear"].ToString();
            }
            else if (Session == "Quarter 1 & 2")
            {
                txtFromDate.Text = "01/04/" + ViewState["CurYear"].ToString();
                txtToDate.Text = "30/09/" + ViewState["CurYear"].ToString();
            }
            else if (Session == "Quarter 2 & 3")
            {
                txtFromDate.Text = "01/07/" + ViewState["CurYear"].ToString();
                txtToDate.Text = "31/12/" + ViewState["CurYear"].ToString();
            }
            else if (Session == "Quarter 3 & 4")
            {
                txtFromDate.Text = "01/10/" + ViewState["CurYear"].ToString();
                txtToDate.Text = "31/03/" + ViewState["NexYear"].ToString();
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

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }



    protected string GetCurrentFinancialYear()
    {
        string VYear = "";
        string VMonth = "";
        ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "6", ViewState["Office_ID"].ToString() }, "dataset");
        if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
        {
            VYear = ds.Tables[0].Rows[0]["Year"].ToString();
            VMonth = ds.Tables[0].Rows[0]["Month"].ToString();
        }
        int CurrentYear = int.Parse(VYear);
        int PreviousYear = CurrentYear - 1;
        int NextYear = CurrentYear + 1;
        string PreYear = PreviousYear.ToString();
        string NexYear = NextYear.ToString();
        string CurYear = CurrentYear.ToString();
        string FinYear = null;

        if (int.Parse(VMonth) > 3)
        {
            FinYear = CurYear + "-" + NexYear;
            ViewState["CurYear"] = CurYear.ToString();
            ViewState["NexYear"] = NexYear.ToString();
        }
        else
        {
            FinYear = PreYear + "-" + CurYear;
            ViewState["CurYear"] = PreYear.ToString();
            ViewState["NexYear"] = CurYear.ToString();
        }

        return FinYear.Trim();
    }

    protected void btnSearchDetailed_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            string Office = "";
            string OfficeName = "";
            int SerialNo = 0;
            DataSet ds1;
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
                /*****************SET Search Option Start********************/
                #region SetSearchOption
                Session["CommonOffice"] = Office;
                Session["CommonFromDate"] = txtFromDate.Text;
                Session["CommonToDate"] = txtToDate.Text;
                #endregion
                /*****************SET Search Option End********************/

                Session["FromDate"] = txtFromDate.Text;
                Session["ToDate"] = txtToDate.Text;
                Session["Office"] = Office;

                Session["ChkOpen"] = "False";
                Session["ChkTxn"] = "False";
                Session["ChkClose"] = "False";
                Session["Ledger_ID_Mlt"] = ViewState["Ledger_ID_Mlt"].ToString();

                if (chkOpeningBal.Checked == true)
                {
                    Session["ChkOpen"] = "True";
                }
                if (chkTransactionAmt.Checked == true)
                {
                    Session["ChkTxn"] = "True";
                }
                if (chkClosingBal.Checked == true)
                {
                    Session["ChkClose"] = "True";
                }

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
                Session["Header"] = "<p class='text-center' style='font-weight:600'>Detailed Trial Balance <br /> SFA Technologies Pvt. Ltd. <br/> [ " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy");
                sb.Append("<table style='width:100%;'><tr><td colspan='5' style='text-align:center;'><p class='text-center' style='font-weight:600'>Detailed Trial Balance <br /> M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd, <br/> [ " + OfficeName + " ] <br />  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd-MM-yyyy") + "  To " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd-MM-yyyy") + "</p> <br /></td></tr></table>");

                sb.Append("<table id='GrvTable' style='width:100%;' class='table table-hover table-bordered'>");
                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<th style='width:40%;'>Group Name</th>");
                if (chkOpeningBal.Checked == true && chkTransactionAmt.Checked == true && chkClosingBal.Checked == true)
                {
                    sb.Append("<th style='width:160px;'>Opening</th>");
                    sb.Append("<th style='width:160px;'>Txn. <br> [Debit Amt.]</th>");
                    sb.Append("<th style='width:160px;'>Txn. <br> [Credit Amt.]</th>");
                    sb.Append("<th style='width:160px;'>Closing</th>");

                }
                else
                {
                    if (chkOpeningBal.Checked == true)
                    {
                        sb.Append("<th style='width:160px;'>Opening <br> [Debit Amt.]</th>");
                        sb.Append("<th style='width:160px;'>Opening <br> [Credit Amt.]</th>");
                    }
                    if (chkTransactionAmt.Checked == true)
                    {
                        sb.Append("<th style='width:160px;'>Txn. <br> [Debit Amt.]</th>");
                        sb.Append("<th style='width:160px;'>Txn. <br> [Credit Amt.]</th>");
                    }
                    if (chkClosingBal.Checked == true)
                    {
                        sb.Append("<th style='width:160px;'>Closing <br> [Debit Amt.]</th>");
                        sb.Append("<th style='width:160px;'>Closing <br> [Credit Amt.]</th>");
                    }
                }
                sb.Append("</tr>");
                sb.Append("</thead>");
                ds = objdb.ByProcedure("SpFinRptTrialBalanceDetailed", new string[] { "flag" }, new string[] { "1" }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    sb.Append("<tbody>");

                    int rowcount = ds.Tables[0].Rows.Count;

                    decimal GTotalOpeningBal = 0;
                    decimal GTotalClosingBal = 0;

                    decimal GOpenTotalDr = 0;
                    decimal GOpenTotalCr = 0;
                    decimal GTotalDebitAmt = 0;
                    decimal GTotalCreditAmt = 0;
                    decimal GCloseTotalDr = 0;
                    decimal GCloseTotalCr = 0;

                    for (int i = 0; i < rowcount; i++)
                    {
                        int Grid1 = 0; int Grid2 = 0;
                        decimal TotalOpeningBal = 0;
                        decimal TotalClosingBal = 0;
                        decimal TotalDebitAmt = 0;
                        decimal TotalCreditAmt = 0;
                        //GridView1.FooterRow.Visible = true;

                        decimal OpenTotalDr = 0;
                        decimal OpenTotalCr = 0;
                        decimal CloseTotalDr = 0;
                        decimal CloseTotalCr = 0;
                        StringBuilder sbChild = new StringBuilder();

                        ds1 = objdb.ByProcedure("SpFinRptTrialBalanceDetailed",
                            new string[] { "flag", "Office_ID_Mlt", "Head_ID", "FromDate", "ToDate", "Ledger_ID_Mlt" },
                            new string[] { "2", Office, ds.Tables[0].Rows[i]["Head_ID"].ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Ledger_ID_Mlt"].ToString() }, "dataset");
                        if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                        {

                            Grid1 = 1;
                            //GridView1.DataSource = ds1;
                            //GridView1.DataBind();



                            // Opening Balance
                            int rowcountChild = ds1.Tables[0].Rows.Count;
                            for (int k = 0; k < rowcountChild; k++)
                            {
                                decimal OpeningBal = 0;
                                decimal PreOpeningBal = 0;

                                if (ds1.Tables[0].Rows[k]["PreOpeningBalance"].ToString() != "")
                                    PreOpeningBal = decimal.Parse(ds1.Tables[0].Rows[k]["PreOpeningBalance"].ToString());

                                if (ds1.Tables[0].Rows[k]["OpeningBalance"].ToString() != "")
                                    OpeningBal = decimal.Parse(ds1.Tables[0].Rows[k]["OpeningBalance"].ToString());

                                if (ds1.Tables[0].Rows[k]["DebitAmt"].ToString() != "")
                                    TotalDebitAmt = TotalDebitAmt + decimal.Parse(ds1.Tables[0].Rows[k]["DebitAmt"].ToString());

                                if (ds1.Tables[0].Rows[k]["CreditAmt"].ToString() != "")
                                    TotalCreditAmt = TotalCreditAmt + decimal.Parse(ds1.Tables[0].Rows[k]["CreditAmt"].ToString());

                                TotalOpeningBal = TotalOpeningBal + PreOpeningBal;
                                TotalClosingBal = TotalClosingBal + OpeningBal;

                                string HeadID = ds1.Tables[0].Rows[k]["Head_ID"].ToString();

                                if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                                {
                                    sbChild.Append("<tr class='Chead'>");
                                    if (HeadID == "-1")
                                    {
                                        sbChild.Append("<td style='padding-left:13px;'>" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</td>");

                                    }
                                    else if (HeadID == "0")
                                    {
                                        string urlOpen = "RptStockSummary.aspx?FromDate=" + objdb.Encrypt(txtFromDate.Text) + "&ToDate=" + objdb.Encrypt(txtToDate.Text) + "&MltOfficeID=" + objdb.Encrypt(Office);

                                        sbChild.Append("<td style='padding-left:13px;'><a href='" + urlOpen + "' target='_blank'>" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</a></td>");
                                    }
                                    else
                                    {
                                        if (ds1.Tables[0].Rows[k]["DebitAmt"].ToString() != "" || ds1.Tables[0].Rows[k]["CreditAmt"].ToString() != "")
                                            sbChild.Append("<td style='padding-left:13px;'><a onclick='GetHeadData(" + HeadID + ")'>" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</a></td>");
                                        else
                                            sbChild.Append("<td style='padding-left:13px;'>" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</td>");
                                    }

                                    if (PreOpeningBal >= 0)
                                    {
                                        sbChild.Append("<td class='align-right'>" + PreOpeningBal.ToString() + " Cr" + "</td>");
                                        //GridView1.Rows[k].Cells[2].Text = PreOpeningBal.ToString() + " Cr";
                                    }
                                    else
                                    {
                                        sbChild.Append("<td class='align-right'>" + Math.Abs(PreOpeningBal).ToString() + " Dr" + "</td>");
                                        //GridView1.Rows[k].Cells[2].Text = Math.Abs(PreOpeningBal).ToString() + " Dr";
                                    }

                                    sbChild.Append("<td class='align-right'>" + ds1.Tables[0].Rows[k]["DebitAmt"].ToString() + "</td>");
                                    sbChild.Append("<td class='align-right'>" + ds1.Tables[0].Rows[k]["CreditAmt"].ToString() + "</td>");
                                    if (OpeningBal >= 0)
                                    {
                                        sbChild.Append("<td class='align-right'>" + OpeningBal.ToString() + " Cr" + "</td>");
                                        //  GridView1.Rows[k].Cells[6].Text = OpeningBal.ToString() + " Cr";

                                    }
                                    else
                                    {
                                        sbChild.Append("<td class='align-right'>" + Math.Abs(OpeningBal).ToString() + " Dr" + "</td>");
                                        // GridView1.Rows[k].Cells[6].Text = Math.Abs(OpeningBal).ToString() + " Dr";
                                    }
                                    sbChild.Append("</tr>");
                                    sbChild.Append("<tr> <td class='CHeadC' colspan='5' id='td" + HeadID + "'></ <td></tr>");
                                }
                                else
                                {
                                    int colcount = 0;
                                    sbChild.Append("<tr class='Chead'>");
                                    // sbChild.Append("<td>&nbsp;&nbsp;" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</td>");
                                    if (HeadID == "-1")
                                    {
                                        sbChild.Append("<td  style='padding-left:13px;'>" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</td>");

                                    }
                                    else if (HeadID == "0")
                                    {
                                        string urlOpen = "";// "RptStockSummary.aspx?FromDate=" + objdb.Encrypt(txtFromDate.Text) + "&ToDate=" + objdb.Encrypt(txtToDate.Text) + "&MltOfficeID=" + objdb.Encrypt(Office);
                                        if (ds1.Tables[0].Rows[k]["DebitAmt"].ToString() != "" || ds1.Tables[0].Rows[k]["CreditAmt"].ToString() != "")
                                            sbChild.Append("<td  style='padding-left:13px;'><a href='" + urlOpen + "' target='_blank'>" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</a></td>");
                                        else
                                            sbChild.Append("<td style='padding-left:13px;'>" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</td>");
                                    }
                                    else
                                    {
                                        sbChild.Append("<td style='padding-left:13px;'><a onclick='GetHeadData(" + HeadID + ")'>" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</a></td>");
                                    }
                                    if (chkOpeningBal.Checked == true)
                                    {
                                        colcount = colcount + 1;

                                        sbChild.Append("<td class='align-right'>" + ds1.Tables[0].Rows[k]["OpeningBalanceDR"].ToString() + "</td>");
                                        if (ds1.Tables[0].Rows[k]["OpeningBalanceDR"].ToString() != "")
                                        {
                                            OpenTotalDr = OpenTotalDr + decimal.Parse(ds1.Tables[0].Rows[k]["OpeningBalanceDR"].ToString());
                                        }
                                        sbChild.Append("<td class='align-right'>" + ds1.Tables[0].Rows[k]["OpeningBalanceCR"].ToString() + "</td>");
                                        if (ds1.Tables[0].Rows[k]["OpeningBalanceCR"].ToString() != "")
                                        {
                                            OpenTotalCr = OpenTotalCr + decimal.Parse(ds1.Tables[0].Rows[k]["OpeningBalanceCR"].ToString());
                                        }
                                    }
                                    if (chkTransactionAmt.Checked == true)
                                    {
                                        colcount = colcount + 1;
                                        sbChild.Append("<td class='align-right'>" + ds1.Tables[0].Rows[k]["DebitAmt"].ToString() + "</td>");
                                        sbChild.Append("<td class='align-right'>" + ds1.Tables[0].Rows[k]["CreditAmt"].ToString() + "</td>");
                                    }
                                    if (chkClosingBal.Checked == true)
                                    {
                                        colcount = colcount + 1;
                                        sbChild.Append("<td class='align-right'>" + ds1.Tables[0].Rows[k]["DebitClosingBalance"].ToString() + "</td>");
                                        if (ds1.Tables[0].Rows[k]["DebitClosingBalance"].ToString() != "")
                                        {
                                            CloseTotalDr = CloseTotalDr + decimal.Parse(ds1.Tables[0].Rows[k]["DebitClosingBalance"].ToString());
                                        }
                                        sbChild.Append("<td class='align-right'>" + ds1.Tables[0].Rows[k]["CreditClosingBalance"].ToString() + "</td>");
                                        if (ds1.Tables[0].Rows[k]["CreditClosingBalance"].ToString() != "")
                                        {
                                            CloseTotalCr = CloseTotalCr + decimal.Parse(ds1.Tables[0].Rows[k]["CreditClosingBalance"].ToString());
                                        }
                                    }
                                    sbChild.Append("</tr>");
                                    if (colcount < 2)
                                        sbChild.Append("<tr> <td class='CHeadC' colspan='3' id='td" + HeadID + "'></ <td></tr>");
                                    else
                                        sbChild.Append("<tr> <td class='CHeadC' colspan='5' id='td" + HeadID + "'></ <td></tr>");
                                }

                            }

                        }

                        ds1 = objdb.ByProcedure("SpFinRptTrialBalanceDetailed", new string[] { "flag", "Office_ID_Mlt", "Head_ID", "FromDate", "ToDate", "Ledger_ID_Mlt" },
                            new string[] { "3", Office, ds.Tables[0].Rows[i]["Head_ID"].ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Ledger_ID_Mlt"].ToString() }, "dataset");
                        if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                        {

                            Grid2 = 1;
                            //GridView2.DataSource = ds1;
                            //GridView2.DataBind();
                            //GridView2.UseAccessibleHeader = true;
                            //GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;




                            int rowcountChild = ds1.Tables[0].Rows.Count;


                            for (int k = 0; k < rowcountChild; k++)
                            {
                                decimal OpeningBal = 0;
                                decimal PreOpeningBal = 0;


                                if (ds1.Tables[0].Rows[k]["PreOpeningBalance"].ToString() != "")
                                    PreOpeningBal = decimal.Parse(ds1.Tables[0].Rows[k]["PreOpeningBalance"].ToString());

                                if (ds1.Tables[0].Rows[k]["OpeningBalance"].ToString() != "")
                                    OpeningBal = decimal.Parse(ds1.Tables[0].Rows[k]["OpeningBalance"].ToString());

                                if (ds1.Tables[0].Rows[k]["DebitAmt"].ToString() != "")
                                    TotalDebitAmt = TotalDebitAmt + decimal.Parse(ds1.Tables[0].Rows[k]["DebitAmt"].ToString());

                                if (ds1.Tables[0].Rows[k]["CreditAmt"].ToString() != "")
                                    TotalCreditAmt = TotalCreditAmt + decimal.Parse(ds1.Tables[0].Rows[k]["CreditAmt"].ToString());

                                TotalOpeningBal = TotalOpeningBal + PreOpeningBal;
                                TotalClosingBal = TotalClosingBal + OpeningBal;
                                sbChild.Append("<tr>");

                                if (ds1.Tables[0].Rows[k]["DebitAmt"].ToString() != "" || ds1.Tables[0].Rows[k]["CreditAmt"].ToString() != "")
                                    sbChild.Append("<td style='padding-left:23px;font-style: italic;'><a onclick='GetLedgerDetail(" + ds1.Tables[0].Rows[k]["Ledger_ID"].ToString() + ")'>" + ds1.Tables[0].Rows[k]["Ledger_Name"].ToString() + "</a></td>");
                                else
                                    sbChild.Append("<td style='padding-left:23px;font-style: italic;'>" + ds1.Tables[0].Rows[k]["Ledger_Name"].ToString() + "</td>");

                                if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                                {
                                    if (PreOpeningBal >= 0)
                                    {
                                        sbChild.Append("<td class='align-right'>" + PreOpeningBal.ToString() + " Cr" + "</td>");
                                        // GridView2.Rows[k].Cells[2].Text = PreOpeningBal.ToString() + " Cr";
                                    }
                                    else
                                    {
                                        sbChild.Append("<td class='align-right'>" + Math.Abs(PreOpeningBal).ToString() + " Dr" + "</td>");
                                        // GridView2.Rows[k].Cells[2].Text = Math.Abs(PreOpeningBal).ToString() + " Dr";
                                    }
                                    sbChild.Append("<td class='align-right'>" + ds1.Tables[0].Rows[k]["DebitAmt"].ToString() + "</td>");
                                    sbChild.Append("<td class='align-right'>" + ds1.Tables[0].Rows[k]["CreditAmt"].ToString() + "</td>");
                                    if (OpeningBal >= 0)
                                    {
                                        sbChild.Append("<td class='align-right'>" + OpeningBal.ToString() + " Cr" + "</td>");
                                        //GridView2.Rows[k].Cells[6].Text = OpeningBal.ToString() + " Cr";

                                    }
                                    else
                                    {
                                        sbChild.Append("<td class='align-right'>" + Math.Abs(OpeningBal).ToString() + " Dr" + "</td>");
                                        // GridView2.Rows[k].Cells[6].Text = Math.Abs(OpeningBal).ToString() + " Dr";
                                    }
                                }
                                else
                                {
                                    if (chkOpeningBal.Checked == true)
                                    {
                                        if (PreOpeningBal >= 0)
                                        {
                                            //GridView2.Rows[k].Cells[3].Text = PreOpeningBal.ToString();// +" Cr";
                                            OpenTotalCr = OpenTotalCr + PreOpeningBal;
                                            sbChild.Append("<td class='align-right'></td>");
                                            sbChild.Append("<td class='align-right'>" + PreOpeningBal.ToString() + "</td>");
                                        }
                                        else
                                        {
                                            //GridView2.Rows[k].Cells[2].Text = Math.Abs(PreOpeningBal).ToString();// + " Dr";
                                            OpenTotalDr = OpenTotalDr + Math.Abs(PreOpeningBal);
                                            sbChild.Append("<td class='align-right'>" + Math.Abs(PreOpeningBal).ToString() + "</td>");
                                            sbChild.Append("<td class='align-right'></td>");

                                        }
                                    }
                                    if (chkTransactionAmt.Checked == true)
                                    {
                                        sbChild.Append("<td class='align-right'>" + ds1.Tables[0].Rows[k]["DebitAmt"].ToString() + "</td>");
                                        sbChild.Append("<td class='align-right'>" + ds1.Tables[0].Rows[k]["CreditAmt"].ToString() + "</td>");
                                    }
                                    if (chkClosingBal.Checked == true)
                                    {

                                        if (OpeningBal >= 0)
                                        {
                                            // GridView2.Rows[k].Cells[7].Text = OpeningBal.ToString();// + " Cr";
                                            CloseTotalCr = CloseTotalCr + OpeningBal;
                                            sbChild.Append("<td class='align-right'></td>");
                                            sbChild.Append("<td class='align-right'>" + OpeningBal.ToString() + "</td>");

                                        }
                                        else
                                        {
                                            //GridView2.Rows[k].Cells[6].Text = Math.Abs(OpeningBal).ToString();// + " Dr";
                                            CloseTotalDr = CloseTotalDr + Math.Abs(OpeningBal);
                                            sbChild.Append("<td class='align-right'>" + Math.Abs(OpeningBal).ToString() + "</td>");
                                            sbChild.Append("<td class='align-right'></td>");
                                        }
                                    }

                                }
                                sbChild.Append("</tr>");
                            }

                            //GridView2.FooterRow.Cells[1].Text = "<b>Grand Total</b>";


                            //if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                            //{
                            //    if (TotalOpeningBal >= 0)
                            //        GridView2.FooterRow.Cells[2].Text = TotalOpeningBal.ToString() + " Cr";
                            //    else
                            //        GridView2.FooterRow.Cells[2].Text = Math.Abs(TotalOpeningBal).ToString() + " Dr";

                            //    GridView2.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
                            //    GridView2.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();

                            //    if (TotalClosingBal >= 0)
                            //        GridView2.FooterRow.Cells[6].Text = TotalClosingBal.ToString() + " Cr";
                            //    else
                            //        GridView2.FooterRow.Cells[6].Text = Math.Abs(TotalClosingBal).ToString() + " Dr";

                            //}
                            //else
                            //{

                            //    GridView2.FooterRow.Cells[2].Text = Math.Abs(OpenTotalDr).ToString();//Dr
                            //    GridView2.FooterRow.Cells[3].Text = OpenTotalCr.ToString(); //Cr


                            //    GridView2.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
                            //    GridView2.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();


                            //    GridView2.FooterRow.Cells[6].Text = Math.Abs(CloseTotalDr).ToString(); //Dr
                            //    GridView2.FooterRow.Cells[7].Text = CloseTotalCr.ToString(); //Cr
                            //}



                        }

                        if (Grid1 != 0 || Grid2 != 0)
                        {

                            if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                            {
                                sb.Append("<tr class='Mhead'>");
                                sb.Append("<td style='padding-left:4px;'>" + ds.Tables[0].Rows[i]["Head_Name"].ToString() + "</td>");

                                GTotalOpeningBal = GTotalOpeningBal + TotalOpeningBal;
                                GTotalDebitAmt = GTotalDebitAmt + TotalDebitAmt;
                                GTotalCreditAmt = GTotalCreditAmt + TotalCreditAmt;
                                GTotalClosingBal = GTotalClosingBal + TotalClosingBal;

                                if (TotalOpeningBal >= 0)
                                {
                                    sb.Append("<td class='align-right'>" + TotalOpeningBal.ToString() + " Cr" + "</td>");
                                    //  GridView2.FooterRow.Cells[2].Text = TotalOpeningBal.ToString() + " Cr";
                                }
                                else
                                {
                                    sb.Append("<td class='align-right'>" + Math.Abs(TotalOpeningBal).ToString() + " Dr" + "</td>");
                                    //GridView2.FooterRow.Cells[2].Text = Math.Abs(TotalOpeningBal).ToString() + " Dr";
                                }


                                //GridView2.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
                                //GridView2.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();

                                sb.Append("<td class='align-right'>" + TotalDebitAmt.ToString() + "</td>");
                                sb.Append("<td class='align-right'>" + TotalCreditAmt.ToString() + "</td>");

                                if (TotalClosingBal >= 0)
                                {
                                    sb.Append("<td class='align-right'>" + TotalClosingBal.ToString() + " Cr" + "</td>");
                                    //GridView2.FooterRow.Cells[6].Text = TotalClosingBal.ToString() + " Cr";
                                }
                                else
                                {
                                    sb.Append("<td class='align-right'>" + Math.Abs(TotalClosingBal).ToString() + " Dr" + "</td>");
                                    //GridView2.FooterRow.Cells[6].Text = Math.Abs(TotalClosingBal).ToString() + " Dr";
                                }

                                sb.Append("</tr>");

                            }
                            else
                            {
                                GOpenTotalDr = GOpenTotalDr + OpenTotalDr;
                                GOpenTotalCr = GOpenTotalCr + OpenTotalCr;
                                GTotalDebitAmt = GTotalDebitAmt + TotalDebitAmt;
                                GTotalCreditAmt = GTotalCreditAmt + TotalCreditAmt;
                                GCloseTotalDr = GCloseTotalDr + CloseTotalDr;
                                GCloseTotalCr = GCloseTotalCr + CloseTotalCr;

                                sb.Append("<tr class='Mhead'>");
                                sb.Append("<td style='padding-left:4px;'>" + ds.Tables[0].Rows[i]["Head_Name"].ToString() + "</td>");
                                if (chkOpeningBal.Checked == true)
                                {
                                    sb.Append("<td class='align-right'>" + Math.Abs(OpenTotalDr).ToString() + "</td>");
                                    sb.Append("<td class='align-right'>" + OpenTotalCr.ToString() + "</td>");
                                    //GridView2.FooterRow.Cells[2].Text = Math.Abs(OpenTotalDr).ToString();//Dr
                                    //GridView2.FooterRow.Cells[3].Text = OpenTotalCr.ToString(); //Cr

                                }
                                if (chkTransactionAmt.Checked == true)
                                {
                                    sb.Append("<td class='align-right'>" + Math.Abs(TotalDebitAmt).ToString() + "</td>");
                                    sb.Append("<td class='align-right'>" + TotalCreditAmt.ToString() + "</td>");
                                    //GridView2.FooterRow.Cells[4].Text = TotalDebitAmt.ToString();
                                    //GridView2.FooterRow.Cells[5].Text = TotalCreditAmt.ToString();
                                }
                                if (chkClosingBal.Checked == true)
                                {

                                    sb.Append("<td class='align-right'>" + Math.Abs(CloseTotalDr).ToString() + "</td>");
                                    sb.Append("<td class='align-right'>" + CloseTotalCr.ToString() + "</td>");
                                    //GridView2.FooterRow.Cells[6].Text = Math.Abs(CloseTotalDr).ToString(); //Dr
                                    //GridView2.FooterRow.Cells[7].Text = CloseTotalCr.ToString(); //Cr
                                }
                                sb.Append("</tr>");
                            }
                            sb.Append(" " + sbChild.ToString() + " ");
                        }

                    }
                    sb.Append("</tbody>");
                    /****************GRAND TOTAL****************/
                    sb.Append("<tfoot>");
                    if (chkTransactionAmt.Checked == true && chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                    {
                        /****************Diff Opening****************/
                        int status = 0;
                        string Diff = "";
                        Diff = "<tr class='Mhead'>";
                        Diff += "<td style='padding-left:4px;'>Difference in Opening Balances </td>";


                        if (GTotalOpeningBal > 0)
                        {
                            status = 1;
                            Diff += "<td class='align-right'>" + GTotalOpeningBal.ToString() + " Dr" + "</td>";
                        }
                        else
                        {
                            status = 1;
                            Diff += "<td class='align-right'>" + Math.Abs(GTotalOpeningBal).ToString() + " Cr" + "</td>";
                        }

                        Diff += "<td class='align-right'></td>";
                        Diff += "<td class='align-right'></td>";


                        if (GTotalClosingBal > 0)
                        {
                            status = 1;
                            Diff += "<td class='align-right'>" + GTotalClosingBal.ToString() + " Dr" + "</td>";
                            //GridView2.FooterRow.Cells[6].Text = TotalClosingBal.ToString() + " Cr";
                        }
                        else
                        {
                            status = 1;
                            Diff += "<td class='align-right'>" + Math.Abs(GTotalClosingBal).ToString() + " Cr" + "</td>";
                            //GridView2.FooterRow.Cells[6].Text = Math.Abs(TotalClosingBal).ToString() + " Dr";
                        }
                        Diff += "</tr>";
                        if (status == 1)
                            sb.Append("" + Diff);
                        /****************GRAND TOTAL****************/
                        sb.Append("<tr class='Ghead'>");
                        sb.Append("<td style='padding-left:4px;'>Grand Total </td>");

                        sb.Append("<td class='align-right'></td>");
                        //if (GTotalOpeningBal >= 0)
                        //{
                        //    sb.Append("<td class='align-right'>" + GTotalOpeningBal.ToString() + " Cr" + "</td>");
                        //}
                        //else
                        //{
                        //    sb.Append("<td class='align-right'>" + Math.Abs(GTotalOpeningBal).ToString() + " Dr" + "</td>");
                        //}
                        sb.Append("<td class='align-right'>" + GTotalDebitAmt.ToString() + "</td>");
                        sb.Append("<td class='align-right'>" + GTotalCreditAmt.ToString() + "</td>");
                        sb.Append("<td class='align-right'></td>");
                        //if (GTotalClosingBal >= 0)
                        //{
                        //    sb.Append("<td class='align-right'>" + GTotalClosingBal.ToString() + " Cr" + "</td>");
                        //}
                        //else
                        //{
                        //    sb.Append("<td class='align-right'>" + Math.Abs(GTotalClosingBal).ToString() + " Dr" + "</td>");
                        //}

                        sb.Append("</tr>");

                    }
                    else
                    {
                        int status = 0;
                        string Diff = "";
                        Diff = "<tr class='Mhead'>";
                        Diff += "<td style='padding-left:4px;'>Difference in Opening Balances </td>";
                        if (GOpenTotalDr != GOpenTotalCr || GCloseTotalDr != GCloseTotalCr || GTotalDebitAmt != GTotalCreditAmt)
                        {


                            if (chkOpeningBal.Checked == true)
                            {
                                if (GOpenTotalDr < GOpenTotalCr)
                                {
                                    status = 1;
                                    // GridView1.Rows[rowcount].Cells[2].Text = (GOpenTotalCr - GOpenTotalDr).ToString();// +"Dr";
                                    Diff += "<td class='align-right'>" + (GOpenTotalCr - GOpenTotalDr).ToString() + "</td>";
                                    Diff += "<td></td>";

                                    GOpenTotalDr = GOpenTotalCr;
                                }
                                else if (GOpenTotalDr > GOpenTotalCr)
                                {
                                    status = 1;
                                    // GridView1.Rows[rowcount].Cells[3].Text = (GOpenTotalDr - GOpenTotalCr).ToString();// + "Cr";
                                    Diff += "<td></td>";
                                    Diff += "<td class='align-right'>" + (GOpenTotalDr - GOpenTotalCr).ToString() + "</td>";
                                    GOpenTotalCr = GOpenTotalDr;
                                }
                                else
                                {
                                    Diff += "<td></td>";
                                    Diff += "<td></td>";
                                }
                            }

                            if (chkTransactionAmt.Checked == true)
                            {
                                if (GTotalDebitAmt < GTotalCreditAmt)
                                {
                                    status = 1;
                                    // GridView1.Rows[rowcount].Cells[4].Text = (GTotalCreditAmt - GTotalDebitAmt).ToString();// +"Dr";
                                    Diff += "<td class='align-right'>" + (GTotalCreditAmt - GTotalDebitAmt).ToString() + "</td>";
                                    Diff += "<td></td>";
                                    GTotalDebitAmt = GTotalCreditAmt;
                                }
                                else if (GTotalDebitAmt > GTotalCreditAmt)
                                {
                                    status = 1;
                                    //  GridView1.Rows[rowcount].Cells[5].Text = (GTotalDebitAmt - GTotalCreditAmt).ToString();// + "Cr";
                                    Diff += "<td></td>";
                                    Diff += "<td class='align-right'>" + (GTotalDebitAmt - GTotalCreditAmt).ToString() + "</td>";
                                    GTotalCreditAmt = GTotalDebitAmt;
                                }
                                else
                                {
                                    Diff += "<td></td>";
                                    Diff += "<td></td>";
                                }
                            }
                            if (chkClosingBal.Checked == true)
                            {
                                if (GCloseTotalDr < GCloseTotalCr)
                                {
                                    status = 1;
                                    //GridView1.Rows[rowcount].Cells[6].Text = (GCloseTotalCr - GCloseTotalDr).ToString();// + "Dr";

                                    Diff += "<td class='align-right'>" + (GCloseTotalCr - GCloseTotalDr).ToString() + "</td>";
                                    Diff += "<td></td>";
                                    GCloseTotalDr = GCloseTotalCr;
                                }
                                else if (GCloseTotalDr > GCloseTotalCr)
                                {
                                    status = 1;
                                    // GridView1.Rows[rowcount].Cells[7].Text = (GCloseTotalDr - GCloseTotalCr).ToString();// + "Cr";
                                    Diff += "<td></td>";
                                    Diff += "<td class='align-right'>" + (GCloseTotalDr - GCloseTotalCr).ToString() + "</td>";
                                    GCloseTotalCr = GCloseTotalDr;
                                }
                                else
                                {
                                    Diff += "<td></td>";
                                    Diff += "<td></td>";
                                }
                            }


                        }
                        if (status == 1)
                            sb.Append("" + Diff);

                        sb.Append("<tr class='Ghead'>");
                        sb.Append("<td style='padding-left:4px;'>Grand Total</td>");
                        if (chkOpeningBal.Checked == true)
                        {
                            sb.Append("<td class='align-right'>" + Math.Abs(GOpenTotalDr).ToString() + "</td>");
                            sb.Append("<td class='align-right'>" + GOpenTotalCr.ToString() + "</td>");

                        }
                        if (chkTransactionAmt.Checked == true)
                        {
                            sb.Append("<td class='align-right'>" + Math.Abs(GTotalDebitAmt).ToString() + "</td>");
                            sb.Append("<td class='align-right'>" + GTotalCreditAmt.ToString() + "</td>");
                        }
                        if (chkClosingBal.Checked == true)
                        {

                            sb.Append("<td class='align-right'>" + Math.Abs(GCloseTotalDr).ToString() + "</td>");
                            sb.Append("<td class='align-right'>" + GCloseTotalCr.ToString() + "</td>");
                        }
                        sb.Append("</tr>");

                    }
                    sb.Append("</tfoot>");
                }
                sb.Append("</table>");
                DivTBMain.InnerHtml = sb.ToString();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select atleast one Office.');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetHeadDetail(string HeadID)
    {
        AbstApiDBApi objdbWeb = new APIProcedure();
        CultureInfo cult = new CultureInfo("gu-IN", true);
        DataSet ds1;
        string FromDate = Convert.ToDateTime(HttpContext.Current.Session["FromDate"].ToString(), cult).ToString("yyyy/MM/dd");
        string ToDate = Convert.ToDateTime(HttpContext.Current.Session["ToDate"].ToString(), cult).ToString("yyyy/MM/dd");//HttpContext.Current.Session["ToDate"].ToString();
        string Office = HttpContext.Current.Session["Office"].ToString();

        string ChkOpen = HttpContext.Current.Session["ChkOpen"].ToString();
        string ChkTxn = HttpContext.Current.Session["ChkTxn"].ToString();
        string ChkClose = HttpContext.Current.Session["ChkClose"].ToString();
        string Ledger_ID_Mlt = HttpContext.Current.Session["Ledger_ID_Mlt"].ToString();

        //   string DataReturn = "HeadID - " + HeadID.ToString() + "From Date - " + FromDate.ToString() + "To Date - " + ToDate.ToString() + "Office - " + Office.ToString();

        decimal TotalOpeningBal = 0;
        decimal TotalClosingBal = 0;
        decimal TotalDebitAmt = 0;
        decimal TotalCreditAmt = 0;
        //GridView1.FooterRow.Visible = true;

        decimal OpenTotalDr = 0;
        decimal OpenTotalCr = 0;
        decimal CloseTotalDr = 0;
        decimal CloseTotalCr = 0;
        StringBuilder sbChild = new StringBuilder();
        sbChild.Append("<a onclick='HideFun(" + HeadID.ToString() + ")'  class='backCss'><<</a><table class='table table-hover table-bordered' style='margin-bottom: 0px;'><tbody>");
        ds1 = objdbWeb.ByProcedure("SpFinRptTrialBalanceDetailed",
             new string[] { "flag", "Office_ID_Mlt", "Head_ID", "FromDate", "ToDate", "Ledger_ID_Mlt" },
             new string[] { "2", Office, HeadID.ToString(), FromDate, ToDate, Ledger_ID_Mlt }, "dataset");
        if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
        {

            // Opening Balance
            int rowcountChild = ds1.Tables[0].Rows.Count;
            for (int k = 0; k < rowcountChild; k++)
            {
                decimal OpeningBal = 0;
                decimal PreOpeningBal = 0;

                if (ds1.Tables[0].Rows[k]["PreOpeningBalance"].ToString() != "")
                    PreOpeningBal = decimal.Parse(ds1.Tables[0].Rows[k]["PreOpeningBalance"].ToString());

                if (ds1.Tables[0].Rows[k]["OpeningBalance"].ToString() != "")
                    OpeningBal = decimal.Parse(ds1.Tables[0].Rows[k]["OpeningBalance"].ToString());

                if (ds1.Tables[0].Rows[k]["DebitAmt"].ToString() != "")
                    TotalDebitAmt = TotalDebitAmt + decimal.Parse(ds1.Tables[0].Rows[k]["DebitAmt"].ToString());

                if (ds1.Tables[0].Rows[k]["CreditAmt"].ToString() != "")
                    TotalCreditAmt = TotalCreditAmt + decimal.Parse(ds1.Tables[0].Rows[k]["CreditAmt"].ToString());

                TotalOpeningBal = TotalOpeningBal + PreOpeningBal;
                TotalClosingBal = TotalClosingBal + OpeningBal;

                string HeadChild = ds1.Tables[0].Rows[k]["Head_ID"].ToString();

                if (ChkOpen == "True" && ChkTxn == "True" && ChkClose == "True")
                {
                    sbChild.Append("<tr class='Chead'>");
                    if (HeadChild == "-1")
                    {
                        sbChild.Append("<td style='padding-left:13px;'>" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</td>");

                    }
                    else
                    {
                        if (ds1.Tables[0].Rows[k]["DebitAmt"].ToString() != "" || ds1.Tables[0].Rows[k]["CreditAmt"].ToString() != "")
                            sbChild.Append("<td style='padding-left:13px;'><a onclick='GetHeadData(" + HeadChild.ToString() + ")'>" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</a></td>");
                        else
                            sbChild.Append("<td style='padding-left:13px;'>" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</td>");
                    }

                    if (PreOpeningBal >= 0)
                    {
                        sbChild.Append("<td class='align-right Wcss'>" + PreOpeningBal.ToString() + " Cr" + "</td>");
                        //GridView1.Rows[k].Cells[2].Text = PreOpeningBal.ToString() + " Cr";
                    }
                    else
                    {
                        sbChild.Append("<td class='align-right Wcss'>" + Math.Abs(PreOpeningBal).ToString() + " Dr" + "</td>");
                        //GridView1.Rows[k].Cells[2].Text = Math.Abs(PreOpeningBal).ToString() + " Dr";
                    }

                    sbChild.Append("<td class='align-right Wcss'>" + ds1.Tables[0].Rows[k]["DebitAmt"].ToString() + "</td>");
                    sbChild.Append("<td class='align-right Wcss'>" + ds1.Tables[0].Rows[k]["CreditAmt"].ToString() + "</td>");
                    if (OpeningBal >= 0)
                    {
                        sbChild.Append("<td class='align-right Wcss'>" + OpeningBal.ToString() + " Cr" + "</td>");
                        //  GridView1.Rows[k].Cells[6].Text = OpeningBal.ToString() + " Cr";

                    }
                    else
                    {
                        sbChild.Append("<td class='align-right Wcss'>" + Math.Abs(OpeningBal).ToString() + " Dr" + "</td>");
                        // GridView1.Rows[k].Cells[6].Text = Math.Abs(OpeningBal).ToString() + " Dr";
                    }
                    sbChild.Append("</tr>");
                    sbChild.Append("<tr> <td class='CHeadC' colspan='5' id='td" + HeadChild + "'></ <td></tr>");
                }
                else
                {
                    int colcount = 0;
                    sbChild.Append("<tr class='Chead'>");
                    // sbChild.Append("<td>&nbsp;&nbsp;" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</td>");
                    if (HeadChild == "-1")
                    {
                        sbChild.Append("<td style='padding-left:13px;'>" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</td>");

                    }
                    else
                    {
                        if (ds1.Tables[0].Rows[k]["DebitAmt"].ToString() != "" || ds1.Tables[0].Rows[k]["CreditAmt"].ToString() != "")
                            sbChild.Append("<td style='padding-left:13px;'><a onclick='GetHeadData(" + HeadChild + ")'>" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</a></td>");
                        else
                            sbChild.Append("<td style='padding-left:13px;'>" + ds1.Tables[0].Rows[k]["Head_Name"].ToString() + "</td>");
                    }
                    if (ChkOpen == "True")
                    {
                        colcount = colcount + 1;
                        sbChild.Append("<td class='align-right Wcss'>" + ds1.Tables[0].Rows[k]["OpeningBalanceDR"].ToString() + "</td>");
                        if (ds1.Tables[0].Rows[k]["OpeningBalanceDR"].ToString() != "")
                        {
                            OpenTotalDr = OpenTotalDr + decimal.Parse(ds1.Tables[0].Rows[k]["OpeningBalanceDR"].ToString());
                        }
                        sbChild.Append("<td class='align-right Wcss'>" + ds1.Tables[0].Rows[k]["OpeningBalanceCR"].ToString() + "</td>");
                        if (ds1.Tables[0].Rows[k]["OpeningBalanceCR"].ToString() != "")
                        {
                            OpenTotalCr = OpenTotalCr + decimal.Parse(ds1.Tables[0].Rows[k]["OpeningBalanceCR"].ToString());
                        }
                    }
                    if (ChkTxn == "True")
                    {
                        colcount = colcount + 1;
                        sbChild.Append("<td class='align-right Wcss'>" + ds1.Tables[0].Rows[k]["DebitAmt"].ToString() + "</td>");
                        sbChild.Append("<td class='align-right Wcss'>" + ds1.Tables[0].Rows[k]["CreditAmt"].ToString() + "</td>");
                    }
                    if (ChkClose == "True")
                    {
                        colcount = colcount + 1;
                        sbChild.Append("<td class='align-right Wcss'>" + ds1.Tables[0].Rows[k]["DebitClosingBalance"].ToString() + "</td>");
                        if (ds1.Tables[0].Rows[k]["DebitClosingBalance"].ToString() != "")
                        {
                            CloseTotalDr = CloseTotalDr + decimal.Parse(ds1.Tables[0].Rows[k]["DebitClosingBalance"].ToString());
                        }
                        sbChild.Append("<td class='align-right Wcss'>" + ds1.Tables[0].Rows[k]["CreditClosingBalance"].ToString() + "</td>");
                        if (ds1.Tables[0].Rows[k]["CreditClosingBalance"].ToString() != "")
                        {
                            CloseTotalCr = CloseTotalCr + decimal.Parse(ds1.Tables[0].Rows[k]["CreditClosingBalance"].ToString());
                        }
                    }
                    sbChild.Append("</tr>");
                    if (colcount < 2)
                        sbChild.Append("<tr> <td class='CHeadC' colspan='3' id='td" + HeadChild + "'></ <td></tr>");
                    else
                        sbChild.Append("<tr> <td class='CHeadC' colspan='5' id='td" + HeadChild + "'></ <td></tr>");
                }

            }

        }

        ds1 = objdbWeb.ByProcedure("SpFinRptTrialBalanceDetailed", new string[] { "flag", "Office_ID_Mlt", "Head_ID", "FromDate", "ToDate", "Ledger_ID_Mlt" },
            new string[] { "3", Office, HeadID.ToString(), FromDate, ToDate, Ledger_ID_Mlt }, "dataset");
        if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
        {

            int rowcountChild = ds1.Tables[0].Rows.Count;


            for (int k = 0; k < rowcountChild; k++)
            {
                decimal OpeningBal = 0;
                decimal PreOpeningBal = 0;


                if (ds1.Tables[0].Rows[k]["PreOpeningBalance"].ToString() != "")
                    PreOpeningBal = decimal.Parse(ds1.Tables[0].Rows[k]["PreOpeningBalance"].ToString());

                if (ds1.Tables[0].Rows[k]["OpeningBalance"].ToString() != "")
                    OpeningBal = decimal.Parse(ds1.Tables[0].Rows[k]["OpeningBalance"].ToString());

                if (ds1.Tables[0].Rows[k]["DebitAmt"].ToString() != "")
                    TotalDebitAmt = TotalDebitAmt + decimal.Parse(ds1.Tables[0].Rows[k]["DebitAmt"].ToString());

                if (ds1.Tables[0].Rows[k]["CreditAmt"].ToString() != "")
                    TotalCreditAmt = TotalCreditAmt + decimal.Parse(ds1.Tables[0].Rows[k]["CreditAmt"].ToString());

                TotalOpeningBal = TotalOpeningBal + PreOpeningBal;
                TotalClosingBal = TotalClosingBal + OpeningBal;
                sbChild.Append("<tr>");
                // sbChild.Append("<td>&nbsp;&nbsp;&nbsp;&nbsp;" + ds1.Tables[0].Rows[k]["Ledger_Name"].ToString() + "</td>");

                if (ds1.Tables[0].Rows[k]["DebitAmt"].ToString() != "" || ds1.Tables[0].Rows[k]["CreditAmt"].ToString() != "")
                    sbChild.Append("<td style='padding-left:23px;font-style: italic;'><a onclick='GetLedgerDetail(" + ds1.Tables[0].Rows[k]["Ledger_ID"].ToString() + ")'>" + ds1.Tables[0].Rows[k]["Ledger_Name"].ToString() + "</a></td>");
                else
                    sbChild.Append("<td style='padding-left:23px;font-style: italic;'>" + ds1.Tables[0].Rows[k]["Ledger_Name"].ToString() + "</td>");

                if (ChkOpen == "True" && ChkTxn == "True" && ChkClose == "True")
                {
                    if (PreOpeningBal >= 0)
                    {
                        sbChild.Append("<td class='align-right Wcss'>" + PreOpeningBal.ToString() + " Cr" + "</td>");
                        // GridView2.Rows[k].Cells[2].Text = PreOpeningBal.ToString() + " Cr";
                    }
                    else
                    {
                        sbChild.Append("<td class='align-right Wcss'>" + Math.Abs(PreOpeningBal).ToString() + " Dr" + "</td>");
                        // GridView2.Rows[k].Cells[2].Text = Math.Abs(PreOpeningBal).ToString() + " Dr";
                    }
                    sbChild.Append("<td class='align-right Wcss'>" + ds1.Tables[0].Rows[k]["DebitAmt"].ToString() + "</td>");
                    sbChild.Append("<td class='align-right Wcss'>" + ds1.Tables[0].Rows[k]["CreditAmt"].ToString() + "</td>");
                    if (OpeningBal >= 0)
                    {
                        sbChild.Append("<td class='align-right Wcss'>" + OpeningBal.ToString() + " Cr" + "</td>");
                        //GridView2.Rows[k].Cells[6].Text = OpeningBal.ToString() + " Cr";

                    }
                    else
                    {
                        sbChild.Append("<td class='align-right Wcss'>" + Math.Abs(OpeningBal).ToString() + " Dr" + "</td>");
                        // GridView2.Rows[k].Cells[6].Text = Math.Abs(OpeningBal).ToString() + " Dr";
                    }
                }
                else
                {
                    if (ChkOpen == "True")
                    {
                        if (PreOpeningBal >= 0)
                        {
                            //GridView2.Rows[k].Cells[3].Text = PreOpeningBal.ToString();// +" Cr";
                            OpenTotalCr = OpenTotalCr + PreOpeningBal;
                            sbChild.Append("<td class='align-right Wcss'></td>");
                            sbChild.Append("<td class='align-right Wcss'>" + PreOpeningBal.ToString() + "</td>");
                        }
                        else
                        {
                            //GridView2.Rows[k].Cells[2].Text = Math.Abs(PreOpeningBal).ToString();// + " Dr";
                            OpenTotalDr = OpenTotalDr + Math.Abs(PreOpeningBal);
                            sbChild.Append("<td class='align-right Wcss'>" + Math.Abs(PreOpeningBal).ToString() + "</td>");
                            sbChild.Append("<td class='align-right Wcss'></td>");

                        }
                    }
                    if (ChkTxn == "True")
                    {
                        sbChild.Append("<td class='align-right Wcss'>" + ds1.Tables[0].Rows[k]["DebitAmt"].ToString() + "</td>");
                        sbChild.Append("<td class='align-right Wcss'>" + ds1.Tables[0].Rows[k]["CreditAmt"].ToString() + "</td>");
                    }
                    if (ChkClose == "True")
                    {

                        if (OpeningBal >= 0)
                        {
                            // GridView2.Rows[k].Cells[7].Text = OpeningBal.ToString();// + " Cr";
                            CloseTotalCr = CloseTotalCr + OpeningBal;
                            sbChild.Append("<td class='align-right Wcss'></td>");
                            sbChild.Append("<td class='align-right Wcss'>" + OpeningBal.ToString() + "</td>");

                        }
                        else
                        {
                            //GridView2.Rows[k].Cells[6].Text = Math.Abs(OpeningBal).ToString();// + " Dr";
                            CloseTotalDr = CloseTotalDr + Math.Abs(OpeningBal);
                            sbChild.Append("<td class='align-right Wcss'>" + Math.Abs(OpeningBal).ToString() + "</td>");
                            sbChild.Append("<td class='align-right Wcss'></td>");
                        }
                    }

                }
                sbChild.Append("</tr>");
            }
        }
        sbChild.Append("</tbody></Table>");
        return sbChild.ToString();
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetLedgerDetail(string LedgerID)
    {
        AbstApiDBApi objdbWeb = new APIProcedure();
        CultureInfo cult = new CultureInfo("gu-IN", true);
        DataSet ds1;
        string FromDate = Convert.ToDateTime(HttpContext.Current.Session["FromDate"].ToString(), cult).ToString("yyyy/MM/dd");
        string ToDate = Convert.ToDateTime(HttpContext.Current.Session["ToDate"].ToString(), cult).ToString("yyyy/MM/dd");//HttpContext.Current.Session["ToDate"].ToString();
        string Office = HttpContext.Current.Session["Office"].ToString();

        string ChkOpen = HttpContext.Current.Session["ChkOpen"].ToString();
        string ChkTxn = HttpContext.Current.Session["ChkTxn"].ToString();
        string ChkClose = HttpContext.Current.Session["ChkClose"].ToString();
        string Ledger_ID_Mlt = HttpContext.Current.Session["Ledger_ID_Mlt"].ToString();
        string Header = HttpContext.Current.Session["Header"].ToString();

        StringBuilder sbChild = new StringBuilder();


        ds1 = objdbWeb.ByProcedure("SpFinRptTrialBalanceDetailed", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate", "Ledger_ID_Mlt" },
            new string[] { "4", Office, LedgerID, FromDate, ToDate, Ledger_ID_Mlt }, "dataset");
        if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
        {


            decimal PreOpening = 0;
            decimal OpeningBal = 0;

            if (ds1.Tables[0].Rows[0]["PreOpening"].ToString() != "")
                PreOpening = decimal.Parse(ds1.Tables[0].Rows[0]["PreOpening"].ToString());


            OpeningBal = PreOpening;

            string LedgerName = ds1.Tables[1].Rows[0]["Ledger_Name"].ToString();

            if (ChkOpen == "True")
            {
                if ((PreOpening + decimal.Parse(ds1.Tables[0].Rows[0]["CurrentOpening"].ToString())) < 0)
                {
                    Header = Header + "<br/>Ledger Name : " + LedgerName + "<br/><span class='text-center' style='color:#d03535;'> Opening Bal. : " + Math.Abs(PreOpening + decimal.Parse(ds1.Tables[0].Rows[0]["CurrentOpening"].ToString())).ToString() + " Dr </span></p><br/>";
                }
                else
                {
                    Header = Header + "<br/>Ledger Name : " + LedgerName + "<br/><span class='text-center' style='color:#d03535;'> Opening Bal. : " + Math.Abs(PreOpening + decimal.Parse(ds1.Tables[0].Rows[0]["CurrentOpening"].ToString())).ToString() + " Cr </span></p><br/>";
                }
            }
            string OpenBal = (PreOpening + decimal.Parse(ds1.Tables[0].Rows[0]["CurrentOpening"].ToString())).ToString();

            sbChild.Append("<table style='width:100%;'><tr><td colspan='5' style='text-align:center;'>" + Header + "</td></tr></table>" + "<a onclick='BackLedger()' class='backCss'><<</a><table class='table table-hover table-bordered' id='GridView3' >");
            sbChild.Append("<thead>");
            sbChild.Append("<tr>");
            sbChild.Append("<th>Month Name</th>");
            sbChild.Append("<th style='width:160px;'>Debit Amt.</th>");
            sbChild.Append("<th style='width:160px;'>Credit Amt.</th>");
            sbChild.Append("<th style='width:160px;'>Closing Bal.</th>");
            sbChild.Append("</tr>");
            sbChild.Append("</thead>");
            sbChild.Append("<tbody>");

            // string  ViewState_Bal = (PreOpening + decimal.Parse(ds1.Tables[0].Rows[0]["CurrentOpening"].ToString())).ToString();
            decimal TotDebitAmt = 0;
            decimal TotCreditAmt = 0;

            int rowcount = ds1.Tables[0].Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {

                string DebitAmt = "0";
                decimal CreditAmt = 0;
                decimal CurrentOpening = 0;

                string clickFun = "";




                if (ds1.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                {
                    DebitAmt = "-" + ds1.Tables[0].Rows[i]["DebitAmt"].ToString();
                    TotDebitAmt = TotDebitAmt + decimal.Parse(ds1.Tables[0].Rows[i]["DebitAmt"].ToString());
                }

                if (ds1.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                {
                    CreditAmt = decimal.Parse(ds1.Tables[0].Rows[i]["CreditAmt"].ToString());
                    TotCreditAmt = TotCreditAmt + decimal.Parse(ds1.Tables[0].Rows[i]["CreditAmt"].ToString());
                }

                if (ds1.Tables[0].Rows[0]["CurrentOpening"].ToString() != "")
                    CurrentOpening = decimal.Parse(ds1.Tables[0].Rows[i]["CurrentOpening"].ToString());


                if (ds1.Tables[0].Rows[i]["DebitAmt"].ToString() != "" || ds1.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                {
                    if (i != 0)
                        clickFun = " onclick='GetLedgerDayBook(" + LedgerID + "," + ds1.Tables[0].Rows[i]["MonthID"].ToString() + "," + OpeningBal.ToString() + ")'";
                    else
                        clickFun = " onclick='GetLedgerDayBook(" + LedgerID + "," + ds1.Tables[0].Rows[i]["MonthID"].ToString() + "," + OpenBal.ToString() + ")'";

                }


                OpeningBal = OpeningBal + CurrentOpening + decimal.Parse(DebitAmt) + CreditAmt;




                sbChild.Append("<tr>");

                sbChild.Append("<td><a " + clickFun + ">" + ds1.Tables[0].Rows[i]["MonthName"].ToString() + "</a></td>");

                sbChild.Append("<td class='align-right'>" + ds1.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");




                sbChild.Append("<td class='align-right'>" + ds1.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");

                if (OpeningBal >= 0)
                {
                    sbChild.Append("<td class='align-right'>" + OpeningBal.ToString() + " Cr" + "</td>");
                    // GridView3.Rows[i].Cells[4].Text = OpeningBal.ToString() + " Cr";
                }
                else
                {
                    sbChild.Append("<td class='align-right'>" + Math.Abs(OpeningBal).ToString() + " Dr" + "</td>");
                    // GridView3.Rows[i].Cells[4].Text = Math.Abs(OpeningBal).ToString() + " Dr";
                }
                sbChild.Append("</tr>");
            }
            sbChild.Append("<tr>");
            sbChild.Append("<td><b>Total</b></td>");
            if (TotDebitAmt != 0)
            {
                sbChild.Append("<td class='align-right'><b>" + TotDebitAmt.ToString() + "</b></td>");
            }
            else
            {
                sbChild.Append("<td class='align-right'></td>");
            }
            if (TotCreditAmt != 0)
            {
                sbChild.Append("<td class='align-right'><b>" + TotCreditAmt.ToString() + "</b></td>");
            }
            else
            {
                sbChild.Append("<td class='align-right'></td>");
            }
            sbChild.Append("<td class='align-right'></td>");
            sbChild.Append("</tr>");
            sbChild.Append("</tbody></Table>");
        }

        return sbChild.ToString();
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetLedgerDayBook(string LedgerID, string MonthID, string OpenBal)
    {
        AbstApiDBApi objdbWeb = new APIProcedure();
        CultureInfo cult = new CultureInfo("gu-IN", true);
        DataSet ds1;

        string FromDate = Convert.ToDateTime(HttpContext.Current.Session["FromDate"].ToString(), cult).ToString("yyyy/MM/dd");
        string ToDate = Convert.ToDateTime(HttpContext.Current.Session["ToDate"].ToString(), cult).ToString("yyyy/MM/dd");//HttpContext.Current.Session["ToDate"].ToString();
        string Office = HttpContext.Current.Session["Office"].ToString();
        string Office_ID = HttpContext.Current.Session["Office_ID"].ToString();

        string ChkOpen = HttpContext.Current.Session["ChkOpen"].ToString();
        string ChkTxn = HttpContext.Current.Session["ChkTxn"].ToString();
        string ChkClose = HttpContext.Current.Session["ChkClose"].ToString();
        string Ledger_ID_Mlt = HttpContext.Current.Session["Ledger_ID_Mlt"].ToString();
        string Header = HttpContext.Current.Session["Header"].ToString();


        StringBuilder htmlStr = new StringBuilder();

        decimal BAl = 0;
        if (OpenBal.ToString() != "")
            BAl = decimal.Parse(OpenBal.ToString());



        ds1 = objdbWeb.ByProcedure("SpFinRptTrialBalanceDetailed", new string[] { "flag", "Ledger_ID" }, new string[] { "10", LedgerID.ToString() }, "dataset");
        if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
        {
            DateTimeFormatInfo mfi = new DateTimeFormatInfo();
            //  lblHeadName.Text = ds1.Tables[0].Rows[0]["Ledger_Name"].ToString() + "( Month : " + mfi.GetMonthName(int.Parse(MonthID)).ToString() + " )"; // + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";

            Header = Header + "<br/>" + ds1.Tables[0].Rows[0]["Ledger_Name"].ToString() + "( Month : " + mfi.GetMonthName(int.Parse(MonthID)).ToString() + " )</p>";
        }




        string sDate = FromDate;
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

        DataSet ds = objdbWeb.ByProcedure("SpFinRptTrialBalanceDetailed", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "LedgerTx_Month", "FromDate", "ToDate", "Ledger_ID_Mlt", "FinancialYear" },
            new string[] { "6", Office, LedgerID, MonthID, FromDate, ToDate, Ledger_ID_Mlt, FinancialYear }, "dataset");
        if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
        {



            decimal OpeningBal = BAl;// decimal.Parse(ViewState["Bal"].ToString());

            decimal CurrentBal = 0;
            decimal DebitTotal = 0;
            decimal CreditTotal = 0;
            int rowcount = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {

                string DebitAmt = "0";
                decimal CreditAmt = 0;

                if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                {
                    DebitAmt = "-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString();
                    DebitTotal = DebitTotal + decimal.Parse(ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                }
                if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                {
                    CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                    CreditTotal = CreditTotal + decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                }

                CurrentBal = CurrentBal + decimal.Parse(DebitAmt) + CreditAmt;

            }
            htmlStr.Append("<table style='width:100%;'><tr><td colspan='5' style='text-align:center;'>" + Header + "</td></tr></table>");
            htmlStr.Append("<a onclick='BackMonth()' class='backCss'><<</a><table  id='DetailGrid' class='lastdatatable table table-hover table-bordered' >");
            htmlStr.Append("<thead>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<th style='width: 65px;'>Voucher Date</th>");
            htmlStr.Append("<th>Particulars</th>");
            htmlStr.Append("<th>Vch Type</th>");
            htmlStr.Append("<th>Office Name</th>");
            htmlStr.Append("<th style='width:70px !important'>Vch No.</th>");
            htmlStr.Append("<th>Debit Amt.</th>");
            htmlStr.Append("<th>Credit Amt.</th>");
            htmlStr.Append("<th class='hide_print'>Action</th>");
            htmlStr.Append("</tr>");
            htmlStr.Append("</thead>");

            htmlStr.Append("<tbody>");

            string ValidDays = "No";

            int Count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < Count; i++)
            {

                ValidDays = "No";
                ValidDays = ds.Tables[0].Rows[i]["V_Editright"].ToString();

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");

                // Inner Transaction
                ds1 = null;
                ds1 = objdbWeb.ByProcedure("SpFinRptTrialBalanceDetailed", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", }, new string[] { "12", ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString(), LedgerID }, "dataset");
                int Count1 = ds1.Tables[0].Rows.Count;

                string Narration = ds1.Tables[1].Rows[0]["VoucherTx_Narration"].ToString();

                if (Count1 > 1)
                {

                    htmlStr.Append("<td style='min-width: 346px;'><p class='HideRecord' style='display:none;'>(As per Details) <br/></p>");
                    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                    {

                        for (int j = 0; j < Count1; j++)
                        {
                            if (j == 0)
                            {
                                htmlStr.Append("\n<p class='subledger'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                            }
                            else
                            {
                                htmlStr.Append("\n<p class='subledger HideRecord'  style='display:none;'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                            }

                            htmlStr.Append("\t \t \t<span class='Ledger_Amt HideRecord' style='display:none;'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                            htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span><br/></p>");
                            //htmlStr.Append("<br/>");

                        }
                    }

                    htmlStr.Append("\n<p class='subledger HideRecord' style='display:none;'><span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span></p>");
                    htmlStr.Append("</td>");
                }
                else
                {
                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                    htmlStr.Append("\n<p class='subledger HideRecord' style='display:none;'><span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span></p>");
                    htmlStr.Append("</td>");
                }


                //  htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");


                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                htmlStr.Append("<td  class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdbWeb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdbWeb.Encrypt("1") + "&Office_ID=" + objdbWeb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a> ");

                if (ds.Tables[0].Rows[i]["Office_ID"].ToString() == Office_ID)
                {
                    if (ValidDays != "No")
                    {
                        htmlStr.Append("<a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdbWeb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdbWeb.Encrypt("2") + "' target='_blank'>Edit</a>");
                    }
                }
                htmlStr.Append("</td>");
                htmlStr.Append("</tr>");
            }
            htmlStr.Append("</tbody>");
            htmlStr.Append("<tfoot>");

            //OPENING BALANCE TOTAL
            htmlStr.Append("<tr>");
            htmlStr.Append("<td></td>");
            htmlStr.Append("<td><b>OPENING BALANCE :</b></td>");
            htmlStr.Append("<td></td>");
            htmlStr.Append("<td></td>");
            htmlStr.Append("<td></td>");
            string TotalBAL = "";
            if (OpeningBal < 0)
            {
                TotalBAL = "";

                if (Math.Abs(OpeningBal) != 0)
                    TotalBAL = Math.Abs(OpeningBal).ToString();

                htmlStr.Append("<td class='align-right'><b>" + TotalBAL.ToString() + "</b></td>");
                htmlStr.Append("<td></td>");

            }
            else
            {
                TotalBAL = "";
                htmlStr.Append("<td></td>");

                if (Math.Abs(OpeningBal) != 0)
                    TotalBAL = Math.Abs(OpeningBal).ToString();

                htmlStr.Append("<td class='align-right'><b>" + TotalBAL.ToString() + "</b></td>");
            }
            htmlStr.Append("<td  class='hide_print'></td>");
            htmlStr.Append("</tr>");

            //CURRENT TOTAL
            htmlStr.Append("<tr>");
            htmlStr.Append("<td></td>");
            htmlStr.Append("<td><b>CURRENT TOTAL :</b></td>");
            htmlStr.Append("<td></td>");
            htmlStr.Append("<td></td>");
            htmlStr.Append("<td></td>");

            //DebitTotal = 0;
            //CreditTotal = 0;
            htmlStr.Append("<td class='align-right'><b>" + Math.Abs(DebitTotal).ToString() + "</b></td>");
            htmlStr.Append("<td class='align-right'><b>" + Math.Abs(CreditTotal).ToString() + "</b></td>");
            //if (CurrentBal < 0)
            //{
            //    htmlStr.Append("<td class='align-right'><b>" + Math.Abs(CurrentBal).ToString() + "</b></td>");
            //    htmlStr.Append("<td></td>");

            //}
            //else
            //{
            //    htmlStr.Append("<td></td>");
            //    htmlStr.Append("<td class='align-right'><b>" + Math.Abs(CurrentBal).ToString() + "</b></td>");
            //}
            htmlStr.Append("<td class='hide_print'></td>");
            htmlStr.Append("</tr>");
            //CLOSING BALANCE TOTAL
            htmlStr.Append("<tr>");
            htmlStr.Append("<td></td>");
            htmlStr.Append("<td><b>CLOSING BALANCE : </b></td>");
            htmlStr.Append("<td></td>");
            htmlStr.Append("<td></td>");
            htmlStr.Append("<td></td>");
            if ((OpeningBal + CurrentBal) < 0)
            {
                htmlStr.Append("<td class='align-right'><b>" + Math.Abs((OpeningBal + CurrentBal)).ToString() + "</b></td>");
                htmlStr.Append("<td></td>");

            }
            else
            {
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td class='align-right'><b>" + Math.Abs((OpeningBal + CurrentBal)).ToString() + "</b></td>");
            }
            htmlStr.Append("<td class='hide_print'></td>");
            htmlStr.Append("</tr>");
            htmlStr.Append("</tfoot>");
            htmlStr.Append("</table>");


        }

        return htmlStr.ToString();
    }
    protected void ddlRegionalOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillOffice();
    }
    protected void GetNotOpenLedgerList()
    {
        try
        {
            DataSet ds1 = objdb.ByProcedure("SpFinRptTrialBalanceDetailed", new string[] { "flag" }, new string[] { "0" }, "dataset");
            if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                ViewState["Ledger_ID_Mlt"] = ds1.Tables[0].Rows[0]["Ledger_ID_Mlt"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
