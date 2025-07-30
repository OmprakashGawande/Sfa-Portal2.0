using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.Drawing;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;  

public partial class mis_Finance_RptLedgerSummary_new : System.Web.UI.Page
{
    static DataSet ds6;
    //DataSet dsDetail, dswithoutDetail;
    DataSet ds;
  
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    string SearchString = "";  
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
                    ViewState["Search"] = "0";
                   
                    //ddlOffice.Enabled = false;
                    divExcel.Visible = false;
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    string FY = GetCurrentFinancialYear();
                    string[] YEAR = FY.Split('-');
                    DateTime FromDate = new DateTime(int.Parse(YEAR[0]), 4, 1);
                    txtFromDate.Text = FromDate.ToString("dd/MM/yyyy");
                    ViewState["FY_StartDate"] = FromDate.ToString("dd/MM/yyyy");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    //Session["dsDetail"] = null;
                    //Session["dswithoutDetail"] = null;
                    FillVoucherDate();
                    FillFromDate();

                    FillDropdown();
                    GetLedgerName();
                    GetCommonSearch();
                    ddlOffice_SelectedIndexChanged(sender, e);
                    HeadList();
                    if (ViewState["Office_ID"].ToString() != "1")
                    {
                        //ddlOffice.Enabled = false;
                    }
                    btnBack.Enabled = false;
                    btnBackN.Enabled = false;

                    chkOpeningBal.Checked = true;
                    //chkOpeningBal.Checked = false;
                    chkDebitAmt.Checked = true;
                    chkCreditAmt.Checked = true;
                    chkClosingBal.Checked = true;
                    chkNarration.Checked = true;
                    chkOppositeLedger.Checked = false;
                    //chkOppositeLedger.Checked = true;

                    ViewState["DayBookVisible"] = "true";
                    btnShowDetailBook.Enabled = false;
                    btnBillByBill.Visible = false;

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
                    if (ViewState["OfficeType_Title"].ToString() == "Circle Office (CCF)")
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

            //ds = objdb.ByProcedure("SpFinRptTrialBalanceNewFF",
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
    //protected void FillDropdown()
    //{
    //    try
    //    {
    //        if (ViewState["Office_ID"].ToString() == "1")
    //        {
    //            ddlOffice.Enabled = true;
    //        }
    //        ds = objdb.ByProcedure("SpFinRptTrialBalanceNew",
    //               new string[] { "flag" },
    //               new string[] { "0" }, "dataset");
    //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlOffice.DataSource = ds;
    //            ddlOffice.DataTextField = "Office_Name";
    //            ddlOffice.DataValueField = "Office_ID";
    //            ddlOffice.DataBind();
    //            //ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
    //            ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
    //        }
    //        //string Office = "";
    //        //foreach (ListItem item in ddlOffice.Items)
    //        //{
    //        //    if (item.Selected)
    //        //    {
    //        //        Office += item.Value + ",";
    //        //    }
    //        //}
    //        //if (Office != "")
    //        //{
    //        //    DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary_new", new string[] { "flag", "Office_ID_Mlt" }, new string[] { "1", Office }, "dataset");
    //        //    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
    //        //    {
    //        //        ddlLedger.DataSource = ds1;
    //        //        ddlLedger.DataTextField = "Ledger_Name";
    //        //        ddlLedger.DataValueField = "Ledger_ID";
    //        //        ddlLedger.DataBind();
    //        //        ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

    //        //    }
    //        //}

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

    //    }
    //}
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
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            //ddlLedger.ClearSelection();
            //ddlLedger.DataSource = null;
            //ddlLedger.DataBind();
            DivTable.InnerHtml = "";
            btnBack.Enabled = false;
            btnBackN.Enabled = false;
            btnBillByBill.Visible = false;
            lblTab.Text = "";
            GetLedgerName();
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
            //    DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary_new", new string[] { "flag", "Office_ID_Mlt" }, new string[] { "1", Office }, "dataset");
            //    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            //    {
            //        ddlLedger.DataSource = ds1;
            //        ddlLedger.DataTextField = "Ledger_Name";
            //        ddlLedger.DataValueField = "Ledger_ID";
            //        ddlLedger.DataBind();
            //        ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

            //    }
            //}
            //if (ddlOffice.SelectedIndex > 0)
            //{
            //    DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary_new", new string[] { "flag", "Office_ID" }, new string[] { "1", ddlOffice.SelectedValue.ToString() }, "dataset");
            //    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            //    {
            //        ddlLedger.DataSource = ds1;
            //        ddlLedger.DataTextField = "Ledger_Name";
            //        ddlLedger.DataValueField = "Ledger_ID";
            //        ddlLedger.DataBind();
            //        ddlLedger.Items.Insert(0, new ListItem("Select", "0"));

            //    }
            //}

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //protected void ddlLedger_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        lblReportName.Text = "";
    //        DivTable.InnerHtml = "";
    //        btnBack.Enabled = false;
    //        btnBackN.Enabled = false;
    //        btnBillByBill.Visible = false;
    //        lblTab.Text = "";
    //        //if (ddlOffice.SelectedIndex >0 && ddlLedger.SelectedIndex > 0 && txtFromDate.Text !="" && txtToDate.Text !="")
    //        //{
    //        //    lblReportName.Text = "Ledger Of : " + txtLedgerName.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
    //        //    FillGrid();
    //        //}

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    //protected void FillGrid()
    //{
    //    try
    //    {
    //        ViewState["DayBookVisible"] = "true";
    //        ViewState["Search"] = "0";
    //        btnShowDetailBook.Enabled = false;
    //        GridView4.DataSource = null;
    //        GridView4.DataBind();
    //        string HeadName = "";

    //        DivTable.InnerHtml = "";
    //        string Office = "";
    //        string OfficeName = "";
    //        int SerialNo = 0;
    //        int totalListItem = ddlOffice.Items.Count;
    //        foreach (ListItem item in ddlOffice.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                SerialNo++;
    //                Office += item.Value + ",";
    //                OfficeName += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
    //            }
    //        }
    //        if (totalListItem == SerialNo)
    //        {
    //            OfficeName = "All Offices";
    //        }
    //        else if (SerialNo == 0)
    //        {
    //            OfficeName = "---Office Not Selected---";
    //        }
    //        else
    //        {
    //            OfficeName = OfficeName.Remove(OfficeName.Length - 1, 1);
    //        }

    //        string sDate = (Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd")).ToString();
    //        DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
    //        int Month = int.Parse(datevalue.Month.ToString());
    //        int Year = int.Parse(datevalue.Year.ToString());
    //        int FY = Year;
    //        string FinancialYear = Year.ToString();
    //        string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
    //        FinancialYear = "";
    //        int FY_Start = FY;
    //        if (Month <= 3)
    //        {
    //            FY = Year - 1;
    //            FinancialYear = FY.ToString() + "-" + LFY.ToString();
    //            FY_Start = FY;
    //        }
    //        else
    //        {

    //            FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
    //        }

    //        DateTime StartDate = new DateTime(FY_Start, 4, 1);
    //        string FY_StartDate = StartDate.ToString("dd/MM/yyyy");


    //        ds = objdb.ByProcedure("SpFinRptLedgerSummary_new", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate", "FinancialYear", "FY_StartDate" }, new string[] { "2", Office, hfLedgerID.Value, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), FinancialYear, Convert.ToDateTime(FY_StartDate, cult).ToString("yyyy/MM/dd") }, "dataset");
    //        if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
    //        {
    //            //string clsHide = "";
    //            //if (chkClosingBal.Checked =true)
    //            //    clsHide = "hidden";
    //            HeadName = ds.Tables[2].Rows[0]["Head_Name"].ToString();
    //            btnShowDetailBook.Enabled = true;
    //            DivTable.Visible = true;
    //            divExcel.Visible = true;

    //            btnBack.Enabled = true;
    //            btnBackN.Enabled = true;
    //            StringBuilder htmlStr = new StringBuilder();


    //            htmlStr.Append("<table  id='DetailGrid' class='datatable table table-hover table-bordered' >");
    //            htmlStr.Append("<thead>");
    //            htmlStr.Append("<tr>");
    //            htmlStr.Append("<th>Voucher Date</th>");
    //            htmlStr.Append("<th>Particulars</th>");
    //            htmlStr.Append("<th>Vch Type</th>");
    //            htmlStr.Append("<th>Vch No.</th>");
    //            if (chkDebitAmt.Checked == true)
    //                htmlStr.Append("<th>Debit Amt.</th>");
    //            if (chkCreditAmt.Checked == true)
    //                htmlStr.Append("<th>Credit Amt.</th>");

    //            int Count = ds.Tables[0].Rows.Count;
    //            decimal OpeningBalance = 0;
    //            decimal TDebitAmt = 0;
    //            decimal TCreditAmt = 0;
    //            string AmtType = "";
    //            decimal PreOpening = 0;

    //            if (ds.Tables[0].Rows[0]["OpeningBalance"].ToString() != "")
    //                OpeningBalance = decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString());

    //            if (ds.Tables[1].Rows[0]["PreOpening"].ToString() != "")
    //                PreOpening = decimal.Parse(ds.Tables[1].Rows[0]["PreOpening"].ToString());

    //            if (chkOpeningBal.Checked == true)
    //            {
    //                OpeningBalance = OpeningBalance + PreOpening;
    //            }

    //            ViewState["OpeningBalance"] = OpeningBalance.ToString();


    //            if (chkClosingBal.Checked == true)
    //                htmlStr.Append("<th class='ClosingBal'>Closing Bal.</th>");
    //            //htmlStr.Append("<th class='ClosingBal'>Closing Bal. <br />\n<p class='subledger'>" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString())) + " " + AmtType + "</p></th>");
    //            htmlStr.Append("<th style='width: 65px;'  class='hide_print'>Action</th>");
    //            htmlStr.Append("</tr>");
    //            htmlStr.Append("</thead>");
    //            htmlStr.Append("<tbody>");

    //            if (chkOpeningBal.Checked == true)
    //            {
    //                //if (OpeningBalance >= 0)
    //                //    lblReportName.Text = lblReportName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
    //                //else
    //                //    lblReportName.Text = lblReportName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";


    //                if (OpeningBalance >= 0)
    //                {
    //                    lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span><br>[" + OfficeName + "]</p>";

    //                    HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Cr";
    //                }
    //                else
    //                {
    //                    lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + "</span> <br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span><br>[" + OfficeName + "]</p>";

