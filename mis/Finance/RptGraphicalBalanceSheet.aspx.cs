    using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_RptGraphicalBalanceSheet : System.Web.UI.Page
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
                        //string headingfirst = objdb.Decrypt(Request.QueryString["headingFirst"].ToString());
                        if (Session["headingBS"].ToString() != null)
                        {
                            lblheadingFirst.Text = Session["headingBS"].ToString();
                        }
                        ViewState["FromDate"] = objdb.Decrypt(Request.QueryString["FromDate"].ToString());
                        DateTime FDate = Convert.ToDateTime(ViewState["FromDate"].ToString());                        
                        ViewState["ToDate"] = objdb.Decrypt(Request.QueryString["ToDate"].ToString());
                        DateTime TDate = Convert.ToDateTime(ViewState["ToDate"].ToString());                       
                        string FY = GetCurrentFinancialYear();                        
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
        ds = objdb.ByProcedure("SpFinRptGraphicalBalanceSheet", new string[] { "flag", "Office_ID_Mlt", "FromDate", "ToDate" }, new string[] { "1", ViewState["OfficeID"].ToString(), Convert.ToDateTime(ViewState["FromDate"].ToString(), cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(ViewState["ToDate"].ToString(), cult).ToString("yyyy/MM/dd") }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
           

            StringBuilder sb = new StringBuilder();
            sb.Append("<script>");
            sb.Append("Highcharts.chart('container', {");
            sb.Append("chart: {plotBackgroundColor: null,plotBorderWidth: null,plotShadow: false, type: 'pie'},");
            sb.Append("title: { text: 'Liabilities'},");
            sb.Append("tooltip: {pointFormat: '{series.name}: <b>{point.y:.2f}</b>'},");
            sb.Append("plotOptions: { pie: {allowPointSelect: true,cursor: 'pointer',dataLabels: {enabled: true,format: '<b>{point.name}</b>: {point.y:.2f}'}}},");
            sb.Append("series: [{name: 'Txn',colorByPoint: true,data: [");

            int Count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < Count; i++)
            {
                string Amount = ds.Tables[0].Rows[i]["TxnAmt"].ToString();
                if (Amount.Contains("-"))
                {
                    sb.Append("{name:'" + ds.Tables[0].Rows[i]["Head_Name"].ToString() + "(-)',");
                    string[] Amt = Amount.Split(')');
                    Amount = Amt[1].ToString();
                }
                else
                {
                     sb.Append("{name:'" + ds.Tables[0].Rows[i]["Head_Name"].ToString() + "',");
                }
               
               

                sb.Append("y:" + Amount.ToString() + ",");
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
            sb1.Append("title: { text: 'Assets'},");
            sb.Append("tooltip: {pointFormat: '{series.name}: <b>{point.y:.2f}</b>'},");
            sb1.Append("plotOptions: { pie: {allowPointSelect: true,cursor: 'pointer',dataLabels: {enabled: true,format: '<b>{point.name}</b>: {point.y:.2f}'}}},");
            sb1.Append("series: [{name: 'Txn',colorByPoint: true,data: [");

            int Count1 = ds.Tables[1].Rows.Count;
            for (int i = 0; i < Count1; i++)
            {
                 string Amount = ds.Tables[1].Rows[i]["TxnAmt"].ToString();
                 if (Amount.Contains("-"))
                 {
                     sb1.Append("{name:'" + ds.Tables[1].Rows[i]["Head_Name"].ToString() + "(-)',");
                     string[] Amt = Amount.Split(')');
                     Amount = Amt[1].ToString();
                 }
                 else
                 {
                     sb1.Append("{name:'" + ds.Tables[1].Rows[i]["Head_Name"].ToString() + "',");
                 }

                
                sb1.Append("y:" + Amount.ToString() + ",");                     
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