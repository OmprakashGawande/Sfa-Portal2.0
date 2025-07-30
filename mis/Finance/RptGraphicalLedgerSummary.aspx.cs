using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_RptGraphicalLedgerSummary : System.Web.UI.Page
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
                    if (Request.QueryString["OfficeID"] != null && Request.QueryString["FromDate"] != null && Request.QueryString["ToDate"] != null)
                    {
                        ViewState["OfficeID"] = objdb.Decrypt(Request.QueryString["OfficeID"].ToString());

                        if (Session["heading"].ToString() != null)
                        {
                            lblheadingFirst.Text = Session["heading"].ToString();
                        }                    
                        ViewState["FromDate"] = objdb.Decrypt(Request.QueryString["FromDate"].ToString());                        
                        ViewState["ToDate"] = objdb.Decrypt(Request.QueryString["ToDate"].ToString());
                        ViewState["Ledger_ID"] = objdb.Decrypt(Request.QueryString["Ledger_ID"].ToString());
                        FillChart();

                    }
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
    protected void FillChart()
    {
        ds = objdb.ByProcedure("SpFinRptGraphicalLedgerSummary", new string[] { "flag", "Office_ID_Mlt", "Ledger_ID", "FromDate", "ToDate" }, new string[] { "0", ViewState["OfficeID"].ToString(), ViewState["Ledger_ID"].ToString(), Convert.ToDateTime(ViewState["FromDate"].ToString(), cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(ViewState["ToDate"].ToString(), cult).ToString("yyyy/MM/dd") }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            DataView dv = new DataView();
            dv = ds.Tables[0].DefaultView;
            dv.RowFilter = "ClosingBalance > 0";
            DataTable dtCR = dv.ToTable();
            dv.RowFilter = "ClosingBalance < 0";
            DataTable dtDR = dv.ToTable();
            
            StringBuilder sb = new StringBuilder();
            sb.Append("<script>");
            sb.Append("Highcharts.chart('container', {");
            sb.Append("chart: {plotBackgroundColor: null,plotBorderWidth: null,plotShadow: false, type: 'pie'},");
            sb.Append("title: { text: 'Closing Balance(In Cr)'},");
            sb.Append("tooltip: {pointFormat: '{series.name}: <b>{point.y:.2f}</b>'},");
            sb.Append("plotOptions: { pie: {allowPointSelect: true,cursor: 'pointer',dataLabels: {enabled: true,format: '<b>{point.name}</b>: {point.y:.2f}'}}},");
            sb.Append("series: [{name: 'Closing Balance',colorByPoint: true,data: [");

            int Count = dtCR.Rows.Count;
            for (int i = 0; i < Count; i ++ )
            {
                sb.Append("{name:'" + dtCR.Rows[i]["MonthName"].ToString() + "',");
                string Amount = dtCR.Rows[i]["ClosingBalance"].ToString();               
                sb.Append("y: " + Amount.ToString() + ",");
                sb.Append("sliced: false,selected: true");
                sb.Append("},");
            }

            sb.Append("] }]});");
            
            sb.Append("</script>");
            divchartCr.InnerHtml = sb.ToString();
            
            StringBuilder sb1 = new StringBuilder();
            sb1.Append("<script>");
            sb1.Append("Highcharts.chart('container1', {");
            sb1.Append("chart: {plotBackgroundColor: null,plotBorderWidth: null,plotShadow: false, type: 'pie'},");
            sb1.Append("title: { text: 'Closing Balance(In Dr)'},");
            sb.Append("tooltip: {pointFormat: '{series.name}: <b>{point.y:.2f}</b>'},");
            sb1.Append("plotOptions: { pie: {allowPointSelect: true,cursor: 'pointer',dataLabels: {enabled: true,format: '<b>{point.name}</b>: {point.y:.2f}'}}},");
            sb1.Append("series: [{name: 'Closing Balance',colorByPoint: true,data: [");

            int Count1 = dtDR.Rows.Count;
            for (int i = 0; i < Count1; i++)
            {
                sb1.Append("{name:'" + dtDR.Rows[i]["MonthName"].ToString() + "',");
                decimal Amt = Math.Abs(decimal.Parse(dtDR.Rows[i]["ClosingBalance"].ToString()));
                string Amount = Amt.ToString();
                sb1.Append("y: " + Amount.ToString() + ",");
                sb1.Append("sliced: false,selected: true");
                sb1.Append("},");
            }

            sb1.Append("] }]});");

            sb1.Append("</script>");
            divchartDr.InnerHtml = sb1.ToString();




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
}