    //                    HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Dr";
    //                }
    //            }
    //            else
    //            {

    //                lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<br>[" + OfficeName + "]</p>";

    //                HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
    //                //OpeningBalance = 0;
    //            }


    //            //Opening Balance Row Start
    //            if (chkOpeningBal.Checked == true)
    //            {
    //                htmlStr.Append("<tr style='background-color: antiquewhite; font-weight: 700;'>");
    //                htmlStr.Append("<td></td>");
    //                htmlStr.Append("<td>Opening Balance</td>");
    //                htmlStr.Append("<td></td>");
    //                htmlStr.Append("<td></td>");


    //                if (chkDebitAmt.Checked == true)
    //                    htmlStr.Append("<td></td>");
    //                if (chkCreditAmt.Checked == true)
    //                    htmlStr.Append("<td></td>");
    //                if (OpeningBalance >= 0)
    //                    htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
    //                else
    //                    htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");

    //                htmlStr.Append("<td  class='hide_print'></td>");
    //                htmlStr.Append("</tr>");
    //            }
    //            //Opening Balance Row End

    //            string ValidDays = "No";
    //            var watch = System.Diagnostics.Stopwatch.StartNew();
    //            for (int i = 0; i < Count; i++)
    //            {
    //                decimal DebitAmt = 0;
    //                decimal CreditAmt = 0;
    //                decimal OpeningBal = 0;
    //                decimal tempOpeningBal = 0;
    //                ValidDays = "No";
    //                ValidDays = ds.Tables[0].Rows[i]["V_Editright"].ToString();

    //                if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
    //                    DebitAmt = decimal.Parse("-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString());
    //                if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
    //                    CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());

    //                if (chkCreditAmt.Checked == true && chkDebitAmt.Checked == true)
    //                {
    //                    tempOpeningBal = OpeningBalance;
    //                    OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
    //                    OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
    //                    TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
    //                    TCreditAmt = TCreditAmt + CreditAmt;

    //                    if (OpeningBal >= 0)
    //                        AmtType = "Cr";
    //                    else
    //                        AmtType = "Dr";


    //                    htmlStr.Append("<tr>");
    //                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
    //                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
    //                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
    //                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
    //                    if (chkDebitAmt.Checked == true)
    //                        htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
    //                    if (chkCreditAmt.Checked == true)
    //                        htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
    //                    if (chkClosingBal.Checked == true)
    //                        htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");

    //                    htmlStr.Append("<td class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a> ");
    //                    if (ds.Tables[0].Rows[i]["Office_ID"].ToString() == ViewState["Office_ID"].ToString())
    //                    {
    //                        if (ValidDays != "No")
    //                        {
    //                            htmlStr.Append("<a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("2") + "' target='_blank'>Edit</a>");
    //                        }
    //                    }
    //                    htmlStr.Append("</td>");

    //                    htmlStr.Append("</tr>");
    //                }
    //                else if (chkCreditAmt.Checked == true && ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
    //                {
    //                    decimal CreditAmt1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
    //                    if (CreditAmt1 > 0)
    //                    {
    //                        tempOpeningBal = OpeningBalance;
    //                        OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
    //                        OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
    //                        TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
    //                        TCreditAmt = TCreditAmt + CreditAmt;

    //                        if (OpeningBal >= 0)
    //                            AmtType = "Cr";
    //                        else
    //                            AmtType = "Dr";

    //                        htmlStr.Append("<tr>");
    //                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");

    //                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
    //                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
    //                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
    //                        if (chkDebitAmt.Checked == true)
    //                            htmlStr.Append("<td class='align-right'></td>");
    //                        if (chkCreditAmt.Checked == true)
    //                            htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
    //                        if (chkClosingBal.Checked == true)
    //                            htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");
    //                        htmlStr.Append("<td class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a> ");
    //                        if (ds.Tables[0].Rows[i]["Office_ID"].ToString() == ViewState["Office_ID"].ToString())
    //                        {
    //                            if (ValidDays != "No")
    //                            {
    //                                htmlStr.Append(" <a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("2") + "' target='_blank'>Edit</a>");
    //                            }
    //                        }
    //                        htmlStr.Append("</td>");

    //                        htmlStr.Append("</tr>");
    //                    }

    //                }
    //                else if (chkDebitAmt.Checked == true && ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
    //                {
    //                    decimal DebitAmt1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["DebitAmt"].ToString());
    //                    if (DebitAmt1 > 0)
    //                    {
    //                        tempOpeningBal = OpeningBalance;
    //                        OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
    //                        OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
    //                        TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
    //                        TCreditAmt = TCreditAmt + CreditAmt;

    //                        if (OpeningBal >= 0)
    //                            AmtType = "Cr";
    //                        else
    //                            AmtType = "Dr";

    //                        htmlStr.Append("<tr>");
    //                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
    //                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");
    //                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
    //                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
    //                        if (chkDebitAmt.Checked == true)
    //                            htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
    //                        if (chkCreditAmt.Checked == true)
    //                            htmlStr.Append("<td class='align-right'></td>");
    //                        if (chkClosingBal.Checked == true)
    //                            htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");
    //                        htmlStr.Append("<td class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a> ");
    //                        if (ds.Tables[0].Rows[i]["Office_ID"].ToString() == ViewState["Office_ID"].ToString())
    //                        {
    //                            if (ValidDays != "No")
    //                            {
    //                                htmlStr.Append("<a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("2") + "' target='_blank'>Edit</a>");
    //                            }
    //                        }
    //                        htmlStr.Append("</td>");


    //                        htmlStr.Append("</tr>");
    //                    }
    //                }

    //            }
    //            watch.Stop();
    //            var elapsedMs = watch.ElapsedMilliseconds;

    //            //htmlStr.Append("</tbody>");
    //            //htmlStr.Append("<tfoot>");
    //            htmlStr.Append("<tr style='font-weight: 700;'>");
    //            htmlStr.Append("<td></td>");
    //            htmlStr.Append("<td></td>");
    //            htmlStr.Append("<td>Total :</td>");

    //            if (decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString()) >= 0)
    //                AmtType = "Cr";
    //            else
    //                AmtType = "Dr";

    //            htmlStr.Append("<td></td>");

    //            if (OpeningBalance >= 0)
    //                AmtType = "Cr";
    //            else
    //                AmtType = "Dr";
    //            if (chkDebitAmt.Checked == true)
    //                htmlStr.Append("<td class='align-right'>" + TDebitAmt.ToString() + "</td>");
    //            if (chkCreditAmt.Checked == true)
    //                htmlStr.Append("<td class='align-right'>" + TCreditAmt.ToString() + "</td>");
    //            if (chkClosingBal.Checked == true && chkDebitAmt.Checked == false && chkCreditAmt.Checked == false)
    //            {
    //                decimal? Cr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
    //                decimal? Dr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));

    //                Dr = decimal.Parse("-" + Dr.ToString());
    //                OpeningBalance = OpeningBalance + decimal.Parse(Cr.ToString()) + decimal.Parse(Dr.ToString());
    //                if (OpeningBalance >= 0)
    //                {
    //                    htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
    //                }
    //                else
    //                {
    //                    htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");
    //                }

    //            }
    //            else if (chkClosingBal.Checked == true)
    //            {
    //                htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " " + AmtType + "</td>");
    //            }

    //            htmlStr.Append("<td class='hide_print'></td>");
    //            htmlStr.Append("</tr>");
    //            htmlStr.Append("</tbody>");
    //            // htmlStr.Append("</tfoot>");
    //            htmlStr.Append("</table>");

    //            DivTable.InnerHtml = htmlStr.ToString();

    //        }
    //        else if (ds.Tables.Count != 0 && ds.Tables[1].Rows.Count != 0)
    //        {
    //            decimal OpeningBalance = 0;
    //            decimal PreOpening = 0;


    //            HeadName = ds.Tables[2].Rows[0]["Head_Name"].ToString();

    //            if (ds.Tables[1].Rows[0]["OpeningBalance"].ToString() != "")
    //                OpeningBalance = decimal.Parse(ds.Tables[1].Rows[0]["OpeningBalance"].ToString());

    //            if (ds.Tables[1].Rows[0]["PreOpening"].ToString() != "")
    //                PreOpening = decimal.Parse(ds.Tables[1].Rows[0]["PreOpening"].ToString());

    //            if (chkOpeningBal.Checked == true)
    //            {
    //                OpeningBalance = OpeningBalance + PreOpening;
    //            }

    //            if (chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
    //            {
    //                if (OpeningBalance >= 0)
    //                {
    //                    lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>  & " + "<span style='color:#d03535;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span><br>[" + OfficeName + "]</p>";
    //                    HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Cr  & " + "<span style='color:#d03535;'> Closing Balance - " + Math.Abs(OpeningBalance).ToString() + " Cr";
    //                }
    //                else
    //                    lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span> & " + "<span style='color:#d03535;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span><br>[" + OfficeName + "]</p>";
    //                HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Dr & " + " Closing Balance - " + Math.Abs(OpeningBalance).ToString() + " Dr";
    //            }
    //            else if (chkOpeningBal.Checked == true)
    //            {
    //                if (OpeningBalance >= 0)
    //                {
    //                    lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span><br>[" + OfficeName + "]</p>";
    //                    HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Cr";
    //                }
    //                else
    //                {
    //                    lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span><br>[" + OfficeName + "]</p>";
    //                    HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Dr";
    //                }
    //            }
    //            else if (chkClosingBal.Checked == true)
    //            {
    //                if (OpeningBalance >= 0)
    //                {
    //                    lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span><br>[" + OfficeName + "]</p>";
    //                    HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Closing Balance - " + Math.Abs(OpeningBalance).ToString() + " Cr";
    //                }
    //                else
    //                {
    //                    lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span><br>[" + OfficeName + "]</p>";
    //                    HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Closing Balance - " + Math.Abs(OpeningBalance).ToString() + " Dr";
    //                }
    //            }
    //        }
    //        else
    //        {
    //            lblTab.Text = "No record found.";
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

    //    }
    //}
    protected void FillGridTesting(string VouhcerNo)
    {
        try
        {
            lblTotal.Text = "";
            lblDetailTotal.Text = "";
            spnSearch.Visible = false;
            ViewState["DayBookVisible"] = "true";
            ViewState["Search"] = "0";
            btnShowDetailBook.Enabled = false;
            txtSearch.Visible = false;
            GridView4.DataSource = null;
            GridView4.DataBind();
            gvDetail1.DataSource = null;
            gvDetail1.DataBind();
            string HeadName = "";
            string Opening = "0";
            DivTable.InnerHtml = "";
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
            if (chkOpeningBal.Checked == true)
            {
                
                Opening = "1";
            }
            DateTime StartDate = new DateTime(FY_Start, 4, 1);
            string FY_StartDate = StartDate.ToString("dd/MM/yyyy");


            ds = objdb.ByProcedure("SpFinRptLedgerSummary_new", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate", "FinancialYear", "FY_StartDate", "OpeningStatus", "Office_ID" }, new string[] { "2", Office, hfLedgerID.Value, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), FinancialYear, Convert.ToDateTime(FY_StartDate, cult).ToString("yyyy/MM/dd"), Opening, ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
               
                //string clsHide = "";
                //if (chkClosingBal.Checked =true)
                //    clsHide = "hidden";

                txtSearch.Visible = true;
                spnSearch.Visible = true;
                HeadName = ds.Tables[2].Rows[0]["Head_Name"].ToString();
                btnShowDetailBook.Enabled = true;
                DivTable.Visible = true;
                divExcel.Visible = true;

                btnBack.Enabled = true;
                btnBackN.Enabled = true;
                
                decimal OpeningBalance = 0;
               
                decimal PreOpening = 0;

                if (ds.Tables[0].Rows[0]["OpeningBalance"].ToString() != "")
                    OpeningBalance = decimal.Parse(ds.Tables[1].Rows[0]["OpeningBalance"].ToString());

                if (ds.Tables[1].Rows[0]["PreOpening"].ToString() != "")
                    PreOpening = decimal.Parse(ds.Tables[1].Rows[0]["PreOpening"].ToString());

                if (chkOpeningBal.Checked == true)
                {
                    OpeningBalance = OpeningBalance + PreOpening;
                  
                }
               
                
                ViewState["OpeningBalance"] = OpeningBalance.ToString();
              

                

                if (chkOpeningBal.Checked == true)
                {
                    //if (OpeningBalance >= 0)
                    //    lblReportName.Text = lblReportName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                    //else
                    //    lblReportName.Text = lblReportName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";


                    if (OpeningBalance >= 0)
                    {
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span><br>[" + OfficeName + "]</p>";

                        HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Cr";
                    }
                    else
                    {
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + "</span> <br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span><br>[" + OfficeName + "]</p>";

                        HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Dr";
                    }
                }
                else
                {

                    lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<br>[" + OfficeName + "]</p>";

                    HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
                    //OpeningBalance = 0;
                }
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    
                    //DataTable dt = new DataTable();
                    //dt = ds.Tables[0];
                    ////dswithoutDetail=ds;
                    //Session["dswithoutDetail"] = ds;
                    if (VouhcerNo == "0")
                    {
                        GvDetail.DataSource = ds.Tables[0];
                        GvDetail.DataBind();
                       
                    }
                    else
                    {
                        DataTable dt = ds.Tables[0];
                        DataView dv = new DataView(dt);
                        string SearchExpression = null;
                        //check if value is null or not, if not search it
                        if (!String.IsNullOrEmpty(txtSearch.Text))
                        {
                            string[] Searchstr = txtSearch.Text.Split('[');
                            
                            SearchExpression = string.Format("{0} '%{1}%'",
                            GvDetail.SortExpression, Searchstr[0]);
                            dv.RowFilter = "VoucherTx_No like" + SearchExpression + "or  VoucherTx_Type like" + SearchExpression + "or  Ledger_Name like" + SearchExpression; ;
                            //dv.RowFilter = "VoucherTx_Type like" + SearchExpression;
                            //dv.RowFilter = "Ledger_Name like" + SearchExpression;
                            GvDetail.DataSource = dv;
                            GvDetail.DataBind();

                        }

                        GvDetail.DataSource = dv;
                        GvDetail.DataBind();
                      
                    }
                     lblTotal.Text = "Total Rows: " + ds.Tables[0].Rows.Count;
                    //GvDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //GvDetail.UseAccessibleHeader = true;
                    
                    if (chkDebitAmt.Checked == false)
                    {
                        GvDetail.Columns[4].Visible = false;
                    }
                    else
                    {
                        GvDetail.Columns[4].Visible = true;
                    }
                    if (chkCreditAmt.Checked == false)
                    {
                        GvDetail.Columns[5].Visible = false;
                    }
                    else
                    {
                        GvDetail.Columns[5].Visible = true;
                    }
                    if (chkClosingBal.Checked == false)
                    {
                        GvDetail.Columns[6].Visible = false;
                    }
                    else
                    {
                        GvDetail.Columns[6].Visible = true;
                    }

                    int Count = ds.Tables[0].Rows.Count;
                    decimal? Cr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
                    decimal? Dr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));
                    //decimal? CB = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CB"));
                    decimal? CB = decimal.Parse(ds.Tables[0].Rows[Count - 1]["CB"].ToString());
		            decimal? CBType = decimal.Parse(ds.Tables[0].Rows[Count - 1]["CBType"].ToString());
                    string AmtType = "";
                    if (CB >= 0)
                    {
                        AmtType = "Cr";
                    }
                    else
                    {
                        AmtType = "Dr";
                    }
		   
                    GvDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
                    GvDetail.FooterRow.Cells[4].Text = "<b>" + Dr.ToString() + "</b>";
                    GvDetail.FooterRow.Cells[5].Text = "<b>" + Cr.ToString() + "</b>";
                    GvDetail.FooterRow.Cells[6].Text = "<b>" + (CBType .ToString() + " " + AmtType) + "</b>";
                    GvDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    GvDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    GvDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    GvDetail.FooterRow.Cells[7].CssClass = "hide_print";


                    

            }
               
            else if (ds.Tables.Count != 0 && ds.Tables[1].Rows.Count != 0)
            {
                decimal OpeningBalance = 0;
                decimal PreOpening = 0;


                HeadName = ds.Tables[2].Rows[0]["Head_Name"].ToString();

                if (ds.Tables[1].Rows[0]["OpeningBalance"].ToString() != "")
                    OpeningBalance = decimal.Parse(ds.Tables[1].Rows[0]["OpeningBalance"].ToString());

                if (ds.Tables[1].Rows[0]["PreOpening"].ToString() != "")
                    PreOpening = decimal.Parse(ds.Tables[1].Rows[0]["PreOpening"].ToString());

                if (chkOpeningBal.Checked == true)
                {
                    OpeningBalance = OpeningBalance + PreOpening;
                }

                if (chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                {
                    if (OpeningBalance >= 0)
                    {
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>  & " + "<span style='color:#d03535;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span><br>[" + OfficeName + "]</p>";
                        HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Cr  & " + "<span style='color:#d03535;'> Closing Balance - " + Math.Abs(OpeningBalance).ToString() + " Cr";
                    }
                    else
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span> & " + "<span style='color:#d03535;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span><br>[" + OfficeName + "]</p>";
                    HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Dr & " + " Closing Balance - " + Math.Abs(OpeningBalance).ToString() + " Dr";
                }
                else if (chkOpeningBal.Checked == true)
                {
                    if (OpeningBalance >= 0)
                    {
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span><br>[" + OfficeName + "]</p>";
                        HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Cr";
                    }
                    else
                    {
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span><br>[" + OfficeName + "]</p>";
                        HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Dr";
                    }
                }
                else if (chkClosingBal.Checked == true)
                {
                    if (OpeningBalance >= 0)
                    {
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span><br>[" + OfficeName + "]</p>";
                        HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Closing Balance - " + Math.Abs(OpeningBalance).ToString() + " Cr";
                    }
                    else
                    {
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span><br>[" + OfficeName + "]</p>";
                        HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Closing Balance - " + Math.Abs(OpeningBalance).ToString() + " Dr";
                    }
                }
            }
            else
            {
                lblTab.Text = "No record found.";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillGridDetailTesting(string VoucherNo)
    {
        try
        {
	        lblTotal.Text = "";
            lblDetailTotal.Text = "";
            spnSearch.Visible = false;
            ViewState["DayBookVisible"] = "true";
            ViewState["Search"] = "1";
            btnShowDetailBook.Enabled = false;
            txtSearch.Visible = false;
            GridView4.DataSource = null;
            GridView4.DataBind();
            GvDetail.DataSource = null;
            GvDetail.DataBind();
            
            string HeadName = "";
            string NarrationStatus = "0";
            string OppositeledgerStatus = "0";
            string Opening = "0";
            DivTable.InnerHtml = "";
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

            DateTime StartDate = new DateTime(FY_Start, 4, 1);
            string FY_StartDate = StartDate.ToString("dd/MM/yyyy");
            if (chkNarration.Checked == true && chkOppositeLedger.Checked == true)
            {
                NarrationStatus = "1";
                OppositeledgerStatus = "1";
            }
            if(chkNarration.Checked == true)
            {
                NarrationStatus = "1";
            }
            if(chkOppositeLedger.Checked == true)
            {
                OppositeledgerStatus = "1";
            }
            if (chkOpeningBal.Checked == true)
            {

                Opening = "1";
            }
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            ds = objdb.ByProcedure("SpFinRptLedgerSummary_new", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate", "FinancialYear", "FY_StartDate", "NarrationStatus", "OppositeledgerStatus", "OpeningStatus", "Office_ID" }, new string[] { "7", Office, hfLedgerID.Value, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), FinancialYear, Convert.ToDateTime(FY_StartDate, cult).ToString("yyyy/MM/dd"), NarrationStatus, OppositeledgerStatus, Opening, ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                //watch.Stop();
                //var elapsedMs = watch.ElapsedMilliseconds;
                //string clsHide = "";
                //if (chkClosingBal.Checked =true)
                //    clsHide = "hidden";

                txtSearch.Visible = true;
                spnSearch.Visible = true;
                HeadName = ds.Tables[2].Rows[0]["Head_Name"].ToString();
                btnShowDetailBook.Enabled = true;
                DivTable.Visible = true;
                divExcel.Visible = true;

                btnBack.Enabled = true;
                btnBackN.Enabled = true;

                decimal OpeningBalance = 0;

                decimal PreOpening = 0;

                if (ds.Tables[0].Rows[0]["OpeningBalance"].ToString() != "")
                    OpeningBalance = decimal.Parse(ds.Tables[1].Rows[0]["OpeningBalance"].ToString());

                if (ds.Tables[1].Rows[0]["PreOpening"].ToString() != "")
                    PreOpening = decimal.Parse(ds.Tables[1].Rows[0]["PreOpening"].ToString());

                if (chkOpeningBal.Checked == true)
                {
                    OpeningBalance = OpeningBalance + PreOpening;
                }


                ViewState["OpeningBalance"] = OpeningBalance.ToString();




                if (chkOpeningBal.Checked == true)
                {
                    //if (OpeningBalance >= 0)
                    //    lblReportName.Text = lblReportName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                    //else
                    //    lblReportName.Text = lblReportName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";


                    if (OpeningBalance >= 0)
                    {
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span><br>[" + OfficeName + "]</p>";

                        HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Cr";
                    }
                    else
                    {
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + "</span> <br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span><br>[" + OfficeName + "]</p>";

                        HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Dr";
                    }
                }
                else
                {

                    lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<br>[" + OfficeName + "]</p>";

                    HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
                    //OpeningBalance = 0;
                }
                if(VoucherNo == "0")
                {
                    gvDetail1.DataSource = ds.Tables[0];
                    gvDetail1.DataBind();
                   
                }
                else
                {
                    DataTable dt = ds.Tables[0];
                    DataView dv = new DataView(dt);
                    string SearchExpression = null;
                    //check if value is null or not, if not search it
                    if (!String.IsNullOrEmpty(txtSearch.Text))
                    {
                        string[] Searchstr = txtSearch.Text.Split('[');
                            SearchExpression = string.Format("{0} '%{1}%'",
                            GvDetail.SortExpression, Searchstr[0]);
                        
                        dv.RowFilter = "VoucherTx_No like" + SearchExpression + "or  VoucherTx_Type like" + SearchExpression + "or  Ledger_Name like" + SearchExpression + "or  Variable like" + SearchExpression;
                        //dv.RowFilter = "VoucherTx_Type like" + SearchExpression;
                        //dv.RowFilter = "Ledger_Name like" + SearchExpression;
                       gvDetail1.DataSource = dv;
                       gvDetail1.DataBind();



                    }

                   gvDetail1.DataSource = dv;
                   gvDetail1.DataBind();
                   
                }
		lblDetailTotal.Text = "Total Rows: " + ds.Tables[0].Rows.Count;
                //DataTable dt = new DataTable();
                //dt = ds.Tables[0];
                ////dsDetail = ds;
                //Session["dsDetail"] = ds;
                //ViewState["dtDetailedPaging"] = dt;

                int Count = ds.Tables[0].Rows.Count;
                if (chkDebitAmt.Checked == false)
                {
                    gvDetail1.Columns[4].Visible = false;
                }
                else
                {
                    gvDetail1.Columns[4].Visible = true;
                }
                if (chkCreditAmt.Checked == false)
                {
                    gvDetail1.Columns[5].Visible = false;
                }
                else
                {
                    gvDetail1.Columns[5].Visible = true;
                }
                if (chkClosingBal.Checked == false)
                {
                    gvDetail1.Columns[6].Visible = false;
                }
                else
                {
                    gvDetail1.Columns[6].Visible = true;
                }
                decimal? Cr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
                decimal? Dr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));
                //decimal? CB = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CB"));
                decimal? CB = decimal.Parse(ds.Tables[0].Rows[Count - 1]["CB"].ToString());
                decimal? CBType = decimal.Parse(ds.Tables[0].Rows[Count - 1]["CBType"].ToString());
                string AmtType = "";
                if (CB >= 0)
                {
                    AmtType = "Cr";
                }
                else
                {
                    AmtType = "Dr";
                }
                gvDetail1.FooterRow.Cells[2].Text = "<b>Total : </b>";
                gvDetail1.FooterRow.Cells[4].Text = "<b>" + Dr.ToString() + "</b>";
                gvDetail1.FooterRow.Cells[5].Text = "<b>" + Cr.ToString() + "</b>";
                gvDetail1.FooterRow.Cells[6].Text = "<b>" + (CBType.ToString() + " " + AmtType) + "</b>";
                gvDetail1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                gvDetail1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                gvDetail1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                gvDetail1.FooterRow.Cells[7].CssClass = "hide_print";



            }

            else if (ds.Tables.Count != 0 && ds.Tables[1].Rows.Count != 0)
            {
                decimal OpeningBalance = 0;
                decimal PreOpening = 0;


                HeadName = ds.Tables[2].Rows[0]["Head_Name"].ToString();

                if (ds.Tables[1].Rows[0]["OpeningBalance"].ToString() != "")
                    OpeningBalance = decimal.Parse(ds.Tables[1].Rows[0]["OpeningBalance"].ToString());

                if (ds.Tables[1].Rows[0]["PreOpening"].ToString() != "")
                    PreOpening = decimal.Parse(ds.Tables[1].Rows[0]["PreOpening"].ToString());

                if (chkOpeningBal.Checked == true)
                {
                    OpeningBalance = OpeningBalance + PreOpening;
                }

                if (chkOpeningBal.Checked == true && chkClosingBal.Checked == true)
                {
                    if (OpeningBalance >= 0)
                    {
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>  & " + "<span style='color:#d03535;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span><br>[" + OfficeName + "]</p>";
                        HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Cr  & " + "<span style='color:#d03535;'> Closing Balance - " + Math.Abs(OpeningBalance).ToString() + " Cr";
                    }
                    else
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span> & " + "<span style='color:#d03535;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span><br>[" + OfficeName + "]</p>";
                    HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Dr & " + " Closing Balance - " + Math.Abs(OpeningBalance).ToString() + " Dr";
                }
                else if (chkOpeningBal.Checked == true)
                {
                    if (OpeningBalance >= 0)
                    {
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span><br>[" + OfficeName + "]</p>";
                        HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Cr";
                    }
                    else
                    {
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span><br>[" + OfficeName + "]</p>";
                        HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Dr";
                    }
                }
                else if (chkClosingBal.Checked == true)
                {
                    if (OpeningBalance >= 0)
                    {
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span><br>[" + OfficeName + "]</p>";
                        HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Closing Balance - " + Math.Abs(OpeningBalance).ToString() + " Cr";
                    }
                    else
                    {
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Closing Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span><br>[" + OfficeName + "]</p>";
                        HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Closing Balance - " + Math.Abs(OpeningBalance).ToString() + " Dr";
                    }
                }
            }
            else
            {
                lblTab.Text = "No record found.";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillGridDetail()
    {
        try
        {
            ViewState["DayBookVisible"] = "true";
            btnShowDetailBook.Enabled = false;
            GridView4.DataSource = null;
            GridView4.DataBind();

            ViewState["Search"] = "1";
            string HeadName = "";
            string OfficeName = "";
            int SerialNo = 0;
            int totalListItem = ddlOffice.Items.Count;

            DivTable.InnerHtml = "";
            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {

                    SerialNo++;
                    Office += item.Value + ",";
                    OfficeName += " <span style='color:tomato;'>" + SerialNo + ".</span>" + item.Text + " ,";
                }
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

            DateTime StartDate = new DateTime(FY_Start, 4, 1);
            string FY_StartDate = StartDate.ToString("dd/MM/yyyy");

            ds = objdb.ByProcedure("SpFinRptLedgerSummary_new", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate", "FinancialYear", "FY_StartDate" }, new string[] { "2", Office, hfLedgerID.Value, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), FinancialYear, Convert.ToDateTime(FY_StartDate, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                HeadName = ds.Tables[2].Rows[0]["Head_Name"].ToString();

                btnShowDetailBook.Enabled = true;
                DivTable.Visible = true;

                btnBack.Enabled = true;
                btnBackN.Enabled = true;
                StringBuilder htmlStr = new StringBuilder();


                htmlStr.Append("<table  id='DetailGrid' class='datatable table table-hover table-bordered' >");
                htmlStr.Append("<thead>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<th style='width: 65px;'>Voucher Date</th>");
                htmlStr.Append("<th>Particulars</th>");
                htmlStr.Append("<th>Vch Type</th>");
                htmlStr.Append("<th style='min-width: 10%;'>Vch No.</th>");
                if (chkDebitAmt.Checked == true)
                    htmlStr.Append("<th>Debit Amt.</th>");
                if (chkCreditAmt.Checked == true)
                    htmlStr.Append("<th>Credit Amt.</th>");

                int Count = ds.Tables[0].Rows.Count;
                decimal OpeningBalance = 0;
                decimal TDebitAmt = 0;
                decimal TCreditAmt = 0;
                string AmtType = "";
                decimal PreOpening = 0;

                if (ds.Tables[0].Rows[0]["OpeningBalance"].ToString() != "")
                    OpeningBalance = decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString());

                if (ds.Tables[1].Rows[0]["PreOpening"].ToString() != "")
                    PreOpening = decimal.Parse(ds.Tables[1].Rows[0]["PreOpening"].ToString());

                if (chkOpeningBal.Checked == true)
                {
                    OpeningBalance = OpeningBalance + PreOpening;
                }

                ViewState["OpeningBalance"] = OpeningBalance.ToString();
                if (chkClosingBal.Checked == true)
                    htmlStr.Append("<th class='ClosingBal'>Closing Bal.</th>");
                // htmlStr.Append("<th class='ClosingBal'>Closing Bal. <br />\n<p class='subledger'>" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString())) + " " + AmtType + "</p></th>");

                htmlStr.Append("<th class='hide_print' style='width: 65px;'>Action</th>");
                htmlStr.Append("</tr>");
                htmlStr.Append("</thead>");
                htmlStr.Append("<tbody>");

                if (chkOpeningBal.Checked == true)
                {

                    //if (OpeningBalance >= 0)
                    //    lblReportName.Text = lblReportName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span>";
                    //else
                    //    lblReportName.Text = lblReportName.Text + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span>";
                    if (OpeningBalance >= 0)
                    {
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Cr</span><br>[" + OfficeName + "]</p>";

                        HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Cr";
                    }
                    else
                    {
                        lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<span style='color:#d03535;'> Opening Bal. : " + Math.Abs(OpeningBalance).ToString() + " Dr</span><br>[" + OfficeName + "]</p>";

                        HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + " Opening Balance - " + Math.Abs(OpeningBalance).ToString() + " Dr";
                    }
                }
                else
                {

                    lblReportName.Text = "<p class='text-center' style='font-weight:600'> SFA Technologies Pvt. Ltd. <br/>Ledger Of : <span style='text-transform: uppercase;font-size: 17px;'>" + txtLedgerName.Text + " </span><br/> Head Name : " + HeadName + " <br>( " + txtFromDate.Text + " - " + txtToDate.Text + " )" + "<br>[" + OfficeName + "]</p>";

                    HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + " [ Head Name : " + HeadName + " ]( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
                    //OpeningBalance = 0;
                }

                //Opening Balance Row Start
                if (chkOpeningBal.Checked == true)
                {
                    htmlStr.Append("<tr style='background-color: antiquewhite; font-weight: 700;'>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Opening Balance</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");


                    if (chkDebitAmt.Checked == true)
                        htmlStr.Append("<td></td>");
                    if (chkCreditAmt.Checked == true)
                        htmlStr.Append("<td></td>");

                    if (OpeningBalance >= 0)
                        htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                    else
                        htmlStr.Append("<td  class='align-right'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");

                    htmlStr.Append("<td class='hide_print'></td>");
                    htmlStr.Append("</tr>");
                }
                //Opening Balance Row End
                string ValidDays = "No";
                for (int i = 0; i < Count; i++)
                {
                    decimal DebitAmt = 0;
                    decimal CreditAmt = 0;
                    decimal OpeningBal = 0;
                    decimal tempOpeningBal = 0;

                    ValidDays = "No";
                    ValidDays = ds.Tables[0].Rows[i]["V_Editright"].ToString();


                    if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                        DebitAmt = decimal.Parse("-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                    if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                        CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());


                    DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary_new", new string[] { "flag", "VoucherTx_ID", "Ledger_ID", }, new string[] { "3", ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString(), hfLedgerID.Value }, "dataset");
                    int Count1 = ds1.Tables[0].Rows.Count;

                    string Narration = ds1.Tables[1].Rows[0]["VoucherTx_Narration"].ToString();
                    if (chkCreditAmt.Checked == true && chkDebitAmt.Checked == true)
                    {
                        tempOpeningBal = OpeningBalance;
                        OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                        OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                        TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                        TCreditAmt = TCreditAmt + CreditAmt;

                        if (OpeningBal >= 0)
                            AmtType = "Cr";
                        else
                            AmtType = "Dr";



                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                        if (Count1 > 1)
                        {


                            htmlStr.Append("<td>");
                            if (chkOppositeLedger.Checked == true)
                            {
                                htmlStr.Append("(As per Details) <br/>");
                                if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                {

                                    for (int j = 0; j < Count1; j++)
                                    {
                                        htmlStr.Append("\n<p class='subledger'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                                        htmlStr.Append("\t \t \t<span class='Ledger_Amt'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                        htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span></p>");
                                        htmlStr.Append("<br/>");

                                    }
                                }
                            }
                            htmlStr.Append("\n<p class='subledger'>");
                            if (chkNarration.Checked == true)
                                htmlStr.Append("<span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span>");

                            htmlStr.Append("</p>");
                            htmlStr.Append("</td>");
                        }
                        else
                        {
                            if (chkOppositeLedger.Checked == true)
                            {
                                htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                            }
                            else
                            {
                                htmlStr.Append("<td>");
                            }
                            htmlStr.Append("\n<p class='subledger'>");
                            if (chkNarration.Checked == true)
                                htmlStr.Append("<span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span>");
                            htmlStr.Append("</p>");
                            htmlStr.Append("</td>");
                        }
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                        if (chkDebitAmt.Checked == true)
                            htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                        if (chkCreditAmt.Checked == true)
                            htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                        if (chkClosingBal.Checked == true)
                            htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");

                        htmlStr.Append("<td  class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a> ");
                        if (ds.Tables[0].Rows[i]["Office_ID"].ToString() == ViewState["Office_ID"].ToString())
                        {
                            if (ValidDays != "No")
                            {
                                htmlStr.Append(" <a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("2") + "' target='_blank'>Edit</a> ");
                            }
                        }
                        htmlStr.Append("</td>");

                        htmlStr.Append("</tr>");
                    }
                    else if (chkCreditAmt.Checked == true && ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                    {
                        decimal CreditAmt1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                        if (CreditAmt1 > 0)
                        {
                            tempOpeningBal = OpeningBalance;
                            OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                            OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                            TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                            TCreditAmt = TCreditAmt + CreditAmt;

                            if (OpeningBal >= 0)
                                AmtType = "Cr";
                            else
                                AmtType = "Dr";

                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                            if (Count1 > 1)
                            {

                                // htmlStr.Append("<td>(As per Details) <br/>");
                                htmlStr.Append("<td>");
                                if (chkOppositeLedger.Checked == true)
                                {
                                    htmlStr.Append("(As per Details) <br/>");
                                    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                    {

                                        for (int j = 0; j < Count1; j++)
                                        {

                                            htmlStr.Append("\n<p class='subledger'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                                            htmlStr.Append("\t \t \t<span class='Ledger_Amt'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                            htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span></p>");
                                            htmlStr.Append("<br/>");

                                        }
                                    }
                                }

                                htmlStr.Append("\n<p class='subledger'>");
                                if (chkNarration.Checked == true)
                                    htmlStr.Append("<span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span>");
                                htmlStr.Append("</p>");
                                htmlStr.Append("</td>");
                            }
                            else
                            {
                                // htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");

                                //htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                                //htmlStr.Append("\n<p class='subledger'><span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span></p>");
                                //htmlStr.Append("</td>");

                                if (chkOppositeLedger.Checked == true)
                                {
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                                }
                                else
                                {
                                    htmlStr.Append("<td>");
                                }
                                htmlStr.Append("\n<p class='subledger'>");
                                if (chkNarration.Checked == true)
                                    htmlStr.Append("<span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span>");
                                htmlStr.Append("</p>");
                                htmlStr.Append("</td>");
                            }
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                            if (chkDebitAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'></td>");
                            if (chkCreditAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["CreditAmt"].ToString() + "</td>");
                            if (chkClosingBal.Checked == true)
                                htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");
                            htmlStr.Append("<td class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a> ");
                            if (ds.Tables[0].Rows[i]["Office_ID"].ToString() == ViewState["Office_ID"].ToString())
                            {
                                if (ValidDays != "No")
                                {
                                    htmlStr.Append(" <a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("2") + "' target='_blank'>Edit</a> ");
                                }
                            }
                            htmlStr.Append("</td>");


                            htmlStr.Append("</tr>");



                        }

                    }
                    else if (chkDebitAmt.Checked == true && ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                    {
                        decimal DebitAmt1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["DebitAmt"].ToString());
                        if (DebitAmt1 > 0)
                        {
                            tempOpeningBal = OpeningBalance;
                            OpeningBal = tempOpeningBal + DebitAmt + CreditAmt;
                            OpeningBalance = OpeningBalance + DebitAmt + CreditAmt;
                            TDebitAmt = TDebitAmt + Math.Abs(DebitAmt);
                            TCreditAmt = TCreditAmt + CreditAmt;

                            if (OpeningBal >= 0)
                                AmtType = "Cr";
                            else
                                AmtType = "Dr";



                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                            if (Count1 > 1)
                            {

                                //htmlStr.Append("<td>(As per Details) <br/>");
                                htmlStr.Append("<td>");
                                if (chkOppositeLedger.Checked == true)
                                {
                                    htmlStr.Append("(As per Details) <br/>");
                                    if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                    {

                                        for (int j = 0; j < Count1; j++)
                                        {


                                            htmlStr.Append("\n<p class='subledger'><span class='Ledger_Name'>" + ds1.Tables[0].Rows[j]["Ledger_Name"].ToString() + "</span>");
                                            htmlStr.Append("\t \t \t<span class='Ledger_Amt'>" + ds1.Tables[0].Rows[j]["Tx_Amount"].ToString() + "");
                                            htmlStr.Append("\t" + ds1.Tables[0].Rows[j]["AmtType"].ToString() + "</span></p>");
                                            htmlStr.Append("<br/>");

                                        }
                                    }
                                }
                                //htmlStr.Append("\n<p class='subledger'><span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span></p>");
                                htmlStr.Append("\n<p class='subledger'>");
                                if (chkNarration.Checked == true)
                                    htmlStr.Append("<span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span>");
                                htmlStr.Append("</p>");

                                htmlStr.Append("</td>");
                            }
                            else
                            {
                                // htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "</td>");

                                //htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                                //htmlStr.Append("\n<p class='subledger'><span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span></p>");
                                //htmlStr.Append("</td>");

                                if (chkOppositeLedger.Checked == true)
                                {
                                    htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["Ledger_Name"].ToString() + "");
                                }
                                else
                                {
                                    htmlStr.Append("<td>");
                                }
                                htmlStr.Append("\n<p class='subledger'>");
                                if (chkNarration.Checked == true)
                                    htmlStr.Append("<span class='Narration'> <b>Narration</b>\t : \t" + Narration + "</span>");
                                htmlStr.Append("</p>");
                                htmlStr.Append("</td>");


                            }
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Type"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_No"].ToString() + "</td>");
                            if (chkDebitAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'>" + ds.Tables[0].Rows[i]["DebitAmt"].ToString() + "</td>");
                            if (chkCreditAmt.Checked == true)
                                htmlStr.Append("<td class='align-right'></td>");
                            if (chkClosingBal.Checked == true)
                                htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBal).ToString() + " " + AmtType + "</td>");

                            htmlStr.Append("<td class='hide_print'>  <a class='label label-info' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["Office_ID"].ToString()) + "' target='_blank'>View</a> ");
                            if (ds.Tables[0].Rows[i]["Office_ID"].ToString() == ViewState["Office_ID"].ToString())
                            {
                                if (ValidDays != "No")
                                {
                                    htmlStr.Append("  <a class='label label-primary' href='" + ds.Tables[0].Rows[i]["PageURL"].ToString() + "?VoucherTx_ID=" + objdb.Encrypt(ds.Tables[0].Rows[i]["VoucherTx_ID"].ToString()) + "&Action=" + objdb.Encrypt("2") + "' target='_blank'>Edit</a>");

                                }
                            }
                            htmlStr.Append("</td>");



                            htmlStr.Append("</tr>");
                        }
                    }
                }

                //htmlStr.Append("</tbody>");
                //htmlStr.Append("<tfoot>");
                htmlStr.Append("<tr style='font-weight: 700;'>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Total :</td>");

                if (decimal.Parse(ds.Tables[0].Rows[0]["OpeningBalance"].ToString()) >= 0)
                    AmtType = "Cr";
                else
                    AmtType = "Dr";

                htmlStr.Append("<td></td>");

                if (OpeningBalance >= 0)
                    AmtType = "Cr";
                else
                    AmtType = "Dr";
                if (chkDebitAmt.Checked == true)
                    htmlStr.Append("<td class='align-right'>" + TDebitAmt.ToString() + "</td>");
                if (chkCreditAmt.Checked == true)
                    htmlStr.Append("<td class='align-right'>" + TCreditAmt.ToString() + "</td>");
                if (chkClosingBal.Checked == true && chkDebitAmt.Checked == false && chkCreditAmt.Checked == false)
                {
                    decimal? Cr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
                    decimal? Dr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));

                    Dr = decimal.Parse("-" + Dr.ToString());
                    OpeningBalance = OpeningBalance + decimal.Parse(Cr.ToString()) + decimal.Parse(Dr.ToString());
                    if (OpeningBalance >= 0)
                    {
                        htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Cr</td>");
                    }
                    else
                    {
                        htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " Dr</td>");
                    }

                }
                else if (chkClosingBal.Checked == true)
                    htmlStr.Append("<td class='align-right ClosingBal'>" + Math.Abs(OpeningBalance).ToString() + " " + AmtType + "</td>");
                htmlStr.Append("<td class='hide_print'></td>");
                htmlStr.Append("</tr>");

                // htmlStr.Append("</tfoot>");
                htmlStr.Append("</tbody>");
                htmlStr.Append("</table>");

                DivTable.InnerHtml = htmlStr.ToString();
            }
            else
            {
                lblTab.Text = "No record found.";

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
            var watch = System.Diagnostics.Stopwatch.StartNew();

            lblMsg.Text = "";
            DivTable.InnerHtml = "";
            lblTab.Text = "";
            lblReportName.Text = "";
            if (txtLedgerName.Text != "" && txtFromDate.Text != "" && txtToDate.Text != "")
            {
                //FillGridDetail();
                FillGridDetailTesting("0");
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<div class='Hiderow'><b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span></div>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnBackN_Click(object sender, EventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            lblMsg.Text = "";
            DivTable.InnerHtml = "";
            lblTab.Text = "";
            lblReportName.Text = "";
            if (txtLedgerName.Text != "" && txtFromDate.Text != "" && txtToDate.Text != "")
            {
                FillGridTesting("0");
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<div class='Hiderow'><b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span></div>";
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
            var watch = System.Diagnostics.Stopwatch.StartNew();

            lblMsg.Text = "";
            lblReportName.Text = "";
            DivTable.InnerHtml = "";
            GvDetail.DataSource = null;
            GvDetail.DataBind();
            gvDetail1.DataSource = null;
            gvDetail1.DataBind();
            btnBack.Enabled = false;
            btnBackN.Enabled = false;
            btnBillByBill.Visible = false;
            spnSearch.Visible = false;
            txtSearch.Visible = false;
            lblDetailTotal.Text = "";
            lblTotal.Text = "";
            //if (ddlLedger.SelectedIndex > 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            //{
            //    lblReportName.Text = "Ledger Of : " + txtLedgerName.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
            //    FillGrid();
            //}



            string msg = "";
            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            if (Office == "")
            {
                msg = "Select at least one Office.";
            }
            if (txtLedgerName.Text == "")
            {
                msg += "Enter Ledger Name.\\n";
            }
            if (msg == "")
            {
                if (txtFromDate.Text != "" && txtToDate.Text != "")
                {
                    /*****************SET Search Option Start********************/
                    #region SetSearchOption
                    Session["CommonOffice"] = Office;
                    Session["CommonFromDate"] = txtFromDate.Text;
                    Session["CommonToDate"] = txtToDate.Text;
                    #endregion
                    /*****************SET Search Option End********************/


                    lblReportName.Text = "Ledger Of : " + txtLedgerName.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
                    HF_ReportName.Value = "Ledger Of - " + txtLedgerName.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )";
                    btnBillByBill.Visible = true;
                    if (ViewState["Search"].ToString() == "0")
                    {
                        //FillGrid();
                        FillGridTesting("0");
                    }
                    else
                    {
                        //FillGridDetail();
                       FillGridDetailTesting("0");
                    }

                }

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;

                lblExecTime.Text = "<div class='Hiderow'><b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span></div>";
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "hideshow();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btngraphical_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            string Office = "";
            string OfficeID = "";
            string heading = "";

            ViewState["DayBookVisible"] = "true";
            btnShowDetailBook.Enabled = false;
            GridView4.DataSource = null;
            GridView4.DataBind();


            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            if (Office == "")
            {
                msg = "Select at least one Office.\\n";
            }
            if (txtLedgerName.Text != "")
            {
                msg += "Select List Of Ledger.";
            }
            if (msg == "")
            {

                int totalListItem = ddlOffice.Items.Count;
                heading = "<p class='text-center' style='font-weight:600'>Closing Balance : " + txtLedgerName.Text + "( " + txtFromDate.Text + " - " + txtToDate.Text + " )</p>";
                Session["heading"] = heading.ToString();
                foreach (ListItem item in ddlOffice.Items)
                {
                    if (item.Selected)
                    {
                        OfficeID += item.Value + ",";
                    }
                }
                OfficeID = objdb.Encrypt(OfficeID);
                string FromDate = objdb.Encrypt(Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"));
                string ToDate = objdb.Encrypt(Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"));
                string Ledger_ID = objdb.Encrypt(hfLedgerID.Value);
                string url = "RptGraphicalLedgerSummary.aspx?OfficeID=" + OfficeID + "&FromDate=" + FromDate + "&ToDate=" + ToDate + "&Ledger_ID=" + Ledger_ID;

                StringBuilder sb = new StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.open('");
                sb.Append(url);
                sb.Append("', '_blank');");


                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
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
    protected void btnShowDetailBook_Click(object sender, EventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            if (ViewState["DayBookVisible"].ToString() == "true")
            {
                DivTable.Visible = false;
                GvDetail.DataSource = null;
                GvDetail.DataBind();
                gvDetail1.DataSource = null;
                gvDetail1.DataBind();
                ViewState["DayBookVisible"] = "false";



                GridView4.DataSource = null;
                GridView4.DataBind();


                string Office = "";

                foreach (ListItem item in ddlOffice.Items)
                {
                    if (item.Selected)
                    {
                        Office += item.Value + ",";
                    }
                }


                ds = objdb.ByProcedure("SpFinRptLedgerSummary_new", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate" }, new string[] { "4", Office, hfLedgerID.Value, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {

                    GridView4.Visible = true;

                    GridView4.DataSource = ds;
                    GridView4.DataBind();

                    decimal PreOpening = decimal.Parse(ViewState["OpeningBalance"].ToString());
                    decimal OpeningBal = 0;
                    string PreClosing = "";



                    int rowcount = ds.Tables[0].Rows.Count;

                    for (int i = 0; i < rowcount; i++)
                    {

                        string DebitAmt = "0";
                        decimal CreditAmt = 0;

                        //if (i != 0)
                        //{
                        if (ds.Tables[0].Rows[i]["DebitAmt"].ToString() != "")
                            DebitAmt = "-" + ds.Tables[0].Rows[i]["DebitAmt"].ToString();

                        if (ds.Tables[0].Rows[i]["CreditAmt"].ToString() != "")
                            CreditAmt = decimal.Parse(ds.Tables[0].Rows[i]["CreditAmt"].ToString());
                        //}

                        if (i == 0)
                        {

                            OpeningBal = PreOpening + decimal.Parse(DebitAmt) + CreditAmt;

                        }
                        else
                        {
                            OpeningBal = OpeningBal + decimal.Parse(DebitAmt) + CreditAmt;
                        }

                        //  OpeningBal = OpeningBal + decimal.Parse(DebitAmt) + CreditAmt;
                        if (OpeningBal >= 0)
                        {
                            GridView4.Rows[i].Cells[3].Text = OpeningBal.ToString() + " Cr";
                            //  GridView4.Rows[i].BackColor = System.Drawing.Color.Bisque;
                        }
                        else
                        {
                            GridView4.Rows[i].Cells[3].Text = Math.Abs(OpeningBal).ToString() + " Dr";
                        }
                    }
                    GridView4.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView4.UseAccessibleHeader = true;
                }
            }
            else
            {
                DivTable.Visible = true;
                GridView4.Visible = false;
                ViewState["DayBookVisible"] = "true";
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            lblExecTime.Text = "<div class='Hiderow'><b>Report Execution Time:</b> <span style='color: #3c8dbc; font-weight:bold; text-decoration:underline'>" + Math.Round(TimeSpan.FromMilliseconds((double)elapsedMs).TotalSeconds, 2).ToString() + " Seconds</span><div>";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnBillByBill_Click(object sender, EventArgs e)
    {
        try
        {


            GridViewRefDetail.DataSource = new string[] { };
            GridViewRefDetail.DataBind();

            string Ledger_ID = hfLedgerID.Value;
            lblLedgerName.Text = txtLedgerName.ToString();
            lblBillTotal.Text = "";
            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if (item.Selected)
                {
                    Office += item.Value + ",";
                }
            }
            ds = objdb.ByProcedure("SpFinRptTrialBalanceNewFF", new string[] { "flag", "Ledger_ID", "Office_ID_Mlt", "FromDate", "ToDate" }, new string[] { "15", Ledger_ID, Office, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridViewRefDetail.DataSource = ds.Tables[0];
                GridViewRefDetail.DataBind();
                lblBillTotal.Text = ds.Tables[1].Rows[0]["ClosingTotal"].ToString();
            }

            // lblLedgerName.Text = btnlink.Text;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowBillDetailModal();", true);


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



    //protected void GvDetail_DataBound(object sender, EventArgs e)
    //{
    //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
    //    TableHeaderCell cell = new TableHeaderCell();
    //    cell.Text = "";
    //    row.Controls.Add(cell);

    //    cell = new TableHeaderCell();
    //    cell.Text = "Opening Balance";
    //    row.Controls.Add(cell);

    //    cell = new TableHeaderCell();
    //    cell.Text = "";
    //    row.Controls.Add(cell);

    //    cell = new TableHeaderCell();
    //    cell.Text = "";
    //    row.Controls.Add(cell);

    //    cell = new TableHeaderCell();
    //    cell.Text = "";
    //    row.Controls.Add(cell);

    //    cell = new TableHeaderCell();
    //    cell.Text = "";
    //    row.Controls.Add(cell);


    //    cell = new TableHeaderCell();
    //    //cell.Text = ViewState["OpeningBalance"].ToString();
    //    cell.Text ="";
    //    row.Controls.Add(cell);


    //    cell = new TableHeaderCell();
    //    cell.Text = "";
    //    row.Controls.Add(cell);

    //    row.BackColor = ColorTranslator.FromHtml("#faebd7");
    //    GvDetail.HeaderRow.Parent.Controls.AddAt(0, row);
    //}
    protected void GvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       // GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
       
        
        if(e.CommandName == "View")
        {
            //string VoucherTx_ID = e.CommandArgument.ToString();
            GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            //GridViewRow gvr = (GridViewRow)((HyperLink)e.CommandSource).NamingContainer;

            int RowIndex = gvr.RowIndex;
            Label lblVoucherTx_ID = (Label)GvDetail.Rows[RowIndex].Cells[8].FindControl("lblVoucherTx_ID");
            Label lblOffice_ID = (Label)GvDetail.Rows[RowIndex].Cells[8].FindControl("lblOffice_ID");
            Label lblPageURL = (Label)GvDetail.Rows[RowIndex].Cells[8].FindControl("lblPageURL");
            //HyperLink lnkView = (HyperLink)GvDetail.Rows[RowIndex].Cells[8].FindControl("lnkView");
            string url = lblPageURL.Text + "?VoucherTx_ID=" + objdb.Encrypt(lblVoucherTx_ID.Text) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(lblOffice_ID.Text);

            //lnkView.NavigateUrl = url;
            //Response.Redirect(url);
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_blank');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
 	else if (e.CommandName == "EditRecord")
        {
            //string VoucherTx_ID = e.CommandArgument.ToString();
            GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            //GridViewRow gvr = (GridViewRow)((HyperLink)e.CommandSource).NamingContainer;

            int RowIndex = gvr.RowIndex;
            Label lblVoucherTx_ID = (Label)GvDetail.Rows[RowIndex].Cells[8].FindControl("lblVoucherTx_ID");
            Label lblOffice_ID = (Label)GvDetail.Rows[RowIndex].Cells[8].FindControl("lblOffice_ID");
            Label lblPageURL = (Label)GvDetail.Rows[RowIndex].Cells[8].FindControl("lblPageURL");
            //HyperLink lnkView = (HyperLink)GvDetail.Rows[RowIndex].Cells[8].FindControl("lnkView");
            string url = lblPageURL.Text + "?VoucherTx_ID=" + objdb.Encrypt(lblVoucherTx_ID.Text) + "&Action=" + objdb.Encrypt("2") + "&Office_ID=" + objdb.Encrypt(lblOffice_ID.Text);

            //lnkView.NavigateUrl = url;
            //Response.Redirect(url);
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_blank');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
    }

    protected void gvDetail1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            //string VoucherTx_ID = e.CommandArgument.ToString();
            GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

            int RowIndex = gvr.RowIndex;
            Label lblVoucherTx_ID = (Label)gvDetail1.Rows[RowIndex].Cells[8].FindControl("lblVoucherTx_ID");
            Label lblOffice_ID = (Label)gvDetail1.Rows[RowIndex].Cells[8].FindControl("lblOffice_ID");
            Label lblPageURL = (Label)gvDetail1.Rows[RowIndex].Cells[8].FindControl("lblPageURL");
            string url = lblPageURL.Text + "?VoucherTx_ID=" + objdb.Encrypt(lblVoucherTx_ID.Text) + "&Action=" + objdb.Encrypt("1") + "&Office_ID=" + objdb.Encrypt(lblOffice_ID.Text);

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_blank');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
	else if (e.CommandName == "EditRecord")
        {
            //string VoucherTx_ID = e.CommandArgument.ToString();
            GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

            int RowIndex = gvr.RowIndex;
            Label lblVoucherTx_ID = (Label)gvDetail1.Rows[RowIndex].Cells[8].FindControl("lblVoucherTx_ID");
            Label lblOffice_ID = (Label)gvDetail1.Rows[RowIndex].Cells[8].FindControl("lblOffice_ID");
            Label lblPageURL = (Label)gvDetail1.Rows[RowIndex].Cells[8].FindControl("lblPageURL");
            string url = lblPageURL.Text + "?VoucherTx_ID=" + objdb.Encrypt(lblVoucherTx_ID.Text) + "&Action=" + objdb.Encrypt("2") + "&Office_ID=" + objdb.Encrypt(lblOffice_ID.Text);

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_blank');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        try
        {


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void btnExporttotexcel_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GvDetail.AllowPaging = false;
            gvDetail1.AllowPaging = false;
            txtSearch.Visible = false;
            if(ViewState["Search"].ToString() == "1")
            {
                GvDetail.DataSource = null;
                GvDetail.DataBind();
                FillGridDetailTesting("0");
                gvDetail1.Columns[7].Visible = false;
                //DataSet dsDetail = (DataSet)Session["dsDetail"];
                //gvDetail1.DataSource = dsDetail.Tables[0];
                //gvDetail1.DataBind();
                //int Count = dsDetail.Tables[0].Rows.Count;
                //decimal? Cr = dsDetail.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
                //decimal? Dr = dsDetail.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));
                ////decimal? CB = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CB"));
                //decimal? CB = decimal.Parse(dsDetail.Tables[0].Rows[Count - 1]["CB"].ToString());
                //string AmtType = "";
                //if (CB >= 0)
                //{
                //    AmtType = "Cr";
                //}
                //else
                //{
                //    AmtType = "Dr";
                //}
                //gvDetail1.FooterRow.Cells[2].Text = "<b>Total : </b>";
                //gvDetail1.FooterRow.Cells[4].Text = "<b>" + Dr.ToString() + "</b>";
                //gvDetail1.FooterRow.Cells[5].Text = "<b>" + Cr.ToString() + "</b>";
                //gvDetail1.FooterRow.Cells[6].Text = "<b>" + (CB.ToString() + " " + AmtType) + "</b>";
                //gvDetail1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                //gvDetail1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                //gvDetail1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right; 
                //
            }

            else
            {
                gvDetail1.DataSource = null;
                gvDetail1.DataBind();
                FillGridTesting("0");
                GvDetail.Columns[7].Visible = false;
                //DataSet dswithoutDetail = (DataSet)Session["dswithoutDetail"];
                //GvDetail.DataSource = dswithoutDetail.Tables[0];
                //GvDetail.DataBind();
                //int Count1 = dswithoutDetail.Tables[0].Rows.Count;
                //decimal? Cr1 = dswithoutDetail.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
                //decimal? Dr1 = dswithoutDetail.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));
                ////decimal? CB = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CB"));
                //decimal? CB1 = decimal.Parse(dswithoutDetail.Tables[0].Rows[Count1 - 1]["CB"].ToString());
                //string AmtType1 = "";
                //if (CB1 >= 0)
                //{
                //    AmtType1 = "Cr";
                //}
                //else
                //{
                //    AmtType1 = "Dr";
                //}
                //GvDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
                //GvDetail.FooterRow.Cells[4].Text = "<b>" + Dr1.ToString() + "</b>";
                //GvDetail.FooterRow.Cells[5].Text = "<b>" + Cr1.ToString() + "</b>";
                //GvDetail.FooterRow.Cells[6].Text = "<b>" + (CB1.ToString() + " " + AmtType1) + "</b>";
                //GvDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                //GvDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                //GvDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                //
            }



            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "tableToExcel('tableData', 'W3C Example Table');", true);
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "LedgerReport" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            tableData.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            GvDetail.AllowPaging = true;
            gvDetail1.AllowPaging = true;
            Response.End();

            //string attachment = "attachment; filename=Contacts.xls";
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", attachment);
            //Response.ContentType = "application/ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //// Create a form to contain the grid
            //HtmlForm frm = new HtmlForm();
            //GvDetail.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(GvDetail);
            //frm.RenderControl(htw);
            //GvDetail.AllowPaging = true;
            //gvDetail1.AllowPaging = true;
            ////GridView1.RenderControl(htw);
            //Response.Write(sw.ToString());
            //Response.End();



           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
    protected void BindGrid()
    {

        DataTable dt = (DataTable)Session["dswithoutDetail"];
        GvDetail.DataSource = dt;
        GvDetail.DataBind();
        if (chkDebitAmt.Checked == false)
        {
            GvDetail.Columns[4].Visible = false;
        }
        else
        {
            GvDetail.Columns[4].Visible = true;
        }
        if (chkCreditAmt.Checked == false)
        {
            GvDetail.Columns[5].Visible = false;
        }
        else
        {
            GvDetail.Columns[5].Visible = true;
        }
        if (chkClosingBal.Checked == false)
        {
            GvDetail.Columns[6].Visible = false;
        }
        else
        {
            GvDetail.Columns[6].Visible = true;
        }

        int Count = dt.Rows.Count;
        decimal? Cr = dt.AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
        decimal? Dr = dt.AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));
        //decimal? CB = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CB"));
        decimal? CB = decimal.Parse(dt.Rows[Count - 1]["CB"].ToString());
        string AmtType = "";
        if (CB >= 0)
        {
            AmtType = "Cr";
        }
        else
        {
            AmtType = "Dr";
        }
        GvDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
        GvDetail.FooterRow.Cells[4].Text = "<b>" + Dr.ToString() + "</b>";
        GvDetail.FooterRow.Cells[5].Text = "<b>" + Cr.ToString() + "</b>";
        GvDetail.FooterRow.Cells[6].Text = "<b>" + (CB.ToString() + " " + AmtType) + "</b>";
        GvDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
        GvDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
        GvDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
    }
    protected void DetailedBindGrid()
    {
        DataTable dt = (DataTable)Session["dsDetail"];
        gvDetail1.DataSource = dt;
        gvDetail1.DataBind();
        if (chkDebitAmt.Checked == false)
        {
            gvDetail1.Columns[4].Visible = false;
        }
        else
        {
            gvDetail1.Columns[4].Visible = true;
        }
        if (chkCreditAmt.Checked == false)
        {
            gvDetail1.Columns[5].Visible = false;
        }
        else
        {
            gvDetail1.Columns[5].Visible = true;
        }
        if (chkClosingBal.Checked == false)
        {
            gvDetail1.Columns[6].Visible = false;
        }
        else
        {
            gvDetail1.Columns[6].Visible = true;
        }
        int Count = dt.Rows.Count;
        decimal? Cr = dt.AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
        decimal? Dr = dt.AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));
        //decimal? CB = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CB"));
        decimal? CB = decimal.Parse(dt.Rows[Count - 1]["CB"].ToString());
        string AmtType = "";
        if (CB >= 0)
        {
            AmtType = "Cr";
        }
        else
        {
            AmtType = "Dr";
        }
        gvDetail1.FooterRow.Cells[2].Text = "<b>Total : </b>";
        gvDetail1.FooterRow.Cells[4].Text = "<b>" + Dr.ToString() + "</b>";
        gvDetail1.FooterRow.Cells[5].Text = "<b>" + Cr.ToString() + "</b>";
        gvDetail1.FooterRow.Cells[6].Text = "<b>" + (CB.ToString() + " " + AmtType) + "</b>";
        gvDetail1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
        gvDetail1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
        gvDetail1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
    }
    protected void GvDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GvDetail.PageIndex = e.NewPageIndex;
        //BindGrid();
         string VoucherNo = "";
        VoucherNo = txtSearch.Text;
        FillGridTesting(VoucherNo);

    }
    protected void gvDetail1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetail1.PageIndex = e.NewPageIndex;
       // DetailedBindGrid();
         string VoucherNo = "";
        VoucherNo = txtSearch.Text;
       // DetailedBindGrid();
        FillGridDetailTesting(VoucherNo);
    }
    protected void SearchCustomers()
    {
        if(ViewState["Search"] == "1")
        {
            string VoucherNo = "";
            VoucherNo = txtSearch.Text;
            FillGridDetailTesting(VoucherNo);
            //GvDetail.DataSource = null;
            //GvDetail.DataBind();
            //DataView dv = new DataView();
            //DataSet dsDetail = (DataSet)Session["dsDetail"];
            //dv = dsDetail.Tables[0].DefaultView;
            //dv.RowFilter = "VoucherTx_No like '%" + txtSearch.Text + "%'";

            //DataTable dt = dv.ToTable();

            //gvDetail1.DataSource = dt;
            //gvDetail1.DataBind();
            //int Count = dt.Rows.Count;
            //decimal? Cr = dt.AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
            //decimal? Dr = dt.AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));
            ////decimal? CB = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CB"));
            //decimal? CB = decimal.Parse(dt.Rows[Count - 1]["CB"].ToString());
            //string AmtType = "";
            //if (CB >= 0)
            //{
            //    AmtType = "Cr";
            //}
            //else
            //{
            //    AmtType = "Dr";
            //}
            //gvDetail1.FooterRow.Cells[2].Text = "<b>Total : </b>";
            //gvDetail1.FooterRow.Cells[4].Text = "<b>" + Dr.ToString() + "</b>";
            //gvDetail1.FooterRow.Cells[5].Text = "<b>" + Cr.ToString() + "</b>";
            //gvDetail1.FooterRow.Cells[6].Text = "<b>" + (CB.ToString() + " " + AmtType) + "</b>";
            //gvDetail1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            //gvDetail1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            //gvDetail1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            //if (chkDebitAmt.Checked == false)
            //{
            //    gvDetail1.Columns[4].Visible = false;
            //}
            //else
            //{
            //    gvDetail1.Columns[4].Visible = true;
            //}
            //if (chkCreditAmt.Checked == false)
            //{
            //    gvDetail1.Columns[5].Visible = false;
            //}
            //else
            //{
            //    gvDetail1.Columns[5].Visible = true;
            //}
            //if (chkClosingBal.Checked == false)
            //{
            //    gvDetail1.Columns[6].Visible = false;
            //}
            //else
            //{
            //    gvDetail1.Columns[6].Visible = true;
            //}

        }
        else
        {
            string VoucherNo = "";
            VoucherNo = txtSearch.Text;
            FillGridTesting(VoucherNo);
            //gvDetail1.DataSource = null;
            //gvDetail1.DataBind();
            //DataView dv = new DataView();
            //DataSet dswithoutDetail = (DataSet)Session["dswithoutDetail"];
            //dv = dswithoutDetail.Tables[0].DefaultView;
            //dv.RowFilter = "VoucherTx_No like '%" + txtSearch.Text + "%'";
            //DataTable dt = dv.ToTable();
            //GvDetail.DataSource = dt;
            //GvDetail.DataBind();
            //int Count1 = dt.Rows.Count;
            //decimal? Cr1 = dt.AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
            //decimal? Dr1 = dt.AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));
            ////decimal? CB = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CB"));
            //decimal? CB1 = decimal.Parse(dt.Rows[Count1 - 1]["CB"].ToString());
            //string AmtType1 = "";
            //if (CB1 >= 0)
            //{
            //    AmtType1 = "Cr";
            //}
            //else
            //{
            //    AmtType1 = "Dr";
            //}
            //GvDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
            //GvDetail.FooterRow.Cells[4].Text = "<b>" + Dr1.ToString() + "</b>";
            //GvDetail.FooterRow.Cells[5].Text = "<b>" + Cr1.ToString() + "</b>";
            //GvDetail.FooterRow.Cells[6].Text = "<b>" + (CB1.ToString() + " " + AmtType1) + "</b>";
            //GvDetail.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            //GvDetail.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            //GvDetail.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            //if (chkDebitAmt.Checked == false)
            //{
            //    GvDetail.Columns[4].Visible = false;
            //}
            //else
            //{
            //    GvDetail.Columns[4].Visible = true;
            //}
            //if (chkCreditAmt.Checked == false)
            //{
            //    GvDetail.Columns[5].Visible = false;
            //}
            //else
            //{
            //    GvDetail.Columns[5].Visible = true;
            //}
            //if (chkClosingBal.Checked == false)
            //{
            //    GvDetail.Columns[6].Visible = false;
            //}
            //else
            //{
            //    GvDetail.Columns[6].Visible = true;
            //}
                    

        }
        
    }
   

    private void SearchText()
    {
        if (ViewState["Search"].ToString() == "1")
        {
            string VoucherNo = "";
            VoucherNo = txtSearch.Text;
            FillGridDetailTesting(VoucherNo);
         

        }
        else
        {
            string VoucherNo = "";
            VoucherNo = txtSearch.Text;
            FillGridTesting(VoucherNo);
           
        }
          

    }

    //this function returns the highlighted code html
    public string Highlight(string InputTxt)
    {
        string Search_Str = txtSearch.Text.ToString();
        // Setup the regular expression and add the Or operator.
        Regex RegExp = new Regex(Search_Str.Replace(" ", "|").Trim(),
        RegexOptions.IgnoreCase);

        // Highlight keywords by calling the 
        //delegate each time a keyword is found.
        return RegExp.Replace(InputTxt,
        new MatchEvaluator(ReplaceKeyWords));



    }
    //wrap the hightlighted code inside html
    public string ReplaceKeyWords(Match m)
    {

        return "<span class='HighLightedWord'>" + m.Value + "</span>";

    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        SearchText();
    }
    protected void GetLedgerName()
    {
        try
        {
            ds6 = null;
            string Office = "";
            foreach (ListItem item in ddlOffice.Items)
            {
                if(item.Selected)
                {
                    Office += item.Value + ",";
                }
                   
                
            }
            if (Office != "")
            {
                DataSet ds1 = objdb.ByProcedure("SpFinRptLedgerSummary_new", new string[] { "flag", "Office_ID_Mlt" }, new string[] { "8", Office }, "dataset");             
                ds6 = ds1;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> SearchLedger(string Ledger_Name)
  {
       
        List<string> Ledgers = new List<string>();
        try
        {
            DataView dv = new DataView();
            dv = ds6.Tables[0].DefaultView;
            dv.RowFilter = "Ledger_Name like '%" + Ledger_Name + "%'";
            DataTable dt = dv.ToTable();

            foreach (DataRow rs in dt.Rows)
            {
                Ledgers.Add(string.Format("{0}-Ledger_Name-{1}", rs[0], rs[1]));
                //foreach (DataColumn col in dt.Columns)
                //{
                //    Ledgers.Add(rs[col].ToString());
                //}
            }

        }
        catch { }
        return Ledgers;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
           
            GvDetail.AllowPaging = false;
            gvDetail1.AllowPaging = false;
            txtSearch.Visible = true;
            if (ViewState["Search"].ToString() == "1")
            {
                GvDetail.DataSource = null;
                GvDetail.DataBind();
                FillGridDetailTesting("0");
                
                //DataSet dsDetail = (DataSet)Session["dsDetail"];
                //gvDetail1.DataSource = dsDetail.Tables[0];
                //gvDetail1.DataBind();
                //int Count = dsDetail.Tables[0].Rows.Count;
                //decimal? Cr = dsDetail.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CreditAmt"));
                //decimal? Dr = dsDetail.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("DebitAmt"));
                ////decimal? CB = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>("CB"));
                //decimal? CB = decimal.Parse(dsDetail.Tables[0].Rows[Count - 1]["CB"].ToString());
                //string AmtType = "";
                //if (CB >= 0)
                //{
                //    AmtType = "Cr";
                //}
                //else
                //{
                //    AmtType = "Dr";
                //}
                //gvDetail1.FooterRow.Cells[2].Text = "<b>Total : </b>";
                //gvDetail1.FooterRow.Cells[4].Text = "<b>" + Dr.ToString() + "</b>";
                //gvDetail1.FooterRow.Cells[5].Text = "<b>" + Cr.ToString() + "</b>";
                //gvDetail1.FooterRow.Cells[6].Text = "<b>" + (CB.ToString() + " " + AmtType) + "</b>";
                //gvDetail1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                //gvDetail1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                //gvDetail1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right; 
                //
            }

            else
            {
                gvDetail1.DataSource = null;
                gvDetail1.DataBind();
                FillGridTesting("0");
                
              
            }
           
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "window.print();", true);
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.print();");
  
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
            
            GvDetail.AllowPaging = true;
            gvDetail1.AllowPaging = true;          

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
